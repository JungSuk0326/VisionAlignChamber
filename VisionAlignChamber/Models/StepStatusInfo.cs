using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VisionAlignChamber.Models
{
    /// <summary>
    /// 스텝 실행 상태
    /// </summary>
    public enum StepStatus
    {
        /// <summary>
        /// 대기
        /// </summary>
        Pending,

        /// <summary>
        /// 진행 중
        /// </summary>
        Running,

        /// <summary>
        /// 완료
        /// </summary>
        Completed,

        /// <summary>
        /// 스킵됨
        /// </summary>
        Skipped,

        /// <summary>
        /// 에러
        /// </summary>
        Error
    }

    /// <summary>
    /// 시퀀스 스텝 상태 정보
    /// </summary>
    public class StepStatusInfo : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Properties

        /// <summary>
        /// 스텝 번호 (1-based)
        /// </summary>
        public int StepNumber { get; set; }

        /// <summary>
        /// 스텝 이름
        /// </summary>
        public string StepName { get; set; }

        /// <summary>
        /// 스텝 설명
        /// </summary>
        public string Description { get; set; }

        private StepStatus _status = StepStatus.Pending;
        /// <summary>
        /// 현재 상태
        /// </summary>
        public StepStatus Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(StatusText));
                    OnPropertyChanged(nameof(StatusIcon));
                    OnPropertyChanged(nameof(IsRunning));
                    OnPropertyChanged(nameof(IsCompleted));
                    OnPropertyChanged(nameof(IsSkipped));
                }
            }
        }

        private double _elapsedSeconds;
        /// <summary>
        /// 소요 시간 (초)
        /// </summary>
        public double ElapsedSeconds
        {
            get => _elapsedSeconds;
            set
            {
                if (Math.Abs(_elapsedSeconds - value) > 0.001)
                {
                    _elapsedSeconds = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ElapsedText));
                }
            }
        }

        private string _errorMessage;
        /// <summary>
        /// 에러 메시지
        /// </summary>
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Computed Properties

        /// <summary>
        /// 상태 텍스트
        /// </summary>
        public string StatusText
        {
            get
            {
                switch (Status)
                {
                    case StepStatus.Pending: return "";
                    case StepStatus.Running: return "진행중...";
                    case StepStatus.Completed: return $"완료 {ElapsedText}";
                    case StepStatus.Skipped: return "Skip";
                    case StepStatus.Error: return "에러";
                    default: return "";
                }
            }
        }

        /// <summary>
        /// 상태 아이콘
        /// </summary>
        public string StatusIcon
        {
            get
            {
                switch (Status)
                {
                    case StepStatus.Pending: return "○";
                    case StepStatus.Running: return "▶";
                    case StepStatus.Completed: return "✓";
                    case StepStatus.Skipped: return "⊘";
                    case StepStatus.Error: return "✗";
                    default: return "○";
                }
            }
        }

        /// <summary>
        /// 소요 시간 텍스트
        /// </summary>
        public string ElapsedText
        {
            get
            {
                if (ElapsedSeconds <= 0) return "";
                return $"{ElapsedSeconds:F1}s";
            }
        }

        /// <summary>
        /// 진행 중 여부
        /// </summary>
        public bool IsRunning => Status == StepStatus.Running;

        /// <summary>
        /// 완료 여부
        /// </summary>
        public bool IsCompleted => Status == StepStatus.Completed;

        /// <summary>
        /// 스킵 여부
        /// </summary>
        public bool IsSkipped => Status == StepStatus.Skipped;

        #endregion

        #region Methods

        /// <summary>
        /// 상태 초기화
        /// </summary>
        public void Reset()
        {
            Status = StepStatus.Pending;
            ElapsedSeconds = 0;
            ErrorMessage = null;
        }

        /// <summary>
        /// 스텝 시작
        /// </summary>
        public void Start()
        {
            Status = StepStatus.Running;
            ElapsedSeconds = 0;
        }

        /// <summary>
        /// 스텝 완료
        /// </summary>
        public void Complete(double elapsedSeconds)
        {
            Status = StepStatus.Completed;
            ElapsedSeconds = elapsedSeconds;
        }

        /// <summary>
        /// 스텝 스킵
        /// </summary>
        public void Skip()
        {
            Status = StepStatus.Skipped;
        }

        /// <summary>
        /// 에러 설정
        /// </summary>
        public void SetError(string message)
        {
            Status = StepStatus.Error;
            ErrorMessage = message;
        }

        #endregion
    }
}
