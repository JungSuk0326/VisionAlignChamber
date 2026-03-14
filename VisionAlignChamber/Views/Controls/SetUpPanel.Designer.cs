namespace VisionAlignChamber.Views.Controls
{
    partial class SetUpPanel
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpPreCenter = new System.Windows.Forms.GroupBox();
            this.lblPreCenterDesc = new System.Windows.Forms.Label();
            this.lblCenterL_Pos = new System.Windows.Forms.Label();
            this.txtCenterL_Pos = new System.Windows.Forms.TextBox();
            this.lblCenterR_Pos = new System.Windows.Forms.Label();
            this.txtCenterR_Pos = new System.Windows.Forms.TextBox();
            this.lblVelocity = new System.Windows.Forms.Label();
            this.txtVelocity = new System.Windows.Forms.TextBox();
            this.lblAccel = new System.Windows.Forms.Label();
            this.txtAccel = new System.Windows.Forms.TextBox();
            this.lblDecel = new System.Windows.Forms.Label();
            this.txtDecel = new System.Windows.Forms.TextBox();
            this.btnPreCenterExecute = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblPreCenterStatus = new System.Windows.Forms.Label();
            this.grpAlarmTest = new System.Windows.Forms.GroupBox();
            this.lblAlarmTestDesc = new System.Windows.Forms.Label();
            this.cboAlarmList = new System.Windows.Forms.ComboBox();
            this.btnAlarmRaise = new System.Windows.Forms.Button();
            this.btnAlarmClear = new System.Windows.Forms.Button();
            this.lblAlarmTestStatus = new System.Windows.Forms.Label();
            this.grpPreCenter.SuspendLayout();
            this.grpAlarmTest.SuspendLayout();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(56, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SetUp";
            //
            // grpPreCenter
            //
            this.grpPreCenter.Controls.Add(this.lblPreCenterDesc);
            this.grpPreCenter.Controls.Add(this.lblCenterL_Pos);
            this.grpPreCenter.Controls.Add(this.txtCenterL_Pos);
            this.grpPreCenter.Controls.Add(this.lblCenterR_Pos);
            this.grpPreCenter.Controls.Add(this.txtCenterR_Pos);
            this.grpPreCenter.Controls.Add(this.lblVelocity);
            this.grpPreCenter.Controls.Add(this.txtVelocity);
            this.grpPreCenter.Controls.Add(this.lblAccel);
            this.grpPreCenter.Controls.Add(this.txtAccel);
            this.grpPreCenter.Controls.Add(this.lblDecel);
            this.grpPreCenter.Controls.Add(this.txtDecel);
            this.grpPreCenter.Controls.Add(this.btnPreCenterExecute);
            this.grpPreCenter.Controls.Add(this.btnStop);
            this.grpPreCenter.Controls.Add(this.lblPreCenterStatus);
            this.grpPreCenter.ForeColor = System.Drawing.Color.White;
            this.grpPreCenter.Location = new System.Drawing.Point(10, 40);
            this.grpPreCenter.Name = "grpPreCenter";
            this.grpPreCenter.Size = new System.Drawing.Size(400, 260);
            this.grpPreCenter.TabIndex = 1;
            this.grpPreCenter.TabStop = false;
            this.grpPreCenter.Text = "PreCenter Test";
            //
            // lblPreCenterDesc
            //
            this.lblPreCenterDesc.AutoSize = true;
            this.lblPreCenterDesc.ForeColor = System.Drawing.Color.LightGray;
            this.lblPreCenterDesc.Location = new System.Drawing.Point(10, 22);
            this.lblPreCenterDesc.Name = "lblPreCenterDesc";
            this.lblPreCenterDesc.Size = new System.Drawing.Size(300, 15);
            this.lblPreCenterDesc.TabIndex = 0;
            this.lblPreCenterDesc.Text = "Centering Stage L/R → MinCtr 위치 이동 테스트";
            //
            // lblCenterL_Pos
            //
            this.lblCenterL_Pos.AutoSize = true;
            this.lblCenterL_Pos.Location = new System.Drawing.Point(10, 50);
            this.lblCenterL_Pos.Name = "lblCenterL_Pos";
            this.lblCenterL_Pos.Size = new System.Drawing.Size(90, 15);
            this.lblCenterL_Pos.TabIndex = 1;
            this.lblCenterL_Pos.Text = "CenterL Pos";
            //
            // txtCenterL_Pos
            //
            this.txtCenterL_Pos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtCenterL_Pos.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtCenterL_Pos.ForeColor = System.Drawing.Color.White;
            this.txtCenterL_Pos.Location = new System.Drawing.Point(120, 47);
            this.txtCenterL_Pos.Name = "txtCenterL_Pos";
            this.txtCenterL_Pos.Size = new System.Drawing.Size(120, 22);
            this.txtCenterL_Pos.TabIndex = 2;
            //
            // lblCenterR_Pos
            //
            this.lblCenterR_Pos.AutoSize = true;
            this.lblCenterR_Pos.Location = new System.Drawing.Point(10, 80);
            this.lblCenterR_Pos.Name = "lblCenterR_Pos";
            this.lblCenterR_Pos.Size = new System.Drawing.Size(90, 15);
            this.lblCenterR_Pos.TabIndex = 3;
            this.lblCenterR_Pos.Text = "CenterR Pos";
            //
            // txtCenterR_Pos
            //
            this.txtCenterR_Pos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtCenterR_Pos.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtCenterR_Pos.ForeColor = System.Drawing.Color.White;
            this.txtCenterR_Pos.Location = new System.Drawing.Point(120, 77);
            this.txtCenterR_Pos.Name = "txtCenterR_Pos";
            this.txtCenterR_Pos.Size = new System.Drawing.Size(120, 22);
            this.txtCenterR_Pos.TabIndex = 4;
            //
            // lblVelocity
            //
            this.lblVelocity.AutoSize = true;
            this.lblVelocity.Location = new System.Drawing.Point(10, 120);
            this.lblVelocity.Name = "lblVelocity";
            this.lblVelocity.Size = new System.Drawing.Size(90, 15);
            this.lblVelocity.TabIndex = 5;
            this.lblVelocity.Text = "Velocity";
            //
            // txtVelocity
            //
            this.txtVelocity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtVelocity.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtVelocity.ForeColor = System.Drawing.Color.White;
            this.txtVelocity.Location = new System.Drawing.Point(120, 117);
            this.txtVelocity.Name = "txtVelocity";
            this.txtVelocity.Size = new System.Drawing.Size(120, 22);
            this.txtVelocity.TabIndex = 6;
            //
            // lblAccel
            //
            this.lblAccel.AutoSize = true;
            this.lblAccel.Location = new System.Drawing.Point(10, 150);
            this.lblAccel.Name = "lblAccel";
            this.lblAccel.Size = new System.Drawing.Size(90, 15);
            this.lblAccel.TabIndex = 7;
            this.lblAccel.Text = "Accel";
            //
            // txtAccel
            //
            this.txtAccel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtAccel.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtAccel.ForeColor = System.Drawing.Color.White;
            this.txtAccel.Location = new System.Drawing.Point(120, 147);
            this.txtAccel.Name = "txtAccel";
            this.txtAccel.Size = new System.Drawing.Size(120, 22);
            this.txtAccel.TabIndex = 8;
            //
            // lblDecel
            //
            this.lblDecel.AutoSize = true;
            this.lblDecel.Location = new System.Drawing.Point(10, 180);
            this.lblDecel.Name = "lblDecel";
            this.lblDecel.Size = new System.Drawing.Size(90, 15);
            this.lblDecel.TabIndex = 9;
            this.lblDecel.Text = "Decel";
            //
            // txtDecel
            //
            this.txtDecel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.txtDecel.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtDecel.ForeColor = System.Drawing.Color.White;
            this.txtDecel.Location = new System.Drawing.Point(120, 177);
            this.txtDecel.Name = "txtDecel";
            this.txtDecel.Size = new System.Drawing.Size(120, 22);
            this.txtDecel.TabIndex = 10;
            //
            // btnPreCenterExecute
            //
            this.btnPreCenterExecute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnPreCenterExecute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreCenterExecute.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPreCenterExecute.ForeColor = System.Drawing.Color.White;
            this.btnPreCenterExecute.Location = new System.Drawing.Point(10, 215);
            this.btnPreCenterExecute.Name = "btnPreCenterExecute";
            this.btnPreCenterExecute.Size = new System.Drawing.Size(120, 32);
            this.btnPreCenterExecute.TabIndex = 11;
            this.btnPreCenterExecute.Text = "Execute";
            this.btnPreCenterExecute.UseVisualStyleBackColor = false;
            this.btnPreCenterExecute.Click += new System.EventHandler(this.btnPreCenterExecute_Click);
            //
            // btnStop
            //
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(140, 215);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 32);
            this.btnStop.TabIndex = 12;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            //
            // lblPreCenterStatus
            //
            this.lblPreCenterStatus.AutoSize = true;
            this.lblPreCenterStatus.ForeColor = System.Drawing.Color.LightGray;
            this.lblPreCenterStatus.Location = new System.Drawing.Point(250, 223);
            this.lblPreCenterStatus.Name = "lblPreCenterStatus";
            this.lblPreCenterStatus.Size = new System.Drawing.Size(40, 15);
            this.lblPreCenterStatus.TabIndex = 13;
            this.lblPreCenterStatus.Text = "Ready";
            //
            // grpAlarmTest
            //
            this.grpAlarmTest.Controls.Add(this.lblAlarmTestDesc);
            this.grpAlarmTest.Controls.Add(this.cboAlarmList);
            this.grpAlarmTest.Controls.Add(this.btnAlarmRaise);
            this.grpAlarmTest.Controls.Add(this.btnAlarmClear);
            this.grpAlarmTest.Controls.Add(this.lblAlarmTestStatus);
            this.grpAlarmTest.ForeColor = System.Drawing.Color.White;
            this.grpAlarmTest.Location = new System.Drawing.Point(420, 40);
            this.grpAlarmTest.Name = "grpAlarmTest";
            this.grpAlarmTest.Size = new System.Drawing.Size(340, 160);
            this.grpAlarmTest.TabIndex = 2;
            this.grpAlarmTest.TabStop = false;
            this.grpAlarmTest.Text = "Alarm Test";
            //
            // lblAlarmTestDesc
            //
            this.lblAlarmTestDesc.AutoSize = true;
            this.lblAlarmTestDesc.ForeColor = System.Drawing.Color.LightGray;
            this.lblAlarmTestDesc.Location = new System.Drawing.Point(10, 22);
            this.lblAlarmTestDesc.Name = "lblAlarmTestDesc";
            this.lblAlarmTestDesc.Size = new System.Drawing.Size(200, 15);
            this.lblAlarmTestDesc.TabIndex = 0;
            this.lblAlarmTestDesc.Text = "알람 강제 발생/해제 테스트";
            //
            // cboAlarmList
            //
            this.cboAlarmList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.cboAlarmList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAlarmList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboAlarmList.Font = new System.Drawing.Font("Consolas", 9F);
            this.cboAlarmList.ForeColor = System.Drawing.Color.White;
            this.cboAlarmList.Location = new System.Drawing.Point(10, 50);
            this.cboAlarmList.Name = "cboAlarmList";
            this.cboAlarmList.Size = new System.Drawing.Size(320, 22);
            this.cboAlarmList.TabIndex = 1;
            //
            // btnAlarmRaise
            //
            this.btnAlarmRaise.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnAlarmRaise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAlarmRaise.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAlarmRaise.ForeColor = System.Drawing.Color.White;
            this.btnAlarmRaise.Location = new System.Drawing.Point(10, 85);
            this.btnAlarmRaise.Name = "btnAlarmRaise";
            this.btnAlarmRaise.Size = new System.Drawing.Size(100, 32);
            this.btnAlarmRaise.TabIndex = 2;
            this.btnAlarmRaise.Text = "Raise";
            this.btnAlarmRaise.UseVisualStyleBackColor = false;
            this.btnAlarmRaise.Click += new System.EventHandler(this.btnAlarmRaise_Click);
            //
            // btnAlarmClear
            //
            this.btnAlarmClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnAlarmClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAlarmClear.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAlarmClear.ForeColor = System.Drawing.Color.White;
            this.btnAlarmClear.Location = new System.Drawing.Point(120, 85);
            this.btnAlarmClear.Name = "btnAlarmClear";
            this.btnAlarmClear.Size = new System.Drawing.Size(100, 32);
            this.btnAlarmClear.TabIndex = 3;
            this.btnAlarmClear.Text = "Clear";
            this.btnAlarmClear.UseVisualStyleBackColor = false;
            this.btnAlarmClear.Click += new System.EventHandler(this.btnAlarmClear_Click);
            //
            // lblAlarmTestStatus
            //
            this.lblAlarmTestStatus.AutoSize = true;
            this.lblAlarmTestStatus.ForeColor = System.Drawing.Color.LightGray;
            this.lblAlarmTestStatus.Location = new System.Drawing.Point(10, 130);
            this.lblAlarmTestStatus.Name = "lblAlarmTestStatus";
            this.lblAlarmTestStatus.Size = new System.Drawing.Size(40, 15);
            this.lblAlarmTestStatus.TabIndex = 4;
            this.lblAlarmTestStatus.Text = "Ready";
            //
            // SetUpPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.grpAlarmTest);
            this.Controls.Add(this.grpPreCenter);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "SetUpPanel";
            this.Size = new System.Drawing.Size(774, 631);
            this.grpAlarmTest.ResumeLayout(false);
            this.grpAlarmTest.PerformLayout();
            this.grpPreCenter.ResumeLayout(false);
            this.grpPreCenter.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpPreCenter;
        private System.Windows.Forms.Label lblPreCenterDesc;
        private System.Windows.Forms.Label lblCenterL_Pos;
        private System.Windows.Forms.TextBox txtCenterL_Pos;
        private System.Windows.Forms.Label lblCenterR_Pos;
        private System.Windows.Forms.TextBox txtCenterR_Pos;
        private System.Windows.Forms.Label lblVelocity;
        private System.Windows.Forms.TextBox txtVelocity;
        private System.Windows.Forms.Label lblAccel;
        private System.Windows.Forms.TextBox txtAccel;
        private System.Windows.Forms.Label lblDecel;
        private System.Windows.Forms.TextBox txtDecel;
        private System.Windows.Forms.Button btnPreCenterExecute;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblPreCenterStatus;
        private System.Windows.Forms.GroupBox grpAlarmTest;
        private System.Windows.Forms.Label lblAlarmTestDesc;
        private System.Windows.Forms.ComboBox cboAlarmList;
        private System.Windows.Forms.Button btnAlarmRaise;
        private System.Windows.Forms.Button btnAlarmClear;
        private System.Windows.Forms.Label lblAlarmTestStatus;
    }
}
