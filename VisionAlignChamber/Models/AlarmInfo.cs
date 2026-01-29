using System;
using System.Collections.Generic;

namespace VisionAlignChamber.Models
{
    /// <summary>
    /// 알람 레벨
    /// </summary>
    public enum AlarmLevel
    {
        /// <summary>
        /// 정보 (참고용)
        /// </summary>
        Info,

        /// <summary>
        /// 경고 (동작 계속 가능)
        /// </summary>
        Warning,

        /// <summary>
        /// 에러 (해당 동작 중지)
        /// </summary>
        Error,

        /// <summary>
        /// 치명적 (시스템 정지 필요)
        /// </summary>
        Critical
    }

    /// <summary>
    /// 알람 소스
    /// </summary>
    public enum AlarmSource
    {
        System,
        Motion,
        IO,
        Vision,
        Sequence
    }

    /// <summary>
    /// 알람 정보
    /// </summary>
    public class AlarmInfo
    {
        /// <summary>
        /// 알람 코드
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 알람 메시지
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 알람 레벨
        /// </summary>
        public AlarmLevel Level { get; set; } = AlarmLevel.Warning;

        /// <summary>
        /// 알람 발생 소스
        /// </summary>
        public AlarmSource Source { get; set; } = AlarmSource.System;

        /// <summary>
        /// 발생 시간
        /// </summary>
        public DateTime OccurredAt { get; set; } = DateTime.Now;

        /// <summary>
        /// 해제 시간
        /// </summary>
        public DateTime? ClearedAt { get; set; }

        /// <summary>
        /// 활성 상태
        /// </summary>
        public bool IsActive => !ClearedAt.HasValue;

        /// <summary>
        /// 추가 정보
        /// </summary>
        public string Details { get; set; } = string.Empty;

        /// <summary>
        /// 알람 생성
        /// </summary>
        public static AlarmInfo Create(int code, string message, AlarmLevel level, AlarmSource source, string details = "")
        {
            return new AlarmInfo
            {
                Code = code,
                Message = message,
                Level = level,
                Source = source,
                OccurredAt = DateTime.Now,
                Details = details
            };
        }

        /// <summary>
        /// 알람 해제
        /// </summary>
        public void Clear()
        {
            ClearedAt = DateTime.Now;
        }

        public override string ToString()
        {
            return $"[{Level}] {Source} - {Code:D4}: {Message}";
        }
    }

    /// <summary>
    /// 알람 코드 정의
    /// </summary>
    public static class AlarmCodes
    {
        // System (1xxx)
        public const int EMO_ACTIVATED = 1001;
        public const int INITIALIZATION_FAILED = 1002;
        public const int COMMUNICATION_ERROR = 1003;

        // Motion (2xxx)
        public const int MOTION_ALARM = 2001;
        public const int HOME_FAILED = 2002;
        public const int POSITION_ERROR = 2003;
        public const int MOTION_TIMEOUT = 2004;
        public const int SOFT_LIMIT_ERROR = 2005;

        // IO (3xxx)
        public const int VACUUM_ERROR = 3001;
        public const int SENSOR_ERROR = 3002;
        public const int INTERLOCK_ERROR = 3003;

        // Vision (4xxx)
        public const int VISION_INIT_FAILED = 4001;
        public const int WAFER_NOT_FOUND = 4002;
        public const int NOTCH_NOT_FOUND = 4003;
        public const int ALIGNMENT_FAILED = 4004;
        public const int VISION_TIMEOUT = 4005;

        // Sequence (5xxx)
        public const int SEQUENCE_ERROR = 5001;
        public const int STEP_TIMEOUT = 5002;
        public const int ABORT_REQUESTED = 5003;

        /// <summary>
        /// 알람 코드에 대한 기본 메시지 가져오기
        /// </summary>
        public static string GetDefaultMessage(int code)
        {
            return code switch
            {
                EMO_ACTIVATED => "Emergency stop activated",
                INITIALIZATION_FAILED => "System initialization failed",
                COMMUNICATION_ERROR => "Communication error",
                MOTION_ALARM => "Motion alarm occurred",
                HOME_FAILED => "Home operation failed",
                POSITION_ERROR => "Position error detected",
                MOTION_TIMEOUT => "Motion timeout",
                SOFT_LIMIT_ERROR => "Soft limit error",
                VACUUM_ERROR => "Vacuum error",
                SENSOR_ERROR => "Sensor error",
                INTERLOCK_ERROR => "Interlock error",
                VISION_INIT_FAILED => "Vision initialization failed",
                WAFER_NOT_FOUND => "Wafer not found",
                NOTCH_NOT_FOUND => "Notch/Flat not found",
                ALIGNMENT_FAILED => "Alignment failed",
                VISION_TIMEOUT => "Vision timeout",
                SEQUENCE_ERROR => "Sequence error",
                STEP_TIMEOUT => "Step timeout",
                ABORT_REQUESTED => "Abort requested",
                _ => $"Unknown alarm ({code})"
            };
        }
    }
}
