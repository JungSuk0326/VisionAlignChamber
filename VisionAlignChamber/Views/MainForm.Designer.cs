namespace VisionAlignChamber.Views
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panelVisionDisplay = new System.Windows.Forms.Panel();
            this.picVisionDisplay = new System.Windows.Forms.PictureBox();
            this.lblVisionInfo = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabProcess = new System.Windows.Forms.TabPage();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.tabControlHistory = new System.Windows.Forms.TabControl();
            this.tabResult = new System.Windows.Forms.TabPage();
            this.tabAlarm = new System.Windows.Forms.TabPage();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.logPanel = new VisionAlignChamber.Views.Controls.LogPanel();
            this.tabMaint = new System.Windows.Forms.TabPage();
            this.tabControlMaint = new System.Windows.Forms.TabControl();
            this.tabVision = new System.Windows.Forms.TabPage();
            this.visionPanel = new VisionAlignChamber.Views.Controls.VisionPanel();
            this.tabEddy = new System.Windows.Forms.TabPage();
            this.eddyPanel = new VisionAlignChamber.Views.Controls.EddyPanel();
            this.tabPN = new System.Windows.Forms.TabPage();
            this.tabIO = new System.Windows.Forms.TabPage();
            this.ioPanel = new VisionAlignChamber.Views.Controls.IOPanel();
            this.tabMotion = new System.Windows.Forms.TabPage();
            this.motionPanel = new VisionAlignChamber.Views.Controls.MotionPanel();
            this.tabParameter = new System.Windows.Forms.TabPage();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnResetAlarm = new System.Windows.Forms.Button();
            this.btnHomeAll = new System.Windows.Forms.Button();
            this.btnInitialize = new System.Windows.Forms.Button();
            this.btnEmergencyStop = new System.Windows.Forms.Button();
            this.grpControlMode = new System.Windows.Forms.GroupBox();
            this.rbLocal = new System.Windows.Forms.RadioButton();
            this.rbRemote = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.lblControlAuthority = new System.Windows.Forms.Label();
            this.lblSystemMode = new System.Windows.Forms.Label();
            this.lblSystemStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.lblStatusMessage = new System.Windows.Forms.Label();
            this.lblAlarmIndicator = new System.Windows.Forms.Label();
            this.timerAlarmBlink = new System.Windows.Forms.Timer();
            this.lblCTCLabel = new System.Windows.Forms.Label();
            this.lblCTCStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panelVisionDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVisionDisplay)).BeginInit();
            this.panelRight.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabHistory.SuspendLayout();
            this.tabControlHistory.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.tabMaint.SuspendLayout();
            this.tabControlMaint.SuspendLayout();
            this.tabVision.SuspendLayout();
            this.tabEddy.SuspendLayout();
            this.tabIO.SuspendLayout();
            this.tabMotion.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.grpControlMode.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 60);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.panelVisionDisplay);
            this.splitContainer.Panel1MinSize = 500;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panelRight);
            this.splitContainer.Size = new System.Drawing.Size(1400, 700);
            this.splitContainer.SplitterDistance = 600;
            this.splitContainer.TabIndex = 0;
            // 
            // panelVisionDisplay
            // 
            this.panelVisionDisplay.BackColor = System.Drawing.Color.Black;
            this.panelVisionDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelVisionDisplay.Controls.Add(this.picVisionDisplay);
            this.panelVisionDisplay.Controls.Add(this.lblVisionInfo);
            this.panelVisionDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVisionDisplay.Location = new System.Drawing.Point(0, 0);
            this.panelVisionDisplay.Name = "panelVisionDisplay";
            this.panelVisionDisplay.Padding = new System.Windows.Forms.Padding(5);
            this.panelVisionDisplay.Size = new System.Drawing.Size(600, 700);
            this.panelVisionDisplay.TabIndex = 0;
            // 
            // picVisionDisplay
            // 
            this.picVisionDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.picVisionDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picVisionDisplay.Location = new System.Drawing.Point(5, 5);
            this.picVisionDisplay.Name = "picVisionDisplay";
            this.picVisionDisplay.Size = new System.Drawing.Size(588, 665);
            this.picVisionDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picVisionDisplay.TabIndex = 0;
            this.picVisionDisplay.TabStop = false;
            // 
            // lblVisionInfo
            // 
            this.lblVisionInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.lblVisionInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblVisionInfo.ForeColor = System.Drawing.Color.White;
            this.lblVisionInfo.Location = new System.Drawing.Point(5, 670);
            this.lblVisionInfo.Name = "lblVisionInfo";
            this.lblVisionInfo.Size = new System.Drawing.Size(588, 23);
            this.lblVisionInfo.TabIndex = 1;
            this.lblVisionInfo.Text = "Vision Display";
            this.lblVisionInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.tabMain);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(796, 700);
            this.panelRight.TabIndex = 0;
            // 
            // tabMain
            //
            this.tabMain.Controls.Add(this.tabProcess);
            this.tabMain.Controls.Add(this.tabHistory);
            this.tabMain.Controls.Add(this.tabLog);
            this.tabMain.Controls.Add(this.tabMaint);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(796, 700);
            this.tabMain.TabIndex = 0;
            // 
            // tabProcess
            // 
            this.tabProcess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.tabProcess.Location = new System.Drawing.Point(4, 29);
            this.tabProcess.Name = "tabProcess";
            this.tabProcess.Padding = new System.Windows.Forms.Padding(3);
            this.tabProcess.Size = new System.Drawing.Size(788, 667);
            this.tabProcess.TabIndex = 0;
            this.tabProcess.Text = "Main";
            //
            // tabHistory
            //
            this.tabHistory.Controls.Add(this.tabControlHistory);
            this.tabHistory.Location = new System.Drawing.Point(4, 29);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Size = new System.Drawing.Size(788, 667);
            this.tabHistory.TabIndex = 1;
            this.tabHistory.Text = "History";
            this.tabHistory.UseVisualStyleBackColor = true;
            //
            // tabControlHistory
            //
            this.tabControlHistory.Controls.Add(this.tabResult);
            this.tabControlHistory.Controls.Add(this.tabAlarm);
            this.tabControlHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlHistory.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControlHistory.Location = new System.Drawing.Point(0, 0);
            this.tabControlHistory.Name = "tabControlHistory";
            this.tabControlHistory.SelectedIndex = 0;
            this.tabControlHistory.Size = new System.Drawing.Size(788, 667);
            this.tabControlHistory.TabIndex = 0;
            //
            // tabResult
            //
            this.tabResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.tabResult.Location = new System.Drawing.Point(4, 26);
            this.tabResult.Name = "tabResult";
            this.tabResult.Padding = new System.Windows.Forms.Padding(3);
            this.tabResult.Size = new System.Drawing.Size(780, 637);
            this.tabResult.TabIndex = 0;
            this.tabResult.Text = "Result";
            //
            // tabAlarm
            //
            this.tabAlarm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.tabAlarm.Location = new System.Drawing.Point(4, 26);
            this.tabAlarm.Name = "tabAlarm";
            this.tabAlarm.Padding = new System.Windows.Forms.Padding(3);
            this.tabAlarm.Size = new System.Drawing.Size(780, 637);
            this.tabAlarm.TabIndex = 1;
            this.tabAlarm.Text = "Alarm";
            //
            // tabLog
            //
            this.tabLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.tabLog.Controls.Add(this.logPanel);
            this.tabLog.Location = new System.Drawing.Point(4, 29);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(788, 667);
            this.tabLog.TabIndex = 2;
            this.tabLog.Text = "Log";
            //
            // logPanel
            //
            this.logPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.logPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logPanel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.logPanel.Location = new System.Drawing.Point(3, 3);
            this.logPanel.Name = "logPanel";
            this.logPanel.Size = new System.Drawing.Size(782, 661);
            this.logPanel.TabIndex = 0;
            //
            // tabMaint
            //
            this.tabMaint.Controls.Add(this.tabControlMaint);
            this.tabMaint.Location = new System.Drawing.Point(4, 29);
            this.tabMaint.Name = "tabMaint";
            this.tabMaint.Size = new System.Drawing.Size(788, 667);
            this.tabMaint.TabIndex = 3;
            this.tabMaint.Text = "Maint";
            this.tabMaint.UseVisualStyleBackColor = true;
            // 
            // tabControlMaint
            // 
            this.tabControlMaint.Controls.Add(this.tabVision);
            this.tabControlMaint.Controls.Add(this.tabEddy);
            this.tabControlMaint.Controls.Add(this.tabPN);
            this.tabControlMaint.Controls.Add(this.tabIO);
            this.tabControlMaint.Controls.Add(this.tabMotion);
            this.tabControlMaint.Controls.Add(this.tabParameter);
            this.tabControlMaint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMaint.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControlMaint.Location = new System.Drawing.Point(0, 0);
            this.tabControlMaint.Name = "tabControlMaint";
            this.tabControlMaint.SelectedIndex = 0;
            this.tabControlMaint.Size = new System.Drawing.Size(788, 667);
            this.tabControlMaint.TabIndex = 0;
            // 
            // tabVision
            // 
            this.tabVision.Controls.Add(this.visionPanel);
            this.tabVision.Location = new System.Drawing.Point(4, 26);
            this.tabVision.Name = "tabVision";
            this.tabVision.Padding = new System.Windows.Forms.Padding(3);
            this.tabVision.Size = new System.Drawing.Size(780, 637);
            this.tabVision.TabIndex = 0;
            this.tabVision.Text = "Vision";
            this.tabVision.UseVisualStyleBackColor = true;
            // 
            // visionPanel
            // 
            this.visionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visionPanel.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.visionPanel.Location = new System.Drawing.Point(3, 3);
            this.visionPanel.Name = "visionPanel";
            this.visionPanel.Size = new System.Drawing.Size(774, 631);
            this.visionPanel.TabIndex = 0;
            // 
            // tabEddy
            // 
            this.tabEddy.Controls.Add(this.eddyPanel);
            this.tabEddy.Location = new System.Drawing.Point(4, 26);
            this.tabEddy.Name = "tabEddy";
            this.tabEddy.Padding = new System.Windows.Forms.Padding(3);
            this.tabEddy.Size = new System.Drawing.Size(780, 637);
            this.tabEddy.TabIndex = 3;
            this.tabEddy.Text = "Eddy";
            this.tabEddy.UseVisualStyleBackColor = true;
            // 
            // eddyPanel
            // 
            this.eddyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eddyPanel.Location = new System.Drawing.Point(3, 3);
            this.eddyPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.eddyPanel.Name = "eddyPanel";
            this.eddyPanel.Size = new System.Drawing.Size(774, 631);
            this.eddyPanel.TabIndex = 0;
            // 
            // tabPN
            // 
            this.tabPN.Location = new System.Drawing.Point(4, 26);
            this.tabPN.Name = "tabPN";
            this.tabPN.Padding = new System.Windows.Forms.Padding(3);
            this.tabPN.Size = new System.Drawing.Size(780, 637);
            this.tabPN.TabIndex = 4;
            this.tabPN.Text = "PN";
            this.tabPN.UseVisualStyleBackColor = true;
            // 
            // tabIO
            // 
            this.tabIO.Controls.Add(this.ioPanel);
            this.tabIO.Location = new System.Drawing.Point(4, 26);
            this.tabIO.Name = "tabIO";
            this.tabIO.Padding = new System.Windows.Forms.Padding(3);
            this.tabIO.Size = new System.Drawing.Size(780, 637);
            this.tabIO.TabIndex = 1;
            this.tabIO.Text = "I/O";
            this.tabIO.UseVisualStyleBackColor = true;
            // 
            // ioPanel
            // 
            this.ioPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ioPanel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ioPanel.Location = new System.Drawing.Point(3, 3);
            this.ioPanel.Name = "ioPanel";
            this.ioPanel.Size = new System.Drawing.Size(774, 631);
            this.ioPanel.TabIndex = 0;
            // 
            // tabMotion
            // 
            this.tabMotion.Controls.Add(this.motionPanel);
            this.tabMotion.Location = new System.Drawing.Point(4, 26);
            this.tabMotion.Name = "tabMotion";
            this.tabMotion.Padding = new System.Windows.Forms.Padding(3);
            this.tabMotion.Size = new System.Drawing.Size(780, 637);
            this.tabMotion.TabIndex = 2;
            this.tabMotion.Text = "Motion";
            this.tabMotion.UseVisualStyleBackColor = true;
            // 
            // motionPanel
            // 
            this.motionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.motionPanel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.motionPanel.Location = new System.Drawing.Point(3, 3);
            this.motionPanel.Name = "motionPanel";
            this.motionPanel.Size = new System.Drawing.Size(774, 631);
            this.motionPanel.TabIndex = 0;
            //
            // tabParameter
            //
            this.tabParameter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.tabParameter.Location = new System.Drawing.Point(4, 26);
            this.tabParameter.Name = "tabParameter";
            this.tabParameter.Padding = new System.Windows.Forms.Padding(3);
            this.tabParameter.Size = new System.Drawing.Size(780, 637);
            this.tabParameter.TabIndex = 5;
            this.tabParameter.Text = "Parameter";
            //
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panelTop.Controls.Add(this.lblAlarmIndicator);
            this.panelTop.Controls.Add(this.lblCTCLabel);
            this.panelTop.Controls.Add(this.lblCTCStatus);
            this.panelTop.Controls.Add(this.btnResetAlarm);
            this.panelTop.Controls.Add(this.btnHomeAll);
            this.panelTop.Controls.Add(this.btnInitialize);
            this.panelTop.Controls.Add(this.btnEmergencyStop);
            this.panelTop.Controls.Add(this.grpControlMode);
            this.panelTop.Controls.Add(this.label3);
            this.panelTop.Controls.Add(this.lblControlAuthority);
            this.panelTop.Controls.Add(this.lblSystemMode);
            this.panelTop.Controls.Add(this.lblSystemStatus);
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1400, 60);
            this.panelTop.TabIndex = 1;
            // 
            // btnResetAlarm
            // 
            this.btnResetAlarm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnResetAlarm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetAlarm.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnResetAlarm.ForeColor = System.Drawing.Color.White;
            this.btnResetAlarm.Location = new System.Drawing.Point(370, 10);
            this.btnResetAlarm.Name = "btnResetAlarm";
            this.btnResetAlarm.Size = new System.Drawing.Size(100, 40);
            this.btnResetAlarm.TabIndex = 3;
            this.btnResetAlarm.Text = "Reset";
            this.btnResetAlarm.UseVisualStyleBackColor = false;
            this.btnResetAlarm.Click += new System.EventHandler(this.btnResetAlarm_Click);
            // 
            // btnHomeAll
            // 
            this.btnHomeAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnHomeAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHomeAll.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnHomeAll.ForeColor = System.Drawing.Color.White;
            this.btnHomeAll.Location = new System.Drawing.Point(260, 10);
            this.btnHomeAll.Name = "btnHomeAll";
            this.btnHomeAll.Size = new System.Drawing.Size(100, 40);
            this.btnHomeAll.TabIndex = 2;
            this.btnHomeAll.Text = "Home All";
            this.btnHomeAll.UseVisualStyleBackColor = false;
            this.btnHomeAll.Click += new System.EventHandler(this.btnHomeAll_Click);
            // 
            // btnInitialize
            // 
            this.btnInitialize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnInitialize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInitialize.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnInitialize.ForeColor = System.Drawing.Color.White;
            this.btnInitialize.Location = new System.Drawing.Point(150, 10);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Size = new System.Drawing.Size(100, 40);
            this.btnInitialize.TabIndex = 1;
            this.btnInitialize.Text = "Initialize";
            this.btnInitialize.UseVisualStyleBackColor = false;
            this.btnInitialize.Click += new System.EventHandler(this.btnInitialize_Click);
            // 
            // btnEmergencyStop
            // 
            this.btnEmergencyStop.BackColor = System.Drawing.Color.Red;
            this.btnEmergencyStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmergencyStop.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnEmergencyStop.ForeColor = System.Drawing.Color.White;
            this.btnEmergencyStop.Location = new System.Drawing.Point(12, 10);
            this.btnEmergencyStop.Name = "btnEmergencyStop";
            this.btnEmergencyStop.Size = new System.Drawing.Size(120, 40);
            this.btnEmergencyStop.TabIndex = 0;
            this.btnEmergencyStop.Text = "EMO";
            this.btnEmergencyStop.UseVisualStyleBackColor = false;
            this.btnEmergencyStop.Click += new System.EventHandler(this.btnEmergencyStop_Click);
            // 
            // grpControlMode
            // 
            this.grpControlMode.Controls.Add(this.rbLocal);
            this.grpControlMode.Controls.Add(this.rbRemote);
            this.grpControlMode.ForeColor = System.Drawing.Color.White;
            this.grpControlMode.Location = new System.Drawing.Point(490, 5);
            this.grpControlMode.Name = "grpControlMode";
            this.grpControlMode.Size = new System.Drawing.Size(180, 50);
            this.grpControlMode.TabIndex = 8;
            this.grpControlMode.TabStop = false;
            this.grpControlMode.Text = "Control Authority";
            // 
            // rbLocal
            // 
            this.rbLocal.AutoSize = true;
            this.rbLocal.Checked = true;
            this.rbLocal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.rbLocal.ForeColor = System.Drawing.Color.LimeGreen;
            this.rbLocal.Location = new System.Drawing.Point(10, 20);
            this.rbLocal.Name = "rbLocal";
            this.rbLocal.Size = new System.Drawing.Size(62, 23);
            this.rbLocal.TabIndex = 0;
            this.rbLocal.TabStop = true;
            this.rbLocal.Text = "Local";
            this.rbLocal.UseVisualStyleBackColor = true;
            this.rbLocal.CheckedChanged += new System.EventHandler(this.rbLocal_CheckedChanged);
            // 
            // rbRemote
            // 
            this.rbRemote.AutoSize = true;
            this.rbRemote.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.rbRemote.ForeColor = System.Drawing.Color.White;
            this.rbRemote.Location = new System.Drawing.Point(90, 20);
            this.rbRemote.Name = "rbRemote";
            this.rbRemote.Size = new System.Drawing.Size(79, 23);
            this.rbRemote.TabIndex = 1;
            this.rbRemote.Text = "Remote";
            this.rbRemote.UseVisualStyleBackColor = true;
            this.rbRemote.CheckedChanged += new System.EventHandler(this.rbRemote_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(690, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 19);
            this.label3.TabIndex = 9;
            this.label3.Text = "Control:";
            // 
            // lblControlAuthority
            // 
            this.lblControlAuthority.AutoSize = true;
            this.lblControlAuthority.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblControlAuthority.ForeColor = System.Drawing.Color.Cyan;
            this.lblControlAuthority.Location = new System.Drawing.Point(750, 12);
            this.lblControlAuthority.Name = "lblControlAuthority";
            this.lblControlAuthority.Size = new System.Drawing.Size(44, 19);
            this.lblControlAuthority.TabIndex = 10;
            this.lblControlAuthority.Text = "Local";
            // 
            // lblSystemMode
            // 
            this.lblSystemMode.AutoSize = true;
            this.lblSystemMode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSystemMode.ForeColor = System.Drawing.Color.White;
            this.lblSystemMode.Location = new System.Drawing.Point(850, 34);
            this.lblSystemMode.Name = "lblSystemMode";
            this.lblSystemMode.Size = new System.Drawing.Size(58, 19);
            this.lblSystemMode.TabIndex = 7;
            this.lblSystemMode.Text = "Manual";
            this.lblSystemMode.Visible = false;
            // 
            // lblSystemStatus
            // 
            this.lblSystemStatus.AutoSize = true;
            this.lblSystemStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSystemStatus.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblSystemStatus.Location = new System.Drawing.Point(750, 34);
            this.lblSystemStatus.Name = "lblSystemStatus";
            this.lblSystemStatus.Size = new System.Drawing.Size(34, 19);
            this.lblSystemStatus.TabIndex = 5;
            this.lblSystemStatus.Text = "Idle";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(850, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 19);
            this.label2.TabIndex = 6;
            this.label2.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(690, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "Status:";
            //
            // lblAlarmIndicator
            //
            this.lblAlarmIndicator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAlarmIndicator.BackColor = System.Drawing.Color.Red;
            this.lblAlarmIndicator.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblAlarmIndicator.ForeColor = System.Drawing.Color.White;
            this.lblAlarmIndicator.Location = new System.Drawing.Point(1270, 10);
            this.lblAlarmIndicator.Name = "lblAlarmIndicator";
            this.lblAlarmIndicator.Size = new System.Drawing.Size(120, 40);
            this.lblAlarmIndicator.TabIndex = 11;
            this.lblAlarmIndicator.Text = "ALARM";
            this.lblAlarmIndicator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAlarmIndicator.Visible = false;
            this.lblAlarmIndicator.Click += new System.EventHandler(this.lblAlarmIndicator_Click);
            //
            // lblCTCLabel
            //
            this.lblCTCLabel.AutoSize = true;
            this.lblCTCLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCTCLabel.ForeColor = System.Drawing.Color.White;
            this.lblCTCLabel.Location = new System.Drawing.Point(920, 12);
            this.lblCTCLabel.Name = "lblCTCLabel";
            this.lblCTCLabel.Size = new System.Drawing.Size(35, 19);
            this.lblCTCLabel.TabIndex = 12;
            this.lblCTCLabel.Text = "CTC:";
            //
            // lblCTCStatus
            //
            this.lblCTCStatus.AutoSize = true;
            this.lblCTCStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCTCStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblCTCStatus.Location = new System.Drawing.Point(960, 12);
            this.lblCTCStatus.Name = "lblCTCStatus";
            this.lblCTCStatus.Size = new System.Drawing.Size(95, 19);
            this.lblCTCStatus.TabIndex = 13;
            this.lblCTCStatus.Text = "Disconnected";
            //
            // timerAlarmBlink
            //
            this.timerAlarmBlink.Interval = 500;
            this.timerAlarmBlink.Tick += new System.EventHandler(this.timerAlarmBlink_Tick);
            //
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panelBottom.Controls.Add(this.lblStatusMessage);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 760);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1400, 30);
            this.panelBottom.TabIndex = 2;
            // 
            // lblStatusMessage
            // 
            this.lblStatusMessage.AutoSize = true;
            this.lblStatusMessage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatusMessage.ForeColor = System.Drawing.Color.White;
            this.lblStatusMessage.Location = new System.Drawing.Point(12, 6);
            this.lblStatusMessage.Name = "lblStatusMessage";
            this.lblStatusMessage.Size = new System.Drawing.Size(39, 15);
            this.lblStatusMessage.TabIndex = 0;
            this.lblStatusMessage.Text = "Ready";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 790);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vision Align Chamber";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panelVisionDisplay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picVisionDisplay)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabHistory.ResumeLayout(false);
            this.tabControlHistory.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.tabMaint.ResumeLayout(false);
            this.tabControlMaint.ResumeLayout(false);
            this.tabVision.ResumeLayout(false);
            this.tabEddy.ResumeLayout(false);
            this.tabIO.ResumeLayout(false);
            this.tabMotion.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.grpControlMode.ResumeLayout(false);
            this.grpControlMode.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel panelVisionDisplay;
        private System.Windows.Forms.PictureBox picVisionDisplay;
        private System.Windows.Forms.Label lblVisionInfo;
        private System.Windows.Forms.Panel panelRight;
        // 상위 탭
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabProcess;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.TabPage tabMaint;
        // 하위 탭 (History)
        private System.Windows.Forms.TabControl tabControlHistory;
        private System.Windows.Forms.TabPage tabResult;
        private System.Windows.Forms.TabPage tabAlarm;
        // 하위 탭 (Maint)
        private System.Windows.Forms.TabControl tabControlMaint;
        private System.Windows.Forms.TabPage tabMotion;
        private System.Windows.Forms.TabPage tabIO;
        private System.Windows.Forms.TabPage tabVision;
        private System.Windows.Forms.TabPage tabEddy;
        private System.Windows.Forms.TabPage tabPN;
        private System.Windows.Forms.TabPage tabParameter;
        // 패널
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnEmergencyStop;
        private System.Windows.Forms.Button btnInitialize;
        private System.Windows.Forms.Button btnHomeAll;
        private System.Windows.Forms.Button btnResetAlarm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSystemStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSystemMode;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblStatusMessage;
        private Controls.MotionPanel motionPanel;
        private Controls.IOPanel ioPanel;
        private Controls.VisionPanel visionPanel;
        private Controls.EddyPanel eddyPanel;
        private System.Windows.Forms.GroupBox grpControlMode;
        private System.Windows.Forms.RadioButton rbLocal;
        private System.Windows.Forms.RadioButton rbRemote;
        private System.Windows.Forms.Label lblControlAuthority;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblAlarmIndicator;
        private System.Windows.Forms.Timer timerAlarmBlink;
        private Controls.LogPanel logPanel;
        private System.Windows.Forms.Label lblCTCLabel;
        private System.Windows.Forms.Label lblCTCStatus;
    }
}
