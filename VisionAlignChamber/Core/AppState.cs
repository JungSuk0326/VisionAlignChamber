using System;
using VisionAlignChamber.Database;
using VisionAlignChamber.Models;

namespace VisionAlignChamber.Core
{
    /// <summary>
    /// 애플리케이션 전역 상태 관리 (싱글톤)
    /// </summary>
    /// <remarks>
    /// 역할:
    /// - 시스템 상태 중앙 관리 (SystemStatus, IsEmergencyStop 등)
    /// - 프로세스 데이터 공유 (LastVisionResult, TotalRunCount 등)
    /// - UI 알림용 EventManager 연동
    ///
    /// 하드웨어 제어는 VisionAlignerSystem이 담당합니다.
    /// </remarks>
    public class AppState
    {
        #region Singleton

        private static readonly Lazy<AppState> _instance = new Lazy<AppState>(() => new AppState());

        /// <summary>
        /// 싱글톤 인스턴스
        /// </summary>
        public static AppState Current => _instance.Value;

        private AppState()
        {
            _systemStatus = SystemStatus.Idle;
            _controlAuthority = ControlAuthority.Remote;
        }

        #endregion

        #region System State

        private SystemStatus _systemStatus;
        /// <summary>
        /// 현재 시스템 상태 (Idle, Running, Error, EMO 등)
        /// </summary>
        public SystemStatus SystemStatus
        {
            get => _systemStatus;
            set
            {
                if (_systemStatus != value)
                {
                    _systemStatus = value;
                    OnStateChanged(nameof(SystemStatus));
                }
            }
        }

        private ControlAuthority _controlAuthority;
        /// <summary>
        /// 현재 제어 권한 (Local, Remote, Locked)
        /// </summary>
        public ControlAuthority ControlAuthority
        {
            get => _controlAuthority;
            set
            {
                if (_controlAuthority != value)
                {
                    _controlAuthority = value;
                    OnStateChanged(nameof(ControlAuthority));
                    EventManager.Publish(EventManager.ControlAuthorityChanged, value);
                }
            }
        }

        private bool _isEmergencyStop;
        /// <summary>
        /// 비상정지 상태
        /// </summary>
        public bool IsEmergencyStop
        {
            get => _isEmergencyStop;
            set
            {
                if (_isEmergencyStop != value)
                {
                    _isEmergencyStop = value;
                    OnStateChanged(nameof(IsEmergencyStop));
                }
            }
        }

        private bool _isInitialized;
        /// <summary>
        /// 시스템 초기화 완료 여부
        /// </summary>
        public bool IsInitialized
        {
            get => _isInitialized;
            set
            {
                if (_isInitialized != value)
                {
                    _isInitialized = value;
                    OnStateChanged(nameof(IsInitialized));
                }
            }
        }

        private bool _isHomed;
        /// <summary>
        /// 원점 복귀 완료 여부
        /// </summary>
        public bool IsHomed
        {
            get => _isHomed;
            set
            {
                if (_isHomed != value)
                {
                    _isHomed = value;
                    OnStateChanged(nameof(IsHomed));
                }
            }
        }

        private bool _isWaferExist;
        /// <summary>
        /// 웨이퍼 존재 여부 (Sensor1 AND Sensor2)
        /// </summary>
        public bool IsWaferExist
        {
            get => _isWaferExist;
            set
            {
                if (_isWaferExist != value)
                {
                    _isWaferExist = value;
                    OnStateChanged(nameof(IsWaferExist));
                }
            }
        }

        private SystemMode _systemMode;
        /// <summary>
        /// 현재 운전 모드 (Manual, Auto, Setup)
        /// </summary>
        public SystemMode SystemMode
        {
            get => _systemMode;
            set
            {
                if (_systemMode != value)
                {
                    _systemMode = value;
                    OnStateChanged(nameof(SystemMode));
                }
            }
        }

        #endregion

        #region Process Data

        private WaferVisionResult _lastVisionResult;
        /// <summary>
        /// 최근 비전 검사 결과
        /// </summary>
        public WaferVisionResult LastVisionResult
        {
            get => _lastVisionResult;
            set
            {
                _lastVisionResult = value;
                OnStateChanged(nameof(LastVisionResult));
            }
        }

        private int _currentRunStep;
        /// <summary>
        /// 현재 Running Step
        /// </summary>
        public int CurrentRunStep
        {
            get => _currentRunStep;
            set
            {
                if (_currentRunStep != value)
                {
                    _currentRunStep = value;
                    OnStateChanged(nameof(CurrentRunStep));
                }
            }
        }

        private int _totalRunCount;
        /// <summary>
        /// 총 Running Count
        /// </summary>
        public int TotalRunCount
        {
            get => _totalRunCount;
            set
            {
                if (_totalRunCount != value)
                {
                    _totalRunCount = value;
                    OnStateChanged(nameof(TotalRunCount));
                }
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// 상태 변경 알림 (UI 갱신용)
        /// </summary>
        private void OnStateChanged(string propertyName)
        {
            EventManager.Publish(EventManager.SystemStateChanged, propertyName);
        }

        /// <summary>
        /// 상태 메시지 발행 (UI 상태바용)
        /// </summary>
        public void PublishStatusMessage(string message)
        {
            EventManager.Publish(EventManager.StatusMessage, message);
        }

        /// <summary>
        /// 상태 초기화
        /// </summary>
        public void Initialize()
        {
            // 데이터베이스 초기화 (테이블 자동 생성)
            var db = DatabaseManager.Instance;

            SystemStatus = SystemStatus.Idle;
            ControlAuthority = ControlAuthority.Remote;
            IsEmergencyStop = false;
            IsInitialized = false;  // VisionAlignerSystem.InitializeAll() 후 true로 설정됨
            IsHomed = false;
            IsWaferExist = false;
            SystemMode = SystemMode.Manual;
            CurrentRunStep = 0;
            TotalRunCount = 0;
            _lastVisionResult = WaferVisionResult.Empty;
        }

        /// <summary>
        /// 상태 정리
        /// </summary>
        public void Shutdown()
        {
            IsInitialized = false;
            SystemStatus = SystemStatus.Idle;

            // 이벤트 구독 정리
            EventManager.Clear();
        }

        #endregion
    }
}
