using System;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Targets;
using VisionAlignChamber.ViewModels;

namespace VisionAlignChamber.Log
{
    /// <summary>
    /// UI 로그 표시용 커스텀 타겟
    /// </summary>
    [Target("UITarget")]
    public class UILogTarget : TargetWithLayout
    {
        /// <summary>
        /// 로그 수신 이벤트
        /// </summary>
        public static event Action<LogEntry> LogReceived;

        protected override void Write(LogEventInfo logEvent)
        {
            var entry = new LogEntry
            {
                Timestamp = logEvent.TimeStamp,
                Level = logEvent.Level.Name.ToUpper(),
                Logger = logEvent.LoggerName,
                Message = logEvent.FormattedMessage,
                Exception = logEvent.Exception?.ToString()
            };

            LogReceived?.Invoke(entry);
        }
    }

    /// <summary>
    /// NLog 기반 로그 관리자
    /// 시스템 전역에서 사용할 수 있는 로깅 유틸리티
    /// </summary>
    public static class LogManager
    {
        #region Fields

        private static bool _isInitialized;
        private static readonly object _lock = new object();
        private static LogViewModel _logViewModel;
        private static UILogTarget _uiTarget;

        // 미리 정의된 로거들
        private static Logger _system;
        private static Logger _motion;
        private static Logger _io;
        private static Logger _vision;
        private static Logger _alarm;
        private static Logger _sequence;
        private static Logger _db;
        private static Logger _trace;

        #endregion

        #region Predefined Loggers

        /// <summary>시스템 로거</summary>
        public static Logger System => _system ?? (_system = GetLogger("System"));

        /// <summary>모션 로거</summary>
        public static Logger Motion => _motion ?? (_motion = GetLogger("Motion"));

        /// <summary>I/O 로거</summary>
        public static Logger IO => _io ?? (_io = GetLogger("IO"));

        /// <summary>비전 로거</summary>
        public static Logger Vision => _vision ?? (_vision = GetLogger("Vision"));

        /// <summary>알람 로거</summary>
        public static Logger Alarm => _alarm ?? (_alarm = GetLogger("Alarm"));

        /// <summary>시퀀스 로거</summary>
        public static Logger Sequence => _sequence ?? (_sequence = GetLogger("Sequence"));

        /// <summary>데이터베이스 로거</summary>
        public static Logger DB => _db ?? (_db = GetLogger("DB"));

        /// <summary>추적 로거</summary>
        public static Logger Trace => _trace ?? (_trace = GetLogger("Trace"));

        #endregion

        #region Initialization

        /// <summary>
        /// NLog 초기화
        /// </summary>
        /// <param name="configPath">NLog.config 경로 (null이면 기본 경로)</param>
        public static void Initialize(string configPath = null)
        {
            if (_isInitialized)
                return;

            lock (_lock)
            {
                if (_isInitialized)
                    return;

                try
                {
                    // 설정 파일 경로 결정
                    if (string.IsNullOrEmpty(configPath))
                    {
                        configPath = Path.Combine(
                            AppDomain.CurrentDomain.BaseDirectory,
                            "Log", "DLLs", "NLog.config");
                    }

                    // 설정 파일이 있으면 로드
                    if (File.Exists(configPath))
                    {
                        NLog.LogManager.Configuration = new XmlLoggingConfiguration(configPath);
                    }
                    else
                    {
                        // 기본 설정 사용 (콘솔 출력만)
                        CreateDefaultConfiguration();
                    }

                    // UI 타겟 추가
                    AddUITarget();

                    _isInitialized = true;

                    System.Info("LogManager initialized");
                }
                catch (Exception ex)
                {
                    global::System.Diagnostics.Debug.WriteLine($"[LogManager] Initialize error: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 기본 설정 생성 (설정 파일이 없을 경우)
        /// </summary>
        private static void CreateDefaultConfiguration()
        {
            var config = new LoggingConfiguration();

            // 콘솔 타겟
            var consoleTarget = new NLog.Targets.ConsoleTarget("console")
            {
                Layout = "${longdate} ${uppercase:${level}} [${logger}] ${message} ${exception:format=tostring}"
            };
            config.AddTarget(consoleTarget);

            // 파일 타겟
            var logFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            var fileTarget = new NLog.Targets.FileTarget("file")
            {
                FileName = Path.Combine(logFolder, "${shortdate}.log"),
                Layout = "${longdate} ${uppercase:${level}} [${logger}] ${message} ${exception:format=tostring}",
                ArchiveEvery = NLog.Targets.FileArchivePeriod.Day,
                MaxArchiveFiles = 30
            };
            config.AddTarget(fileTarget);

            // 규칙 추가
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTarget);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, fileTarget);

            NLog.LogManager.Configuration = config;
        }

        /// <summary>
        /// NLog 종료
        /// </summary>
        public static void Shutdown()
        {
            UILogTarget.LogReceived -= OnLogReceived;
            _logViewModel = null;
            NLog.LogManager.Shutdown();
            _isInitialized = false;
        }

        /// <summary>
        /// UI 로그 타겟 추가
        /// </summary>
        private static void AddUITarget()
        {
            var config = NLog.LogManager.Configuration ?? new LoggingConfiguration();

            _uiTarget = new UILogTarget
            {
                Name = "uiTarget",
                Layout = "${message}"
            };

            config.AddTarget(_uiTarget);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, _uiTarget);

            NLog.LogManager.Configuration = config;
        }

        /// <summary>
        /// LogViewModel 등록 (UI에 로그 표시용)
        /// </summary>
        public static void RegisterLogViewModel(LogViewModel viewModel)
        {
            if (_logViewModel != null)
            {
                UILogTarget.LogReceived -= OnLogReceived;
            }

            _logViewModel = viewModel;

            if (_logViewModel != null)
            {
                UILogTarget.LogReceived += OnLogReceived;
            }
        }

        private static void OnLogReceived(LogEntry entry)
        {
            _logViewModel?.AddLog(entry);
        }

        #endregion

        #region Logger Factory

        /// <summary>
        /// 로거 가져오기
        /// </summary>
        public static Logger GetLogger(string name)
        {
            if (!_isInitialized)
            {
                Initialize();
            }

            return NLog.LogManager.GetLogger(name);
        }

        /// <summary>
        /// 타입 기반 로거 가져오기
        /// </summary>
        public static Logger GetLogger(Type type)
        {
            return GetLogger(type.Name);
        }

        /// <summary>
        /// 제네릭 타입 기반 로거 가져오기
        /// </summary>
        public static Logger GetLogger<T>()
        {
            return GetLogger(typeof(T).Name);
        }

        #endregion

        #region Convenience Methods

        /// <summary>
        /// 디버그 로그
        /// </summary>
        public static void Debug(string message)
        {
            System.Debug(message);
        }

        /// <summary>
        /// 정보 로그
        /// </summary>
        public static void Info(string message)
        {
            System.Info(message);
        }

        /// <summary>
        /// 경고 로그
        /// </summary>
        public static void Warn(string message)
        {
            System.Warn(message);
        }

        /// <summary>
        /// 에러 로그
        /// </summary>
        public static void Error(string message)
        {
            System.Error(message);
        }

        /// <summary>
        /// 에러 로그 (예외 포함)
        /// </summary>
        public static void Error(Exception ex, string message)
        {
            System.Error(ex, message);
        }

        /// <summary>
        /// 치명적 에러 로그
        /// </summary>
        public static void Fatal(string message)
        {
            System.Fatal(message);
        }

        /// <summary>
        /// 치명적 에러 로그 (예외 포함)
        /// </summary>
        public static void Fatal(Exception ex, string message)
        {
            System.Fatal(ex, message);
        }

        #endregion
    }
}
