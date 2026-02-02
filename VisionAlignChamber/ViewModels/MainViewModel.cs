using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VisionAlignChamber.ViewModels.Base;
using VisionAlignChamber.Models;
using VisionAlignChamber.Core;

namespace VisionAlignChamber.ViewModels
{
    /// <summary>
    /// 메인 ViewModel - 모든 ViewModel 조율
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private VisionAlignerSystem _system;

        #endregion

        #region Child ViewModels

        public MotionViewModel Motion { get; private set; }
        public IOViewModel IO { get; private set; }
        public VisionViewModel Vision { get; private set; }

        #endregion

        #region System State

        private SystemState _systemState;
        public SystemState SystemState
        {
            get => _systemState;
            set => SetProperty(ref _systemState, value);
        }

        private SystemMode _currentMode;
        public SystemMode CurrentMode
        {
            get => _currentMode;
            set
            {
                if (SetProperty(ref _currentMode, value))
                {
                    SystemState.Mode = value;
                    OnPropertyChanged(nameof(IsManualMode));
                    OnPropertyChanged(nameof(IsAutoMode));
                    OnPropertyChanged(nameof(IsSetupMode));
                }
            }
        }

        public bool IsManualMode => CurrentMode == SystemMode.Manual;
        public bool IsAutoMode => CurrentMode == SystemMode.Auto;
        public bool IsSetupMode => CurrentMode == SystemMode.Setup;

        private SystemStatus _currentStatus;
        public SystemStatus CurrentStatus
        {
            get => _currentStatus;
            set
            {
                if (SetProperty(ref _currentStatus, value))
                {
                    SystemState.Status = value;
                    OnPropertyChanged(nameof(IsIdle));
                    OnPropertyChanged(nameof(IsRunning));
                    OnPropertyChanged(nameof(IsError));
                }
            }
        }

        public bool IsIdle => CurrentStatus == SystemStatus.Idle;
        public bool IsRunning => CurrentStatus == SystemStatus.Running;
        public bool IsError => CurrentStatus == SystemStatus.Error || CurrentStatus == SystemStatus.EMO;

        #endregion

        #region Properties

        private bool _isInitialized;
        public bool IsInitialized
        {
            get => _isInitialized;
            set
            {
                if (SetProperty(ref _isInitialized, value))
                {
                    SystemState.IsInitialized = value;
                }
            }
        }

        private bool _isHomed;
        public bool IsHomed
        {
            get => _isHomed;
            set
            {
                if (SetProperty(ref _isHomed, value))
                {
                    SystemState.IsHomed = value;
                }
            }
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        private AlignParameters _alignParameters;
        public AlignParameters AlignParameters
        {
            get => _alignParameters;
            set => SetProperty(ref _alignParameters, value);
        }

        #endregion

        #region Alarm

        private ObservableCollection<AlarmInfo> _activeAlarms;
        public ObservableCollection<AlarmInfo> ActiveAlarms
        {
            get => _activeAlarms;
            set => SetProperty(ref _activeAlarms, value);
        }

        private bool _hasActiveAlarm;
        public bool HasActiveAlarm
        {
            get => _hasActiveAlarm;
            set => SetProperty(ref _hasActiveAlarm, value);
        }

        #endregion

        #region Constructor

        public MainViewModel()
        {
            // 상태 초기화
            SystemState = new SystemState();
            AlignParameters = AlignParameters.CreateDefault();
            ActiveAlarms = new ObservableCollection<AlarmInfo>();

            InitializeCommands();
        }

        /// <summary>
        /// 하드웨어 연결 후 ViewModel 초기화
        /// </summary>
        public void Initialize(VisionAlignerSystem system)
        {
            _system = system ?? throw new ArgumentNullException(nameof(system));

            // 이벤트 구독
            _system.InitializationProgress += OnInitializationProgress;

            // Child ViewModel 생성
            Motion = new MotionViewModel(_system.Motion);
            IO = new IOViewModel(_system.IO);
            Vision = new VisionViewModel(_system.Vision);

            OnPropertyChanged(nameof(Motion));
            OnPropertyChanged(nameof(IO));
            OnPropertyChanged(nameof(Vision));

            StatusMessage = "시스템 준비 완료";
        }

        private void OnInitializationProgress(object sender, InitializationProgressEventArgs e)
        {
            StatusMessage = e.Message;
        }

        #endregion

        #region Commands

        public ICommand SetManualModeCommand { get; private set; }
        public ICommand SetAutoModeCommand { get; private set; }
        public ICommand SetSetupModeCommand { get; private set; }

        public ICommand InitializeSystemCommand { get; private set; }
        public ICommand HomeAllCommand { get; private set; }
        public ICommand EmergencyStopCommand { get; private set; }
        public ICommand ResetAlarmCommand { get; private set; }

        private void InitializeCommands()
        {
            SetManualModeCommand = new RelayCommand(() => CurrentMode = SystemMode.Manual);
            SetAutoModeCommand = new RelayCommand(() => CurrentMode = SystemMode.Auto, () => IsInitialized && IsHomed);
            SetSetupModeCommand = new RelayCommand(() => CurrentMode = SystemMode.Setup);

            InitializeSystemCommand = new RelayCommand(ExecuteInitializeSystem, () => !IsInitialized);
            HomeAllCommand = new RelayCommand(ExecuteHomeAll, () => IsInitialized && !IsHomed);
            EmergencyStopCommand = new RelayCommand(ExecuteEmergencyStop);
            ResetAlarmCommand = new RelayCommand(ExecuteResetAlarm, () => HasActiveAlarm);
        }

        #endregion

        #region Command Implementations

        private void ExecuteInitializeSystem()
        {
            try
            {
                CurrentStatus = SystemStatus.Running;
                StatusMessage = "시스템 초기화 중...";

                // VisionAlignerSystem을 통해 초기화 (비즈니스 로직)
                if (_system == null)
                {
                    throw new InvalidOperationException("VisionAlignerSystem이 설정되지 않았습니다.");
                }

                if (!_system.InitializeAll())
                {
                    throw new Exception(_system.LastError ?? "시스템 초기화 실패");
                }

                // ViewModel 상태 갱신
                OnPropertyChanged(nameof(Motion));
                OnPropertyChanged(nameof(IO));
                OnPropertyChanged(nameof(Vision));

                IsInitialized = true;
                CurrentStatus = SystemStatus.Idle;
                StatusMessage = "시스템 초기화 완료";

                RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                AddAlarm(AlarmCodes.INITIALIZATION_FAILED, ex.Message, AlarmLevel.Error, AlarmSource.System);
                CurrentStatus = SystemStatus.Error;
                StatusMessage = $"초기화 실패: {ex.Message}";
            }
        }

        private void ExecuteHomeAll()
        {
            try
            {
                CurrentStatus = SystemStatus.Running;
                StatusMessage = "원점 복귀 중...";

                // VisionAlignerSystem을 통해 원점 복귀 (비즈니스 로직)
                if (!_system.HomeAll())
                {
                    throw new Exception(_system.LastError ?? "원점 복귀 실패");
                }

                IsHomed = true;
                CurrentStatus = SystemStatus.Idle;
                StatusMessage = "원점 복귀 완료";

                RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                AddAlarm(AlarmCodes.HOME_FAILED, ex.Message, AlarmLevel.Error, AlarmSource.Motion);
                CurrentStatus = SystemStatus.Error;
                StatusMessage = $"원점 복귀 실패: {ex.Message}";
            }
        }

        private void ExecuteEmergencyStop()
        {
            // VisionAlignerSystem을 통해 비상 정지 (비즈니스 로직)
            _system?.EmergencyStop();

            CurrentStatus = SystemStatus.EMO;
            StatusMessage = "비상 정지!";

            AddAlarm(AlarmCodes.EMO_ACTIVATED, "Emergency stop activated", AlarmLevel.Critical, AlarmSource.System);
        }

        private void ExecuteResetAlarm()
        {
            foreach (var alarm in ActiveAlarms)
            {
                alarm.Clear();
            }
            ActiveAlarms.Clear();
            HasActiveAlarm = false;

            if (CurrentStatus == SystemStatus.EMO || CurrentStatus == SystemStatus.Error)
            {
                CurrentStatus = SystemStatus.Idle;
                IsHomed = false; // 홈 상태 리셋
            }

            StatusMessage = "알람 리셋 완료";
            RaiseCanExecuteChanged();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 주기적 상태 업데이트 (Timer에서 호출)
        /// </summary>
        public void UpdateStatus()
        {
            Motion?.UpdateStatus();
            IO?.UpdateStatus();
            Vision?.UpdateStatus();

            SystemState.LastUpdated = DateTime.Now;
        }

        /// <summary>
        /// 알람 추가
        /// </summary>
        public void AddAlarm(int code, string message, AlarmLevel level, AlarmSource source)
        {
            var alarm = AlarmInfo.Create(code, message, level, source);
            ActiveAlarms.Add(alarm);
            HasActiveAlarm = true;

            if (level == AlarmLevel.Critical || level == AlarmLevel.Error)
            {
                CurrentStatus = SystemStatus.Error;
            }
        }

        #endregion

        #region Private Methods

        private void RaiseCanExecuteChanged()
        {
            ((RelayCommand)SetAutoModeCommand).RaiseCanExecuteChanged();
            ((RelayCommand)InitializeSystemCommand).RaiseCanExecuteChanged();
            ((RelayCommand)HomeAllCommand).RaiseCanExecuteChanged();
            ((RelayCommand)ResetAlarmCommand).RaiseCanExecuteChanged();
        }

        #endregion

        #region Dispose

        protected override void OnDisposing()
        {
            // 이벤트 구독 해제
            if (_system != null)
            {
                _system.InitializationProgress -= OnInitializationProgress;
            }

            // Child ViewModel Dispose
            Motion?.Dispose();
            IO?.Dispose();
            Vision?.Dispose();

            // VisionAlignerSystem은 MainForm에서 관리 (여기서 Dispose하지 않음)
            base.OnDisposing();
        }

        #endregion
    }
}
