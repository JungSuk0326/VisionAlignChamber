using System;

namespace VisionAlignChamber.Hardware.IO
{
    /// <summary>
    /// Vision Aligner Digital Input 정의
    /// </summary>
    public enum VADigitalInput
    {
        Sensor1_Wafer_Check,
        Sensor2_Wafer_Check,

        PN_Check_P,
        PN_Check_N,
    }

    /// <summary>
    /// Vision Aligner Digital Output 정의
    /// </summary>
    public enum VADigitalOutput
    {
        // Lift Pin Vacuum Solenoid (4EA)
        LiftPin_BlowSol,
        LiftPin_VaccumSol,
        // Chuck Vacuum Solenoid
        Chuck_BlowSol,
        Chuck_VacuumSol,

        // Vision Light
        Vision_Light,
    }

    /// <summary>
    /// Vision Aligner Motion Axis 정의
    /// </summary>
    public enum VAMotionAxis
    {
        /// <summary>
        /// 웨지 상하 스테이지 (Servo)
        /// </summary>
        WedgeUpDown,

        /// <summary>
        /// 척 회전 (DD Motor)
        /// </summary>
        ChuckRotation,

        /// <summary>
        /// 센터링 스테이지 (Stepping)
        /// </summary>
        CenteringStage_1,

        /// <summary>
        /// 센터링 스테이지2 (Stepping)
        /// </summary>
        CenteringStage_2
    }

    /// <summary>
    /// I/O 채널 정보
    /// </summary>
    public class IOChannelInfo
    {
        /// <summary>
        /// 모듈 번호
        /// </summary>
        public int ModuleNo { get; set; }

        /// <summary>
        /// 채널 번호
        /// </summary>
        public int Channel { get; set; }

        /// <summary>
        /// 설명
        /// </summary>
        public string Description { get; set; }

        public IOChannelInfo(int moduleNo, int channel, string description = "")
        {
            ModuleNo = moduleNo;
            Channel = channel;
            Description = description;
        }

        public IOChannelInfo() : this(0, 0, "") { }
    }

    /// <summary>
    /// Motion Axis 정보
    /// </summary>
    public class AxisInfo
    {
        /// <summary>
        /// 축 번호
        /// </summary>
        public int AxisNo { get; set; }

        /// <summary>
        /// 설명
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 기본 속도 (pulse/sec)
        /// </summary>
        public double DefaultVelocity { get; set; }

        /// <summary>
        /// 기본 가속도 (pulse/sec^2)
        /// </summary>
        public double DefaultAccel { get; set; }

        /// <summary>
        /// 기본 감속도 (pulse/sec^2)
        /// </summary>
        public double DefaultDecel { get; set; }

        public AxisInfo(int axisNo, string description = "")
        {
            AxisNo = axisNo;
            Description = description;
            DefaultVelocity = 10000;
            DefaultAccel = 50000;
            DefaultDecel = 50000;
        }

        public AxisInfo() : this(0, "") { }
    }
}
