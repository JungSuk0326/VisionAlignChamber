using System;
using System.Drawing;
using System.Windows.Forms;
using VisionAlignChamber.ViewModels;
using VisionAlignChamber.Hardware.Facade;
using VisionAlignChamber.Hardware.Ajin;
using VisionAlignChamber.Hardware.Simulation;
using VisionAlignChamber.Vision;
using VisionAlignChamber.Core;
using VisionAlignChamber.Eddy;
using VisionAlignChamber.Interfaces;
using VisionAlignChamber.Models;
using VisionAlignChamber.Config;

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

        // 하드웨어 (인터페이스 타입으로 선언 - 실제/시뮬레이션 모두 지원)
        private IMotionController _motionController;
        private IDigitalIO _digitalIO;
        private HardwareMapping _hardwareMapping;
        private VisionAlignerMotion _vaMotion;
        private VisionAlignerIO _vaIO;
        private VisionAlignWrapper _vision;
        private IEddyCurrentSensor _eddySensor;

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

            var initErrors = new System.Collections.Generic.List<string>();
            bool isSimulation = AppSettings.SimulationMode;

            // 1. HardwareMapping 초기화 (필수)
            try
            {
                _hardwareMapping = new HardwareMapping(configPath);
            }
            catch (Exception ex)
            {
                initErrors.Add($"HardwareMapping: {ex.Message}");
                _hardwareMapping = null;
            }

            // 2. Motion 초기화 (Simulation 모드 분기)
            try
            {
                if (isSimulation)
                {
                    _motionController = new SimulationMotionController();
                    System.Diagnostics.Debug.WriteLine("[Simulation Mode] Using SimulationMotionController");
                }
                else
                {
                    _motionController = new AjinMotionController();
                }

                if (_hardwareMapping != null)
                {
                    _vaMotion = new VisionAlignerMotion(_motionController, _hardwareMapping);
                }
            }
            catch (Exception ex)
            {
                initErrors.Add($"Motion: {ex.Message}");
                _vaMotion = null;
            }

            // 3. IO 초기화 (Simulation 모드 분기)
            try
            {
                if (isSimulation)
                {
                    _digitalIO = new SimulationDigitalIO();
                    System.Diagnostics.Debug.WriteLine("[Simulation Mode] Using SimulationDigitalIO");
                }
                else
                {
                    _digitalIO = new AjinDigitalIO();
                }

                if (_hardwareMapping != null)
                {
                    _vaIO = new VisionAlignerIO(_digitalIO, _hardwareMapping);
                }
            }
            catch (Exception ex)
            {
                initErrors.Add($"IO: {ex.Message}");
                _vaIO = null;
            }

            // 4. Vision 초기화 (개별)
            try
            {
                _vision = new VisionAlignWrapper();
            }
            catch (Exception ex)
            {
                initErrors.Add($"Vision: {ex.Message}");
                _vision = null;
            }

            // 5. Eddy 초기화 (개별)
            try
            {
                _eddySensor = new EddyCurrentSensor();
            }
            catch (Exception ex)
            {
                initErrors.Add($"Eddy: {ex.Message}");
                _eddySensor = null;
            }

            // VisionAlignerSystem 생성 (사용 가능한 모듈만 전달)
            _system = new VisionAlignerSystem(_vaMotion, _vaIO, _vision, _eddySensor);

            // ViewModel 생성 및 초기화
            _viewModel = new MainViewModel();
            _viewModel.Initialize(_system);

            // 상태 메시지
            string modeText = isSimulation ? "[Simulation] " : "";
            if (initErrors.Count == 0)
            {
                UpdateStatusMessage($"{modeText}시스템 준비 완료");
            }
            else
            {
                string availableModules = GetAvailableModulesText();
                UpdateStatusMessage($"{modeText}일부 모듈 초기화 실패. 사용 가능: {availableModules}");
                System.Diagnostics.Debug.WriteLine($"초기화 오류: {string.Join(", ", initErrors)}");
            }
        }

        private string GetAvailableModulesText()
        {
            var modules = new System.Collections.Generic.List<string>();
            if (_vaMotion != null) modules.Add("Motion");
            if (_vaIO != null) modules.Add("IO");
            if (_vision != null) modules.Add("Vision");
            if (_eddySensor != null) modules.Add("Eddy");
            return modules.Count > 0 ? string.Join(", ", modules) : "없음";
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

            // Eddy 탭 바인딩
            if (_viewModel.Eddy != null)
            {
                eddyPanel.BindViewModel(_viewModel.Eddy);
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
                                        _viewModel.IsRunning ? Color.Green : Color.White;

            // 제어권 상태
            UpdateControlAuthorityDisplay();

            // 모드 라디오 버튼 상태
            UpdateModeRadioButtons();

            // UI 활성화/비활성화
            UpdateUIEnabledState();
        }

        private void UpdateControlAuthorityDisplay()
        {
            lblControlAuthority.Text = _viewModel.ControlAuthority.ToString();

            switch (_viewModel.ControlAuthority)
            {
                case ControlAuthority.Local:
                    lblControlAuthority.ForeColor = Color.Cyan;
                    break;
                case ControlAuthority.Remote:
                    lblControlAuthority.ForeColor = Color.Yellow;
                    break;
                case ControlAuthority.Locked:
                    lblControlAuthority.ForeColor = Color.Red;
                    break;
            }
        }

        private bool _isUpdatingRadioButtons = false;

        private void UpdateModeRadioButtons()
        {
            // 라디오 버튼 상태 동기화 (플래그로 이벤트 처리 방지)
            _isUpdatingRadioButtons = true;
            try
            {
                bool shouldBeManual = _viewModel.IsManualMode || _viewModel.IsSetupMode;
                bool shouldBeAuto = _viewModel.IsAutoMode;

                if (rbManual.Checked != shouldBeManual)
                {
                    rbManual.Checked = shouldBeManual;
                }
                if (rbAuto.Checked != shouldBeAuto)
                {
                    rbAuto.Checked = shouldBeAuto;
                }

                // 색상 업데이트
                rbManual.ForeColor = rbManual.Checked ? Color.LimeGreen : Color.White;
                rbAuto.ForeColor = rbAuto.Checked ? Color.LimeGreen : Color.White;
            }
            finally
            {
                _isUpdatingRadioButtons = false;
            }
        }

        private void UpdateUIEnabledState()
        {
            bool canOperate = _viewModel.CanOperateUI;

            // 시스템 버튼
            btnInitialize.Enabled = canOperate && !_viewModel.IsInitialized;
            btnHomeAll.Enabled = canOperate && _viewModel.IsInitialized && !_viewModel.IsHomed;
            btnResetAlarm.Enabled = _viewModel.IsLocalControl && _viewModel.HasActiveAlarm;

            // 모드 전환 (개별 컨트롤 제어)
            // Manual은 Local 상태에서 항상 가능
            rbManual.Enabled = _viewModel.IsLocalControl;

            // Auto는 Initialize + Home 완료 후에만 가능 (프로덕션)
            // Simulation 모드: Local이면 Auto 활성화 (Initialize/Home 불필요)
            bool autoEnabled = AppSettings.SimulationMode
                ? _viewModel.IsLocalControl
                : _viewModel.IsLocalControl && _viewModel.IsInitialized && _viewModel.IsHomed;
            rbAuto.Enabled = autoEnabled;

            // 탭 패널 활성화
            motionPanel.Enabled = canOperate;
            ioPanel.Enabled = canOperate;
            visionPanel.Enabled = canOperate;
            eddyPanel.Enabled = canOperate;
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

        private void rbManual_CheckedChanged(object sender, EventArgs e)
        {
            // 프로그래밍적 업데이트 중에는 무시
            if (_isUpdatingRadioButtons) return;

            if (rbManual.Checked && _viewModel != null)
            {
                _viewModel.SetManualModeCommand?.Execute(null);
            }
        }

        private void rbAuto_CheckedChanged(object sender, EventArgs e)
        {
            // 프로그래밍적 업데이트 중에는 무시
            if (_isUpdatingRadioButtons) return;

            if (rbAuto.Checked && _viewModel != null)
            {
                _viewModel.SetAutoModeCommand?.Execute(null);
            }
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
