using System;
using VisionAlignChamber.Config;
using VisionAlignChamber.Interfaces;
using VisionAlignChamber.Models;

namespace VisionAlignChamber.Core
{
    /// <summary>
    /// 애플리케이션 전역 컨텍스트 (싱글톤)
    /// 시스템 상태, 공유 데이터, 서비스 참조를 중앙에서 관리합니다.
    /// </summary>
    /// <remarks>
    /// 사용 예시:
    /// - 상태 접근: AppContext.Current.SystemState
    /// - 데이터 접근: AppContext.Current.LastVisionResult
    /// - 서비스 접근: AppContext.Current.Vision
    /// </remarks>
    public class AppContext
    {
        #region Singleton

        private static readonly Lazy<AppContext> _instance = new Lazy<AppContext>(() => new AppContext());

        /// <summary>
        /// 싱글톤 인스턴스
        /// </summary>
        public static AppContext Current => _instance.Value;

        private AppContext()
        {
            // 기본값 초기화
            _systemStatus = SystemStatus.Idle;
            _controlAuthority = ControlAuthority.Local;
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
                    OnDataChanged(nameof(SystemStatus));
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
                    OnDataChanged(nameof(ControlAuthority));
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
                    OnDataChanged(nameof(IsEmergencyStop));
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
                    OnDataChanged(nameof(IsInitialized));
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
                OnDataChanged(nameof(LastVisionResult));
                EventManager.Publish(EventManager.InspectionComplete, value);
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
                    OnDataChanged(nameof(CurrentRunStep));
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
                    OnDataChanged(nameof(TotalRunCount));
                }
            }
        }

        #endregion

        #region Services

        /// <summary>
        /// 비전 프로세서
        /// </summary>
        public IVisionProcessor Vision { get; set; }

        /// <summary>
        /// 모션 컨트롤러
        /// </summary>
        public IMotionController Motion { get; set; }

        /// <summary>
        /// 디지털 I/O
        /// </summary>
        public IDigitalIO DigitalIO { get; set; }

        #endregion

        #region Settings

        // Note: AppSettings는 static 클래스이므로 직접 접근 (예: AppSettings.SimulationMode)

        #endregion

        #region Helper Methods

        /// <summary>
        /// 데이터 변경 알림 (EventManager 연동)
        /// </summary>
        private void OnDataChanged(string propertyName)
        {
            EventManager.Publish(EventManager.SystemStateChanged, propertyName);
        }

        /// <summary>
        /// 시스템 상태 메시지 발행
        /// </summary>
        public void PublishStatusMessage(string message)
        {
            EventManager.Publish(EventManager.StatusMessage, message);
        }

        /// <summary>
        /// 알람 발생
        /// </summary>
        public void RaiseAlarm(AlarmInfo alarm)
        {
            EventManager.Publish(EventManager.AlarmOccurred, alarm);
        }

        /// <summary>
        /// 알람 해제
        /// </summary>
        public void ClearAlarm(AlarmInfo alarm)
        {
            EventManager.Publish(EventManager.AlarmCleared, alarm);
        }

        /// <summary>
        /// 시스템 초기화
        /// </summary>
        public void Initialize()
        {
            SystemStatus = SystemStatus.Idle;
            ControlAuthority = ControlAuthority.Local;
            IsEmergencyStop = false;
            IsInitialized = true;
            CurrentRunStep = 0;
            TotalRunCount = 0;
            _lastVisionResult = WaferVisionResult.Empty;
        }

        /// <summary>
        /// 시스템 종료 정리
        /// </summary>
        public void Shutdown()
        {
            IsInitialized = false;
            SystemStatus = SystemStatus.Idle;

            // 서비스 정리
            Vision?.Dispose();
            Motion?.Close();
            DigitalIO?.Close();

            // 이벤트 정리
            EventManager.Clear();
        }

        #endregion
    }
}
