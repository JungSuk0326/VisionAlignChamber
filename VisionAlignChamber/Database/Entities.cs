using System;
using VisionAlignChamber.Interlock;
using VisionAlignChamber.Models;

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
        public Interlock.AlarmSeverity SeverityEnum => (Interlock.AlarmSeverity)Severity;

        /// <summary>
        /// 알람 카테고리 (enum)
        /// </summary>
        public Interlock.AlarmCategory CategoryEnum => (Interlock.AlarmCategory)Category;

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
        public static AlarmRecord FromAlarmInfo(Interlock.AlarmInfo alarm)
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

    /// <summary>
    /// 측정 결과 이력 엔티티 (DB 테이블 매핑)
    /// </summary>
    public class ResultHistoryEntity
    {
        public long Id { get; set; }

        /// <summary>
        /// 측정 시간
        /// </summary>
        public string MeasuredTime { get; set; }

        /// <summary>
        /// 웨이퍼 타입 (0: Notch, 1: Flat)
        /// </summary>
        public int WaferType { get; set; }

        // Vision 결과
        public int IsValid { get; set; }
        public int Found { get; set; }
        public int Index1st { get; set; }
        public int Index2nd { get; set; }
        public double OffAngle { get; set; }
        public double AbsAngle { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        // Wafer 정보 (Flatten)
        public double CenterX { get; set; }
        public double CenterY { get; set; }
        public double Radius { get; set; }
        public double TotalOffset { get; set; }

        // 측정값
        public double EddyValue { get; set; }
        public int PNValue { get; set; }

        #region Computed Properties

        /// <summary>
        /// 측정 시간 (DateTime)
        /// </summary>
        public DateTime MeasuredDateTime => DateTime.TryParse(MeasuredTime, out var dt) ? dt : DateTime.MinValue;

        /// <summary>
        /// 유효성 (bool)
        /// </summary>
        public bool IsValidBool => IsValid == 1;

        /// <summary>
        /// 검출 여부 (bool)
        /// </summary>
        public bool FoundBool => Found == 1;

        /// <summary>
        /// 웨이퍼 타입 문자열
        /// </summary>
        public string WaferTypeText => WaferType == 1 ? "Flat" : "Notch";

        /// <summary>
        /// P/N 값 문자열
        /// </summary>
        public string PNText => PNValue == 1 ? "P" : "N";

        #endregion

        #region Factory Methods

        /// <summary>
        /// WaferVisionResult에서 ResultHistoryEntity 생성
        /// </summary>
        public static ResultHistoryEntity FromResult(WaferVisionResult result, bool isFlat)
        {
            return new ResultHistoryEntity
            {
                MeasuredTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                WaferType = isFlat ? 1 : 0,
                IsValid = result.IsValid ? 1 : 0,
                Found = result.Found ? 1 : 0,
                Index1st = result.Index1st,
                Index2nd = result.Index2nd,
                OffAngle = result.OffAngle,
                AbsAngle = result.AbsAngle,
                Width = result.Width,
                Height = result.Height,
                CenterX = result.Wafer.CenterX,
                CenterY = result.Wafer.CenterY,
                Radius = result.Wafer.Radius,
                TotalOffset = result.Wafer.TotalOffset,
                EddyValue = result.EddyValue,
                PNValue = result.PNValue
            };
        }

        /// <summary>
        /// WaferVisionResult로 변환
        /// </summary>
        public WaferVisionResult ToVisionResult()
        {
            return new WaferVisionResult
            {
                IsValid = IsValidBool,
                Found = FoundBool,
                Index1st = Index1st,
                Index2nd = Index2nd,
                OffAngle = OffAngle,
                AbsAngle = AbsAngle,
                Width = Width,
                Height = Height,
                Wafer = new WaferInfo
                {
                    CenterX = CenterX,
                    CenterY = CenterY,
                    Radius = Radius
                },
                EddyValue = EddyValue,
                PNValue = PNValue
            };
        }

        #endregion

        public override string ToString()
        {
            return $"[{MeasuredTime}] {WaferTypeText} - Angle: {AbsAngle:F3}°, Offset: {TotalOffset:F3}mm, Valid: {IsValidBool}";
        }
    }
}
