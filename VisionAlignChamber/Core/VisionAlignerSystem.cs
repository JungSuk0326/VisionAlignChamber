using System;
using System.Collections.Generic;
using CommObject;
using VisionAlignChamber.Hardware.Facade;
using VisionAlignChamber.Vision;
using VisionAlignChamber.Interfaces;
using VisionAlignChamber.Communication.Interfaces;
using VisionAlignChamber.Database;
using VisionAlignChamber.Interlock;
using VisionAlignChamber.Log;
using VisionAlignChamber.Models;
using System.Net.Configuration;
using System.Threading.Tasks;

namespace VisionAlignChamber.Core
{
    /// <summary>
    /// Vision Aligner 시스템 최상위 비즈니스 로직
    /// Motion, IO, Vision을 조합하여 시스템 레벨 동작 수행
    /// </summary>
    public class VisionAlignerSystem : IDisposable
    {
        #region Fields

        private readonly VisionAlignerMotion _motion;
        private readonly VisionAlignerIO _io;
        private readonly VisionAlignWrapper _vision;
        private readonly IEddyCurrentSensor _eddy;
        private readonly ICTCCommunication _ctcComm;

        private VisionAlignerSequence _sequence;
        private InterlockMonitor _interlockMonitor;
        private Communication.CTCStatusBridge _statusBridge;
        private bool _isInitialized;
        private bool _disposed;

        #endregion

        #region Constructor

        /// <summary>
        /// 생성자 - 각 모듈은 null 허용 (개별 테스트 가능)
        /// </summary>
        public VisionAlignerSystem(
            VisionAlignerMotion motion = null,
            VisionAlignerIO io = null,
            VisionAlignWrapper vision = null,
            IEddyCurrentSensor eddy = null,
            ICTCCommunication ctcComm = null)
        {
            _motion = motion;
            _io = io;
            _vision = vision;
            _eddy = eddy;
            _ctcComm = ctcComm;

            // CTC 통신 이벤트 구독 (비즈니스 로직용)
            SubscribeCTCEvents();

            // AppState 변경 이벤트 구독 (CTC 상태 동기화용)
            SubscribeAppStateEvents();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 시스템 초기화 상태
        /// </summary>
        public bool IsInitialized => _isInitialized;

        #region 모듈 가용성

        /// <summary>
        /// Motion 모듈 사용 가능 여부
        /// </summary>
        public bool HasMotion => _motion != null;

        /// <summary>
        /// IO 모듈 사용 가능 여부
        /// </summary>
        public bool HasIO => _io != null;

        /// <summary>
        /// Vision 모듈 사용 가능 여부
        /// </summary>
        public bool HasVision => _vision != null;

        /// <summary>
        /// Eddy 모듈 사용 가능 여부
        /// </summary>
        public bool HasEddy => _eddy != null;

        /// <summary>
        /// CTC 통신 모듈 사용 가능 여부
        /// </summary>
        public bool HasCTC => _ctcComm != null;

        #endregion

        #region 모듈 초기화 상태

        /// <summary>
        /// 모션 컨트롤러 초기화 상태
        /// </summary>
        public bool IsMotionInitialized => _motion?.IsInitialized ?? false;

        /// <summary>
        /// 디지털 IO 초기화 상태
        /// </summary>
        public bool IsIOInitialized => _io?.IsInitialized ?? false;

        /// <summary>
        /// 비전 초기화 상태
        /// </summary>
        public bool IsVisionInitialized => _vision?.IsInitialized ?? false;

        /// <summary>
        /// Eddy 센서 연결 상태
        /// </summary>
        public bool IsEddyConnected => _eddy?.IsConnected ?? false;

        /// <summary>
        /// CTC 통신 리스닝 상태
        /// </summary>
        public bool IsCTCListening => _ctcComm?.IsListening ?? false;

        #endregion

        #region 모듈 접근자

        /// <summary>
        /// Motion Facade (ViewModel에서 직접 접근용)
        /// </summary>
        public VisionAlignerMotion Motion => _motion;

        /// <summary>
        /// IO Facade (ViewModel에서 직접 접근용)
        /// </summary>
        public VisionAlignerIO IO => _io;

        /// <summary>
        /// Vision Wrapper (ViewModel에서 직접 접근용)
        /// </summary>
        public VisionAlignWrapper Vision => _vision;

        /// <summary>
        /// Eddy Current 센서 (ViewModel에서 직접 접근용)
        /// </summary>
        public IEddyCurrentSensor Eddy => _eddy;

        /// <summary>
        /// CTC 통신 컨트롤러 (ViewModel에서 직접 접근용)
        /// </summary>
        public ICTCCommunication CTCComm => _ctcComm;

        /// <summary>
        /// Vision Align 시퀀스 (Main Tab ViewModel용)
        /// </summary>
        public VisionAlignerSequence Sequence => _sequence;

        #endregion

        /// <summary>
        /// 마지막 에러 메시지
        /// </summary>
        public string LastError { get; private set; }

        #endregion

        #region Events

        /// <summary>
        /// 초기화 진행 상태 이벤트
        /// </summary>
        public event EventHandler<InitializationProgressEventArgs> InitializationProgress;

        #endregion

        #region Initialization

        /// <summary>
        /// 전체 시스템 초기화 (사용 가능한 모듈만)
        /// 순서: Motion → IO → Vision
        /// </summary>
        /// <returns>하나 이상의 모듈이 초기화되면 true</returns>
        public bool InitializeAll()
        {
            if (_isInitialized)
            {
                LastError = "이미 초기화되어 있습니다.";
                return true;
            }

            // 알람 DB 저장 활성화
            InterlockManager.Instance.EnableDatabase();

            bool anySuccess = false;
            int step = 0;
            int totalSteps = (HasMotion ? 1 : 0) + (HasIO ? 1 : 0) + (HasVision ? 1 : 0) + (HasCTC ? 1 : 0);

            try
            {
                // Step 1: Motion 초기화
                if (HasMotion)
                {
                    RaiseInitializationProgress("모션 컨트롤러 초기화 중...", step, totalSteps);
                    if (InitializeMotion())
                    {
                        anySuccess = true;
                    }
                    step++;
                }

                // Step 2: IO 초기화
                if (HasIO)
                {
                    RaiseInitializationProgress("디지털 IO 초기화 중...", step, totalSteps);
                    if (InitializeIO())
                    {
                        anySuccess = true;
                    }
                    step++;
                }

                // Step 3: Vision 초기화
                if (HasVision)
                {
                    RaiseInitializationProgress("비전 시스템 초기화 중...", step, totalSteps);
                    if (InitializeVision())
                    {
                        anySuccess = true;
                    }
                    step++;
                }

                // Step 4: CTC 통신 시작
                if (HasCTC)
                {
                    RaiseInitializationProgress("CTC 통신 시작 중...", step, totalSteps);
                    StartCTCCommunication();
                    step++;
                }

                _isInitialized = anySuccess;

                // 모션 초기화 성공 시 서보 알람 상시 체크 등록
                if (IsMotionInitialized)
                {
                    RegisterServoAlarmMonitor();
                }

                // Sequence 생성 (Motion과 IO가 필수)
                if (HasMotion && HasIO)
                {
                    _sequence = new VisionAlignerSequence(_motion, _io, _vision, _eddy);
                    SubscribeSequenceEvents();
                }

                // StatusBridge 생성 (AppState → StatusObject 자동 변환)
                if (HasCTC)
                {
                    _statusBridge = new Communication.CTCStatusBridge(_ctcComm);
                }

                // 초기화 완료 시 CTC 상태: 웨이퍼 유무에 따라 판단
                if (_io != null && _io.IsInitialized && _io.IsWaferDetectedOnAllSensors())
                {
                    AppState.Current.IsWaferExist = true;
                    SetCTCTransferStatus(CTCTransferStatus.GetReady);
                }
                else
                {
                    AppState.Current.IsWaferExist = false;
                    SetCTCTransferStatus(CTCTransferStatus.PutReady);
                }

                // AppContext 상태 동기화
                SyncAppContextState();

                RaiseInitializationProgress("시스템 초기화 완료", totalSteps, totalSteps);

                // 초기화 완료 이벤트 발행 (MainForm 등에서 UI 갱신용)
                if (anySuccess)
                {
                    EventManager.Publish(EventManager.SystemInitialized, this);
                }
                _ = _sequence.ReleaseChuckAsync();
                return anySuccess;
            }
            catch (Exception ex)
            {
                LastError = $"초기화 중 예외 발생: {ex.Message}";
                return anySuccess;
            }
        }

        /// <summary>
        /// 모션 컨트롤러 초기화
        /// 초기화 완료 후 모든 축 서보 ON
        /// </summary>
        public bool InitializeMotion()
        {
            if (_motion == null)
            {
                LastError = "Motion 모듈을 사용할 수 없습니다.";
                return false;
            }

            if (!_motion.Initialize())
            {
                LastError = "모션 컨트롤러 초기화 실패";
                return false;
            }

            // 모든 축 서보 알람 클리어
            _motion.ClearAlarmAll();

            // 모든 축 서보 ON
            if (!_motion.ServoOnAll())
            {
                LastError = "서보 ON 실패";
                return false;
            }

            return true;
        }

        /// <summary>
        /// 디지털 IO 초기화
        /// </summary>
        public bool InitializeIO()
        {
            if (_io == null)
            {
                LastError = "IO 모듈을 사용할 수 없습니다.";
                return false;
            }

            if (!_io.Initialize())
            {
                LastError = "디지털 IO 초기화 실패";
                return false;
            }

            // 초기화 직후 모든 Output OFF (안전 상태 확보)
            _io.AllOutputOff();

            return true;
        }

        /// <summary>
        /// 비전 시스템 초기화
        /// </summary>
        public bool InitializeVision()
        {
            if (_vision == null)
            {
                LastError = "Vision 모듈을 사용할 수 없습니다.";
                return false;
            }

            if (!_vision.Initialize())
            {
                LastError = "비전 시스템 초기화 실패";
                return false;
            }

            return true;
        }

        #endregion

        #region Shutdown

        /// <summary>
        /// 전체 시스템 종료
        /// 순서: CTC → Eddy → Vision → IO → Motion (초기화 역순)
        /// </summary>
        public void ShutdownAll()
        {
            try
            {
                // 인터락 모니터 정지
                _interlockMonitor?.Dispose();
                _interlockMonitor = null;

                // StatusBridge 해제
                _statusBridge?.Dispose();
                _statusBridge = null;

                // Sequence 이벤트 해제
                UnsubscribeSequenceEvents();

                // CTC 통신 종료
                StopCTCCommunication();

                // Eddy 센서 종료
                if (_eddy?.IsConnected == true)
                {
                    _eddy.Disconnect();
                }
                _eddy?.Dispose();

                // Vision 종료
                _vision?.Dispose();

                // IO 종료 (모든 출력 OFF 후 종료)
                if (_io?.IsInitialized == true)
                {
                    _io.SetLiftPinVacuum(false);
                    _io.SetLiftPinBlow(false);
                    _io.SetChuckVacuum(false);
                    _io.SetChuckBlow(false);
                    _io.Close();
                }

                // Motion 종료 (모든 축 정지 후 종료)
                if (_motion?.IsInitialized == true)
                {
                    _motion.EmergencyStopAll();
                    _motion.Close();
                }

                _isInitialized = false;

                // AppContext 상태 동기화
                AppState.Current.IsInitialized = false;
            }
            catch (Exception ex)
            {
                LastError = $"종료 중 예외 발생: {ex.Message}";
            }
        }

        #endregion

        #region Emergency

        /// <summary>
        /// 비상 정지
        /// </summary>
        public void EmergencyStop()
        {
            // 모션 비상 정지
            _motion?.EmergencyStopAll();

            // 모든 출력 OFF
            if (_io?.IsInitialized == true)
            {
                _io.SetLiftPinVacuum(false);
                _io.SetLiftPinBlow(false);
                _io.SetChuckVacuum(false);
                _io.SetChuckBlow(false);
            }

            // Vision Light OFF
            _vision?.SetLightOff();
        }

        #endregion

        #region Home

        /// <summary>
        /// 전체 축 원점 복귀
        /// </summary>
        public bool HomeAll()
        {
            if (_motion == null)
            {
                LastError = "Motion 모듈을 사용할 수 없습니다.";
                return false;
            }

            if (!_motion.IsInitialized)
            {
                LastError = "모션 컨트롤러가 초기화되지 않았습니다.";
                return false;
            }

            return _motion.HomeAll();
        }

        #endregion

        #region Private Methods

        private void RaiseInitializationProgress(string message, int currentStep, int totalSteps)
        {
            InitializationProgress?.Invoke(this, new InitializationProgressEventArgs(message, currentStep, totalSteps));
        }

        /// <summary>
        /// 축별 서보 알람 상시 체크 등록
        /// AlarmDefine.csv: 2011~2014 (축별 서보 알람)
        /// </summary>
        private void RegisterServoAlarmMonitor()
        {
            _interlockMonitor = new InterlockMonitor();

            // 축별 서보 알람 ID 매핑
            var axisAlarmMap = new Dictionary<VAMotionAxis, int>
            {
                { VAMotionAxis.WedgeUpDown,      2011 },
                { VAMotionAxis.ChuckRotation,    2012 },
                { VAMotionAxis.CenteringStage_1, 2013 },
                { VAMotionAxis.CenteringStage_2, 2014 },
            };

            foreach (var pair in axisAlarmMap)
            {
                var axis = pair.Key;
                int alarmId = pair.Value;

                _interlockMonitor.RegisterMonitorItem(
                    alarmId,
                    () => _motion.IsAlarm(axis),
                    () => $"Axis: {axis}"
                );
            }

            _interlockMonitor.Start();
            System.Diagnostics.Debug.WriteLine("[VisionAlignerSystem] Servo alarm monitor started (4 axes)");
        }

        /// <summary>
        /// AppContext 상태 동기화
        /// </summary>
        private void SyncAppContextState()
        {
            AppState.Current.IsInitialized = _isInitialized;
            AppState.Current.SystemStatus = _isInitialized ? SystemStatus.Idle : SystemStatus.Error;
        }

        #endregion
         
        #region CTC Communication

        /// <summary>
        /// CTC 통신 이벤트 구독 (비즈니스 로직용)
        /// </summary>
        private void SubscribeCTCEvents()
        {
            if (_ctcComm == null) return;

            // 명령 수신 이벤트 - 비즈니스 로직 처리
            _ctcComm.OnCommandReceived += HandleCTCCommand;
        }

        /// <summary>
        /// CTC 통신 시작
        /// </summary>
        private void StartCTCCommunication()
        {
            _ctcComm?.Start();
        }

        /// <summary>
        /// CTC 통신 종료
        /// </summary>
        private void StopCTCCommunication()
        {
            _ctcComm?.Stop();
        }

        /// <summary>
        /// CTC 명령 처리 (비즈니스 로직)
        /// </summary>
        private void HandleCTCCommand(CommObject.CommandObject cmd)
        {
            LogManager.System.Info($"[CTC Command] {cmd.Command}");

            // Local(PM) 모드에서는 CTC 명령 거부 (AllStop, AlarmClear 제외)
            if (AppState.Current.ControlAuthority == ControlAuthority.Local
                && cmd.Command != CommObject.CommandObject.eCMD.AllStop
                && cmd.Command != CommObject.CommandObject.eCMD.AlarmClear
                && cmd.Command != CommObject.CommandObject.eCMD.GetModuleStatus
                && cmd.Command != CommObject.CommandObject.eCMD.GetWaferCheck)
            {
                _ctcComm?.SendResponse(cmd.Command, false, "PM mode - command rejected");
                LogManager.System.Warn($"[CTC Command] Rejected (PM mode): {cmd.Command}");
                return;
            }

            switch (cmd.Command)
            {
                case CommObject.CommandObject.eCMD.Initialize:
                    HandleInitialize(cmd);
                    break;

                case CommObject.CommandObject.eCMD.MeasurementStart:
                    HandleMeasurementStart(cmd);
                    break;

                case CommObject.CommandObject.eCMD.MeasurementStop:
                    HandleMeasurementStop(cmd);
                    break;

                case CommObject.CommandObject.eCMD.TransferReady:
                    HandleTransferReady(cmd);
                    break;

                case CommObject.CommandObject.eCMD.AllStop:
                    HandleAllStop(cmd);
                    break;

                case CommObject.CommandObject.eCMD.AlarmClear:
                    HandleAlarmClear(cmd);
                    break;

                case CommObject.CommandObject.eCMD.SetRecipeData:
                    HandleSetRecipeData(cmd);
                    break;
            }
        }

        #region CTC Command Handlers

        private void HandleInitialize(CommObject.CommandObject cmd)
        {
            try
            {
                //if (_isInitialized)
                //{
                //    _ctcComm?.SendResponse(cmd.Command, true, "Already initialized");
                //    return;
                //}

                AppState.Current.SystemStatus = SystemStatus.Running;

                if (!InitializeAll())
                {
                    _ctcComm?.SendResponse(cmd.Command, false, LastError ?? "Initialize failed");
                    AppState.Current.SystemStatus = SystemStatus.Error;
                    return;
                }

                if (!HomeAll())
                {
                    _ctcComm?.SendResponse(cmd.Command, false, LastError ?? "HomeAll failed");
                    AppState.Current.SystemStatus = SystemStatus.Error;
                    return;
                }

                AppState.Current.IsHomed = true;
                _ctcComm?.SendResponse(cmd.Command, true);
                AppState.Current.SystemStatus = SystemStatus.Idle;
            }
            catch (Exception ex)
            {
                _ctcComm?.SendResponse(cmd.Command, false, ex.Message);
                AppState.Current.SystemStatus = SystemStatus.Error;
            }
        }

        private async void HandleMeasurementStart(CommObject.CommandObject cmd)
        {
            try
            {
                //// 사전 조건 체크 -> 주석
                //if (!_isInitialized)
                //{
                //    _ctcComm?.SendResponse(cmd.Command, false, "Not initialized");
                //    return;
                //}

                if (AppState.Current.SystemStatus == SystemStatus.Running)
                {
                    _ctcComm?.SendResponse(cmd.Command, false, "Already running");
                    return;
                }

                if (!AppState.Current.IsWaferExist)
                {
                    _ctcComm?.SendResponse(cmd.Command, false, "No wafer detected");
                    return;
                }

                if (_sequence == null)
                {
                    _ctcComm?.SendResponse(cmd.Command, false, "Sequence not available");
                    return;
                }

                // 상태 전환: Execute + NotReady
                AppState.Current.SystemStatus = SystemStatus.Running;
                SetCTCTransferStatus(CTCTransferStatus.NotReady);

                _ctcComm?.SendResponse(cmd.Command, true);

                // 시퀀스 비동기 실행
                // TODO: isFlat 판단 로직 (레시피에서 결정)
                bool isFlat = false;
                if (cmd.SetRecipeInfo != null) 
                {
                    isFlat = cmd.SetRecipeInfo.Type.Equals(RecipeEvalType.Flat) ? true : false;
                }

                bool result = await _sequence.RunSequenceAsync(isFlat);

                if (!result && _sequence.State != VisionAlignerSequence.SequenceState.Aborted)
                {
                    LogManager.System.Error($"[MeasurementStart] Sequence failed: {_sequence.LastError}");
                }

                // 완료 후 상태는 OnSequenceCompleted에서 처리됨
            }
            catch (Exception ex)
            {
                AppState.Current.SystemStatus = SystemStatus.Error;
                LogManager.System.Error($"[MeasurementStart] Error: {ex.Message}");
            }
        }

        private void HandleMeasurementStop(CommObject.CommandObject cmd)
        {
            try
            {
                _sequence?.Stop();
                AppState.Current.SystemStatus = SystemStatus.Idle;
                _ctcComm?.SendResponse(cmd.Command, true);
            }
            catch (Exception ex)
            {
                _ctcComm?.SendResponse(cmd.Command, false, ex.Message);
            }
        }

        private async void HandleTransferReady(CommObject.CommandObject cmd)
        {
            try
            {
                if (AppState.Current.SystemStatus == SystemStatus.Running)
                {
                    _ctcComm?.SendResponse(cmd.Command, false, "System is running");
                    return;
                }

                if (_sequence == null)
                {
                    _ctcComm?.SendResponse(cmd.Command, false, "Sequence not available");
                    return;
                }

                AppState.Current.SystemStatus = SystemStatus.Running;

                bool success;
                if (AppState.Current.IsWaferExist)
                {
                    // 웨이퍼 있음 → Get 준비 (Centering Open + ChuckZ Down + LiftPin Blow)
                    success = await _sequence.PrepareForGetAsync();
                    if (success)
                    {
                        SetCTCTransferStatus(CTCTransferStatus.GetReady);
                    }
                }
                else
                {
                    // 웨이퍼 없음 → Put 준비 (Centering Open + ChuckZ Down + LiftPin Blow)
                    success = await _sequence.PrepareForPutAsync();
                    if (success)
                    {
                        SetCTCTransferStatus(CTCTransferStatus.PutReady);
                    }
                }

                AppState.Current.SystemStatus = SystemStatus.Idle;

                if (success)
                {
                    _ctcComm?.SendResponse(cmd.Command, true);
                }
                else
                {
                    _ctcComm?.SendResponse(cmd.Command, false, "Transfer ready failed");
                    AppState.Current.SystemStatus = SystemStatus.Error;
                }
            }
            catch (Exception ex)
            {
                AppState.Current.SystemStatus = SystemStatus.Error;
                _ctcComm?.SendResponse(cmd.Command, false, ex.Message);
            }
        }

        private void HandleAllStop(CommObject.CommandObject cmd)
        {
            try
            {
                EmergencyStop();
                _sequence?.Stop();
                AppState.Current.SystemStatus = SystemStatus.EMO;
                AppState.Current.IsEmergencyStop = true;
                SetCTCTransferStatus(CTCTransferStatus.NotReady);
                _ctcComm?.SendResponse(cmd.Command, true);
            }
            catch (Exception ex)
            {
                _ctcComm?.SendResponse(cmd.Command, false, ex.Message);
            }
        }

        private void HandleAlarmClear(CommObject.CommandObject cmd)
        {
            try
            {
                InterlockManager.Instance.ClearAllAlarms();
                AppState.Current.IsEmergencyStop = false;
                AppState.Current.SystemStatus = SystemStatus.Idle;
                AppState.Current.IsHomed = false; // 알람 후 홈 재수행 필요

                // 웨이퍼 유무에 따라 TransferStatus 복구
                if (AppState.Current.IsWaferExist)
                    SetCTCTransferStatus(CTCTransferStatus.GetReady);
                else
                    SetCTCTransferStatus(CTCTransferStatus.PutReady);

                _ctcComm?.SendResponse(cmd.Command, true);
            }
            catch (Exception ex)
            {
                _ctcComm?.SendResponse(cmd.Command, false, ex.Message);
            }
        }

        private void HandleSetRecipeData(CommObject.CommandObject cmd)
        {
            try
            {
                // TODO: 레시피 데이터 적용
                LogManager.System.Info($"[SetRecipeData] RecipeIndex: {cmd.SetRecipeIndex}");
                _ctcComm?.SendResponse(cmd.Command, true);
            }
            catch (Exception ex)
            {
                _ctcComm?.SendResponse(cmd.Command, false, ex.Message);
            }
        }

        #endregion

        #endregion

        #region Sequence-CTC Coordination

        /// <summary>
        /// Sequence 이벤트 구독
        /// </summary>
        private void SubscribeSequenceEvents()
        {
            if (_sequence == null) return;

            _sequence.TransferStatusChangeRequested += OnSequenceTransferStatusChanged;
            _sequence.SequenceCompleted += OnSequenceCompleted;
            _sequence.ErrorOccurred += OnSequenceError;
        }

        /// <summary>
        /// Sequence 이벤트 해제
        /// </summary>
        private void UnsubscribeSequenceEvents()
        {
            if (_sequence == null) return;

            _sequence.TransferStatusChangeRequested -= OnSequenceTransferStatusChanged;
            _sequence.SequenceCompleted -= OnSequenceCompleted;
            _sequence.ErrorOccurred -= OnSequenceError;
        }

        /// <summary>
        /// Sequence 에러 발생 시 알람 연동
        /// </summary>
        private void OnSequenceError(object sender, SequenceErrorEventArgs e)
        {
            InterlockManager.Instance.RaiseAlarm(e.AlarmCode, "Sequence", e.Message);
        }

        /// <summary>
        /// Sequence 완료 처리 - AppContext 동기화 및 이벤트 발행
        /// </summary>
        private void OnSequenceCompleted(object sender, WaferVisionResult result)
        {
            // DB에 결과 저장
            bool isFlat = _sequence?.IsFlat ?? false;
            ResultRepository.Instance.Insert(result, isFlat);

            // AppContext에 결과 저장
            AppState.Current.LastVisionResult = result;
            AppState.Current.TotalRunCount++;
            AppState.Current.SystemStatus = SystemStatus.Idle;

            // EventManager로 시퀀스 완료 이벤트 발행 (UI 갱신용)
            EventManager.Publish(EventManager.SequenceCompleted, result);
            //if (AppState.Current.SystemMode == SystemMode.Auto)
            //{
            //    SetCTCTransferStatus(CTCTransferStatus.GetReady, result);
            //}
        }

        /// <summary>
        /// Sequence의 CTC 상태 전환 요청 처리
        /// </summary>
        private void OnSequenceTransferStatusChanged(object sender, CTCTransferStatusEventArgs e)
        {
            SetCTCTransferStatus(e.Status, e.Result);
        }

        /// <summary>
        /// CTC Transfer 상태 설정 및 전송
        /// </summary>
        /// <param name="status">설정할 상태</param>
        /// <param name="result">측정 결과 (GetReady 시 전송)</param>
        private void SetCTCTransferStatus(CTCTransferStatus status, WaferVisionResult? result = null)
        {
            if (_ctcComm == null) return;

            // CTCTransferStatus를 StatusObject.eTransferStatus로 변환
            CommObject.StatusObject.eTransferStatus ctcStatus;
            switch (status)
            {
                case CTCTransferStatus.GetReady:
                    ctcStatus = CommObject.StatusObject.eTransferStatus.GetReady;
                    break;
                case CTCTransferStatus.PutReady:
                    ctcStatus = CommObject.StatusObject.eTransferStatus.PutReady;
                    break;
                case CTCTransferStatus.NotReady:
                default:
                    ctcStatus = CommObject.StatusObject.eTransferStatus.NotReady;
                    break;
            }

            // 상태 직접 업데이트
            _ctcComm.CurrentStatus.TransferStatus = ctcStatus;

            // 상태 전송
            _ctcComm.SendStatus();

            // GetReady 상태일 때 결과 데이터 전송
            if (status == CTCTransferStatus.GetReady && result.HasValue)
            {
                SendMeasurementResult(result.Value);
            }
        }

        /// <summary>
        /// 측정 결과 전송
        /// </summary>
        private void SendMeasurementResult(WaferVisionResult result)
        {
            if (_ctcComm == null) return;

            var resultObj = new CommObject.ResultObject
            {
                Type = result.Index1st >= 0 ? RecipeEvalType.Notch : 
                            result.Index2nd >= 0 ?RecipeEvalType.Flat : RecipeEvalType.Glass,
                Radius = result.Radius,
                Width = result.Width,
                Height = result.Height,
                Eddy = result.EddyValue,
                PN = result.PNValue.Equals(1) ? true : false, 
                // TODO: CenterX, CenterY, ThetaOffset 등 결과 데이터 추가 필요
                // ResultObject 확장 또는 별도 전송 방식 필요
            };

            _ctcComm.SendResult(resultObj);
        }

        #endregion

        #region AppState Events

        private Action<object> _appStateChangedHandler;
        private Action<object> _alarmOccurredHandler;

        /// <summary>
        /// AppState 변경 이벤트 구독
        /// </summary>
        private void SubscribeAppStateEvents()
        {
            _appStateChangedHandler = OnAppStateChanged;
            EventManager.Subscribe(EventManager.SystemStateChanged, _appStateChangedHandler);

            // 알람 발생 이벤트 구독 (CTC 전달용)
            _alarmOccurredHandler = OnAlarmOccurred;
            EventManager.Subscribe(EventManager.AlarmOccurred, _alarmOccurredHandler);
        }

        /// <summary>
        /// AppState 변경 이벤트 해제
        /// </summary>
        private void UnsubscribeAppStateEvents()
        {
            if (_appStateChangedHandler != null)
            {
                EventManager.Unsubscribe(EventManager.SystemStateChanged, _appStateChangedHandler);
                _appStateChangedHandler = null;
            }

            if (_alarmOccurredHandler != null)
            {
                EventManager.Unsubscribe(EventManager.AlarmOccurred, _alarmOccurredHandler);
                _alarmOccurredHandler = null;
            }
        }

        /// <summary>
        /// 알람 발생 핸들러 - Severity에 따른 시스템 동작 분기
        /// Critical/Error: 모션 정지 + IO OFF + 시퀀스 중단 + CTC 통보
        /// Warning: 로그 + UI 알림(10초) + 자동 Clear
        /// Info: 로그만
        /// </summary>
        private void OnAlarmOccurred(object data)
        {
            if (!(data is Interlock.AlarmInfo alarm) || alarm.Definition == null)
                return;

            var severity = alarm.Definition.Severity;

            switch (severity)
            {
                case Interlock.AlarmSeverity.Critical:
                case Interlock.AlarmSeverity.Error:
                    // 비상 정지: 모션 전축 정지 + Output IO 전체 OFF
                    EmergencyStop();

                    // 시퀀스 중단
                    if (_sequence != null && _sequence.State == VisionAlignerSequence.SequenceState.Running)
                    {
                        _sequence.Stop();
                    }

                    // SystemStatus를 Error로 전환
                    AppState.Current.SystemStatus = SystemStatus.Error;

                    LogManager.System.Error(
                        $"[{severity}] 시스템 정지 - [{alarm.Definition.Code}] {alarm.Definition.Name}: " +
                        $"{alarm.AdditionalMessage ?? alarm.Definition.Description}");
                    break;

                case Interlock.AlarmSeverity.Warning:
                    LogManager.System.Warn(
                        $"[Warning] [{alarm.Definition.Code}] {alarm.Definition.Name}: " +
                        $"{alarm.AdditionalMessage ?? alarm.Definition.Description}");

                    // 10초 후 자동 Clear
                    var warningAlarmId = alarm.Definition.Id;
                    System.Threading.Tasks.Task.Delay(10000).ContinueWith(_ =>
                    {
                        try
                        {
                            Interlock.InterlockManager.Instance.ClearAlarm(warningAlarmId);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"[VisionAlignerSystem] Warning auto-clear failed: {ex.Message}");
                        }
                    });
                    break;

                case Interlock.AlarmSeverity.Info:
                    LogManager.System.Info(
                        $"[Info] [{alarm.Definition.Code}] {alarm.Definition.Name}: " +
                        $"{alarm.AdditionalMessage ?? alarm.Definition.Description}");
                    break;
            }

            // CTC로 알람 전달 (모든 Severity)
            try
            {
                 _ctcComm?.SendAlarm(
                    alarm.Definition.Id,
                    alarm.Definition.Name,
                    alarm.AdditionalMessage ?? alarm.Definition.Description);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[VisionAlignerSystem] SendAlarm failed: {ex.Message}");
            }
        }

        /// <summary>
        /// AppState 변경 핸들러 - CTC 상태 동기화
        /// </summary>
        private void OnAppStateChanged(object data)
        {
            if (data is string propertyName)
            {
                switch (propertyName)
                {
                    case nameof(AppState.IsWaferExist):
                        // CTC에 웨이퍼 존재 상태 동기화
                        _ctcComm?.UpdateWaferPresence(AppState.Current.IsWaferExist);
                        break;
                }
            }
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // AppState 이벤트 구독 해제
                UnsubscribeAppStateEvents();

                ShutdownAll();
            }

            _disposed = true;
        }

        #endregion
    }

    #region Event Args

    /// <summary>
    /// 초기화 진행 상태 이벤트 인자
    /// </summary>
    public class InitializationProgressEventArgs : EventArgs
    {
        public string Message { get; }
        public int CurrentStep { get; }
        public int TotalSteps { get; }
        public int ProgressPercent => TotalSteps > 0 ? (CurrentStep * 100) / TotalSteps : 0;

        public InitializationProgressEventArgs(string message, int currentStep, int totalSteps)
        {
            Message = message;
            CurrentStep = currentStep;
            TotalSteps = totalSteps;
        }
    }

    #endregion
}
