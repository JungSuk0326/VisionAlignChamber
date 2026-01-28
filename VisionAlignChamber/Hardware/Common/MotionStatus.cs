using System;

namespace VisionAlignChamber.Hardware.Common
{
    /// <summary>
    /// 모션 상태 열거형
    /// </summary>
    [Flags]
    public enum MotionStatus : uint
    {
        /// <summary>
        /// 정상 상태
        /// </summary>
        None = 0,

        /// <summary>
        /// 이동 중
        /// </summary>
        InMotion = 1 << 0,

        /// <summary>
        /// 원점 복귀 중
        /// </summary>
        Homing = 1 << 1,

        /// <summary>
        /// 원점 복귀 완료
        /// </summary>
        HomeDone = 1 << 2,

        /// <summary>
        /// 서보 ON
        /// </summary>
        ServoOn = 1 << 3,

        /// <summary>
        /// 알람 발생
        /// </summary>
        Alarm = 1 << 4,

        /// <summary>
        /// 양방향 리밋 센서 감지
        /// </summary>
        LimitPlus = 1 << 5,

        /// <summary>
        /// 음방향 리밋 센서 감지
        /// </summary>
        LimitMinus = 1 << 6,

        /// <summary>
        /// 소프트웨어 양방향 리밋
        /// </summary>
        SoftLimitPlus = 1 << 7,

        /// <summary>
        /// 소프트웨어 음방향 리밋
        /// </summary>
        SoftLimitMinus = 1 << 8,

        /// <summary>
        /// 비상 정지
        /// </summary>
        EmergencyStop = 1 << 9,

        /// <summary>
        /// In Position (목표 위치 도달)
        /// </summary>
        InPosition = 1 << 10,

        /// <summary>
        /// 서보 레디
        /// </summary>
        ServoReady = 1 << 11
    }

    /// <summary>
    /// 원점 복귀 상태
    /// </summary>
    public enum HomeStatus
    {
        /// <summary>
        /// 원점 복귀 안됨
        /// </summary>
        None = 0,

        /// <summary>
        /// 원점 복귀 진행 중
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// 원점 복귀 완료
        /// </summary>
        Done = 2,

        /// <summary>
        /// 원점 복귀 실패
        /// </summary>
        Failed = 3
    }

    /// <summary>
    /// 축 상태 정보 구조체
    /// </summary>
    public struct AxisStatus
    {
        /// <summary>
        /// 축 번호
        /// </summary>
        public int AxisNo;

        /// <summary>
        /// 모션 상태 플래그
        /// </summary>
        public MotionStatus Status;

        /// <summary>
        /// 실제 위치
        /// </summary>
        public double ActualPosition;

        /// <summary>
        /// 명령 위치
        /// </summary>
        public double CommandPosition;

        /// <summary>
        /// 현재 속도
        /// </summary>
        public double CurrentVelocity;

        /// <summary>
        /// 원점 복귀 상태
        /// </summary>
        public HomeStatus HomeStatus;

        /// <summary>
        /// 이동 중 여부
        /// </summary>
        public bool IsMoving => (Status & MotionStatus.InMotion) != 0;

        /// <summary>
        /// 서보 ON 여부
        /// </summary>
        public bool IsServoOn => (Status & MotionStatus.ServoOn) != 0;

        /// <summary>
        /// 알람 발생 여부
        /// </summary>
        public bool HasAlarm => (Status & MotionStatus.Alarm) != 0;

        /// <summary>
        /// In Position 여부
        /// </summary>
        public bool IsInPosition => (Status & MotionStatus.InPosition) != 0;
    }
}
