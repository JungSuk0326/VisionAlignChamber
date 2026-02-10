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
using VisionAlignChamber.Interlock;
using VisionAlignChamber.Log;
using VisionAlignChamber.Communication;
using VisionAlignChamber.Communication.Interfaces;

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
        private LogViewModel _logViewModel;
        private Timer _updateTimer;
        private bool _alarmBlinkState; // 알람 깜빡임 상태

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

        // CTC 통신
        private CTCCommController _ctcController;

        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
            InitializeAppContext();
            SubscribeEvents();
            InitializeViewModel();
            InitializeTimer();
            BindViewModel();
            InitializeAlarmIndicator();
        }

        #endregion

        #region Initialization

        private void InitializeAppContext()
        {
            // LogManager 초기화
            LogManager.Initialize();

            // AppContext 초기화
            Core.AppContext.Current.Initialize();
            // Note: AppSettings는 static 클래스이므로 직접 접근
        }

        private void SubscribeEvents()
        {
            // EventManager 구독
            EventManager.Subscribe(EventManager.StatusMessage, OnStatusMessageReceived);
            EventManager.Subscribe(EventManager.AlarmOccurred, OnAlarmOccurred);
            EventManager.Subscribe(EventManager.AlarmCleared, OnAlarmCleared);
            EventManager.Subscribe(EventManager.ControlAuthorityChanged, OnControlAuthorityChanged);
            EventManager.Subscribe(EventManager.SystemStateChanged, OnSystemStateChanged);
        }

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

            // 6. CTC 통신 초기화 (UI 스레드에서 생성)
            try
            {
                _ctcController = new CTCCommController(AppSettings.CTCPort);
                SubscribeCTCEvents();
                _ctcController.Start(); // 서버 리스닝 시작
                UpdateCTCStatusDisplay(); // 초기 상태 표시
            }
            catch (Exception ex)
            {
                initErrors.Add($"CTC Communication: {ex.Message}");
                _ctcController = null;
                UpdateCTCStatusDisplay(); // N/A 표시
            }

            // VisionAlignerSystem 생성 (사용 가능한 모듈만 전달)
            _system = new VisionAlignerSystem(_vaMotion, _vaIO, _vision, _eddySensor, _ctcController);

            // AppContext에 서비스 등록
            Core.AppContext.Current.Motion = _motionController;
            Core.AppContext.Current.DigitalIO = _digitalIO;

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
            if (_ctcController != null) modules.Add("CTC");
            return modules.Count > 0 ? string.Join(", ", modules) : "없음";
        }

        /// <summary>
        /// CTC 통신 이벤트 구독 (UI 스레드 안전 처리)
        /// </summary>
        private void SubscribeCTCEvents()
        {
            if (_ctcController == null) return;

            // 연결 상태 변경 이벤트
            _ctcController.OnConnectionChanged += (endPoint, connected) =>
            {
                BeginInvoke(new Action(() =>
                {
                    string status = connected ? "연결됨" : "연결 해제됨";
                    UpdateStatusMessage($"[CTC] {endPoint} {status}");
                    LogManager.System.Info($"[CTC] {endPoint} {status}");

                    // CTC 상태 표시 업데이트
                    UpdateCTCStatusDisplay();
                }));
            };

            // 명령 수신 이벤트 (UI 표시용)
            _ctcController.OnCommandReceived += cmd =>
            {
                BeginInvoke(new Action(() =>
                {
                    UpdateStatusMessage($"[CTC] 명령 수신: {cmd.Command}");
                }));
            };

            // 객체 수신 이벤트 (로그용)
            _ctcController.OnObjectReceived += obj =>
            {
                BeginInvoke(new Action(() =>
                {
                    LogManager.System.Debug($"[CTC] 객체 수신: {obj.GetType().Name}");
                }));
            };
        }

        /// <summary>
        /// CTC 연결 상태 표시 업데이트
        /// </summary>
        private void UpdateCTCStatusDisplay()
        {
            if (_ctcController == null)
            {
                lblCTCStatus.Text = "N/A";
                lblCTCStatus.ForeColor = Color.Gray;
                return;
            }

            int clientCount = _ctcController.ClientCount;
            bool isListening = _ctcController.IsListening;

            if (clientCount > 0)
            {
                // 클라이언트 연결됨
                lblCTCStatus.Text = $"Connected ({clientCount})";
                lblCTCStatus.ForeColor = Color.LimeGreen;
            }
            else if (isListening)
            {
                // 서버 리스닝 중 (연결 대기)
                lblCTCStatus.Text = "Listening...";
                lblCTCStatus.ForeColor = Color.Yellow;
            }
            else
            {
                // 서버 미시작
                lblCTCStatus.Text = "Stopped";
                lblCTCStatus.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// CTC 통신 이벤트 구독 해제
        /// </summary>
        private void UnsubscribeCTCEvents()
        {
            // CTCCommController.Dispose()에서 자동으로 이벤트 언링크됨
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

            // Log 탭 바인딩
            _logViewModel = new LogViewModel();
            logPanel.BindViewModel(_logViewModel);
            LogManager.RegisterLogViewModel(_logViewModel);

            // Comm 탭 바인딩 (CTC 컨트롤러)
            if (_ctcController != null)
            {
                commPanel.BindController(_ctcController);
            }

            // 초기 로그 메시지
            LogManager.System.Info("VisionAlignChamber 시작");
        }

        private void InitializeAlarmIndicator()
        {
            // 시작 시 활성 알람 확인하여 표시등 상태 초기화
            UpdateAlarmIndicator();
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

            // 제어권 라디오 버튼 상태
            UpdateControlAuthorityRadioButtons();

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

        private void UpdateControlAuthorityRadioButtons()
        {
            // 라디오 버튼 상태 동기화 (플래그로 이벤트 처리 방지)
            _isUpdatingRadioButtons = true;
            try
            {
                bool shouldBeLocal = _viewModel.IsLocalControl;
                bool shouldBeRemote = _viewModel.IsRemoteControl;

                if (rbLocal.Checked != shouldBeLocal)
                {
                    rbLocal.Checked = shouldBeLocal;
                }
                if (rbRemote.Checked != shouldBeRemote)
                {
                    rbRemote.Checked = shouldBeRemote;
                }

                // 색상 업데이트
                rbLocal.ForeColor = rbLocal.Checked ? Color.LimeGreen : Color.White;
                rbRemote.ForeColor = rbRemote.Checked ? Color.Yellow : Color.White;
            }
            finally
            {
                _isUpdatingRadioButtons = false;
            }
        }

        private void UpdateUIEnabledState()
        {
            bool canOperate = _viewModel.CanOperateUI;
            bool isLocal = _viewModel.IsLocalControl;

            // 시스템 버튼 (Local 모드에서만 활성화)
            btnInitialize.Enabled = isLocal && !_viewModel.IsInitialized;
            btnHomeAll.Enabled = isLocal && _viewModel.IsInitialized && !_viewModel.IsHomed;
            btnResetAlarm.Enabled = isLocal && _viewModel.HasActiveAlarm;

            // 제어권 전환 (Local/Remote)
            // Local 버튼: Locked 상태가 아니면 항상 활성화
            rbLocal.Enabled = !_viewModel.IsLocked;

            // Remote 버튼: Local 상태 + Initialize + Home 완료 후에만 활성화
            // Simulation 모드: Local이면 Remote 활성화 가능
            bool remoteEnabled = AppSettings.SimulationMode
                ? isLocal
                : isLocal && _viewModel.IsInitialized && _viewModel.IsHomed && !_viewModel.HasActiveAlarm;
            rbRemote.Enabled = remoteEnabled;

            // 탭 패널 활성화 (Local 모드에서만)
            motionPanel.Enabled = isLocal;
            ioPanel.Enabled = isLocal;
            visionPanel.Enabled = isLocal;
            eddyPanel.Enabled = isLocal;
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

        #region EventManager Handlers

        private void OnStatusMessageReceived(object data)
        {
            var message = data as string;
            if (!string.IsNullOrEmpty(message))
            {
                UpdateStatusMessage(message);
            }
        }

        private void OnAlarmOccurred(object data)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object>(OnAlarmOccurred), data);
                return;
            }

            // InterlockManager의 AlarmInfo 처리
            var interlockAlarm = data as Interlock.AlarmInfo;
            if (interlockAlarm != null)
            {
                // 알람 발생 시 UI 업데이트
                lblSystemStatus.Text = "Alarm";
                lblSystemStatus.ForeColor = Color.Red;
                UpdateStatusMessage($"알람 발생: [{interlockAlarm.Definition?.Code}] {interlockAlarm.Definition?.Name}");

                // 알람 표시등 활성화 및 깜빡임 시작
                UpdateAlarmIndicator();
                return;
            }

            // 기존 Models.AlarmInfo 처리 (하위 호환성)
            var alarm = data as Models.AlarmInfo;
            if (alarm != null)
            {
                lblSystemStatus.Text = "Alarm";
                lblSystemStatus.ForeColor = Color.Red;
                UpdateStatusMessage($"알람 발생: [{alarm.Code}] {alarm.Message}");

                // 알람 표시등 활성화 및 깜빡임 시작
                UpdateAlarmIndicator();
            }
        }

        private void OnAlarmCleared(object data)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object>(OnAlarmCleared), data);
                return;
            }

            // InterlockManager의 AlarmInfo 처리
            var interlockAlarm = data as Interlock.AlarmInfo;
            if (interlockAlarm != null)
            {
                UpdateStatusMessage($"알람 해제: [{interlockAlarm.Definition?.Code}]");
                UpdateAlarmIndicator();
                return;
            }

            // 기존 Models.AlarmInfo 처리 (하위 호환성)
            var alarm = data as Models.AlarmInfo;
            if (alarm != null)
            {
                UpdateStatusMessage($"알람 해제: [{alarm.Code}]");
                UpdateAlarmIndicator();
            }
        }

        private void UpdateAlarmIndicator()
        {
            bool hasAlarm = InterlockManager.Instance.HasActiveAlarm;

            if (hasAlarm)
            {
                // 알람 있으면 표시등 보이기 및 깜빡임 시작
                lblAlarmIndicator.Visible = true;
                if (!timerAlarmBlink.Enabled)
                {
                    timerAlarmBlink.Start();
                }
            }
            else
            {
                // 알람 없으면 표시등 숨기기 및 깜빡임 정지
                lblAlarmIndicator.Visible = false;
                timerAlarmBlink.Stop();
                _alarmBlinkState = false;
            }
        }

        private void OnControlAuthorityChanged(object data)
        {
            if (data is ControlAuthority authority)
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<object>(OnControlAuthorityChanged), data);
                    return;
                }

                // AppContext의 제어권 상태와 UI 동기화
                UpdateControlAuthorityDisplay();
                UpdateControlAuthorityRadioButtons();
            }
        }

        private void OnSystemStateChanged(object data)
        {
            var propertyName = data as string;
            if (!string.IsNullOrEmpty(propertyName))
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<object>(OnSystemStateChanged), data);
                    return;
                }

                // 시스템 상태 변경에 따른 UI 업데이트
                switch (propertyName)
                {
                    case nameof(Core.AppContext.SystemStatus):
                        lblSystemStatus.Text = Core.AppContext.Current.SystemStatus.ToString();
                        break;
                    case nameof(Core.AppContext.IsEmergencyStop):
                        if (Core.AppContext.Current.IsEmergencyStop)
                        {
                            lblSystemStatus.Text = "EMO";
                            lblSystemStatus.ForeColor = Color.Red;
                        }
                        break;
                }
            }
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

        private void rbLocal_CheckedChanged(object sender, EventArgs e)
        {
            // 프로그래밍적 업데이트 중에는 무시
            if (_isUpdatingRadioButtons) return;

            if (rbLocal.Checked && _viewModel != null)
            {
                _viewModel.SetLocalControlCommand?.Execute(null);
            }
        }

        private void rbRemote_CheckedChanged(object sender, EventArgs e)
        {
            // 프로그래밍적 업데이트 중에는 무시
            if (_isUpdatingRadioButtons) return;

            if (rbRemote.Checked && _viewModel != null)
            {
                _viewModel.SetRemoteControlCommand?.Execute(null);
            }
        }

        private void timerAlarmBlink_Tick(object sender, EventArgs e)
        {
            // 알람 표시등 깜빡임
            _alarmBlinkState = !_alarmBlinkState;

            if (_alarmBlinkState)
            {
                lblAlarmIndicator.BackColor = Color.Red;
                lblAlarmIndicator.ForeColor = Color.White;
            }
            else
            {
                lblAlarmIndicator.BackColor = Color.DarkRed;
                lblAlarmIndicator.ForeColor = Color.Yellow;
            }
        }

        private void lblAlarmIndicator_Click(object sender, EventArgs e)
        {
            // 알람 클릭 시 History > Alarm 탭으로 이동
            tabMain.SelectedTab = tabHistory;
            tabControlHistory.SelectedTab = tabAlarm;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 알람 깜빡임 타이머 정지
            timerAlarmBlink?.Stop();

            _updateTimer?.Stop();
            _updateTimer?.Dispose();

            // EventManager 구독 해제
            EventManager.Unsubscribe(EventManager.StatusMessage, OnStatusMessageReceived);
            EventManager.Unsubscribe(EventManager.AlarmOccurred, OnAlarmOccurred);
            EventManager.Unsubscribe(EventManager.AlarmCleared, OnAlarmCleared);
            EventManager.Unsubscribe(EventManager.ControlAuthorityChanged, OnControlAuthorityChanged);
            EventManager.Unsubscribe(EventManager.SystemStateChanged, OnSystemStateChanged);

            _viewModel?.Dispose();

            // VisionAlignerSystem이 내부적으로 모든 하드웨어 종료 처리 (CTC 포함)
            _system?.Dispose();

            // CTC 통신 종료 (System에서 처리되지 않은 경우 대비)
            _ctcController?.Dispose();

            // LogManager 종료
            LogManager.Shutdown();

            // AppContext 종료
            Core.AppContext.Current.Shutdown();
        }

        #endregion

    }
}
