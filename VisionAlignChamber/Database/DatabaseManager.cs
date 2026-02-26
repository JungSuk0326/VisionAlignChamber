using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using Dapper;

namespace VisionAlignChamber.Database
{
    /// <summary>
    /// 데이터베이스 관리자 (싱글톤)
    /// SQLite 연결 관리 및 테이블 초기화를 담당합니다.
    /// Dapper를 사용하여 ORM 기능을 제공합니다.
    /// </summary>
    public class DatabaseManager : IDisposable
    {
        #region Singleton

        private static readonly Lazy<DatabaseManager> _instance =
            new Lazy<DatabaseManager>(() => new DatabaseManager());

        /// <summary>
        /// 싱글톤 인스턴스
        /// </summary>
        public static DatabaseManager Instance => _instance.Value;

        private DatabaseManager()
        {
            // 인스턴스 생성 시 자동으로 초기화 (테이블 생성)
            Initialize();
        }

        #endregion

        #region Fields

        private string _connectionString;
        private readonly object _lock = new object();
        private bool _isInitialized;
        private string _databasePath;

        #endregion

        #region Properties

        /// <summary>
        /// 데이터베이스 파일 경로
        /// </summary>
        public string DatabasePath => _databasePath;

        /// <summary>
        /// 초기화 여부
        /// </summary>
        public bool IsInitialized => _isInitialized;

        /// <summary>
        /// 연결 문자열
        /// </summary>
        public string ConnectionString => _connectionString;

        #endregion

        #region Initialization

        /// <summary>
        /// 데이터베이스 초기화
        /// </summary>
        /// <param name="databaseFolder">데이터베이스 폴더 경로 (null이면 기본 Data 폴더)</param>
        /// <param name="databaseName">데이터베이스 파일명 (기본: VisionAlignChamber.db)</param>
        public void Initialize(string databaseFolder = null, string databaseName = "VisionAlignChamber.db")
        {
            if (_isInitialized)
                return;

            lock (_lock)
            {
                if (_isInitialized)
                    return;

                try
                {
                    // 데이터베이스 폴더 설정
                    if (string.IsNullOrEmpty(databaseFolder))
                    {
                        databaseFolder = Path.Combine(
                            AppDomain.CurrentDomain.BaseDirectory, "Database");
                    }

                    // 폴더 생성
                    if (!Directory.Exists(databaseFolder))
                    {
                        Directory.CreateDirectory(databaseFolder);
                    }

                    // 데이터베이스 파일 경로
                    _databasePath = Path.Combine(databaseFolder, databaseName);

                    // 연결 문자열
                    _connectionString = $"Data Source={_databasePath};Version=3;";

                    // 테이블 생성
                    CreateTables();

                    _isInitialized = true;

                    System.Diagnostics.Debug.WriteLine($"[DatabaseManager] Initialized: {_databasePath}");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[DatabaseManager] Initialize error: {ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// 테이블 생성
        /// </summary>
        private void CreateTables()
        {
            // 순환 호출 방지: CreateConnection() 대신 직접 연결 생성
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                // 알람 이력 테이블
                connection.Execute(@"
                    CREATE TABLE IF NOT EXISTS AlarmHistory (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        InterlockId INTEGER NOT NULL,
                        Code TEXT,
                        Name TEXT,
                        Description TEXT,
                        Severity INTEGER,
                        Category INTEGER,
                        OccurredTime TEXT NOT NULL,
                        ClearedTime TEXT,
                        Source TEXT,
                        AdditionalMessage TEXT,
                        IsCleared INTEGER DEFAULT 0
                    )");

                // 알람 이력 인덱스
                connection.Execute(@"
                    CREATE INDEX IF NOT EXISTS IX_AlarmHistory_OccurredTime
                    ON AlarmHistory(OccurredTime DESC)");

                connection.Execute(@"
                    CREATE INDEX IF NOT EXISTS IX_AlarmHistory_InterlockId
                    ON AlarmHistory(InterlockId)");

                // 측정 결과 이력 테이블
                connection.Execute(@"
                    CREATE TABLE IF NOT EXISTS ResultHistory (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        MeasuredTime TEXT NOT NULL,
                        WaferType INTEGER NOT NULL DEFAULT 0,
                        IsValid INTEGER NOT NULL DEFAULT 0,
                        Found INTEGER NOT NULL DEFAULT 0,
                        Index1st INTEGER,
                        Index2nd INTEGER,
                        OffAngle REAL,
                        AbsAngle REAL,
                        Width REAL,
                        Height REAL,
                        CenterX REAL,
                        CenterY REAL,
                        Radius REAL,
                        TotalOffset REAL,
                        EddyValue REAL,
                        PNValue INTEGER
                    )");

                // 측정 결과 인덱스 (시간순 조회용)
                connection.Execute(@"
                    CREATE INDEX IF NOT EXISTS IX_ResultHistory_MeasuredTime
                    ON ResultHistory(MeasuredTime DESC)");
            }

            System.Diagnostics.Debug.WriteLine("[DatabaseManager] Tables created");
        }

        #endregion

        #region Connection Management

        /// <summary>
        /// 새 연결 생성
        /// </summary>
        /// <returns>SQLite 연결 (사용 후 Dispose 필요)</returns>
        public IDbConnection CreateConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        /// <summary>
        /// 연결을 열고 액션 실행 후 자동 닫기
        /// </summary>
        public void Execute(Action<IDbConnection> action)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                action(connection);
            }
        }

        /// <summary>
        /// 연결을 열고 함수 실행 후 결과 반환
        /// </summary>
        public T Execute<T>(Func<IDbConnection, T> func)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                return func(connection);
            }
        }

        #endregion

        #region IDisposable

        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _disposed = true;
            System.Diagnostics.Debug.WriteLine("[DatabaseManager] Disposed");
        }

        ~DatabaseManager()
        {
            Dispose(false);
        }

        #endregion
    }
}
