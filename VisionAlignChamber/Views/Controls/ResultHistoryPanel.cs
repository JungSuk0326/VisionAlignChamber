using System;
using System.Drawing;
using System.Windows.Forms;
using VisionAlignChamber.ViewModels;
using VisionAlignChamber.Database;

namespace VisionAlignChamber.Views.Controls
{
    /// <summary>
    /// 측정 결과 히스토리 패널
    /// </summary>
    public partial class ResultHistoryPanel : UserControl
    {
        #region Fields

        private ResultHistoryViewModel _viewModel;

        #endregion

        #region Constructor

        public ResultHistoryPanel()
        {
            InitializeComponent();
            SetupDataGridView();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// ViewModel 바인딩
        /// </summary>
        public void BindViewModel(ResultHistoryViewModel viewModel)
        {
            _viewModel = viewModel;

            if (_viewModel != null)
            {
                // 이벤트 구독
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

                // 라디오 버튼 이벤트
                rbAll.CheckedChanged += OnWaferTypeChanged;
                rbNotch.CheckedChanged += OnWaferTypeChanged;
                rbFlat.CheckedChanged += OnWaferTypeChanged;

                // 체크박스 이벤트
                chkValidOnly.CheckedChanged += (s, e) =>
                {
                    _viewModel.ValidOnlyFilter = chkValidOnly.Checked;
                };

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
                case nameof(ResultHistoryViewModel.Results):
                    RefreshDataGrid();
                    break;

                case nameof(ResultHistoryViewModel.StartDate):
                    dtpStartDate.Value = _viewModel.StartDate;
                    break;

                case nameof(ResultHistoryViewModel.EndDate):
                    dtpEndDate.Value = _viewModel.EndDate;
                    break;

                case nameof(ResultHistoryViewModel.TotalCount):
                    lblTotalValue.Text = _viewModel.TotalCount.ToString("N0");
                    break;

                case nameof(ResultHistoryViewModel.ValidCount):
                    lblValidValue.Text = _viewModel.ValidCount.ToString("N0");
                    break;

                case nameof(ResultHistoryViewModel.ValidRate):
                    lblRateValue.Text = $"{_viewModel.ValidRate:F1} %";
                    break;

                case nameof(ResultHistoryViewModel.AverageOffset):
                    lblAvgOffsetValue.Text = $"{_viewModel.AverageOffset:F3} mm";
                    break;
            }
        }

        private void OnWaferTypeChanged(object sender, EventArgs e)
        {
            if (_viewModel == null) return;

            if (rbAll.Checked)
                _viewModel.WaferTypeFilter = 0;
            else if (rbNotch.Checked)
                _viewModel.WaferTypeFilter = 1;
            else if (rbFlat.Checked)
                _viewModel.WaferTypeFilter = 2;
        }

        #endregion

        #region Private Methods

        private void SetupDataGridView()
        {
            // 다크 테마 설정
            dgvResults.EnableHeadersVisualStyles = false;
            dgvResults.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            dgvResults.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvResults.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvResults.DefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
            dgvResults.DefaultCellStyle.ForeColor = Color.White;
            dgvResults.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 122, 204);
            dgvResults.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvResults.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 55);
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
            if (_viewModel?.Results == null)
            {
                dgvResults.Rows.Clear();
                return;
            }

            dgvResults.Rows.Clear();

            int rowNum = 1;
            foreach (var result in _viewModel.Results)
            {
                var row = new object[]
                {
                    rowNum++,
                    result.MeasuredTime,
                    result.WaferTypeText,
                    result.IsValidBool ? "O" : "X",
                    result.AbsAngle.ToString("F3"),
                    result.TotalOffset.ToString("F3"),
                    result.CenterX.ToString("F3"),
                    result.CenterY.ToString("F3"),
                    result.EddyValue.ToString("F2"),
                    result.PNText
                };

                int rowIndex = dgvResults.Rows.Add(row);

                // Valid 여부에 따라 색상 지정
                if (!result.IsValidBool)
                {
                    dgvResults.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Gray;
                }

                // P/N 색상
                dgvResults.Rows[rowIndex].Cells[colPN.Index].Style.ForeColor =
                    result.PNValue == 1 ? Color.Lime : Color.Orange;
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
