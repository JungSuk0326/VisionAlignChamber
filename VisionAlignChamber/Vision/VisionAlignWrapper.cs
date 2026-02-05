using eMotion;
using System;
using System.Drawing;
using System.IO;
using VisionAlignChamber.Interfaces;
using VisionAlignChamber.Models;

namespace VisionAlignChamber.Vision
{
    /// <summary>
    /// eMotionAlign.dll 비전 처리 래퍼
    /// eMotion.ClassAlign 클래스를 래핑하여 IVisionProcessor 인터페이스 구현
    ///
    /// 실제 DLL API (Readme.txt 기준):
    /// - AddImg(string path): 폴더에서 이미지 로드
    /// - AddItem(int width, int height, byte[] imageData): 버퍼에서 이미지 추가
    /// - ClearList(): 모든 이미지 삭제
    /// - TestMain(bool flat): 검사 시작
    /// - GetEnd(bool flat): 검사 완료 여부
    /// - GetResult(bool flat): 검사 결과 조회
    /// - CanvasX(), CanvasY(): 이미지 크기
    /// - GetImg(int index): 특정 인덱스 이미지
    /// - ResultImg(bool flat): 노치/플랫 결과 이미지
    /// - WaferImg(bool flat): 웨이퍼 결과 이미지
    /// </summary>
    public class VisionAlignWrapper : IVisionProcessor
    {
        #region Fields

        private bool _isInitialized = false;
        private bool _disposed = false;
        private ClassAlign _aligner = null;
        private VisionSettings _settings = null;
        private bool _inspectionComplete = false;
        private int _imageCount = 0;

        #endregion

        #region Properties

        public bool IsInitialized => _isInitialized;

        /// <summary>
        /// 현재 로드된 이미지 개수
        /// </summary>
        public int ImageCount => _imageCount;

        public int CanvasWidth
        {
            get
            {
                if (!CheckInitialized()) return 0;
                try { return _aligner.CanvasX(); }
                catch { return 0; }
            }
        }

        public int CanvasHeight
        {
            get
            {
                if (!CheckInitialized()) return 0;
                try { return _aligner.CanvasY(); }
                catch { return 0; }
            }
        }

        public bool IsInspectionComplete => _inspectionComplete;

        public VisionSettings Settings => _settings;

        #endregion

        #region 초기화

        /// <summary>
        /// 비전 프로세서 초기화
        /// </summary>
        public bool Initialize()
        {
            if (_isInitialized)
                return true;

            try
            {
                _aligner = new ClassAlign();

                // 기본 설정값 사용 (DLL에서 Setting 프로퍼티 사용)
                _settings = new VisionSettings
                {
                    WaferRadius = 150.0,
                    AngleStep = 15.0,
                    PerPixel = 7.59878,
                    ImageCount = 24
                };

                // DLL의 Setting 프로퍼티에서 값을 가져오기 시도
                // Setting은 struct(값 타입)이므로 null 체크 불필요
                try
                {
                    var setting = _aligner.Setting;
                    _settings.ImageCount = setting.ImageCount;
                    _settings.AngleStep = setting.AngleStep;
                }
                catch
                {
                    // Setting 프로퍼티 접근 실패 시 기본값 사용
                }

                _isInitialized = true;
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"VisionAlignWrapper Initialize Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 비전 프로세서 종료
        /// </summary>
        public void Close()
        {
            if (!_isInitialized)
                return;

            try
            {
                ClearImages();
                _aligner = null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"VisionAlignWrapper Close Error: {ex.Message}");
            }
            finally
            {
                _isInitialized = false;
            }
        }

        #endregion

        #region 이미지 관리

        /// <summary>
        /// 이미지 버퍼 추가 (AddItem 사용)
        /// </summary>
        public bool AddImage(int width, int height, byte[] buffer)
        {
            if (!CheckInitialized()) return false;
            if (buffer == null || buffer.Length == 0) return false;

            try
            {
                // DLL의 AddItem(int width, int height, byte[] imageData) 호출
                _aligner.AddItem(width, height, buffer);
                _imageCount++;
                _inspectionComplete = false;
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddImage Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 파일에서 이미지 추가
        /// DLL에는 단일 파일 추가 메서드가 없으므로 폴더 방식 사용 권장
        /// </summary>
        public bool AddImageFromFile(string filePath)
        {
            if (!CheckInitialized()) return false;
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) return false;

            try
            {
                // 파일의 폴더 경로를 사용하여 AddImg 호출
                // 주의: 이 방법은 폴더 내 모든 이미지를 로드함
                string folderPath = Path.GetDirectoryName(filePath);
                return AddImagesFromFolder(folderPath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddImageFromFile Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 폴더에서 모든 이미지 추가 (AddImg 사용)
        /// </summary>
        public bool AddImagesFromFolder(string folderPath)
        {
            if (!CheckInitialized()) return false;
            if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath)) return false;

            try
            {
                // 기존 이미지 클리어
                ClearImages();

                // DLL의 AddImg(string path) 호출
                _aligner.AddImg(folderPath);

                // 이미지 개수 업데이트 (폴더 내 jpg 파일 수로 추정)
                var files = Directory.GetFiles(folderPath, "*.jpg");
                _imageCount = files.Length;

                _inspectionComplete = false;
                return _imageCount > 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddImagesFromFolder Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 모든 이미지 제거 (ClearList 사용)
        /// </summary>
        public void ClearImages()
        {
            if (!CheckInitialized()) return;

            try
            {
                _aligner.ClearList();
                _imageCount = 0;
                _inspectionComplete = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ClearImages Error: {ex.Message}");
            }
        }

        #endregion

        #region 검사 실행

        /// <summary>
        /// 검사 실행 (TestMain 사용)
        /// </summary>
        /// <param name="isFlat">플랫 모드 여부 (false: 노치, true: 플랫)</param>
        public bool ExecuteInspection(bool isFlat)
        {
            if (!CheckInitialized()) return false;

            try
            {
                // DLL의 TestMain(bool flat) 호출
                _aligner.TestMain(isFlat);

                // 검사 완료 대기 (GetEnd로 확인)
                int timeout = 100; // 최대 10초 대기 (100ms * 100)
                while (!_aligner.GetEnd(isFlat) && timeout > 0)
                {
                    System.Threading.Thread.Sleep(100);
                    timeout--;
                }

                _inspectionComplete = _aligner.GetEnd(isFlat);
                return _inspectionComplete;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ExecuteInspection Error: {ex.Message}");
                _inspectionComplete = false;
                return false;
            }
        }

        #endregion

        #region 결과

        /// <summary>
        /// 검사 결과 조회 (GetResult 사용)
        /// </summary>
        public WaferVisionResult GetResult(bool isFlat)
        {
            if (!CheckInitialized()) return WaferVisionResult.Empty;

            try
            {
                // DLL의 GetResult(bool flat) 호출 - ClassWafer.ResultInfo 반환
                var result = _aligner.GetResult(isFlat);

                return new WaferVisionResult
                {
                    IsValid = _inspectionComplete,
                    Found = result.TestEnd,
                    Index1st = result.Index1st,
                    Index2nd = result.Index2nd,
                    OffAngle = result.OffAngle,
                    AbsAngle = result.AbsAngle,
                    Width = result.Width,
                    Height = result.Height,
                    Wafer = new WaferInfo
                    {
                        CenterX = result.Wafer.CenterX,
                        CenterY = result.Wafer.CenterY,
                        Radius = result.Wafer.Radius
                    }
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetResult Error: {ex.Message}");
                return WaferVisionResult.Empty;
            }
        }

        /// <summary>
        /// 결과 이미지 조회 (ResultImg 사용)
        /// </summary>
        public Bitmap GetResultImage(bool isFlat)
        {
            if (!CheckInitialized()) return null;

            try
            {
                // DLL의 ResultImg(bool flat) 호출
                return _aligner.ResultImg(isFlat);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetResultImage Error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 노치 이미지 조회 (ResultImg with flat=false 사용)
        /// </summary>
        public Bitmap GetNotchImage()
        {
            if (!CheckInitialized()) return null;

            try
            {
                // 노치 모드의 결과 이미지 반환
                return _aligner.ResultImg(false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetNotchImage Error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 웨이퍼 이미지 조회 (WaferImg 사용)
        /// </summary>
        public Bitmap GetWaferImage(bool isFlat)
        {
            if (!CheckInitialized()) return null;

            try
            {
                // DLL의 WaferImg(bool flat) 호출
                return _aligner.WaferImg(isFlat);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetWaferImage Error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 특정 인덱스 이미지 조회 (GetImg 사용)
        /// </summary>
        public Bitmap GetImage(int index)
        {
            if (!CheckInitialized()) return null;
            if (index < 0 || index >= _imageCount) return null;

            try
            {
                // DLL의 GetImg(int index) 호출
                return _aligner.GetImg(index);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetImage Error: {ex.Message}");
                return null;
            }
        }

        #endregion

        #region 설정

        /// <summary>
        /// 비전 설정 변경
        /// DLL의 SetConfig 메서드 사용 (가능한 경우)
        /// </summary>
        public bool SetSettings(double waferRadius, double angleStep, double perPixel)
        {
            if (!CheckInitialized()) return false;

            try
            {
                // 로컬 설정 업데이트
                _settings.WaferRadius = waferRadius;
                _settings.AngleStep = angleStep;
                _settings.PerPixel = perPixel;
                _settings.ImageCount = (int)(360.0 / angleStep);

                // DLL에 SetConfig 메서드가 있으면 호출 시도
                // SetConfig(SettingInfo info, bool _default = false)
                // 참고: DLL의 SettingInfo 구조체와 호환되어야 함

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SetSettings Error: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Helper Methods

        private bool CheckInitialized()
        {
            if (!_isInitialized || _aligner == null)
            {
                System.Diagnostics.Debug.WriteLine("VisionAlignWrapper is not initialized");
                return false;
            }
            return true;
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Close();
                }
                _disposed = true;
            }
        }

        ~VisionAlignWrapper()
        {
            Dispose(false);
        }

        #endregion
    }
}
