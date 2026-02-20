using System;
using CommObject;
using VisionAlignChamber.Hardware.Facade;
using VisionAlignChamber.Vision;
using VisionAlignChamber.Interfaces;
using VisionAlignChamber.Communication.Interfaces;
using VisionAlignChamber.Models;

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

                // Sequence 생성 (Motion과 IO가 필수)
                if (HasMotion && HasIO)
                {
                    _sequence = new VisionAlignerSequence(_motion, _io, _vision, _eddy);
                    SubscribeSequenceEvents();
                }

                // 초기화 완료 시 CTC 상태: PutReady (웨이퍼 수신 대기)
                SetCTCTransferStatus(CTCTransferStatus.PutReady);

                // AppContext 상태 동기화
                SyncAppContextState();

                RaiseInitializationProgress("시스템 초기화 완료", totalSteps, totalSteps);
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

            if (_motion.IsInitialized)
            {
                return true;
            }

            if (!_motion.Initialize())
            {
                LastError = "모션 컨트롤러 초기화 실패";
                return false;
            }

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

            if (_io.IsInitialized)
            {
                return true;
            }

            if (!_io.Initialize())
            {
                LastError = "디지털 IO 초기화 실패";
                return false;
            }

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

            if (_vision.IsInitialized)
            {
                return true;
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
                    _io.SetVisionLight(false);
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
                AppContext.Current.IsInitialized = false;
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
                _io.SetVisionLight(false);
            }
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
        /// AppContext 상태 동기화
        /// </summary>
        private void SyncAppContextState()
        {
            AppContext.Current.IsInitialized = _isInitialized;
            AppContext.Current.SystemStatus = _isInitialized ? SystemStatus.Idle : SystemStatus.Error;
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
            // 비즈니스 로직에 따른 명령 처리
            // UI와 무관한 로직만 여기서 처리
            // 예: Initialize, MeasurementStart, TransferReady 등

            switch (cmd.Command)
            {
                case CommObject.CommandObject.eCMD.Initialize:
                    // 초기화 명령 처리
                    break;

                case CommObject.CommandObject.eCMD.MeasurementStart:
                    // 측정 시작 명령 처리
                    break;

                case CommObject.CommandObject.eCMD.TransferReady:
                    // 전송 준비 명령 처리
                    break;

                // 다른 명령들은 필요에 따라 추가
            }
        }

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
        }

        /// <summary>
        /// Sequence 이벤트 해제
        /// </summary>
        private void UnsubscribeSequenceEvents()
        {
            if (_sequence == null) return;

            _sequence.TransferStatusChangeRequested -= OnSequenceTransferStatusChanged;
            _sequence.SequenceCompleted -= OnSequenceCompleted;
        }

        /// <summary>
        /// Sequence 완료 처리 - AppContext 동기화
        /// </summary>
        private void OnSequenceCompleted(object sender, WaferVisionResult result)
        {
            // AppContext에 결과 저장
            AppContext.Current.LastVisionResult = result;
            AppContext.Current.TotalRunCount++;
            AppContext.Current.SystemStatus = SystemStatus.Idle;
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
                MeasureTime = DateTime.Now,
                EddyValue = result.EddyValue
                // TODO: CenterX, CenterY, ThetaOffset 등 결과 데이터 추가 필요
                // ResultObject 확장 또는 별도 전송 방식 필요
            };

            _ctcComm.SendResult(resultObj);
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
