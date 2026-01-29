using System;

namespace VisionAlignChamber.Models
{
    /// <summary>
    /// 시스템 운전 모드
    /// </summary>
    public enum SystemMode
    {
        /// <summary>
        /// 수동 모드 - 개별 동작 제어
        /// </summary>
        Manual,

        /// <summary>
        /// 자동 모드 - 시퀀스 자동 실행
        /// </summary>
        Auto,

        /// <summary>
        /// 셋업 모드 - 파라미터 설정
        /// </summary>
        Setup
    }

    /// <summary>
    /// 시스템 상태
    /// </summary>
    public enum SystemStatus
    {
        /// <summary>
        /// 대기 상태
        /// </summary>
        Idle,

        /// <summary>
        /// 동작 중
        /// </summary>
        Running,

        /// <summary>
        /// 일시 정지
        /// </summary>
        Paused,

        /// <summary>
        /// 에러 발생
        /// </summary>
        Error,

        /// <summary>
        /// 비상 정지
        /// </summary>
        EMO
    }

    /// <summary>
    /// 시스템 상태 정보
    /// </summary>
    public class SystemState
    {
        /// <summary>
        /// 현재 운전 모드
        /// </summary>
        public SystemMode Mode { get; set; } = SystemMode.Manual;

        /// <summary>
        /// 현재 시스템 상태
        /// </summary>
        public SystemStatus Status { get; set; } = SystemStatus.Idle;

        /// <summary>
        /// 초기화 완료 여부
        /// </summary>
        public bool IsInitialized { get; set; }

        /// <summary>
        /// 원점 복귀 완료 여부
        /// </summary>
        public bool IsHomed { get; set; }

        /// <summary>
        /// 웨이퍼 로드 여부
        /// </summary>
        public bool IsWaferLoaded { get; set; }

        /// <summary>
        /// 정렬 완료 여부
        /// </summary>
        public bool IsAligned { get; set; }

        /// <summary>
        /// 마지막 상태 변경 시간
        /// </summary>
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        /// <summary>
        /// 상태 메시지
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// 자동 운전 가능 여부
        /// </summary>
        public bool CanRunAuto => IsInitialized && IsHomed && Status != SystemStatus.EMO && Status != SystemStatus.Error;

        /// <summary>
        /// 상태 복사
        /// </summary>
        public SystemState Clone()
        {
            return new SystemState
            {
                Mode = this.Mode,
                Status = this.Status,
                IsInitialized = this.IsInitialized,
                IsHomed = this.IsHomed,
                IsWaferLoaded = this.IsWaferLoaded,
                IsAligned = this.IsAligned,
                LastUpdated = this.LastUpdated,
                Message = this.Message
            };
        }
    }
}
