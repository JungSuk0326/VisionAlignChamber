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
            this.txtJogDistance = new System.Windows.Forms.TextBox();
            this.panelJog = new System.Windows.Forms.Panel();
            this.btnJogPlus = new System.Windows.Forms.Button();
            this.btnJogMinus = new System.Windows.Forms.Button();
            this.grpAxis.SuspendLayout();
            this.tableLayout.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.panelButtons.SuspendLayout();
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
            this.grpAxis.Size = new System.Drawing.Size(380, 120);
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
            this.tableLayout.Controls.Add(this.txtJogDistance, 1, 3);
            this.tableLayout.Controls.Add(this.panelJog, 3, 3);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(5, 21);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 4;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayout.Size = new System.Drawing.Size(370, 94);
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
            this.panelStatus.Controls.Add(this.lblMovingStatus);
            this.panelStatus.Controls.Add(this.chkHomed);
            this.panelStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatus.Location = new System.Drawing.Point(83, 3);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(184, 22);
            this.panelStatus.TabIndex = 1;
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
            this.chkHomed.Location = new System.Drawing.Point(70, 2);
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
            this.panelButtons.Location = new System.Drawing.Point(273, 3);
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
            this.txtPosition.Size = new System.Drawing.Size(134, 23);
            this.txtPosition.TabIndex = 4;
            this.txtPosition.Text = "0.00";
            this.txtPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblUnit
            // 
            this.lblUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUnit.Location = new System.Drawing.Point(223, 28);
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
            this.txtTargetPosition.Size = new System.Drawing.Size(184, 23);
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
            this.btnMove.Location = new System.Drawing.Point(273, 59);
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
            this.lblJogLabel.Size = new System.Drawing.Size(74, 28);
            this.lblJogLabel.TabIndex = 9;
            this.lblJogLabel.Text = "Jog";
            this.lblJogLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtJogDistance
            // 
            this.tableLayout.SetColumnSpan(this.txtJogDistance, 2);
            this.txtJogDistance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtJogDistance.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtJogDistance.Location = new System.Drawing.Point(83, 87);
            this.txtJogDistance.Name = "txtJogDistance";
            this.txtJogDistance.Size = new System.Drawing.Size(184, 23);
            this.txtJogDistance.TabIndex = 10;
            this.txtJogDistance.Text = "1000";
            this.txtJogDistance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panelJog
            // 
            this.panelJog.Controls.Add(this.btnJogPlus);
            this.panelJog.Controls.Add(this.btnJogMinus);
            this.panelJog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelJog.Location = new System.Drawing.Point(273, 87);
            this.panelJog.Name = "panelJog";
            this.panelJog.Size = new System.Drawing.Size(94, 22);
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
            this.btnJogPlus.Click += new System.EventHandler(this.btnJogPlus_Click);
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
            this.btnJogMinus.Click += new System.EventHandler(this.btnJogMinus_Click);
            // 
            // AxisControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpAxis);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "AxisControlPanel";
            this.Size = new System.Drawing.Size(380, 120);
            this.grpAxis.ResumeLayout(false);
            this.tableLayout.ResumeLayout(false);
            this.tableLayout.PerformLayout();
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.panelJog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpAxis;
        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.Label lblAxisName;
        private System.Windows.Forms.Panel panelStatus;
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
        private System.Windows.Forms.TextBox txtJogDistance;
        private System.Windows.Forms.Panel panelJog;
        private System.Windows.Forms.Button btnJogMinus;
        private System.Windows.Forms.Button btnJogPlus;
    }
}
