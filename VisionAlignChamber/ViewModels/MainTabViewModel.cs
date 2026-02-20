using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Input;
using VisionAlignChamber.ViewModels.Base;
using VisionAlignChamber.Models;
using VisionAlignChamber.Core;

namespace VisionAlignChamber.ViewModels
{
    /// <summary>
    /// Main 탭 ViewModel - 시퀀스 진행 상황 표시
    /// </summary>
    public class MainTabViewModel : ViewModelBase
    {
        #region Fields

        private VisionAlignerSequence _sequence;
        private readonly Stopwatch _totalStopwatch = new Stopwatch();
        private readonly Stopwatch _stepStopwatch = new Stopwatch();
        private Timer _elapsedTimer;
        private bool _skipEddy = false;

        #endregion

        #region Properties

        /// <summary>
        /// 스텝 상태 목록
        /// </summary>
        public ObservableCollection<StepStatusInfo> Steps { get; private set; }

        private VisionAlignerSequence.SequenceStep _currentStep;
        /// <summary>
        /// 현재 스텝
        /// </summary>
        public VisionAlignerSequence.SequenceStep CurrentStep
        {
            get => _currentStep;
            private set
            {
                if (SetProperty(ref _currentStep, value))
                {
                    OnPropertyChanged(nameof(CurrentStepText));
                }
            }
        }

        private VisionAlignerSequence.SequenceState _state;
        /// <summary>
        /// 시퀀스 상태
        /// </summary>
        public VisionAlignerSequence.SequenceState State
        {
            get => _state;
            private set
            {
                if (SetProperty(ref _state, value))
                {
                    OnPropertyChanged(nameof(StateText));
                    OnPropertyChanged(nameof(IsRunning));
                    OnPropertyChanged(nameof(CanStart));
                    OnPropertyChanged(nameof(CanStop));
                }
            }
        }

        private double _totalElapsedSeconds;
        /// <summary>
        /// 총 경과 시간 (초)
        /// </summary>
        public double TotalElapsedSeconds
        {
            get => _totalElapsedSeconds;
            private set
            {
                if (SetProperty(ref _totalElapsedSeconds, value))
                {
                    OnPropertyChanged(nameof(TotalElapsedText));
                }
            }
        }

        private int _progressPercent;
        /// <summary>
        /// 진행률 (0-100)
        /// </summary>
        public int ProgressPercent
        {
            get => _progressPercent;
            private set => SetProperty(ref _progressPercent, value);
        }

        private string _errorMessage;
        /// <summary>
        /// 에러 메시지
        /// </summary>
        public string ErrorMessage
        {
            get => _errorMessage;
            private set => SetProperty(ref _errorMessage, value);
        }

        private WaferVisionResult _lastResult;
        /// <summary>
        /// 마지막 검사 결과
        /// </summary>
        public WaferVisionResult LastResult
        {
            get => _lastResult;
            private set => SetProperty(ref _lastResult, value);
        }

        /// <summary>
        /// Eddy 스텝 스킵 여부
        /// </summary>
        public bool SkipEddy
        {
            get => _skipEddy;
            set => SetProperty(ref _skipEddy, value);
        }

        private bool _isNotchType = true;
        /// <summary>
        /// Notch 타입 여부 (false면 Flat 타입)
        /// </summary>
        public bool IsNotchType
        {
            get => _isNotchType;
            set => SetProperty(ref _isNotchType, value);
        }

        #endregion

        #region Computed Properties

        /// <summary>
        /// 현재 스텝 텍스트
        /// </summary>
        public string CurrentStepText
        {
            get
            {
                if (State == VisionAlignerSequence.SequenceState.Idle)
                    return "대기";
                return $"Step {(int)CurrentStep}/7: {GetStepName(CurrentStep)}";
            }
        }

        /// <summary>
        /// 상태 텍스트
        /// </summary>
        public string StateText
        {
            get
            {
                switch (State)
                {
                    case VisionAlignerSequence.SequenceState.Idle: return "대기";
                    case VisionAlignerSequence.SequenceState.Running: return "실행 중";
                    case VisionAlignerSequence.SequenceState.Paused: return "일시정지";
                    case VisionAlignerSequence.SequenceState.Completed: return "완료";
                    case VisionAlignerSequence.SequenceState.Error: return "에러";
                    case VisionAlignerSequence.SequenceState.Aborted: return "중단됨";
                    default: return "";
                }
            }
        }

        /// <summary>
        /// 총 경과 시간 텍스트
        /// </summary>
        public string TotalElapsedText
        {
            get
            {
                var ts = TimeSpan.FromSeconds(TotalElapsedSeconds);
                return $"{ts.Minutes:D2}:{ts.Seconds:D2}.{ts.Milliseconds / 100}";
            }
        }

        /// <summary>
        /// 실행 중 여부
        /// </summary>
        public bool IsRunning => State == VisionAlignerSequence.SequenceState.Running;

        /// <summary>
        /// 시작 가능 여부
        /// </summary>
        public bool CanStart => State == VisionAlignerSequence.SequenceState.Idle ||
                                State == VisionAlignerSequence.SequenceState.Completed ||
                                State == VisionAlignerSequence.SequenceState.Error ||
                                State == VisionAlignerSequence.SequenceState.Aborted;

        /// <summary>
        /// 정지 가능 여부
        /// </summary>
        public bool CanStop => State == VisionAlignerSequence.SequenceState.Running;

        #endregion

        #region Commands

        public ICommand StartCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }

        #endregion

        #region Constructor

        public MainTabViewModel()
        {
            InitializeSteps();
            InitializeCommands();
            InitializeTimer();
        }

        private void InitializeSteps()
        {
            // 7-Step 시퀀스 (Receive/Release는 CTC 상태 전환으로 처리)
            Steps = new ObservableCollection<StepStatusInfo>
            {
                new StepStatusInfo { StepNumber = 1, StepName = "PreCenter", Description = "Pre-Centering" },
                new StepStatusInfo { StepNumber = 2, StepName = "Ready", Description = "스캔 준비" },
                new StepStatusInfo { StepNumber = 3, StepName = "ScanStart", Description = "스캔 시작 위치" },
                new StepStatusInfo { StepNumber = 4, StepName = "Scan", Description = "Vision 스캔" },
                new StepStatusInfo { StepNumber = 5, StepName = "Rewind", Description = "Theta Rewind" },
                new StepStatusInfo { StepNumber = 6, StepName = "Align", Description = "Center + Theta 정렬" },
                new StepStatusInfo { StepNumber = 7, StepName = "Eddy", Description = "Eddy Current 측정" }
            };
        }

        private void InitializeCommands()
        {
            StartCommand = new RelayCommand(ExecuteStart, () => CanStart);
            StopCommand = new RelayCommand(ExecuteStop, () => CanStop);
            ResetCommand = new RelayCommand(ExecuteReset);
        }

        private void InitializeTimer()
        {
            _elapsedTimer = new Timer();
            _elapsedTimer.Interval = 100; // 100ms
            _elapsedTimer.Tick += OnElapsedTimerTick;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sequence 연결
        /// </summary>
        public void Initialize(VisionAlignerSequence sequence)
        {
            if (_sequence != null)
            {
                // 기존 이벤트 해제
                _sequence.StepChanged -= OnStepChanged;
                _sequence.StateChanged -= OnStateChanged;
                _sequence.SequenceCompleted -= OnSequenceCompleted;
                _sequence.ErrorOccurred -= OnErrorOccurred;
            }

            _sequence = sequence;

            if (_sequence != null)
            {
                // 이벤트 구독
                _sequence.StepChanged += OnStepChanged;
                _sequence.StateChanged += OnStateChanged;
                _sequence.SequenceCompleted += OnSequenceCompleted;
                _sequence.ErrorOccurred += OnErrorOccurred;

                // 현재 상태 동기화
                CurrentStep = _sequence.CurrentStep;
                State = _sequence.State;
            }
        }

        /// <summary>
        /// 모든 스텝 초기화
        /// </summary>
        public void ResetAllSteps()
        {
            foreach (var step in Steps)
            {
                step.Reset();
            }
            TotalElapsedSeconds = 0;
            ProgressPercent = 0;
            ErrorMessage = null;
        }

        #endregion

        #region Command Implementations

        private async void ExecuteStart()
        {
            if (_sequence == null) return;

            ResetAllSteps();

            // Eddy 스킵 설정 시 해당 스텝 미리 Skip 표시
            if (SkipEddy && Steps.Count >= 7)
            {
                Steps[6].Skip(); // Eddy는 7번째 (인덱스 6)
            }

            _totalStopwatch.Restart();
            _elapsedTimer.Start();

            // 시퀀스 시작 (비동기) - isFlat, skipEddy 파라미터 전달
            bool isFlat = !IsNotchType;
            await _sequence.RunSequenceAsync(isFlat, SkipEddy);
        }

        private void ExecuteStop()
        {
            _sequence?.Stop();
            _elapsedTimer.Stop();
            _totalStopwatch.Stop();
        }

        private void ExecuteReset()
        {
            ResetAllSteps();
            State = VisionAlignerSequence.SequenceState.Idle;
            CurrentStep = VisionAlignerSequence.SequenceStep.Idle;
        }

        #endregion

        #region Event Handlers

        private void OnStepChanged(object sender, VisionAlignerSequence.SequenceStep newStep)
        {
            // UI 스레드에서 실행
            if (Application.OpenForms.Count > 0 && Application.OpenForms[0].InvokeRequired)
            {
                Application.OpenForms[0].BeginInvoke(new Action(() => HandleStepChanged(newStep)));
            }
            else
            {
                HandleStepChanged(newStep);
            }
        }

        private void HandleStepChanged(VisionAlignerSequence.SequenceStep newStep)
        {
            // 이전 스텝 완료 처리
            if (CurrentStep != VisionAlignerSequence.SequenceStep.Idle &&
                CurrentStep != VisionAlignerSequence.SequenceStep.Complete &&
                CurrentStep != VisionAlignerSequence.SequenceStep.Error)
            {
                int prevIndex = (int)CurrentStep - 1;
                if (prevIndex >= 0 && prevIndex < Steps.Count)
                {
                    var prevStep = Steps[prevIndex];
                    if (prevStep.Status == StepStatus.Running)
                    {
                        prevStep.Complete(_stepStopwatch.Elapsed.TotalSeconds);
                    }
                }
            }

            CurrentStep = newStep;

            // 새 스텝 시작 처리
            if (newStep != VisionAlignerSequence.SequenceStep.Idle &&
                newStep != VisionAlignerSequence.SequenceStep.Complete &&
                newStep != VisionAlignerSequence.SequenceStep.Error)
            {
                int newIndex = (int)newStep - 1;
                if (newIndex >= 0 && newIndex < Steps.Count)
                {
                    var step = Steps[newIndex];

                    // Eddy 스킵 체크
                    if (newStep == VisionAlignerSequence.SequenceStep.Eddy && SkipEddy)
                    {
                        step.Skip();
                    }
                    else if (step.Status != StepStatus.Skipped)
                    {
                        step.Start();
                        _stepStopwatch.Restart();
                    }
                }

                // 진행률 계산
                UpdateProgress(newIndex);
            }
        }

        private void OnStateChanged(object sender, VisionAlignerSequence.SequenceState newState)
        {
            // UI 스레드에서 실행
            if (Application.OpenForms.Count > 0 && Application.OpenForms[0].InvokeRequired)
            {
                Application.OpenForms[0].BeginInvoke(new Action(() => HandleStateChanged(newState)));
            }
            else
            {
                HandleStateChanged(newState);
            }
        }

        private void HandleStateChanged(VisionAlignerSequence.SequenceState newState)
        {
            State = newState;

            if (newState == VisionAlignerSequence.SequenceState.Completed ||
                newState == VisionAlignerSequence.SequenceState.Error ||
                newState == VisionAlignerSequence.SequenceState.Aborted)
            {
                _elapsedTimer.Stop();
                _totalStopwatch.Stop();
                TotalElapsedSeconds = _totalStopwatch.Elapsed.TotalSeconds;

                // 마지막 실행 중인 스텝 완료 처리
                foreach (var step in Steps)
                {
                    if (step.Status == StepStatus.Running)
                    {
                        if (newState == VisionAlignerSequence.SequenceState.Completed)
                        {
                            step.Complete(_stepStopwatch.Elapsed.TotalSeconds);
                        }
                        else
                        {
                            step.SetError(ErrorMessage ?? "중단됨");
                        }
                    }
                }

                if (newState == VisionAlignerSequence.SequenceState.Completed)
                {
                    ProgressPercent = 100;
                }
            }

            // CanExecute 갱신
            ((RelayCommand)StartCommand).RaiseCanExecuteChanged();
            ((RelayCommand)StopCommand).RaiseCanExecuteChanged();
        }

        private void OnSequenceCompleted(object sender, WaferVisionResult result)
        {
            // UI 스레드에서 실행
            if (Application.OpenForms.Count > 0 && Application.OpenForms[0].InvokeRequired)
            {
                Application.OpenForms[0].BeginInvoke(new Action(() => LastResult = result));
            }
            else
            {
                LastResult = result;
            }
        }

        private void OnErrorOccurred(object sender, string error)
        {
            // UI 스레드에서 실행
            if (Application.OpenForms.Count > 0 && Application.OpenForms[0].InvokeRequired)
            {
                Application.OpenForms[0].BeginInvoke(new Action(() => ErrorMessage = error));
            }
            else
            {
                ErrorMessage = error;
            }
        }

        private void OnElapsedTimerTick(object sender, EventArgs e)
        {
            TotalElapsedSeconds = _totalStopwatch.Elapsed.TotalSeconds;
        }

        #endregion

        #region Private Methods

        private void UpdateProgress(int currentIndex)
        {
            // 스킵된 스텝 제외하고 진행률 계산
            int totalSteps = Steps.Count;
            int skippedCount = 0;
            foreach (var step in Steps)
            {
                if (step.Status == StepStatus.Skipped) skippedCount++;
            }

            int effectiveTotal = totalSteps - skippedCount;
            if (effectiveTotal > 0)
            {
                int completedCount = 0;
                foreach (var step in Steps)
                {
                    if (step.Status == StepStatus.Completed) completedCount++;
                }
                ProgressPercent = (int)((completedCount * 100.0) / effectiveTotal);
            }
        }

        private string GetStepName(VisionAlignerSequence.SequenceStep step)
        {
            switch (step)
            {
                case VisionAlignerSequence.SequenceStep.PreCenter: return "PreCenter";
                case VisionAlignerSequence.SequenceStep.Ready: return "Ready";
                case VisionAlignerSequence.SequenceStep.ScanStart: return "ScanStart";
                case VisionAlignerSequence.SequenceStep.Scan: return "Scan";
                case VisionAlignerSequence.SequenceStep.Rewind: return "Rewind";
                case VisionAlignerSequence.SequenceStep.Align: return "Align";
                case VisionAlignerSequence.SequenceStep.Eddy: return "Eddy";
                default: return step.ToString();
            }
        }

        #endregion

        #region Dispose

        protected override void OnDisposing()
        {
            _elapsedTimer?.Stop();
            _elapsedTimer?.Dispose();

            if (_sequence != null)
            {
                _sequence.StepChanged -= OnStepChanged;
                _sequence.StateChanged -= OnStateChanged;
                _sequence.SequenceCompleted -= OnSequenceCompleted;
                _sequence.ErrorOccurred -= OnErrorOccurred;
            }

            base.OnDisposing();
        }

        #endregion
    }
}
