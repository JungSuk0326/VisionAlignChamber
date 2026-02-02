using System;
using VisionAlignChamber.Hardware.IO;
using VisionAlignChamber.Vision;

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

        private bool _isInitialized;
        private bool _disposed;

        #endregion

        #region Constructor

        public VisionAlignerSystem(VisionAlignerMotion motion, VisionAlignerIO io, VisionAlignWrapper vision)
        {
            _motion = motion ?? throw new ArgumentNullException(nameof(motion));
            _io = io ?? throw new ArgumentNullException(nameof(io));
            _vision = vision ?? throw new ArgumentNullException(nameof(vision));
        }

        #endregion

        #region Properties

        /// <summary>
        /// 시스템 초기화 상태
        /// </summary>
        public bool IsInitialized => _isInitialized;

        /// <summary>
        /// 모션 컨트롤러 초기화 상태
        /// </summary>
        public bool IsMotionInitialized => _motion.IsInitialized;

        /// <summary>
        /// 디지털 IO 초기화 상태
        /// </summary>
        public bool IsIOInitialized => _io.IsInitialized;

        /// <summary>
        /// 비전 초기화 상태
        /// </summary>
        public bool IsVisionInitialized => _vision.IsInitialized;

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
        /// 전체 시스템 초기화
        /// 순서: Motion → IO → Vision
        /// </summary>
        /// <returns>성공 여부</returns>
        public bool InitializeAll()
        {
            if (_isInitialized)
            {
                LastError = "이미 초기화되어 있습니다.";
                return true;
            }

            try
            {
                // Step 1: Motion 초기화
                RaiseInitializationProgress("모션 컨트롤러 초기화 중...", 0, 3);
                if (!InitializeMotion())
                {
                    return false;
                }

                // Step 2: IO 초기화
                RaiseInitializationProgress("디지털 IO 초기화 중...", 1, 3);
                if (!InitializeIO())
                {
                    return false;
                }

                // Step 3: Vision 초기화
                RaiseInitializationProgress("비전 시스템 초기화 중...", 2, 3);
                if (!InitializeVision())
                {
                    return false;
                }

                _isInitialized = true;
                RaiseInitializationProgress("시스템 초기화 완료", 3, 3);
                return true;
            }
            catch (Exception ex)
            {
                LastError = $"초기화 중 예외 발생: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// 모션 컨트롤러 초기화
        /// 초기화 완료 후 모든 축 서보 ON
        /// </summary>
        public bool InitializeMotion()
        {
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
        /// 순서: Vision → IO → Motion (초기화 역순)
        /// </summary>
        public void ShutdownAll()
        {
            try
            {
                // Vision 종료
                _vision?.Dispose();

                // IO 종료 (모든 출력 OFF 후 종료)
                if (_io.IsInitialized)
                {
                    _io.SetLiftPinVacuum(false);
                    _io.SetLiftPinBlow(false);
                    _io.SetChuckVacuum(false);
                    _io.SetChuckBlow(false);
                    _io.SetVisionLight(false);
                    _io.Close();
                }

                // Motion 종료 (모든 축 정지 후 종료)
                if (_motion.IsInitialized)
                {
                    _motion.EmergencyStopAll();
                    _motion.Close();
                }

                _isInitialized = false;
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
            _motion.EmergencyStopAll();

            // 모든 출력 OFF
            if (_io.IsInitialized)
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
