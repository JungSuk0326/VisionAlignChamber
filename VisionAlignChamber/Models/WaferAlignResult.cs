using System;

namespace VisionAlignChamber.Models
{
    /// <summary>
    /// 웨이퍼 정렬 검사 결과
    /// </summary>
    public struct WaferAlignResult
    {
        /// <summary>
        /// 결과 유효성
        /// </summary>
        public bool IsValid;

        /// <summary>
        /// 노치/플랫 검출 여부
        /// </summary>
        public bool Found;

        /// <summary>
        /// 1차 검출 이미지 인덱스
        /// </summary>
        public int Index1st;

        /// <summary>
        /// 2차 검출 이미지 인덱스
        /// </summary>
        public int Index2nd;

        /// <summary>
        /// 오프셋 각도 (중심 기준)
        /// </summary>
        public double OffAngle;

        /// <summary>
        /// 절대 각도
        /// </summary>
        public double AbsAngle;

        /// <summary>
        /// 노치/플랫 너비 (mm)
        /// </summary>
        public double Width;

        /// <summary>
        /// 노치/플랫 높이(깊이) (mm)
        /// </summary>
        public double Height;

        /// <summary>
        /// 웨이퍼 정보
        /// </summary>
        public WaferInfo Wafer;

        /// <summary>
        /// 기본값 생성
        /// </summary>
        public static WaferAlignResult Empty => new WaferAlignResult
        {
            IsValid = false,
            Found = false,
            Index1st = -1,
            Index2nd = -1,
            OffAngle = 0,
            AbsAngle = 0,
            Width = 0,
            Height = 0,
            Wafer = WaferInfo.Empty
        };

        public override string ToString()
        {
            return $"Valid={IsValid}, Found={Found}, Angle={AbsAngle:F3}°, Center=({Wafer.CenterX:F3}, {Wafer.CenterY:F3})";
        }
    }

    /// <summary>
    /// 웨이퍼 정보
    /// </summary>
    public struct WaferInfo
    {
        /// <summary>
        /// 웨이퍼 중심 X 오프셋 (mm)
        /// </summary>
        public double CenterX;

        /// <summary>
        /// 웨이퍼 중심 Y 오프셋 (mm)
        /// </summary>
        public double CenterY;

        /// <summary>
        /// 웨이퍼 반지름 (mm)
        /// </summary>
        public double Radius;

        /// <summary>
        /// 총 오프셋 거리 (mm)
        /// </summary>
        public double TotalOffset => Math.Sqrt(CenterX * CenterX + CenterY * CenterY);

        /// <summary>
        /// 오프셋 방향 각도 (degree)
        /// </summary>
        public double DirectionAngle => Math.Atan2(CenterY, CenterX) * 180.0 / Math.PI;

        /// <summary>
        /// 기본값 생성
        /// </summary>
        public static WaferInfo Empty => new WaferInfo
        {
            CenterX = 0,
            CenterY = 0,
            Radius = 0
        };

        public override string ToString()
        {
            return $"Center=({CenterX:F3}, {CenterY:F3}), R={Radius:F3}, Offset={TotalOffset:F3}mm";
        }
    }

    /// <summary>
    /// 노치 정보
    /// </summary>
    public struct NotchInfo
    {
        /// <summary>
        /// 검출 여부
        /// </summary>
        public bool Found;

        /// <summary>
        /// 검출 이미지 인덱스
        /// </summary>
        public int Index;

        /// <summary>
        /// 각도 (degree)
        /// </summary>
        public double Angle;

        /// <summary>
        /// 노치 크기
        /// </summary>
        public double Size;

        /// <summary>
        /// 기본값 생성
        /// </summary>
        public static NotchInfo Empty => new NotchInfo
        {
            Found = false,
            Index = -1,
            Angle = 0,
            Size = 0
        };
    }

    /// <summary>
    /// 중심 정보
    /// </summary>
    public struct CenterInfo
    {
        /// <summary>
        /// 실제 반지름
        /// </summary>
        public double TrueRadius;

        /// <summary>
        /// X 오프셋
        /// </summary>
        public double OffsetX;

        /// <summary>
        /// Y 오프셋
        /// </summary>
        public double OffsetY;

        /// <summary>
        /// 총 오프셋
        /// </summary>
        public double TotalOffset;

        /// <summary>
        /// 방향 각도
        /// </summary>
        public double DirectAngle;

        /// <summary>
        /// 기본값 생성
        /// </summary>
        public static CenterInfo Empty => new CenterInfo
        {
            TrueRadius = 0,
            OffsetX = 0,
            OffsetY = 0,
            TotalOffset = 0,
            DirectAngle = 0
        };
    }
}
