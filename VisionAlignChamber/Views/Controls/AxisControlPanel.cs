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

            // 버튼 활성화 상태
            bool canMove = _viewModel.IsEnabled && !_viewModel.IsMoving && _viewModel.IsServoOn;
            btnHome.Enabled = canMove;
            btnMove.Enabled = canMove;
            btnJogPlus.Enabled = canMove;
            btnJogMinus.Enabled = canMove;
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

        private void btnJogPlus_Click(object sender, EventArgs e)
        {
            if (_viewModel == null) return;

            if (double.TryParse(txtJogDistance.Text, out double distance))
            {
                _viewModel.JogPlusCommand?.Execute(distance);
            }
        }

        private void btnJogMinus_Click(object sender, EventArgs e)
        {
            if (_viewModel == null) return;

            if (double.TryParse(txtJogDistance.Text, out double distance))
            {
                _viewModel.JogMinusCommand?.Execute(distance);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _viewModel?.StopCommand?.Execute(null);
        }

        private void btnServo_Click(object sender, EventArgs e)
        {
            _viewModel?.ServoToggleCommand?.Execute(null);
        }

        #endregion
    }
}
