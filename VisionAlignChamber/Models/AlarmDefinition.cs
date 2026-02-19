using System;

namespace VisionAlignChamber.Models
{
    /// <summary>
    /// 알람 심각도
    /// </summary>
    public enum AlarmSeverity
    {
        Info = 0,
        Warning = 1,
        Error = 2,
        Critical = 3
    }

    /// <summary>
    /// 알람 카테고리
    /// </summary>
    public enum AlarmCategory
    {
        System,
        Motion,
        IO,
        Vision,
        Sequence,
        Sensor
    }

    /// <summary>
    /// 알람 발생 소스 레벨
    /// </summary>
    public enum AlarmSourceLevel
    {
        /// <summary>
        /// 시스템 레벨
        /// </summary>
        System,

        /// <summary>
        /// 하드웨어 레벨 (AJIN, 센서 등)
        /// </summary>
        Hardware,

        /// <summary>
        /// Facade 레벨 (타임아웃, 조건 실패 등)
        /// </summary>
        Facade
    }

    /// <summary>
    /// 알람 정의 (CSV에서 로드)
    /// </summary>
    public class AlarmDefinition
    {
        /// <summary>
        /// 알람 코드
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 알람 이름 (영문)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 알람 설명 (한글)
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 심각도
        /// </summary>
        public AlarmSeverity Level { get; set; }

        /// <summary>
        /// 카테고리
        /// </summary>
        public AlarmCategory Category { get; set; }

        /// <summary>
        /// 발생 소스 레벨
        /// </summary>
        public AlarmSourceLevel Source { get; set; }

        /// <summary>
        /// CSV 라인에서 AlarmDefinition 생성
        /// </summary>
        public static AlarmDefinition Parse(string csvLine)
        {
            if (string.IsNullOrWhiteSpace(csvLine) || csvLine.StartsWith("#"))
                return null;

            var parts = csvLine.Split(',');
            if (parts.Length < 6)
                return null;

            if (!int.TryParse(parts[0].Trim(), out int code))
                return null;

            return new AlarmDefinition
            {
                Code = code,
                Name = parts[1].Trim(),
                Description = parts[2].Trim(),
                Level = ParseSeverity(parts[3].Trim()),
                Category = ParseCategory(parts[4].Trim()),
                Source = ParseSource(parts[5].Trim())
            };
        }

        private static AlarmSeverity ParseSeverity(string value)
        {
            switch (value.ToLower())
            {
                case "info": return AlarmSeverity.Info;
                case "warning": return AlarmSeverity.Warning;
                case "error": return AlarmSeverity.Error;
                case "critical": return AlarmSeverity.Critical;
                default: return AlarmSeverity.Warning;
            }
        }

        private static AlarmCategory ParseCategory(string value)
        {
            switch (value.ToLower())
            {
                case "system": return AlarmCategory.System;
                case "motion": return AlarmCategory.Motion;
                case "io": return AlarmCategory.IO;
                case "vision": return AlarmCategory.Vision;
                case "sequence": return AlarmCategory.Sequence;
                case "sensor": return AlarmCategory.Sensor;
                default: return AlarmCategory.System;
            }
        }

        private static AlarmSourceLevel ParseSource(string value)
        {
            switch (value.ToLower())
            {
                case "system": return AlarmSourceLevel.System;
                case "hardware": return AlarmSourceLevel.Hardware;
                case "facade": return AlarmSourceLevel.Facade;
                default: return AlarmSourceLevel.Facade;
            }
        }

        public override string ToString()
        {
            return $"[{Code:D4}] {Name}: {Description} ({Level}/{Category}/{Source})";
        }
    }
}
