using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VisionAlignChamber.ViewModels.Base;
using VisionAlignChamber.Models;
using VisionAlignChamber.Core;
using VisionAlignChamber.Config;

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
        public EddyViewModel Eddy { get; private set; }

        #endregion

        #region Control Authority

        private ControlAuthority _controlAuthority = ControlAuthority.Local;
        public ControlAuthority ControlAuthority
        {
            get => _controlAuthority;
            set
            {
                if (SetProperty(ref _controlAuthority, value))
                {
                    OnPropertyChanged(nameof(IsLocalControl));
                    OnPropertyChanged(nameof(IsRemoteControl));
                    OnPropertyChanged(nameof(IsLocked));
                    OnPropertyChanged(nameof(CanOperateUI));

                    // Remote로 전환 시 Auto 모드 강제
                    if (value == ControlAuthority.Remote)
                    {
                        CurrentMode = SystemMode.Auto;
                    }

                    RaiseCanExecuteChanged();
                }
            }
        }

        /// <summary>
        /// 로컬(UI) 제어 상태
        /// </summary>
        public bool IsLocalControl => ControlAuthority == ControlAuthority.Local;

        /// <summary>
        /// 원격(CTC) 제어 상태
        /// </summary>
        public bool IsRemoteControl => ControlAuthority == ControlAuthority.Remote;

        /// <summary>
        /// 잠금 상태 (EMO/Interlock)
        /// </summary>
        public bool IsLocked => ControlAuthority == ControlAuthority.Locked;

        /// <summary>
        /// UI 조작 가능 여부 (Local 상태에서만)
        /// </summary>
        public bool CanOperateUI => IsLocalControl && !IsError;

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

            // Child ViewModel 생성 (사용 가능한 모듈만)
            if (_system.HasMotion)
            {
                Motion = new MotionViewModel(_system.Motion);
            }

            if (_system.HasIO)
            {
                IO = new IOViewModel(_system.IO);
            }

            if (_system.HasVision)
            {
                Vision = new VisionViewModel(_system.Vision);
            }

            if (_system.HasEddy)
            {
                Eddy = new EddyViewModel(_system.Eddy);
            }

            OnPropertyChanged(nameof(Motion));
            OnPropertyChanged(nameof(IO));
            OnPropertyChanged(nameof(Vision));
            OnPropertyChanged(nameof(Eddy));

            StatusMessage = GetInitStatusMessage();
        }

        private string GetInitStatusMessage()
        {
            var available = new System.Collections.Generic.List<string>();
            var unavailable = new System.Collections.Generic.List<string>();

            if (_system.HasMotion) available.Add("Motion"); else unavailable.Add("Motion");
            if (_system.HasIO) available.Add("IO"); else unavailable.Add("IO");
            if (_system.HasVision) available.Add("Vision"); else unavailable.Add("Vision");
            if (_system.HasEddy) available.Add("Eddy"); else unavailable.Add("Eddy");

            if (unavailable.Count == 0)
            {
                return "시스템 준비 완료";
            }
            else
            {
                return $"사용 가능: {string.Join(", ", available)} | 사용 불가: {string.Join(", ", unavailable)}";
            }
        }

        private void OnInitializationProgress(object sender, InitializationProgressEventArgs e)
        {
            StatusMessage = e.Message;
        }

        #endregion

        #region Commands

        // 제어권 전환 명령
        public ICommand SetLocalControlCommand { get; private set; }
        public ICommand SetRemoteControlCommand { get; private set; }

        // 모드 전환 명령
        public ICommand SetManualModeCommand { get; private set; }
        public ICommand SetAutoModeCommand { get; private set; }
        public ICommand SetSetupModeCommand { get; private set; }

        // 시스템 명령
        public ICommand InitializeSystemCommand { get; private set; }
        public ICommand HomeAllCommand { get; private set; }
        public ICommand EmergencyStopCommand { get; private set; }
        public ICommand ResetAlarmCommand { get; private set; }

        private void InitializeCommands()
        {
            // 제어권 전환 (Local 상태에서만 Remote로 전환 가능)
            SetLocalControlCommand = new RelayCommand(
                () => ControlAuthority = ControlAuthority.Local,
                () => !IsLocked);
            SetRemoteControlCommand = new RelayCommand(
                () => ControlAuthority = ControlAuthority.Remote,
                () => IsLocalControl && IsInitialized && IsHomed && !HasActiveAlarm);

            // 모드 전환 (Local 상태에서만)
            SetManualModeCommand = new RelayCommand(
                () => CurrentMode = SystemMode.Manual,
                () => IsLocalControl);
            // Auto는 Initialize + Home 완료 후에만 가능 (프로덕션)
            // Simulation 모드: Local이면 Auto 가능 (Initialize/Home 불필요)
            SetAutoModeCommand = new RelayCommand(
                () => CurrentMode = SystemMode.Auto,
                () => AppSettings.SimulationMode
                    ? IsLocalControl
                    : IsLocalControl && IsInitialized && IsHomed);
            SetSetupModeCommand = new RelayCommand(
                () => CurrentMode = SystemMode.Setup,
                () => IsLocalControl);

            // 시스템 명령 (Local 상태에서만)
            InitializeSystemCommand = new RelayCommand(
                ExecuteInitializeSystem,
                () => IsLocalControl && !IsInitialized);
            HomeAllCommand = new RelayCommand(
                ExecuteHomeAll,
                () => IsLocalControl && IsInitialized && !IsHomed);
            EmergencyStopCommand = new RelayCommand(ExecuteEmergencyStop); // EMO는 항상 가능
            ResetAlarmCommand = new RelayCommand(
                ExecuteResetAlarm,
                () => IsLocalControl && HasActiveAlarm);
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

            // 제어권 잠금
            ControlAuthority = ControlAuthority.Locked;

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

                // Locked 상태였다면 Local로 복구
                if (IsLocked)
                {
                    ControlAuthority = ControlAuthority.Local;
                }
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
            Eddy?.UpdateStatus();

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
            // 제어권 전환 명령
            ((RelayCommand)SetLocalControlCommand).RaiseCanExecuteChanged();
            ((RelayCommand)SetRemoteControlCommand).RaiseCanExecuteChanged();

            // 모드 전환 명령
            ((RelayCommand)SetManualModeCommand).RaiseCanExecuteChanged();
            ((RelayCommand)SetAutoModeCommand).RaiseCanExecuteChanged();
            ((RelayCommand)SetSetupModeCommand).RaiseCanExecuteChanged();

            // 시스템 명령
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
            Eddy?.Dispose();

            // VisionAlignerSystem은 MainForm에서 관리 (여기서 Dispose하지 않음)
            base.OnDisposing();
        }

        #endregion
    }
}
