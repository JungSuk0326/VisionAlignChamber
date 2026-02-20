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
    /// Vision Align Chamber 7-Step 측정 시퀀스
    /// PreCtr(FOV) -> Ready -> ScanStart -> Scan(xN) -> Rewind -> Align(Center+Theta) -> Eddy
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
        /// 에러 발생 이벤트
        /// </summary>
        public event EventHandler<string> ErrorOccurred;

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

            // CTC 상태: 측정 중 - 이송 불가
            RequestTransferStatusChange(CTCTransferStatus.NotReady);

            try
            {
                LogManager.System.Info($"[Sequence] 시퀀스 시작 (Type: {(isFlat ? "Flat" : "Notch")}, SkipEddy: {skipEddy})");

                // Step 1: PreCenter
                if (!await ExecuteStepAsync(SequenceStep.PreCenter, ExecutePreCenterAsync))
                    return false;

                // Step 2: Ready
                if (!await ExecuteStepAsync(SequenceStep.Ready, ExecuteReadyAsync))
                    return false;

                // Step 3: ScanStart
                if (!await ExecuteStepAsync(SequenceStep.ScanStart, ExecuteScanStartAsync))
                    return false;

                // Step 4: Scan
                if (!await ExecuteStepAsync(SequenceStep.Scan, ExecuteScanAsync))
                    return false;

                // Step 5: Rewind
                if (!await ExecuteStepAsync(SequenceStep.Rewind, ExecuteRewindAsync))
                    return false;

                // Step 6: Align (Center + Theta)
                if (!await ExecuteStepAsync(SequenceStep.Align, ExecuteAlignAsync))
                    return false;

                // Step 7: Eddy (스킵 가능)
                if (!skipEddy)
                {
                    if (!await ExecuteStepAsync(SequenceStep.Eddy, ExecuteEddyAsync))
                        return false;
                }
                else
                {
                    LogManager.System.Info("[Sequence] Step 7: Eddy - Skipped");
                }

                // 완료
                CurrentStep = SequenceStep.Complete;
                State = SequenceState.Completed;
                LogManager.System.Info("[Sequence] 시퀀스 완료");

                // CTC 상태: 측정 완료 - 웨이퍼 가져갈 수 있음
                RequestTransferStatusChange(CTCTransferStatus.GetReady);

                SequenceCompleted?.Invoke(this, _visionResult);
                return true;
            }
            catch (OperationCanceledException)
            {
                State = SequenceState.Aborted;
                LogManager.System.Warn("[Sequence] 시퀀스 중단됨");
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
                LogManager.System.Warn("[Sequence] IO 모듈이 없어 인터락 체크를 건너뜁니다.");
                return true;
            }

            if (!_io.IsWaferDetectedOnAllSensors())
            {
                SetError("웨이퍼가 감지되지 않았습니다. (Interlock)");
                return false;
            }

            LogManager.System.Info("[Sequence] 웨이퍼 센서 인터락 체크 OK");
            return true;
        }

        /// <summary>
        /// 시퀀스 중지
        /// </summary>
        public void Stop()
        {
            _cts?.Cancel();
            _motion?.EmergencyStopAll();
            State = SequenceState.Aborted;
            LogManager.System.Warn("[Sequence] 시퀀스 정지 요청");
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

        #endregion

        #region Step Execution

        private async Task<bool> ExecuteStepAsync(SequenceStep step, Func<Task<bool>> stepAction)
        {
            _cts.Token.ThrowIfCancellationRequested();

            CurrentStep = step;
            LogManager.System.Info($"[Sequence] Step {(int)step}: {step}");

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
                // Centering MinCtr (L/R 동시)
                if (!await _motion.CenteringStagesMoveSyncAsync(
                    _param.CenterL_MinCtr, _param.CenterR_MinCtr, ct: _cts.Token))
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
                // Centering Open
                if (!await _motion.CenteringStagesMoveSyncAsync(
                    _param.CenterL_Open, _param.CenterR_Open, ct: _cts.Token))
                {
                    SetError("Centering Open 이동 실패");
                    return false;
                }

                // Chuck Z Vision
                if (!await _motion.WedgeStageMoveAsync(_param.ChuckZ_Vision, ct: _cts.Token))
                {
                    SetError("Chuck Z Vision 이동 실패");
                    return false;
                }

                // Vision Light ON
                _io?.SetVisionLight(true);

                return true;
            }
            catch (Exception ex)
            {
                SetError($"Ready 스텝 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Step 3: SCAN_START - 스캔 시작 위치
        /// Theta=ScanStart
        /// </summary>
        private async Task<bool> ExecuteScanStartAsync()
        {
            try
            {
                // Theta ScanStart (각도 단위로 전달)
                if (!await _motion.ChuckRotateAbsoluteAsync(_param.Theta_ScanStart, ct: _cts.Token))
                {
                    SetError("Theta ScanStart 이동 실패");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                SetError($"ScanStart 스텝 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Step 4: SCAN - Vision 스캔 (xN)
        /// Theta를 StepAngle씩 회전하며 이미지 촬영
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

                int imageCount = _param.ScanImageCount;
                double stepAngle = _param.ScanStepAngle;

                // Vision 이미지 클리어
                _vision.ClearImages();

                // Vision 스캔 - 각 각도에서 이미지 촬영
                for (int i = 0; i < imageCount; i++)
                {
                    _cts.Token.ThrowIfCancellationRequested();

                    // 이미지 촬영 (TODO: 카메라에서 버퍼 획득 후 추가)
                    // _vision.AddImage(width, height, buffer);

                    // 다음 각도로 이동 (마지막 제외)
                    if (i < imageCount - 1)
                    {
                        double targetAngle = _param.Theta_ScanStart + (stepAngle * (i + 1));

                        if (!await _motion.ChuckRotateAbsoluteAsync(targetAngle, ct: _cts.Token))
                        {
                            SetError($"Scan 이동 실패 (Image {i + 1})");
                            return false;
                        }
                    }
                }

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

                LogManager.System.Info($"[Sequence] Vision Result - Radius: {_visionResult.Radius:F3}, AbsAngle: {_visionResult.AbsAngle:F3}");
                return true;
            }
            catch (Exception ex)
            {
                SetError($"Scan 스텝 실패: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Step 5: REWIND - Theta 와이어 풀림
        /// Theta를 역방향으로 회전
        /// </summary>
        private async Task<bool> ExecuteRewindAsync()
        {
            try
            {
                // Theta Rewind (ScanStart 위치로 복귀)
                if (!await _motion.ChuckRotateAbsoluteAsync(_param.Theta_ScanStart, ct: _cts.Token))
                {
                    SetError("Theta Rewind 이동 실패");
                    return false;
                }

                // Vision Light OFF
                _io?.SetVisionLight(false);

                return true;
            }
            catch (Exception ex)
            {
                SetError($"Rewind 스텝 실패: {ex.Message}");
                return false;
            }
        }

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

                // Chuck Z Down
                if (!await _motion.WedgeStageMoveAsync(_param.ChuckZ_Down, ct: _cts.Token))
                {
                    SetError("Chuck Z Down 이동 실패");
                    return false;
                }

                // Centering with Radius - Wafer 중심 오프셋 기준 센터링 위치 계산
                // TODO: Radius 값을 실제 Centering 위치로 변환하는 로직 필요
                double centerPosition = _param.CenteringStage_ClosePos + (_visionResult.Radius * _param.CenteringStage_MarginUm);

                // Centering (L/R 동시) + Theta Align 동시 시작
                var centerTask = _motion.CenteringStagesMoveSyncAsync(centerPosition, centerPosition, ct: _cts.Token);
                var thetaTask = _motion.ChuckRotateAbsoluteAsync(_visionResult.AbsAngle, ct: _cts.Token);

                await Task.WhenAll(centerTask, thetaTask);

                if (!centerTask.Result || !thetaTask.Result)
                {
                    SetError("Align 이동 실패");
                    return false;
                }

                LogManager.System.Info($"[Sequence] Align 완료 - Center: {centerPosition:F3}, Theta: {_visionResult.AbsAngle:F3}°");
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
                    _param.CenterL_Open, _param.CenterR_Open, ct: _cts.Token))
                {
                    SetError("Centering Open 이동 실패");
                    return false;
                }

                // Chuck Z Eddy
                if (!await _motion.WedgeStageMoveAsync(_param.ChuckZ_Eddy, ct: _cts.Token))
                {
                    SetError("Chuck Z Eddy 이동 실패");
                    return false;
                }

                // Eddy 측정
                if (_eddy != null && _eddy.IsConnected)
                {
                    double eddyValue = _eddy.GetData();
                    _visionResult.EddyValue = eddyValue;
                    LogManager.System.Info($"[Sequence] Eddy Value: {eddyValue:F3}");
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

        private void SetError(string message)
        {
            LastError = message;
            State = SequenceState.Error;
            LogManager.System.Error($"[Sequence] Error: {message}");
            ErrorOccurred?.Invoke(this, message);
        }

        /// <summary>
        /// CTC Transfer 상태 변경 요청 발생
        /// </summary>
        private void RequestTransferStatusChange(CTCTransferStatus status)
        {
            LogManager.System.Info($"[Sequence] CTC Transfer Status 변경 요청: {status}");
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
}
