using System;

namespace VisionAlignChamber.Models
{
    /// <summary>
    /// 웨이퍼 타입
    /// </summary>
    public enum WaferType
    {
        /// <summary>
        /// 노치 웨이퍼
        /// </summary>
        Notch,

        /// <summary>
        /// 플랫 웨이퍼
        /// </summary>
        Flat
    }

    /// <summary>
    /// 웨이퍼 크기
    /// </summary>
    public enum WaferSize
    {
        /// <summary>
        /// 200mm (8인치)
        /// </summary>
        Size200mm,

        /// <summary>
        /// 300mm (12인치)
        /// </summary>
        Size300mm
    }

    /// <summary>
    /// 정렬 파라미터
    /// </summary>
    public class AlignParameters
    {
        /// <summary>
        /// 웨이퍼 타입
        /// </summary>
        public WaferType WaferType { get; set; } = WaferType.Notch;

        /// <summary>
        /// 웨이퍼 크기
        /// </summary>
        public WaferSize WaferSize { get; set; } = WaferSize.Size300mm;

        /// <summary>
        /// 웨이퍼 직경 (mm)
        /// </summary>
        public double WaferDiameter => WaferSize == WaferSize.Size300mm ? 300.0 : 200.0;

        /// <summary>
        /// 목표 노치/플랫 각도 (degree)
        /// </summary>
        public double TargetAngle { get; set; } = 0.0;

        /// <summary>
        /// 센터링 마진 (um)
        /// </summary>
        public double CenteringMargin { get; set; } = 20.0;

        /// <summary>
        /// 정렬 허용 오차 - 각도 (degree)
        /// </summary>
        public double AngleTolerance { get; set; } = 0.01;

        /// <summary>
        /// 정렬 허용 오차 - 센터 (um)
        /// </summary>
        public double CenterTolerance { get; set; } = 10.0;

        /// <summary>
        /// 비전 스캔 횟수
        /// </summary>
        public int ScanCount { get; set; } = 1;

        /// <summary>
        /// 비전 노출 시간 (ms)
        /// </summary>
        public int ExposureTime { get; set; } = 100;

        /// <summary>
        /// 비전 밝기 레벨 (0-255)
        /// </summary>
        public int LightLevel { get; set; } = 128;

        /// <summary>
        /// 정렬 재시도 횟수
        /// </summary>
        public int RetryCount { get; set; } = 3;

        /// <summary>
        /// 정렬 타임아웃 (ms)
        /// </summary>
        public int AlignTimeout { get; set; } = 30000;

        /// <summary>
        /// 기본 파라미터 생성
        /// </summary>
        public static AlignParameters CreateDefault()
        {
            return new AlignParameters
            {
                WaferType = WaferType.Notch,
                WaferSize = WaferSize.Size300mm,
                TargetAngle = 0.0,
                CenteringMargin = 20.0,
                AngleTolerance = 0.01,
                CenterTolerance = 10.0,
                ScanCount = 1,
                ExposureTime = 100,
                LightLevel = 128,
                RetryCount = 3,
                AlignTimeout = 30000
            };
        }

        /// <summary>
        /// 파라미터 복사
        /// </summary>
        public AlignParameters Clone()
        {
            return new AlignParameters
            {
                WaferType = this.WaferType,
                WaferSize = this.WaferSize,
                TargetAngle = this.TargetAngle,
                CenteringMargin = this.CenteringMargin,
                AngleTolerance = this.AngleTolerance,
                CenterTolerance = this.CenterTolerance,
                ScanCount = this.ScanCount,
                ExposureTime = this.ExposureTime,
                LightLevel = this.LightLevel,
                RetryCount = this.RetryCount,
                AlignTimeout = this.AlignTimeout
            };
        }
    }
}
