using System;

namespace VisionAlignChamber.Hardware.Common
{
    /// <summary>
    /// 축 파라미터 구조체
    /// </summary>
    public class AxisParameter
    {
        /// <summary>
        /// 축 번호
        /// </summary>
        public int AxisNo { get; set; }

        /// <summary>
        /// 축 이름
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 단위당 펄스 수 (pulse/unit)
        /// </summary>
        public double PulsePerUnit { get; set; } = 1000.0;

        /// <summary>
        /// 최대 속도
        /// </summary>
        public double MaxVelocity { get; set; } = 100000.0;

        /// <summary>
        /// 최소 속도
        /// </summary>
        public double MinVelocity { get; set; } = 1.0;

        /// <summary>
        /// 기본 가속도
        /// </summary>
        public double DefaultAccel { get; set; } = 10000.0;

        /// <summary>
        /// 기본 감속도
        /// </summary>
        public double DefaultDecel { get; set; } = 10000.0;

        /// <summary>
        /// 소프트웨어 상한 리밋
        /// </summary>
        public double SoftLimitPlus { get; set; } = double.MaxValue;

        /// <summary>
        /// 소프트웨어 하한 리밋
        /// </summary>
        public double SoftLimitMinus { get; set; } = double.MinValue;

        /// <summary>
        /// 소프트웨어 리밋 사용 여부
        /// </summary>
        public bool UseSoftLimit { get; set; } = false;

        /// <summary>
        /// 원점 복귀 방향 (1: 정방향, -1: 역방향)
        /// </summary>
        public int HomeDirection { get; set; } = -1;

        /// <summary>
        /// 원점 복귀 1차 속도
        /// </summary>
        public double HomeFirstVelocity { get; set; } = 10000.0;

        /// <summary>
        /// 원점 복귀 2차 속도
        /// </summary>
        public double HomeSecondVelocity { get; set; } = 2000.0;

        /// <summary>
        /// 원점 복귀 3차 속도
        /// </summary>
        public double HomeThirdVelocity { get; set; } = 500.0;

        /// <summary>
        /// 원점 복귀 마지막 속도
        /// </summary>
        public double HomeLastVelocity { get; set; } = 100.0;

        /// <summary>
        /// 원점 복귀 가속도
        /// </summary>
        public double HomeAccel { get; set; } = 10000.0;

        /// <summary>
        /// 기본값으로 복사본 생성
        /// </summary>
        public AxisParameter Clone()
        {
            return new AxisParameter
            {
                AxisNo = this.AxisNo,
                Name = this.Name,
                PulsePerUnit = this.PulsePerUnit,
                MaxVelocity = this.MaxVelocity,
                MinVelocity = this.MinVelocity,
                DefaultAccel = this.DefaultAccel,
                DefaultDecel = this.DefaultDecel,
                SoftLimitPlus = this.SoftLimitPlus,
                SoftLimitMinus = this.SoftLimitMinus,
                UseSoftLimit = this.UseSoftLimit,
                HomeDirection = this.HomeDirection,
                HomeFirstVelocity = this.HomeFirstVelocity,
                HomeSecondVelocity = this.HomeSecondVelocity,
                HomeThirdVelocity = this.HomeThirdVelocity,
                HomeLastVelocity = this.HomeLastVelocity,
                HomeAccel = this.HomeAccel
            };
        }
    }
}
