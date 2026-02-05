using System;
using System.Windows.Input;
using VisionAlignChamber.ViewModels.Base;
using VisionAlignChamber.Interfaces;

namespace VisionAlignChamber.ViewModels
{
    /// <summary>
    /// Eddy Current 센서 제어 ViewModel
    /// </summary>
    public class EddyViewModel : ViewModelBase
    {
        #region Fields

        private readonly IEddyCurrentSensor _sensor;

        #endregion

        #region Constructor

        public EddyViewModel(IEddyCurrentSensor sensor)
        {
            _sensor = sensor ?? throw new ArgumentNullException(nameof(sensor));

            // 기본값 설정
            IpAddress = "192.168.1.99";
            Port = 502;

            InitializeCommands();
        }

        #endregion

        #region Connection Properties

        private string _ipAddress;
        public string IpAddress
        {
            get => _ipAddress;
            set => SetProperty(ref _ipAddress, value);
        }

        private int _port;
        public int Port
        {
            get => _port;
            set => SetProperty(ref _port, value);
        }

        /// <summary>
        /// 연결 상태
        /// </summary>
        public bool IsConnected => _sensor?.IsConnected ?? false;

        #endregion

        #region Measurement Properties

        private double _currentValue;
        /// <summary>
        /// 현재 측정값
        /// </summary>
        public double CurrentValue
        {
            get => _currentValue;
            set => SetProperty(ref _currentValue, value);
        }

        private bool _isZeroSet;
        /// <summary>
        /// 영점 설정 여부
        /// </summary>
        public bool IsZeroSet
        {
            get => _isZeroSet;
            set => SetProperty(ref _isZeroSet, value);
        }

        #endregion

        #region Status Properties

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        #endregion

        #region Commands

        public ICommand ConnectCommand { get; private set; }
        public ICommand DisconnectCommand { get; private set; }
        public ICommand SetZeroCommand { get; private set; }
        public ICommand GetDataCommand { get; private set; }

        private void InitializeCommands()
        {
            ConnectCommand = new RelayCommand(ExecuteConnect, () => !IsConnected && !IsBusy);
            DisconnectCommand = new RelayCommand(ExecuteDisconnect, () => IsConnected && !IsBusy);
            SetZeroCommand = new RelayCommand(ExecuteSetZero, () => IsConnected && !IsBusy);
            GetDataCommand = new RelayCommand(ExecuteGetData, () => IsConnected && !IsBusy);
        }

        #endregion

        #region Command Implementations

        private void ExecuteConnect()
        {
            try
            {
                IsBusy = true;
                StatusMessage = "연결 중...";

                // IP 유효성 검사
                if (!System.Net.IPAddress.TryParse(IpAddress, out _))
                {
                    StatusMessage = "유효하지 않은 IP 주소입니다.";
                    return;
                }

                // 포트 유효성 검사
                if (Port < 0 || Port > 65535)
                {
                    StatusMessage = "유효하지 않은 포트입니다.";
                    return;
                }

                if (_sensor.Connect(IpAddress, Port))
                {
                    StatusMessage = $"연결됨: {IpAddress}:{Port}";
                    IsZeroSet = false;
                    RaiseCanExecuteChanged();
                }
                else
                {
                    StatusMessage = "연결 실패";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"연결 오류: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged(nameof(IsConnected));
            }
        }

        private void ExecuteDisconnect()
        {
            try
            {
                IsBusy = true;
                StatusMessage = "연결 해제 중...";

                _sensor.Disconnect();

                StatusMessage = "연결 해제됨";
                IsZeroSet = false;
                CurrentValue = 0;
                RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                StatusMessage = $"연결 해제 오류: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
                OnPropertyChanged(nameof(IsConnected));
            }
        }

        private void ExecuteSetZero()
        {
            try
            {
                IsBusy = true;
                StatusMessage = "영점 설정 중...";

                if (_sensor.SetZero())
                {
                    IsZeroSet = true;
                    StatusMessage = "영점 설정 완료";
                    RaiseCanExecuteChanged();
                }
                else
                {
                    StatusMessage = "영점 설정 실패";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"영점 설정 오류: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void ExecuteGetData()
        {
            try
            {
                IsBusy = true;

                double value = _sensor.GetData();
                CurrentValue = value;
                StatusMessage = $"측정값: {value:F4}";
            }
            catch (Exception ex)
            {
                StatusMessage = $"데이터 읽기 오류: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 상태 업데이트 (Timer에서 호출)
        /// </summary>
        public void UpdateStatus()
        {
            OnPropertyChanged(nameof(IsConnected));
        }

        /// <summary>
        /// 데이터 읽기 (외부에서 호출 가능)
        /// </summary>
        public double ReadData()
        {
            if (!IsConnected) return 0;

            try
            {
                CurrentValue = _sensor.GetData();
                return CurrentValue;
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region Private Methods

        private void RaiseCanExecuteChanged()
        {
            ((RelayCommand)ConnectCommand).RaiseCanExecuteChanged();
            ((RelayCommand)DisconnectCommand).RaiseCanExecuteChanged();
            ((RelayCommand)SetZeroCommand).RaiseCanExecuteChanged();
            ((RelayCommand)GetDataCommand).RaiseCanExecuteChanged();
        }

        #endregion

        #region Dispose

        protected override void OnDisposing()
        {
            if (IsConnected)
            {
                _sensor?.Disconnect();
            }
            _sensor?.Dispose();
            base.OnDisposing();
        }

        #endregion
    }
}
