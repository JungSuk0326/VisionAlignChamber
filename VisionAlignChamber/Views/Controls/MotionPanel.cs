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

            // 개별 축 바인딩 (활성화된 축만 표시)
            BindAxisPanel(axisWedge, _viewModel.WedgeAxis);
            BindAxisPanel(axisChuck, _viewModel.ChuckAxis);
            BindAxisPanel(axisCentering1, _viewModel.CenteringStage1Axis);
            BindAxisPanel(axisCentering2, _viewModel.CenteringStage2Axis);

            _updateTimer.Start();
        }

        private void BindAxisPanel(AxisControlPanel panel, AxisViewModel axis)
        {
            if (axis.IsEnabled)
            {
                panel.BindViewModel(axis);
                panel.Visible = true;
            }
            else
            {
                panel.Visible = false;
            }
        }

        #endregion

        #region Timer

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (_viewModel == null) return;

            // 활성화된 축만 UI 업데이트
            if (axisWedge.Visible) axisWedge.UpdateDisplay();
            if (axisChuck.Visible) axisChuck.UpdateDisplay();
            if (axisCentering1.Visible) axisCentering1.UpdateDisplay();
            if (axisCentering2.Visible) axisCentering2.UpdateDisplay();

            // 전체 상태 업데이트
            UpdateGlobalStatus();
        }

        private void UpdateGlobalStatus()
        {
            // 초기화 상태 표시
            lblInitStatus.Text = _viewModel.IsInitialized ? "Initialized" : "Not Initialized";
            lblInitStatus.ForeColor = _viewModel.IsInitialized ?
                System.Drawing.Color.LimeGreen : System.Drawing.Color.Gray;

            // 홈 상태 표시
            lblAllHomedStatus.Text = _viewModel.IsAllHomed ? "All Homed" : "Not Homed";
            lblAllHomedStatus.ForeColor = _viewModel.IsAllHomed ?
                System.Drawing.Color.LimeGreen : System.Drawing.Color.Orange;

            // 버튼 활성화 상태
            btnInitialize.Enabled = !_viewModel.IsInitialized;
            btnHomeAll.Enabled = _viewModel.IsInitialized && !_viewModel.IsAnyAxisMoving;
        }

        #endregion

        #region Event Handlers

        private void btnInitialize_Click(object sender, EventArgs e)
        {
            _viewModel?.InitializeCommand?.Execute(null);
        }

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
