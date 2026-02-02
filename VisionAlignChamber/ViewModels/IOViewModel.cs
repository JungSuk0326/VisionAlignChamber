using System;
using System.Windows.Input;
using VisionAlignChamber.ViewModels.Base;
using VisionAlignChamber.Hardware.IO;

namespace VisionAlignChamber.ViewModels
{
    /// <summary>
    /// I/O 제어 ViewModel
    /// </summary>
    public class IOViewModel : ViewModelBase
    {
        #region Fields

        private readonly VisionAlignerIO _io;

        #endregion

        #region Constructor

        public IOViewModel(VisionAlignerIO io)
        {
            _io = io ?? throw new ArgumentNullException(nameof(io));
            InitializeCommands();
        }

        #endregion

        #region Initialization Properties

        /// <summary>
        /// 디지털 IO 초기화 상태
        /// </summary>
        public bool IsInitialized => _io.IsInitialized;

        #endregion

        #region Sensor Input Properties (Read-Only)

        private bool _sensor1WaferDetected;
        public bool Sensor1WaferDetected
        {
            get => _sensor1WaferDetected;
            set => SetProperty(ref _sensor1WaferDetected, value);
        }

        private bool _sensor2WaferDetected;
        public bool Sensor2WaferDetected
        {
            get => _sensor2WaferDetected;
            set => SetProperty(ref _sensor2WaferDetected, value);
        }

        private bool _pnCheckP;
        public bool PNCheckP
        {
            get => _pnCheckP;
            set => SetProperty(ref _pnCheckP, value);
        }

        private bool _pnCheckN;
        public bool PNCheckN
        {
            get => _pnCheckN;
            set => SetProperty(ref _pnCheckN, value);
        }

        /// <summary>
        /// 웨이퍼가 모든 센서에서 감지됨
        /// </summary>
        public bool IsWaferDetected => Sensor1WaferDetected && Sensor2WaferDetected;

        #endregion

        #region Lift Pin Output Properties

        private bool _liftPinVacuum;
        public bool LiftPinVacuum
        {
            get => _liftPinVacuum;
            set
            {
                if (SetProperty(ref _liftPinVacuum, value))
                {
                    _io.SetLiftPinVacuum(value);
                }
            }
        }

        private bool _liftPinBlow;
        public bool LiftPinBlow
        {
            get => _liftPinBlow;
            set
            {
                if (SetProperty(ref _liftPinBlow, value))
                {
                    _io.SetLiftPinBlow(value);
                }
            }
        }

        #endregion

        #region Chuck Output Properties

        private bool _chuckVacuum;
        public bool ChuckVacuum
        {
            get => _chuckVacuum;
            set
            {
                if (SetProperty(ref _chuckVacuum, value))
                {
                    _io.SetChuckVacuum(value);
                }
            }
        }

        private bool _chuckBlow;
        public bool ChuckBlow
        {
            get => _chuckBlow;
            set
            {
                if (SetProperty(ref _chuckBlow, value))
                {
                    _io.SetChuckBlow(value);
                }
            }
        }

        #endregion

        #region Vision Light Properties

        private bool _visionLightOn;
        public bool VisionLightOn
        {
            get => _visionLightOn;
            set
            {
                if (SetProperty(ref _visionLightOn, value))
                {
                    _io.SetVisionLight(value);
                }
            }
        }

        #endregion

        #region Status Properties

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        #endregion

        #region Commands

        public ICommand InitializeCommand { get; private set; }

        public ICommand LiftPinVacuumOnCommand { get; private set; }
        public ICommand LiftPinVacuumOffCommand { get; private set; }
        public ICommand LiftPinBlowOnCommand { get; private set; }
        public ICommand LiftPinBlowOffCommand { get; private set; }

        public ICommand ChuckVacuumOnCommand { get; private set; }
        public ICommand ChuckVacuumOffCommand { get; private set; }
        public ICommand ChuckBlowOnCommand { get; private set; }
        public ICommand ChuckBlowOffCommand { get; private set; }

        public ICommand VisionLightOnCommand { get; private set; }
        public ICommand VisionLightOffCommand { get; private set; }

        public ICommand AllOutputOffCommand { get; private set; }

        private void InitializeCommands()
        {
            // Initialize Command
            InitializeCommand = new RelayCommand(ExecuteInitialize, () => !IsInitialized);

            // Lift Pin Commands
            LiftPinVacuumOnCommand = new RelayCommand(() => LiftPinVacuum = true, () => IsInitialized);
            LiftPinVacuumOffCommand = new RelayCommand(() => LiftPinVacuum = false, () => IsInitialized);
            LiftPinBlowOnCommand = new RelayCommand(() => LiftPinBlow = true, () => IsInitialized);
            LiftPinBlowOffCommand = new RelayCommand(() => LiftPinBlow = false, () => IsInitialized);

            // Chuck Commands
            ChuckVacuumOnCommand = new RelayCommand(() => ChuckVacuum = true, () => IsInitialized);
            ChuckVacuumOffCommand = new RelayCommand(() => ChuckVacuum = false, () => IsInitialized);
            ChuckBlowOnCommand = new RelayCommand(() => ChuckBlow = true, () => IsInitialized);
            ChuckBlowOffCommand = new RelayCommand(() => ChuckBlow = false, () => IsInitialized);

            // Vision Light Commands
            VisionLightOnCommand = new RelayCommand(() => VisionLightOn = true, () => IsInitialized);
            VisionLightOffCommand = new RelayCommand(() => VisionLightOn = false, () => IsInitialized);

            // All Off Command
            AllOutputOffCommand = new RelayCommand(ExecuteAllOutputOff, () => IsInitialized);
        }

        #endregion

        #region Command Implementations

        private void ExecuteInitialize()
        {
            StatusMessage = "디지털 IO 초기화 중...";
            if (_io.Initialize())
            {
                StatusMessage = $"디지털 IO 초기화 완료 (모듈 수: {_io.ModuleCount})";
                OnPropertyChanged(nameof(IsInitialized));
                RaiseAllCommandsCanExecuteChanged();
            }
            else
            {
                StatusMessage = "디지털 IO 초기화 실패";
            }
        }

        private void ExecuteAllOutputOff()
        {
            LiftPinVacuum = false;
            LiftPinBlow = false;
            ChuckVacuum = false;
            ChuckBlow = false;
            VisionLightOn = false;
            StatusMessage = "모든 출력 OFF";
        }

        private void RaiseAllCommandsCanExecuteChanged()
        {
            ((RelayCommand)InitializeCommand).RaiseCanExecuteChanged();
            ((RelayCommand)LiftPinVacuumOnCommand).RaiseCanExecuteChanged();
            ((RelayCommand)LiftPinVacuumOffCommand).RaiseCanExecuteChanged();
            ((RelayCommand)LiftPinBlowOnCommand).RaiseCanExecuteChanged();
            ((RelayCommand)LiftPinBlowOffCommand).RaiseCanExecuteChanged();
            ((RelayCommand)ChuckVacuumOnCommand).RaiseCanExecuteChanged();
            ((RelayCommand)ChuckVacuumOffCommand).RaiseCanExecuteChanged();
            ((RelayCommand)ChuckBlowOnCommand).RaiseCanExecuteChanged();
            ((RelayCommand)ChuckBlowOffCommand).RaiseCanExecuteChanged();
            ((RelayCommand)VisionLightOnCommand).RaiseCanExecuteChanged();
            ((RelayCommand)VisionLightOffCommand).RaiseCanExecuteChanged();
            ((RelayCommand)AllOutputOffCommand).RaiseCanExecuteChanged();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 상태 업데이트 (Timer에서 호출)
        /// </summary>
        public void UpdateStatus()
        {
            var status = _io.GetStatusSummary();

            // Input 상태 업데이트
            Sensor1WaferDetected = status.Sensor1WaferDetected;
            Sensor2WaferDetected = status.Sensor2WaferDetected;
            PNCheckP = status.PNCheckP;
            PNCheckN = status.PNCheckN;

            // Output 상태 업데이트 (실제 하드웨어 상태 반영)
            _liftPinVacuum = status.LiftPinVacuum;
            OnPropertyChanged(nameof(LiftPinVacuum));

            _liftPinBlow = status.LiftPinBlow;
            OnPropertyChanged(nameof(LiftPinBlow));

            _chuckVacuum = status.ChuckVacuum;
            OnPropertyChanged(nameof(ChuckVacuum));

            _chuckBlow = status.ChuckBlow;
            OnPropertyChanged(nameof(ChuckBlow));

            _visionLightOn = status.VisionLightOn;
            OnPropertyChanged(nameof(VisionLightOn));

            // IsWaferDetected 업데이트 알림
            OnPropertyChanged(nameof(IsWaferDetected));
        }

        /// <summary>
        /// 웨이퍼 로드 시퀀스용 - Lift Pin Vacuum ON
        /// </summary>
        public void ActivateLiftPinForLoad()
        {
            LiftPinBlow = false;
            LiftPinVacuum = true;
            StatusMessage = "Lift Pin Vacuum ON";
        }

        /// <summary>
        /// 웨이퍼 언로드 시퀀스용 - Lift Pin Blow
        /// </summary>
        public void ActivateLiftPinForUnload()
        {
            LiftPinVacuum = false;
            LiftPinBlow = true;
            StatusMessage = "Lift Pin Blow ON";
        }

        /// <summary>
        /// 척 흡착
        /// </summary>
        public void ActivateChuckVacuum()
        {
            ChuckBlow = false;
            ChuckVacuum = true;
            StatusMessage = "Chuck Vacuum ON";
        }

        /// <summary>
        /// 척 해제
        /// </summary>
        public void ReleaseChuckVacuum()
        {
            ChuckVacuum = false;
            ChuckBlow = true;
            StatusMessage = "Chuck Blow ON";
        }

        #endregion
    }
}
