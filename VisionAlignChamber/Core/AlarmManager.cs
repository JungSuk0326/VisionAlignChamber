using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using VisionAlignChamber.Models;
using VisionAlignChamber.Database;
using VisionAlignChamber.Log;

namespace VisionAlignChamber.Core
{
    /// <summary>
    /// 알람 관리자
    /// CSV에서 알람 정의를 로드하고, 알람 발생/해제/이력 관리
    /// </summary>
    public class AlarmManager
    {
        #region Singleton

        private static readonly Lazy<AlarmManager> _instance =
            new Lazy<AlarmManager>(() => new AlarmManager());

        public static AlarmManager Instance => _instance.Value;

        private AlarmManager()
        {
            _definitions = new Dictionary<int, AlarmDefinition>();
            _activeAlarms = new ConcurrentDictionary<int, ActiveAlarm>();
        }

        #endregion

        #region Fields

        private readonly Dictionary<int, AlarmDefinition> _definitions;
        private readonly ConcurrentDictionary<int, ActiveAlarm> _activeAlarms;
        private bool _isLoaded = false;

        #endregion

        #region Properties

        /// <summary>
        /// 정의 로드 여부
        /// </summary>
        public bool IsLoaded => _isLoaded;

        /// <summary>
        /// 등록된 알람 정의 수
        /// </summary>
        public int DefinitionCount => _definitions.Count;

        /// <summary>
        /// 현재 활성 알람 수
        /// </summary>
        public int ActiveAlarmCount => _activeAlarms.Count;

        /// <summary>
        /// 활성 알람 목록
        /// </summary>
        public IEnumerable<ActiveAlarm> ActiveAlarms => _activeAlarms.Values.ToList();

        /// <summary>
        /// Critical 알람 존재 여부
        /// </summary>
        public bool HasCriticalAlarm => _activeAlarms.Values.Any(a => a.Definition.Level == AlarmSeverity.Critical);

        /// <summary>
        /// Error 이상 알람 존재 여부
        /// </summary>
        public bool HasErrorOrAbove => _activeAlarms.Values.Any(a => a.Definition.Level >= AlarmSeverity.Error);

        #endregion

        #region Events

        /// <summary>
        /// 알람 발생 이벤트
        /// </summary>
        public event EventHandler<AlarmEventArgs> AlarmRaised;

        /// <summary>
        /// 알람 해제 이벤트
        /// </summary>
        public event EventHandler<AlarmEventArgs> AlarmCleared;

        /// <summary>
        /// 모든 알람 해제 이벤트
        /// </summary>
        public event EventHandler AllAlarmsCleared;

        #endregion

        #region Load Definitions

        /// <summary>
        /// CSV 파일에서 알람 정의 로드
        /// </summary>
        public bool LoadDefinitions(string csvPath = null)
        {
            if (string.IsNullOrEmpty(csvPath))
            {
                csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "AlarmDefine.csv");
            }

            if (!File.Exists(csvPath))
            {
                LogManager.System.Error($"[AlarmManager] AlarmDefine.csv not found: {csvPath}");
                return false;
            }

            try
            {
                _definitions.Clear();

                var lines = File.ReadAllLines(csvPath);
                int loadedCount = 0;

                foreach (var line in lines)
                {
                    // Skip header and comments
                    if (string.IsNullOrWhiteSpace(line) ||
                        line.StartsWith("#") ||
                        line.StartsWith("Code,"))
                        continue;

                    var definition = AlarmDefinition.Parse(line);
                    if (definition != null)
                    {
                        _definitions[definition.Code] = definition;
                        loadedCount++;
                    }
                }

                _isLoaded = true;
                LogManager.System.Info($"[AlarmManager] Loaded {loadedCount} alarm definitions from CSV");
                return true;
            }
            catch (Exception ex)
            {
                LogManager.System.Error($"[AlarmManager] Failed to load alarm definitions: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 알람 정의 조회
        /// </summary>
        public AlarmDefinition GetDefinition(int code)
        {
            return _definitions.TryGetValue(code, out var definition) ? definition : null;
        }

        /// <summary>
        /// 카테고리별 알람 정의 조회
        /// </summary>
        public IEnumerable<AlarmDefinition> GetDefinitionsByCategory(AlarmCategory category)
        {
            return _definitions.Values.Where(d => d.Category == category);
        }

        #endregion

        #region Raise Alarm

        /// <summary>
        /// 알람 발생
        /// </summary>
        /// <param name="code">알람 코드</param>
        /// <param name="additionalMessage">추가 메시지 (옵션)</param>
        /// <param name="axisNo">축 번호 (Motion 알람용, 옵션)</param>
        /// <returns>발생된 알람 정보</returns>
        public ActiveAlarm RaiseAlarm(int code, string additionalMessage = null, int? axisNo = null)
        {
            var definition = GetDefinition(code);
            if (definition == null)
            {
                LogManager.System.Warn($"[AlarmManager] Unknown alarm code: {code}");
                // 정의되지 않은 알람도 처리
                definition = new AlarmDefinition
                {
                    Code = code,
                    Name = "UNKNOWN",
                    Description = $"정의되지 않은 알람 (Code: {code})",
                    Level = AlarmSeverity.Warning,
                    Category = AlarmCategory.System,
                    Source = AlarmSourceLevel.System
                };
            }

            // 이미 활성화된 동일 알람이 있으면 무시
            if (_activeAlarms.ContainsKey(code))
            {
                return _activeAlarms[code];
            }

            var activeAlarm = new ActiveAlarm
            {
                Definition = definition,
                OccurredTime = DateTime.Now,
                AdditionalMessage = additionalMessage,
                AxisNo = axisNo
            };

            _activeAlarms[code] = activeAlarm;

            // DB에 저장
            SaveToHistory(activeAlarm);

            // 로그
            LogAlarm(activeAlarm, true);

            // 이벤트 발생
            AlarmRaised?.Invoke(this, new AlarmEventArgs(activeAlarm));

            return activeAlarm;
        }

        /// <summary>
        /// 알람 발생 (정의 객체 사용)
        /// </summary>
        public ActiveAlarm RaiseAlarm(AlarmDefinition definition, string additionalMessage = null, int? axisNo = null)
        {
            if (definition == null)
                return null;

            return RaiseAlarm(definition.Code, additionalMessage, axisNo);
        }

        #endregion

        #region Clear Alarm

        /// <summary>
        /// 특정 알람 해제
        /// </summary>
        public bool ClearAlarm(int code)
        {
            if (_activeAlarms.TryRemove(code, out var alarm))
            {
                alarm.ClearedTime = DateTime.Now;

                // DB 업데이트
                UpdateHistoryCleared(alarm);

                // 로그
                LogAlarm(alarm, false);

                // 이벤트 발생
                AlarmCleared?.Invoke(this, new AlarmEventArgs(alarm));

                return true;
            }
            return false;
        }

        /// <summary>
        /// 카테고리별 알람 해제
        /// </summary>
        public int ClearAlarmsByCategory(AlarmCategory category)
        {
            var toClear = _activeAlarms.Values
                .Where(a => a.Definition.Category == category)
                .Select(a => a.Definition.Code)
                .ToList();

            int cleared = 0;
            foreach (var code in toClear)
            {
                if (ClearAlarm(code))
                    cleared++;
            }
            return cleared;
        }

        /// <summary>
        /// 모든 알람 해제
        /// </summary>
        public int ClearAllAlarms()
        {
            var codes = _activeAlarms.Keys.ToList();
            int cleared = 0;

            foreach (var code in codes)
            {
                if (ClearAlarm(code))
                    cleared++;
            }

            AllAlarmsCleared?.Invoke(this, EventArgs.Empty);
            return cleared;
        }

        #endregion

        #region Query

        /// <summary>
        /// 알람 활성 여부 확인
        /// </summary>
        public bool IsAlarmActive(int code)
        {
            return _activeAlarms.ContainsKey(code);
        }

        /// <summary>
        /// 활성 알람 조회
        /// </summary>
        public ActiveAlarm GetActiveAlarm(int code)
        {
            return _activeAlarms.TryGetValue(code, out var alarm) ? alarm : null;
        }

        /// <summary>
        /// 카테고리별 활성 알람 조회
        /// </summary>
        public IEnumerable<ActiveAlarm> GetActiveAlarmsByCategory(AlarmCategory category)
        {
            return _activeAlarms.Values.Where(a => a.Definition.Category == category);
        }

        /// <summary>
        /// 심각도 이상 활성 알람 조회
        /// </summary>
        public IEnumerable<ActiveAlarm> GetActiveAlarmsAboveSeverity(AlarmSeverity minSeverity)
        {
            return _activeAlarms.Values.Where(a => a.Definition.Level >= minSeverity);
        }

        #endregion

        #region Database

        private void SaveToHistory(ActiveAlarm alarm)
        {
            try
            {
                // Database.AlarmRecord 사용 (Code는 string 타입)
                var record = new AlarmRecord
                {
                    InterlockId = alarm.Definition.Code,
                    Code = alarm.Definition.Code.ToString("D4"),
                    Name = alarm.Definition.Name,
                    Description = alarm.Definition.Description,
                    Severity = (int)alarm.Definition.Level,
                    Category = (int)alarm.Definition.Category,
                    Source = alarm.Definition.Source.ToString(),
                    OccurredTime = alarm.OccurredTime.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    AdditionalMessage = alarm.AdditionalMessage ?? string.Empty,
                    IsCleared = 0
                };

                alarm.RecordId = AlarmRepository.Instance.Insert(record);
            }
            catch (Exception ex)
            {
                LogManager.System.Error($"[AlarmManager] Failed to save alarm to history: {ex.Message}");
            }
        }

        private void UpdateHistoryCleared(ActiveAlarm alarm)
        {
            try
            {
                if (alarm.RecordId > 0)
                {
                    var record = AlarmRepository.Instance.GetById(alarm.RecordId);
                    if (record != null)
                    {
                        record.ClearedTime = alarm.ClearedTime?.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        record.IsCleared = 1;
                        AlarmRepository.Instance.Update(record);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.System.Error($"[AlarmManager] Failed to update alarm cleared: {ex.Message}");
            }
        }

        #endregion

        #region Logging

        private void LogAlarm(ActiveAlarm alarm, bool isRaised)
        {
            string action = isRaised ? "RAISED" : "CLEARED";
            string severity = alarm.Definition.Level.ToString().ToUpper();
            string message = $"[Alarm {action}] [{severity}] {alarm.Definition.Code:D4} - {alarm.Definition.Description}";

            if (!string.IsNullOrEmpty(alarm.AdditionalMessage))
            {
                message += $" ({alarm.AdditionalMessage})";
            }

            if (alarm.AxisNo.HasValue)
            {
                message += $" [Axis {alarm.AxisNo}]";
            }

            switch (alarm.Definition.Level)
            {
                case AlarmSeverity.Critical:
                case AlarmSeverity.Error:
                    LogManager.System.Error(message);
                    break;
                case AlarmSeverity.Warning:
                    LogManager.System.Warn(message);
                    break;
                default:
                    LogManager.System.Info(message);
                    break;
            }
        }

        #endregion
    }

    #region Supporting Classes

    /// <summary>
    /// 활성 알람 정보
    /// </summary>
    public class ActiveAlarm
    {
        /// <summary>
        /// 알람 정의
        /// </summary>
        public AlarmDefinition Definition { get; set; }

        /// <summary>
        /// 발생 시간
        /// </summary>
        public DateTime OccurredTime { get; set; }

        /// <summary>
        /// 해제 시간
        /// </summary>
        public DateTime? ClearedTime { get; set; }

        /// <summary>
        /// 추가 메시지
        /// </summary>
        public string AdditionalMessage { get; set; }

        /// <summary>
        /// 축 번호 (Motion 알람용)
        /// </summary>
        public int? AxisNo { get; set; }

        /// <summary>
        /// DB 레코드 ID
        /// </summary>
        public long RecordId { get; set; }

        /// <summary>
        /// 활성 상태
        /// </summary>
        public bool IsActive => !ClearedTime.HasValue;

        /// <summary>
        /// 경과 시간
        /// </summary>
        public TimeSpan ElapsedTime => DateTime.Now - OccurredTime;

        public override string ToString()
        {
            return $"{Definition} - {OccurredTime:HH:mm:ss}";
        }
    }

    /// <summary>
    /// 알람 이벤트 인자
    /// </summary>
    public class AlarmEventArgs : EventArgs
    {
        public ActiveAlarm Alarm { get; }

        public AlarmEventArgs(ActiveAlarm alarm)
        {
            Alarm = alarm;
        }
    }

    #endregion
}
