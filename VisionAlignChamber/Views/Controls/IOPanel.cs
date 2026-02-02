using System;
using System.Drawing;
using System.Windows.Forms;
using VisionAlignChamber.ViewModels;

namespace VisionAlignChamber.Views.Controls
{
    /// <summary>
    /// I/O 제어 패널
    /// </summary>
    public partial class IOPanel : UserControl
    {
        #region Fields

        private IOViewModel _viewModel;
        private Timer _updateTimer;

        #endregion

        #region Constructor

        public IOPanel()
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
        public void BindViewModel(IOViewModel viewModel)
        {
            _viewModel = viewModel;

            if (_viewModel == null) return;

            _updateTimer.Start();
        }

        #endregion

        #region Timer

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (_viewModel == null) return;

            UpdateInitStatus();
            UpdateInputStatus();
            UpdateOutputStatus();
        }

        private void UpdateInitStatus()
        {
            // 초기화 상태 표시
            lblInitStatus.Text = _viewModel.IsInitialized ? "Initialized" : "Not Initialized";
            lblInitStatus.ForeColor = _viewModel.IsInitialized ? Color.LimeGreen : Color.Gray;

            // 버튼 활성화 상태
            btnInitialize.Enabled = !_viewModel.IsInitialized;
        }

        private void UpdateInputStatus()
        {
            // Wafer Sensors
            UpdateIndicator(lblSensor1, _viewModel.Sensor1WaferDetected);
            UpdateIndicator(lblSensor2, _viewModel.Sensor2WaferDetected);

            // PN Check
            UpdateIndicator(lblPNCheckP, _viewModel.PNCheckP);
            UpdateIndicator(lblPNCheckN, _viewModel.PNCheckN);
        }

        private void UpdateOutputStatus()
        {
            // Lift Pin
            chkLiftPinVacuum.Checked = _viewModel.LiftPinVacuum;
            chkLiftPinBlow.Checked = _viewModel.LiftPinBlow;

            // Chuck
            chkChuckVacuum.Checked = _viewModel.ChuckVacuum;
            chkChuckBlow.Checked = _viewModel.ChuckBlow;

            // Vision Light
            chkVisionLight.Checked = _viewModel.VisionLightOn;
        }

        private void UpdateIndicator(Label label, bool isOn)
        {
            label.BackColor = isOn ? Color.LimeGreen : Color.Gray;
        }

        #endregion

        #region Event Handlers - Initialize

        private void btnInitialize_Click(object sender, EventArgs e)
        {
            _viewModel?.InitializeCommand?.Execute(null);
        }

        #endregion

        #region Event Handlers - Lift Pin

        private void btnLiftPinVacuumOn_Click(object sender, EventArgs e)
        {
            _viewModel?.LiftPinVacuumOnCommand?.Execute(null);
        }

        private void btnLiftPinVacuumOff_Click(object sender, EventArgs e)
        {
            _viewModel?.LiftPinVacuumOffCommand?.Execute(null);
        }

        private void btnLiftPinBlowOn_Click(object sender, EventArgs e)
        {
            _viewModel?.LiftPinBlowOnCommand?.Execute(null);
        }

        private void btnLiftPinBlowOff_Click(object sender, EventArgs e)
        {
            _viewModel?.LiftPinBlowOffCommand?.Execute(null);
        }

        #endregion

        #region Event Handlers - Chuck

        private void btnChuckVacuumOn_Click(object sender, EventArgs e)
        {
            _viewModel?.ChuckVacuumOnCommand?.Execute(null);
        }

        private void btnChuckVacuumOff_Click(object sender, EventArgs e)
        {
            _viewModel?.ChuckVacuumOffCommand?.Execute(null);
        }

        private void btnChuckBlowOn_Click(object sender, EventArgs e)
        {
            _viewModel?.ChuckBlowOnCommand?.Execute(null);
        }

        private void btnChuckBlowOff_Click(object sender, EventArgs e)
        {
            _viewModel?.ChuckBlowOffCommand?.Execute(null);
        }

        #endregion

        #region Event Handlers - Vision Light

        private void btnVisionLightOn_Click(object sender, EventArgs e)
        {
            _viewModel?.VisionLightOnCommand?.Execute(null);
        }

        private void btnVisionLightOff_Click(object sender, EventArgs e)
        {
            _viewModel?.VisionLightOffCommand?.Execute(null);
        }

        #endregion

        #region Event Handlers - All Off

        private void btnAllOff_Click(object sender, EventArgs e)
        {
            _viewModel?.AllOutputOffCommand?.Execute(null);
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
