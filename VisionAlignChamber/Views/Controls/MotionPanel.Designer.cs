namespace VisionAlignChamber.Views.Controls
{
    partial class MotionPanel
    {
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.panelAxes = new System.Windows.Forms.Panel();
            this.axisWedge = new VisionAlignChamber.Views.Controls.AxisControlPanel();
            this.axisChuck = new VisionAlignChamber.Views.Controls.AxisControlPanel();
            this.axisCentering1 = new VisionAlignChamber.Views.Controls.AxisControlPanel();
            this.axisCentering2 = new VisionAlignChamber.Views.Controls.AxisControlPanel();
            this.panelGlobalControls = new System.Windows.Forms.Panel();
            this.grpGlobalControl = new System.Windows.Forms.GroupBox();
            this.lblInitStatus = new System.Windows.Forms.Label();
            this.lblAllHomedStatus = new System.Windows.Forms.Label();
            this.btnEmergencyStop = new System.Windows.Forms.Button();
            this.btnStopAll = new System.Windows.Forms.Button();
            this.btnHomeAll = new System.Windows.Forms.Button();
            this.btnInitialize = new System.Windows.Forms.Button();

            this.panelAxes.SuspendLayout();
            this.panelGlobalControls.SuspendLayout();
            this.grpGlobalControl.SuspendLayout();
            this.SuspendLayout();

            //
            // panelAxes
            //
            this.panelAxes.AutoScroll = true;
            this.panelAxes.Controls.Add(this.axisCentering2);
            this.panelAxes.Controls.Add(this.axisCentering1);
            this.panelAxes.Controls.Add(this.axisChuck);
            this.panelAxes.Controls.Add(this.axisWedge);
            this.panelAxes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAxes.Location = new System.Drawing.Point(0, 0);
            this.panelAxes.Name = "panelAxes";
            this.panelAxes.Padding = new System.Windows.Forms.Padding(10);
            this.panelAxes.Size = new System.Drawing.Size(780, 560);
            this.panelAxes.TabIndex = 0;

            //
            // axisWedge
            //
            this.axisWedge.Dock = System.Windows.Forms.DockStyle.Top;
            this.axisWedge.Location = new System.Drawing.Point(10, 10);
            this.axisWedge.Name = "axisWedge";
            this.axisWedge.Size = new System.Drawing.Size(760, 140);
            this.axisWedge.TabIndex = 0;

            //
            // axisChuck
            //
            this.axisChuck.Dock = System.Windows.Forms.DockStyle.Top;
            this.axisChuck.Location = new System.Drawing.Point(10, 150);
            this.axisChuck.Name = "axisChuck";
            this.axisChuck.Size = new System.Drawing.Size(760, 140);
            this.axisChuck.TabIndex = 1;

            //
            // axisCentering1
            //
            this.axisCentering1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axisCentering1.Location = new System.Drawing.Point(10, 290);
            this.axisCentering1.Name = "axisCentering1";
            this.axisCentering1.Size = new System.Drawing.Size(760, 140);
            this.axisCentering1.TabIndex = 2;

            //
            // axisCentering2
            //
            this.axisCentering2.Dock = System.Windows.Forms.DockStyle.Top;
            this.axisCentering2.Location = new System.Drawing.Point(10, 430);
            this.axisCentering2.Name = "axisCentering2";
            this.axisCentering2.Size = new System.Drawing.Size(760, 140);
            this.axisCentering2.TabIndex = 3;

            //
            // panelGlobalControls
            //
            this.panelGlobalControls.Controls.Add(this.grpGlobalControl);
            this.panelGlobalControls.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelGlobalControls.Location = new System.Drawing.Point(0, 560);
            this.panelGlobalControls.Name = "panelGlobalControls";
            this.panelGlobalControls.Padding = new System.Windows.Forms.Padding(10);
            this.panelGlobalControls.Size = new System.Drawing.Size(780, 100);
            this.panelGlobalControls.TabIndex = 1;

            //
            // grpGlobalControl
            //
            this.grpGlobalControl.Controls.Add(this.lblInitStatus);
            this.grpGlobalControl.Controls.Add(this.lblAllHomedStatus);
            this.grpGlobalControl.Controls.Add(this.btnEmergencyStop);
            this.grpGlobalControl.Controls.Add(this.btnStopAll);
            this.grpGlobalControl.Controls.Add(this.btnHomeAll);
            this.grpGlobalControl.Controls.Add(this.btnInitialize);
            this.grpGlobalControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpGlobalControl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpGlobalControl.Location = new System.Drawing.Point(10, 10);
            this.grpGlobalControl.Name = "grpGlobalControl";
            this.grpGlobalControl.Size = new System.Drawing.Size(760, 80);
            this.grpGlobalControl.TabIndex = 0;
            this.grpGlobalControl.TabStop = false;
            this.grpGlobalControl.Text = "Global Control";

            //
            // btnInitialize
            //
            this.btnInitialize.BackColor = System.Drawing.Color.ForestGreen;
            this.btnInitialize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInitialize.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnInitialize.ForeColor = System.Drawing.Color.White;
            this.btnInitialize.Location = new System.Drawing.Point(15, 30);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Size = new System.Drawing.Size(100, 35);
            this.btnInitialize.TabIndex = 0;
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
            this.btnHomeAll.Location = new System.Drawing.Point(125, 30);
            this.btnHomeAll.Name = "btnHomeAll";
            this.btnHomeAll.Size = new System.Drawing.Size(100, 35);
            this.btnHomeAll.TabIndex = 1;
            this.btnHomeAll.Text = "Home All";
            this.btnHomeAll.UseVisualStyleBackColor = false;
            this.btnHomeAll.Click += new System.EventHandler(this.btnHomeAll_Click);

            //
            // btnStopAll
            //
            this.btnStopAll.BackColor = System.Drawing.Color.FromArgb(255, 128, 0);
            this.btnStopAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopAll.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnStopAll.ForeColor = System.Drawing.Color.White;
            this.btnStopAll.Location = new System.Drawing.Point(235, 30);
            this.btnStopAll.Name = "btnStopAll";
            this.btnStopAll.Size = new System.Drawing.Size(100, 35);
            this.btnStopAll.TabIndex = 2;
            this.btnStopAll.Text = "Stop All";
            this.btnStopAll.UseVisualStyleBackColor = false;
            this.btnStopAll.Click += new System.EventHandler(this.btnStopAll_Click);

            //
            // btnEmergencyStop
            //
            this.btnEmergencyStop.BackColor = System.Drawing.Color.Red;
            this.btnEmergencyStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmergencyStop.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEmergencyStop.ForeColor = System.Drawing.Color.White;
            this.btnEmergencyStop.Location = new System.Drawing.Point(345, 30);
            this.btnEmergencyStop.Name = "btnEmergencyStop";
            this.btnEmergencyStop.Size = new System.Drawing.Size(100, 35);
            this.btnEmergencyStop.TabIndex = 3;
            this.btnEmergencyStop.Text = "EMO";
            this.btnEmergencyStop.UseVisualStyleBackColor = false;
            this.btnEmergencyStop.Click += new System.EventHandler(this.btnEmergencyStop_Click);

            //
            // lblInitStatus
            //
            this.lblInitStatus.AutoSize = true;
            this.lblInitStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblInitStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblInitStatus.Location = new System.Drawing.Point(480, 37);
            this.lblInitStatus.Name = "lblInitStatus";
            this.lblInitStatus.Size = new System.Drawing.Size(108, 19);
            this.lblInitStatus.TabIndex = 4;
            this.lblInitStatus.Text = "Not Initialized";

            //
            // lblAllHomedStatus
            //
            this.lblAllHomedStatus.AutoSize = true;
            this.lblAllHomedStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAllHomedStatus.ForeColor = System.Drawing.Color.Orange;
            this.lblAllHomedStatus.Location = new System.Drawing.Point(610, 37);
            this.lblAllHomedStatus.Name = "lblAllHomedStatus";
            this.lblAllHomedStatus.Size = new System.Drawing.Size(82, 19);
            this.lblAllHomedStatus.TabIndex = 5;
            this.lblAllHomedStatus.Text = "Not Homed";

            //
            // MotionPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelAxes);
            this.Controls.Add(this.panelGlobalControls);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "MotionPanel";
            this.Size = new System.Drawing.Size(780, 660);

            this.panelAxes.ResumeLayout(false);
            this.panelGlobalControls.ResumeLayout(false);
            this.grpGlobalControl.ResumeLayout(false);
            this.grpGlobalControl.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelAxes;
        private AxisControlPanel axisWedge;
        private AxisControlPanel axisChuck;
        private AxisControlPanel axisCentering1;
        private AxisControlPanel axisCentering2;
        private System.Windows.Forms.Panel panelGlobalControls;
        private System.Windows.Forms.GroupBox grpGlobalControl;
        private System.Windows.Forms.Button btnInitialize;
        private System.Windows.Forms.Button btnHomeAll;
        private System.Windows.Forms.Button btnStopAll;
        private System.Windows.Forms.Button btnEmergencyStop;
        private System.Windows.Forms.Label lblInitStatus;
        private System.Windows.Forms.Label lblAllHomedStatus;
    }
}
