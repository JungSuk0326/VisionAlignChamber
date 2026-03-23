using System;
using System.Threading;
using System.Threading.Tasks;
using VisionAlignChamber.Config;
using VisionAlignChamber.Hardware.Facade;
using VisionAlignChamber.Vision;
using VisionAlignChamber.Interfaces;
using VisionAlignChamber.Models;
using VisionAlignChamber.Log;

namespace VisionAlignChamber.Core
{
    /// <summary>
    /// Vision Align Chamber 측정 시퀀스
    /// PreCtr(FOV) -> Ready -> Scan(0°→360°, xN) -> Align(Center+Theta) -> Eddy
    /// ScanStart/Rewind는 무한 회전 구조로 불필요 (주석 처리)
    /// Receive/Release는 CTC 상태 전환(GetReady/TransferReady)으로 처리
    /// </summary>
    public class VisionAlignerSequence
    {
        #region Enums

        /// <summary>
        /// 시퀀스 스텝 정의 (7-Step)
        /// Receive/Release는 CTC 상태 전환으로 처리
        /// </summary>
        public enum SequenceStep
        {
            Idle = 0,
            PreCenter = 1,    // Step 1: FOV용 Pre-Centering
            Ready = 2,        // Step 2: Vision 스캔 준비
            ScanStart = 3,    // Step 3: 스캔 시작 위치
            Scan = 4,         // Step 4: Vision 스캔 (xN)
            Rewind = 5,       // Step 5: Theta Rewind
            Align = 6,        // Step 6: Center + Theta 정렬
            Eddy = 7,         // Step 7: Eddy Current 측정
            Complete = 8,     // 완료
            Error = -1        // 에러
        }

        /// <summary>
        /// 시퀀스 실행 상태
        /// </summary>
        public enum SequenceState
        {
            Idle,
            Running,
            Paused,
            Completed,
            Error,
            Aborted
        }

        #endregion

        #region Fields

        private readonly VisionAlignerMotion _motion;
        private readonly VisionAlignerIO _io;
        private readonly VisionAlignWrapper _vision;
        private readonly IEddyCurrentSensor _eddy;
        private readonly TeachingParameter _param;

        private CancellationTokenSource _cts;
        private SequenceStep _currentStep;
        private SequenceState _state;
        private bool _isFlat; // Flat 타입 웨이퍼 여부
        private int? _overrideImageCount;    // VisionPanel에서 전달된 Count (null이면 _param 사용)
        private double? _overrideStepAngle;  // VisionPanel에서 전달된 Angle (null이면 _param 사용)

        // Vision 결과
        private WaferVisionResult _visionResult;

        #endregion

        #region Properties

        /// <summary>
        /// 현재 스텝
        /// </summary>
        public SequenceStep CurrentStep
        {
            get => _currentStep;
            private set
            {
                if (_currentStep != value)
                {
                    _currentStep = value;
                    StepChanged?.Invoke(this, value);
                }
            }
        }

        /// <summary>
        /// 시퀀스 상태
        /// </summary>
        public SequenceState State
        {
            get => _state;
            private set
            {
                if (_state != value)
                {
                    _state = value;
                    StateChanged?.Invoke(this, value);
                }
            }
        }

        /// <summary>
        /// 실행 중 여부
        /// </summary>
        public bool IsRunning => State == SequenceState.Running;

        /// <summary>
        /// Vision 측정 결과
        /// </summary>
        public WaferVisionResult VisionResult => _visionResult;

        /// <summary>
        /// Flat 타입 웨이퍼 여부
        /// </summary>
        public bool IsFlat => _isFlat;

        /// <summary>
        /// 마지막 에러 메시지
        /// </summary>
        public string LastError { get; private set; }

        #endregion

        #region Events

        /// <summary>
        /// 스텝 변경 이벤트
        /// </summary>
        public event EventHandler<SequenceStep> StepChanged;

        /// <summary>
        /// 상태 변경 이벤트
        /// </summary>
        public event EventHandler<SequenceState> StateChanged;

        /// <summary>
        /// 시퀀스 완료 이벤트
        /// </summary>
        public event EventHandler<WaferVisionResult> SequenceCompleted;

        /// <summary>
        /// 에러 발생 이벤트 (알람 코드 포함)
        /// </summary>
        public event EventHandler<SequenceErrorEventArgs> ErrorOccurred;

        /// <summary>
        /// CTC Transfer 상태 변경 요청 이벤트
        /// Stage 입장: GetReady = 가져가도 됨, PutReady = 올려놓아도 됨
        /// </summary>
        public event EventHandler<CTCTransferStatusEventArgs> TransferStatusChangeRequested;

        #endregion

        #region Constructor

        public VisionAlignerSequence(
            VisionAlignerMotion motion,
            VisionAlignerIO io,
            VisionAlignWrapper vision,
            IEddyCurrentSensor eddy)
        {
            _motion = motion ?? throw new ArgumentNullException(nameof(motion));
            _io = io ?? throw new ArgumentNullException(nameof(io));
            _vision = vision;
            _eddy = eddy;
            _param = TeachingParameter.Instance;

            _currentStep = SequenceStep.Idle;
            _state = SequenceState.Idle;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 전체 시퀀스 실행 (7-Step)
        /// </summary>
        /// <param name="isFlat">Flat 타입 웨이퍼 여부 (false = Notch 타입)</param>
        /// <param name="skipEddy">Eddy 스텝 스킵 여부</param>
        public async Task<bool> RunSequenceAsync(bool isFlat = false, bool skipEddy = false)
        {
            if (IsRunning)
            {
                LastError = "시퀀스가 이미 실행 중입니다.";
                return false;
            }

            // 웨이퍼 센서 인터락 체크
            if (!CheckWaferSensorInterlock())
            {
                return false;
            }

            _cts = new CancellationTokenSource();
            State = SequenceState.Running;
            _visionResult = WaferVisionResult.Empty;
            _isFlat = isFlat;

            // 이전 결과 이미지 초기화 (스캔 중 라이브 이미지 표시를 위해)
            EventManager.Publish(EventManager.DisplayImageChanged, null);

            // AppState 상태 동기화 (SSOT)
            AppState.Current.SystemStatus = SystemStatus.Running;

            // CTC 상태: 측정 중 - 이송 불가
            RequestTransferStatusChange(CTCTransferStatus.NotReady);

            try
            {
                LogManager.Sequence.Info($"시퀀스 시작 (Type: {(isFlat ? "Flat" : "Notch")}, SkipEddy: {skipEddy})");

                // Step 1: PreCenter
                if (!await ExecuteStepAsync(SequenceStep.PreCenter, ExecutePreCenterAsync))
                    return false;

                // Step 2: Ready
                if (!await ExecuteStepAsync(SequenceStep.Ready, ExecuteReadyAsync))
                    return false;

                // Step 3: ScanStart - 무한 회전 구조로 불필요 (0°에서 시작)
                // if (!await ExecuteStepAsync(SequenceStep.ScanStart, ExecuteScanStartAsync))
                //     return false;

                // Step 4: Scan (0° → 360°, AngleStep씩 이동 후 촬영)
                if (!await ExecuteStepAsync(SequenceStep.Scan, ExecuteScanAsync))
                    return false;

                // Step 5: Rewind - 무한 회전 구조로 되감기 불필요
                // if (!await ExecuteStepAsync(SequenceStep.Rewind, ExecuteRewindAsync))
                //     return false;

                // Step 6: Align (Center + Theta)
                if (!await ExecuteStepAsync(SequenceStep.Align, ExecuteAlignAsync))
                    return false;

                // Step 7: Eddy - Scan 스텝에서 병렬로 측정하도록 변경 (기존 단독 스텝 주석 처리)
                // if (!skipEddy)
                // {
                //     if (!await ExecuteStepAsync(SequenceStep.Eddy, ExecuteEddyAsync))
                //         return false;
                // }
                // else
                // {
                //     LogManager.Sequence.Info("Step 7: Eddy - Skipped");
                // }

                // 완료
                CurrentStep = SequenceStep.Complete;
                State = SequenceState.Completed;
                LogManager.Sequence.Info("시퀀스 완료");



                // 결과 이미지를 공용 디스플레이에 발행 (_isFlat 기준)
                var resultImg = _vision?.GetResultImage(_isFlat);
                if (resultImg != null)
                    EventManager.Publish(EventManager.DisplayImageChanged, resultImg);

                SequenceCompleted?.Invoke(this, _visionResult);

                // CTC 상태: 측정 완료 - 웨이퍼 가져갈 수 있음
                RequestTransferStatusChange(CTCTransferStatus.GetReady);
                return true;
            }
            catch (OperationCanceledException)
            {
                State = SequenceState.Aborted;
                LogManager.Sequence.Warn("시퀀스 중단됨");
                return false;
            }
            catch (Exception ex)
            {
                SetError($"시퀀스 예외: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 웨이퍼 센서 인터락 체크
        /// </summary>
        private bool CheckWaferSensorInterlock()
        {
            if (_io == null)
            {
                LogManager.Sequence.Warn("IO 모듈이 없어 인터락 체크를 건너뜁니다.");
                return true;
            }

            if (!_io.IsWaferDetectedOnAllSensors())
            {
                SetError("웨이퍼가 감지되지 않았습니다. (Interlock)");
                return false;
            }

            LogManager.Sequence.Info("웨이퍼 센서 인터락 체크 OK");
            return true;
        }

        /// <summary>
        /// Scan만 실행 (Light ON → 이동+촬영×N → Light OFF → Vision 분석)
        /// VisionPanel Start 버튼용 - PreCenter/Ready/Align/Eddy 스킵
        /// </summary>
        /// <param name="isFlat">Flat 타입 웨이퍼 여부</param>
        public async Task<bool> RunScanOnlyAsync(bool isFlat = false)
        {
            if (IsRunning)
            {
                LastError = "시퀀스가 이미 실행 중입니다.";
                return false;
            }

            _cts = new CancellationTokenSource();
            State = SequenceState.Running;
            _visionResult = WaferVisionResult.Empty;
            _isFlat = isFlat;

            // AppState 상태 동기화 (SSOT)
            AppState.Current.SystemStatus = SystemStatus.Running;

            try
            {
                LogManager.Sequence.Info($"Scan Only 시작 (Type: {(isFlat ? "Flat" : "Notch")})");

                // Light ON
                _vision?.SetLightOn();

                // Scan 실행
                if (!await ExecuteStepAsync(SequenceStep.Scan, ExecuteScanAsync))
                    return false;

                // 완료
                CurrentStep = SequenceStep.Complete;
                State = SequenceState.Completed;
                AppState.Current.SystemStatus = SystemStatus.Idle;
                LogManager.Sequence.Info("Scan Only 완료");

                // 결과 이미지를 공용 디스플레이에 발행 (_isFlat 기준)
                var resultImg = _vision?.GetResultImage(_isFlat);
                if (resultImg != null)
                    EventManager.Publish(EventManager.DisplayImageChanged, resultImg);

                SequenceCompleted?.Invoke(this, _visionResult);
                return true;
            }
            catch (OperationCanceledException)
            {
                State = SequenceState.Aborted;
                AppState.Current.SystemStatus = SystemStatus.Idle;
                LogManager.Sequence.Warn("Scan Only 중단됨");
                return false;
            }
            catch (Exception ex)
            {
                SetError($"Scan Only 예외: {ex.Message}");
                AppState.Current.SystemStatus = SystemStatus.Error;
                return false;
            }
        }

        /// <summary>
        /// Scan만 실행 (VisionPanel용 오버로드 - Count/Angle 직접 지정)
        /// </summary>
        public async Task<bool> RunScanOnlyAsync(bool isFlat, int imageCount, double stepAngle)
        {
            _overrideImageCount = imageCount;
            _overrideStepAngle = stepAngle;
            return await RunScanOnlyAsync(isFlat);
        }

        /// <summary>
        /// 시퀀스 중지
        /// </summary>
        public void Stop()
        {
            _cts?.Cancel();
            _motion?.EmergencyStopAll();
            State = SequenceState.Aborted;
            LogManager.Sequence.Warn("시퀀스 정지 요청");
        }

        /// <summary>
        /// 시퀀스 리셋
        /// </summary>
        public void Reset()
        {
            CurrentStep = SequenceStep.Idle;
            State = SequenceState.Idle;
            _visionResult = WaferVisionResult.Empty;
            LastError = null;
        }

        /// <summary>
        /// Transfer Put 준비 (웨이퍼 수신 대기 자세)
        /// Centering Open + Chuck Z Down + Lift Pin Blow ON
        /// 로봇이 Lift Pin 위에 웨이퍼를 내려놓을 수 있는 상태
        /// </summary>
        public async Task<bool> PrepareForPutAsync()
        {
            try
            {
                _cts = new CancellationTokenSource();
                LogManager.Sequence.Info("PrepareForPut 시작");

                // Centering Open
                if (!await _motion.CenteringStagesMoveSyncAsync(
                    _param.CenterL_Open, _param.CenterR_Open,
                    _param.CenteringStage1.Velocity, ct: _cts.Token))
                {
                    LogManager.Sequence.Error("PrepareForPut: Centering Open 실패");
                    return false;
                }

                // Chuck Z Down
                if (!await _motion.WedgeStageMoveAsync(_param.ChuckZ_Down, _param.WedgeUpDown.Velocity, ct: _cts.Token))
                {
                    LogManager.Sequence.Error("PrepareForPut: Chuck Z Down 실패");
                    return false;
                }

                // Lift Pin Vacuum ON / Blow OFF (핀 올림 → 로봇이 웨이퍼 놓을 위치)
                _io.SetLiftPinBlow(false);
                _io.SetLiftPinVacuum(false);
                _io.SetChuckVacuum(false);
                _io.SetChuckBlow(false);

                /* Wafer Exist Check*/

                // Eddy SetZero (웨이퍼 없는 상태에서 영점 설정)
                if (_eddy != null && _eddy.IsConnected)
                {
                    if (!_eddy.SetZero())
                    {
                        LogManager.Sequence.Warn("Eddy SetZero 실패 - 측정은 계속 진행");
                    }
                    else
                    {
                        LogManager.Sequence.Info("Eddy SetZero 완료");
                    }
                    // 영점 안정화 대기
                    await Task.Delay(200, _cts.Token);
                }

                LogManager.Sequence.Info("PrepareForPut 완료");
                return true;
            }
            catch (Exception ex)
            {
                LogManager.Sequence.Error($"PrepareForPut 실패: {ex.Message}");
                return false;
            }
            finally
            {
                _cts?.Dispose();
                _cts = null;
            }
        }

        /// <summary>
        /// Transfer Get 준비 (웨이퍼 반출 대기 자세)
        /// Centering Open + Chuck Z Down + Lift Pin Blow ON
        /// 웨이퍼가 Lift Pin 위에 올라간 상태에서 로봇이 가져갈 수 있는 상태
        /// </summary>
        public async Task<bool> PrepareForGetAsync()
        {
            try
            {
                _cts = new CancellationTokenSource();
                LogManager.Sequence.Info("PrepareForGet 시작");

                // Centering Open
                if (!await _motion.CenteringStagesMoveSyncAsync(
                    _param.CenterL_Open, _param.CenterR_Open,
                    _param.CenteringStage1.Velocity, ct: _cts.Token))
                {
                    LogManager.Sequence.Error("PrepareForGet: Centering Open 실패");
                    return false;
                }

                // Chuck Z Down
                if (!await _motion.WedgeStageMoveAsync(_param.ChuckZ_Down, _param.WedgeUpDown.Velocity, ct: _cts.Token))
                {
                    LogManager.Sequence.Error("PrepareForGet: Chuck Z Down 실패");
                    return false;
                }

                // Lift Pin Vacuum OFF / Blow ON (웨이퍼를 핀 위로 올림 → 로봇이 가져갈 위치)
                _io.SetLiftPinVacuum(false);
                _io.SetLiftPinBlow(true);
                _io.SetChuckVacuum(false);
                _io.SetChuckBlow(false);

                await Task.Delay(1000);

                _io.SetLiftPinVacuum(false);
                _io.SetLiftPinBlow(false);
                _io.SetChuckVacuum(false);
                _io.SetChuckBlow(false);


                LogManager.Sequence.Info("PrepareForGet 완료");
                return true;
            }
            catch (Exception ex)
            {
                LogManager.Sequence.Error($"PrepareForGet 실패: {ex.Message}");
                return false;
            }
            finally
            {
                _cts?.Dispose();
                _cts = null;
            }
        }

        /// <summary>
        /// Ready 스텝 단독 실행 (SetUp 테스트용)
        /// Chuck Vacuum ON + LiftPin OFF + Centering Open + ChuckZ Vision + Light ON
        /// </summary>
        public async Task<bool> RunStepReadyAsync()
        {
            try
            {
                _cts = new CancellationTokenSource();
                LogManager.Sequence.Info("RunStepReady 시작");

                bool result = await ExecuteReadyAsync();

                LogManager.Sequence.Info($"RunStepReady {(result ? "완료" : "실패")}");
                return result;
            }
            catch (Exception ex)
            {
                LogManager.Sequence.Error($"RunStepReady 실패: {ex.Message}");
                return false;
            }
            finally
            {
                _cts?.Dispose();
                _cts = null;
            }
        }

        /// <summary>
        /// PreCenter 스텝 단독 실행 (SetUp 테스트용)
        /// LiftPin Blow ON + Centering MinCtr (L/R 동시)
        /// </summary>
        public async Task<bool> RunStepPreCenterAsync()
        {
            try
            {
                _cts = new CancellationTokenSource();
                LogManager.Sequence.Info("RunStepPreCenter 시작");

                bool result = await ExecutePreCenterAsync();

                LogManager.Sequence.Info($"RunStepPreCenter {(result ? "완료" : "실패")}");
                return result;
            }
            catch (Exception ex)
            {
                LogManager.Sequence.Error($"RunStepPreCenter 실패: {ex.Message}");
                return false;
            }
            finally
            {
                _cts?.Dispose();
                _cts = null;
            }
        }

        #endregion

        #region Step Execution

        private async Task<bool> ExecuteStepAsync(SequenceStep step, Func<Task<bool>> stepAction)
        {
            _cts.Token.ThrowIfCancellationRequested();

            CurrentStep = step;
            LogManager.Sequence.Info($"Step {(int)step}: {step}");

            bool result = await stepAction();

            if (!result)
            {
                State = SequenceState.Error;
                return false;
            }

            return true;
        }

        #endregion

        #region Step Implementations

        /// <summary>
        /// Step 1: PRE_CENTER - FOV용 최소 센터링
        /// ChuckZ=Down, Center=MinCtr, Theta=Home
        /// </summary>
        private async Task<bool> ExecutePreCenterAsync()
        {
            try
            {
                // Lift Pin Vacuum OFF / Blow ON (에어로 웨이퍼가 미끄러지며 센터링)
                _io.SetLiftPinVacuum(false);
                _io.SetLiftPinBlow(true);

                // Centering MinCtr (L/R 동시)
                if (!await _motion.CenteringStagesMoveSyncAsync(
                    _param.CenterL_MinCtr, _param.CenterR_MinCtr,
                    _param.CenteringStage1.Velocity, ct: _cts.Token))
                {
                    SetError("PreCenter 이동 실패");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                SetError($"PreCenter 스텝 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Step 2: READY - Vision 스캔 준비
        /// ChuckZ=Vision, Center=Open, Theta=ScanStart
        /// </summary>
        private async Task<bool> ExecuteReadyAsync()
        {
            try
            {
                // Chuck Vacuum ON / Blow OFF (척 흡착 준비)
                _io.SetChuckVacuum(true);
                _io.SetChuckBlow(false);

                // Lift Pin Vacuum OFF / Blow OFF (핀 해제)
                _io.SetLiftPinVacuum(false);
                _io.SetLiftPinBlow(false);

                // Centering Open
                if (!await _motion.CenteringStagesMoveSyncAsync(
                    _param.CenterL_Open, _param.CenterR_Open,
                    _param.CenteringStage1.Velocity, ct: _cts.Token))
                {
                    SetError("Centering Open 이동 실패");
                    return false;
                }

                // Chuck Z Vision (상승하면서 LiftPin 위 웨이퍼를 Chuck으로 안착)
                if (!await _motion.WedgeStageMoveAsync(_param.ChuckZ_Vision, _param.WedgeUpDown.Velocity, ct: _cts.Token))
                {
                    SetError("Chuck Z Vision 이동 실패");
                    return false;
                }

                // Vision Light ON (LfineLight 조명 제어)
                _vision?.SetLightOn();

                return true;
            }
            catch (Exception ex)
            {
                SetError($"Ready 스텝 실패: {ex.Message}");
                return false;
            }
        }

        // Step 3: SCAN_START - 무한 회전 구조로 불필요 (주석 처리)
        // private async Task<bool> ExecuteScanStartAsync()
        // {
        //     try
        //     {
        //         if (!await _motion.ChuckRotateAbsoluteAsync(_param.Theta_Home, ct: _cts.Token))
        //         {
        //             SetError("Theta ScanStart 이동 실패");
        //             return false;
        //         }
        //         return true;
        //     }
        //     catch (Exception ex)
        //     {
        //         SetError($"ScanStart 스텝 실패: {ex.Message}");
        //         return false;
        //     }
        // }

        /// <summary>
        /// Step 4: SCAN - Vision 스캔 (xN)
        /// 0°부터 AngleStep씩 이동 후 촬영, ImageCount회 반복하여 360° 회전
        /// 무한 회전 구조 - ScanStart/Rewind 불필요
        /// </summary>
        private async Task<bool> ExecuteScanAsync()
        {
            try
            {
                if (_vision == null)
                {
                    SetError("Vision 모듈이 없습니다.");
                    return false;
                }

                // 이전 결과 이미지 클리어 → Scan 중 캡처 이미지가 picVisionDisplay에 표시되도록
                EventManager.Publish(EventManager.DisplayImageChanged, null);

                int imageCount = _overrideImageCount ?? _param.ScanImageCount;
                double stepAngle = _overrideStepAngle ?? _param.ScanStepAngle;
                _overrideImageCount = null;
                _overrideStepAngle = null;

                // Vision 이미지 클리어
                _vision.ClearImages();
                _vision.SetSettings(stepAngle, imageCount);
                // 현재 위치를 0으로 리셋
                _motion.SetPosition(VAMotionAxis.ChuckRotation, 0);

                // Eddy 측정값 / PN 판정값 (Scan 중 병렬로 측정)
                double eddyAverage = 0;
                int pnValue = 2; // 기본값: 판정 불가

                // 시뮬레이션 모드: 폴더에서 이미지 로드
                if (AppSettings.SimulationMode)
                {
                    string imagePath = _isFlat
                        ? AppSettings.SimulationImagePath_Flat
                        : AppSettings.SimulationImagePath_Notch;

                    if (string.IsNullOrEmpty(imagePath) || !System.IO.Directory.Exists(imagePath))
                    {
                        SetError($"시뮬레이션 이미지 경로가 설정되지 않았거나 존재하지 않습니다: {imagePath}");
                        return false;
                    }

                    LogManager.Sequence.Info($"시뮬레이션 모드 - 이미지 로드: {imagePath}");

                    if (!_vision.AddImagesFromFolder(imagePath))
                    {
                        SetError("시뮬레이션 이미지 로드 실패");
                        return false;
                    }

                    LogManager.Sequence.Info($"이미지 {_vision.ImageCount}개 로드 완료");

                    // 시뮬레이션 Eddy / PN 값
                    eddyAverage = AppSettings.SimulationEddyValue;
                    pnValue = AppSettings.SimulationPNValue;
                }
                else
                {
                    _vision.SetLightOn();
                    // 실제 하드웨어 모드: (AngleStep 이동 → Trigger+Capture) × ImageCount
                    // 0° → 15° → 30° → ... → 345° → 360° (총 24회)

                    // 스캔 전 TrigLive 비활성화 (수동 Trigger 모드)
                    _vision.SetTrigLive(false);

                    // Scan + Eddy + PN Check 병렬 실행
                    bool eddyEnabled = _eddy != null && _eddy.IsConnected;
                    var scanTask = RunScanLoopAsync(imageCount, stepAngle);
                    var eddyTask = RunEddyMeasureAsync(eddyEnabled);
                    var pnTask = RunPNCheckAsync();

                    await Task.WhenAll(scanTask, eddyTask, pnTask);

                    if (!scanTask.Result)
                        return false;

                    // Eddy 평균값 계산
                    if (eddyEnabled && eddyTask.Result != null)
                    {
                        eddyAverage = eddyTask.Result.Value;
                        LogManager.Sequence.Info($"Eddy Average: {eddyAverage:F1}");
                    }

                    // PN 결과 저장
                    pnValue = pnTask.Result;
                }
                _vision.SetLightOff();
                // 360° 회전 완료 후 현재 위치를 0으로 리셋
                _motion.SetPosition(VAMotionAxis.ChuckRotation, 0);
                LogManager.Sequence.Info("Chuck 위치 0으로 리셋");

                // Vision Light OFF (스캔 완료 후 조명 끄기)
                _vision?.SetLightOff();

                // Vision 분석 수행
                if (!_vision.ExecuteInspection(_isFlat))
                {
                    SetError("Vision 검사 실행 실패");
                    return false;
                }

                // 결과 가져오기
                _visionResult = _vision.GetResult(_isFlat);

                if (!_visionResult.IsValid)
                {
                    SetError("Vision 분석 실패");
                    return false;
                }

                // Eddy / PN 측정값을 Vision 결과에 저장
                _visionResult.EddyValue = eddyAverage;
                _visionResult.PNValue = pnValue;

                LogManager.Sequence.Info($"Vision Result - Radius: {_visionResult.Radius:F3}, AbsAngle: {_visionResult.AbsAngle:F3}, Eddy: {eddyAverage:F1}, PN: {(pnValue == 1 ? "P" : pnValue == 0 ? "N" : "?")}");
                return true;
            }
            catch (Exception ex)
            {
                SetError($"Scan 스텝 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Scan 루프 태스크: AngleStep 이동 → Trigger+Capture 반복
        /// </summary>
        private async Task<bool> RunScanLoopAsync(int imageCount, double stepAngle)
        {
            for (int i = 0; i < imageCount; i++)
            {
                _cts.Token.ThrowIfCancellationRequested();

                double targetAngle = stepAngle * (i + 1);

                if (!await _motion.ChuckRotateAbsoluteAsync(targetAngle, _param.ChuckRotation.Velocity, ct: _cts.Token))
                {
                    SetError($"Scan 이동 실패 (Image {i + 1}/{imageCount}, Target: {targetAngle:F1}°)");
                    return false;
                }

                // 모터 안정화 대기
                await Task.Delay(100, _cts.Token);

                if (!await _vision.TriggerAndCaptureAsync(_cts.Token))
                {
                    SetError($"이미지 획득 실패 (Image {i + 1}/{imageCount}, Angle: {targetAngle:F1}°)");
                    return false;
                }

                LogManager.Sequence.Debug($"Scan {i + 1}/{imageCount} - Angle: {targetAngle:F1}°");
            }
            return true;
        }

        /// <summary>
        /// Eddy 측정 태스크: 0° 시점과 180° 통과 시점에서 GetData, 평균값 반환
        /// </summary>
        /// <returns>평균값 (소수점 1자리), Eddy 미사용 시 null</returns>
        private async Task<double?> RunEddyMeasureAsync(bool eddyEnabled)
        {
            if (!eddyEnabled) return null;

            try
            {
                // 0° 시점 측정 (회전 시작 전)
                double eddyAt0 = _eddy.GetData();
                LogManager.Sequence.Info($"Eddy GetData at 0°: {eddyAt0:F4}");

                // 180° 통과 감지: 위치가 180°를 넘는 순간 측정
                double eddyAt180 = 0;
                while (!_cts.Token.IsCancellationRequested)
                {
                    await Task.Delay(50, _cts.Token);
                    double currentPos = _motion.GetPosition(VAMotionAxis.ChuckRotation);

                    if (currentPos >= 180.0)
                    {
                        eddyAt180 = _eddy.GetData();
                        LogManager.Sequence.Info($"Eddy GetData at 180° (pos: {currentPos:F1}°): {eddyAt180:F4}");
                        break;
                    }
                }

                double average = Math.Round((eddyAt0 + eddyAt180) / 2.0, 1);
                return average;
            }
            catch (OperationCanceledException)
            {
                return null;
            }
            catch (Exception ex)
            {
                LogManager.Sequence.Warn($"Eddy 측정 태스크 오류: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// PN Check 태스크: PNSwitch1 ON → P/N 상태 1초 유지 확인 → 결과 반환
        /// </summary>
        /// <returns>1=P, 0=N, 2=판정불가(타임아웃)</returns>
        private async Task<int> RunPNCheckAsync()
        {
            if (_io == null) return 2;

            try
            {
                // PN Switch 1 ON
                _io.SetPNSwitch1(true);
                LogManager.Sequence.Info("PN Check: Switch1 ON");

                int timeoutMs = _param.PNTimeout;
                int pollInterval = _param.PNPollInterval;
                int holdRequiredMs = _param.PNHoldTime;

                int holdTimeP = 0;
                int holdTimeN = 0;
                int elapsed = 0;

                while (elapsed < timeoutMs && !_cts.Token.IsCancellationRequested)
                {
                    await Task.Delay(pollInterval, _cts.Token);
                    elapsed += pollInterval;

                    bool p = _io.IsPNCheckP();
                    bool n = _io.IsPNCheckN();

                    // P 유지 카운트
                    holdTimeP = p ? holdTimeP + pollInterval : 0;
                    // N 유지 카운트
                    holdTimeN = n ? holdTimeN + pollInterval : 0;

                    if (holdTimeP >= holdRequiredMs)
                    {
                        _io.SetPNSwitch1(false);
                        LogManager.Sequence.Info($"PN Check: P 확정 ({holdTimeP}ms 유지)");
                        return 1;
                    }

                    if (holdTimeN >= holdRequiredMs)
                    {
                        _io.SetPNSwitch1(false);
                        LogManager.Sequence.Info($"PN Check: N 확정 ({holdTimeN}ms 유지)");
                        return 0;
                    }
                }

                // 타임아웃: 판정 불가
                _io.SetPNSwitch1(false);
                LogManager.Sequence.Warn("PN Check: 타임아웃 - 판정 불가");
                return 2;
            }
            catch (OperationCanceledException)
            {
                _io?.SetPNSwitch1(false);
                return 2;
            }
            catch (Exception ex)
            {
                _io?.SetPNSwitch1(false);
                LogManager.Sequence.Warn($"PN Check 오류: {ex.Message}");
                return 2;
            }
        }

        // Step 5: REWIND - 무한 회전 구조로 불필요 (주석 처리)
        // Light OFF는 ExecuteScanAsync 완료 후 처리
        // private async Task<bool> ExecuteRewindAsync()
        // {
        //     try
        //     {
        //         if (!await _motion.ChuckRotateAbsoluteAsync(_param.Theta_Home, ct: _cts.Token))
        //         {
        //             SetError("Theta Rewind 이동 실패");
        //             return false;
        //         }
        //         _vision?.SetLightOff();
        //         return true;
        //     }
        //     catch (Exception ex)
        //     {
        //         SetError($"Rewind 스텝 실패: {ex.Message}");
        //         return false;
        //     }
        // }

        /// <summary>
        /// Step 6: ALIGN - 센터링 + Theta 정렬
        /// ChuckZ=Down, Center=Radius, Theta=AbsAngle
        /// </summary>
        private async Task<bool> ExecuteAlignAsync()
        {
            try
            {
                if (!_visionResult.IsValid)
                {
                    SetError("Vision 결과가 없습니다.");
                    return false;
                }

                // 1. Theta 정렬 — 편심 벡터를 X축에 일치시키기 위한 회전
                // Scan 완료 후 현재 위치 = 0° (절대 위치 제어)
                double cx = _visionResult.Wafer.CenterX;
                double cy = _visionResult.Wafer.CenterY;
                double atan2Deg = Math.Atan2(cy, cx) * 180.0 / Math.PI;

                double thetaAlign = atan2Deg;
                //if (cx >= 0 && cy >= 0)        // Q1
                //    thetaAlign = atan2Deg;
                //else if (cx < 0 && cy >= 0)    // Q2
                //    thetaAlign = atan2Deg;
                //else if (cx < 0 && cy < 0)     // Q3
                //    thetaAlign = atan2Deg;
                //else                           // Q4
                //    thetaAlign = atan2Deg;

                LogManager.Sequence.Info($"Theta Align 계산 - Cx: {cx:F3}, Cy: {cy:F3}, atan2: {atan2Deg:F3}°, θalign: {thetaAlign:F3}°");

                if (!await _motion.ChuckRotateAbsoluteAsync(thetaAlign, _param.ChuckRotation.Velocity, ct: _cts.Token))
                {
                    SetError("Theta Align 이동 실패");
                    return false;
                }

                _motion.SetPosition(VAMotionAxis.ChuckRotation, 0);

                LogManager.Sequence.Info($"Theta Align 완료 - θalign: {thetaAlign:F3}°");

                // 2. 웨이퍼를 Chuck → LiftPin으로 이동
                // Chuck 해제, LiftPin 받을 준비
                _io.SetChuckVacuum(false);
                _io.SetChuckBlow(true);
                _io.SetLiftPinVacuum(true);
                _io.SetLiftPinBlow(false);

                // Chuck Z Down (하강 → 웨이퍼가 LiftPin에 안착)
                if (!await _motion.WedgeStageMoveAsync(_param.ChuckZ_Down, _param.WedgeUpDown.Velocity, ct: _cts.Token))
                {
                    SetError("Chuck Z Down 이동 실패");
                    return false;
                }

                // 3. 센터 정렬 (LiftPin 위에서 에어 센터링)
                _io.SetLiftPinVacuum(false);
                _io.SetLiftPinBlow(true);
                _io.SetChuckVacuum(false);
                _io.SetChuckBlow(false) ;

                double centerL = _param.CenterL_MinCtr + _visionResult.Wafer.TotalOffset + _visionResult.Wafer.Radius + 0.02;
                double centerR = _param.CenterR_MinCtr + _visionResult.Wafer.TotalOffset - _visionResult.Wafer.Radius - 0.02;

                if (!await _motion.CenteringStagesMoveSyncAsync(centerL, centerR, _param.CenteringStage1.Velocity, ct: _cts.Token))
                {
                    SetError("Center Align 이동 실패");
                    return false;
                }

                if (!await _motion.CenteringStagesMoveSyncAsync(centerL - _visionResult.Wafer.TotalOffset, centerR + _visionResult.Wafer.TotalOffset, _param.CenteringStage1.Velocity, ct: _cts.Token))
                {
                    SetError("Center Align 이동 실패");
                    return false;
                }

                // 3. 센터 정렬 (LiftPin 위에서 에어 센터링)
                _io.SetLiftPinVacuum(false);
                _io.SetLiftPinBlow(false);
                _io.SetChuckVacuum(true);
                _io.SetChuckBlow(false);


                if (!await _motion.CenteringStagesMoveSyncAsync(_param.CenterL_Open, _param.CenterR_Open, _param.CenteringStage1.Velocity, ct: _cts.Token))
                {
                    SetError("Center Align 이동 실패");
                    return false;
                }

                if (!await _motion.WedgeStageMoveAsync(_param.ChuckZ_Eddy, _param.WedgeUpDown.Velocity, ct: _cts.Token))
                {
                    SetError("Chuck Z up 이동 실패");
                    return false;
                }

                double findNotchAngle = _visionResult.AbsAngle - thetaAlign; 
                if (!await _motion.ChuckRotateAbsoluteAsync(findNotchAngle, _param.ChuckRotation.Velocity, ct: _cts.Token))
                {
                    SetError("Theta Align 이동 실패");
                    return false;
                }

                _motion.SetPosition(VAMotionAxis.ChuckRotation, 0);


                LogManager.Sequence.Info($"Center Align 완료 - CenterL: {centerL:F3}, CenterR: {centerR:F3}");
                return true;
            }
            catch (Exception ex)
            {
                SetError($"Align 스텝 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Step 7: EDDY - Eddy Current 측정
        /// ChuckZ=Eddy, Center=Open
        /// </summary>
        private async Task<bool> ExecuteEddyAsync()
        {
            try
            {
                // Centering Open
                if (!await _motion.CenteringStagesMoveSyncAsync(
                    _param.CenterL_Open, _param.CenterR_Open,
                    _param.CenteringStage1.Velocity, ct: _cts.Token))
                {
                    SetError("Centering Open 이동 실패");
                    return false;
                }

                // Chuck Z Eddy
                if (!await _motion.WedgeStageMoveAsync(_param.ChuckZ_Eddy, _param.WedgeUpDown.Velocity, ct: _cts.Token))
                {
                    SetError("Chuck Z Eddy 이동 실패");
                    return false;
                }

                // Eddy 측정 및 PN 판정
                if (AppSettings.SimulationMode)
                {
                    // 시뮬레이션 모드: Settings.ini의 더미값 사용
                    double eddyValue = AppSettings.SimulationEddyValue;
                    int pnValue = AppSettings.SimulationPNValue;

                    _visionResult.EddyValue = eddyValue;
                    _visionResult.PNValue = pnValue;

                    LogManager.Sequence.Info($"Eddy Value (Simulation): {eddyValue:F3}, PN: {(pnValue == 1 ? "P" : "N")}");
                }
                else if (_eddy != null && _eddy.IsConnected)
                {
                    // 실제 하드웨어 모드
                    double eddyValue = _eddy.GetData();
                    _visionResult.EddyValue = eddyValue;

                    // TODO: 실제 PN 센서에서 값 읽기
                    // _visionResult.PNValue = _io.GetPNValue();

                    LogManager.Sequence.Info($"Eddy Value: {eddyValue:F3}");
                }

                return true;
            }
            catch (Exception ex)
            {
                SetError($"Eddy 스텝 실패: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Private Methods

        private void SetError(string message, int alarmCode = AlarmCodes.SEQUENCE_ERROR)
        {
            LastError = message;
            State = SequenceState.Error;
            LogManager.Sequence.Error($"Error [{alarmCode}]: {message}");
            ErrorOccurred?.Invoke(this, new SequenceErrorEventArgs(alarmCode, message));
        }

        /// <summary>
        /// CTC Transfer 상태 변경 요청 발생
        /// </summary>
        private void RequestTransferStatusChange(CTCTransferStatus status)
        {
            LogManager.Sequence.Info($"CTC Transfer Status 변경 요청: {status}");
            TransferStatusChangeRequested?.Invoke(this, new CTCTransferStatusEventArgs(status, _visionResult));
        }

        #endregion
    }

    #region CTC Transfer Status

    /// <summary>
    /// CTC Transfer 상태 (Stage 입장)
    /// </summary>
    public enum CTCTransferStatus
    {
        /// <summary>
        /// CTC가 웨이퍼를 가져갈 수 있음 (측정 완료)
        /// </summary>
        GetReady,

        /// <summary>
        /// CTC가 웨이퍼를 올려놓을 수 있음 (수신 대기)
        /// </summary>
        PutReady,

        /// <summary>
        /// 이송 불가 (측정 중 등)
        /// </summary>
        NotReady
    }

    /// <summary>
    /// CTC Transfer 상태 변경 이벤트 인자
    /// </summary>
    public class CTCTransferStatusEventArgs : EventArgs
    {
        public CTCTransferStatus Status { get; }
        public WaferVisionResult? Result { get; }

        public CTCTransferStatusEventArgs(CTCTransferStatus status, WaferVisionResult? result = null)
        {
            Status = status;
            Result = result;
        }
    }

    #endregion

    #region Sequence Error

    /// <summary>
    /// 시퀀스 에러 이벤트 인자 (알람 코드 + 메시지)
    /// </summary>
    public class SequenceErrorEventArgs : EventArgs
    {
        public int AlarmCode { get; }
        public string Message { get; }

        public SequenceErrorEventArgs(int alarmCode, string message)
        {
            AlarmCode = alarmCode;
            Message = message;
        }
    }

    #endregion
}
