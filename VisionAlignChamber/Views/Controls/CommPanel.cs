using ObjectCommunication.Models;
using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using VisionAlignChamber.Communication;
using VisionAlignChamber.Config;

namespace VisionAlignChamber.Views.Controls
{
    /// <summary>
    /// CTC 통신 설정 및 로그 뷰어 패널
    /// </summary>
    public partial class CommPanel : UserControl
    {
        #region Fields

        private CTCCommController _ctcController;
        private readonly object _logLock = new object();
        private const int MaxLogLines = 1000;

        #endregion

        #region Constructor

        public CommPanel()
        {
            InitializeComponent();
            InitializeUI();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// CTCCommController 바인딩
        /// </summary>
        public void BindController(CTCCommController controller)
        {
            // 기존 이벤트 해제
            if (_ctcController != null)
            {
                _ctcController.OnConnectionChanged -= OnConnectionChanged;
                _ctcController.OnObjectReceived -= OnObjectReceived;
                _ctcController.OnCommandReceived -= OnCommandReceived;
            }

            _ctcController = controller;

            if (_ctcController != null)
            {
                // 이벤트 구독
                _ctcController.OnConnectionChanged += OnConnectionChanged;
                _ctcController.OnObjectReceived += OnObjectReceived;
                _ctcController.OnCommandReceived += OnCommandReceived;

                // UI 업데이트
                UpdateConnectionStatus();
            }

            UpdateUIState();
        }

        /// <summary>
        /// 로그 추가
        /// </summary>
        public void AddLog(string message, LogLevel level = LogLevel.Info)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => AddLog(message, level)));
                return;
            }

            lock (_logLock)
            {
                string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
                string prefix = GetLogPrefix(level);
                string logText = $"[{timestamp}] {prefix} {message}";

                // 최대 줄 수 제한
                if (rtbCommLog.Lines.Length > MaxLogLines)
                {
                    rtbCommLog.SelectionStart = 0;
                    rtbCommLog.SelectionLength = rtbCommLog.GetFirstCharIndexFromLine(100);
                    rtbCommLog.SelectedText = "";
                }

                // 색상 설정
                rtbCommLog.SelectionStart = rtbCommLog.TextLength;
                rtbCommLog.SelectionColor = GetLogColor(level);
                rtbCommLog.AppendText(logText + Environment.NewLine);
                rtbCommLog.SelectionColor = rtbCommLog.ForeColor;

                // 자동 스크롤
                if (chkAutoScroll.Checked)
                {
                    rtbCommLog.SelectionStart = rtbCommLog.TextLength;
                    rtbCommLog.ScrollToCaret();
                }
            }
        }

        #endregion

        #region Private Methods - Initialization

        private void InitializeUI()
        {
            // 설정값 로드
            txtServerIP.Text = GetLocalIPAddress();
            numServerPort.Value = AppSettings.CTCPort;

            // 초기 상태
            UpdateUIState();
        }

        private string GetLocalIPAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
            }
            catch { }
            return "127.0.0.1";
        }

        #endregion

        #region Private Methods - UI Update

        private void UpdateUIState()
        {
            bool hasController = _ctcController != null;
            bool isListening = _ctcController?.IsListening ?? false;

            btnStart.Enabled = hasController && !isListening;
            btnStop.Enabled = hasController && isListening;
            numServerPort.Enabled = !isListening;
            btnApplyPort.Enabled = !isListening;

            UpdateConnectionStatus();
        }

        private void UpdateConnectionStatus()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(UpdateConnectionStatus));
                return;
            }

            if (_ctcController == null)
            {
                lblStatus.Text = "N/A";
                lblStatus.ForeColor = Color.Gray;
                lblClientCount.Text = "0";
                return;
            }

            bool isListening = _ctcController.IsListening;
            int clientCount = _ctcController.ClientCount;

            lblClientCount.Text = clientCount.ToString();

            if (clientCount > 0)
            {
                lblStatus.Text = "Connected";
                lblStatus.ForeColor = Color.LimeGreen;
            }
            else if (isListening)
            {
                lblStatus.Text = "Listening";
                lblStatus.ForeColor = Color.Yellow;
            }
            else
            {
                lblStatus.Text = "Stopped";
                lblStatus.ForeColor = Color.Gray;
            }
        }

        #endregion

        #region Private Methods - Event Handlers

        private void OnConnectionChanged(IPEndPoint endPoint, bool connected)
        {
            string status = connected ? "Connected" : "Disconnected";
            AddLog($"Client {endPoint} {status}", connected ? LogLevel.Success : LogLevel.Warning);
            UpdateConnectionStatus();
            UpdateUIState();
        }

        private void OnObjectReceived(CommObjectBase obj)
        {
            AddLog($"[RX] {obj.GetType().Name}", LogLevel.Receive);
        }

        private void OnCommandReceived(CommObject.CommandObject cmd)
        {
            AddLog($"[RX] Command: {cmd.Command}", LogLevel.Receive);
        }

        #endregion

        #region Private Methods - Button Handlers

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                _ctcController?.Start();
                AddLog($"Server started on port {AppSettings.CTCPort}", LogLevel.Success);
                UpdateUIState();
            }
            catch (Exception ex)
            {
                AddLog($"Start failed: {ex.Message}", LogLevel.Error);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                _ctcController?.Stop();
                AddLog("Server stopped", LogLevel.Warning);
                UpdateUIState();
            }
            catch (Exception ex)
            {
                AddLog($"Stop failed: {ex.Message}", LogLevel.Error);
            }
        }

        private void btnApplyPort_Click(object sender, EventArgs e)
        {
            int newPort = (int)numServerPort.Value;
            AppSettings.CTCPort = newPort;
            AddLog($"Port changed to {newPort} (restart server to apply)", LogLevel.Info);
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            rtbCommLog.Clear();
        }

        private void btnSaveLog_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                sfd.FileName = $"CommLog_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        System.IO.File.WriteAllText(sfd.FileName, rtbCommLog.Text);
                        AddLog($"Log saved: {sfd.FileName}", LogLevel.Success);
                    }
                    catch (Exception ex)
                    {
                        AddLog($"Save failed: {ex.Message}", LogLevel.Error);
                    }
                }
            }
        }

        #endregion

        #region Helper Methods

        private string GetLogPrefix(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Error: return "[ERROR]";
                case LogLevel.Warning: return "[WARN]";
                case LogLevel.Success: return "[OK]";
                case LogLevel.Send: return "[TX]";
                case LogLevel.Receive: return "[RX]";
                default: return "[INFO]";
            }
        }

        private Color GetLogColor(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Error: return Color.Red;
                case LogLevel.Warning: return Color.Orange;
                case LogLevel.Success: return Color.LimeGreen;
                case LogLevel.Send: return Color.Cyan;
                case LogLevel.Receive: return Color.Yellow;
                default: return Color.White;
            }
        }

        #endregion

        #region Enums

        public enum LogLevel
        {
            Info,
            Warning,
            Error,
            Success,
            Send,
            Receive
        }

        #endregion
    }
}
