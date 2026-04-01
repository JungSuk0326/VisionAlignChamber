using eMotion;
using System;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using VisionAlignChamber.Config;
using VisionAlignChamber.Core;
using VisionAlignChamber.Interfaces;
using VisionAlignChamber.Models;
using static System.Net.Mime.MediaTypeNames;

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
        private SerialPort _lightSerial = null;
        private VisionSettings _settings = null;
        private MulticamEx _grabber = null;
        private bool _isCameraOpened = false;
        private int _imageWidth = 0;
        private int _imageHeight = 0;
        private bool _inspectionComplete = false;
        private int _imageCount = 0;
        private bool _isLightInitialized = false;
        private bool _isLightOn = false;
        private int _currentLightPower = 80;
        private readonly object _imageLock = new object();

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

        /// <summary>
        /// 카메라(Grabber) 오픈 여부
        /// </summary>
        public bool IsCameraOpened => _isCameraOpened;

        /// <summary>
        /// Grabber Acquisition 활성화 여부
        /// </summary>
        public bool IsGrabberActive => _grabber != null && _grabber.Actived;

        /// <summary>
        /// 이미지 가로 크기 (Grabber에서 획득)
        /// </summary>
        public int ImageWidth => _imageWidth;

        /// <summary>
        /// 이미지 세로 크기 (Grabber에서 획득)
        /// </summary>
        public int ImageHeight => _imageHeight;

        /// <summary>
        /// 조명 초기화 여부
        /// </summary>
        public bool IsLightInitialized => _isLightInitialized;

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
                    WaferRadius = 150.7,
                    AngleStep = 15.0,
                    PerPixel = 131.0,
                    ImageCount = 30
                };

                // DLL의 Setting 프로퍼티에서 값을 가져오기 시도
                // Setting은 struct(값 타입)이므로 null 체크 불필요
                try
                {
                    var setting = _aligner.Setting;
                    _settings.ImageCount = setting.ImageCount;
                    _settings.AngleStep = setting.AngleStep;

                    _aligner.SetConfig(setting);
                }
                catch
                {
                    // Setting 프로퍼티 접근 실패 시 기본값 사용
                }

                // Grabber 생성 (카메라 오픈은 별도)
                bool debug = false;
                //#if DEBUG
                //                debug = true;
                //#endif
                debug = AppSettings.SimulationMode;
                
                _grabber = new MulticamEx(debug);
                _grabber.OnCallback += OnGrabberCallback;
                
                _isInitialized = true;

                // 조명 초기화 (실패해도 비전은 사용 가능)
                InitializeLight();

                // 카메라 자동 오픈 (Settings.ini에서 CamFile 경로 + AutoOpen 설정)
                if (AppSettings.CameraAutoOpen)
                {
                    string camFile = AppSettings.CamFilePath;
                    if (!string.IsNullOrEmpty(camFile) && File.Exists(camFile))
                    {
                        if (OpenCamera(camFile))
                        {
                            ActivateGrabber();
                            System.Diagnostics.Debug.WriteLine($"Camera auto-opened: {camFile}");
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Camera auto-open skipped: CamFile not found ({camFile})");
                    }
                }

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
                CloseCamera();
                CloseLight();
                if (_grabber != null)
                {
                    _grabber.OnCallback -= OnGrabberCallback;
                    _grabber = null;
                }
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

        #region 카메라/Grabber 제어

        /// <summary>
        /// Grabber 이미지 콜백 이벤트
        /// UI에서 라이브 이미지 표시에 사용
        /// </summary>
        public event Action<Bitmap> ImageCaptured;

        /// <summary>
        /// Grabber 콜백 (이미지 수신 시 호출)
        /// </summary>
        private void OnGrabberCallback()
        {
            try
            {
                lock (_imageLock)
                {
                    var srcImage = _grabber?.GetImage();
                    if (srcImage != null)
                    {
                        var image = new Bitmap(srcImage);
                        ImageCaptured?.Invoke(image);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"OnGrabberCallback Error: {ex.Message}");
            }
        }

        /// <summary>
        /// 카메라 오픈 (Grabber Board 초기화)
        /// </summary>
        /// <param name="camFilePath">cam 파일 경로</param>
        /// <param name="boardIndex">보드 번호 (기본 0)</param>
        /// <param name="connector">커넥터 ("M")</param>
        /// <param name="topology">토폴로지 ("MONO_DECA")</param>
        public bool OpenCamera(string camFilePath, uint boardIndex = 0, string connector = "M", string topology = "MONO_DECA")
        {
            if (_grabber == null || _isCameraOpened) return false;

            try
            {
                _grabber.OpenBoard(boardIndex, connector, topology, camFilePath);

                _grabber.GetWidth(out _imageWidth);
                _grabber.GetHeight(out _imageHeight);

                _isCameraOpened = true;
                System.Diagnostics.Debug.WriteLine($"Camera opened: {camFilePath} ({_imageWidth}x{_imageHeight})");
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"OpenCamera Error: {ex.Message}");
                _isCameraOpened = false;
                return false;
            }
        }

        /// <summary>
        /// 카메라 닫기 (Grabber Board 종료)
        /// </summary>
        public void CloseCamera()
        {
            if (_grabber == null || !_isCameraOpened) return;

            try
            {
                if (_grabber.Actived)
                    _grabber.SetAcquisition(0);

                _grabber.CloseBoard();

                _isCameraOpened = false;
                _imageWidth = 0;
                _imageHeight = 0;
                System.Diagnostics.Debug.WriteLine("Camera closed");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CloseCamera Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Grabber Acquisition 활성화
        /// </summary>
        public bool ActivateGrabber()
        {
            if (_grabber == null || !_isCameraOpened) return false;

            try
            {
                _grabber.SetAcquisition(MulticamEx.eState.ACTIVE);
                System.Diagnostics.Debug.WriteLine("Grabber activated");
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ActivateGrabber Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Grabber Acquisition 비활성화
        /// </summary>
        public bool DeactivateGrabber()
        {
            if (_grabber == null || !_isCameraOpened) return false;

            try
            {
                _grabber.SetAcquisition(MulticamEx.eState.IDLE);
                System.Diagnostics.Debug.WriteLine("Grabber deactivated");
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeactivateGrabber Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 수동 Trigger 실행 (단발 촬영)
        /// </summary>
        public bool Trigger()
        {
            if (_grabber == null || !_isCameraOpened) return false;

            try
            {
                _grabber.OnTrigger();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Trigger Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Trigger → GrabDone 대기 → AddItem (async)
        /// 시퀀스 스캔에서 사용: 촬영 후 Aligner에 이미지 추가
        /// </summary>
        /// <param name="ct">취소 토큰</param>
        /// <param name="timeoutMs">GrabDone 대기 타임아웃 (ms)</param>
        public async Task<bool> TriggerAndCaptureAsync(CancellationToken ct, int timeoutMs = 5000)
        {
            if (_grabber == null || !_isCameraOpened || !_grabber.Actived)
                return false;

            try
            {
                // Trigger 발사
                _grabber.OnTrigger();

                // GrabDone 대기 (polling)
                int elapsed = 0;
                const int pollInterval = 10;
                while (!_grabber.GrabDone)
                {
                    ct.ThrowIfCancellationRequested();
                    await Task.Delay(pollInterval, ct);
                    elapsed += pollInterval;
                    if (elapsed >= timeoutMs)
                    {
                        System.Diagnostics.Debug.WriteLine("TriggerAndCapture: GrabDone timeout");
                        return false;
                    }
                }

                // Aligner에 이미지 추가
                _aligner.AddItem(_imageWidth, _imageHeight, _grabber.GetImagePointer());
                _imageCount++;
                _inspectionComplete = false;

                // Local일 때만 캡처 이미지 디스플레이 (비동기, CTC 모드에서는 스킵)
                if (AppState.Current.ControlAuthority == ControlAuthority.Local)
                {
                    var srcImage = _grabber.GetImage();
                    if (srcImage != null)
                    {
                        _ = Task.Run(() =>
                        {
                            lock (_imageLock)
                            {
                                var capturedImage = new Bitmap(srcImage);
                                ImageCaptured?.Invoke(capturedImage);
                            }
                        });
                    }
                }

                return true;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"TriggerAndCapture Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Grabber 이미지 저장
        /// </summary>
        public bool SaveGrabberImage(string filePath)
        {
            if (_grabber == null || !_isCameraOpened) return false;

            try
            {
                _grabber.SaveImage(filePath);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SaveGrabberImage Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Grabber 이미지 저장
        /// </summary>
        public bool SaveAlignImages(string filePath)
        {

            try
            {
                _aligner.SaveImage(filePath);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Save Images Error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Grabber 현재 이미지 가져오기
        /// </summary>
        public Bitmap GetGrabberImage()
        {
            if (_grabber == null || !_isCameraOpened) return null;

            try
            {
                lock (_imageLock)
                {
                    var src = _grabber.GetImage();
                    return src != null ? new Bitmap(src) : null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetGrabberImage Error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Exposure Time 설정
        /// </summary>
        public void SetExposureTime(int value)
        {
            if (_grabber == null || !_isCameraOpened) return;

            try
            {
                _grabber.SetExposureTime(value);
                System.Diagnostics.Debug.WriteLine($"Exposure time set to {value}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SetExposureTime Error: {ex.Message}");
            }
        }

        /// <summary>
        /// TrigLive 모드 설정
        /// </summary>
        public void SetTrigLive(bool enable)
        {
            if (_grabber == null) return;
            _grabber.TrigLive = enable;
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

                if (_imageWidth == 0 && _imageWidth == 0)
                {
                    _imageWidth = _aligner.CanvasX();
                    _imageHeight = _aligner.CanvasY();
                }

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
                    OffAngle = result.Angle,
                    AbsAngle = result.Angle,
                    Width = result.Width,
                    Height = result.Height,        
                    Radius = result.Wafer.Radius,
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
        public bool SetSettings(double angleStep, int imageCnt, double notchOffset = 0.4, double waferRadius = 150.0, double perPixel = 130.0, int noisePixel = 30)
        {
            if (!CheckInitialized()) return false;

            try
            {
                // 로컬 설정 업데이트
                _settings.WaferRadius = waferRadius;
                _settings.AngleStep = angleStep;
                _settings.PerPixel = perPixel;
                _settings.ImageCount = (int)(360.0 / angleStep);

                var setting = _aligner.Setting;
                setting.WaferRadius = waferRadius;
                setting.AngleStep = angleStep;
                setting.ImageCount = imageCnt;
                setting.PerPixel = perPixel;
                setting.NoisePixel = noisePixel;
                setting.NotchOffset = notchOffset;
                //setting.NotchOffset : default = 0.4
                // DLL에 SetConfig 메서드가 있으면 호출 시도
                _aligner.SetConfig(setting, false);
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

        #region 조명 제어 (Direct Serial Port)

        /// <summary>
        /// 조명 초기화 (Settings.ini에서 ComPort 설정 사용)
        /// 직접 SerialPort를 사용하여 조명 컨트롤러와 통신
        /// </summary>
        public bool InitializeLight()
        {
            if (_isLightInitialized)
                return true;

            try
            {
                int comPort = AppSettings.LightComPort;
                if (comPort <= 0)
                {
                    System.Diagnostics.Debug.WriteLine("Light ComPort not configured in Settings.ini");
                    return false;
                }

                // SerialPort 생성 및 설정
                _lightSerial = new SerialPort($"COM{comPort}")
                {
                    BaudRate = 9600,
                    DataBits = 8,
                    Parity = Parity.None,
                    StopBits = StopBits.One,
                    ReadTimeout = 1000,
                    WriteTimeout = 1000
                };

                // COM 포트 열기
                _lightSerial.Open();

                // 초기 Power 설정
                _currentLightPower = AppSettings.LightPower;
                SendLightPower(_currentLightPower);

                _isLightInitialized = true;
                System.Diagnostics.Debug.WriteLine($"Light initialized: COM{comPort}, Power={_currentLightPower}");

                // AutoOn 설정 시 조명 켜기
                if (AppSettings.LightAutoOn)
                {
                    //SetLightOn();
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"InitializeLight Error: {ex.Message}");
                _isLightInitialized = false;
                if (_lightSerial != null && _lightSerial.IsOpen)
                {
                    _lightSerial.Close();
                }
                _lightSerial = null;
                return false;
            }
        }

        /// <summary>
        /// 조명 켜기
        /// </summary>
        public void SetLightOn()
        {
            if (!_isLightInitialized || _lightSerial == null || !_lightSerial.IsOpen) return;

            try
            {
                SendLightOnOff(true);
                _isLightOn = true;
                System.Diagnostics.Debug.WriteLine("Light ON");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SetLightOn Error: {ex.Message}");
            }
        }

        /// <summary>
        /// 조명 끄기
        /// </summary>
        public void SetLightOff()
        {
            if (!_isLightInitialized || _lightSerial == null || !_lightSerial.IsOpen) return;

            try
            {
                SendLightOnOff(false);
                _isLightOn = false;
                System.Diagnostics.Debug.WriteLine("Light OFF");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SetLightOff Error: {ex.Message}");
            }
        }

        /// <summary>
        /// 조명 강도 설정 (1~100)
        /// </summary>
        public void SetLightPower(int power)
        {
            if (!_isLightInitialized || _lightSerial == null || !_lightSerial.IsOpen) return;

            try
            {
                _currentLightPower = Math.Min(Math.Max(power, 1), 100);
                if (_isLightOn)
                {
                    SendLightPower(_currentLightPower);
                }
                System.Diagnostics.Debug.WriteLine($"Light Power set to {_currentLightPower}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SetLightPower Error: {ex.Message}");
            }
        }

        /// <summary>
        /// 조명 On/Off 패킷 전송
        /// 패킷 형식: [STX][0][o/f][ETX] (4바이트)
        /// 'o' = On, 'f' = Off
        /// </summary>
        private void SendLightOnOff(bool on)
        {
            if (_lightSerial == null || !_lightSerial.IsOpen) return;

            int i = 0;
            byte[] packet = new byte[4];
            packet[i++] = 0x02;                         // STX
            packet[i++] = (byte)'0';                    // Channel
            packet[i++] = on ? (byte)'o' : (byte)'f';   // 'o' = On, 'f' = Off
            packet[i++] = 0x03;                         // ETX

            _lightSerial.Write(packet, 0, packet.Length);
        }

        /// <summary>
        /// 조명 컨트롤러에 Power 값 전송
        /// 패킷 형식: [STX][0][w][천][백][십][일][ETX] (8바이트)
        /// </summary>
        /// <param name="percentValue">Power 값 (1~100%)</param>
        private void SendLightPower(int percentValue)
        {
            if (_lightSerial == null || !_lightSerial.IsOpen) return;

            // UI 값(1~100%)을 하드웨어 값(1~1024)으로 변환
            int hwValue = (int)(percentValue / 100.0 * 1024);
            hwValue = Math.Min(Math.Max(hwValue, 1), 1024);

            int i = 0;
            byte[] packet = new byte[10];
            packet[i++] = 0x02;                                                     // STX
            packet[i++] = (byte)'0';                                                // Channel
            packet[i++] = (byte)'w';                                                // Write command
            packet[i++] = (byte)Convert.ToChar(((hwValue / 1000) % 10).ToString()); // 천의 자리
            packet[i++] = (byte)Convert.ToChar(((hwValue / 100) % 10).ToString());  // 백의 자리
            packet[i++] = (byte)Convert.ToChar(((hwValue / 10) % 10).ToString());   // 십의 자리
            packet[i++] = (byte)Convert.ToChar((hwValue % 10).ToString());          // 일의 자리
            packet[i++] = 0x03;                                                     // ETX

            _lightSerial.Write(packet, 0, i);
        }

        /// <summary>
        /// 조명 종료 (COM 포트 닫기)
        /// </summary>
        public void CloseLight()
        {
            if (_lightSerial == null) return;

            try
            {
                if (_isLightInitialized && _lightSerial.IsOpen)
                {
                    SetLightOff();
                }

                if (_lightSerial.IsOpen)
                {
                    _lightSerial.Close();
                }

                _lightSerial.Dispose();
                _lightSerial = null;
                _isLightInitialized = false;
                _isLightOn = false;
                System.Diagnostics.Debug.WriteLine("Light closed");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CloseLight Error: {ex.Message}");
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
