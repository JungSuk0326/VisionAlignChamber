using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using VisionAlignChamber.ViewModels.Base;
using VisionAlignChamber.Core;
using VisionAlignChamber.Database;
using VisionAlignChamber.Interlock;

namespace VisionAlignChamber.ViewModels
{
    /// <summary>
    /// 알람 히스토리 ViewModel
    /// </summary>
    public class AlarmHistoryViewModel : ViewModelBase
    {
        #region Fields

        private List<AlarmRecord> _alarms;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _severityFilter;   // 0: 전체, 1: Critical, 2: Error, 3: Warning, 4: Info
        private int _categoryFilter;   // 0: 전체, 1~: AlarmCategory enum 값+1
        private int _statusFilter;     // 0: 전체, 1: Active, 2: Cleared

        #endregion

        #region Properties

        public List<AlarmRecord> Alarms
        {
            get => _alarms;
            set => SetProperty(ref _alarms, value);
        }

        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        public DateTime EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        public int SeverityFilter
        {
            get => _severityFilter;
            set => SetProperty(ref _severityFilter, value);
        }

        public int CategoryFilter
        {
            get => _categoryFilter;
            set => SetProperty(ref _categoryFilter, value);
        }

        public int StatusFilter
        {
            get => _statusFilter;
            set => SetProperty(ref _statusFilter, value);
        }

        #endregion

        #region Statistics Properties

        private int _totalCount;
        public int TotalCount
        {
            get => _totalCount;
            set => SetProperty(ref _totalCount, value);
        }

        private int _activeCount;
        public int ActiveCount
        {
            get => _activeCount;
            set => SetProperty(ref _activeCount, value);
        }

        #endregion

        #region Commands

        public ICommand SearchCommand { get; private set; }
        public ICommand TodayCommand { get; private set; }
        public ICommand WeekCommand { get; private set; }
        public ICommand MonthCommand { get; private set; }
        public ICommand AllCommand { get; private set; }
        public ICommand ClearSelectedCommand { get; private set; }
        public ICommand ClearAllCommand { get; private set; }

        #endregion

        #region Constructor

        public AlarmHistoryViewModel()
        {
            _startDate = DateTime.Today;
            _endDate = DateTime.Now;
            _severityFilter = 0;
            _categoryFilter = 0;
            _statusFilter = 0;
            _alarms = new List<AlarmRecord>();

            InitializeCommands();
            SubscribeEvents();
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
            ClearSelectedCommand = new RelayCommand<long>(ExecuteClearSelected);
            ClearAllCommand = new RelayCommand(ExecuteClearAll);
        }

        #endregion

        #region Event Subscription

        private void SubscribeEvents()
        {
            EventManager.Subscribe(EventManager.AlarmOccurred, OnAlarmChanged);
            EventManager.Subscribe(EventManager.AlarmCleared, OnAlarmChanged);
        }

        private void UnsubscribeEvents()
        {
            EventManager.Unsubscribe(EventManager.AlarmOccurred, OnAlarmChanged);
            EventManager.Unsubscribe(EventManager.AlarmCleared, OnAlarmChanged);
        }

        private void OnAlarmChanged(object obj)
        {
            ExecuteSearch();
        }

        #endregion

        #region Command Execution

        public void ExecuteSearch()
        {
            try
            {
                var endDateTime = EndDate.Date.AddDays(1).AddMilliseconds(-1);

                var allAlarms = AlarmRepository.Instance.GetByDateRange(StartDate, endDateTime).ToList();

                var filtered = allAlarms.AsEnumerable();

                // Severity 필터
                if (SeverityFilter > 0)
                {
                    var targetSeverity = SeverityFilter; // 1=Critical, 2=Error, 3=Warning, 4=Info
                    filtered = filtered.Where(a => a.Severity == targetSeverity);
                }

                // Category 필터
                if (CategoryFilter > 0)
                {
                    var targetCategory = CategoryFilter - 1; // ComboBox index 1 → enum 0
                    filtered = filtered.Where(a => a.Category == targetCategory);
                }

                // Status 필터
                if (StatusFilter == 1) // Active only
                    filtered = filtered.Where(a => a.IsCleared == 0);
                else if (StatusFilter == 2) // Cleared only
                    filtered = filtered.Where(a => a.IsCleared == 1);

                Alarms = filtered.ToList();
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AlarmHistoryViewModel] Search error: {ex.Message}");
                Alarms = new List<AlarmRecord>();
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
            StartDate = DateTime.MinValue.AddYears(2000);
            EndDate = DateTime.Now;
            ExecuteSearch();
        }

        private void ExecuteClearSelected(long interlockId)
        {
            try
            {
                InterlockManager.Instance.ClearAlarm((int)interlockId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AlarmHistoryViewModel] ClearSelected error: {ex.Message}");
            }
        }

        private void ExecuteClearAll()
        {
            try
            {
                var activeAlarms = InterlockManager.Instance.GetActiveAlarms();
                foreach (var alarm in activeAlarms)
                {
                    InterlockManager.Instance.ClearAlarm(alarm.Definition.Id);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AlarmHistoryViewModel] ClearAll error: {ex.Message}");
            }
        }

        #endregion

        #region Private Methods

        private void UpdateStatistics()
        {
            if (Alarms == null || Alarms.Count == 0)
            {
                TotalCount = 0;
                ActiveCount = 0;
                return;
            }

            TotalCount = Alarms.Count;
            ActiveCount = Alarms.Count(a => a.IsCleared == 0);
        }

        #endregion

        #region Dispose

        protected override void OnDisposing()
        {
            UnsubscribeEvents();
            Alarms?.Clear();
            base.OnDisposing();
        }

        #endregion
    }
}
