using System;
using VisionAlignChamber.Interlock;

namespace VisionAlignChamber.Database
{
    /// <summary>
    /// 알람 이력 엔티티 (DB 테이블 매핑)
    /// </summary>
    public class AlarmRecord
    {
        public long Id { get; set; }
        public int InterlockId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Severity { get; set; }
        public int Category { get; set; }
        public string OccurredTime { get; set; }
        public string ClearedTime { get; set; }
        public string Source { get; set; }
        public string AdditionalMessage { get; set; }
        public int IsCleared { get; set; }

        #region Computed Properties

        /// <summary>
        /// 알람 심각도 (enum)
        /// </summary>
        public AlarmSeverity SeverityEnum => (AlarmSeverity)Severity;

        /// <summary>
        /// 알람 카테고리 (enum)
        /// </summary>
        public AlarmCategory CategoryEnum => (AlarmCategory)Category;

        /// <summary>
        /// 발생 시간 (DateTime)
        /// </summary>
        public DateTime OccurredDateTime => DateTime.TryParse(OccurredTime, out var dt) ? dt : DateTime.MinValue;

        /// <summary>
        /// 해제 시간 (DateTime?)
        /// </summary>
        public DateTime? ClearedDateTime => string.IsNullOrEmpty(ClearedTime) ? (DateTime?)null :
            DateTime.TryParse(ClearedTime, out var dt) ? dt : (DateTime?)null;

        /// <summary>
        /// 해제 여부
        /// </summary>
        public bool IsClearedBool => IsCleared == 1;

        /// <summary>
        /// 알람 지속 시간
        /// </summary>
        public TimeSpan? Duration => ClearedDateTime.HasValue ? ClearedDateTime.Value - OccurredDateTime : (TimeSpan?)null;

        #endregion

        #region Factory Methods

        /// <summary>
        /// AlarmInfo에서 AlarmRecord 생성
        /// </summary>
        public static AlarmRecord FromAlarmInfo(AlarmInfo alarm)
        {
            if (alarm?.Definition == null)
                return null;

            return new AlarmRecord
            {
                InterlockId = alarm.Definition.Id,
                Code = alarm.Definition.Code ?? "",
                Name = alarm.Definition.Name ?? "",
                Description = alarm.Definition.Description ?? "",
                Severity = (int)alarm.Definition.Severity,
                Category = (int)alarm.Definition.Category,
                OccurredTime = alarm.OccurredTime.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                ClearedTime = alarm.ClearedTime?.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                Source = alarm.Source ?? "",
                AdditionalMessage = alarm.AdditionalMessage ?? "",
                IsCleared = alarm.ClearedTime.HasValue ? 1 : 0
            };
        }

        #endregion

        public override string ToString()
        {
            string status = IsClearedBool ? "Cleared" : "Active";
            return $"[{Code}] {Name} - {OccurredTime} ({status})";
        }
    }

    /// <summary>
    /// 검사 결과 엔티티 (DB 테이블 매핑)
    /// </summary>
    public class InspectionRecord
    {
        public long Id { get; set; }
        public string WaferId { get; set; }
        public string LotId { get; set; }
        public string InspectionTime { get; set; }
        public int Result { get; set; }
        public double CenterX { get; set; }
        public double CenterY { get; set; }
        public double Angle { get; set; }
        public double NotchAngle { get; set; }
        public double FlatAngle { get; set; }
        public double ProcessTime { get; set; }
        public string ImagePath { get; set; }
        public string AdditionalData { get; set; }

        public DateTime InspectionDateTime => DateTime.TryParse(InspectionTime, out var dt) ? dt : DateTime.MinValue;
        public bool IsPass => Result == 1;
    }
}
