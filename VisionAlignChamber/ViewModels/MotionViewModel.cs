using System;
using System.Collections.Generic;
using System.Windows.Input;
using VisionAlignChamber.ViewModels.Base;
using VisionAlignChamber.Hardware.Facade;

namespace VisionAlignChamber.ViewModels
{
    /// <summary>
    /// Motion 제어 ViewModel
    /// 개별 축 ViewModel을 관리
    /// </summary>
    public class MotionViewModel : ViewModelBase
    {
        #region Fields

        private readonly VisionAlignerMotion _motion;
        private readonly List<AxisViewModel> _allAxes;

        #endregion

        #region Constructor

        public MotionViewModel(VisionAlignerMotion motion)
        {
            _motion = motion ?? throw new ArgumentNullException(nameof(motion));

            // 개별 축 ViewModel 생성
            WedgeAxis = new AxisViewModel(motion, VAMotionAxis.WedgeUpDown, "Wedge Up/Down", "pulse");
            ChuckAxis = new AxisViewModel(motion, VAMotionAxis.ChuckRotation, "Chuck Rotation", "degree");
            CenteringStage1Axis = new AxisViewModel(motion, VAMotionAxis.CenteringStage_1, "Centering Stage 1", "pulse");
            CenteringStage2Axis = new AxisViewModel(motion, VAMotionAxis.CenteringStage_2, "Centering Stage 2", "pulse");

            // INI 설정에서 축 활성화 상태 반영
            WedgeAxis.IsEnabled = motion.IsAxisEnabled(VAMotionAxis.WedgeUpDown);
            ChuckAxis.IsEnabled = motion.IsAxisEnabled(VAMotionAxis.ChuckRotation);
            CenteringStage1Axis.IsEnabled = motion.IsAxisEnabled(VAMotionAxis.CenteringStage_1);
            CenteringStage2Axis.IsEnabled = motion.IsAxisEnabled(VAMotionAxis.CenteringStage_2);

            // 전체 축 리스트 (반복 처리용)
            _allAxes = new List<AxisViewModel>
            {
                WedgeAxis,
                ChuckAxis,
                CenteringStage1Axis,
                CenteringStage2Axis
            };

            InitializeCommands();
        }

        #endregion

        #region Axis ViewModels

        /// <summary>
        /// Wedge Up/Down 축
        /// </summary>
        public AxisViewModel WedgeAxis { get; }

        /// <summary>
        /// Chuck Rotation 축
        /// </summary>
        public AxisViewModel ChuckAxis { get; }

        /// <summary>
        /// Centering Stage 1 축
        /// </summary>
        public AxisViewModel CenteringStage1Axis { get; }

        /// <summary>
        /// Centering Stage 2 축
        /// </summary>
        public AxisViewModel CenteringStage2Axis { get; }

        /// <summary>
        /// 모든 축 목록
        /// </summary>
        public IReadOnlyList<AxisViewModel> AllAxes => _allAxes;

        #endregion

        #region Common Properties

        /// <summary>
        /// 모션 컨트롤러 초기화 상태
        /// </summary>
        public bool IsInitialized => _motion.IsInitialized;

        private bool _isAnyAxisMoving;
        /// <summary>
        /// 어떤 축이든 이동 중인지 여부
        /// </summary>
        public bool IsAnyAxisMoving
        {
            get => _isAnyAxisMoving;
            set => SetProperty(ref _isAnyAxisMoving, value);
        }

        private bool _isAllHomed;
        /// <summary>
        /// 모든 축 원점 복귀 완료 여부
        /// </summary>
        public bool IsAllHomed
        {
            get => _isAllHomed;
            set => SetProperty(ref _isAllHomed, value);
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
        /// 모션 컨트롤러 초기화 명령
        /// </summary>
        public ICommand InitializeCommand { get; private set; }

        /// <summary>
        /// 전체 축 원점 복귀 명령
        /// </summary>
        public ICommand HomeAllCommand { get; private set; }

        /// <summary>
        /// 전체 축 정지 명령
        /// </summary>
        public ICommand StopAllCommand { get; private set; }

        /// <summary>
        /// 전체 축 비상 정지 명령
        /// </summary>
        public ICommand EmergencyStopAllCommand { get; private set; }

        /// <summary>
        /// 전체 축 알람 클리어 명령
        /// </summary>
        public ICommand ClearAllAlarmsCommand { get; private set; }

        private void InitializeCommands()
        {
            InitializeCommand = new RelayCommand(ExecuteInitialize, () => !IsInitialized);
            HomeAllCommand = new RelayCommand(ExecuteHomeAll, () => IsInitialized && !IsAnyAxisMoving);
            StopAllCommand = new RelayCommand(ExecuteStopAll);
            EmergencyStopAllCommand = new RelayCommand(ExecuteEmergencyStopAll);
            ClearAllAlarmsCommand = new RelayCommand(ExecuteClearAllAlarms, () => IsInitialized);
        }

        #endregion

        #region Command Implementations

        private void ExecuteInitialize()
        {
            StatusMessage = "모션 컨트롤러 초기화 중...";
            if (_motion.Initialize())
            {
                StatusMessage = $"모션 컨트롤러 초기화 완료 (축 수: {_motion.AxisCount})";
                OnPropertyChanged(nameof(IsInitialized));
                ((RelayCommand)InitializeCommand).RaiseCanExecuteChanged();
                ((RelayCommand)HomeAllCommand).RaiseCanExecuteChanged();
            }
            else
            {
                StatusMessage = "모션 컨트롤러 초기화 실패";
            }
        }

        private void ExecuteHomeAll()
        {
            StatusMessage = "전체 축 Home 동작 중...";
            _motion.HomeAll();
        }

        private void ExecuteStopAll()
        {
            _motion.StopAll();
            StatusMessage = "전체 축 정지";
        }

        private void ExecuteEmergencyStopAll()
        {
            _motion.EmergencyStopAll();
            StatusMessage = "비상 정지!";
        }

        private void ExecuteClearAllAlarms()
        {
            StatusMessage = "전체 축 알람 클리어 중...";
            if (_motion.ClearAlarmAll())
            {
                StatusMessage = "전체 축 알람 클리어 완료";
            }
            else
            {
                StatusMessage = "알람 클리어 실패";
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 상태 업데이트 (Timer에서 호출)
        /// </summary>
        public void UpdateStatus()
        {
            // 개별 축 상태 업데이트
            foreach (var axis in _allAxes)
            {
                axis.UpdateStatus();
            }

            // 전체 상태 계산
            IsAnyAxisMoving = _motion.IsAnyAxisMoving();
            IsAllHomed = WedgeAxis.IsHomed && ChuckAxis.IsHomed &&
                         CenteringStage1Axis.IsHomed && CenteringStage2Axis.IsHomed;

            // Command 실행 가능 상태 갱신
            ((RelayCommand)HomeAllCommand).RaiseCanExecuteChanged();
        }

        /// <summary>
        /// 축 이름으로 AxisViewModel 가져오기
        /// </summary>
        public AxisViewModel GetAxis(VAMotionAxis axis)
        {
            switch (axis)
            {
                case VAMotionAxis.WedgeUpDown: return WedgeAxis;
                case VAMotionAxis.ChuckRotation: return ChuckAxis;
                case VAMotionAxis.CenteringStage_1: return CenteringStage1Axis;
                case VAMotionAxis.CenteringStage_2: return CenteringStage2Axis;
                default: return null;
            }
        }

        /// <summary>
        /// Centering Stage 동시 이동
        /// </summary>
        public bool MoveCenteringStagesSync(double position1, double position2, double? velocity = null)
        {
            if (CenteringStage1Axis.IsMoving || CenteringStage2Axis.IsMoving)
                return false;

            return _motion.CenteringStagesMoveSync(position1, position2, velocity);
        }

        /// <summary>
        /// Centering Stage 동시 원점 복귀
        /// </summary>
        public bool HomeCenteringStagesSync()
        {
            if (CenteringStage1Axis.IsMoving || CenteringStage2Axis.IsMoving)
                return false;

            return _motion.CenteringStagesHomeSync();
        }

        #endregion

        #region Dispose

        protected override void OnDisposing()
        {
            foreach (var axis in _allAxes)
            {
                axis.Dispose();
            }
            base.OnDisposing();
        }

        #endregion
    }
}
