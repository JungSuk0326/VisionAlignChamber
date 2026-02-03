using System;
using System.Windows.Input;
using VisionAlignChamber.ViewModels.Base;
using VisionAlignChamber.Hardware.Facade;
using VisionAlignChamber.Models;

namespace VisionAlignChamber.ViewModels
{
    /// <summary>
    /// 단일 축 ViewModel (재사용 가능)
    /// </summary>
    public class AxisViewModel : ViewModelBase
    {
        #region Fields

        private readonly VisionAlignerMotion _motion;
        private readonly VAMotionAxis _axis;

        #endregion

        #region Constructor

        public AxisViewModel(VisionAlignerMotion motion, VAMotionAxis axis, string name, string unit = "pulse")
        {
            _motion = motion ?? throw new ArgumentNullException(nameof(motion));
            _axis = axis;
            Name = name;
            Unit = unit;

            InitializeCommands();
        }

        #endregion

        #region Axis Info Properties

        /// <summary>
        /// 축 종류
        /// </summary>
        public VAMotionAxis Axis => _axis;

        /// <summary>
        /// 축 이름
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 단위 (pulse, degree, mm 등)
        /// </summary>
        public string Unit { get; }

        #endregion

        #region Position Properties

        private double _position;
        /// <summary>
        /// 현재 위치
        /// </summary>
        public double Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        private double _targetPosition;
        /// <summary>
        /// 목표 위치
        /// </summary>
        public double TargetPosition
        {
            get => _targetPosition;
            set => SetProperty(ref _targetPosition, value);
        }

        private double _velocity;
        /// <summary>
        /// 이동 속도
        /// </summary>
        public double Velocity
        {
            get => _velocity;
            set => SetProperty(ref _velocity, value);
        }

        #endregion

        #region Status Properties

        private bool _isMoving;
        /// <summary>
        /// 이동 중 여부
        /// </summary>
        public bool IsMoving
        {
            get => _isMoving;
            set => SetProperty(ref _isMoving, value);
        }

        private bool _isHomed;
        /// <summary>
        /// 원점 복귀 완료 여부
        /// </summary>
        public bool IsHomed
        {
            get => _isHomed;
            set => SetProperty(ref _isHomed, value);
        }

        private bool _isEnabled = true;
        /// <summary>
        /// 축 활성화 여부
        /// </summary>
        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private bool _hasError;
        /// <summary>
        /// 에러 발생 여부
        /// </summary>
        public bool HasError
        {
            get => _hasError;
            set => SetProperty(ref _hasError, value);
        }

        private bool _isServoOn;
        /// <summary>
        /// 서보 ON 상태
        /// </summary>
        public bool IsServoOn
        {
            get => _isServoOn;
            set => SetProperty(ref _isServoOn, value);
        }

        private bool _isAlarm;
        /// <summary>
        /// 알람 발생 여부
        /// </summary>
        public bool IsAlarm
        {
            get => _isAlarm;
            set => SetProperty(ref _isAlarm, value);
        }

        private string _statusMessage;
        /// <summary>
        /// 상태 메시지
        /// </summary>
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        #endregion

        #region Commands

        /// <summary>
        /// 원점 복귀 명령
        /// </summary>
        public ICommand HomeCommand { get; private set; }

        /// <summary>
        /// 절대 위치 이동 명령
        /// </summary>
        public ICommand MoveAbsoluteCommand { get; private set; }

        /// <summary>
        /// 상대 위치 이동 명령
        /// </summary>
        public ICommand MoveRelativeCommand { get; private set; }

        /// <summary>
        /// Jog+ 명령
        /// </summary>
        public ICommand JogPlusCommand { get; private set; }

        /// <summary>
        /// Jog- 명령
        /// </summary>
        public ICommand JogMinusCommand { get; private set; }

        /// <summary>
        /// 정지 명령
        /// </summary>
        public ICommand StopCommand { get; private set; }

        /// <summary>
        /// 비상 정지 명령
        /// </summary>
        public ICommand EmergencyStopCommand { get; private set; }

        /// <summary>
        /// 서보 ON 명령
        /// </summary>
        public ICommand ServoOnCommand { get; private set; }

        /// <summary>
        /// 서보 OFF 명령
        /// </summary>
        public ICommand ServoOffCommand { get; private set; }

        /// <summary>
        /// 서보 토글 명령
        /// </summary>
        public ICommand ServoToggleCommand { get; private set; }

        /// <summary>
        /// 알람 클리어 명령
        /// </summary>
        public ICommand ClearAlarmCommand { get; private set; }

        private void InitializeCommands()
        {
            HomeCommand = new RelayCommand(ExecuteHome, CanExecuteMotion);
            MoveAbsoluteCommand = new RelayCommand(ExecuteMoveAbsolute, CanExecuteMotion);
            MoveRelativeCommand = new RelayCommand<double>(ExecuteMoveRelative, _ => CanExecuteMotion());
            JogPlusCommand = new RelayCommand<double>(ExecuteJogPlus, _ => CanExecuteMotion());
            JogMinusCommand = new RelayCommand<double>(ExecuteJogMinus, _ => CanExecuteMotion());
            StopCommand = new RelayCommand(ExecuteStop);
            EmergencyStopCommand = new RelayCommand(ExecuteEmergencyStop);
            ServoOnCommand = new RelayCommand(ExecuteServoOn, () => !IsServoOn);
            ServoOffCommand = new RelayCommand(ExecuteServoOff, () => IsServoOn);
            ServoToggleCommand = new RelayCommand(ExecuteServoToggle);
            ClearAlarmCommand = new RelayCommand(ExecuteClearAlarm, () => IsAlarm);
        }

        #endregion

        #region Command Implementations

        private bool CanExecuteMotion()
        {
            return IsEnabled && !IsMoving && !HasError && IsServoOn;
        }

        private void ExecuteHome()
        {
            try
            {
                _motion.MoveHome(_axis);
                StatusMessage = "Home 동작 중...";
            }
            catch (Exception ex)
            {
                HasError = true;
                StatusMessage = $"Home 오류: {ex.Message}";
            }
        }

        private void ExecuteMoveAbsolute()
        {
            try
            {
                double? vel = Velocity > 0 ? Velocity : (double?)null;
                _motion.MoveAbsolute(_axis, TargetPosition, vel);
                StatusMessage = $"이동 중: {TargetPosition} {Unit}";
            }
            catch (Exception ex)
            {
                HasError = true;
                StatusMessage = $"이동 오류: {ex.Message}";
            }
        }

        private void ExecuteMoveRelative(double distance)
        {
            try
            {
                double? vel = Velocity > 0 ? Velocity : (double?)null;
                _motion.MoveRelative(_axis, distance, vel);
                StatusMessage = $"상대 이동: {distance:+0.###;-0.###} {Unit}";
            }
            catch (Exception ex)
            {
                HasError = true;
                StatusMessage = $"이동 오류: {ex.Message}";
            }
        }

        private void ExecuteJogPlus(double jogDistance)
        {
            ExecuteMoveRelative(Math.Abs(jogDistance));
        }

        private void ExecuteJogMinus(double jogDistance)
        {
            ExecuteMoveRelative(-Math.Abs(jogDistance));
        }

        private void ExecuteStop()
        {
            try
            {
                _motion.Stop(_axis);
                StatusMessage = "정지";
            }
            catch (Exception ex)
            {
                StatusMessage = $"정지 오류: {ex.Message}";
            }
        }

        private void ExecuteEmergencyStop()
        {
            try
            {
                _motion.EmergencyStop(_axis);
                StatusMessage = "비상 정지!";
            }
            catch (Exception ex)
            {
                StatusMessage = $"비상 정지 오류: {ex.Message}";
            }
        }

        private void ExecuteServoOn()
        {
            try
            {
                if (_motion.ServoOn(_axis))
                {
                    IsServoOn = true;
                    StatusMessage = "Servo ON";
                }
                else
                {
                    StatusMessage = "Servo ON 실패";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Servo ON 오류: {ex.Message}";
            }
        }

        private void ExecuteServoOff()
        {
            try
            {
                if (_motion.ServoOff(_axis))
                {
                    IsServoOn = false;
                    StatusMessage = "Servo OFF";
                }
                else
                {
                    StatusMessage = "Servo OFF 실패";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Servo OFF 오류: {ex.Message}";
            }
        }

        private void ExecuteServoToggle()
        {
            if (IsServoOn)
                ExecuteServoOff();
            else
                ExecuteServoOn();
        }

        private void ExecuteClearAlarm()
        {
            try
            {
                if (_motion.ClearAlarm(_axis))
                {
                    IsAlarm = false;
                    HasError = false;
                    StatusMessage = "Alarm Cleared";
                }
                else
                {
                    StatusMessage = "Alarm Clear 실패";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Alarm Clear 오류: {ex.Message}";
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 상태 업데이트 (Timer에서 호출)
        /// </summary>
        public void UpdateStatus()
        {
            Position = _motion.GetPosition(_axis);
            IsMoving = _motion.IsMoving(_axis);
            IsServoOn = _motion.IsServoOn(_axis);
            IsAlarm = _motion.IsAlarm(_axis);

            // 알람 발생 시 HasError 설정
            if (IsAlarm)
            {
                HasError = true;
            }

            // Command 실행 가능 상태 갱신
            RaiseCanExecuteChanged();
        }

        /// <summary>
        /// 지정 위치로 이동
        /// </summary>
        public bool MoveTo(double position, double? velocity = null)
        {
            if (!CanExecuteMotion())
                return false;

            TargetPosition = position;
            if (velocity.HasValue)
                Velocity = velocity.Value;

            ExecuteMoveAbsolute();
            return true;
        }

        /// <summary>
        /// 이동 완료 대기
        /// </summary>
        public bool WaitForDone(int timeoutMs = 30000)
        {
            return _motion.WaitForDone(_axis, timeoutMs);
        }

        #endregion

        #region Private Methods

        private void RaiseCanExecuteChanged()
        {
            ((RelayCommand)HomeCommand).RaiseCanExecuteChanged();
            ((RelayCommand)MoveAbsoluteCommand).RaiseCanExecuteChanged();
            ((RelayCommand)StopCommand).RaiseCanExecuteChanged();
            ((RelayCommand)ServoOnCommand).RaiseCanExecuteChanged();
            ((RelayCommand)ServoOffCommand).RaiseCanExecuteChanged();
            ((RelayCommand)ClearAlarmCommand).RaiseCanExecuteChanged();
        }

        #endregion
    }
}
