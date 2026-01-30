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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabMotion = new System.Windows.Forms.TabPage();
            this.motionPanel = new VisionAlignChamber.Views.Controls.MotionPanel();
            this.tabIO = new System.Windows.Forms.TabPage();
            this.ioPanel = new VisionAlignChamber.Views.Controls.IOPanel();
            this.tabVision = new System.Windows.Forms.TabPage();
            this.visionPanel = new VisionAlignChamber.Views.Controls.VisionPanel();
            this.tabEddy = new System.Windows.Forms.TabPage();
            this.tabPN = new System.Windows.Forms.TabPage();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnResetAlarm = new System.Windows.Forms.Button();
            this.btnHomeAll = new System.Windows.Forms.Button();
            this.btnInitialize = new System.Windows.Forms.Button();
            this.btnEmergencyStop = new System.Windows.Forms.Button();
            this.lblSystemMode = new System.Windows.Forms.Label();
            this.lblSystemStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.lblStatusMessage = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panelVisionDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picVisionDisplay)).BeginInit();
            this.panelRight.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabMotion.SuspendLayout();
            this.tabIO.SuspendLayout();
            this.tabVision.SuspendLayout();
            this.panelTop.SuspendLayout();
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
            // splitContainer.Panel1 - Vision Display
            //
            this.splitContainer.Panel1.Controls.Add(this.panelVisionDisplay);
            this.splitContainer.Panel1MinSize = 500;
            //
            // splitContainer.Panel2 - Tabs
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
            this.picVisionDisplay.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
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
            this.lblVisionInfo.BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
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
            this.panelRight.Controls.Add(this.tabControl);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(796, 700);
            this.panelRight.TabIndex = 0;

            //
            // tabControl
            //
            this.tabControl.Controls.Add(this.tabVision);
            this.tabControl.Controls.Add(this.tabEddy);
            this.tabControl.Controls.Add(this.tabPN);
            this.tabControl.Controls.Add(this.tabIO);
            this.tabControl.Controls.Add(this.tabMotion);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(796, 700);
            this.tabControl.TabIndex = 0;

            //
            // tabMotion
            //
            this.tabMotion.Controls.Add(this.motionPanel);
            this.tabMotion.Location = new System.Drawing.Point(4, 28);
            this.tabMotion.Name = "tabMotion";
            this.tabMotion.Padding = new System.Windows.Forms.Padding(3);
            this.tabMotion.Size = new System.Drawing.Size(788, 668);
            this.tabMotion.TabIndex = 2;
            this.tabMotion.Text = "Motion";
            this.tabMotion.UseVisualStyleBackColor = true;

            //
            // motionPanel
            //
            this.motionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.motionPanel.Location = new System.Drawing.Point(3, 3);
            this.motionPanel.Name = "motionPanel";
            this.motionPanel.Size = new System.Drawing.Size(782, 662);
            this.motionPanel.TabIndex = 0;

            //
            // tabIO
            //
            this.tabIO.Controls.Add(this.ioPanel);
            this.tabIO.Location = new System.Drawing.Point(4, 28);
            this.tabIO.Name = "tabIO";
            this.tabIO.Padding = new System.Windows.Forms.Padding(3);
            this.tabIO.Size = new System.Drawing.Size(788, 668);
            this.tabIO.TabIndex = 1;
            this.tabIO.Text = "I/O";
            this.tabIO.UseVisualStyleBackColor = true;

            //
            // ioPanel
            //
            this.ioPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ioPanel.Location = new System.Drawing.Point(3, 3);
            this.ioPanel.Name = "ioPanel";
            this.ioPanel.Size = new System.Drawing.Size(782, 662);
            this.ioPanel.TabIndex = 0;

            //
            // tabVision
            //
            this.tabVision.Controls.Add(this.visionPanel);
            this.tabVision.Location = new System.Drawing.Point(4, 28);
            this.tabVision.Name = "tabVision";
            this.tabVision.Padding = new System.Windows.Forms.Padding(3);
            this.tabVision.Size = new System.Drawing.Size(788, 668);
            this.tabVision.TabIndex = 0;
            this.tabVision.Text = "Vision";
            this.tabVision.UseVisualStyleBackColor = true;

            //
            // visionPanel
            //
            this.visionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visionPanel.Location = new System.Drawing.Point(3, 3);
            this.visionPanel.Name = "visionPanel";
            this.visionPanel.Size = new System.Drawing.Size(782, 662);
            this.visionPanel.TabIndex = 0;

            //
            // tabEddy
            //
            this.tabEddy.Location = new System.Drawing.Point(4, 28);
            this.tabEddy.Name = "tabEddy";
            this.tabEddy.Padding = new System.Windows.Forms.Padding(3);
            this.tabEddy.Size = new System.Drawing.Size(788, 668);
            this.tabEddy.TabIndex = 3;
            this.tabEddy.Text = "Eddy";
            this.tabEddy.UseVisualStyleBackColor = true;

            //
            // tabPN
            //
            this.tabPN.Location = new System.Drawing.Point(4, 28);
            this.tabPN.Name = "tabPN";
            this.tabPN.Padding = new System.Windows.Forms.Padding(3);
            this.tabPN.Size = new System.Drawing.Size(788, 668);
            this.tabPN.TabIndex = 4;
            this.tabPN.Text = "PN";
            this.tabPN.UseVisualStyleBackColor = true;

            //
            // panelTop
            //
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            this.panelTop.Controls.Add(this.btnResetAlarm);
            this.panelTop.Controls.Add(this.btnHomeAll);
            this.panelTop.Controls.Add(this.btnInitialize);
            this.panelTop.Controls.Add(this.btnEmergencyStop);
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
            // btnInitialize
            //
            this.btnInitialize.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
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
            // btnHomeAll
            //
            this.btnHomeAll.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
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
            // btnResetAlarm
            //
            this.btnResetAlarm.BackColor = System.Drawing.Color.FromArgb(255, 128, 0);
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
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(550, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "Status:";

            //
            // lblSystemStatus
            //
            this.lblSystemStatus.AutoSize = true;
            this.lblSystemStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSystemStatus.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblSystemStatus.Location = new System.Drawing.Point(610, 12);
            this.lblSystemStatus.Name = "lblSystemStatus";
            this.lblSystemStatus.Size = new System.Drawing.Size(36, 19);
            this.lblSystemStatus.TabIndex = 5;
            this.lblSystemStatus.Text = "Idle";

            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(550, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Mode:";

            //
            // lblSystemMode
            //
            this.lblSystemMode.AutoSize = true;
            this.lblSystemMode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSystemMode.ForeColor = System.Drawing.Color.White;
            this.lblSystemMode.Location = new System.Drawing.Point(610, 34);
            this.lblSystemMode.Name = "lblSystemMode";
            this.lblSystemMode.Size = new System.Drawing.Size(58, 19);
            this.lblSystemMode.TabIndex = 7;
            this.lblSystemMode.Text = "Manual";

            //
            // panelBottom
            //
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
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
            this.tabControl.ResumeLayout(false);
            this.tabMotion.ResumeLayout(false);
            this.tabIO.ResumeLayout(false);
            this.tabVision.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
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
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabMotion;
        private System.Windows.Forms.TabPage tabIO;
        private System.Windows.Forms.TabPage tabVision;
        private System.Windows.Forms.TabPage tabEddy;
        private System.Windows.Forms.TabPage tabPN;
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
    }
}
