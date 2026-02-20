namespace VisionAlignChamber.Views.Controls
{
    partial class MainTabPanel
    {
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.grpSequence = new System.Windows.Forms.GroupBox();
            this.listSteps = new System.Windows.Forms.ListView();
            this.colIcon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.tableLayoutStatus = new System.Windows.Forms.TableLayoutPanel();
            this.lblStatusLabel = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblCurrentStepLabel = new System.Windows.Forms.Label();
            this.lblCurrentStep = new System.Windows.Forms.Label();
            this.lblElapsedLabel = new System.Windows.Forms.Label();
            this.lblElapsedTime = new System.Windows.Forms.Label();
            this.lblProgressLabel = new System.Windows.Forms.Label();
            this.panelProgress = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.grpControl = new System.Windows.Forms.GroupBox();
            this.chkSkipEddy = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblWaferType = new System.Windows.Forms.Label();
            this.rbNotch = new System.Windows.Forms.RadioButton();
            this.rbFlat = new System.Windows.Forms.RadioButton();
            this.grpSequence.SuspendLayout();
            this.grpStatus.SuspendLayout();
            this.tableLayoutStatus.SuspendLayout();
            this.panelProgress.SuspendLayout();
            this.grpControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSequence
            // 
            this.grpSequence.Controls.Add(this.listSteps);
            this.grpSequence.ForeColor = System.Drawing.Color.White;
            this.grpSequence.Location = new System.Drawing.Point(12, 9);
            this.grpSequence.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpSequence.Name = "grpSequence";
            this.grpSequence.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpSequence.Size = new System.Drawing.Size(443, 295);
            this.grpSequence.TabIndex = 0;
            this.grpSequence.TabStop = false;
            this.grpSequence.Text = "시퀀스 진행 상황";
            // 
            // listSteps
            // 
            this.listSteps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIcon,
            this.colNo,
            this.colName,
            this.colDescription,
            this.colStatus});
            this.listSteps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listSteps.FullRowSelect = true;
            this.listSteps.GridLines = true;
            this.listSteps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listSteps.HideSelection = false;
            this.listSteps.Location = new System.Drawing.Point(4, 17);
            this.listSteps.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.listSteps.Name = "listSteps";
            this.listSteps.Size = new System.Drawing.Size(435, 275);
            this.listSteps.TabIndex = 0;
            this.listSteps.UseCompatibleStateImageBehavior = false;
            this.listSteps.View = System.Windows.Forms.View.Details;
            // 
            // colIcon
            // 
            this.colIcon.Text = "";
            this.colIcon.Width = 30;
            // 
            // colNo
            // 
            this.colNo.Text = "No";
            this.colNo.Width = 35;
            // 
            // colName
            // 
            this.colName.Text = "스텝";
            this.colName.Width = 80;
            // 
            // colDescription
            // 
            this.colDescription.Text = "설명";
            this.colDescription.Width = 130;
            // 
            // colStatus
            // 
            this.colStatus.Text = "상태";
            this.colStatus.Width = 90;
            // 
            // grpStatus
            // 
            this.grpStatus.Controls.Add(this.tableLayoutStatus);
            this.grpStatus.ForeColor = System.Drawing.Color.White;
            this.grpStatus.Location = new System.Drawing.Point(467, 9);
            this.grpStatus.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpStatus.Size = new System.Drawing.Size(268, 185);
            this.grpStatus.TabIndex = 1;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "상태 정보";
            // 
            // tableLayoutStatus
            // 
            this.tableLayoutStatus.ColumnCount = 2;
            this.tableLayoutStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93F));
            this.tableLayoutStatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutStatus.Controls.Add(this.lblStatusLabel, 0, 0);
            this.tableLayoutStatus.Controls.Add(this.lblStatus, 1, 0);
            this.tableLayoutStatus.Controls.Add(this.lblCurrentStepLabel, 0, 1);
            this.tableLayoutStatus.Controls.Add(this.lblCurrentStep, 1, 1);
            this.tableLayoutStatus.Controls.Add(this.lblElapsedLabel, 0, 2);
            this.tableLayoutStatus.Controls.Add(this.lblElapsedTime, 1, 2);
            this.tableLayoutStatus.Controls.Add(this.lblProgressLabel, 0, 3);
            this.tableLayoutStatus.Controls.Add(this.panelProgress, 1, 3);
            this.tableLayoutStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutStatus.Location = new System.Drawing.Point(4, 17);
            this.tableLayoutStatus.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tableLayoutStatus.Name = "tableLayoutStatus";
            this.tableLayoutStatus.RowCount = 5;
            this.tableLayoutStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutStatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutStatus.Size = new System.Drawing.Size(260, 165);
            this.tableLayoutStatus.TabIndex = 0;
            // 
            // lblStatusLabel
            // 
            this.lblStatusLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStatusLabel.AutoSize = true;
            this.lblStatusLabel.ForeColor = System.Drawing.Color.White;
            this.lblStatusLabel.Location = new System.Drawing.Point(4, 8);
            this.lblStatusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatusLabel.Name = "lblStatusLabel";
            this.lblStatusLabel.Size = new System.Drawing.Size(33, 12);
            this.lblStatusLabel.TabIndex = 0;
            this.lblStatusLabel.Text = "상태:";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(97, 6);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(31, 15);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "대기";
            // 
            // lblCurrentStepLabel
            // 
            this.lblCurrentStepLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCurrentStepLabel.AutoSize = true;
            this.lblCurrentStepLabel.ForeColor = System.Drawing.Color.White;
            this.lblCurrentStepLabel.Location = new System.Drawing.Point(4, 36);
            this.lblCurrentStepLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentStepLabel.Name = "lblCurrentStepLabel";
            this.lblCurrentStepLabel.Size = new System.Drawing.Size(61, 12);
            this.lblCurrentStepLabel.TabIndex = 2;
            this.lblCurrentStepLabel.Text = "현재 스텝:";
            // 
            // lblCurrentStep
            // 
            this.lblCurrentStep.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCurrentStep.AutoSize = true;
            this.lblCurrentStep.ForeColor = System.Drawing.Color.White;
            this.lblCurrentStep.Location = new System.Drawing.Point(97, 36);
            this.lblCurrentStep.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentStep.Name = "lblCurrentStep";
            this.lblCurrentStep.Size = new System.Drawing.Size(29, 12);
            this.lblCurrentStep.TabIndex = 3;
            this.lblCurrentStep.Text = "대기";
            // 
            // lblElapsedLabel
            // 
            this.lblElapsedLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblElapsedLabel.AutoSize = true;
            this.lblElapsedLabel.ForeColor = System.Drawing.Color.White;
            this.lblElapsedLabel.Location = new System.Drawing.Point(4, 64);
            this.lblElapsedLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblElapsedLabel.Name = "lblElapsedLabel";
            this.lblElapsedLabel.Size = new System.Drawing.Size(61, 12);
            this.lblElapsedLabel.TabIndex = 4;
            this.lblElapsedLabel.Text = "경과 시간:";
            // 
            // lblElapsedTime
            // 
            this.lblElapsedTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblElapsedTime.AutoSize = true;
            this.lblElapsedTime.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElapsedTime.ForeColor = System.Drawing.Color.LightGreen;
            this.lblElapsedTime.Location = new System.Drawing.Point(97, 61);
            this.lblElapsedTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblElapsedTime.Name = "lblElapsedTime";
            this.lblElapsedTime.Size = new System.Drawing.Size(64, 17);
            this.lblElapsedTime.TabIndex = 5;
            this.lblElapsedTime.Text = "00:00.0";
            // 
            // lblProgressLabel
            // 
            this.lblProgressLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblProgressLabel.AutoSize = true;
            this.lblProgressLabel.ForeColor = System.Drawing.Color.White;
            this.lblProgressLabel.Location = new System.Drawing.Point(4, 92);
            this.lblProgressLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProgressLabel.Name = "lblProgressLabel";
            this.lblProgressLabel.Size = new System.Drawing.Size(45, 12);
            this.lblProgressLabel.TabIndex = 6;
            this.lblProgressLabel.Text = "진행률:";
            // 
            // panelProgress
            // 
            this.panelProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panelProgress.Controls.Add(this.progressBar);
            this.panelProgress.Controls.Add(this.lblProgress);
            this.panelProgress.Location = new System.Drawing.Point(97, 87);
            this.panelProgress.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new System.Drawing.Size(159, 21);
            this.panelProgress.TabIndex = 7;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(0, 0);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(115, 21);
            this.progressBar.TabIndex = 0;
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblProgress.ForeColor = System.Drawing.Color.Cyan;
            this.lblProgress.Location = new System.Drawing.Point(118, 3);
            this.lblProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(41, 16);
            this.lblProgress.TabIndex = 1;
            this.lblProgress.Text = "0%";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpControl
            //
            this.grpControl.Controls.Add(this.rbFlat);
            this.grpControl.Controls.Add(this.rbNotch);
            this.grpControl.Controls.Add(this.lblWaferType);
            this.grpControl.Controls.Add(this.chkSkipEddy);
            this.grpControl.Controls.Add(this.btnReset);
            this.grpControl.Controls.Add(this.btnStop);
            this.grpControl.Controls.Add(this.btnStart);
            this.grpControl.ForeColor = System.Drawing.Color.White;
            this.grpControl.Location = new System.Drawing.Point(467, 203);
            this.grpControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpControl.Name = "grpControl";
            this.grpControl.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grpControl.Size = new System.Drawing.Size(268, 125);
            this.grpControl.TabIndex = 2;
            this.grpControl.TabStop = false;
            this.grpControl.Text = "제어";
            // 
            // chkSkipEddy
            // 
            this.chkSkipEddy.AutoSize = true;
            this.chkSkipEddy.ForeColor = System.Drawing.Color.White;
            this.chkSkipEddy.Location = new System.Drawing.Point(18, 78);
            this.chkSkipEddy.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkSkipEddy.Name = "chkSkipEddy";
            this.chkSkipEddy.Size = new System.Drawing.Size(109, 16);
            this.chkSkipEddy.TabIndex = 3;
            this.chkSkipEddy.Text = "Eddy 스텝 스킵";
            this.chkSkipEddy.CheckedChanged += new System.EventHandler(this.chkSkipEddy_CheckedChanged);
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnReset.ForeColor = System.Drawing.Color.Black;
            this.btnReset.Location = new System.Drawing.Point(181, 23);
            this.btnReset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(70, 46);
            this.btnReset.TabIndex = 2;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStop.ForeColor = System.Drawing.Color.Red;
            this.btnStop.Location = new System.Drawing.Point(99, 23);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(70, 46);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStart.ForeColor = System.Drawing.Color.Black;
            this.btnStart.Location = new System.Drawing.Point(18, 23);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(70, 46);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            //
            // lblWaferType
            //
            this.lblWaferType.AutoSize = true;
            this.lblWaferType.ForeColor = System.Drawing.Color.White;
            this.lblWaferType.Location = new System.Drawing.Point(18, 100);
            this.lblWaferType.Name = "lblWaferType";
            this.lblWaferType.Size = new System.Drawing.Size(37, 12);
            this.lblWaferType.TabIndex = 4;
            this.lblWaferType.Text = "Type:";
            //
            // rbNotch
            //
            this.rbNotch.AutoSize = true;
            this.rbNotch.Checked = true;
            this.rbNotch.ForeColor = System.Drawing.Color.White;
            this.rbNotch.Location = new System.Drawing.Point(65, 98);
            this.rbNotch.Name = "rbNotch";
            this.rbNotch.Size = new System.Drawing.Size(57, 16);
            this.rbNotch.TabIndex = 5;
            this.rbNotch.TabStop = true;
            this.rbNotch.Text = "Notch";
            this.rbNotch.CheckedChanged += new System.EventHandler(this.rbWaferType_CheckedChanged);
            //
            // rbFlat
            //
            this.rbFlat.AutoSize = true;
            this.rbFlat.ForeColor = System.Drawing.Color.White;
            this.rbFlat.Location = new System.Drawing.Point(130, 98);
            this.rbFlat.Name = "rbFlat";
            this.rbFlat.Size = new System.Drawing.Size(42, 16);
            this.rbFlat.TabIndex = 6;
            this.rbFlat.Text = "Flat";
            this.rbFlat.CheckedChanged += new System.EventHandler(this.rbWaferType_CheckedChanged);
            //
            // MainTabPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpControl);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.grpSequence);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainTabPanel";
            this.Size = new System.Drawing.Size(747, 314);
            this.grpSequence.ResumeLayout(false);
            this.grpStatus.ResumeLayout(false);
            this.tableLayoutStatus.ResumeLayout(false);
            this.tableLayoutStatus.PerformLayout();
            this.panelProgress.ResumeLayout(false);
            this.grpControl.ResumeLayout(false);
            this.grpControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSequence;
        private System.Windows.Forms.ListView listSteps;
        private System.Windows.Forms.ColumnHeader colIcon;
        private System.Windows.Forms.ColumnHeader colNo;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colDescription;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutStatus;
        private System.Windows.Forms.Label lblStatusLabel;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblCurrentStepLabel;
        private System.Windows.Forms.Label lblCurrentStep;
        private System.Windows.Forms.Label lblElapsedLabel;
        private System.Windows.Forms.Label lblElapsedTime;
        private System.Windows.Forms.Label lblProgressLabel;
        private System.Windows.Forms.Panel panelProgress;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.GroupBox grpControl;
        private System.Windows.Forms.CheckBox chkSkipEddy;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblWaferType;
        private System.Windows.Forms.RadioButton rbNotch;
        private System.Windows.Forms.RadioButton rbFlat;
    }
}
