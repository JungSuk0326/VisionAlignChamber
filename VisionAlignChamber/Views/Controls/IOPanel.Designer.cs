namespace VisionAlignChamber.Views.Controls
{
    partial class IOPanel
    {
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.grpInputs = new System.Windows.Forms.GroupBox();
            this.tableInputs = new System.Windows.Forms.TableLayoutPanel();
            this.lblSensor1Label = new System.Windows.Forms.Label();
            this.lblSensor1 = new System.Windows.Forms.Label();
            this.lblSensor2Label = new System.Windows.Forms.Label();
            this.lblSensor2 = new System.Windows.Forms.Label();
            this.lblPNCheckPLabel = new System.Windows.Forms.Label();
            this.lblPNCheckP = new System.Windows.Forms.Label();
            this.lblPNCheckNLabel = new System.Windows.Forms.Label();
            this.lblPNCheckN = new System.Windows.Forms.Label();

            this.grpLiftPin = new System.Windows.Forms.GroupBox();
            this.chkLiftPinVacuum = new System.Windows.Forms.CheckBox();
            this.btnLiftPinVacuumOn = new System.Windows.Forms.Button();
            this.btnLiftPinVacuumOff = new System.Windows.Forms.Button();
            this.chkLiftPinBlow = new System.Windows.Forms.CheckBox();
            this.btnLiftPinBlowOn = new System.Windows.Forms.Button();
            this.btnLiftPinBlowOff = new System.Windows.Forms.Button();

            this.grpChuck = new System.Windows.Forms.GroupBox();
            this.chkChuckVacuum = new System.Windows.Forms.CheckBox();
            this.btnChuckVacuumOn = new System.Windows.Forms.Button();
            this.btnChuckVacuumOff = new System.Windows.Forms.Button();
            this.chkChuckBlow = new System.Windows.Forms.CheckBox();
            this.btnChuckBlowOn = new System.Windows.Forms.Button();
            this.btnChuckBlowOff = new System.Windows.Forms.Button();

            this.grpVisionLight = new System.Windows.Forms.GroupBox();
            this.chkVisionLight = new System.Windows.Forms.CheckBox();
            this.btnVisionLightOn = new System.Windows.Forms.Button();
            this.btnVisionLightOff = new System.Windows.Forms.Button();

            this.btnAllOff = new System.Windows.Forms.Button();

            this.grpInputs.SuspendLayout();
            this.tableInputs.SuspendLayout();
            this.grpLiftPin.SuspendLayout();
            this.grpChuck.SuspendLayout();
            this.grpVisionLight.SuspendLayout();
            this.SuspendLayout();

            //
            // grpInputs
            //
            this.grpInputs.Controls.Add(this.tableInputs);
            this.grpInputs.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpInputs.Location = new System.Drawing.Point(10, 10);
            this.grpInputs.Name = "grpInputs";
            this.grpInputs.Size = new System.Drawing.Size(350, 150);
            this.grpInputs.TabIndex = 0;
            this.grpInputs.TabStop = false;
            this.grpInputs.Text = "Digital Inputs";

            //
            // tableInputs
            //
            this.tableInputs.ColumnCount = 2;
            this.tableInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableInputs.Controls.Add(this.lblSensor1Label, 0, 0);
            this.tableInputs.Controls.Add(this.lblSensor1, 1, 0);
            this.tableInputs.Controls.Add(this.lblSensor2Label, 0, 1);
            this.tableInputs.Controls.Add(this.lblSensor2, 1, 1);
            this.tableInputs.Controls.Add(this.lblPNCheckPLabel, 0, 2);
            this.tableInputs.Controls.Add(this.lblPNCheckP, 1, 2);
            this.tableInputs.Controls.Add(this.lblPNCheckNLabel, 0, 3);
            this.tableInputs.Controls.Add(this.lblPNCheckN, 1, 3);
            this.tableInputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableInputs.Location = new System.Drawing.Point(3, 19);
            this.tableInputs.Name = "tableInputs";
            this.tableInputs.RowCount = 4;
            this.tableInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableInputs.Size = new System.Drawing.Size(344, 128);
            this.tableInputs.TabIndex = 0;

            //
            // lblSensor1Label
            //
            this.lblSensor1Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSensor1Label.Location = new System.Drawing.Point(3, 0);
            this.lblSensor1Label.Name = "lblSensor1Label";
            this.lblSensor1Label.Size = new System.Drawing.Size(234, 32);
            this.lblSensor1Label.TabIndex = 0;
            this.lblSensor1Label.Text = "Sensor 1 - Wafer Check";
            this.lblSensor1Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            //
            // lblSensor1
            //
            this.lblSensor1.BackColor = System.Drawing.Color.Gray;
            this.lblSensor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSensor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSensor1.Location = new System.Drawing.Point(243, 3);
            this.lblSensor1.Margin = new System.Windows.Forms.Padding(3);
            this.lblSensor1.Name = "lblSensor1";
            this.lblSensor1.Size = new System.Drawing.Size(98, 26);
            this.lblSensor1.TabIndex = 1;

            //
            // lblSensor2Label
            //
            this.lblSensor2Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSensor2Label.Location = new System.Drawing.Point(3, 32);
            this.lblSensor2Label.Name = "lblSensor2Label";
            this.lblSensor2Label.Size = new System.Drawing.Size(234, 32);
            this.lblSensor2Label.TabIndex = 2;
            this.lblSensor2Label.Text = "Sensor 2 - Wafer Check";
            this.lblSensor2Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            //
            // lblSensor2
            //
            this.lblSensor2.BackColor = System.Drawing.Color.Gray;
            this.lblSensor2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSensor2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSensor2.Location = new System.Drawing.Point(243, 35);
            this.lblSensor2.Margin = new System.Windows.Forms.Padding(3);
            this.lblSensor2.Name = "lblSensor2";
            this.lblSensor2.Size = new System.Drawing.Size(98, 26);
            this.lblSensor2.TabIndex = 3;

            //
            // lblPNCheckPLabel
            //
            this.lblPNCheckPLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPNCheckPLabel.Location = new System.Drawing.Point(3, 64);
            this.lblPNCheckPLabel.Name = "lblPNCheckPLabel";
            this.lblPNCheckPLabel.Size = new System.Drawing.Size(234, 32);
            this.lblPNCheckPLabel.TabIndex = 4;
            this.lblPNCheckPLabel.Text = "PN Check - P";
            this.lblPNCheckPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            //
            // lblPNCheckP
            //
            this.lblPNCheckP.BackColor = System.Drawing.Color.Gray;
            this.lblPNCheckP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPNCheckP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPNCheckP.Location = new System.Drawing.Point(243, 67);
            this.lblPNCheckP.Margin = new System.Windows.Forms.Padding(3);
            this.lblPNCheckP.Name = "lblPNCheckP";
            this.lblPNCheckP.Size = new System.Drawing.Size(98, 26);
            this.lblPNCheckP.TabIndex = 5;

            //
            // lblPNCheckNLabel
            //
            this.lblPNCheckNLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPNCheckNLabel.Location = new System.Drawing.Point(3, 96);
            this.lblPNCheckNLabel.Name = "lblPNCheckNLabel";
            this.lblPNCheckNLabel.Size = new System.Drawing.Size(234, 32);
            this.lblPNCheckNLabel.TabIndex = 6;
            this.lblPNCheckNLabel.Text = "PN Check - N";
            this.lblPNCheckNLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            //
            // lblPNCheckN
            //
            this.lblPNCheckN.BackColor = System.Drawing.Color.Gray;
            this.lblPNCheckN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPNCheckN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPNCheckN.Location = new System.Drawing.Point(243, 99);
            this.lblPNCheckN.Margin = new System.Windows.Forms.Padding(3);
            this.lblPNCheckN.Name = "lblPNCheckN";
            this.lblPNCheckN.Size = new System.Drawing.Size(98, 26);
            this.lblPNCheckN.TabIndex = 7;

            //
            // grpLiftPin
            //
            this.grpLiftPin.Controls.Add(this.chkLiftPinVacuum);
            this.grpLiftPin.Controls.Add(this.btnLiftPinVacuumOn);
            this.grpLiftPin.Controls.Add(this.btnLiftPinVacuumOff);
            this.grpLiftPin.Controls.Add(this.chkLiftPinBlow);
            this.grpLiftPin.Controls.Add(this.btnLiftPinBlowOn);
            this.grpLiftPin.Controls.Add(this.btnLiftPinBlowOff);
            this.grpLiftPin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpLiftPin.Location = new System.Drawing.Point(10, 170);
            this.grpLiftPin.Name = "grpLiftPin";
            this.grpLiftPin.Size = new System.Drawing.Size(350, 100);
            this.grpLiftPin.TabIndex = 1;
            this.grpLiftPin.TabStop = false;
            this.grpLiftPin.Text = "Lift Pin";

            //
            // chkLiftPinVacuum
            //
            this.chkLiftPinVacuum.AutoSize = true;
            this.chkLiftPinVacuum.Enabled = false;
            this.chkLiftPinVacuum.Location = new System.Drawing.Point(15, 30);
            this.chkLiftPinVacuum.Name = "chkLiftPinVacuum";
            this.chkLiftPinVacuum.Size = new System.Drawing.Size(70, 19);
            this.chkLiftPinVacuum.TabIndex = 0;
            this.chkLiftPinVacuum.Text = "Vacuum";

            //
            // btnLiftPinVacuumOn
            //
            this.btnLiftPinVacuumOn.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnLiftPinVacuumOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLiftPinVacuumOn.ForeColor = System.Drawing.Color.White;
            this.btnLiftPinVacuumOn.Location = new System.Drawing.Point(100, 25);
            this.btnLiftPinVacuumOn.Name = "btnLiftPinVacuumOn";
            this.btnLiftPinVacuumOn.Size = new System.Drawing.Size(60, 28);
            this.btnLiftPinVacuumOn.TabIndex = 1;
            this.btnLiftPinVacuumOn.Text = "ON";
            this.btnLiftPinVacuumOn.UseVisualStyleBackColor = false;
            this.btnLiftPinVacuumOn.Click += new System.EventHandler(this.btnLiftPinVacuumOn_Click);

            //
            // btnLiftPinVacuumOff
            //
            this.btnLiftPinVacuumOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLiftPinVacuumOff.Location = new System.Drawing.Point(165, 25);
            this.btnLiftPinVacuumOff.Name = "btnLiftPinVacuumOff";
            this.btnLiftPinVacuumOff.Size = new System.Drawing.Size(60, 28);
            this.btnLiftPinVacuumOff.TabIndex = 2;
            this.btnLiftPinVacuumOff.Text = "OFF";
            this.btnLiftPinVacuumOff.UseVisualStyleBackColor = true;
            this.btnLiftPinVacuumOff.Click += new System.EventHandler(this.btnLiftPinVacuumOff_Click);

            //
            // chkLiftPinBlow
            //
            this.chkLiftPinBlow.AutoSize = true;
            this.chkLiftPinBlow.Enabled = false;
            this.chkLiftPinBlow.Location = new System.Drawing.Point(15, 65);
            this.chkLiftPinBlow.Name = "chkLiftPinBlow";
            this.chkLiftPinBlow.Size = new System.Drawing.Size(51, 19);
            this.chkLiftPinBlow.TabIndex = 3;
            this.chkLiftPinBlow.Text = "Blow";

            //
            // btnLiftPinBlowOn
            //
            this.btnLiftPinBlowOn.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnLiftPinBlowOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLiftPinBlowOn.ForeColor = System.Drawing.Color.White;
            this.btnLiftPinBlowOn.Location = new System.Drawing.Point(100, 60);
            this.btnLiftPinBlowOn.Name = "btnLiftPinBlowOn";
            this.btnLiftPinBlowOn.Size = new System.Drawing.Size(60, 28);
            this.btnLiftPinBlowOn.TabIndex = 4;
            this.btnLiftPinBlowOn.Text = "ON";
            this.btnLiftPinBlowOn.UseVisualStyleBackColor = false;
            this.btnLiftPinBlowOn.Click += new System.EventHandler(this.btnLiftPinBlowOn_Click);

            //
            // btnLiftPinBlowOff
            //
            this.btnLiftPinBlowOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLiftPinBlowOff.Location = new System.Drawing.Point(165, 60);
            this.btnLiftPinBlowOff.Name = "btnLiftPinBlowOff";
            this.btnLiftPinBlowOff.Size = new System.Drawing.Size(60, 28);
            this.btnLiftPinBlowOff.TabIndex = 5;
            this.btnLiftPinBlowOff.Text = "OFF";
            this.btnLiftPinBlowOff.UseVisualStyleBackColor = true;
            this.btnLiftPinBlowOff.Click += new System.EventHandler(this.btnLiftPinBlowOff_Click);

            //
            // grpChuck
            //
            this.grpChuck.Controls.Add(this.chkChuckVacuum);
            this.grpChuck.Controls.Add(this.btnChuckVacuumOn);
            this.grpChuck.Controls.Add(this.btnChuckVacuumOff);
            this.grpChuck.Controls.Add(this.chkChuckBlow);
            this.grpChuck.Controls.Add(this.btnChuckBlowOn);
            this.grpChuck.Controls.Add(this.btnChuckBlowOff);
            this.grpChuck.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpChuck.Location = new System.Drawing.Point(10, 280);
            this.grpChuck.Name = "grpChuck";
            this.grpChuck.Size = new System.Drawing.Size(350, 100);
            this.grpChuck.TabIndex = 2;
            this.grpChuck.TabStop = false;
            this.grpChuck.Text = "Chuck";

            //
            // chkChuckVacuum
            //
            this.chkChuckVacuum.AutoSize = true;
            this.chkChuckVacuum.Enabled = false;
            this.chkChuckVacuum.Location = new System.Drawing.Point(15, 30);
            this.chkChuckVacuum.Name = "chkChuckVacuum";
            this.chkChuckVacuum.Size = new System.Drawing.Size(70, 19);
            this.chkChuckVacuum.TabIndex = 0;
            this.chkChuckVacuum.Text = "Vacuum";

            //
            // btnChuckVacuumOn
            //
            this.btnChuckVacuumOn.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnChuckVacuumOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChuckVacuumOn.ForeColor = System.Drawing.Color.White;
            this.btnChuckVacuumOn.Location = new System.Drawing.Point(100, 25);
            this.btnChuckVacuumOn.Name = "btnChuckVacuumOn";
            this.btnChuckVacuumOn.Size = new System.Drawing.Size(60, 28);
            this.btnChuckVacuumOn.TabIndex = 1;
            this.btnChuckVacuumOn.Text = "ON";
            this.btnChuckVacuumOn.UseVisualStyleBackColor = false;
            this.btnChuckVacuumOn.Click += new System.EventHandler(this.btnChuckVacuumOn_Click);

            //
            // btnChuckVacuumOff
            //
            this.btnChuckVacuumOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChuckVacuumOff.Location = new System.Drawing.Point(165, 25);
            this.btnChuckVacuumOff.Name = "btnChuckVacuumOff";
            this.btnChuckVacuumOff.Size = new System.Drawing.Size(60, 28);
            this.btnChuckVacuumOff.TabIndex = 2;
            this.btnChuckVacuumOff.Text = "OFF";
            this.btnChuckVacuumOff.UseVisualStyleBackColor = true;
            this.btnChuckVacuumOff.Click += new System.EventHandler(this.btnChuckVacuumOff_Click);

            //
            // chkChuckBlow
            //
            this.chkChuckBlow.AutoSize = true;
            this.chkChuckBlow.Enabled = false;
            this.chkChuckBlow.Location = new System.Drawing.Point(15, 65);
            this.chkChuckBlow.Name = "chkChuckBlow";
            this.chkChuckBlow.Size = new System.Drawing.Size(51, 19);
            this.chkChuckBlow.TabIndex = 3;
            this.chkChuckBlow.Text = "Blow";

            //
            // btnChuckBlowOn
            //
            this.btnChuckBlowOn.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnChuckBlowOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChuckBlowOn.ForeColor = System.Drawing.Color.White;
            this.btnChuckBlowOn.Location = new System.Drawing.Point(100, 60);
            this.btnChuckBlowOn.Name = "btnChuckBlowOn";
            this.btnChuckBlowOn.Size = new System.Drawing.Size(60, 28);
            this.btnChuckBlowOn.TabIndex = 4;
            this.btnChuckBlowOn.Text = "ON";
            this.btnChuckBlowOn.UseVisualStyleBackColor = false;
            this.btnChuckBlowOn.Click += new System.EventHandler(this.btnChuckBlowOn_Click);

            //
            // btnChuckBlowOff
            //
            this.btnChuckBlowOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChuckBlowOff.Location = new System.Drawing.Point(165, 60);
            this.btnChuckBlowOff.Name = "btnChuckBlowOff";
            this.btnChuckBlowOff.Size = new System.Drawing.Size(60, 28);
            this.btnChuckBlowOff.TabIndex = 5;
            this.btnChuckBlowOff.Text = "OFF";
            this.btnChuckBlowOff.UseVisualStyleBackColor = true;
            this.btnChuckBlowOff.Click += new System.EventHandler(this.btnChuckBlowOff_Click);

            //
            // grpVisionLight
            //
            this.grpVisionLight.Controls.Add(this.chkVisionLight);
            this.grpVisionLight.Controls.Add(this.btnVisionLightOn);
            this.grpVisionLight.Controls.Add(this.btnVisionLightOff);
            this.grpVisionLight.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpVisionLight.Location = new System.Drawing.Point(10, 390);
            this.grpVisionLight.Name = "grpVisionLight";
            this.grpVisionLight.Size = new System.Drawing.Size(350, 65);
            this.grpVisionLight.TabIndex = 3;
            this.grpVisionLight.TabStop = false;
            this.grpVisionLight.Text = "Vision Light";

            //
            // chkVisionLight
            //
            this.chkVisionLight.AutoSize = true;
            this.chkVisionLight.Enabled = false;
            this.chkVisionLight.Location = new System.Drawing.Point(15, 30);
            this.chkVisionLight.Name = "chkVisionLight";
            this.chkVisionLight.Size = new System.Drawing.Size(53, 19);
            this.chkVisionLight.TabIndex = 0;
            this.chkVisionLight.Text = "Light";

            //
            // btnVisionLightOn
            //
            this.btnVisionLightOn.BackColor = System.Drawing.Color.FromArgb(0, 122, 204);
            this.btnVisionLightOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVisionLightOn.ForeColor = System.Drawing.Color.White;
            this.btnVisionLightOn.Location = new System.Drawing.Point(100, 25);
            this.btnVisionLightOn.Name = "btnVisionLightOn";
            this.btnVisionLightOn.Size = new System.Drawing.Size(60, 28);
            this.btnVisionLightOn.TabIndex = 1;
            this.btnVisionLightOn.Text = "ON";
            this.btnVisionLightOn.UseVisualStyleBackColor = false;
            this.btnVisionLightOn.Click += new System.EventHandler(this.btnVisionLightOn_Click);

            //
            // btnVisionLightOff
            //
            this.btnVisionLightOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVisionLightOff.Location = new System.Drawing.Point(165, 25);
            this.btnVisionLightOff.Name = "btnVisionLightOff";
            this.btnVisionLightOff.Size = new System.Drawing.Size(60, 28);
            this.btnVisionLightOff.TabIndex = 2;
            this.btnVisionLightOff.Text = "OFF";
            this.btnVisionLightOff.UseVisualStyleBackColor = true;
            this.btnVisionLightOff.Click += new System.EventHandler(this.btnVisionLightOff_Click);

            //
            // btnAllOff
            //
            this.btnAllOff.BackColor = System.Drawing.Color.OrangeRed;
            this.btnAllOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllOff.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAllOff.ForeColor = System.Drawing.Color.White;
            this.btnAllOff.Location = new System.Drawing.Point(10, 470);
            this.btnAllOff.Name = "btnAllOff";
            this.btnAllOff.Size = new System.Drawing.Size(350, 40);
            this.btnAllOff.TabIndex = 4;
            this.btnAllOff.Text = "All Output OFF";
            this.btnAllOff.UseVisualStyleBackColor = false;
            this.btnAllOff.Click += new System.EventHandler(this.btnAllOff_Click);

            //
            // IOPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpInputs);
            this.Controls.Add(this.grpLiftPin);
            this.Controls.Add(this.grpChuck);
            this.Controls.Add(this.grpVisionLight);
            this.Controls.Add(this.btnAllOff);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "IOPanel";
            this.Size = new System.Drawing.Size(780, 660);

            this.grpInputs.ResumeLayout(false);
            this.tableInputs.ResumeLayout(false);
            this.grpLiftPin.ResumeLayout(false);
            this.grpLiftPin.PerformLayout();
            this.grpChuck.ResumeLayout(false);
            this.grpChuck.PerformLayout();
            this.grpVisionLight.ResumeLayout(false);
            this.grpVisionLight.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpInputs;
        private System.Windows.Forms.TableLayoutPanel tableInputs;
        private System.Windows.Forms.Label lblSensor1Label;
        private System.Windows.Forms.Label lblSensor1;
        private System.Windows.Forms.Label lblSensor2Label;
        private System.Windows.Forms.Label lblSensor2;
        private System.Windows.Forms.Label lblPNCheckPLabel;
        private System.Windows.Forms.Label lblPNCheckP;
        private System.Windows.Forms.Label lblPNCheckNLabel;
        private System.Windows.Forms.Label lblPNCheckN;
        private System.Windows.Forms.GroupBox grpLiftPin;
        private System.Windows.Forms.CheckBox chkLiftPinVacuum;
        private System.Windows.Forms.Button btnLiftPinVacuumOn;
        private System.Windows.Forms.Button btnLiftPinVacuumOff;
        private System.Windows.Forms.CheckBox chkLiftPinBlow;
        private System.Windows.Forms.Button btnLiftPinBlowOn;
        private System.Windows.Forms.Button btnLiftPinBlowOff;
        private System.Windows.Forms.GroupBox grpChuck;
        private System.Windows.Forms.CheckBox chkChuckVacuum;
        private System.Windows.Forms.Button btnChuckVacuumOn;
        private System.Windows.Forms.Button btnChuckVacuumOff;
        private System.Windows.Forms.CheckBox chkChuckBlow;
        private System.Windows.Forms.Button btnChuckBlowOn;
        private System.Windows.Forms.Button btnChuckBlowOff;
        private System.Windows.Forms.GroupBox grpVisionLight;
        private System.Windows.Forms.CheckBox chkVisionLight;
        private System.Windows.Forms.Button btnVisionLightOn;
        private System.Windows.Forms.Button btnVisionLightOff;
        private System.Windows.Forms.Button btnAllOff;
    }
}
