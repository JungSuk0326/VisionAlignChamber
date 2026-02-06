using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using VisionAlignChamber.Interlock;

namespace VisionAlignChamber.Database
{
    /// <summary>
    /// 알람 이력 저장소
    /// Dapper를 사용하여 알람 CRUD를 수행합니다.
    /// </summary>
    public class AlarmRepository
    {
        #region Singleton

        private static readonly Lazy<AlarmRepository> _instance =
            new Lazy<AlarmRepository>(() => new AlarmRepository());

        /// <summary>
        /// 싱글톤 인스턴스
        /// </summary>
        public static AlarmRepository Instance => _instance.Value;

        private AlarmRepository()
        {
        }

        #endregion

        #region Create

        /// <summary>
        /// 알람 저장
        /// </summary>
        public long Insert(AlarmInfo alarm)
        {
            if (alarm?.Definition == null)
                return -1;

            var record = AlarmRecord.FromAlarmInfo(alarm);
            return Insert(record);
        }

        /// <summary>
        /// 알람 레코드 저장
        /// </summary>
        public long Insert(AlarmRecord record)
        {
            if (record == null)
                return -1;

            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                {
                    conn.Execute(@"
                        INSERT INTO AlarmHistory
                        (InterlockId, Code, Name, Description, Severity, Category,
                         OccurredTime, ClearedTime, Source, AdditionalMessage, IsCleared)
                        VALUES
                        (@InterlockId, @Code, @Name, @Description, @Severity, @Category,
                         @OccurredTime, @ClearedTime, @Source, @AdditionalMessage, @IsCleared)",
                        record);

                    return conn.ExecuteScalar<long>("SELECT last_insert_rowid()");
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AlarmRepository] Insert error: {ex.Message}");
                return -1;
            }
        }

        #endregion

        #region Read

        /// <summary>
        /// ID로 알람 조회
        /// </summary>
        public AlarmRecord GetById(long id)
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.QueryFirstOrDefault<AlarmRecord>(
                        "SELECT * FROM AlarmHistory WHERE Id = @Id", new { Id = id }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AlarmRepository] GetById error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 모든 알람 조회
        /// </summary>
        public IEnumerable<AlarmRecord> GetAll()
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.Query<AlarmRecord>("SELECT * FROM AlarmHistory ORDER BY OccurredTime DESC"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AlarmRepository] GetAll error: {ex.Message}");
                return Enumerable.Empty<AlarmRecord>();
            }
        }

        /// <summary>
        /// 최근 알람 이력 조회
        /// </summary>
        public IEnumerable<AlarmRecord> GetRecent(int count = 100)
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.Query<AlarmRecord>(
                        "SELECT * FROM AlarmHistory ORDER BY OccurredTime DESC LIMIT @Count",
                        new { Count = count }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AlarmRepository] GetRecent error: {ex.Message}");
                return Enumerable.Empty<AlarmRecord>();
            }
        }

        /// <summary>
        /// 기간별 알람 조회
        /// </summary>
        public IEnumerable<AlarmRecord> GetByDateRange(DateTime from, DateTime to)
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.Query<AlarmRecord>(
                        @"SELECT * FROM AlarmHistory
                          WHERE OccurredTime >= @From AND OccurredTime <= @To
                          ORDER BY OccurredTime DESC",
                        new
                        {
                            From = from.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                            To = to.ToString("yyyy-MM-dd HH:mm:ss.fff")
                        }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AlarmRepository] GetByDateRange error: {ex.Message}");
                return Enumerable.Empty<AlarmRecord>();
            }
        }

        /// <summary>
        /// 오늘 알람 조회
        /// </summary>
        public IEnumerable<AlarmRecord> GetToday()
        {
            return GetByDateRange(DateTime.Today, DateTime.Now);
        }

        /// <summary>
        /// 심각도별 알람 조회
        /// </summary>
        public IEnumerable<AlarmRecord> GetBySeverity(AlarmSeverity minSeverity, int count = 100)
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.Query<AlarmRecord>(
                        @"SELECT * FROM AlarmHistory
                          WHERE Severity >= @Severity
                          ORDER BY OccurredTime DESC
                          LIMIT @Count",
                        new { Severity = (int)minSeverity, Count = count }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AlarmRepository] GetBySeverity error: {ex.Message}");
                return Enumerable.Empty<AlarmRecord>();
            }
        }

        /// <summary>
        /// 카테고리별 알람 조회
        /// </summary>
        public IEnumerable<AlarmRecord> GetByCategory(AlarmCategory category, int count = 100)
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.Query<AlarmRecord>(
                        @"SELECT * FROM AlarmHistory
                          WHERE Category = @Category
                          ORDER BY OccurredTime DESC
                          LIMIT @Count",
                        new { Category = (int)category, Count = count }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AlarmRepository] GetByCategory error: {ex.Message}");
                return Enumerable.Empty<AlarmRecord>();
            }
        }

        /// <summary>
        /// 인터락 ID별 알람 조회
        /// </summary>
        public IEnumerable<AlarmRecord> GetByInterlockId(int interlockId, int count = 100)
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.Query<AlarmRecord>(
                        @"SELECT * FROM AlarmHistory
                          WHERE InterlockId = @InterlockId
                          ORDER BY OccurredTime DESC
                          LIMIT @Count",
                        new { InterlockId = interlockId, Count = count }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AlarmRepository] GetByInterlockId error: {ex.Message}");
                return Enumerable.Empty<AlarmRecord>();
            }
        }

        /// <summary>
        /// 활성(미해제) 알람 조회
        /// </summary>
        public IEnumerable<AlarmRecord> GetActive()
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.Query<AlarmRecord>(
                        "SELECT * FROM AlarmHistory WHERE IsCleared = 0 ORDER BY OccurredTime DESC"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AlarmRepository] GetActive error: {ex.Message}");
                return Enumerable.Empty<AlarmRecord>();
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// 알람 레코드 업데이트
        /// </summary>
        public bool Update(AlarmRecord record)
        {
            if (record == null)
                return false;

            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                {
                    int affected = conn.Execute(@"
                        UPDATE AlarmHistory SET
                            InterlockId = @InterlockId,
                            Code = @Code,
                            Name = @Name,
                            Description = @Description,
                            Severity = @Severity,
                            Category = @Category,
                            OccurredTime = @OccurredTime,
                            ClearedTime = @ClearedTime,
                            Source = @Source,
                            AdditionalMessage = @AdditionalMessage,
                            IsCleared = @IsCleared
                        WHERE Id = @Id", record);
                    return affected > 0;
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AlarmRepository] Update error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 알람 해제 처리
        /// </summary>
        public bool SetCleared(int interlockId, DateTime clearedTime)
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                {
                    int affected = conn.Execute(
                        @"UPDATE AlarmHistory
                          SET ClearedTime = @ClearedTime, IsCleared = 1
                          WHERE InterlockId = @InterlockId
                            AND IsCleared = 0
                            AND Id = (
                                SELECT Id FROM AlarmHistory
                                WHERE InterlockId = @InterlockId AND IsCleared = 0
                                ORDER BY OccurredTime DESC LIMIT 1
                            )",
                        new
                        {
                            InterlockId = interlockId,
                            ClearedTime = clearedTime.ToString("yyyy-MM-dd HH:mm:ss.fff")
                        });
                    return affected > 0;
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AlarmRepository] SetCleared error: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// 알람 삭제
        /// </summary>
        public bool Delete(long id)
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                {
                    int affected = conn.Execute(
                        "DELETE FROM AlarmHistory WHERE Id = @Id", new { Id = id });
                    return affected > 0;
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AlarmRepository] Delete error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 오래된 알람 삭제
        /// </summary>
        public int DeleteOlderThan(int daysToKeep = 90)
        {
            try
            {
                string cutoffDate = DateTime.Now.AddDays(-daysToKeep).ToString("yyyy-MM-dd");
                return DatabaseManager.Instance.Execute(conn =>
                    conn.Execute(
                        "DELETE FROM AlarmHistory WHERE OccurredTime < @CutoffDate",
                        new { CutoffDate = cutoffDate }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AlarmRepository] DeleteOlderThan error: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 모든 알람 삭제
        /// </summary>
        public int DeleteAll()
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.Execute("DELETE FROM AlarmHistory"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AlarmRepository] DeleteAll error: {ex.Message}");
                return 0;
            }
        }

        #endregion

        #region Statistics

        /// <summary>
        /// 전체 알람 수
        /// </summary>
        public int Count()
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.ExecuteScalar<int>("SELECT COUNT(*) FROM AlarmHistory"));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 오늘 알람 수
        /// </summary>
        public int CountToday()
        {
            try
            {
                string today = DateTime.Today.ToString("yyyy-MM-dd");
                return DatabaseManager.Instance.Execute(conn =>
                    conn.ExecuteScalar<int>(
                        "SELECT COUNT(*) FROM AlarmHistory WHERE OccurredTime >= @Today",
                        new { Today = today }));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 인터락별 발생 횟수
        /// </summary>
        public int CountByInterlockId(int interlockId)
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.ExecuteScalar<int>(
                        "SELECT COUNT(*) FROM AlarmHistory WHERE InterlockId = @Id",
                        new { Id = interlockId }));
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region Legacy Support

        public long InsertAlarm(AlarmInfo alarm) => Insert(alarm);

        public int UpdateAlarmCleared(int interlockId, DateTime clearedTime)
            => SetCleared(interlockId, clearedTime) ? 1 : 0;

        public List<AlarmRecord> GetAlarmHistory(int count = 100)
            => GetRecent(count).ToList();

        public List<AlarmRecord> GetAlarmHistory(DateTime from, DateTime to)
            => GetByDateRange(from, to).ToList();

        #endregion
    }
}
