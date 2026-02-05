using System;
using System.Drawing;
using System.Windows.Forms;
using VisionAlignChamber.ViewModels;

namespace VisionAlignChamber.Views.Controls
{
    /// <summary>
    /// Vision 제어 패널
    /// </summary>
    public partial class VisionPanel : UserControl
    {
        #region Fields

        private VisionViewModel _viewModel;
        private Timer _updateTimer;
        private int _resultCounter;
        private object _lastResultKey;  // To detect new results
        private const int MaxHistoryCount = 100;

        #endregion

        #region Constructor

        public VisionPanel()
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
        public void BindViewModel(VisionViewModel viewModel)
        {
            _viewModel = viewModel;

            if (_viewModel == null) return;

            // Initialize Deg value
            UpdateDegValue();

            _updateTimer.Start();
        }

        #endregion

        #region Timer

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (_viewModel == null) return;

            UpdateStatus();
            UpdateResult();
        }

        private void UpdateStatus()
        {
            // 상태 표시
            lblInitStatus.Text = _viewModel.IsInitialized ? "Initialized" : "Not Initialized";
            lblInitStatus.ForeColor = _viewModel.IsInitialized ?
                Color.LimeGreen : Color.Gray;

            lblImageCount.Text = _viewModel.ImageCount.ToString();

            // 검사 모드
            rdoNotch.Checked = _viewModel.IsNotchMode;
            rdoFlat.Checked = _viewModel.IsFlatMode;

            // Running 섹션 업데이트
            txtRunCnt.Text = _viewModel.RunCount.ToString();
            txtRunStep.Text = _viewModel.RunStep.ToString();

            // Running 버튼 상태
            bool isRunning = _viewModel.IsRunning;
            btnRun.Enabled = _viewModel.IsInitialized && !isRunning;
            btnStop.Enabled = isRunning;
            numCount.Enabled = !isRunning;

            // Setting 버튼 상태
            btnGrabberActive.Enabled = _viewModel.IsCameraOpened;
            btnTrigger.Enabled = _viewModel.IsCameraOpened && _viewModel.IsGrabberActive;
            btnFileSave.Enabled = _viewModel.IsCameraOpened;

            // Setting 버튼 텍스트 동기화
            btnCamOpen.Text = _viewModel.IsCameraOpened ? "Close" : "Open";
            btnGrabberActive.Text = _viewModel.IsGrabberActive ? "Idle" : "Active";

            // 버튼 상태
            btnInitialize.Enabled = !_viewModel.IsInitialized;
            btnLoadImages.Enabled = _viewModel.IsInitialized && !_viewModel.IsInspecting && !isRunning;
            btnClearImages.Enabled = _viewModel.IsInitialized && _viewModel.ImageCount > 0 && !isRunning;
            btnExecute.Enabled = _viewModel.IsInitialized && !_viewModel.IsInspecting && _viewModel.ImageCount > 0 && !isRunning;

            // 상태 메시지
            lblStatusMessage.Text = _viewModel.StatusMessage ?? "";
        }

        private void UpdateResult()
        {
            var result = _viewModel.AlignResult;

            if (!result.IsValid)
                return;

            // Create a key to detect if this is a new result
            var currentKey = $"{result.Index1st}_{result.Index2nd}_{result.OffAngle}_{result.AbsAngle}";
            if (currentKey.Equals(_lastResultKey))
                return;

            _lastResultKey = currentKey;
            _resultCounter++;

            // Add new result row at the top
            var item = new ListViewItem(_resultCounter.ToString());
            item.SubItems.Add(result.Index1st.ToString());
            item.SubItems.Add(result.Index2nd.ToString());
            item.SubItems.Add(result.OffAngle.ToString("F3"));
            item.SubItems.Add(result.AbsAngle.ToString("F3"));
            item.SubItems.Add(result.Width.ToString("F3"));
            item.SubItems.Add(result.Height.ToString("F3"));
            item.SubItems.Add(result.Wafer.CenterX.ToString("F3"));
            item.SubItems.Add(result.Wafer.CenterY.ToString("F3"));
            item.SubItems.Add(result.Wafer.Radius.ToString("F3"));

            // Alternate row color for readability
            if (_resultCounter % 2 == 0)
            {
                item.BackColor = Color.LightBlue;
            }

            listResult.Items.Insert(0, item);

            // Limit history count
            while (listResult.Items.Count > MaxHistoryCount)
            {
                listResult.Items.RemoveAt(listResult.Items.Count - 1);
            }
        }

        #endregion

        #region Event Handlers

        private void numCount_ValueChanged(object sender, EventArgs e)
        {
            UpdateDegValue();
            _viewModel?.SetRunningCount((int)numCount.Value);
        }

        private void UpdateDegValue()
        {
            int count = (int)numCount.Value;
            if (count > 0)
            {
                double deg = 360.0 / count;
                txtDeg.Text = deg.ToString("F2");
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            _viewModel?.StartRunCommand?.Execute(null);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _viewModel?.StopRunCommand?.Execute(null);
        }

        private void btnCamOpen_Click(object sender, EventArgs e)
        {
            if (_viewModel == null) return;

            if (_viewModel.IsCameraOpened)
            {
                // Close camera
                _viewModel.CloseCameraCommand?.Execute(null);
                btnCamOpen.Text = "Open";
            }
            else
            {
                // Open camera with file dialog
                using (var dialog = new OpenFileDialog())
                {
                    dialog.Title = "Cam 파일 선택";
                    dialog.Filter = "CAM Files (*.cam)|*.cam|All Files (*.*)|*.*";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        _viewModel.OpenCameraCommand?.Execute(dialog.FileName);
                        if (_viewModel.IsCameraOpened)
                        {
                            btnCamOpen.Text = "Close";
                            lblCamFile.Text = System.IO.Path.GetFileName(dialog.FileName);
                            lblCamFile.ForeColor = Color.LimeGreen;
                        }
                    }
                }
            }
        }

        private void btnGrabberActive_Click(object sender, EventArgs e)
        {
            if (_viewModel == null) return;

            if (_viewModel.IsGrabberActive)
            {
                _viewModel.DeactivateGrabberCommand?.Execute(null);
                btnGrabberActive.Text = "Active";
            }
            else
            {
                _viewModel.ActivateGrabberCommand?.Execute(null);
                btnGrabberActive.Text = "Idle";
            }
        }

        private void btnTrigger_Click(object sender, EventArgs e)
        {
            _viewModel?.TriggerCommand?.Execute(null);
        }

        private void btnFileSave_Click(object sender, EventArgs e)
        {
            if (_viewModel == null) return;

            using (var dialog = new SaveFileDialog())
            {
                dialog.Title = "이미지 저장";
                dialog.Filter = "JPEG Image (*.jpg)|*.jpg|PNG Image (*.png)|*.png|Bitmap Image (*.bmp)|*.bmp";
                dialog.DefaultExt = "jpg";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _viewModel.SaveImageCommand?.Execute(dialog.FileName);
                }
            }
        }

        private void btnInitialize_Click(object sender, EventArgs e)
        {
            _viewModel?.InitializeCommand?.Execute(null);
        }

        private void btnLoadImages_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "이미지 폴더를 선택하세요";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _viewModel?.LoadImagesFromFolder(dialog.SelectedPath);
                }
            }
        }

        private void btnClearImages_Click(object sender, EventArgs e)
        {
            _viewModel?.ClearImagesCommand?.Execute(null);
            ClearResultHistory();
        }

        private void ClearResultHistory()
        {
            listResult.Items.Clear();
            _resultCounter = 0;
            _lastResultKey = null;
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            _viewModel?.ExecuteInspectionCommand?.Execute(null);
        }

        private void rdoNotch_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoNotch.Checked && _viewModel != null)
            {
                _viewModel.IsNotchMode = true;
            }
        }

        private void rdoFlat_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoFlat.Checked && _viewModel != null)
            {
                _viewModel.IsFlatMode = true;
            }
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
