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

            // 버튼 상태
            btnInitialize.Enabled = !_viewModel.IsInitialized;
            btnLoadImages.Enabled = _viewModel.IsInitialized && !_viewModel.IsInspecting;
            btnClearImages.Enabled = _viewModel.IsInitialized && _viewModel.ImageCount > 0;
            btnExecute.Enabled = _viewModel.IsInitialized && !_viewModel.IsInspecting && _viewModel.ImageCount > 0;

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
