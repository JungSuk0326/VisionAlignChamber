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

}
