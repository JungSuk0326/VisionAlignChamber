namespace VisionAlignChamber.Views.Controls
{
    partial class CommPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpServerSettings = new System.Windows.Forms.GroupBox();
            this.btnApplyPort = new System.Windows.Forms.Button();
            this.numServerPort = new System.Windows.Forms.NumericUpDown();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.lblClientCount = new System.Windows.Forms.Label();
            this.lblClientCountLabel = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStatusLabel = new System.Windows.Forms.Label();
            this.grpControl = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.grpLog = new System.Windows.Forms.GroupBox();
            this.rtbCommLog = new System.Windows.Forms.RichTextBox();
            this.panelLogButtons = new System.Windows.Forms.Panel();
            this.chkAutoScroll = new System.Windows.Forms.CheckBox();
            this.btnSaveLog = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.grpServerSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numServerPort)).BeginInit();
            this.grpStatus.SuspendLayout();
            this.grpControl.SuspendLayout();
            this.grpLog.SuspendLayout();
            this.panelLogButtons.SuspendLayout();
            this.SuspendLayout();
            //
            // grpServerSettings
            //
            this.grpServerSettings.Controls.Add(this.btnApplyPort);
            this.grpServerSettings.Controls.Add(this.numServerPort);
            this.grpServerSettings.Controls.Add(this.lblPort);
            this.grpServerSettings.Controls.Add(this.txtServerIP);
            this.grpServerSettings.Controls.Add(this.lblIP);
            this.grpServerSettings.ForeColor = System.Drawing.Color.White;
            this.grpServerSettings.Location = new System.Drawing.Point(10, 10);
            this.grpServerSettings.Name = "grpServerSettings";
            this.grpServerSettings.Size = new System.Drawing.Size(250, 120);
            this.grpServerSettings.TabIndex = 0;
            this.grpServerSettings.TabStop = false;
            this.grpServerSettings.Text = "Server Settings";
            //
            // btnApplyPort
            //
            this.btnApplyPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnApplyPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApplyPort.Location = new System.Drawing.Point(170, 70);
            this.btnApplyPort.Name = "btnApplyPort";
            this.btnApplyPort.Size = new System.Drawing.Size(70, 30);
            this.btnApplyPort.TabIndex = 4;
            this.btnApplyPort.Text = "Apply";
            this.btnApplyPort.UseVisualStyleBackColor = false;
            this.btnApplyPort.Click += new System.EventHandler(this.btnApplyPort_Click);
            //
            // numServerPort
            //
            this.numServerPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.numServerPort.ForeColor = System.Drawing.Color.White;
            this.numServerPort.Location = new System.Drawing.Point(70, 73);
            this.numServerPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numServerPort.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numServerPort.Name = "numServerPort";
            this.numServerPort.Size = new System.Drawing.Size(90, 25);
            this.numServerPort.TabIndex = 3;
            this.numServerPort.Value = new decimal(new int[] {
            9998,
            0,
            0,
            0});
            //
            // lblPort
            //
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(15, 75);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(35, 19);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "Port:";
            //
            // txtServerIP
            //
            this.txtServerIP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtServerIP.ForeColor = System.Drawing.Color.White;
            this.txtServerIP.Location = new System.Drawing.Point(70, 35);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.ReadOnly = true;
            this.txtServerIP.Size = new System.Drawing.Size(170, 25);
            this.txtServerIP.TabIndex = 1;
            //
            // lblIP
            //
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(15, 38);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(23, 19);
            this.lblIP.TabIndex = 0;
            this.lblIP.Text = "IP:";
            //
            // grpStatus
            //
            this.grpStatus.Controls.Add(this.lblClientCount);
            this.grpStatus.Controls.Add(this.lblClientCountLabel);
            this.grpStatus.Controls.Add(this.lblStatus);
            this.grpStatus.Controls.Add(this.lblStatusLabel);
            this.grpStatus.ForeColor = System.Drawing.Color.White;
            this.grpStatus.Location = new System.Drawing.Point(270, 10);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(200, 120);
            this.grpStatus.TabIndex = 1;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Connection Status";
            //
            // lblClientCount
            //
            this.lblClientCount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblClientCount.ForeColor = System.Drawing.Color.Cyan;
            this.lblClientCount.Location = new System.Drawing.Point(100, 70);
            this.lblClientCount.Name = "lblClientCount";
            this.lblClientCount.Size = new System.Drawing.Size(80, 25);
            this.lblClientCount.TabIndex = 3;
            this.lblClientCount.Text = "0";
            this.lblClientCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblClientCountLabel
            //
            this.lblClientCountLabel.AutoSize = true;
            this.lblClientCountLabel.Location = new System.Drawing.Point(15, 75);
            this.lblClientCountLabel.Name = "lblClientCountLabel";
            this.lblClientCountLabel.Size = new System.Drawing.Size(54, 19);
            this.lblClientCountLabel.TabIndex = 2;
            this.lblClientCountLabel.Text = "Clients:";
            //
            // lblStatus
            //
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(100, 35);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(90, 25);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Stopped";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblStatusLabel
            //
            this.lblStatusLabel.AutoSize = true;
            this.lblStatusLabel.Location = new System.Drawing.Point(15, 38);
            this.lblStatusLabel.Name = "lblStatusLabel";
            this.lblStatusLabel.Size = new System.Drawing.Size(49, 19);
            this.lblStatusLabel.TabIndex = 0;
            this.lblStatusLabel.Text = "Status:";
            //
            // grpControl
            //
            this.grpControl.Controls.Add(this.btnStop);
            this.grpControl.Controls.Add(this.btnStart);
            this.grpControl.ForeColor = System.Drawing.Color.White;
            this.grpControl.Location = new System.Drawing.Point(480, 10);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(200, 120);
            this.grpControl.TabIndex = 2;
            this.grpControl.TabStop = false;
            this.grpControl.Text = "Server Control";
            //
            // btnStop
            //
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnStop.Enabled = false;
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStop.Location = new System.Drawing.Point(105, 35);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(80, 60);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            //
            // btnStart
            //
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(120)))), ((int)(((byte)(60)))));
            this.btnStart.Enabled = false;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnStart.Location = new System.Drawing.Point(15, 35);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(80, 60);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            //
            // grpLog
            //
            this.grpLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpLog.Controls.Add(this.rtbCommLog);
            this.grpLog.Controls.Add(this.panelLogButtons);
            this.grpLog.ForeColor = System.Drawing.Color.White;
            this.grpLog.Location = new System.Drawing.Point(10, 140);
            this.grpLog.Name = "grpLog";
            this.grpLog.Size = new System.Drawing.Size(760, 480);
            this.grpLog.TabIndex = 3;
            this.grpLog.TabStop = false;
            this.grpLog.Text = "Communication Log";
            //
            // rtbCommLog
            //
            this.rtbCommLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.rtbCommLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbCommLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbCommLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.rtbCommLog.ForeColor = System.Drawing.Color.White;
            this.rtbCommLog.Location = new System.Drawing.Point(3, 21);
            this.rtbCommLog.Name = "rtbCommLog";
            this.rtbCommLog.ReadOnly = true;
            this.rtbCommLog.Size = new System.Drawing.Size(754, 416);
            this.rtbCommLog.TabIndex = 0;
            this.rtbCommLog.Text = "";
            //
            // panelLogButtons
            //
            this.panelLogButtons.Controls.Add(this.chkAutoScroll);
            this.panelLogButtons.Controls.Add(this.btnSaveLog);
            this.panelLogButtons.Controls.Add(this.btnClearLog);
            this.panelLogButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelLogButtons.Location = new System.Drawing.Point(3, 437);
            this.panelLogButtons.Name = "panelLogButtons";
            this.panelLogButtons.Size = new System.Drawing.Size(754, 40);
            this.panelLogButtons.TabIndex = 1;
            //
            // chkAutoScroll
            //
            this.chkAutoScroll.AutoSize = true;
            this.chkAutoScroll.Checked = true;
            this.chkAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoScroll.Location = new System.Drawing.Point(10, 10);
            this.chkAutoScroll.Name = "chkAutoScroll";
            this.chkAutoScroll.Size = new System.Drawing.Size(95, 23);
            this.chkAutoScroll.TabIndex = 2;
            this.chkAutoScroll.Text = "Auto Scroll";
            this.chkAutoScroll.UseVisualStyleBackColor = true;
            //
            // btnSaveLog
            //
            this.btnSaveLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnSaveLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveLog.Location = new System.Drawing.Point(564, 5);
            this.btnSaveLog.Name = "btnSaveLog";
            this.btnSaveLog.Size = new System.Drawing.Size(90, 30);
            this.btnSaveLog.TabIndex = 1;
            this.btnSaveLog.Text = "Save Log";
            this.btnSaveLog.UseVisualStyleBackColor = false;
            this.btnSaveLog.Click += new System.EventHandler(this.btnSaveLog_Click);
            //
            // btnClearLog
            //
            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnClearLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearLog.Location = new System.Drawing.Point(660, 5);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(90, 30);
            this.btnClearLog.TabIndex = 0;
            this.btnClearLog.Text = "Clear";
            this.btnClearLog.UseVisualStyleBackColor = false;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            //
            // CommPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.grpLog);
            this.Controls.Add(this.grpControl);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.grpServerSettings);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Name = "CommPanel";
            this.Size = new System.Drawing.Size(780, 630);
            this.grpServerSettings.ResumeLayout(false);
            this.grpServerSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numServerPort)).EndInit();
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.grpControl.ResumeLayout(false);
            this.grpLog.ResumeLayout(false);
            this.panelLogButtons.ResumeLayout(false);
            this.panelLogButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpServerSettings;
        private System.Windows.Forms.Button btnApplyPort;
        private System.Windows.Forms.NumericUpDown numServerPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.Label lblClientCount;
        private System.Windows.Forms.Label lblClientCountLabel;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblStatusLabel;
        private System.Windows.Forms.GroupBox grpControl;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox grpLog;
        private System.Windows.Forms.RichTextBox rtbCommLog;
        private System.Windows.Forms.Panel panelLogButtons;
        private System.Windows.Forms.CheckBox chkAutoScroll;
        private System.Windows.Forms.Button btnSaveLog;
        private System.Windows.Forms.Button btnClearLog;
    }
}
