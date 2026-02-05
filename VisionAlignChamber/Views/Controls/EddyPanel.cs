using System;
using System.Drawing;
using System.Windows.Forms;
using VisionAlignChamber.ViewModels;

namespace VisionAlignChamber.Views.Controls
{
    /// <summary>
    /// Eddy Current 센서 제어 패널
    /// </summary>
    public partial class EddyPanel : UserControl
    {
        #region Fields

        private EddyViewModel _viewModel;
        private Timer _updateTimer;

        #endregion

        #region Constructor

        public EddyPanel()
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
        public void BindViewModel(EddyViewModel viewModel)
        {
            _viewModel = viewModel;

            if (_viewModel == null) return;

            // 초기값 설정
            txtIpAddress.Text = _viewModel.IpAddress;
            txtPort.Text = _viewModel.Port.ToString();

            _updateTimer.Start();
        }

        #endregion

        #region Timer

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (_viewModel == null) return;

            UpdateConnectionStatus();
            UpdateMeasurementDisplay();
            UpdateButtonStates();
        }

        private void UpdateConnectionStatus()
        {
            // 연결 상태 표시
            if (_viewModel.IsConnected)
            {
                lblConnectionStatus.Text = "Connected";
                lblConnectionStatus.ForeColor = Color.LimeGreen;
            }
            else
            {
                lblConnectionStatus.Text = "Disconnected";
                lblConnectionStatus.ForeColor = Color.Gray;
            }

            // 상태 메시지
            lblStatusMessage.Text = _viewModel.StatusMessage ?? "";
        }

        private void UpdateMeasurementDisplay()
        {
            // 측정값 표시
            lblCurrentValue.Text = _viewModel.CurrentValue.ToString("F4");

            // 영점 상태 표시
            lblZeroStatus.Text = _viewModel.IsZeroSet ? "Zero Set" : "Not Set";
            lblZeroStatus.ForeColor = _viewModel.IsZeroSet ? Color.LimeGreen : Color.Gray;
        }

        private void UpdateButtonStates()
        {
            bool connected = _viewModel.IsConnected;
            bool busy = _viewModel.IsBusy;

            btnConnect.Enabled = !connected && !busy;
            btnDisconnect.Enabled = connected && !busy;
            btnSetZero.Enabled = connected && !busy;
            btnGetData.Enabled = connected && !busy;

            // 입력 필드 활성화
            txtIpAddress.Enabled = !connected;
            txtPort.Enabled = !connected;
        }

        #endregion

        #region Event Handlers - Connection

        private void btnConnect_Click(object sender, EventArgs e)
        {
            // ViewModel에 값 업데이트
            _viewModel.IpAddress = txtIpAddress.Text;

            if (int.TryParse(txtPort.Text, out int port))
            {
                _viewModel.Port = port;
            }
            else
            {
                MessageBox.Show("유효하지 않은 포트입니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPort.Focus();
                return;
            }

            _viewModel?.ConnectCommand?.Execute(null);
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            _viewModel?.DisconnectCommand?.Execute(null);
        }

        #endregion

        #region Event Handlers - Measurement

        private void btnSetZero_Click(object sender, EventArgs e)
        {
            _viewModel?.SetZeroCommand?.Execute(null);
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            _viewModel?.GetDataCommand?.Execute(null);
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
