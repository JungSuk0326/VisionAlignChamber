using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using VisionAlignChamber.ViewModels.Base;
using VisionAlignChamber.Database;

namespace VisionAlignChamber.ViewModels
{
    /// <summary>
    /// 측정 결과 히스토리 ViewModel
    /// </summary>
    public class ResultHistoryViewModel : ViewModelBase
    {
        #region Fields

        private List<ResultHistoryEntity> _results;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _waferTypeFilter; // 0: 전체, 1: Notch, 2: Flat
        private bool _validOnlyFilter;

        #endregion

        #region Properties

        /// <summary>
        /// 조회 결과 목록
        /// </summary>
        public List<ResultHistoryEntity> Results
        {
            get => _results;
            set => SetProperty(ref _results, value);
        }

        /// <summary>
        /// 시작 날짜
        /// </summary>
        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        /// <summary>
        /// 종료 날짜
        /// </summary>
        public DateTime EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        /// <summary>
        /// 웨이퍼 타입 필터 (0: 전체, 1: Notch, 2: Flat)
        /// </summary>
        public int WaferTypeFilter
        {
            get => _waferTypeFilter;
            set => SetProperty(ref _waferTypeFilter, value);
        }

        /// <summary>
        /// 유효 결과만 필터
        /// </summary>
        public bool ValidOnlyFilter
        {
            get => _validOnlyFilter;
            set => SetProperty(ref _validOnlyFilter, value);
        }

        #endregion

        #region Statistics Properties

        private int _totalCount;
        public int TotalCount
        {
            get => _totalCount;
            set => SetProperty(ref _totalCount, value);
        }

        private int _validCount;
        public int ValidCount
        {
            get => _validCount;
            set => SetProperty(ref _validCount, value);
        }

        private double _validRate;
        public double ValidRate
        {
            get => _validRate;
            set => SetProperty(ref _validRate, value);
        }

        private double _averageOffset;
        public double AverageOffset
        {
            get => _averageOffset;
            set => SetProperty(ref _averageOffset, value);
        }

        #endregion

        #region Commands

        public ICommand SearchCommand { get; private set; }
        public ICommand TodayCommand { get; private set; }
        public ICommand WeekCommand { get; private set; }
        public ICommand MonthCommand { get; private set; }
        public ICommand AllCommand { get; private set; }

        #endregion

        #region Constructor

        public ResultHistoryViewModel()
        {
            // 기본값: 오늘
            _startDate = DateTime.Today;
            _endDate = DateTime.Now;
            _waferTypeFilter = 0;
            _validOnlyFilter = false;
            _results = new List<ResultHistoryEntity>();

            InitializeCommands();
        }

        #endregion

        #region Command Initialization

        private void InitializeCommands()
        {
            SearchCommand = new RelayCommand(ExecuteSearch);
            TodayCommand = new RelayCommand(SetToday);
            WeekCommand = new RelayCommand(SetWeek);
            MonthCommand = new RelayCommand(SetMonth);
            AllCommand = new RelayCommand(SetAll);
        }

        #endregion

        #region Command Execution

        /// <summary>
        /// 검색 실행
        /// </summary>
        public void ExecuteSearch()
        {
            try
            {
                // 종료일은 해당 일의 끝까지 포함
                var endDateTime = EndDate.Date.AddDays(1).AddMilliseconds(-1);

                // DB에서 기간별 조회
                var allResults = ResultRepository.Instance.GetByDateRange(StartDate, endDateTime).ToList();

                // 필터링 적용
                var filtered = allResults.AsEnumerable();

                // 웨이퍼 타입 필터
                if (WaferTypeFilter == 1) // Notch only
                    filtered = filtered.Where(r => r.WaferType == 0);
                else if (WaferTypeFilter == 2) // Flat only
                    filtered = filtered.Where(r => r.WaferType == 1);

                // 유효 결과만 필터
                if (ValidOnlyFilter)
                    filtered = filtered.Where(r => r.IsValid == 1);

                Results = filtered.ToList();

                // 통계 계산
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ResultHistoryViewModel] Search error: {ex.Message}");
                Results = new List<ResultHistoryEntity>();
            }
        }

        private void SetToday()
        {
            StartDate = DateTime.Today;
            EndDate = DateTime.Now;
            ExecuteSearch();
        }

        private void SetWeek()
        {
            StartDate = DateTime.Today.AddDays(-7);
            EndDate = DateTime.Now;
            ExecuteSearch();
        }

        private void SetMonth()
        {
            StartDate = DateTime.Today.AddDays(-30);
            EndDate = DateTime.Now;
            ExecuteSearch();
        }

        private void SetAll()
        {
            StartDate = DateTime.MinValue.AddYears(2000); // 2000년부터
            EndDate = DateTime.Now;
            ExecuteSearch();
        }

        #endregion

        #region Private Methods

        private void UpdateStatistics()
        {
            if (Results == null || Results.Count == 0)
            {
                TotalCount = 0;
                ValidCount = 0;
                ValidRate = 0;
                AverageOffset = 0;
                return;
            }

            TotalCount = Results.Count;
            ValidCount = Results.Count(r => r.IsValid == 1);
            ValidRate = TotalCount > 0 ? (double)ValidCount / TotalCount * 100 : 0;

            var validResults = Results.Where(r => r.IsValid == 1).ToList();
            AverageOffset = validResults.Count > 0 ? validResults.Average(r => r.TotalOffset) : 0;
        }

        #endregion

        #region Dispose

        protected override void OnDisposing()
        {
            Results?.Clear();
            base.OnDisposing();
        }

        #endregion
    }
}
