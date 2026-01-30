using System;
using System.Windows.Forms;
using VisionAlignChamber.ViewModels;

namespace VisionAlignChamber.Views.Controls
{
    /// <summary>
    /// Motion 제어 패널
    /// 모든 축의 AxisControlPanel을 포함
    /// </summary>
    public partial class MotionPanel : UserControl
    {
        #region Fields

        private MotionViewModel _viewModel;
        private Timer _updateTimer;

        #endregion

        #region Constructor

        public MotionPanel()
        {
            InitializeComponent();
            InitializeTimer();
        }

        #endregion

        #region Initialization

        private void InitializeTimer()
        {
            _updateTimer = new Timer();
            _updateTimer.Interval = 100;
            _updateTimer.Tick += UpdateTimer_Tick;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// ViewModel 바인딩
        /// </summary>
        public void BindViewModel(MotionViewModel viewModel)
        {
            _viewModel = viewModel;

            if (_viewModel == null) return;

            // 개별 축 바인딩
            axisWedge.BindViewModel(_viewModel.WedgeAxis);
            axisChuck.BindViewModel(_viewModel.ChuckAxis);
            axisCentering1.BindViewModel(_viewModel.CenteringStage1Axis);
            axisCentering2.BindViewModel(_viewModel.CenteringStage2Axis);

            _updateTimer.Start();
        }

        #endregion

        #region Timer

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (_viewModel == null) return;

            // 각 축 UI 업데이트
            axisWedge.UpdateDisplay();
            axisChuck.UpdateDisplay();
            axisCentering1.UpdateDisplay();
            axisCentering2.UpdateDisplay();

            // 전체 상태 업데이트
            UpdateGlobalStatus();
        }

        private void UpdateGlobalStatus()
        {
            lblAllHomedStatus.Text = _viewModel.IsAllHomed ? "All Homed" : "Not Homed";
            lblAllHomedStatus.ForeColor = _viewModel.IsAllHomed ?
                System.Drawing.Color.LimeGreen : System.Drawing.Color.Orange;

            btnHomeAll.Enabled = !_viewModel.IsAnyAxisMoving;
        }

        #endregion

        #region Event Handlers

        private void btnHomeAll_Click(object sender, EventArgs e)
        {
            _viewModel?.HomeAllCommand?.Execute(null);
        }

        private void btnStopAll_Click(object sender, EventArgs e)
        {
            _viewModel?.StopAllCommand?.Execute(null);
        }

        private void btnEmergencyStop_Click(object sender, EventArgs e)
        {
            _viewModel?.EmergencyStopAllCommand?.Execute(null);
        }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _updateTimer?.Stop();
                _updateTimer?.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
