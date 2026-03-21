using System;
using System.Drawing;
using System.Windows.Forms;
using VisionAlignChamber.Config;
using VisionAlignChamber.Hardware.Facade;

namespace VisionAlignChamber.Views.Controls
{
    /// <summary>
    /// 티칭 파라미터 설정 패널
    /// </summary>
    public partial class ParameterPanel : UserControl
    {
        #region Fields

        private TeachingParameter _param;
        private HardwareMapping _hardwareMapping;
        private bool _isLoading = false;

        #endregion

        #region Constructor

        public ParameterPanel()
        {
            InitializeComponent();
            _param = TeachingParameter.Instance;
            LoadParametersToUI();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// HardwareMapping 주입 (Save 시 모션 파라미터 동기화용)
        /// </summary>
        public void SetHardwareMapping(HardwareMapping mapping)
        {
            _hardwareMapping = mapping;
        }

        #endregion

        #region Load / Save

        /// <summary>
        /// 파라미터를 UI에 로드
        /// </summary>
        private void LoadParametersToUI()
        {
            _isLoading = true;

            try
            {
                // Chuck Z
                txtChuckZ_Down.Text = _param.ChuckZ_Down.ToString();
                txtChuckZ_Vision.Text = _param.ChuckZ_Vision.ToString();
                txtChuckZ_Eddy.Text = _param.ChuckZ_Eddy.ToString();

                // Centering L
                txtCenterL_Open.Text = _param.CenterL_Open.ToString();
                txtCenterL_MinCtr.Text = _param.CenterL_MinCtr.ToString();

                // Centering R
                txtCenterR_Open.Text = _param.CenterR_Open.ToString();
                txtCenterR_MinCtr.Text = _param.CenterR_MinCtr.ToString();

                // Theta
                txtTheta_Home.Text = _param.Theta_Home.ToString();

                // Vision Scan
                txtScanStepAngle.Text = _param.ScanStepAngle.ToString();
                txtScanImageCount.Text = _param.ScanImageCount.ToString();

                // PN Check
                txtPN_HoldTime.Text = _param.PNHoldTime.ToString();
                txtPN_Timeout.Text = _param.PNTimeout.ToString();
                txtPN_PollInterval.Text = _param.PNPollInterval.ToString();

                // Axis Motion Parameters
                txtChuckZ_Vel.Text = _param.WedgeUpDown.Velocity.ToString();
                txtChuckZ_Acc.Text = _param.WedgeUpDown.Accel.ToString();
                txtChuckZ_Dec.Text = _param.WedgeUpDown.Decel.ToString();

                txtCenterL_Vel.Text = _param.CenteringStage1.Velocity.ToString();
                txtCenterL_Acc.Text = _param.CenteringStage1.Accel.ToString();
                txtCenterL_Dec.Text = _param.CenteringStage1.Decel.ToString();

                txtCenterR_Vel.Text = _param.CenteringStage2.Velocity.ToString();
                txtCenterR_Acc.Text = _param.CenteringStage2.Accel.ToString();
                txtCenterR_Dec.Text = _param.CenteringStage2.Decel.ToString();

                txtTheta_Vel.Text = _param.ChuckRotation.Velocity.ToString();
                txtTheta_Acc.Text = _param.ChuckRotation.Accel.ToString();
                txtTheta_Dec.Text = _param.ChuckRotation.Decel.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadParametersToUI Error: {ex.Message}");
            }

            _isLoading = false;
            UpdateSequencePreview();
        }

        /// <summary>
        /// UI 값을 파라미터에 저장
        /// </summary>
        private void SaveParametersFromUI()
        {
            try
            {
                // Chuck Z
                if (!double.TryParse(txtChuckZ_Down.Text, out double chuckZDown) ||
                    !double.TryParse(txtChuckZ_Vision.Text, out double chuckZVision) ||
                    !double.TryParse(txtChuckZ_Eddy.Text, out double chuckZEddy))
                {
                    MessageBox.Show("Chuck Z 값이 올바르지 않습니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Centering L
                if (!double.TryParse(txtCenterL_Open.Text, out double centerLOpen) ||
                    !double.TryParse(txtCenterL_MinCtr.Text, out double centerLMinCtr))
                {
                    MessageBox.Show("Centering L 값이 올바르지 않습니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Centering R
                if (!double.TryParse(txtCenterR_Open.Text, out double centerROpen) ||
                    !double.TryParse(txtCenterR_MinCtr.Text, out double centerRMinCtr))
                {
                    MessageBox.Show("Centering R 값이 올바르지 않습니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Theta
                if (!double.TryParse(txtTheta_Home.Text, out double thetaHome))
                {
                    MessageBox.Show("Theta 값이 올바르지 않습니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Vision Scan
                if (!double.TryParse(txtScanStepAngle.Text, out double scanStepAngle) ||
                    !int.TryParse(txtScanImageCount.Text, out int scanImageCount))
                {
                    MessageBox.Show("Vision Scan 값이 올바르지 않습니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 검증 통과 후 파라미터에 반영
                _param.ChuckZ_Down = chuckZDown;
                _param.ChuckZ_Vision = chuckZVision;
                _param.ChuckZ_Eddy = chuckZEddy;

                _param.CenterL_Open = centerLOpen;
                _param.CenterL_MinCtr = centerLMinCtr;

                _param.CenterR_Open = centerROpen;
                _param.CenterR_MinCtr = centerRMinCtr;

                _param.Theta_Home = thetaHome;

                _param.ScanStepAngle = scanStepAngle;
                _param.ScanImageCount = scanImageCount;

                // PN Check
                if (int.TryParse(txtPN_HoldTime.Text, out int pnHoldTime) &&
                    int.TryParse(txtPN_Timeout.Text, out int pnTimeout) &&
                    int.TryParse(txtPN_PollInterval.Text, out int pnPollInterval))
                {
                    _param.PNHoldTime = pnHoldTime;
                    _param.PNTimeout = pnTimeout;
                    _param.PNPollInterval = pnPollInterval;
                }

                // Axis Motion Parameters
                if (double.TryParse(txtChuckZ_Vel.Text, out double czVel) &&
                    double.TryParse(txtChuckZ_Acc.Text, out double czAcc) &&
                    double.TryParse(txtChuckZ_Dec.Text, out double czDec))
                {
                    _param.WedgeUpDown.Velocity = czVel;
                    _param.WedgeUpDown.Accel = czAcc;
                    _param.WedgeUpDown.Decel = czDec;
                }

                if (double.TryParse(txtCenterL_Vel.Text, out double clVel) &&
                    double.TryParse(txtCenterL_Acc.Text, out double clAcc) &&
                    double.TryParse(txtCenterL_Dec.Text, out double clDec))
                {
                    _param.CenteringStage1.Velocity = clVel;
                    _param.CenteringStage1.Accel = clAcc;
                    _param.CenteringStage1.Decel = clDec;
                }

                if (double.TryParse(txtCenterR_Vel.Text, out double crVel) &&
                    double.TryParse(txtCenterR_Acc.Text, out double crAcc) &&
                    double.TryParse(txtCenterR_Dec.Text, out double crDec))
                {
                    _param.CenteringStage2.Velocity = crVel;
                    _param.CenteringStage2.Accel = crAcc;
                    _param.CenteringStage2.Decel = crDec;
                }

                if (double.TryParse(txtTheta_Vel.Text, out double thVel) &&
                    double.TryParse(txtTheta_Acc.Text, out double thAcc) &&
                    double.TryParse(txtTheta_Dec.Text, out double thDec))
                {
                    _param.ChuckRotation.Velocity = thVel;
                    _param.ChuckRotation.Accel = thAcc;
                    _param.ChuckRotation.Decel = thDec;
                }

                _param.Save();

                // HardwareMapping 모션 파라미터 동기화
                if (_hardwareMapping != null)
                {
                    _hardwareMapping.UpdateAxisParameters(VAMotionAxis.WedgeUpDown,
                        _param.WedgeUpDown.Velocity, _param.WedgeUpDown.Accel, _param.WedgeUpDown.Decel);
                    _hardwareMapping.UpdateAxisParameters(VAMotionAxis.CenteringStage_1,
                        _param.CenteringStage1.Velocity, _param.CenteringStage1.Accel, _param.CenteringStage1.Decel);
                    _hardwareMapping.UpdateAxisParameters(VAMotionAxis.CenteringStage_2,
                        _param.CenteringStage2.Velocity, _param.CenteringStage2.Accel, _param.CenteringStage2.Decel);
                    _hardwareMapping.UpdateAxisParameters(VAMotionAxis.ChuckRotation,
                        _param.ChuckRotation.Velocity, _param.ChuckRotation.Accel, _param.ChuckRotation.Decel);
                }

                MessageBox.Show("Parameters saved successfully.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 시퀀스 미리보기 업데이트
        /// </summary>
        private void UpdateSequencePreview()
        {
            if (_isLoading) return;

            try
            {
                if (!TryParseText(txtChuckZ_Down, out double chuckZDown) ||
                    !TryParseText(txtChuckZ_Vision, out double chuckZVision) ||
                    !TryParseText(txtChuckZ_Eddy, out double chuckZEddy) ||
                    !TryParseText(txtCenterL_Open, out double centerLOpen) ||
                    !TryParseText(txtCenterL_MinCtr, out double centerLMinCtr) ||
                    !TryParseText(txtCenterR_Open, out double centerROpen) ||
                    !TryParseText(txtCenterR_MinCtr, out double centerRMinCtr) ||
                    !TryParseText(txtTheta_Home, out double thetaHome))
                {
                    return;
                }

                dgvSequence.Rows.Clear();

                // Step 1: Receive
                dgvSequence.Rows.Add("1", "Receive", chuckZDown, centerLOpen, centerROpen, thetaHome);
                // Step 2: PreCtr (FOV)
                dgvSequence.Rows.Add("2", "PreCtr(FOV)", chuckZDown, centerLMinCtr, centerRMinCtr, thetaHome);
                // Step 3: Ready
                dgvSequence.Rows.Add("3", "Ready", chuckZVision, centerLOpen, centerROpen, thetaHome);
                // Step 4: Scan (0->360, AngleStep x ImageCount)
                dgvSequence.Rows.Add("4", "Scan(xN)", chuckZVision, centerLOpen, centerROpen, "0->360");
                // Step 5: Center(Radius)
                dgvSequence.Rows.Add("5", "Center(Radius)", chuckZDown, "Radius", "Radius", thetaHome);
                // Step 6: Eddy
                dgvSequence.Rows.Add("6", "Eddy", chuckZEddy, centerLOpen, centerROpen, thetaHome);
                // Step 7: ThetaAlign
                dgvSequence.Rows.Add("7", "ThetaAlign", chuckZEddy, centerLOpen, centerROpen, "AbsAngle");
                // Step 8: Release
                dgvSequence.Rows.Add("8", "Release", chuckZDown, centerLOpen, centerROpen, "HOLD");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateSequencePreview Error: {ex.Message}");
            }
        }

        private bool TryParseText(TextBox textBox, out double value)
        {
            return double.TryParse(textBox.Text, out value);
        }

        #endregion

        #region Event Handlers

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveParametersFromUI();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            _param.Load();
            LoadParametersToUI();
            MessageBox.Show("Parameters loaded successfully.", "Load", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateSequencePreview();
        }

        #endregion
    }
}
