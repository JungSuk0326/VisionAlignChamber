using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VisionAlignChamber.Core;
using VisionAlignChamber.Database;

namespace VisionAlignChamber.Interlock
{
    /// <summary>
    /// 인터락 관리자 (싱글톤 패턴)
    /// 시스템의 모든 인터락/알람을 관리합니다.
    /// </summary>
    /// <remarks>
    /// 주요 기능:
    /// - 인터락 정의 등록/관리
    /// - 알람 발생/해제
    /// - 활성 알람 조회
    /// - 알람 이력 관리
    /// - EventManager를 통한 알람 이벤트 발행
    /// </remarks>
    public class InterlockManager
    {
        #region Singleton

        private static readonly Lazy<InterlockManager> _instance =
            new Lazy<InterlockManager>(() => new InterlockManager());

        /// <summary>
        /// 싱글톤 인스턴스
        /// </summary>
        public static InterlockManager Instance => _instance.Value;

        private InterlockManager()
        {
            Initialize();
        }

        #endregion

        #region Fields

        private readonly object _lock = new object();
        private readonly Dictionary<int, InterlockDefinition> _definitions = new Dictionary<int, InterlockDefinition>();
        private readonly Dictionary<int, AlarmInfo> _activeAlarms = new Dictionary<int, AlarmInfo>();
        private readonly List<AlarmInfo> _alarmHistory = new List<AlarmInfo>();
        private const int MaxHistoryCount = 1000;
        private const string AlarmDefineCsvFileName = "AlarmDefine.csv";
        private bool _databaseEnabled;

        #endregion

        #region Properties

        /// <summary>
        /// 현재 활성 알람 존재 여부
        /// </summary>
        public bool HasActiveAlarm
        {
            get
            {
                lock (_lock)
                {
                    return _activeAlarms.Count > 0;
                }
            }
        }

        /// <summary>
        /// 현재 활성 알람 수
        /// </summary>
        public int ActiveAlarmCount
        {
            get
            {
                lock (_lock)
                {
                    return _activeAlarms.Count;
                }
            }
        }

        /// <summary>
        /// Critical 레벨 알람 활성 여부
        /// </summary>
        public bool HasCriticalAlarm
        {
            get
            {
                lock (_lock)
                {
                    return _activeAlarms.Values.Any(a => a.Definition?.Severity == AlarmSeverity.Critical);
                }
            }
        }

        /// <summary>
        /// Error 이상 레벨 알람 활성 여부
        /// </summary>
        public bool HasErrorOrAbove
        {
            get
            {
                lock (_lock)
                {
                    return _activeAlarms.Values.Any(a =>
                        a.Definition?.Severity >= AlarmSeverity.Error);
                }
            }
        }

        /// <summary>
        /// 데이터베이스 저장 활성화 여부
        /// </summary>
        public bool DatabaseEnabled
        {
            get => _databaseEnabled;
            set => _databaseEnabled = value;
        }

        #endregion

        #region Initialization

        /// <summary>
        /// 초기화 (CSV에서 인터락 정의 로드)
        /// </summary>
        private void Initialize()
        {
            LoadDefinitionsFromCsv();
        }

        /// <summary>
        /// AlarmDefine.csv에서 인터락 정의를 로드합니다.
        /// </summary>
        private void LoadDefinitionsFromCsv()
        {
            string csvPath = FindAlarmDefineCsvPath();

            if (string.IsNullOrEmpty(csvPath) || !File.Exists(csvPath))
            {
                System.Diagnostics.Debug.WriteLine($"[InterlockManager] AlarmDefine.csv not found. No alarm definitions loaded.");
                return;
            }

            try
            {
                var lines = File.ReadAllLines(csvPath);
                int loadedCount = 0;

                foreach (var line in lines)
                {
                    // 헤더, 주석, 빈 줄 스킵
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#") || line.StartsWith("Code,"))
                        continue;

                    var definition = InterlockDefinition.ParseCsvLine(line);
                    if (definition != null)
                    {
                        RegisterDefinition(definition);
                        loadedCount++;
                    }
                }

                System.Diagnostics.Debug.WriteLine($"[InterlockManager] Loaded {loadedCount} alarm definitions from {csvPath}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[InterlockManager] Failed to load AlarmDefine.csv: {ex.Message}");
            }
        }

        /// <summary>
        /// AlarmDefine.csv 파일 경로를 찾습니다.
        /// </summary>
        private string FindAlarmDefineCsvPath()
        {
            // 실행 파일 기준 Config 폴더
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(baseDir, "Config", AlarmDefineCsvFileName);
            if (File.Exists(path))
                return path;

            // 실행 파일 바로 아래
            path = Path.Combine(baseDir, AlarmDefineCsvFileName);
            if (File.Exists(path))
                return path;

            return null;
        }

        #endregion

        #region Definition Management

        /// <summary>
        /// 인터락 정의 등록
        /// </summary>
        public void RegisterDefinition(InterlockDefinition definition)
        {
            if (definition == null)
                return;

            lock (_lock)
            {
                _definitions[definition.Id] = definition;
            }
        }

        /// <summary>
        /// 인터락 정의 조회
        /// </summary>
        public InterlockDefinition GetDefinition(int id)
        {
            lock (_lock)
            {
                return _definitions.TryGetValue(id, out var def) ? def : null;
            }
        }

        /// <summary>
        /// 코드로 인터락 정의 조회
        /// </summary>
        public InterlockDefinition GetDefinitionByCode(string code)
        {
            lock (_lock)
            {
                return _definitions.Values.FirstOrDefault(d => d.Code == code);
            }
        }

        /// <summary>
        /// 모든 인터락 정의 조회
        /// </summary>
        public List<InterlockDefinition> GetAllDefinitions()
        {
            lock (_lock)
            {
                return _definitions.Values.ToList();
            }
        }

        #endregion

        #region Alarm Management

        /// <summary>
        /// 알람 발생
        /// </summary>
        /// <param name="interlockId">인터락 ID</param>
        /// <param name="source">발생 소스</param>
        /// <param name="additionalMessage">추가 메시지</param>
        /// <returns>발생된 알람 정보 (이미 활성 상태면 null)</returns>
        public AlarmInfo RaiseAlarm(int interlockId, string source = null, string additionalMessage = null)
        {
            lock (_lock)
            {
                // 이미 활성 상태인지 확인
                if (_activeAlarms.ContainsKey(interlockId))
                    return null;

                // 정의 조회
                if (!_definitions.TryGetValue(interlockId, out var definition))
                {
                    System.Diagnostics.Debug.WriteLine($"[InterlockManager] Unknown interlock ID: {interlockId}");
                    return null;
                }

                // 비활성화된 인터락이면 무시
                if (!definition.IsEnabled)
                    return null;

                // 알람 정보 생성
                var alarm = new AlarmInfo(definition, source, additionalMessage);

                // 활성 알람에 추가
                _activeAlarms[interlockId] = alarm;

                // 이력에 추가
                AddToHistory(alarm);

                // 데이터베이스에 저장
                SaveAlarmToDatabase(alarm);

                // 이벤트 발행
                EventManager.Publish(EventManager.AlarmOccurred, alarm);

                System.Diagnostics.Debug.WriteLine($"[InterlockManager] Alarm raised: {alarm}");

                return alarm;
            }
        }

        /// <summary>
        /// 알람 발생 (코드로)
        /// </summary>
        public AlarmInfo RaiseAlarmByCode(string code, string source = null, string additionalMessage = null)
        {
            var definition = GetDefinitionByCode(code);
            if (definition == null)
                return null;

            return RaiseAlarm(definition.Id, source, additionalMessage);
        }

        /// <summary>
        /// 알람 해제
        /// </summary>
        /// <param name="interlockId">인터락 ID</param>
        /// <returns>해제 성공 여부</returns>
        public bool ClearAlarm(int interlockId)
        {
            lock (_lock)
            {
                if (!_activeAlarms.TryGetValue(interlockId, out var alarm))
                    return false;

                // 알람 해제 처리
                alarm.Clear();

                // 활성 목록에서 제거
                _activeAlarms.Remove(interlockId);

                // 데이터베이스 업데이트
                UpdateAlarmClearedInDatabase(interlockId, alarm.ClearedTime.Value);

                // 이벤트 발행
                EventManager.Publish(EventManager.AlarmCleared, alarm);

                System.Diagnostics.Debug.WriteLine($"[InterlockManager] Alarm cleared: {alarm}");

                return true;
            }
        }

        /// <summary>
        /// 알람 해제 (코드로)
        /// </summary>
        public bool ClearAlarmByCode(string code)
        {
            var definition = GetDefinitionByCode(code);
            if (definition == null)
                return false;

            return ClearAlarm(definition.Id);
        }

        /// <summary>
        /// 모든 알람 해제
        /// </summary>
        public void ClearAllAlarms()
        {
            lock (_lock)
            {
                var alarmIds = _activeAlarms.Keys.ToList();
                foreach (var id in alarmIds)
                {
                    ClearAlarm(id);
                }
            }
        }

        /// <summary>
        /// 자동 복구 가능한 알람만 해제
        /// </summary>
        public void ClearAutoRecoverableAlarms()
        {
            lock (_lock)
            {
                var autoRecoverableIds = _activeAlarms
                    .Where(kv => kv.Value.Definition?.AutoRecoverable == true)
                    .Select(kv => kv.Key)
                    .ToList();

                foreach (var id in autoRecoverableIds)
                {
                    ClearAlarm(id);
                }
            }
        }

        #endregion

        #region Query Methods

        /// <summary>
        /// 활성 알람 목록 조회
        /// </summary>
        public List<AlarmInfo> GetActiveAlarms()
        {
            lock (_lock)
            {
                return _activeAlarms.Values.ToList();
            }
        }

        /// <summary>
        /// 특정 심각도 이상의 활성 알람 조회
        /// </summary>
        public List<AlarmInfo> GetActiveAlarms(AlarmSeverity minSeverity)
        {
            lock (_lock)
            {
                return _activeAlarms.Values
                    .Where(a => a.Definition?.Severity >= minSeverity)
                    .ToList();
            }
        }

        /// <summary>
        /// 특정 카테고리의 활성 알람 조회
        /// </summary>
        public List<AlarmInfo> GetActiveAlarmsByCategory(AlarmCategory category)
        {
            lock (_lock)
            {
                return _activeAlarms.Values
                    .Where(a => a.Definition?.Category == category)
                    .ToList();
            }
        }

        /// <summary>
        /// 특정 인터락이 활성 상태인지 확인
        /// </summary>
        public bool IsAlarmActive(int interlockId)
        {
            lock (_lock)
            {
                return _activeAlarms.ContainsKey(interlockId);
            }
        }

        /// <summary>
        /// 알람 이력 조회
        /// </summary>
        public List<AlarmInfo> GetAlarmHistory(int count = 100)
        {
            lock (_lock)
            {
                return _alarmHistory
                    .OrderByDescending(a => a.OccurredTime)
                    .Take(count)
                    .ToList();
            }
        }

        /// <summary>
        /// 기간별 알람 이력 조회
        /// </summary>
        public List<AlarmInfo> GetAlarmHistory(DateTime from, DateTime to)
        {
            lock (_lock)
            {
                return _alarmHistory
                    .Where(a => a.OccurredTime >= from && a.OccurredTime <= to)
                    .OrderByDescending(a => a.OccurredTime)
                    .ToList();
            }
        }

        #endregion

        #region Interlock Check

        /// <summary>
        /// 동작 가능 여부 확인 (Critical/Error 알람 없을 때 가능)
        /// </summary>
        public bool CanOperate()
        {
            return !HasErrorOrAbove;
        }

        /// <summary>
        /// 특정 카테고리 동작 가능 여부 확인
        /// </summary>
        public bool CanOperate(AlarmCategory category)
        {
            lock (_lock)
            {
                return !_activeAlarms.Values.Any(a =>
                    a.Definition?.Category == category &&
                    a.Definition?.Severity >= AlarmSeverity.Error);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 이력에 추가 (최대 개수 제한)
        /// </summary>
        private void AddToHistory(AlarmInfo alarm)
        {
            _alarmHistory.Add(alarm);

            // 최대 개수 초과 시 오래된 것 삭제
            while (_alarmHistory.Count > MaxHistoryCount)
            {
                _alarmHistory.RemoveAt(0);
            }
        }

        /// <summary>
        /// 알람을 데이터베이스에 저장
        /// </summary>
        private void SaveAlarmToDatabase(AlarmInfo alarm)
        {
            if (!_databaseEnabled)
                return;

            try
            {
                AlarmRepository.Instance.InsertAlarm(alarm);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[InterlockManager] DB save error: {ex.Message}");
            }
        }

        /// <summary>
        /// 알람 해제 시간을 데이터베이스에 업데이트
        /// </summary>
        private void UpdateAlarmClearedInDatabase(int interlockId, DateTime clearedTime)
        {
            if (!_databaseEnabled)
                return;

            try
            {
                AlarmRepository.Instance.UpdateAlarmCleared(interlockId, clearedTime);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[InterlockManager] DB update error: {ex.Message}");
            }
        }

        #endregion

        #region Database

        /// <summary>
        /// 데이터베이스 활성화 (초기화 시 호출)
        /// </summary>
        public void EnableDatabase()
        {
            try
            {
                DatabaseManager.Instance.Initialize();
                _databaseEnabled = true;
                System.Diagnostics.Debug.WriteLine("[InterlockManager] Database enabled");
            }
            catch (Exception ex)
            {
                _databaseEnabled = false;
                System.Diagnostics.Debug.WriteLine($"[InterlockManager] Database enable failed: {ex.Message}");
            }
        }

        /// <summary>
        /// 데이터베이스에서 알람 이력 조회
        /// </summary>
        public List<AlarmRecord> GetAlarmHistoryFromDatabase(int count = 100)
        {
            if (!_databaseEnabled)
                return new List<AlarmRecord>();

            return AlarmRepository.Instance.GetAlarmHistory(count);
        }

        /// <summary>
        /// 데이터베이스에서 기간별 알람 이력 조회
        /// </summary>
        public List<AlarmRecord> GetAlarmHistoryFromDatabase(DateTime from, DateTime to)
        {
            if (!_databaseEnabled)
                return new List<AlarmRecord>();

            return AlarmRepository.Instance.GetAlarmHistory(from, to);
        }

        #endregion
    }
}
