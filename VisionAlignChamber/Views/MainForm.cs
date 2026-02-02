using System;
using System.Drawing;
using System.Windows.Forms;
using VisionAlignChamber.ViewModels;
using VisionAlignChamber.Hardware.IO;
using VisionAlignChamber.Hardware.Ajin;
using VisionAlignChamber.Vision;
using VisionAlignChamber.Core;

namespace VisionAlignChamber.Views
{
    /// <summary>
    /// 메인 폼 - 탭 기반 UI
    /// 좌측: Vision 이미지 표시
    /// 우측: Motion/IO/Vision 탭
    /// </summary>
    public partial class MainForm : Form
    {
        #region Fields

        private MainViewModel _viewModel;
        private Timer _updateTimer;

        // 하드웨어 (실제 연결 시 사용)
        private AjinMotionController _motionController;
        private AjinDigitalIO _digitalIO;
        private IOMapping _ioMapping;
        private VisionAlignerMotion _vaMotion;
        private VisionAlignerIO _vaIO;
        private VisionAlignWrapper _vision;

        // 비즈니스 로직 (Core)
        private VisionAlignerSystem _system;

        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
            InitializeViewModel();
            InitializeTimer();
            BindViewModel();
        }

        #endregion

        #region Initialization

        private void InitializeViewModel()
        {
            // 설정 파일 경로
            string configPath = System.IO.Path.Combine(
                Application.StartupPath, "Config", "Settings.ini");

            try
            {
                // 하드웨어 초기화 (시뮬레이션 모드로 시작 가능)
                _motionController = new AjinMotionController();
                _digitalIO = new AjinDigitalIO();
                _ioMapping = new IOMapping(configPath);

                // Facade 생성
                _vaMotion = new VisionAlignerMotion(_motionController, _ioMapping);
                _vaIO = new VisionAlignerIO(_digitalIO, _ioMapping);
                _vision = new VisionAlignWrapper();

                // VisionAlignerSystem 생성 (비즈니스 로직 레이어)
                _system = new VisionAlignerSystem(_vaMotion, _vaIO, _vision);

                // ViewModel 생성 및 초기화
                _viewModel = new MainViewModel();
                _viewModel.Initialize(_system);

                UpdateStatusMessage("시스템 준비 완료");
            }
            catch (Exception ex)
            {
                _viewModel = new MainViewModel();
                UpdateStatusMessage($"초기화 오류: {ex.Message}");
            }
        }

        private void InitializeTimer()
        {
            _updateTimer = new Timer();
            _updateTimer.Interval = 100; // 100ms
            _updateTimer.Tick += UpdateTimer_Tick;
            _updateTimer.Start();
        }

        private void BindViewModel()
        {
            if (_viewModel == null) return;

            // Motion 탭 바인딩
            if (_viewModel.Motion != null)
            {
                motionPanel.BindViewModel(_viewModel.Motion);
            }

            // IO 탭 바인딩
            if (_viewModel.IO != null)
            {
                ioPanel.BindViewModel(_viewModel.IO);
            }

            // Vision 탭 바인딩
            if (_viewModel.Vision != null)
            {
                visionPanel.BindViewModel(_viewModel.Vision);
            }
        }

        #endregion

        #region Timer

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // ViewModel 상태 업데이트
                _viewModel?.UpdateStatus();

                // UI 업데이트
                UpdateUI();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Update error: {ex.Message}");
            }
        }

        private void UpdateUI()
        {
            if (_viewModel == null) return;

            // 상태바 업데이트
            UpdateStatusBar();

            // 이미지 업데이트 (Vision에서)
            UpdateVisionImage();
        }

        private void UpdateStatusBar()
        {
            // 시스템 상태
            lblSystemStatus.Text = _viewModel.CurrentStatus.ToString();
            lblSystemMode.Text = _viewModel.CurrentMode.ToString();

            // 상태 색상
            lblSystemStatus.ForeColor = _viewModel.IsError ? Color.Red :
                                        _viewModel.IsRunning ? Color.Green : Color.Black;
        }

        private void UpdateVisionImage()
        {
            if (_viewModel?.Vision?.CurrentImage != null)
            {
                picVisionDisplay.Image = _viewModel.Vision.CurrentImage;
            }
            else if (_viewModel?.Vision?.ResultImage != null)
            {
                picVisionDisplay.Image = _viewModel.Vision.ResultImage;
            }
        }

        private void UpdateStatusMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateStatusMessage), message);
                return;
            }

            lblStatusMessage.Text = message;
        }

        #endregion

        #region Event Handlers

        private void btnEmergencyStop_Click(object sender, EventArgs e)
        {
            _viewModel?.EmergencyStopCommand?.Execute(null);
            UpdateStatusMessage("비상 정지!");
        }

        private void btnInitialize_Click(object sender, EventArgs e)
        {
            _viewModel?.InitializeSystemCommand?.Execute(null);
        }

        private void btnHomeAll_Click(object sender, EventArgs e)
        {
            _viewModel?.HomeAllCommand?.Execute(null);
        }

        private void btnResetAlarm_Click(object sender, EventArgs e)
        {
            _viewModel?.ResetAlarmCommand?.Execute(null);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _updateTimer?.Stop();
            _updateTimer?.Dispose();

            _viewModel?.Dispose();

            // VisionAlignerSystem이 내부적으로 모든 하드웨어 종료 처리
            _system?.Dispose();
        }

        #endregion
    }
}
