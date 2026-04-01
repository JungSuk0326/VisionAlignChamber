using System;
using System.Drawing;
using System.Windows.Forms;
using VisionAlignChamber.Core;
using VisionAlignChamber.Log;
using VisionAlignChamber.ViewModels;
using VisionAlignChamber.Models;

namespace VisionAlignChamber.Views.Controls
{
    /// <summary>
    /// Main 탭 패널 - 시퀀스 진행 상황 표시
    /// </summary>
    public partial class MainTabPanel : UserControl
    {
        #region Fields

        private MainTabViewModel _viewModel;

        #endregion

        #region Constructor

        public MainTabPanel()
        {
            InitializeComponent();

            // 시퀀스 로그 구독
            UILogTarget.LogReceived += OnLogReceived;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// ViewModel 바인딩
        /// </summary>
        public void BindViewModel(MainTabViewModel viewModel)
        {
            _viewModel = viewModel;

            if (_viewModel != null)
            {
                // 스텝 목록 바인딩
                RefreshStepList();

                // 이벤트 구독
                _viewModel.PropertyChanged += OnViewModelPropertyChanged;

                // 각 스텝의 PropertyChanged 구독
                foreach (var step in _viewModel.Steps)
                {
                    step.PropertyChanged += OnStepPropertyChanged;
                }

                // 초기 상태 표시
                UpdateStateDisplay();
                UpdateProgressDisplay();
                UpdateResultDisplay();
            }
        }

        #endregion

        #region Event Handlers

        private void OnViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnViewModelPropertyChanged(sender, e)));
                return;
            }

            switch (e.PropertyName)
            {
                case nameof(MainTabViewModel.State):
                case nameof(MainTabViewModel.StateText):
                    UpdateStateDisplay();
                    break;

                case nameof(MainTabViewModel.CurrentStep):
                case nameof(MainTabViewModel.CurrentStepText):
                    lblCurrentStep.Text = _viewModel.CurrentStepText;
                    HighlightCurrentStep();
                    break;

                case nameof(MainTabViewModel.TotalElapsedSeconds):
                case nameof(MainTabViewModel.TotalElapsedText):
                    lblElapsedTime.Text = _viewModel.TotalElapsedText;
                    break;

                case nameof(MainTabViewModel.ProgressPercent):
                    UpdateProgressDisplay();
                    break;

                case nameof(MainTabViewModel.ErrorMessage):
                    if (!string.IsNullOrEmpty(_viewModel.ErrorMessage))
                    {
                        lblStatus.Text = _viewModel.ErrorMessage;
                        lblStatus.ForeColor = Color.Red;
                    }
                    break;

                case nameof(MainTabViewModel.CanStart):
                case nameof(MainTabViewModel.CanStop):
                    btnStart.Enabled = _viewModel.CanStart;
                    btnStop.Enabled = _viewModel.CanStop;
                    break;

                case nameof(MainTabViewModel.LastResult):
                    UpdateResultDisplay();
                    break;
            }
        }

        private void OnStepPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnStepPropertyChanged(sender, e)));
                return;
            }

            RefreshStepList();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _viewModel?.StartCommand?.Execute(null);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _viewModel?.StopCommand?.Execute(null);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            _viewModel?.ResetCommand?.Execute(null);
            RefreshStepList();
            UpdateStateDisplay();
            UpdateProgressDisplay();
        }

        private void chkSkipEddy_CheckedChanged(object sender, EventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.SkipEddy = chkSkipEddy.Checked;
            }
        }

        private void rbWaferType_CheckedChanged(object sender, EventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.IsNotchType = rbNotch.Checked;
            }
        }

        #endregion

        #region Private Methods

        private void RefreshStepList()
        {
            if (_viewModel == null) return;

            listSteps.Items.Clear();

            foreach (var step in _viewModel.Steps)
            {
                var item = new ListViewItem(new string[]
                {
                    step.StatusIcon,
                    step.StepNumber.ToString(),
                    step.StepName,
                    step.Description,
                    step.StatusText
                });

                // 상태별 색상
                switch (step.Status)
                {
                    case StepStatus.Completed:
                        item.ForeColor = Color.Green;
                        break;
                    case StepStatus.Running:
                        item.ForeColor = Color.Blue;
                        item.Font = new Font(item.Font, FontStyle.Bold);
                        break;
                    case StepStatus.Skipped:
                        item.ForeColor = Color.Gray;
                        break;
                    case StepStatus.Error:
                        item.ForeColor = Color.Red;
                        break;
                    default:
                        item.ForeColor = Color.Black;
                        break;
                }

                item.Tag = step;
                listSteps.Items.Add(item);
            }

            HighlightCurrentStep();
        }

        private void HighlightCurrentStep()
        {
            if (_viewModel == null) return;

            int currentIndex = (int)_viewModel.CurrentStep - 1;

            for (int i = 0; i < listSteps.Items.Count; i++)
            {
                if (i == currentIndex && _viewModel.IsRunning)
                {
                    listSteps.Items[i].BackColor = Color.LightYellow;
                }
                else
                {
                    listSteps.Items[i].BackColor = Color.White;
                }
            }
        }

        private void UpdateStateDisplay()
        {
            if (_viewModel == null) return;

            lblStatus.Text = _viewModel.StateText;

            switch (_viewModel.State)
            {
                case VisionAlignerSequence.SequenceState.Running:
                    lblStatus.ForeColor = Color.Blue;
                    break;
                case VisionAlignerSequence.SequenceState.Completed:
                    lblStatus.ForeColor = Color.Green;
                    break;
                case VisionAlignerSequence.SequenceState.Error:
                case VisionAlignerSequence.SequenceState.Aborted:
                    lblStatus.ForeColor = Color.Red;
                    break;
                default:
                    lblStatus.ForeColor = Color.Black;
                    break;
            }

            btnStart.Enabled = _viewModel.CanStart;
            btnStop.Enabled = _viewModel.CanStop;
        }

        private void UpdateProgressDisplay()
        {
            if (_viewModel == null) return;

            progressBar.Value = Math.Min(_viewModel.ProgressPercent, 100);
            lblProgress.Text = $"{_viewModel.ProgressPercent}%";
        }

        private void UpdateResultDisplay()
        {
            if (_viewModel == null) return;

            var result = _viewModel.LastResult;

            if (result.IsValid)
            {
                // Angle
                lblAngleValue.Text = $"{result.AbsAngle:F3}°";

                // Center (X, Y)
                lblCenterValue.Text = $"({result.Wafer.CenterX:F3}, {result.Wafer.CenterY:F3})";

                // Offset
                lblOffsetValue.Text = $"{result.Wafer.TotalOffset:F3} mm";

                // Size (Width x Height)
                lblSizeValue.Text = $"{result.Width:F2} x {result.Height:F2}";

                // Eddy
                lblEddyValue.Text = $"{result.EddyValue:F2}";

                // P/N
                lblPNValue.Text = result.PNValue == 1 ? "P" : result.PNValue == 0 ? "N" : "Fail";
                lblPNValue.ForeColor = result.PNValue == 1 ? Color.Lime : Color.Orange;
            }
            else
            {
                // 결과 없음 - 초기화
                lblAngleValue.Text = "--";
                lblCenterValue.Text = "--";
                lblOffsetValue.Text = "--";
                lblSizeValue.Text = "--";
                lblEddyValue.Text = "--";
                lblPNValue.Text = "--";
                lblPNValue.ForeColor = Color.Cyan;
            }
        }

        private void OnLogReceived(LogEntry entry)
        {
            // Sequence 로거만 필터링
            if (entry.Logger != "Sequence")
                return;

            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnLogReceived(entry)));
                return;
            }

            AppendSequenceLog(entry);
        }

        private void AppendSequenceLog(LogEntry entry)
        {
            // 최대 라인 수 제한 (1000줄)
            const int MaxLines = 1000;

            var logLine = $"[{entry.Timestamp:HH:mm:ss.fff}] [{entry.Level}] {entry.Message}";

            if (txtSequenceLog.Lines.Length >= MaxLines)
            {
                // 오래된 로그 제거
                var lines = new string[MaxLines - 1];
                Array.Copy(txtSequenceLog.Lines, txtSequenceLog.Lines.Length - MaxLines + 1, lines, 0, MaxLines - 1);
                txtSequenceLog.Lines = lines;
            }

            txtSequenceLog.AppendText(logLine + Environment.NewLine);
            txtSequenceLog.SelectionStart = txtSequenceLog.TextLength;
            txtSequenceLog.ScrollToCaret();
        }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // 로그 구독 해제
                UILogTarget.LogReceived -= OnLogReceived;

                if (_viewModel != null)
                {
                    _viewModel.PropertyChanged -= OnViewModelPropertyChanged;

                    foreach (var step in _viewModel.Steps)
                    {
                        step.PropertyChanged -= OnStepPropertyChanged;
                    }
                }

                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
