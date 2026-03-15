using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionAlignChamber.Config;
using VisionAlignChamber.Core;
using VisionAlignChamber.Hardware;
using VisionAlignChamber.Hardware.Facade;
using VisionAlignChamber.Interlock;

namespace VisionAlignChamber.Views.Controls
{
    /// <summary>
    /// SetUp 탭 패널 - 개별 동작 테스트
    /// </summary>
    public partial class SetUpPanel : UserControl
    {
        #region Fields

        private VisionAlignerMotion _motion;
        private VisionAlignerSequence _sequence;
        private TeachingParameter _param;
        private CancellationTokenSource _cts;
        private bool _isRunning = false;

        #endregion

        #region Constructor

        public SetUpPanel()
        {
            InitializeComponent();
            _param = TeachingParameter.Instance;
            LoadDefaultValues();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Motion 파사드 설정
        /// </summary>
        public void SetMotion(VisionAlignerMotion motion)
        {
            _motion = motion;
        }

        /// <summary>
        /// Sequence 설정 (자세 테스트용)
        /// </summary>
        public void SetSequence(VisionAlignerSequence sequence)
        {
            _sequence = sequence;
        }

        #endregion

        #region PreCenter Test

        private void LoadDefaultValues()
        {
            // TeachingParameter에서 기본값 로드
            txtCenterL_Pos.Text = _param.CenterL_MinCtr.ToString();
            txtCenterR_Pos.Text = _param.CenterR_MinCtr.ToString();
            txtVelocity.Text = _param.CenteringStage1.Velocity.ToString();
            txtAccel.Text = _param.CenteringStage1.Accel.ToString();
            txtDecel.Text = _param.CenteringStage1.Decel.ToString();

            // 알람 목록 로드
            LoadAlarmList();
        }

        private async void btnPreCenterExecute_Click(object sender, EventArgs e)
        {
            if (_motion == null)
            {
                MessageBox.Show("Motion이 초기화되지 않았습니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_isRunning)
            {
                MessageBox.Show("이미 동작 중입니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 입력값 파싱
            if (!double.TryParse(txtCenterL_Pos.Text, out double centerLPos) ||
                !double.TryParse(txtCenterR_Pos.Text, out double centerRPos) ||
                !double.TryParse(txtVelocity.Text, out double velocity) ||
                !double.TryParse(txtAccel.Text, out double accel) ||
                !double.TryParse(txtDecel.Text, out double decel))
            {
                MessageBox.Show("입력값이 올바르지 않습니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (velocity <= 0 || accel <= 0 || decel <= 0)
            {
                MessageBox.Show("속도/가속도/감속도는 0보다 커야 합니다.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _isRunning = true;
            _cts = new CancellationTokenSource();
            SetPreCenterStatus("Running...", Color.Yellow);
            btnPreCenterExecute.Enabled = false;

            try
            {
                // CenteringStage L 이동
                bool start1 = _motion.MoveAbsolute(VAMotionAxis.CenteringStage_1, centerLPos, velocity, accel, decel);
                // CenteringStage R 이동
                bool start2 = _motion.MoveAbsolute(VAMotionAxis.CenteringStage_2, centerRPos, velocity, accel, decel);

                if (!start1 || !start2)
                {
                    SetPreCenterStatus("Move Start Failed", Color.Red);
                    return;
                }

                // 완료 대기
                var wait1 = _motion.WaitForDoneAsync(VAMotionAxis.CenteringStage_1, 30000, _cts.Token);
                var wait2 = _motion.WaitForDoneAsync(VAMotionAxis.CenteringStage_2, 30000, _cts.Token);
                await Task.WhenAll(wait1, wait2);

                if (wait1.Result && wait2.Result)
                {
                    SetPreCenterStatus("Completed", Color.Lime);
                }
                else
                {
                    SetPreCenterStatus("Timeout / Cancelled", Color.Orange);
                }
            }
            catch (OperationCanceledException)
            {
                SetPreCenterStatus("Stopped", Color.Orange);
            }
            catch (Exception ex)
            {
                SetPreCenterStatus($"Error: {ex.Message}", Color.Red);
            }
            finally
            {
                _isRunning = false;
                btnPreCenterExecute.Enabled = true;
                _cts?.Dispose();
                _cts = null;
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_motion != null)
            {
                _motion.Stop(VAMotionAxis.CenteringStage_1);
                _motion.Stop(VAMotionAxis.CenteringStage_2);
            }

            _cts?.Cancel();
            SetPreCenterStatus("Stopped", Color.Orange);
        }

        private void SetPreCenterStatus(string text, Color color)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => SetPreCenterStatus(text, color)));
                return;
            }

            lblPreCenterStatus.Text = text;
            lblPreCenterStatus.ForeColor = color;
        }

        #endregion

        #region Position Test

        private async void btnPutReady_Click(object sender, EventArgs e)
        {
            await ExecutePositionTestAsync("Put Ready", async () => await _sequence.PrepareForPutAsync());
        }

        private async void btnGetReady_Click(object sender, EventArgs e)
        {
            await ExecutePositionTestAsync("Get Ready", async () => await _sequence.PrepareForGetAsync());
        }

        private async void btnReady_Click(object sender, EventArgs e)
        {
            await ExecutePositionTestAsync("Ready", async () =>
            {
                // ExecuteReadyAsync는 private이므로 Sequence의 public 시퀀스 사용
                // Ready: Chuck Vacuum ON + LiftPin OFF + Centering Open + ChuckZ Vision + Light ON
                return await _sequence.RunStepReadyAsync();
            });
        }

        private async void btnScanTest_Click(object sender, EventArgs e)
        {
            bool isFlat = chkFlatMode.Checked;
            await ExecutePositionTestAsync("Scan", async () => await _sequence.RunScanOnlyAsync(isFlat));
        }

        private async Task ExecutePositionTestAsync(string name, Func<Task<bool>> action)
        {
            if (_sequence == null)
            {
                MessageBox.Show("Sequence가 초기화되지 않았습니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_isRunning)
            {
                MessageBox.Show("이미 동작 중입니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _isRunning = true;
            SetPositionTestStatus($"{name} Running...", Color.Yellow);
            btnPutReady.Enabled = false;
            btnGetReady.Enabled = false;
            btnReady.Enabled = false;
            btnScanTest.Enabled = false;

            try
            {
                bool result = await action();
                if (result)
                {
                    SetPositionTestStatus($"{name} Completed", Color.Lime);
                }
                else
                {
                    SetPositionTestStatus($"{name} Failed", Color.Red);
                }
            }
            catch (Exception ex)
            {
                SetPositionTestStatus($"{name} Error: {ex.Message}", Color.Red);
            }
            finally
            {
                _isRunning = false;
                btnPutReady.Enabled = true;
                btnGetReady.Enabled = true;
                btnReady.Enabled = true;
                btnScanTest.Enabled = true;
            }
        }

        private void SetPositionTestStatus(string text, Color color)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => SetPositionTestStatus(text, color)));
                return;
            }

            lblPositionTestStatus.Text = text;
            lblPositionTestStatus.ForeColor = color;
        }

        #endregion

        #region Alarm Test

        private void LoadAlarmList()
        {
            cboAlarmList.Items.Clear();
            var definitions = InterlockManager.Instance.GetAllDefinitions();
            foreach (var def in definitions)
            {
                cboAlarmList.Items.Add(new AlarmComboItem(def));
            }
            if (cboAlarmList.Items.Count > 0)
                cboAlarmList.SelectedIndex = 0;
        }

        private void btnAlarmRaise_Click(object sender, EventArgs e)
        {
            if (cboAlarmList.SelectedItem is AlarmComboItem item)
            {
                var result = InterlockManager.Instance.RaiseAlarm(item.Definition.Id, "SetUpPanel", "수동 테스트 알람");
                if (result != null)
                {
                    lblAlarmTestStatus.Text = $"Raised: [{item.Definition.Code}] {item.Definition.Name}";
                    lblAlarmTestStatus.ForeColor = Color.Red;
                }
                else
                {
                    lblAlarmTestStatus.Text = "Already active";
                    lblAlarmTestStatus.ForeColor = Color.Orange;
                }
            }
        }

        private void btnAlarmClear_Click(object sender, EventArgs e)
        {
            if (cboAlarmList.SelectedItem is AlarmComboItem item)
            {
                bool cleared = InterlockManager.Instance.ClearAlarm(item.Definition.Id);
                if (cleared)
                {
                    lblAlarmTestStatus.Text = $"Cleared: [{item.Definition.Code}] {item.Definition.Name}";
                    lblAlarmTestStatus.ForeColor = Color.Lime;
                }
                else
                {
                    lblAlarmTestStatus.Text = "Not active";
                    lblAlarmTestStatus.ForeColor = Color.LightGray;
                }
            }
        }

        /// <summary>
        /// 알람 콤보박스 아이템
        /// </summary>
        private class AlarmComboItem
        {
            public InterlockDefinition Definition { get; }

            public AlarmComboItem(InterlockDefinition def)
            {
                Definition = def;
            }

            public override string ToString()
            {
                return $"[{Definition.Code}] {Definition.Name} ({Definition.Severity})";
            }
        }

        #endregion
    }
}
