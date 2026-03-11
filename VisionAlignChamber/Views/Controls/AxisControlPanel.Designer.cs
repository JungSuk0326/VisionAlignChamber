namespace VisionAlignChamber.Views.Controls
{
    partial class AxisControlPanel
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.grpAxis = new System.Windows.Forms.GroupBox();
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblAxisName = new System.Windows.Forms.Label();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.lblPlusLimit = new System.Windows.Forms.Label();
            this.lblMinusLimit = new System.Windows.Forms.Label();
            this.btnServo = new System.Windows.Forms.Button();
            this.btnAlarmClear = new System.Windows.Forms.Button();
            this.lblMovingStatus = new System.Windows.Forms.Label();
            this.chkHomed = new System.Windows.Forms.CheckBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.lblPositionLabel = new System.Windows.Forms.Label();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.lblTargetLabel = new System.Windows.Forms.Label();
            this.txtTargetPosition = new System.Windows.Forms.TextBox();
            this.btnMove = new System.Windows.Forms.Button();
            this.lblJogLabel = new System.Windows.Forms.Label();
            this.panelJogParams = new System.Windows.Forms.Panel();
            this.lblVel = new System.Windows.Forms.Label();
            this.txtJogVelocity = new System.Windows.Forms.TextBox();
            this.lblAcc = new System.Windows.Forms.Label();
            this.txtJogAccel = new System.Windows.Forms.TextBox();
            this.lblDec = new System.Windows.Forms.Label();
            this.txtJogDecel = new System.Windows.Forms.TextBox();
            this.panelJog = new System.Windows.Forms.Panel();
            this.btnJogPlus = new System.Windows.Forms.Button();
            this.btnJogMinus = new System.Windows.Forms.Button();
            this.grpAxis.SuspendLayout();
            this.tableLayout.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.panelJogParams.SuspendLayout();
            this.panelJog.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpAxis
            // 
            this.grpAxis.Controls.Add(this.tableLayout);
            this.grpAxis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAxis.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpAxis.Location = new System.Drawing.Point(0, 0);
            this.grpAxis.Name = "grpAxis";
            this.grpAxis.Padding = new System.Windows.Forms.Padding(5);
            this.grpAxis.Size = new System.Drawing.Size(588, 139);
            this.grpAxis.TabIndex = 0;
            this.grpAxis.TabStop = false;
            // 
            // tableLayout
            // 
            this.tableLayout.ColumnCount = 4;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayout.Controls.Add(this.lblAxisName, 0, 0);
            this.tableLayout.Controls.Add(this.panelStatus, 1, 0);
            this.tableLayout.Controls.Add(this.panelButtons, 3, 0);
            this.tableLayout.Controls.Add(this.lblPositionLabel, 0, 1);
            this.tableLayout.Controls.Add(this.txtPosition, 1, 1);
            this.tableLayout.Controls.Add(this.lblUnit, 2, 1);
            this.tableLayout.Controls.Add(this.lblTargetLabel, 0, 2);
            this.tableLayout.Controls.Add(this.txtTargetPosition, 1, 2);
            this.tableLayout.Controls.Add(this.btnMove, 3, 2);
            this.tableLayout.Controls.Add(this.lblJogLabel, 0, 3);
            this.tableLayout.Controls.Add(this.panelJogParams, 1, 3);
            this.tableLayout.Controls.Add(this.panelJog, 3, 3);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(5, 21);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 4;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.Size = new System.Drawing.Size(578, 113);
            this.tableLayout.TabIndex = 0;
            // 
            // lblAxisName
            // 
            this.lblAxisName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAxisName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAxisName.Location = new System.Drawing.Point(3, 0);
            this.lblAxisName.Name = "lblAxisName";
            this.lblAxisName.Size = new System.Drawing.Size(74, 28);
            this.lblAxisName.TabIndex = 0;
            this.lblAxisName.Text = "Axis";
            this.lblAxisName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelStatus
            // 
            this.tableLayout.SetColumnSpan(this.panelStatus, 2);
            this.panelStatus.Controls.Add(this.lblPlusLimit);
            this.panelStatus.Controls.Add(this.lblMinusLimit);
            this.panelStatus.Controls.Add(this.btnServo);
            this.panelStatus.Controls.Add(this.btnAlarmClear);
            this.panelStatus.Controls.Add(this.lblMovingStatus);
            this.panelStatus.Controls.Add(this.chkHomed);
            this.panelStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatus.Location = new System.Drawing.Point(83, 3);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(392, 22);
            this.panelStatus.TabIndex = 1;
            // 
            // lblPlusLimit
            // 
            this.lblPlusLimit.AutoSize = true;
            this.lblPlusLimit.BackColor = System.Drawing.Color.DarkGray;
            this.lblPlusLimit.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.lblPlusLimit.ForeColor = System.Drawing.Color.White;
            this.lblPlusLimit.Location = new System.Drawing.Point(286, 3);
            this.lblPlusLimit.Name = "lblPlusLimit";
            this.lblPlusLimit.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblPlusLimit.Size = new System.Drawing.Size(21, 14);
            this.lblPlusLimit.TabIndex = 5;
            this.lblPlusLimit.Text = "+L";
            // 
            // lblMinusLimit
            // 
            this.lblMinusLimit.AutoSize = true;
            this.lblMinusLimit.BackColor = System.Drawing.Color.DarkGray;
            this.lblMinusLimit.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.lblMinusLimit.ForeColor = System.Drawing.Color.White;
            this.lblMinusLimit.Location = new System.Drawing.Point(245, 3);
            this.lblMinusLimit.Name = "lblMinusLimit";
            this.lblMinusLimit.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.lblMinusLimit.Size = new System.Drawing.Size(18, 14);
            this.lblMinusLimit.TabIndex = 6;
            this.lblMinusLimit.Text = "-L";
            // 
            // btnServo
            // 
            this.btnServo.BackColor = System.Drawing.Color.Gray;
            this.btnServo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnServo.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.btnServo.ForeColor = System.Drawing.Color.White;
            this.btnServo.Location = new System.Drawing.Point(115, 0);
            this.btnServo.Name = "btnServo";
            this.btnServo.Size = new System.Drawing.Size(41, 20);
            this.btnServo.TabIndex = 2;
            this.btnServo.Text = "OFF";
            this.btnServo.UseVisualStyleBackColor = false;
            this.btnServo.Click += new System.EventHandler(this.btnServo_Click);
            // 
            // btnAlarmClear
            // 
            this.btnAlarmClear.BackColor = System.Drawing.Color.DarkGray;
            this.btnAlarmClear.Enabled = false;
            this.btnAlarmClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAlarmClear.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.btnAlarmClear.ForeColor = System.Drawing.Color.White;
            this.btnAlarmClear.Location = new System.Drawing.Point(165, 0);
            this.btnAlarmClear.Name = "btnAlarmClear";
            this.btnAlarmClear.Size = new System.Drawing.Size(40, 20);
            this.btnAlarmClear.TabIndex = 4;
            this.btnAlarmClear.Text = "CLR";
            this.btnAlarmClear.UseVisualStyleBackColor = false;
            this.btnAlarmClear.Click += new System.EventHandler(this.btnAlarmClear_Click);
            // 
            // lblMovingStatus
            // 
            this.lblMovingStatus.AutoSize = true;
            this.lblMovingStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMovingStatus.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblMovingStatus.Location = new System.Drawing.Point(0, 2);
            this.lblMovingStatus.Name = "lblMovingStatus";
            this.lblMovingStatus.Size = new System.Drawing.Size(28, 15);
            this.lblMovingStatus.TabIndex = 0;
            this.lblMovingStatus.Text = "Idle";
            // 
            // chkHomed
            // 
            this.chkHomed.AutoSize = true;
            this.chkHomed.Enabled = false;
            this.chkHomed.Location = new System.Drawing.Point(50, 2);
            this.chkHomed.Name = "chkHomed";
            this.chkHomed.Size = new System.Drawing.Size(66, 19);
            this.chkHomed.TabIndex = 1;
            this.chkHomed.Text = "Homed";
            this.chkHomed.UseVisualStyleBackColor = true;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnStop);
            this.panelButtons.Controls.Add(this.btnHome);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(481, 3);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(94, 22);
            this.panelButtons.TabIndex = 2;
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.OrangeRed;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(48, 0);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(45, 22);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnHome
            // 
            this.btnHome.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnHome.Location = new System.Drawing.Point(0, 0);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(45, 22);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // lblPositionLabel
            // 
            this.lblPositionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPositionLabel.Location = new System.Drawing.Point(3, 28);
            this.lblPositionLabel.Name = "lblPositionLabel";
            this.lblPositionLabel.Size = new System.Drawing.Size(74, 28);
            this.lblPositionLabel.TabIndex = 3;
            this.lblPositionLabel.Text = "Position";
            this.lblPositionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPosition
            // 
            this.txtPosition.BackColor = System.Drawing.Color.Black;
            this.txtPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPosition.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtPosition.ForeColor = System.Drawing.Color.LimeGreen;
            this.txtPosition.Location = new System.Drawing.Point(83, 31);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.ReadOnly = true;
            this.txtPosition.Size = new System.Drawing.Size(342, 23);
            this.txtPosition.TabIndex = 4;
            this.txtPosition.Text = "0.00";
            this.txtPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblUnit
            // 
            this.lblUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUnit.Location = new System.Drawing.Point(431, 28);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(44, 28);
            this.lblUnit.TabIndex = 5;
            this.lblUnit.Text = "pulse";
            this.lblUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTargetLabel
            // 
            this.lblTargetLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTargetLabel.Location = new System.Drawing.Point(3, 56);
            this.lblTargetLabel.Name = "lblTargetLabel";
            this.lblTargetLabel.Size = new System.Drawing.Size(74, 28);
            this.lblTargetLabel.TabIndex = 6;
            this.lblTargetLabel.Text = "Target";
            this.lblTargetLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTargetPosition
            // 
            this.tableLayout.SetColumnSpan(this.txtTargetPosition, 2);
            this.txtTargetPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTargetPosition.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtTargetPosition.Location = new System.Drawing.Point(83, 59);
            this.txtTargetPosition.Name = "txtTargetPosition";
            this.txtTargetPosition.Size = new System.Drawing.Size(392, 23);
            this.txtTargetPosition.TabIndex = 7;
            this.txtTargetPosition.Text = "0";
            this.txtTargetPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnMove
            // 
            this.btnMove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnMove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMove.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnMove.ForeColor = System.Drawing.Color.White;
            this.btnMove.Location = new System.Drawing.Point(481, 59);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(94, 22);
            this.btnMove.TabIndex = 8;
            this.btnMove.Text = "Move";
            this.btnMove.UseVisualStyleBackColor = false;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // lblJogLabel
            // 
            this.lblJogLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblJogLabel.Location = new System.Drawing.Point(3, 84);
            this.lblJogLabel.Name = "lblJogLabel";
            this.lblJogLabel.Size = new System.Drawing.Size(74, 29);
            this.lblJogLabel.TabIndex = 9;
            this.lblJogLabel.Text = "Jog";
            this.lblJogLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelJogParams
            // 
            this.tableLayout.SetColumnSpan(this.panelJogParams, 2);
            this.panelJogParams.Controls.Add(this.lblVel);
            this.panelJogParams.Controls.Add(this.txtJogVelocity);
            this.panelJogParams.Controls.Add(this.lblAcc);
            this.panelJogParams.Controls.Add(this.txtJogAccel);
            this.panelJogParams.Controls.Add(this.lblDec);
            this.panelJogParams.Controls.Add(this.txtJogDecel);
            this.panelJogParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelJogParams.Location = new System.Drawing.Point(83, 87);
            this.panelJogParams.Name = "panelJogParams";
            this.panelJogParams.Size = new System.Drawing.Size(392, 23);
            this.panelJogParams.TabIndex = 10;
            // 
            // lblVel
            // 
            this.lblVel.AutoSize = true;
            this.lblVel.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lblVel.Location = new System.Drawing.Point(0, 4);
            this.lblVel.Name = "lblVel";
            this.lblVel.Size = new System.Drawing.Size(19, 12);
            this.lblVel.TabIndex = 0;
            this.lblVel.Text = "Vel:";
            // 
            // txtJogVelocity
            // 
            this.txtJogVelocity.Font = new System.Drawing.Font("Consolas", 8F);
            this.txtJogVelocity.Location = new System.Drawing.Point(25, 1);
            this.txtJogVelocity.Name = "txtJogVelocity";
            this.txtJogVelocity.Size = new System.Drawing.Size(83, 20);
            this.txtJogVelocity.TabIndex = 1;
            this.txtJogVelocity.Text = "10000";
            this.txtJogVelocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblAcc
            // 
            this.lblAcc.AutoSize = true;
            this.lblAcc.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lblAcc.Location = new System.Drawing.Point(114, 4);
            this.lblAcc.Name = "lblAcc";
            this.lblAcc.Size = new System.Drawing.Size(23, 12);
            this.lblAcc.TabIndex = 2;
            this.lblAcc.Text = "Acc:";
            // 
            // txtJogAccel
            // 
            this.txtJogAccel.Font = new System.Drawing.Font("Consolas", 8F);
            this.txtJogAccel.Location = new System.Drawing.Point(142, 1);
            this.txtJogAccel.Name = "txtJogAccel";
            this.txtJogAccel.Size = new System.Drawing.Size(86, 20);
            this.txtJogAccel.TabIndex = 3;
            this.txtJogAccel.Text = "50000";
            this.txtJogAccel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDec
            // 
            this.lblDec.AutoSize = true;
            this.lblDec.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lblDec.Location = new System.Drawing.Point(239, 4);
            this.lblDec.Name = "lblDec";
            this.lblDec.Size = new System.Drawing.Size(14, 12);
            this.lblDec.TabIndex = 4;
            this.lblDec.Text = "D:";
            // 
            // txtJogDecel
            // 
            this.txtJogDecel.Font = new System.Drawing.Font("Consolas", 8F);
            this.txtJogDecel.Location = new System.Drawing.Point(259, 1);
            this.txtJogDecel.Name = "txtJogDecel";
            this.txtJogDecel.Size = new System.Drawing.Size(81, 20);
            this.txtJogDecel.TabIndex = 5;
            this.txtJogDecel.Text = "50000";
            this.txtJogDecel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panelJog
            // 
            this.panelJog.Controls.Add(this.btnJogPlus);
            this.panelJog.Controls.Add(this.btnJogMinus);
            this.panelJog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelJog.Location = new System.Drawing.Point(481, 87);
            this.panelJog.Name = "panelJog";
            this.panelJog.Size = new System.Drawing.Size(94, 23);
            this.panelJog.TabIndex = 11;
            // 
            // btnJogPlus
            // 
            this.btnJogPlus.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnJogPlus.Location = new System.Drawing.Point(48, 0);
            this.btnJogPlus.Name = "btnJogPlus";
            this.btnJogPlus.Size = new System.Drawing.Size(45, 22);
            this.btnJogPlus.TabIndex = 1;
            this.btnJogPlus.Text = "+";
            this.btnJogPlus.UseVisualStyleBackColor = true;
            this.btnJogPlus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogPlus_MouseDown);
            this.btnJogPlus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJog_MouseUp);
            // 
            // btnJogMinus
            // 
            this.btnJogMinus.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnJogMinus.Location = new System.Drawing.Point(0, 0);
            this.btnJogMinus.Name = "btnJogMinus";
            this.btnJogMinus.Size = new System.Drawing.Size(45, 22);
            this.btnJogMinus.TabIndex = 0;
            this.btnJogMinus.Text = "-";
            this.btnJogMinus.UseVisualStyleBackColor = true;
            this.btnJogMinus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnJogMinus_MouseDown);
            this.btnJogMinus.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnJog_MouseUp);
            // 
            // AxisControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpAxis);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "AxisControlPanel";
            this.Size = new System.Drawing.Size(588, 139);
            this.grpAxis.ResumeLayout(false);
            this.tableLayout.ResumeLayout(false);
            this.tableLayout.PerformLayout();
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelJogParams.ResumeLayout(false);
            this.panelJogParams.PerformLayout();
            this.panelJog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpAxis;
        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.Label lblAxisName;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Button btnServo;
        private System.Windows.Forms.Label lblMovingStatus;
        private System.Windows.Forms.CheckBox chkHomed;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblPositionLabel;
        private System.Windows.Forms.TextBox txtPosition;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label lblTargetLabel;
        private System.Windows.Forms.TextBox txtTargetPosition;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.Label lblJogLabel;
        private System.Windows.Forms.Panel panelJogParams;
        private System.Windows.Forms.Label lblVel;
        private System.Windows.Forms.TextBox txtJogVelocity;
        private System.Windows.Forms.Label lblAcc;
        private System.Windows.Forms.TextBox txtJogAccel;
        private System.Windows.Forms.Label lblDec;
        private System.Windows.Forms.TextBox txtJogDecel;
        private System.Windows.Forms.Panel panelJog;
        private System.Windows.Forms.Button btnJogMinus;
        private System.Windows.Forms.Button btnJogPlus;
        private System.Windows.Forms.Button btnAlarmClear;
        private System.Windows.Forms.Label lblPlusLimit;
        private System.Windows.Forms.Label lblMinusLimit;
    }
}
