namespace VisionAlignChamber.Views.Controls
{
    partial class EddyPanel
    {
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.grpConnection = new System.Windows.Forms.GroupBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.lblIpAddress = new System.Windows.Forms.Label();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.grpMeasurement = new System.Windows.Forms.GroupBox();
            this.btnGetData = new System.Windows.Forms.Button();
            this.btnSetZero = new System.Windows.Forms.Button();
            this.lblZeroStatus = new System.Windows.Forms.Label();
            this.lblZeroLabel = new System.Windows.Forms.Label();
            this.grpResult = new System.Windows.Forms.GroupBox();
            this.lblCurrentValue = new System.Windows.Forms.Label();
            this.lblValueLabel = new System.Windows.Forms.Label();
            this.lblStatusMessage = new System.Windows.Forms.Label();

            this.grpConnection.SuspendLayout();
            this.grpMeasurement.SuspendLayout();
            this.grpResult.SuspendLayout();
            this.SuspendLayout();

            //
            // grpConnection
            //
            this.grpConnection.Controls.Add(this.lblConnectionStatus);
            this.grpConnection.Controls.Add(this.btnDisconnect);
            this.grpConnection.Controls.Add(this.btnConnect);
            this.grpConnection.Controls.Add(this.txtPort);
            this.grpConnection.Controls.Add(this.txtIpAddress);
            this.grpConnection.Controls.Add(this.lblPort);
            this.grpConnection.Controls.Add(this.lblIpAddress);
            this.grpConnection.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.grpConnection.Location = new System.Drawing.Point(15, 15);
            this.grpConnection.Name = "grpConnection";
            this.grpConnection.Size = new System.Drawing.Size(350, 160);
            this.grpConnection.TabIndex = 0;
            this.grpConnection.TabStop = false;
            this.grpConnection.Text = "Connection";

            //
            // lblIpAddress
            //
            this.lblIpAddress.AutoSize = true;
            this.lblIpAddress.Location = new System.Drawing.Point(20, 35);
            this.lblIpAddress.Name = "lblIpAddress";
            this.lblIpAddress.Size = new System.Drawing.Size(77, 19);
            this.lblIpAddress.TabIndex = 0;
            this.lblIpAddress.Text = "IP Address:";

            //
            // txtIpAddress
            //
            this.txtIpAddress.Location = new System.Drawing.Point(110, 32);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(220, 25);
            this.txtIpAddress.TabIndex = 1;
            this.txtIpAddress.Text = "192.168.1.99";

            //
            // lblPort
            //
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(20, 70);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(36, 19);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "Port:";

            //
            // txtPort
            //
            this.txtPort.Location = new System.Drawing.Point(110, 67);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 25);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "502";

            //
            // btnConnect
            //
            this.btnConnect.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.ForeColor = System.Drawing.Color.White;
            this.btnConnect.Location = new System.Drawing.Point(20, 110);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(100, 35);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);

            //
            // btnDisconnect
            //
            this.btnDisconnect.BackColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisconnect.ForeColor = System.Drawing.Color.White;
            this.btnDisconnect.Location = new System.Drawing.Point(130, 110);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(100, 35);
            this.btnDisconnect.TabIndex = 5;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = false;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);

            //
            // lblConnectionStatus
            //
            this.lblConnectionStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblConnectionStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblConnectionStatus.Location = new System.Drawing.Point(240, 117);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(100, 23);
            this.lblConnectionStatus.TabIndex = 6;
            this.lblConnectionStatus.Text = "Disconnected";
            this.lblConnectionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            //
            // grpMeasurement
            //
            this.grpMeasurement.Controls.Add(this.lblZeroStatus);
            this.grpMeasurement.Controls.Add(this.lblZeroLabel);
            this.grpMeasurement.Controls.Add(this.btnGetData);
            this.grpMeasurement.Controls.Add(this.btnSetZero);
            this.grpMeasurement.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.grpMeasurement.Location = new System.Drawing.Point(15, 190);
            this.grpMeasurement.Name = "grpMeasurement";
            this.grpMeasurement.Size = new System.Drawing.Size(350, 120);
            this.grpMeasurement.TabIndex = 1;
            this.grpMeasurement.TabStop = false;
            this.grpMeasurement.Text = "Measurement Control";

            //
            // btnSetZero
            //
            this.btnSetZero.BackColor = System.Drawing.Color.FromArgb(0, 150, 100);
            this.btnSetZero.Enabled = false;
            this.btnSetZero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetZero.ForeColor = System.Drawing.Color.White;
            this.btnSetZero.Location = new System.Drawing.Point(20, 35);
            this.btnSetZero.Name = "btnSetZero";
            this.btnSetZero.Size = new System.Drawing.Size(150, 40);
            this.btnSetZero.TabIndex = 0;
            this.btnSetZero.Text = "Set Zero";
            this.btnSetZero.UseVisualStyleBackColor = false;
            this.btnSetZero.Click += new System.EventHandler(this.btnSetZero_Click);

            //
            // btnGetData
            //
            this.btnGetData.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnGetData.Enabled = false;
            this.btnGetData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetData.ForeColor = System.Drawing.Color.White;
            this.btnGetData.Location = new System.Drawing.Point(180, 35);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(150, 40);
            this.btnGetData.TabIndex = 1;
            this.btnGetData.Text = "Get Data";
            this.btnGetData.UseVisualStyleBackColor = false;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);

            //
            // lblZeroLabel
            //
            this.lblZeroLabel.AutoSize = true;
            this.lblZeroLabel.Location = new System.Drawing.Point(20, 85);
            this.lblZeroLabel.Name = "lblZeroLabel";
            this.lblZeroLabel.Size = new System.Drawing.Size(78, 19);
            this.lblZeroLabel.TabIndex = 2;
            this.lblZeroLabel.Text = "Zero Status:";

            //
            // lblZeroStatus
            //
            this.lblZeroStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblZeroStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblZeroStatus.Location = new System.Drawing.Point(110, 85);
            this.lblZeroStatus.Name = "lblZeroStatus";
            this.lblZeroStatus.Size = new System.Drawing.Size(100, 19);
            this.lblZeroStatus.TabIndex = 3;
            this.lblZeroStatus.Text = "Not Set";

            //
            // grpResult
            //
            this.grpResult.Controls.Add(this.lblCurrentValue);
            this.grpResult.Controls.Add(this.lblValueLabel);
            this.grpResult.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.grpResult.Location = new System.Drawing.Point(15, 325);
            this.grpResult.Name = "grpResult";
            this.grpResult.Size = new System.Drawing.Size(350, 120);
            this.grpResult.TabIndex = 2;
            this.grpResult.TabStop = false;
            this.grpResult.Text = "Measurement Result";

            //
            // lblValueLabel
            //
            this.lblValueLabel.AutoSize = true;
            this.lblValueLabel.Location = new System.Drawing.Point(20, 35);
            this.lblValueLabel.Name = "lblValueLabel";
            this.lblValueLabel.Size = new System.Drawing.Size(92, 19);
            this.lblValueLabel.TabIndex = 0;
            this.lblValueLabel.Text = "Current Value:";

            //
            // lblCurrentValue
            //
            this.lblCurrentValue.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblCurrentValue.ForeColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.lblCurrentValue.Location = new System.Drawing.Point(20, 60);
            this.lblCurrentValue.Name = "lblCurrentValue";
            this.lblCurrentValue.Size = new System.Drawing.Size(310, 50);
            this.lblCurrentValue.TabIndex = 1;
            this.lblCurrentValue.Text = "0.0000";
            this.lblCurrentValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            //
            // lblStatusMessage
            //
            this.lblStatusMessage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatusMessage.ForeColor = System.Drawing.Color.Gray;
            this.lblStatusMessage.Location = new System.Drawing.Point(15, 455);
            this.lblStatusMessage.Name = "lblStatusMessage";
            this.lblStatusMessage.Size = new System.Drawing.Size(350, 20);
            this.lblStatusMessage.TabIndex = 3;
            this.lblStatusMessage.Text = "Ready";

            //
            // EddyPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblStatusMessage);
            this.Controls.Add(this.grpResult);
            this.Controls.Add(this.grpMeasurement);
            this.Controls.Add(this.grpConnection);
            this.Name = "EddyPanel";
            this.Size = new System.Drawing.Size(380, 490);

            this.grpConnection.ResumeLayout(false);
            this.grpConnection.PerformLayout();
            this.grpMeasurement.ResumeLayout(false);
            this.grpMeasurement.PerformLayout();
            this.grpResult.ResumeLayout(false);
            this.grpResult.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpConnection;
        private System.Windows.Forms.Label lblIpAddress;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Label lblConnectionStatus;
        private System.Windows.Forms.GroupBox grpMeasurement;
        private System.Windows.Forms.Button btnSetZero;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.Label lblZeroLabel;
        private System.Windows.Forms.Label lblZeroStatus;
        private System.Windows.Forms.GroupBox grpResult;
        private System.Windows.Forms.Label lblValueLabel;
        private System.Windows.Forms.Label lblCurrentValue;
        private System.Windows.Forms.Label lblStatusMessage;
    }
}
