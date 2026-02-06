using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using VisionAlignChamber.ViewModels.Base;

namespace VisionAlignChamber.ViewModels
{
    /// <summary>
    /// 로그 항목
    /// </summary>
    public class LogEntry : INotifyPropertyChanged
    {
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }

        public string FormattedTime => Timestamp.ToString("HH:mm:ss.fff");
        public string FormattedDate => Timestamp.ToString("yyyy-MM-dd");

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"[{FormattedTime}] [{Level}] [{Logger}] {Message}";
        }
    }

    /// <summary>
    /// 로그 뷰모델
    /// </summary>
    public class LogViewModel : ViewModelBase
    {
        #region Fields

        private readonly object _lock = new object();
        private readonly List<LogEntry> _allLogs = new List<LogEntry>();
        private ObservableCollection<LogEntry> _filteredLogs;
        private const int MaxLogCount = 5000;

        // 필터 설정
        private bool _showSystem = true;
        private bool _showMotion = true;
        private bool _showIO = true;
        private bool _showVision = true;
        private bool _showAlarm = true;
        private bool _showSequence = true;
        private bool _showDB = true;
        private bool _showTrace = true;
        private bool _showOther = true;

        // 레벨 필터
        private bool _showDebug = true;
        private bool _showInfo = true;
        private bool _showWarn = true;
        private bool _showError = true;
        private bool _showFatal = true;

        // 검색
        private string _searchText = string.Empty;

        // 자동 스크롤
        private bool _autoScroll = true;

        #endregion

        #region Properties

        public ObservableCollection<LogEntry> FilteredLogs
        {
            get => _filteredLogs;
            private set => SetProperty(ref _filteredLogs, value);
        }

        public int TotalLogCount => _allLogs.Count;
        public int FilteredLogCount => _filteredLogs?.Count ?? 0;

        // 카테고리 필터
        public bool ShowSystem
        {
            get => _showSystem;
            set { if (SetProperty(ref _showSystem, value)) ApplyFilter(); }
        }

        public bool ShowMotion
        {
            get => _showMotion;
            set { if (SetProperty(ref _showMotion, value)) ApplyFilter(); }
        }

        public bool ShowIO
        {
            get => _showIO;
            set { if (SetProperty(ref _showIO, value)) ApplyFilter(); }
        }

        public bool ShowVision
        {
            get => _showVision;
            set { if (SetProperty(ref _showVision, value)) ApplyFilter(); }
        }

        public bool ShowAlarm
        {
            get => _showAlarm;
            set { if (SetProperty(ref _showAlarm, value)) ApplyFilter(); }
        }

        public bool ShowSequence
        {
            get => _showSequence;
            set { if (SetProperty(ref _showSequence, value)) ApplyFilter(); }
        }

        public bool ShowDB
        {
            get => _showDB;
            set { if (SetProperty(ref _showDB, value)) ApplyFilter(); }
        }

        public bool ShowTrace
        {
            get => _showTrace;
            set { if (SetProperty(ref _showTrace, value)) ApplyFilter(); }
        }

        public bool ShowOther
        {
            get => _showOther;
            set { if (SetProperty(ref _showOther, value)) ApplyFilter(); }
        }

        // 레벨 필터
        public bool ShowDebug
        {
            get => _showDebug;
            set { if (SetProperty(ref _showDebug, value)) ApplyFilter(); }
        }

        public bool ShowInfo
        {
            get => _showInfo;
            set { if (SetProperty(ref _showInfo, value)) ApplyFilter(); }
        }

        public bool ShowWarn
        {
            get => _showWarn;
            set { if (SetProperty(ref _showWarn, value)) ApplyFilter(); }
        }

        public bool ShowError
        {
            get => _showError;
            set { if (SetProperty(ref _showError, value)) ApplyFilter(); }
        }

        public bool ShowFatal
        {
            get => _showFatal;
            set { if (SetProperty(ref _showFatal, value)) ApplyFilter(); }
        }

        public string SearchText
        {
            get => _searchText;
            set { if (SetProperty(ref _searchText, value)) ApplyFilter(); }
        }

        public bool AutoScroll
        {
            get => _autoScroll;
            set => SetProperty(ref _autoScroll, value);
        }

        #endregion

        #region Commands

        public ICommand ClearLogsCommand { get; }
        public ICommand SelectAllCategoriesCommand { get; }
        public ICommand DeselectAllCategoriesCommand { get; }
        public ICommand SelectAllLevelsCommand { get; }
        public ICommand DeselectAllLevelsCommand { get; }

        #endregion

        #region Events

        /// <summary>
        /// 새 로그 추가 시 발생 (UI 스크롤용)
        /// </summary>
        public event EventHandler LogAdded;

        #endregion

        #region Constructor

        public LogViewModel()
        {
            _filteredLogs = new ObservableCollection<LogEntry>();

            ClearLogsCommand = new RelayCommand(() => ClearLogs());
            SelectAllCategoriesCommand = new RelayCommand(() => SelectAllCategories(true));
            DeselectAllCategoriesCommand = new RelayCommand(() => SelectAllCategories(false));
            SelectAllLevelsCommand = new RelayCommand(() => SelectAllLevels(true));
            DeselectAllLevelsCommand = new RelayCommand(() => SelectAllLevels(false));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 로그 추가
        /// </summary>
        public void AddLog(LogEntry entry)
        {
            if (entry == null) return;

            lock (_lock)
            {
                _allLogs.Add(entry);

                // 최대 개수 초과 시 오래된 것 제거
                while (_allLogs.Count > MaxLogCount)
                {
                    _allLogs.RemoveAt(0);
                }

                // 필터 통과하면 표시 목록에도 추가
                if (PassesFilter(entry))
                {
                    _filteredLogs.Add(entry);

                    // 표시 목록도 제한
                    while (_filteredLogs.Count > MaxLogCount)
                    {
                        _filteredLogs.RemoveAt(0);
                    }

                    LogAdded?.Invoke(this, EventArgs.Empty);
                }
            }

            OnPropertyChanged(nameof(TotalLogCount));
            OnPropertyChanged(nameof(FilteredLogCount));
        }

        /// <summary>
        /// 로그 추가 (간편 버전)
        /// </summary>
        public void AddLog(string level, string logger, string message, string exception = null)
        {
            AddLog(new LogEntry
            {
                Timestamp = DateTime.Now,
                Level = level,
                Logger = logger,
                Message = message,
                Exception = exception
            });
        }

        /// <summary>
        /// 로그 전체 삭제
        /// </summary>
        public void ClearLogs()
        {
            lock (_lock)
            {
                _allLogs.Clear();
                _filteredLogs.Clear();
            }

            OnPropertyChanged(nameof(TotalLogCount));
            OnPropertyChanged(nameof(FilteredLogCount));
        }

        #endregion

        #region Private Methods

        private void ApplyFilter()
        {
            lock (_lock)
            {
                _filteredLogs.Clear();

                foreach (var log in _allLogs)
                {
                    if (PassesFilter(log))
                    {
                        _filteredLogs.Add(log);
                    }
                }
            }

            OnPropertyChanged(nameof(FilteredLogCount));
        }

        private bool PassesFilter(LogEntry entry)
        {
            // 레벨 필터
            if (!PassesLevelFilter(entry.Level))
                return false;

            // 카테고리 필터
            if (!PassesCategoryFilter(entry.Logger))
                return false;

            // 검색어 필터
            if (!string.IsNullOrWhiteSpace(_searchText))
            {
                var searchLower = _searchText.ToLower();
                if (!entry.Message.ToLower().Contains(searchLower) &&
                    !entry.Logger.ToLower().Contains(searchLower))
                {
                    return false;
                }
            }

            return true;
        }

        private bool PassesLevelFilter(string level)
        {
            if (string.IsNullOrEmpty(level)) return _showOther;

            var levelUpper = level.ToUpper();
            switch (levelUpper)
            {
                case "DEBUG": return _showDebug;
                case "INFO": return _showInfo;
                case "WARN":
                case "WARNING": return _showWarn;
                case "ERROR": return _showError;
                case "FATAL": return _showFatal;
                default: return true;
            }
        }

        private bool PassesCategoryFilter(string logger)
        {
            if (string.IsNullOrEmpty(logger)) return _showOther;

            var loggerLower = logger.ToLower();

            if (loggerLower.Contains("system")) return _showSystem;
            if (loggerLower.Contains("motion")) return _showMotion;
            if (loggerLower.Contains("io")) return _showIO;
            if (loggerLower.Contains("vision")) return _showVision;
            if (loggerLower.Contains("alarm")) return _showAlarm;
            if (loggerLower.Contains("sequence")) return _showSequence;
            if (loggerLower.Contains("db") || loggerLower.Contains("database")) return _showDB;
            if (loggerLower.Contains("trace")) return _showTrace;

            return _showOther;
        }

        private void SelectAllCategories(bool selected)
        {
            _showSystem = selected;
            _showMotion = selected;
            _showIO = selected;
            _showVision = selected;
            _showAlarm = selected;
            _showSequence = selected;
            _showDB = selected;
            _showTrace = selected;
            _showOther = selected;

            OnPropertyChanged(nameof(ShowSystem));
            OnPropertyChanged(nameof(ShowMotion));
            OnPropertyChanged(nameof(ShowIO));
            OnPropertyChanged(nameof(ShowVision));
            OnPropertyChanged(nameof(ShowAlarm));
            OnPropertyChanged(nameof(ShowSequence));
            OnPropertyChanged(nameof(ShowDB));
            OnPropertyChanged(nameof(ShowTrace));
            OnPropertyChanged(nameof(ShowOther));

            ApplyFilter();
        }

        private void SelectAllLevels(bool selected)
        {
            _showDebug = selected;
            _showInfo = selected;
            _showWarn = selected;
            _showError = selected;
            _showFatal = selected;

            OnPropertyChanged(nameof(ShowDebug));
            OnPropertyChanged(nameof(ShowInfo));
            OnPropertyChanged(nameof(ShowWarn));
            OnPropertyChanged(nameof(ShowError));
            OnPropertyChanged(nameof(ShowFatal));

            ApplyFilter();
        }

        #endregion
    }
}
