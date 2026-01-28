using System;
using System.Drawing;
using VisionAlignChamber.Models;

namespace VisionAlignChamber.Interfaces
{
    /// <summary>
    /// 비전 처리 인터페이스
    /// 웨이퍼 정렬 비전 검사를 위한 추상화 계층
    /// </summary>
    public interface IVisionProcessor : IDisposable
    {
        #region 초기화

        /// <summary>
        /// 비전 프로세서 초기화
        /// </summary>
        /// <returns>성공 여부</returns>
        bool Initialize();

        /// <summary>
        /// 비전 프로세서 종료
        /// </summary>
        void Close();

        /// <summary>
        /// 초기화 상태
        /// </summary>
        bool IsInitialized { get; }

        #endregion

        #region 이미지 관리

        /// <summary>
        /// 이미지 버퍼 추가
        /// </summary>
        /// <param name="width">이미지 너비</param>
        /// <param name="height">이미지 높이</param>
        /// <param name="buffer">이미지 버퍼 (8bit grayscale)</param>
        /// <returns>성공 여부</returns>
        bool AddImage(int width, int height, byte[] buffer);

        /// <summary>
        /// 파일에서 이미지 추가
        /// </summary>
        /// <param name="filePath">이미지 파일 경로</param>
        /// <returns>성공 여부</returns>
        bool AddImageFromFile(string filePath);

        /// <summary>
        /// 폴더에서 모든 이미지 추가
        /// </summary>
        /// <param name="folderPath">이미지 폴더 경로</param>
        /// <returns>성공 여부</returns>
        bool AddImagesFromFolder(string folderPath);

        /// <summary>
        /// 모든 이미지 제거
        /// </summary>
        void ClearImages();

        /// <summary>
        /// 현재 이미지 개수
        /// </summary>
        int ImageCount { get; }

        /// <summary>
        /// 이미지 캔버스 너비
        /// </summary>
        int CanvasWidth { get; }

        /// <summary>
        /// 이미지 캔버스 높이
        /// </summary>
        int CanvasHeight { get; }

        #endregion

        #region 검사 실행

        /// <summary>
        /// 검사 실행
        /// </summary>
        /// <param name="isFlat">플랫 모드 여부 (false: 노치, true: 플랫)</param>
        /// <returns>성공 여부</returns>
        bool ExecuteInspection(bool isFlat);

        /// <summary>
        /// 검사 완료 여부
        /// </summary>
        bool IsInspectionComplete { get; }

        #endregion

        #region 결과

        /// <summary>
        /// 검사 결과 조회
        /// </summary>
        /// <param name="isFlat">플랫 모드 여부</param>
        /// <returns>웨이퍼 정렬 결과</returns>
        WaferAlignResult GetResult(bool isFlat);

        /// <summary>
        /// 결과 이미지 조회 (노치/플랫 표시)
        /// </summary>
        /// <param name="isFlat">플랫 모드 여부</param>
        /// <returns>결과 이미지</returns>
        Bitmap GetResultImage(bool isFlat);

        /// <summary>
        /// 노치 이미지 조회
        /// </summary>
        /// <returns>노치 이미지</returns>
        Bitmap GetNotchImage();

        /// <summary>
        /// 웨이퍼 이미지 조회
        /// </summary>
        /// <param name="isFlat">플랫 모드 여부</param>
        /// <returns>웨이퍼 이미지</returns>
        Bitmap GetWaferImage(bool isFlat);

        /// <summary>
        /// 특정 인덱스 이미지 조회
        /// </summary>
        /// <param name="index">이미지 인덱스</param>
        /// <returns>이미지</returns>
        Bitmap GetImage(int index);

        #endregion

        #region 설정

        /// <summary>
        /// 검사 설정 정보
        /// </summary>
        VisionSettings Settings { get; }

        #endregion
    }

    /// <summary>
    /// 비전 검사 설정
    /// </summary>
    public class VisionSettings
    {
        /// <summary>
        /// 웨이퍼 반지름 (mm)
        /// </summary>
        public double WaferRadius { get; set; } = 150.0;

        /// <summary>
        /// 촬영 각도 간격 (degree)
        /// </summary>
        public double AngleStep { get; set; } = 15.0;

        /// <summary>
        /// 픽셀당 거리 (um/pixel)
        /// </summary>
        public double PerPixel { get; set; } = 7.59878;

        /// <summary>
        /// 이미지 촬영 개수
        /// </summary>
        public int ImageCount { get; set; } = 24;
    }
}
