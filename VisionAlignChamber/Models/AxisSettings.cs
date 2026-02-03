using System;
using VisionAlignChamber.Hardware.Facade;

namespace VisionAlignChamber.Models
{
    /// <summary>
    /// 축별 설정값
    /// </summary>
    public class AxisSettings
    {
        /// <summary>
        /// 축 종류
        /// </summary>
        public VAMotionAxis Axis { get; set; }

        /// <summary>
        /// 축 이름
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 홈 위치 (pulse)
        /// </summary>
        public double HomePosition { get; set; }

        /// <summary>
        /// 대기 위치 (pulse)
        /// </summary>
        public double WaitPosition { get; set; }

        /// <summary>
        /// 작업 위치 (pulse)
        /// </summary>
        public double WorkPosition { get; set; }

        /// <summary>
        /// 소프트웨어 리밋 - (pulse)
        /// </summary>
        public double SoftLimitMinus { get; set; }

        /// <summary>
        /// 소프트웨어 리밋 + (pulse)
        /// </summary>
        public double SoftLimitPlus { get; set; }

        /// <summary>
        /// 최대 속도 (pulse/sec)
        /// </summary>
        public double MaxVelocity { get; set; } = 100000;

        /// <summary>
        /// 기본 속도 (pulse/sec)
        /// </summary>
        public double DefaultVelocity { get; set; } = 10000;

        /// <summary>
        /// 기본 가속도 (pulse/sec^2)
        /// </summary>
        public double DefaultAccel { get; set; } = 50000;

        /// <summary>
        /// 기본 감속도 (pulse/sec^2)
        /// </summary>
        public double DefaultDecel { get; set; } = 50000;

        /// <summary>
        /// 펄스/단위 변환 계수 (pulse per unit)
        /// </summary>
        public double PulsePerUnit { get; set; } = 1.0;

        /// <summary>
        /// 단위 문자열 (mm, degree 등)
        /// </summary>
        public string Unit { get; set; } = "pulse";

        /// <summary>
        /// 위치를 단위로 변환
        /// </summary>
        public double ToUnit(double pulse)
        {
            return PulsePerUnit != 0 ? pulse / PulsePerUnit : pulse;
        }

        /// <summary>
        /// 단위를 펄스로 변환
        /// </summary>
        public double ToPulse(double unit)
        {
            return unit * PulsePerUnit;
        }

        /// <summary>
        /// 기본 설정 생성
        /// </summary>
        public static AxisSettings CreateDefault(VAMotionAxis axis)
        {
            switch (axis)
            {
                case VAMotionAxis.WedgeUpDown:
                    return new AxisSettings
                    {
                        Axis = axis,
                        Name = "Wedge Up/Down",
                        HomePosition = 0,
                        WaitPosition = 0,
                        WorkPosition = 50000,
                        SoftLimitMinus = -1000,
                        SoftLimitPlus = 100000,
                        Unit = "pulse"
                    };
                case VAMotionAxis.ChuckRotation:
                    return new AxisSettings
                    {
                        Axis = axis,
                        Name = "Chuck Rotation",
                        HomePosition = 0,
                        WaitPosition = 0,
                        WorkPosition = 0,
                        SoftLimitMinus = -3600000,
                        SoftLimitPlus = 3600000,
                        PulsePerUnit = 10000,  // 1 degree = 10000 pulse
                        Unit = "degree"
                    };
                case VAMotionAxis.CenteringStage_1:
                    return new AxisSettings
                    {
                        Axis = axis,
                        Name = "Centering Stage 1",
                        HomePosition = 0,
                        WaitPosition = 0,
                        WorkPosition = 0,
                        SoftLimitMinus = -1000,
                        SoftLimitPlus = 500000,
                        Unit = "pulse"
                    };
                case VAMotionAxis.CenteringStage_2:
                    return new AxisSettings
                    {
                        Axis = axis,
                        Name = "Centering Stage 2",
                        HomePosition = 0,
                        WaitPosition = 0,
                        WorkPosition = 0,
                        SoftLimitMinus = -1000,
                        SoftLimitPlus = 500000,
                        Unit = "pulse"
                    };
                default:
                    return new AxisSettings { Axis = axis };
            }
        }
    }
}
