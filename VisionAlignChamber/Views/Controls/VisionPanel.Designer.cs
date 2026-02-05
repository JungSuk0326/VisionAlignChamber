namespace VisionAlignChamber.Views.Controls
{
    partial class VisionPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.lblImageCountLabel = new System.Windows.Forms.Label();
            this.lblImageCount = new System.Windows.Forms.Label();
            this.lblInitLabel = new System.Windows.Forms.Label();
            this.lblInitStatus = new System.Windows.Forms.Label();
            this.grpMode = new System.Windows.Forms.GroupBox();
            this.rdoFlat = new System.Windows.Forms.RadioButton();
            this.rdoNotch = new System.Windows.Forms.RadioButton();
            this.pnlRunSetting = new System.Windows.Forms.Panel();
            this.grpSetting = new System.Windows.Forms.GroupBox();
            this.lblCamFile = new System.Windows.Forms.Label();
            this.btnFileSave = new System.Windows.Forms.Button();
            this.btnTrigger = new System.Windows.Forms.Button();
            this.btnGrabberActive = new System.Windows.Forms.Button();
            this.btnCamOpen = new System.Windows.Forms.Button();
            this.grpRunning = new System.Windows.Forms.GroupBox();
            this.lblRunStep = new System.Windows.Forms.Label();
            this.txtRunStep = new System.Windows.Forms.TextBox();
            this.lblRunCnt = new System.Windows.Forms.Label();
            this.txtRunCnt = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.lblDeg = new System.Windows.Forms.Label();
            this.txtDeg = new System.Windows.Forms.TextBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.numCount = new System.Windows.Forms.NumericUpDown();
            this.grpControl = new System.Windows.Forms.GroupBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnClearImages = new System.Windows.Forms.Button();
            this.btnLoadImages = new System.Windows.Forms.Button();
            this.btnInitialize = new System.Windows.Forms.Button();
            this.grpResult = new System.Windows.Forms.GroupBox();
            this.listResult = new System.Windows.Forms.ListView();
            this.colNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIndex1st = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIndex2nd = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOffAngle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAbsAngle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colWidth = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colHeight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCenterX = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCenterY = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRadius = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlStatusBar = new System.Windows.Forms.Panel();
            this.lblStatusMessage = new System.Windows.Forms.Label();
            this.grpStatus.SuspendLayout();
            this.grpMode.SuspendLayout();
            this.pnlRunSetting.SuspendLayout();
            this.grpSetting.SuspendLayout();
            this.grpRunning.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).BeginInit();
            this.grpControl.SuspendLayout();
            this.grpResult.SuspendLayout();
            this.pnlStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpStatus
            // 
            this.grpStatus.Controls.Add(this.lblImageCountLabel);
            this.grpStatus.Controls.Add(this.lblImageCount);
            this.grpStatus.Controls.Add(this.lblInitLabel);
            this.grpStatus.Controls.Add(this.lblInitStatus);
            this.grpStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpStatus.Location = new System.Drawing.Point(0, 0);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(350, 50);
            this.grpStatus.TabIndex = 0;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "상태";
            // 
            // lblImageCountLabel
            // 
            this.lblImageCountLabel.AutoSize = true;
            this.lblImageCountLabel.Location = new System.Drawing.Point(180, 22);
            this.lblImageCountLabel.Name = "lblImageCountLabel";
            this.lblImageCountLabel.Size = new System.Drawing.Size(62, 15);
            this.lblImageCountLabel.TabIndex = 2;
            this.lblImageCountLabel.Text = "이미지 수:";
            // 
            // lblImageCount
            // 
            this.lblImageCount.AutoSize = true;
            this.lblImageCount.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblImageCount.Location = new System.Drawing.Point(255, 22);
            this.lblImageCount.Name = "lblImageCount";
            this.lblImageCount.Size = new System.Drawing.Size(14, 15);
            this.lblImageCount.TabIndex = 3;
            this.lblImageCount.Text = "0";
            // 
            // lblInitLabel
            // 
            this.lblInitLabel.AutoSize = true;
            this.lblInitLabel.Location = new System.Drawing.Point(10, 22);
            this.lblInitLabel.Name = "lblInitLabel";
            this.lblInitLabel.Size = new System.Drawing.Size(46, 15);
            this.lblInitLabel.TabIndex = 0;
            this.lblInitLabel.Text = "초기화:";
            // 
            // lblInitStatus
            // 
            this.lblInitStatus.AutoSize = true;
            this.lblInitStatus.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblInitStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblInitStatus.Location = new System.Drawing.Point(70, 22);
            this.lblInitStatus.Name = "lblInitStatus";
            this.lblInitStatus.Size = new System.Drawing.Size(89, 15);
            this.lblInitStatus.TabIndex = 1;
            this.lblInitStatus.Text = "Not Initialized";
            // 
            // grpMode
            // 
            this.grpMode.Controls.Add(this.rdoFlat);
            this.grpMode.Controls.Add(this.rdoNotch);
            this.grpMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpMode.Location = new System.Drawing.Point(0, 50);
            this.grpMode.Name = "grpMode";
            this.grpMode.Size = new System.Drawing.Size(350, 45);
            this.grpMode.TabIndex = 1;
            this.grpMode.TabStop = false;
            this.grpMode.Text = "검사 모드";
            // 
            // rdoFlat
            // 
            this.rdoFlat.AutoSize = true;
            this.rdoFlat.Location = new System.Drawing.Point(100, 20);
            this.rdoFlat.Name = "rdoFlat";
            this.rdoFlat.Size = new System.Drawing.Size(44, 19);
            this.rdoFlat.TabIndex = 1;
            this.rdoFlat.Text = "Flat";
            this.rdoFlat.UseVisualStyleBackColor = true;
            this.rdoFlat.CheckedChanged += new System.EventHandler(this.rdoFlat_CheckedChanged);
            // 
            // rdoNotch
            // 
            this.rdoNotch.AutoSize = true;
            this.rdoNotch.Checked = true;
            this.rdoNotch.Location = new System.Drawing.Point(20, 20);
            this.rdoNotch.Name = "rdoNotch";
            this.rdoNotch.Size = new System.Drawing.Size(58, 19);
            this.rdoNotch.TabIndex = 0;
            this.rdoNotch.TabStop = true;
            this.rdoNotch.Text = "Notch";
            this.rdoNotch.UseVisualStyleBackColor = true;
            this.rdoNotch.CheckedChanged += new System.EventHandler(this.rdoNotch_CheckedChanged);
            // 
            // pnlRunSetting
            // 
            this.pnlRunSetting.Controls.Add(this.grpSetting);
            this.pnlRunSetting.Controls.Add(this.grpRunning);
            this.pnlRunSetting.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRunSetting.Location = new System.Drawing.Point(0, 95);
            this.pnlRunSetting.Name = "pnlRunSetting";
            this.pnlRunSetting.Size = new System.Drawing.Size(350, 130);
            this.pnlRunSetting.TabIndex = 2;
            // 
            // grpSetting
            // 
            this.grpSetting.Controls.Add(this.lblCamFile);
            this.grpSetting.Controls.Add(this.btnFileSave);
            this.grpSetting.Controls.Add(this.btnTrigger);
            this.grpSetting.Controls.Add(this.btnGrabberActive);
            this.grpSetting.Controls.Add(this.btnCamOpen);
            this.grpSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSetting.Location = new System.Drawing.Point(175, 0);
            this.grpSetting.Name = "grpSetting";
            this.grpSetting.Size = new System.Drawing.Size(175, 130);
            this.grpSetting.TabIndex = 1;
            this.grpSetting.TabStop = false;
            this.grpSetting.Text = "Setting";
            // 
            // lblCamFile
            // 
            this.lblCamFile.AutoSize = true;
            this.lblCamFile.ForeColor = System.Drawing.Color.Gray;
            this.lblCamFile.Location = new System.Drawing.Point(8, 95);
            this.lblCamFile.Name = "lblCamFile";
            this.lblCamFile.Size = new System.Drawing.Size(70, 15);
            this.lblCamFile.TabIndex = 4;
            this.lblCamFile.Text = "No cam file";
            // 
            // btnFileSave
            // 
            this.btnFileSave.Location = new System.Drawing.Point(90, 55);
            this.btnFileSave.Name = "btnFileSave";
            this.btnFileSave.Size = new System.Drawing.Size(75, 30);
            this.btnFileSave.TabIndex = 3;
            this.btnFileSave.Text = "File Save";
            this.btnFileSave.UseVisualStyleBackColor = true;
            this.btnFileSave.Click += new System.EventHandler(this.btnFileSave_Click);
            // 
            // btnTrigger
            // 
            this.btnTrigger.Location = new System.Drawing.Point(8, 55);
            this.btnTrigger.Name = "btnTrigger";
            this.btnTrigger.Size = new System.Drawing.Size(75, 30);
            this.btnTrigger.TabIndex = 2;
            this.btnTrigger.Text = "Trigger";
            this.btnTrigger.UseVisualStyleBackColor = true;
            this.btnTrigger.Click += new System.EventHandler(this.btnTrigger_Click);
            // 
            // btnGrabberActive
            // 
            this.btnGrabberActive.Location = new System.Drawing.Point(90, 20);
            this.btnGrabberActive.Name = "btnGrabberActive";
            this.btnGrabberActive.Size = new System.Drawing.Size(75, 30);
            this.btnGrabberActive.TabIndex = 1;
            this.btnGrabberActive.Text = "Active";
            this.btnGrabberActive.UseVisualStyleBackColor = true;
            this.btnGrabberActive.Click += new System.EventHandler(this.btnGrabberActive_Click);
            // 
            // btnCamOpen
            // 
            this.btnCamOpen.Location = new System.Drawing.Point(8, 20);
            this.btnCamOpen.Name = "btnCamOpen";
            this.btnCamOpen.Size = new System.Drawing.Size(75, 30);
            this.btnCamOpen.TabIndex = 0;
            this.btnCamOpen.Text = "Open";
            this.btnCamOpen.UseVisualStyleBackColor = true;
            this.btnCamOpen.Click += new System.EventHandler(this.btnCamOpen_Click);
            // 
            // grpRunning
            // 
            this.grpRunning.Controls.Add(this.lblRunStep);
            this.grpRunning.Controls.Add(this.txtRunStep);
            this.grpRunning.Controls.Add(this.lblRunCnt);
            this.grpRunning.Controls.Add(this.txtRunCnt);
            this.grpRunning.Controls.Add(this.btnStop);
            this.grpRunning.Controls.Add(this.btnRun);
            this.grpRunning.Controls.Add(this.lblDeg);
            this.grpRunning.Controls.Add(this.txtDeg);
            this.grpRunning.Controls.Add(this.lblCount);
            this.grpRunning.Controls.Add(this.numCount);
            this.grpRunning.Dock = System.Windows.Forms.DockStyle.Left;
            this.grpRunning.Location = new System.Drawing.Point(0, 0);
            this.grpRunning.Name = "grpRunning";
            this.grpRunning.Size = new System.Drawing.Size(175, 130);
            this.grpRunning.TabIndex = 0;
            this.grpRunning.TabStop = false;
            this.grpRunning.Text = "Running";
            // 
            // lblRunStep
            // 
            this.lblRunStep.AutoSize = true;
            this.lblRunStep.Location = new System.Drawing.Point(85, 50);
            this.lblRunStep.Name = "lblRunStep";
            this.lblRunStep.Size = new System.Drawing.Size(34, 15);
            this.lblRunStep.TabIndex = 6;
            this.lblRunStep.Text = "Step:";
            // 
            // txtRunStep
            // 
            this.txtRunStep.BackColor = System.Drawing.Color.CornflowerBlue;
            this.txtRunStep.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.txtRunStep.ForeColor = System.Drawing.Color.White;
            this.txtRunStep.Location = new System.Drawing.Point(120, 47);
            this.txtRunStep.Name = "txtRunStep";
            this.txtRunStep.ReadOnly = true;
            this.txtRunStep.Size = new System.Drawing.Size(35, 23);
            this.txtRunStep.TabIndex = 7;
            this.txtRunStep.Text = "0";
            this.txtRunStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblRunCnt
            // 
            this.lblRunCnt.AutoSize = true;
            this.lblRunCnt.Location = new System.Drawing.Point(8, 50);
            this.lblRunCnt.Name = "lblRunCnt";
            this.lblRunCnt.Size = new System.Drawing.Size(29, 15);
            this.lblRunCnt.TabIndex = 4;
            this.lblRunCnt.Text = "Cnt:";
            // 
            // txtRunCnt
            // 
            this.txtRunCnt.BackColor = System.Drawing.Color.CornflowerBlue;
            this.txtRunCnt.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.txtRunCnt.ForeColor = System.Drawing.Color.White;
            this.txtRunCnt.Location = new System.Drawing.Point(40, 47);
            this.txtRunCnt.Name = "txtRunCnt";
            this.txtRunCnt.ReadOnly = true;
            this.txtRunCnt.Size = new System.Drawing.Size(35, 23);
            this.txtRunCnt.TabIndex = 5;
            this.txtRunCnt.Text = "0";
            this.txtRunCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnStop.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(90, 78);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 40);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnRun.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.btnRun.ForeColor = System.Drawing.Color.White;
            this.btnRun.Location = new System.Drawing.Point(8, 78);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 40);
            this.btnRun.TabIndex = 8;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // lblDeg
            // 
            this.lblDeg.AutoSize = true;
            this.lblDeg.Location = new System.Drawing.Point(95, 23);
            this.lblDeg.Name = "lblDeg";
            this.lblDeg.Size = new System.Drawing.Size(32, 15);
            this.lblDeg.TabIndex = 2;
            this.lblDeg.Text = "Deg:";
            // 
            // txtDeg
            // 
            this.txtDeg.BackColor = System.Drawing.Color.Khaki;
            this.txtDeg.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.txtDeg.Location = new System.Drawing.Point(128, 20);
            this.txtDeg.Name = "txtDeg";
            this.txtDeg.ReadOnly = true;
            this.txtDeg.Size = new System.Drawing.Size(40, 23);
            this.txtDeg.TabIndex = 3;
            this.txtDeg.Text = "15.00";
            this.txtDeg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(8, 23);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(29, 15);
            this.lblCount.TabIndex = 0;
            this.lblCount.Text = "Cnt:";
            // 
            // numCount
            // 
            this.numCount.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.numCount.Location = new System.Drawing.Point(40, 20);
            this.numCount.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCount.Name = "numCount";
            this.numCount.Size = new System.Drawing.Size(50, 23);
            this.numCount.TabIndex = 1;
            this.numCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numCount.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numCount.ValueChanged += new System.EventHandler(this.numCount_ValueChanged);
            // 
            // grpControl
            // 
            this.grpControl.Controls.Add(this.btnExecute);
            this.grpControl.Controls.Add(this.btnClearImages);
            this.grpControl.Controls.Add(this.btnLoadImages);
            this.grpControl.Controls.Add(this.btnInitialize);
            this.grpControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpControl.Location = new System.Drawing.Point(0, 225);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(350, 75);
            this.grpControl.TabIndex = 3;
            this.grpControl.TabStop = false;
            this.grpControl.Text = "Control";
            // 
            // btnExecute
            // 
            this.btnExecute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnExecute.ForeColor = System.Drawing.Color.White;
            this.btnExecute.Location = new System.Drawing.Point(259, 20);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 45);
            this.btnExecute.TabIndex = 3;
            this.btnExecute.Text = "검사\r\n실행";
            this.btnExecute.UseVisualStyleBackColor = false;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnClearImages
            // 
            this.btnClearImages.Location = new System.Drawing.Point(176, 20);
            this.btnClearImages.Name = "btnClearImages";
            this.btnClearImages.Size = new System.Drawing.Size(75, 45);
            this.btnClearImages.TabIndex = 2;
            this.btnClearImages.Text = "이미지\r\n클리어";
            this.btnClearImages.UseVisualStyleBackColor = true;
            this.btnClearImages.Click += new System.EventHandler(this.btnClearImages_Click);
            // 
            // btnLoadImages
            // 
            this.btnLoadImages.Location = new System.Drawing.Point(93, 20);
            this.btnLoadImages.Name = "btnLoadImages";
            this.btnLoadImages.Size = new System.Drawing.Size(75, 45);
            this.btnLoadImages.TabIndex = 1;
            this.btnLoadImages.Text = "이미지\r\n로드";
            this.btnLoadImages.UseVisualStyleBackColor = true;
            this.btnLoadImages.Click += new System.EventHandler(this.btnLoadImages_Click);
            // 
            // btnInitialize
            // 
            this.btnInitialize.Location = new System.Drawing.Point(10, 20);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Size = new System.Drawing.Size(75, 45);
            this.btnInitialize.TabIndex = 0;
            this.btnInitialize.Text = "초기화";
            this.btnInitialize.UseVisualStyleBackColor = true;
            this.btnInitialize.Click += new System.EventHandler(this.btnInitialize_Click);
            // 
            // grpResult
            // 
            this.grpResult.Controls.Add(this.listResult);
            this.grpResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpResult.Location = new System.Drawing.Point(0, 300);
            this.grpResult.Name = "grpResult";
            this.grpResult.Size = new System.Drawing.Size(350, 270);
            this.grpResult.TabIndex = 4;
            this.grpResult.TabStop = false;
            this.grpResult.Text = "검사 결과";
            // 
            // listResult
            // 
            this.listResult.BackColor = System.Drawing.Color.NavajoWhite;
            this.listResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNo,
            this.colIndex1st,
            this.colIndex2nd,
            this.colOffAngle,
            this.colAbsAngle,
            this.colWidth,
            this.colHeight,
            this.colCenterX,
            this.colCenterY,
            this.colRadius});
            this.listResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listResult.Font = new System.Drawing.Font("Consolas", 9F);
            this.listResult.FullRowSelect = true;
            this.listResult.GridLines = true;
            this.listResult.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listResult.HideSelection = false;
            this.listResult.Location = new System.Drawing.Point(3, 19);
            this.listResult.Name = "listResult";
            this.listResult.Size = new System.Drawing.Size(344, 248);
            this.listResult.TabIndex = 0;
            this.listResult.UseCompatibleStateImageBehavior = false;
            this.listResult.View = System.Windows.Forms.View.Details;
            // 
            // colNo
            // 
            this.colNo.Text = "No";
            this.colNo.Width = 35;
            // 
            // colIndex1st
            // 
            this.colIndex1st.Text = "Idx1";
            this.colIndex1st.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colIndex1st.Width = 45;
            // 
            // colIndex2nd
            // 
            this.colIndex2nd.Text = "Idx2";
            this.colIndex2nd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colIndex2nd.Width = 45;
            // 
            // colOffAngle
            // 
            this.colOffAngle.Text = "OffAngle";
            this.colOffAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colOffAngle.Width = 70;
            // 
            // colAbsAngle
            // 
            this.colAbsAngle.Text = "AbsAngle";
            this.colAbsAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colAbsAngle.Width = 70;
            // 
            // colWidth
            // 
            this.colWidth.Text = "Width";
            this.colWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colWidth.Width = 65;
            // 
            // colHeight
            // 
            this.colHeight.Text = "Height";
            this.colHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colHeight.Width = 65;
            // 
            // colCenterX
            // 
            this.colCenterX.Text = "CenterX";
            this.colCenterX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colCenterX.Width = 70;
            // 
            // colCenterY
            // 
            this.colCenterY.Text = "CenterY";
            this.colCenterY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colCenterY.Width = 70;
            // 
            // colRadius
            // 
            this.colRadius.Text = "Radius";
            this.colRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colRadius.Width = 65;
            // 
            // pnlStatusBar
            // 
            this.pnlStatusBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.pnlStatusBar.Controls.Add(this.lblStatusMessage);
            this.pnlStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatusBar.Location = new System.Drawing.Point(0, 570);
            this.pnlStatusBar.Name = "pnlStatusBar";
            this.pnlStatusBar.Size = new System.Drawing.Size(350, 30);
            this.pnlStatusBar.TabIndex = 5;
            // 
            // lblStatusMessage
            // 
            this.lblStatusMessage.AutoSize = true;
            this.lblStatusMessage.ForeColor = System.Drawing.Color.White;
            this.lblStatusMessage.Location = new System.Drawing.Point(10, 8);
            this.lblStatusMessage.Name = "lblStatusMessage";
            this.lblStatusMessage.Size = new System.Drawing.Size(39, 15);
            this.lblStatusMessage.TabIndex = 0;
            this.lblStatusMessage.Text = "Ready";
            // 
            // VisionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpResult);
            this.Controls.Add(this.pnlStatusBar);
            this.Controls.Add(this.grpControl);
            this.Controls.Add(this.pnlRunSetting);
            this.Controls.Add(this.grpMode);
            this.Controls.Add(this.grpStatus);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.Name = "VisionPanel";
            this.Size = new System.Drawing.Size(350, 600);
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.grpMode.ResumeLayout(false);
            this.grpMode.PerformLayout();
            this.pnlRunSetting.ResumeLayout(false);
            this.grpSetting.ResumeLayout(false);
            this.grpSetting.PerformLayout();
            this.grpRunning.ResumeLayout(false);
            this.grpRunning.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).EndInit();
            this.grpControl.ResumeLayout(false);
            this.grpResult.ResumeLayout(false);
            this.pnlStatusBar.ResumeLayout(false);
            this.pnlStatusBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.Label lblImageCountLabel;
        private System.Windows.Forms.Label lblImageCount;
        private System.Windows.Forms.Label lblInitLabel;
        private System.Windows.Forms.Label lblInitStatus;
        private System.Windows.Forms.GroupBox grpMode;
        private System.Windows.Forms.RadioButton rdoFlat;
        private System.Windows.Forms.RadioButton rdoNotch;
        private System.Windows.Forms.Panel pnlRunSetting;
        private System.Windows.Forms.GroupBox grpRunning;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.NumericUpDown numCount;
        private System.Windows.Forms.Label lblDeg;
        private System.Windows.Forms.TextBox txtDeg;
        private System.Windows.Forms.Label lblRunCnt;
        private System.Windows.Forms.TextBox txtRunCnt;
        private System.Windows.Forms.Label lblRunStep;
        private System.Windows.Forms.TextBox txtRunStep;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox grpSetting;
        private System.Windows.Forms.Button btnCamOpen;
        private System.Windows.Forms.Button btnGrabberActive;
        private System.Windows.Forms.Button btnTrigger;
        private System.Windows.Forms.Button btnFileSave;
        private System.Windows.Forms.Label lblCamFile;
        private System.Windows.Forms.GroupBox grpControl;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnClearImages;
        private System.Windows.Forms.Button btnLoadImages;
        private System.Windows.Forms.Button btnInitialize;
        private System.Windows.Forms.GroupBox grpResult;
        private System.Windows.Forms.ListView listResult;
        private System.Windows.Forms.ColumnHeader colNo;
        private System.Windows.Forms.ColumnHeader colIndex1st;
        private System.Windows.Forms.ColumnHeader colIndex2nd;
        private System.Windows.Forms.ColumnHeader colOffAngle;
        private System.Windows.Forms.ColumnHeader colAbsAngle;
        private System.Windows.Forms.ColumnHeader colWidth;
        private System.Windows.Forms.ColumnHeader colHeight;
        private System.Windows.Forms.ColumnHeader colCenterX;
        private System.Windows.Forms.ColumnHeader colCenterY;
        private System.Windows.Forms.ColumnHeader colRadius;
        private System.Windows.Forms.Panel pnlStatusBar;
        private System.Windows.Forms.Label lblStatusMessage;
    }
}
