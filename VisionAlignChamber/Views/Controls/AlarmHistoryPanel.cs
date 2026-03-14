using System;
using System.Drawing;
using System.Windows.Forms;
using VisionAlignChamber.ViewModels;
using VisionAlignChamber.Database;

namespace VisionAlignChamber.Views.Controls
{
    /// <summary>
    /// 알람 히스토리 패널
    /// </summary>
    public partial class AlarmHistoryPanel : UserControl
    {
        #region Fields

        private AlarmHistoryViewModel _viewModel;

        #endregion

        #region Constructor

        public AlarmHistoryPanel()
        {
            InitializeComponent();
            SetupDataGridView();
            SetupComboBoxes();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// ViewModel 바인딩
        /// </summary>
        public void BindViewModel(AlarmHistoryViewModel viewModel)
        {
            _viewModel = viewModel;

            if (_viewModel != null)
            {
                _viewModel.PropertyChanged += OnViewModelPropertyChanged;

                // 초기값 설정
                dtpStartDate.Value = _viewModel.StartDate;
                dtpEndDate.Value = _viewModel.EndDate;

                // 버튼 이벤트 연결
                btnSearch.Click += (s, e) => SyncFiltersToViewModel();
                btnToday.Click += (s, e) => _viewModel.TodayCommand.Execute(null);
                btnWeek.Click += (s, e) => _viewModel.WeekCommand.Execute(null);
                btnMonth.Click += (s, e) => _viewModel.MonthCommand.Execute(null);
                btnAll.Click += (s, e) => _viewModel.AllCommand.Execute(null);

                // 콤보박스 이벤트
                cboSeverity.SelectedIndexChanged += (s, e) =>
                {
                    _viewModel.SeverityFilter = cboSeverity.SelectedIndex;
                };
                cboCategory.SelectedIndexChanged += (s, e) =>
                {
                    _viewModel.CategoryFilter = cboCategory.SelectedIndex;
                };
                cboStatus.SelectedIndexChanged += (s, e) =>
                {
                    _viewModel.StatusFilter = cboStatus.SelectedIndex;
                };

                // Clear 버튼
                btnClearSelected.Click += OnClearSelectedClick;
                btnClearAll.Click += OnClearAllClick;

                // 초기 조회 (오늘)
                _viewModel.ExecuteSearch();
            }
        }

        #endregion

        #region Event Handlers

        private void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnViewModelPropertyChanged(sender, e)));
                return;
            }

            switch (e.PropertyName)
            {
                case nameof(AlarmHistoryViewModel.Alarms):
                    RefreshDataGrid();
                    break;

                case nameof(AlarmHistoryViewModel.StartDate):
                    dtpStartDate.Value = _viewModel.StartDate;
                    break;

                case nameof(AlarmHistoryViewModel.EndDate):
                    dtpEndDate.Value = _viewModel.EndDate;
                    break;

                case nameof(AlarmHistoryViewModel.TotalCount):
                    lblTotalValue.Text = _viewModel.TotalCount.ToString("N0");
                    break;

                case nameof(AlarmHistoryViewModel.ActiveCount):
                    lblActiveValue.Text = _viewModel.ActiveCount.ToString("N0");
                    lblActiveValue.ForeColor = _viewModel.ActiveCount > 0 ? Color.OrangeRed : Color.LightGreen;
                    break;
            }
        }

        private void OnClearSelectedClick(object sender, EventArgs e)
        {
            if (_viewModel == null) return;

            if (dgvAlarms.SelectedRows.Count == 0)
            {
                MessageBox.Show("Clear할 알람을 선택하세요.", "알림",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dgvAlarms.SelectedRows[0];
            var interlockId = (int)row.Tag;
            var status = row.Cells[colStatus.Index].Value?.ToString();

            if (status == "Cleared")
            {
                MessageBox.Show("이미 해제된 알람입니다.", "알림",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _viewModel.ClearSelectedCommand.Execute((long)interlockId);
        }

        private void OnClearAllClick(object sender, EventArgs e)
        {
            if (_viewModel == null) return;

            if (_viewModel.ActiveCount == 0)
            {
                MessageBox.Show("활성 알람이 없습니다.", "알림",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                $"활성 알람 {_viewModel.ActiveCount}건을 모두 Clear 하시겠습니까?",
                "전체 Clear 확인",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                _viewModel.ClearAllCommand.Execute(null);
            }
        }

        #endregion

        #region Private Methods

        private void SetupDataGridView()
        {
            dgvAlarms.EnableHeadersVisualStyles = false;
            dgvAlarms.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            dgvAlarms.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvAlarms.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvAlarms.DefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
            dgvAlarms.DefaultCellStyle.ForeColor = Color.White;
            dgvAlarms.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 122, 204);
            dgvAlarms.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvAlarms.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 55);
        }

        private void SetupComboBoxes()
        {
            // Severity
            cboSeverity.Items.AddRange(new object[]
            {
                "전체", "Critical", "Error", "Warning", "Info"
            });
            cboSeverity.SelectedIndex = 0;

            // Category
            cboCategory.Items.AddRange(new object[]
            {
                "전체", "System", "Motion", "Vision", "Sensor", "IO", "Communication", "Sequence", "User"
            });
            cboCategory.SelectedIndex = 0;

            // Status
            cboStatus.Items.AddRange(new object[]
            {
                "전체", "Active", "Cleared"
            });
            cboStatus.SelectedIndex = 0;
        }

        private void SyncFiltersToViewModel()
        {
            if (_viewModel == null) return;

            _viewModel.StartDate = dtpStartDate.Value;
            _viewModel.EndDate = dtpEndDate.Value;
            _viewModel.SearchCommand.Execute(null);
        }

        private void RefreshDataGrid()
        {
            if (_viewModel?.Alarms == null)
            {
                dgvAlarms.Rows.Clear();
                return;
            }

            dgvAlarms.Rows.Clear();

            int rowNum = 1;
            foreach (var alarm in _viewModel.Alarms)
            {
                string status = alarm.IsClearedBool ? "Cleared" : "Active";
                string clearedTime = alarm.IsClearedBool ? alarm.ClearedTime : "-";

                var row = new object[]
                {
                    rowNum++,
                    alarm.Code,
                    alarm.Name,
                    alarm.SeverityEnum.ToString(),
                    alarm.CategoryEnum.ToString(),
                    alarm.OccurredTime,
                    clearedTime,
                    status,
                    alarm.Source
                };

                int rowIndex = dgvAlarms.Rows.Add(row);

                // InterlockId를 Tag에 저장 (Clear 시 사용)
                dgvAlarms.Rows[rowIndex].Tag = alarm.InterlockId;

                // Active 알람 강조
                if (!alarm.IsClearedBool)
                {
                    dgvAlarms.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(80, 30, 30);
                }

                // Severity별 색상
                var severityCell = dgvAlarms.Rows[rowIndex].Cells[colSeverity.Index];
                switch (alarm.SeverityEnum)
                {
                    case Interlock.AlarmSeverity.Critical:
                        severityCell.Style.ForeColor = Color.Red;
                        break;
                    case Interlock.AlarmSeverity.Error:
                        severityCell.Style.ForeColor = Color.OrangeRed;
                        break;
                    case Interlock.AlarmSeverity.Warning:
                        severityCell.Style.ForeColor = Color.Yellow;
                        break;
                    case Interlock.AlarmSeverity.Info:
                        severityCell.Style.ForeColor = Color.LightBlue;
                        break;
                }
            }
        }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_viewModel != null)
                {
                    _viewModel.PropertyChanged -= OnViewModelPropertyChanged;
                }

                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
