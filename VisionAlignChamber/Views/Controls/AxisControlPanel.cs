using System;
using System.Drawing;
using System.Windows.Forms;
using VisionAlignChamber.ViewModels;

namespace VisionAlignChamber.Views.Controls
{
    /// <summary>
    /// 단일 축 제어 UserControl (재사용 가능)
    /// </summary>
    public partial class AxisControlPanel : UserControl
    {
        #region Fields

        private AxisViewModel _viewModel;

        #endregion

        #region Constructor

        public AxisControlPanel()
        {
            InitializeComponent();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// ViewModel 바인딩
        /// </summary>
        public void BindViewModel(AxisViewModel viewModel)
        {
            _viewModel = viewModel;

            if (_viewModel == null) return;

            // 축 이름 설정
            lblAxisName.Text = _viewModel.Name;
            lblUnit.Text = _viewModel.Unit;

            // Jog 파라미터 텍스트박스 초기화
            txtJogVelocity.Text = _viewModel.JogVelocity.ToString("F0");
            txtJogAccel.Text = _viewModel.JogAccel.ToString("F0");
            txtJogDecel.Text = _viewModel.JogDecel.ToString("F0");

            // 초기 상태 업데이트
            UpdateDisplay();
        }

        /// <summary>
        /// UI 업데이트 (Timer에서 호출)
        /// </summary>
        public void UpdateDisplay()
        {
            if (_viewModel == null) return;

            // 위치 표시
            txtPosition.Text = _viewModel.Position.ToString("F2");

            // 이동 상태 표시
            if (_viewModel.IsMoving)
            {
                lblMovingStatus.Text = "Moving";
                lblMovingStatus.ForeColor = Color.Orange;
            }
            else
            {
                lblMovingStatus.Text = "Idle";
                lblMovingStatus.ForeColor = Color.LimeGreen;
            }

            // 홈 상태 표시
            chkHomed.Checked = _viewModel.IsHomed;

            // 알람 상태 표시 (CLR 버튼)
            if (_viewModel.IsAlarm)
            {
                btnAlarmClear.Enabled = true;
                btnAlarmClear.BackColor = Color.Red;
            }
            else
            {
                btnAlarmClear.Enabled = false;
                btnAlarmClear.BackColor = Color.DarkGray;
            }

            // 서보 상태 표시
            if (_viewModel.IsServoOn)
            {
                btnServo.Text = "ON";
                btnServo.BackColor = Color.LimeGreen;
            }
            else
            {
                btnServo.Text = "OFF";
                btnServo.BackColor = Color.Gray;
            }

            // Limit 센서 상태 표시
            lblPlusLimit.BackColor = _viewModel.IsPlusLimit ? Color.Red : Color.DarkGray;
            lblMinusLimit.BackColor = _viewModel.IsMinusLimit ? Color.Red : Color.DarkGray;

            // 버튼 활성화 상태 (알람 시 비활성화)
            bool canMove = _viewModel.IsEnabled && !_viewModel.IsMoving && _viewModel.IsServoOn && !_viewModel.IsAlarm;
            btnHome.Enabled = canMove;
            btnMove.Enabled = canMove;
            //btnJogPlus.Enabled = canMove;
            //btnJogMinus.Enabled = canMove;
        }

        #endregion

        #region Event Handlers

        private void btnHome_Click(object sender, EventArgs e)
        {
            _viewModel?.HomeCommand?.Execute(null);
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            if (_viewModel == null) return;

            if (double.TryParse(txtTargetPosition.Text, out double target))
            {
                _viewModel.TargetPosition = target;
                _viewModel.MoveAbsoluteCommand?.Execute(null);
            }
            else
            {
                MessageBox.Show("올바른 위치를 입력하세요.", "입력 오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnJogPlus_MouseDown(object sender, MouseEventArgs e)
        {
            if (_viewModel == null) return;

            // Jog 파라미터 업데이트
            UpdateJogParameters();

            // Jog+ 시작 (속도 제어)
            _viewModel.JogStartPlusCommand?.Execute(null);
        }

        private void btnJogMinus_MouseDown(object sender, MouseEventArgs e)
        {
            if (_viewModel == null) return;

            // Jog 파라미터 업데이트
            UpdateJogParameters();

            // Jog- 시작 (속도 제어)
            _viewModel.JogStartMinusCommand?.Execute(null);
        }

        private void btnJogPlus_MouseUp(object sender, MouseEventArgs e)
        {
            // Jog+ 정지
            _viewModel?.JogStopCommand?.Execute(null);
        }

        private void btnJogPlus_MouseLeave(object sender, EventArgs e)
        {
            // 마우스가 Jog+ 버튼을 벗어나면 정지
            //_viewModel?.JogStopCommand?.Execute(null);
        }

        private void btnJogMinus_MouseUp(object sender, MouseEventArgs e)
        {
            // Jog- 정지
            _viewModel?.JogStopCommand?.Execute(null);
        }

        private void btnJogMinus_MouseLeave(object sender, EventArgs e)
        {
            // 마우스가 Jog- 버튼을 벗어나면 정지
            //_viewModel?.JogStopCommand?.Execute(null);
        }

        private void UpdateJogParameters()
        {
            if (_viewModel == null) return;

            if (double.TryParse(txtJogVelocity.Text, out double velocity))
                _viewModel.JogVelocity = velocity;

            if (double.TryParse(txtJogAccel.Text, out double accel))
                _viewModel.JogAccel = accel;

            if (double.TryParse(txtJogDecel.Text, out double decel))
                _viewModel.JogDecel = decel;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _viewModel?.StopCommand?.Execute(null);
        }

        private void btnServo_Click(object sender, EventArgs e)
        {
            _viewModel?.ServoToggleCommand?.Execute(null);
        }

        private void btnAlarmClear_Click(object sender, EventArgs e)
        {
            _viewModel?.ClearAlarmCommand?.Execute(null);
        }

        #endregion

    }
}
