using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using VisionAlignChamber.Models;

namespace VisionAlignChamber.Database
{
    /// <summary>
    /// 측정 결과 이력 저장소
    /// Dapper를 사용하여 측정 결과 CRUD를 수행합니다.
    /// </summary>
    public class ResultRepository
    {
        #region Singleton

        private static readonly Lazy<ResultRepository> _instance =
            new Lazy<ResultRepository>(() => new ResultRepository());

        /// <summary>
        /// 싱글톤 인스턴스
        /// </summary>
        public static ResultRepository Instance => _instance.Value;

        private ResultRepository()
        {
        }

        #endregion

        #region Create

        /// <summary>
        /// 측정 결과 저장
        /// </summary>
        public long Insert(WaferVisionResult result, bool isFlat)
        {
            var entity = ResultHistoryEntity.FromResult(result, isFlat);
            return Insert(entity);
        }

        /// <summary>
        /// 측정 결과 엔티티 저장
        /// </summary>
        public long Insert(ResultHistoryEntity entity)
        {
            if (entity == null)
                return -1;

            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                {
                    conn.Execute(@"
                        INSERT INTO ResultHistory
                        (MeasuredTime, WaferType, IsValid, Found, Index1st, Index2nd,
                         OffAngle, AbsAngle, Width, Height,
                         CenterX, CenterY, Radius, TotalOffset, EddyValue, PNValue)
                        VALUES
                        (@MeasuredTime, @WaferType, @IsValid, @Found, @Index1st, @Index2nd,
                         @OffAngle, @AbsAngle, @Width, @Height,
                         @CenterX, @CenterY, @Radius, @TotalOffset, @EddyValue, @PNValue)",
                        entity);

                    return conn.ExecuteScalar<long>("SELECT last_insert_rowid()");
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ResultRepository] Insert error: {ex.Message}");
                return -1;
            }
        }

        #endregion

        #region Read

        /// <summary>
        /// ID로 결과 조회
        /// </summary>
        public ResultHistoryEntity GetById(long id)
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.QueryFirstOrDefault<ResultHistoryEntity>(
                        "SELECT * FROM ResultHistory WHERE Id = @Id", new { Id = id }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ResultRepository] GetById error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 모든 결과 조회
        /// </summary>
        public IEnumerable<ResultHistoryEntity> GetAll()
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.Query<ResultHistoryEntity>("SELECT * FROM ResultHistory ORDER BY MeasuredTime DESC"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ResultRepository] GetAll error: {ex.Message}");
                return Enumerable.Empty<ResultHistoryEntity>();
            }
        }

        /// <summary>
        /// 최근 결과 이력 조회
        /// </summary>
        public IEnumerable<ResultHistoryEntity> GetRecent(int count = 100)
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.Query<ResultHistoryEntity>(
                        "SELECT * FROM ResultHistory ORDER BY MeasuredTime DESC LIMIT @Count",
                        new { Count = count }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ResultRepository] GetRecent error: {ex.Message}");
                return Enumerable.Empty<ResultHistoryEntity>();
            }
        }

        /// <summary>
        /// 기간별 결과 조회
        /// </summary>
        public IEnumerable<ResultHistoryEntity> GetByDateRange(DateTime from, DateTime to)
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.Query<ResultHistoryEntity>(
                        @"SELECT * FROM ResultHistory
                          WHERE MeasuredTime >= @From AND MeasuredTime <= @To
                          ORDER BY MeasuredTime DESC",
                        new
                        {
                            From = from.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                            To = to.ToString("yyyy-MM-dd HH:mm:ss.fff")
                        }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ResultRepository] GetByDateRange error: {ex.Message}");
                return Enumerable.Empty<ResultHistoryEntity>();
            }
        }

        /// <summary>
        /// 오늘 결과 조회
        /// </summary>
        public IEnumerable<ResultHistoryEntity> GetToday()
        {
            return GetByDateRange(DateTime.Today, DateTime.Now);
        }

        /// <summary>
        /// 웨이퍼 타입별 결과 조회
        /// </summary>
        public IEnumerable<ResultHistoryEntity> GetByWaferType(bool isFlat, int count = 100)
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.Query<ResultHistoryEntity>(
                        @"SELECT * FROM ResultHistory
                          WHERE WaferType = @WaferType
                          ORDER BY MeasuredTime DESC
                          LIMIT @Count",
                        new { WaferType = isFlat ? 1 : 0, Count = count }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ResultRepository] GetByWaferType error: {ex.Message}");
                return Enumerable.Empty<ResultHistoryEntity>();
            }
        }

        /// <summary>
        /// 유효한 결과만 조회
        /// </summary>
        public IEnumerable<ResultHistoryEntity> GetValidResults(int count = 100)
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.Query<ResultHistoryEntity>(
                        @"SELECT * FROM ResultHistory
                          WHERE IsValid = 1
                          ORDER BY MeasuredTime DESC
                          LIMIT @Count",
                        new { Count = count }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ResultRepository] GetValidResults error: {ex.Message}");
                return Enumerable.Empty<ResultHistoryEntity>();
            }
        }

        /// <summary>
        /// 가장 최근 결과 조회
        /// </summary>
        public ResultHistoryEntity GetLatest()
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.QueryFirstOrDefault<ResultHistoryEntity>(
                        "SELECT * FROM ResultHistory ORDER BY MeasuredTime DESC LIMIT 1"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ResultRepository] GetLatest error: {ex.Message}");
                return null;
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// 결과 삭제
        /// </summary>
        public bool Delete(long id)
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                {
                    int affected = conn.Execute(
                        "DELETE FROM ResultHistory WHERE Id = @Id", new { Id = id });
                    return affected > 0;
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ResultRepository] Delete error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 오래된 결과 삭제
        /// </summary>
        public int DeleteOlderThan(int daysToKeep = 90)
        {
            try
            {
                string cutoffDate = DateTime.Now.AddDays(-daysToKeep).ToString("yyyy-MM-dd");
                return DatabaseManager.Instance.Execute(conn =>
                    conn.Execute(
                        "DELETE FROM ResultHistory WHERE MeasuredTime < @CutoffDate",
                        new { CutoffDate = cutoffDate }));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ResultRepository] DeleteOlderThan error: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// 모든 결과 삭제
        /// </summary>
        public int DeleteAll()
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.Execute("DELETE FROM ResultHistory"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ResultRepository] DeleteAll error: {ex.Message}");
                return 0;
            }
        }

        #endregion

        #region Statistics

        /// <summary>
        /// 전체 결과 수
        /// </summary>
        public int Count()
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.ExecuteScalar<int>("SELECT COUNT(*) FROM ResultHistory"));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 오늘 결과 수
        /// </summary>
        public int CountToday()
        {
            try
            {
                string today = DateTime.Today.ToString("yyyy-MM-dd");
                return DatabaseManager.Instance.Execute(conn =>
                    conn.ExecuteScalar<int>(
                        "SELECT COUNT(*) FROM ResultHistory WHERE MeasuredTime >= @Today",
                        new { Today = today }));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 유효 결과 비율 (%)
        /// </summary>
        public double GetValidRate(int recentCount = 100)
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                {
                    var results = conn.Query<ResultHistoryEntity>(
                        "SELECT IsValid FROM ResultHistory ORDER BY MeasuredTime DESC LIMIT @Count",
                        new { Count = recentCount }).ToList();

                    if (results.Count == 0)
                        return 0;

                    return results.Count(r => r.IsValid == 1) * 100.0 / results.Count;
                });
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 평균 오프셋 (최근 N개)
        /// </summary>
        public double GetAverageOffset(int recentCount = 100)
        {
            try
            {
                return DatabaseManager.Instance.Execute(conn =>
                    conn.ExecuteScalar<double>(
                        @"SELECT AVG(TotalOffset) FROM (
                            SELECT TotalOffset FROM ResultHistory
                            WHERE IsValid = 1
                            ORDER BY MeasuredTime DESC
                            LIMIT @Count
                        )", new { Count = recentCount }));
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }
}
