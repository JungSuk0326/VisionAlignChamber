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

        // Result item names (same as WaferAlign)
        private static readonly string[] ResultItemNames = new string[]
        {
            "Index1st",
            "Index2nd",
            "OffAngle",
            "AbsAngle",
            "Width",
            "Height",
            "CenterX",
            "CenterY",
            "Radius"
        };

        #endregion

        #region Constructor

        public VisionPanel()
        {
            InitializeComponent();
            InitializeTimer();
            InitializeResultList();
        }

        #endregion

        #region Initialization

        private void InitializeTimer()
        {
            _updateTimer = new Timer();
            _updateTimer.Interval = 100;
            _updateTimer.Tick += UpdateTimer_Tick;
        }

        private void InitializeResultList()
        {
            listResult.Items.Clear();

            foreach (var name in ResultItemNames)
            {
                var item = new ListViewItem(name);
                item.SubItems.Add("-");
                item.SubItems[1].BackColor = Color.LightBlue;
                item.UseItemStyleForSubItems = false;
                listResult.Items.Add(item);
            }
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
            {
                // Clear all values
                for (int i = 0; i < listResult.Items.Count; i++)
                {
                    listResult.Items[i].SubItems[1].Text = "-";
                }
                return;
            }

            // Update all result items (same format as WaferAlign: F3 for doubles)
            listResult.Items[0].SubItems[1].Text = result.Index1st.ToString();      // Index1st
            listResult.Items[1].SubItems[1].Text = result.Index2nd.ToString();      // Index2nd
            listResult.Items[2].SubItems[1].Text = result.OffAngle.ToString("F3");  // OffAngle
            listResult.Items[3].SubItems[1].Text = result.AbsAngle.ToString("F3");  // AbsAngle
            listResult.Items[4].SubItems[1].Text = result.Width.ToString("F3");     // Width
            listResult.Items[5].SubItems[1].Text = result.Height.ToString("F3");    // Height
            listResult.Items[6].SubItems[1].Text = result.Wafer.CenterX.ToString("F3"); // CenterX
            listResult.Items[7].SubItems[1].Text = result.Wafer.CenterY.ToString("F3"); // CenterY
            listResult.Items[8].SubItems[1].Text = result.Wafer.Radius.ToString("F3");  // Radius
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
