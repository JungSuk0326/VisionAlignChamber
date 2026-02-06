using System;
using System.Drawing;
using System.Windows.Forms;
using VisionAlignChamber.ViewModels;

namespace VisionAlignChamber.Views.Controls
{
    /// <summary>
    /// 실시간 로그 뷰어 패널
    /// </summary>
    public partial class LogPanel : UserControl
    {
        #region Fields

        private LogViewModel _viewModel;

        #endregion

        #region Constructor

        public LogPanel()
        {
            InitializeComponent();
            SetupListView();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 뷰모델 바인딩
        /// </summary>
        public void BindViewModel(LogViewModel viewModel)
        {
            _viewModel = viewModel;

            if (_viewModel == null) return;

            // 체크박스 바인딩 - 카테고리
            chkSystem.Checked = _viewModel.ShowSystem;
            chkMotion.Checked = _viewModel.ShowMotion;
            chkIO.Checked = _viewModel.ShowIO;
            chkVision.Checked = _viewModel.ShowVision;
            chkAlarm.Checked = _viewModel.ShowAlarm;
            chkSequence.Checked = _viewModel.ShowSequence;
            chkDB.Checked = _viewModel.ShowDB;
            chkTrace.Checked = _viewModel.ShowTrace;
            chkOther.Checked = _viewModel.ShowOther;

            // 체크박스 바인딩 - 레벨
            chkDebug.Checked = _viewModel.ShowDebug;
            chkInfo.Checked = _viewModel.ShowInfo;
            chkWarn.Checked = _viewModel.ShowWarn;
            chkError.Checked = _viewModel.ShowError;
            chkFatal.Checked = _viewModel.ShowFatal;

            // 자동 스크롤
            chkAutoScroll.Checked = _viewModel.AutoScroll;

            // 이벤트 구독
            _viewModel.LogAdded += OnLogAdded;
            _viewModel.PropertyChanged += OnViewModelPropertyChanged;

            // 기존 로그 로드
            RefreshLogList();
        }

        #endregion

        #region Private Methods

        private void SetupListView()
        {
            listViewLogs.View = View.Details;
            listViewLogs.FullRowSelect = true;
            listViewLogs.GridLines = true;
            listViewLogs.VirtualMode = true;
            listViewLogs.VirtualListSize = 0;

            // 더블 버퍼링으로 깜빡임 방지
            listViewLogs.GetType().GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(listViewLogs, true, null);
        }

        private void RefreshLogList()
        {
            if (_viewModel?.FilteredLogs == null) return;

            listViewLogs.VirtualListSize = _viewModel.FilteredLogs.Count;
            listViewLogs.Invalidate();

            UpdateStatusLabel();

            if (_viewModel.AutoScroll && listViewLogs.VirtualListSize > 0)
            {
                listViewLogs.EnsureVisible(listViewLogs.VirtualListSize - 1);
            }
        }

        private void UpdateStatusLabel()
        {
            if (_viewModel == null) return;

            lblStatus.Text = $"Total: {_viewModel.TotalLogCount} | Filtered: {_viewModel.FilteredLogCount}";
        }

        private Color GetLevelColor(string level)
        {
            if (string.IsNullOrEmpty(level)) return Color.White;

            switch (level.ToUpper())
            {
                case "DEBUG": return Color.FromArgb(245, 245, 245);
                case "INFO": return Color.FromArgb(220, 255, 220);
                case "WARN":
                case "WARNING": return Color.FromArgb(255, 255, 200);
                case "ERROR": return Color.FromArgb(255, 220, 220);
                case "FATAL": return Color.FromArgb(255, 180, 180);
                default: return Color.White;
            }
        }

        private Color GetLevelForeColor(string level)
        {
            if (string.IsNullOrEmpty(level)) return Color.Black;

            switch (level.ToUpper())
            {
                case "DEBUG": return Color.Gray;
                case "INFO": return Color.DarkGreen;
                case "WARN":
                case "WARNING": return Color.DarkOrange;
                case "ERROR": return Color.Red;
                case "FATAL": return Color.DarkRed;
                default: return Color.Black;
            }
        }

        #endregion

        #region Event Handlers

        private void OnLogAdded(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnLogAdded(sender, e)));
                return;
            }

            RefreshLogList();
        }

        private void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnViewModelPropertyChanged(sender, e)));
                return;
            }

            if (e.PropertyName == nameof(LogViewModel.FilteredLogCount) ||
                e.PropertyName == nameof(LogViewModel.TotalLogCount))
            {
                RefreshLogList();
            }
        }

        private void listViewLogs_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (_viewModel?.FilteredLogs == null || e.ItemIndex >= _viewModel.FilteredLogs.Count)
            {
                e.Item = new ListViewItem(new string[] { "", "", "", "" });
                return;
            }

            var log = _viewModel.FilteredLogs[e.ItemIndex];
            var item = new ListViewItem(new string[]
            {
                log.FormattedTime,
                log.Level,
                log.Logger,
                log.Message
            });

            item.BackColor = GetLevelColor(log.Level);
            item.ForeColor = GetLevelForeColor(log.Level);

            e.Item = item;
        }

        // 카테고리 필터 이벤트
        private void chkSystem_CheckedChanged(object sender, EventArgs e)
        {
            if (_viewModel != null) _viewModel.ShowSystem = chkSystem.Checked;
        }

        private void chkMotion_CheckedChanged(object sender, EventArgs e)
        {
            if (_viewModel != null) _viewModel.ShowMotion = chkMotion.Checked;
        }

        private void chkIO_CheckedChanged(object sender, EventArgs e)
        {
            if (_viewModel != null) _viewModel.ShowIO = chkIO.Checked;
        }

        private void chkVision_CheckedChanged(object sender, EventArgs e)
        {
            if (_viewModel != null) _viewModel.ShowVision = chkVision.Checked;
        }

        private void chkAlarm_CheckedChanged(object sender, EventArgs e)
        {
            if (_viewModel != null) _viewModel.ShowAlarm = chkAlarm.Checked;
        }

        private void chkSequence_CheckedChanged(object sender, EventArgs e)
        {
            if (_viewModel != null) _viewModel.ShowSequence = chkSequence.Checked;
        }

        private void chkDB_CheckedChanged(object sender, EventArgs e)
        {
            if (_viewModel != null) _viewModel.ShowDB = chkDB.Checked;
        }

        private void chkTrace_CheckedChanged(object sender, EventArgs e)
        {
            if (_viewModel != null) _viewModel.ShowTrace = chkTrace.Checked;
        }

        private void chkOther_CheckedChanged(object sender, EventArgs e)
        {
            if (_viewModel != null) _viewModel.ShowOther = chkOther.Checked;
        }

        // 레벨 필터 이벤트
        private void chkDebug_CheckedChanged(object sender, EventArgs e)
        {
            if (_viewModel != null) _viewModel.ShowDebug = chkDebug.Checked;
        }

        private void chkInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (_viewModel != null) _viewModel.ShowInfo = chkInfo.Checked;
        }

        private void chkWarn_CheckedChanged(object sender, EventArgs e)
        {
            if (_viewModel != null) _viewModel.ShowWarn = chkWarn.Checked;
        }

        private void chkError_CheckedChanged(object sender, EventArgs e)
        {
            if (_viewModel != null) _viewModel.ShowError = chkError.Checked;
        }

        private void chkFatal_CheckedChanged(object sender, EventArgs e)
        {
            if (_viewModel != null) _viewModel.ShowFatal = chkFatal.Checked;
        }

        // 자동 스크롤
        private void chkAutoScroll_CheckedChanged(object sender, EventArgs e)
        {
            if (_viewModel != null) _viewModel.AutoScroll = chkAutoScroll.Checked;
        }

        // 검색
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (_viewModel != null) _viewModel.SearchText = txtSearch.Text;
        }

        // 버튼 이벤트
        private void btnClear_Click(object sender, EventArgs e)
        {
            _viewModel?.ClearLogsCommand?.Execute(null);
        }

        private void btnSelectAllCategories_Click(object sender, EventArgs e)
        {
            _viewModel?.SelectAllCategoriesCommand?.Execute(null);
            UpdateCategoryCheckboxes();
        }

        private void btnDeselectAllCategories_Click(object sender, EventArgs e)
        {
            _viewModel?.DeselectAllCategoriesCommand?.Execute(null);
            UpdateCategoryCheckboxes();
        }

        private void UpdateCategoryCheckboxes()
        {
            if (_viewModel == null) return;

            chkSystem.Checked = _viewModel.ShowSystem;
            chkMotion.Checked = _viewModel.ShowMotion;
            chkIO.Checked = _viewModel.ShowIO;
            chkVision.Checked = _viewModel.ShowVision;
            chkAlarm.Checked = _viewModel.ShowAlarm;
            chkSequence.Checked = _viewModel.ShowSequence;
            chkDB.Checked = _viewModel.ShowDB;
            chkTrace.Checked = _viewModel.ShowTrace;
            chkOther.Checked = _viewModel.ShowOther;
        }

        #endregion
    }
}
