using System;
using System.Drawing;
using System.Windows.Forms;
using VisionAlignChamber.Config;

namespace VisionAlignChamber.Views.Controls
{
    /// <summary>
    /// 티칭 파라미터 설정 패널
    /// </summary>
    public partial class ParameterPanel : UserControl
    {
        #region Fields

        private TeachingParameter _param;
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
                numChuckZ_Down.Value = (decimal)_param.ChuckZ_Down;
                numChuckZ_Vision.Value = (decimal)_param.ChuckZ_Vision;
                numChuckZ_Eddy.Value = (decimal)_param.ChuckZ_Eddy;

                // Centering L
                numCenterL_Open.Value = (decimal)_param.CenterL_Open;
                numCenterL_MinCtr.Value = (decimal)_param.CenterL_MinCtr;

                // Centering R
                numCenterR_Open.Value = (decimal)_param.CenterR_Open;
                numCenterR_MinCtr.Value = (decimal)_param.CenterR_MinCtr;

                // Theta
                numTheta_Home.Value = (decimal)_param.Theta_Home;
                numTheta_ScanStart.Value = (decimal)_param.Theta_ScanStart;
                numTheta_ScanEnd.Value = (decimal)_param.Theta_ScanEnd;

                // Motion
                numVelocity.Value = (decimal)_param.DefaultVelocity;
                numAccel.Value = (decimal)_param.DefaultAccel;
                numDecel.Value = (decimal)_param.DefaultDecel;

                // Vision Scan
                numScanStepAngle.Value = (decimal)_param.ScanStepAngle;
                numScanImageCount.Value = _param.ScanImageCount;

                UpdateSequencePreview();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadParametersToUI Error: {ex.Message}");
            }

            _isLoading = false;
        }

        /// <summary>
        /// UI 값을 파라미터에 저장
        /// </summary>
        private void SaveParametersFromUI()
        {
            try
            {
                // Chuck Z
                _param.ChuckZ_Down = (double)numChuckZ_Down.Value;
                _param.ChuckZ_Vision = (double)numChuckZ_Vision.Value;
                _param.ChuckZ_Eddy = (double)numChuckZ_Eddy.Value;

                // Centering L
                _param.CenterL_Open = (double)numCenterL_Open.Value;
                _param.CenterL_MinCtr = (double)numCenterL_MinCtr.Value;

                // Centering R
                _param.CenterR_Open = (double)numCenterR_Open.Value;
                _param.CenterR_MinCtr = (double)numCenterR_MinCtr.Value;

                // Theta
                _param.Theta_Home = (double)numTheta_Home.Value;
                _param.Theta_ScanStart = (double)numTheta_ScanStart.Value;
                _param.Theta_ScanEnd = (double)numTheta_ScanEnd.Value;

                // Motion
                _param.DefaultVelocity = (double)numVelocity.Value;
                _param.DefaultAccel = (double)numAccel.Value;
                _param.DefaultDecel = (double)numDecel.Value;

                // Vision Scan
                _param.ScanStepAngle = (double)numScanStepAngle.Value;
                _param.ScanImageCount = (int)numScanImageCount.Value;

                _param.Save();
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
                // 시퀀스 테이블 업데이트
                dgvSequence.Rows.Clear();

                // Step 1: Receive
                dgvSequence.Rows.Add("1", "Receive", numChuckZ_Down.Value, numCenterL_Open.Value, numCenterR_Open.Value, numTheta_Home.Value);
                // Step 2: PreCtr (FOV)
                dgvSequence.Rows.Add("2", "PreCtr(FOV)", numChuckZ_Down.Value, numCenterL_MinCtr.Value, numCenterR_MinCtr.Value, numTheta_Home.Value);
                // Step 3: Ready
                dgvSequence.Rows.Add("3", "Ready", numChuckZ_Vision.Value, numCenterL_Open.Value, numCenterR_Open.Value, numTheta_ScanStart.Value);
                // Step 4: ScanStart
                dgvSequence.Rows.Add("4", "ScanStart", numChuckZ_Vision.Value, numCenterL_Open.Value, numCenterR_Open.Value, numTheta_ScanStart.Value);
                // Step 5: Scan(xN)
                dgvSequence.Rows.Add("5", "Scan(xN)", numChuckZ_Vision.Value, numCenterL_Open.Value, numCenterR_Open.Value, "+Step");
                // Step 6: Rewind
                dgvSequence.Rows.Add("6", "Rewind", numChuckZ_Vision.Value, numCenterL_Open.Value, numCenterR_Open.Value, numTheta_ScanStart.Value);
                // Step 7: Center(Radius)
                dgvSequence.Rows.Add("7", "Center(Radius)", numChuckZ_Down.Value, "Radius", "Radius", numTheta_ScanStart.Value);
                // Step 8: Eddy
                dgvSequence.Rows.Add("8", "Eddy", numChuckZ_Eddy.Value, numCenterL_Open.Value, numCenterR_Open.Value, numTheta_ScanStart.Value);
                // Step 9: ThetaAlign
                dgvSequence.Rows.Add("9", "ThetaAlign", numChuckZ_Eddy.Value, numCenterL_Open.Value, numCenterR_Open.Value, "AbsAngle");
                // Step 10: Release
                dgvSequence.Rows.Add("10", "Release", numChuckZ_Down.Value, numCenterL_Open.Value, numCenterR_Open.Value, "HOLD");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateSequencePreview Error: {ex.Message}");
            }
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

        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            UpdateSequencePreview();
        }

        #endregion
    }
}
