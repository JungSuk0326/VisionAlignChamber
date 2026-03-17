using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Input;
using VisionAlignChamber.ViewModels.Base;
using VisionAlignChamber.Vision;
using VisionAlignChamber.Models;
using VisionAlignChamber.Core;

namespace VisionAlignChamber.ViewModels
{
    /// <summary>
    /// Vision 제어 ViewModel
    /// </summary>
    public class VisionViewModel : ViewModelBase
    {
        #region Fields

        private readonly VisionAlignWrapper _vision;
        private VisionAlignerSequence _sequence;

        #endregion

        #region Constructor

        public VisionViewModel(VisionAlignWrapper vision, VisionAlignerSequence sequence = null)
        {
            _vision = vision ?? throw new ArgumentNullException(nameof(vision));
            _sequence = sequence;
            InitializeCommands();
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            // EventManager 구독
            EventManager.Subscribe(EventManager.ControlAuthorityChanged, OnControlAuthorityChanged);
            EventManager.Subscribe(EventManager.SystemStateChanged, OnSystemStateChanged);

            // Grabber 이미지 콜백 구독
            _vision.ImageCaptured += OnImageCaptured;

            // Sequence 이벤트 구독
            if (_sequence != null)
            {
                _sequence.StepChanged += OnSequenceStepChanged;
                _sequence.StateChanged += OnSequenceStateChanged;
            }
        }

        private void OnImageCaptured(System.Drawing.Bitmap image)
        {
            CurrentImage = image;
        }

        private void OnSequenceStepChanged(object sender, VisionAlignerSequence.SequenceStep step)
        {
            RunStep = (int)step;
        }

        private void OnSequenceStateChanged(object sender, VisionAlignerSequence.SequenceState state)
        {
            switch (state)
            {
                case VisionAlignerSequence.SequenceState.Completed:
                    RunCount++;
                    IsRunning = false;
                    StatusMessage = $"시퀀스 완료 (RunCount: {RunCount})";
                    RaiseCanExecuteChanged();
                    break;

                case VisionAlignerSequence.SequenceState.Error:
                    IsRunning = false;
                    StatusMessage = $"시퀀스 에러: {_sequence.LastError}";
                    RaiseCanExecuteChanged();
                    break;

                case VisionAlignerSequence.SequenceState.Aborted:
                    IsRunning = false;
                    StatusMessage = "시퀀스 중단됨";
                    RaiseCanExecuteChanged();
                    break;
            }
        }

        private void OnControlAuthorityChanged(object data)
        {
            if (data is ControlAuthority authority)
            {
                // Remote 모드에서는 UI 조작 비활성화
                bool isLocal = (authority == ControlAuthority.Local);
                // CanExecute 재평가
                RaiseCanExecuteChanged();
            }
        }

        private void OnSystemStateChanged(object data)
        {
            var propertyName = data as string;
            if (propertyName == nameof(Core.AppState.IsEmergencyStop) &&
                Core.AppState.Current.IsEmergencyStop)
            {
                // 비상정지 시 Running 중지
                if (IsRunning)
                {
                    ExecuteStopRun();
                }
            }
        }

        #endregion

        #region Status Properties

        private bool _isInitialized;
        public bool IsInitialized
        {
            get => _isInitialized;
            set => SetProperty(ref _isInitialized, value);
        }

        private bool _isInspecting;
        public bool IsInspecting
        {
            get => _isInspecting;
            set => SetProperty(ref _isInspecting, value);
        }

        private bool _isInspectionComplete;
        public bool IsInspectionComplete
        {
            get => _isInspectionComplete;
            set => SetProperty(ref _isInspectionComplete, value);
        }

        private int _imageCount;
        public int ImageCount
        {
            get => _imageCount;
            set => SetProperty(ref _imageCount, value);
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                if (SetProperty(ref _statusMessage, value))
                {
                    // EventManager를 통해 전역 상태 메시지 발행
                    EventManager.Publish(EventManager.StatusMessage, value);
                }
            }
        }

        #endregion

        #region Running Properties

        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        private int _runCount;
        public int RunCount
        {
            get => _runCount;
            set => SetProperty(ref _runCount, value);
        }

        private int _runStep;
        public int RunStep
        {
            get => _runStep;
            set => SetProperty(ref _runStep, value);
        }

        private int _setCount = 24;
        public int SetCount
        {
            get => _setCount;
            set
            {
                if (SetProperty(ref _setCount, value))
                {
                    OnPropertyChanged(nameof(DegPerStep));
                }
            }
        }

        public double DegPerStep => _setCount > 0 ? 360.0 / _setCount : 0;

        #endregion

        #region Camera/Grabber Properties

        private bool _isCameraOpened;
        public bool IsCameraOpened
        {
            get => _isCameraOpened;
            set => SetProperty(ref _isCameraOpened, value);
        }

        private bool _isGrabberActive;
        public bool IsGrabberActive
        {
            get => _isGrabberActive;
            set => SetProperty(ref _isGrabberActive, value);
        }

        private string _camFilePath;
        public string CamFilePath
        {
            get => _camFilePath;
            set => SetProperty(ref _camFilePath, value);
        }

        private bool _isTrigLive;
        public bool IsTrigLive
        {
            get => _isTrigLive;
            set => SetProperty(ref _isTrigLive, value);
        }

        private int _exposureTime = 10000;
        public int ExposureTime
        {
            get => _exposureTime;
            set => SetProperty(ref _exposureTime, value);
        }

        #endregion

        #region Inspection Mode Properties

        private bool _isNotchMode = true;
        public bool IsNotchMode
        {
            get => _isNotchMode;
            set
            {
                if (SetProperty(ref _isNotchMode, value))
                {
                    OnPropertyChanged(nameof(IsFlatMode));
                }
            }
        }

        public bool IsFlatMode
        {
            get => !_isNotchMode;
            set => IsNotchMode = !value;
        }

        #endregion

        #region Result Properties

        private WaferVisionResult _alignResult;
        public WaferVisionResult AlignResult
        {
            get => _alignResult;
            set
            {
                if (SetProperty(ref _alignResult, value))
                {
                    OnPropertyChanged(nameof(OffsetAngle));
                    OnPropertyChanged(nameof(AbsoluteAngle));
                    OnPropertyChanged(nameof(WaferCenterX));
                    OnPropertyChanged(nameof(WaferCenterY));
                    OnPropertyChanged(nameof(IsResultValid));
                }
            }
        }

        public double OffsetAngle => _alignResult.OffAngle;
        public double AbsoluteAngle => _alignResult.AbsAngle;
        public double WaferCenterX => _alignResult.Wafer.CenterX;
        public double WaferCenterY => _alignResult.Wafer.CenterY;
        public bool IsResultValid => _alignResult.IsValid;

        #endregion

        #region Image Properties

        private Bitmap _resultImage;
        public Bitmap ResultImage
        {
            get => _resultImage;
            set => SetProperty(ref _resultImage, value);
        }

        private Bitmap _waferImage;
        public Bitmap WaferImage
        {
            get => _waferImage;
            set => SetProperty(ref _waferImage, value);
        }

        private Bitmap _currentImage;
        public Bitmap CurrentImage
        {
            get => _currentImage;
            set => SetProperty(ref _currentImage, value);
        }

        private int _currentImageIndex;
        public int CurrentImageIndex
        {
            get => _currentImageIndex;
            set
            {
                if (SetProperty(ref _currentImageIndex, value))
                {
                    LoadCurrentImage();
                }
            }
        }

        #endregion

        #region Light Properties

        private bool _isLightInitialized;
        public bool IsLightInitialized
        {
            get => _isLightInitialized;
            set => SetProperty(ref _isLightInitialized, value);
        }

        private bool _isLightOn;
        public bool IsLightOn
        {
            get => _isLightOn;
            set => SetProperty(ref _isLightOn, value);
        }

        private int _lightPower = 80;
        public int LightPower
        {
            get => _lightPower;
            set => SetProperty(ref _lightPower, value);
        }

        #endregion

        #region Commands

        public ICommand InitializeCommand { get; private set; }
        public ICommand LoadImagesCommand { get; private set; }
        public ICommand ClearImagesCommand { get; private set; }
        public ICommand ExecuteInspectionCommand { get; private set; }
        public ICommand PreviousImageCommand { get; private set; }
        public ICommand NextImageCommand { get; private set; }
        public ICommand StartRunCommand { get; private set; }
        public ICommand StopRunCommand { get; private set; }

        // Camera/Grabber Commands
        public ICommand OpenCameraCommand { get; private set; }
        public ICommand CloseCameraCommand { get; private set; }
        public ICommand ActivateGrabberCommand { get; private set; }
        public ICommand DeactivateGrabberCommand { get; private set; }
        public ICommand TriggerCommand { get; private set; }
        public ICommand SaveImageCommand { get; private set; }

        // Camera Option Commands
        public ICommand SetExposureTimeCommand { get; private set; }
        public ICommand SetTrigLiveCommand { get; private set; }

        // Light Commands
        public ICommand LightOnCommand { get; private set; }
        public ICommand LightOffCommand { get; private set; }
        public ICommand SetLightPowerCommand { get; private set; }

        private void InitializeCommands()
        {
            InitializeCommand = new RelayCommand(ExecuteInitialize, () => !IsInitialized);
            LoadImagesCommand = new RelayCommand<string>(ExecuteLoadImages, _ => IsInitialized && !IsInspecting);
            ClearImagesCommand = new RelayCommand(ExecuteClearImages, () => IsInitialized && ImageCount > 0);
            ExecuteInspectionCommand = new RelayCommand(ExecuteInspection, CanExecuteInspection);
            PreviousImageCommand = new RelayCommand(ExecutePreviousImage, () => CurrentImageIndex > 0);
            NextImageCommand = new RelayCommand(ExecuteNextImage, () => CurrentImageIndex < ImageCount - 1);
            StartRunCommand = new RelayCommand(ExecuteStartRun, () => IsInitialized && !IsRunning);
            StopRunCommand = new RelayCommand(ExecuteStopRun, () => IsRunning);

            // Camera/Grabber Commands
            OpenCameraCommand = new RelayCommand<string>(ExecuteOpenCamera, _ => !IsCameraOpened);
            CloseCameraCommand = new RelayCommand(ExecuteCloseCamera, () => IsCameraOpened);
            ActivateGrabberCommand = new RelayCommand(ExecuteActivateGrabber, () => IsCameraOpened && !IsGrabberActive);
            DeactivateGrabberCommand = new RelayCommand(ExecuteDeactivateGrabber, () => IsGrabberActive);
            TriggerCommand = new RelayCommand(ExecuteTrigger, () => IsCameraOpened && IsGrabberActive);
            SaveImageCommand = new RelayCommand<string>(ExecuteSaveImage, _ => IsCameraOpened);

            // Camera Option Commands
            SetExposureTimeCommand = new RelayCommand<int>(ExecuteSetExposureTime, _ => IsCameraOpened);
            SetTrigLiveCommand = new RelayCommand<bool>(ExecuteSetTrigLive, _ => IsCameraOpened);

            // Light Commands
            LightOnCommand = new RelayCommand(ExecuteLightOn, () => IsLightInitialized && !IsLightOn);
            LightOffCommand = new RelayCommand(ExecuteLightOff, () => IsLightInitialized && IsLightOn);
            SetLightPowerCommand = new RelayCommand<int>(ExecuteSetLightPower, _ => IsLightInitialized);
        }

        #endregion

        #region Command Implementations

        private void ExecuteInitialize()
        {
            try
            {
                IsInitialized = _vision.Initialize();
                StatusMessage = IsInitialized ? "Vision 초기화 완료" : "Vision 초기화 실패";
                RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                StatusMessage = $"초기화 오류: {ex.Message}";
            }
        }

        private void ExecuteLoadImages(string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                StatusMessage = "폴더 경로가 지정되지 않았습니다.";
                return;
            }

            try
            {
                bool success = _vision.AddImagesFromFolder(folderPath);
                if (success)
                {
                    ImageCount = _vision.ImageCount;
                    CurrentImageIndex = 0;
                    StatusMessage = $"이미지 {ImageCount}개 로드 완료";
                }
                else
                {
                    StatusMessage = "이미지 로드 실패";
                }
                RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                StatusMessage = $"이미지 로드 오류: {ex.Message}";
            }
        }

        private void ExecuteClearImages()
        {
            _vision.ClearImages();
            ImageCount = 0;
            CurrentImageIndex = 0;
            CurrentImage = null;
            ResultImage = null;
            WaferImage = null;
            AlignResult = WaferVisionResult.Empty;
            StatusMessage = "이미지 클리어 완료";
            RaiseCanExecuteChanged();
        }

        private bool CanExecuteInspection()
        {
            return IsInitialized && !IsInspecting && ImageCount > 0;
        }

        private async void ExecuteInspection()
        {
            try
            {
                IsInspecting = true;
                StatusMessage = "검사 실행 중...";
                RaiseCanExecuteChanged();

                bool isFlat = IsFlatMode;

                // Vision DLL 검사를 백그라운드에서 수행 (UI 블로킹 방지)
                var (success, result, resultImage, waferImage) = await Task.Run(() =>
                {
                    bool ok = _vision.ExecuteInspection(isFlat);
                    if (!ok) return (false, WaferVisionResult.Empty, (Bitmap)null, (Bitmap)null);

                    return (true,
                        _vision.GetResult(isFlat),
                        _vision.GetResultImage(isFlat),
                        _vision.GetWaferImage(isFlat));
                });

                if (success)
                {
                    IsInspectionComplete = true;
                    AlignResult = result;
                    ResultImage = resultImage;
                    WaferImage = waferImage;

                    // AppContext에 검사 결과 저장 (자동으로 InspectionComplete 이벤트 발행)
                    Core.AppState.Current.LastVisionResult = AlignResult;

                    if (AlignResult.IsValid)
                    {
                        StatusMessage = $"검사 완료 - 각도: {OffsetAngle:F3}°";
                    }
                    else
                    {
                        StatusMessage = "검사 완료 - 결과 없음";
                    }
                }
                else
                {
                    StatusMessage = "검사 실패";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"검사 오류: {ex.Message}";
            }
            finally
            {
                IsInspecting = false;
                RaiseCanExecuteChanged();
            }
        }

        private void ExecutePreviousImage()
        {
            if (CurrentImageIndex > 0)
            {
                CurrentImageIndex--;
            }
        }

        private void ExecuteNextImage()
        {
            if (CurrentImageIndex < ImageCount - 1)
            {
                CurrentImageIndex++;
            }
        }

        private async void ExecuteStartRun()
        {
            if (_sequence == null)
            {
                StatusMessage = "시퀀스 모듈이 없습니다.";
                return;
            }

            try
            {
                IsRunning = true;
                RunCount = 0;
                RunStep = 0;
                StatusMessage = $"시퀀스 시작 (Count: {SetCount}, Deg: {DegPerStep:F2})";
                RaiseCanExecuteChanged();

                bool isFlat = IsFlatMode;
                bool result = await Task.Run(() => _sequence.RunScanOnlyAsync(isFlat));

                if (!result && _sequence.State != VisionAlignerSequence.SequenceState.Aborted)
                {
                    StatusMessage = $"시퀀스 실패: {_sequence.LastError}";
                }

                // 결과 반영
                if (result)
                {
                    AlignResult = _sequence.VisionResult;
                    ResultImage = _vision.GetResultImage(isFlat);
                    WaferImage = _vision.GetWaferImage(isFlat);
                    ImageCount = _vision.ImageCount;
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"시퀀스 오류: {ex.Message}";
            }
            finally
            {
                IsRunning = false;
                RunStep = 0;
                RaiseCanExecuteChanged();
            }
        }

        private void ExecuteStopRun()
        {
            _sequence?.Stop();
            IsRunning = false;
            RunStep = 0;
            StatusMessage = "시퀀스 정지";
            RaiseCanExecuteChanged();
        }

        private void ExecuteOpenCamera(string camFilePath)
        {
            try
            {
                if (string.IsNullOrEmpty(camFilePath))
                {
                    StatusMessage = "Cam 파일이 선택되지 않았습니다.";
                    return;
                }

                bool success = _vision.OpenCamera(camFilePath);
                if (success)
                {
                    CamFilePath = camFilePath;
                    IsCameraOpened = true;
                    StatusMessage = $"카메라 오픈: {System.IO.Path.GetFileName(camFilePath)} ({_vision.ImageWidth}x{_vision.ImageHeight})";
                }
                else
                {
                    StatusMessage = "카메라 오픈 실패";
                    IsCameraOpened = false;
                }
                RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                StatusMessage = $"카메라 오픈 오류: {ex.Message}";
                IsCameraOpened = false;
            }
        }

        private void ExecuteCloseCamera()
        {
            try
            {
                _vision.CloseCamera();
                IsCameraOpened = false;
                IsGrabberActive = false;
                CamFilePath = null;
                StatusMessage = "카메라 닫힘";
                RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                StatusMessage = $"카메라 닫기 오류: {ex.Message}";
            }
        }

        private void ExecuteActivateGrabber()
        {
            try
            {
                bool success = _vision.ActivateGrabber();
                IsGrabberActive = success;
                StatusMessage = success ? "Grabber 활성화" : "Grabber 활성화 실패";
                RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Grabber 활성화 오류: {ex.Message}";
                IsGrabberActive = false;
            }
        }

        private void ExecuteDeactivateGrabber()
        {
            try
            {
                _vision.DeactivateGrabber();
                IsGrabberActive = false;
                StatusMessage = "Grabber 비활성화";
                RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Grabber 비활성화 오류: {ex.Message}";
            }
        }

        private void ExecuteTrigger()
        {
            try
            {
                bool success = _vision.Trigger();
                StatusMessage = success ? "Trigger 실행" : "Trigger 실패";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Trigger 오류: {ex.Message}";
            }
        }

        private void ExecuteSaveImage(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    StatusMessage = "파일 경로가 지정되지 않았습니다.";
                    return;
                }

                // Grabber 이미지 저장 우선, 없으면 CurrentImage 저장
                if (_vision.IsCameraOpened)
                {
                    bool success = _vision.SaveGrabberImage(filePath);
                    StatusMessage = success
                        ? $"이미지 저장: {System.IO.Path.GetFileName(filePath)}"
                        : "Grabber 이미지 저장 실패";
                }
                else if (CurrentImage != null)
                {
                    CurrentImage.Save(filePath);
                    StatusMessage = $"이미지 저장: {System.IO.Path.GetFileName(filePath)}";
                }
                else
                {
                    StatusMessage = "저장할 이미지가 없습니다.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"이미지 저장 오류: {ex.Message}";
            }
        }

        private void ExecuteSetExposureTime(int exposureTime)
        {
            try
            {
                _vision.SetExposureTime(exposureTime);
                ExposureTime = exposureTime;
                StatusMessage = $"Exposure: {exposureTime} μs";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Exposure 설정 오류: {ex.Message}";
            }
        }

        private void ExecuteSetTrigLive(bool enable)
        {
            try
            {
                _vision.SetTrigLive(enable);
                IsTrigLive = enable;
                StatusMessage = enable ? "TrigLive ON" : "TrigLive OFF";
            }
            catch (Exception ex)
            {
                StatusMessage = $"TrigLive 설정 오류: {ex.Message}";
            }
        }

        private void ExecuteLightOn()
        {
            try
            {
                _vision.SetLightOn();
                IsLightOn = true;
                StatusMessage = "조명 ON";
                RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                StatusMessage = $"조명 ON 오류: {ex.Message}";
            }
        }

        private void ExecuteLightOff()
        {
            try
            {
                _vision.SetLightOff();
                IsLightOn = false;
                StatusMessage = "조명 OFF";
                RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                StatusMessage = $"조명 OFF 오류: {ex.Message}";
            }
        }

        private void ExecuteSetLightPower(int power)
        {
            try
            {
                _vision.SetLightPower(power);
                LightPower = power;
                StatusMessage = $"조명 Power: {power}%";
            }
            catch (Exception ex)
            {
                StatusMessage = $"조명 Power 설정 오류: {ex.Message}";
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 폴더에서 이미지 로드 (UI에서 호출)
        /// </summary>
        public void LoadImagesFromFolder(string folderPath)
        {
            ExecuteLoadImages(folderPath);
        }

        /// <summary>
        /// 검사 실행 (외부에서 호출)
        /// </summary>
        public WaferVisionResult RunInspection(bool isFlat)
        {
            IsFlatMode = isFlat;
            ExecuteInspection();
            return AlignResult;
        }

        /// <summary>
        /// Sequence 설정 (Initialize 완료 후 호출)
        /// </summary>
        public void SetSequence(VisionAlignerSequence sequence)
        {
            // 기존 구독 해제
            if (_sequence != null)
            {
                _sequence.StepChanged -= OnSequenceStepChanged;
                _sequence.StateChanged -= OnSequenceStateChanged;
            }

            _sequence = sequence;

            // 새 구독
            if (_sequence != null)
            {
                _sequence.StepChanged += OnSequenceStepChanged;
                _sequence.StateChanged += OnSequenceStateChanged;
            }
        }

        /// <summary>
        /// 상태 업데이트
        /// </summary>
        public void UpdateStatus()
        {
            if (_vision != null)
            {
                IsInitialized = _vision.IsInitialized;
                ImageCount = _vision.ImageCount;
                IsInspectionComplete = _vision.IsInspectionComplete;
                IsLightInitialized = _vision.IsLightInitialized;
            }
        }

        /// <summary>
        /// Running Count 설정
        /// </summary>
        public void SetRunningCount(int count)
        {
            if (count > 0 && count <= 360)
            {
                SetCount = count;
            }
        }

        /// <summary>
        /// 조명 토글
        /// </summary>
        public void ToggleLight()
        {
            if (IsLightOn)
                ExecuteLightOff();
            else
                ExecuteLightOn();
        }

        /// <summary>
        /// 조명 Power 설정
        /// </summary>
        public void SetLightPowerValue(int power)
        {
            ExecuteSetLightPower(power);
        }

        /// <summary>
        /// Exposure Time 설정
        /// </summary>
        public void SetExposureTimeValue(int exposureTime)
        {
            ExecuteSetExposureTime(exposureTime);
        }

        /// <summary>
        /// TrigLive 토글
        /// </summary>
        public void SetTrigLiveValue(bool enable)
        {
            ExecuteSetTrigLive(enable);
        }

        #endregion

        #region Private Methods

        private void LoadCurrentImage()
        {
            if (IsInitialized && CurrentImageIndex >= 0 && CurrentImageIndex < ImageCount)
            {
                CurrentImage = _vision.GetImage(CurrentImageIndex);
            }
        }

        private void RaiseCanExecuteChanged()
        {
            ((RelayCommand)InitializeCommand).RaiseCanExecuteChanged();
            ((RelayCommand)ClearImagesCommand).RaiseCanExecuteChanged();
            ((RelayCommand)ExecuteInspectionCommand).RaiseCanExecuteChanged();
            ((RelayCommand)PreviousImageCommand).RaiseCanExecuteChanged();
            ((RelayCommand)NextImageCommand).RaiseCanExecuteChanged();
            ((RelayCommand)StartRunCommand).RaiseCanExecuteChanged();
            ((RelayCommand)StopRunCommand).RaiseCanExecuteChanged();

            // Camera/Grabber Commands
            ((RelayCommand)CloseCameraCommand).RaiseCanExecuteChanged();
            ((RelayCommand)ActivateGrabberCommand).RaiseCanExecuteChanged();
            ((RelayCommand)DeactivateGrabberCommand).RaiseCanExecuteChanged();
            ((RelayCommand)TriggerCommand).RaiseCanExecuteChanged();

            // Light Commands
            ((RelayCommand)LightOnCommand).RaiseCanExecuteChanged();
            ((RelayCommand)LightOffCommand).RaiseCanExecuteChanged();
        }

        #endregion

        #region Dispose

        protected override void OnDisposing()
        {
            // EventManager 구독 해제
            EventManager.Unsubscribe(EventManager.ControlAuthorityChanged, OnControlAuthorityChanged);
            EventManager.Unsubscribe(EventManager.SystemStateChanged, OnSystemStateChanged);

            // Grabber 이미지 콜백 해제
            _vision.ImageCaptured -= OnImageCaptured;

            // Sequence 이벤트 해제
            if (_sequence != null)
            {
                _sequence.StepChanged -= OnSequenceStepChanged;
                _sequence.StateChanged -= OnSequenceStateChanged;
            }

            ResultImage?.Dispose();
            WaferImage?.Dispose();
            CurrentImage?.Dispose();
            base.OnDisposing();
        }

        #endregion
    }
}
