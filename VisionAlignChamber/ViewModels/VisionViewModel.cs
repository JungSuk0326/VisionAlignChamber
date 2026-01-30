using System;
using System.Drawing;
using System.Windows.Input;
using VisionAlignChamber.ViewModels.Base;
using VisionAlignChamber.Vision;
using VisionAlignChamber.Models;

namespace VisionAlignChamber.ViewModels
{
    /// <summary>
    /// Vision 제어 ViewModel
    /// </summary>
    public class VisionViewModel : ViewModelBase
    {
        #region Fields

        private readonly VisionAlignWrapper _vision;

        #endregion

        #region Constructor

        public VisionViewModel(VisionAlignWrapper vision)
        {
            _vision = vision ?? throw new ArgumentNullException(nameof(vision));
            InitializeCommands();
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
            set => SetProperty(ref _statusMessage, value);
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

        private WaferAlignResult _alignResult;
        public WaferAlignResult AlignResult
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

        #region Commands

        public ICommand InitializeCommand { get; private set; }
        public ICommand LoadImagesCommand { get; private set; }
        public ICommand ClearImagesCommand { get; private set; }
        public ICommand ExecuteInspectionCommand { get; private set; }
        public ICommand PreviousImageCommand { get; private set; }
        public ICommand NextImageCommand { get; private set; }

        private void InitializeCommands()
        {
            InitializeCommand = new RelayCommand(ExecuteInitialize, () => !IsInitialized);
            LoadImagesCommand = new RelayCommand<string>(ExecuteLoadImages, _ => IsInitialized && !IsInspecting);
            ClearImagesCommand = new RelayCommand(ExecuteClearImages, () => IsInitialized && ImageCount > 0);
            ExecuteInspectionCommand = new RelayCommand(ExecuteInspection, CanExecuteInspection);
            PreviousImageCommand = new RelayCommand(ExecutePreviousImage, () => CurrentImageIndex > 0);
            NextImageCommand = new RelayCommand(ExecuteNextImage, () => CurrentImageIndex < ImageCount - 1);
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
            AlignResult = WaferAlignResult.Empty;
            StatusMessage = "이미지 클리어 완료";
            RaiseCanExecuteChanged();
        }

        private bool CanExecuteInspection()
        {
            return IsInitialized && !IsInspecting && ImageCount > 0;
        }

        private void ExecuteInspection()
        {
            try
            {
                IsInspecting = true;
                StatusMessage = "검사 실행 중...";

                bool isFlat = IsFlatMode;
                bool success = _vision.ExecuteInspection(isFlat);

                if (success)
                {
                    IsInspectionComplete = true;
                    AlignResult = _vision.GetResult(isFlat);
                    ResultImage = _vision.GetResultImage(isFlat);
                    WaferImage = _vision.GetWaferImage(isFlat);

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
        public WaferAlignResult RunInspection(bool isFlat)
        {
            IsFlatMode = isFlat;
            ExecuteInspection();
            return AlignResult;
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
            }
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
        }

        #endregion

        #region Dispose

        protected override void OnDisposing()
        {
            ResultImage?.Dispose();
            WaferImage?.Dispose();
            CurrentImage?.Dispose();
            base.OnDisposing();
        }

        #endregion
    }
}
