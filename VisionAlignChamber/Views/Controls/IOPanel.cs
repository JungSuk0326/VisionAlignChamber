using System;
using System.Drawing;
using System.Windows.Forms;
using VisionAlignChamber.ViewModels;
using VisionAlignChamber.Config;

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

            // 시뮬레이션 모드에서 Digital Input Label 클릭 이벤트 연결
            if (AppSettings.SimulationMode)
            {
                SetupSimulationInputClickEvents();
            }

            _updateTimer.Start();
        }

        /// <summary>
        /// 시뮬레이션 모드에서 Digital Input Label 클릭 이벤트 설정
        /// </summary>
        private void SetupSimulationInputClickEvents()
        {
            lblSensor1.Cursor = Cursors.Hand;
            lblSensor2.Cursor = Cursors.Hand;
            lblPNCheckP.Cursor = Cursors.Hand;
            lblPNCheckN.Cursor = Cursors.Hand;

            lblSensor1.Click += lblSensor1_Click;
            lblSensor2.Click += lblSensor2_Click;
            lblPNCheckP.Click += lblPNCheckP_Click;
            lblPNCheckN.Click += lblPNCheckN_Click;

            // 툴팁 추가
            var toolTip = new ToolTip();
            toolTip.SetToolTip(lblSensor1, "[Simulation] 클릭하여 토글");
            toolTip.SetToolTip(lblSensor2, "[Simulation] 클릭하여 토글");
            toolTip.SetToolTip(lblPNCheckP, "[Simulation] 클릭하여 토글");
            toolTip.SetToolTip(lblPNCheckN, "[Simulation] 클릭하여 토글");
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

        #region Event Handlers - All Off

        private void btnAllOff_Click(object sender, EventArgs e)
        {
            _viewModel?.AllOutputOffCommand?.Execute(null);
        }

        #endregion

        #region Event Handlers - Simulation Input Toggle

        private void lblSensor1_Click(object sender, EventArgs e)
        {
            _viewModel?.ToggleSensor1Command?.Execute(null);
        }

        private void lblSensor2_Click(object sender, EventArgs e)
        {
            _viewModel?.ToggleSensor2Command?.Execute(null);
        }

        private void lblPNCheckP_Click(object sender, EventArgs e)
        {
            _viewModel?.TogglePNCheckPCommand?.Execute(null);
        }

        private void lblPNCheckN_Click(object sender, EventArgs e)
        {
            _viewModel?.TogglePNCheckNCommand?.Execute(null);
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
