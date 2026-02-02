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
            this.grpControl = new System.Windows.Forms.GroupBox();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnClearImages = new System.Windows.Forms.Button();
            this.btnLoadImages = new System.Windows.Forms.Button();
            this.btnInitialize = new System.Windows.Forms.Button();
            this.grpResult = new System.Windows.Forms.GroupBox();
            this.listResult = new System.Windows.Forms.ListView();
            this.colNo = new System.Windows.Forms.ColumnHeader();
            this.colIndex1st = new System.Windows.Forms.ColumnHeader();
            this.colIndex2nd = new System.Windows.Forms.ColumnHeader();
            this.colOffAngle = new System.Windows.Forms.ColumnHeader();
            this.colAbsAngle = new System.Windows.Forms.ColumnHeader();
            this.colWidth = new System.Windows.Forms.ColumnHeader();
            this.colHeight = new System.Windows.Forms.ColumnHeader();
            this.colCenterX = new System.Windows.Forms.ColumnHeader();
            this.colCenterY = new System.Windows.Forms.ColumnHeader();
            this.colRadius = new System.Windows.Forms.ColumnHeader();
            this.pnlStatusBar = new System.Windows.Forms.Panel();
            this.lblStatusMessage = new System.Windows.Forms.Label();
            this.grpStatus.SuspendLayout();
            this.grpMode.SuspendLayout();
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
            this.grpStatus.Size = new System.Drawing.Size(350, 70);
            this.grpStatus.TabIndex = 0;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "상태";
            //
            // lblInitLabel
            //
            this.lblInitLabel.AutoSize = true;
            this.lblInitLabel.Location = new System.Drawing.Point(15, 25);
            this.lblInitLabel.Name = "lblInitLabel";
            this.lblInitLabel.Size = new System.Drawing.Size(56, 15);
            this.lblInitLabel.TabIndex = 0;
            this.lblInitLabel.Text = "초기화:";
            //
            // lblInitStatus
            //
            this.lblInitStatus.AutoSize = true;
            this.lblInitStatus.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblInitStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblInitStatus.Location = new System.Drawing.Point(100, 25);
            this.lblInitStatus.Name = "lblInitStatus";
            this.lblInitStatus.Size = new System.Drawing.Size(100, 15);
            this.lblInitStatus.TabIndex = 1;
            this.lblInitStatus.Text = "Not Initialized";
            //
            // lblImageCountLabel
            //
            this.lblImageCountLabel.AutoSize = true;
            this.lblImageCountLabel.Location = new System.Drawing.Point(15, 45);
            this.lblImageCountLabel.Name = "lblImageCountLabel";
            this.lblImageCountLabel.Size = new System.Drawing.Size(70, 15);
            this.lblImageCountLabel.TabIndex = 2;
            this.lblImageCountLabel.Text = "이미지 수:";
            //
            // lblImageCount
            //
            this.lblImageCount.AutoSize = true;
            this.lblImageCount.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold);
            this.lblImageCount.Location = new System.Drawing.Point(100, 45);
            this.lblImageCount.Name = "lblImageCount";
            this.lblImageCount.Size = new System.Drawing.Size(15, 15);
            this.lblImageCount.TabIndex = 3;
            this.lblImageCount.Text = "0";
            //
            // grpMode
            //
            this.grpMode.Controls.Add(this.rdoFlat);
            this.grpMode.Controls.Add(this.rdoNotch);
            this.grpMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpMode.Location = new System.Drawing.Point(0, 70);
            this.grpMode.Name = "grpMode";
            this.grpMode.Size = new System.Drawing.Size(350, 55);
            this.grpMode.TabIndex = 1;
            this.grpMode.TabStop = false;
            this.grpMode.Text = "검사 모드";
            //
            // rdoNotch
            //
            this.rdoNotch.AutoSize = true;
            this.rdoNotch.Checked = true;
            this.rdoNotch.Location = new System.Drawing.Point(20, 25);
            this.rdoNotch.Name = "rdoNotch";
            this.rdoNotch.Size = new System.Drawing.Size(64, 19);
            this.rdoNotch.TabIndex = 0;
            this.rdoNotch.TabStop = true;
            this.rdoNotch.Text = "Notch";
            this.rdoNotch.UseVisualStyleBackColor = true;
            this.rdoNotch.CheckedChanged += new System.EventHandler(this.rdoNotch_CheckedChanged);
            //
            // rdoFlat
            //
            this.rdoFlat.AutoSize = true;
            this.rdoFlat.Location = new System.Drawing.Point(120, 25);
            this.rdoFlat.Name = "rdoFlat";
            this.rdoFlat.Size = new System.Drawing.Size(48, 19);
            this.rdoFlat.TabIndex = 1;
            this.rdoFlat.Text = "Flat";
            this.rdoFlat.UseVisualStyleBackColor = true;
            this.rdoFlat.CheckedChanged += new System.EventHandler(this.rdoFlat_CheckedChanged);
            //
            // grpControl
            //
            this.grpControl.Controls.Add(this.btnExecute);
            this.grpControl.Controls.Add(this.btnClearImages);
            this.grpControl.Controls.Add(this.btnLoadImages);
            this.grpControl.Controls.Add(this.btnInitialize);
            this.grpControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpControl.Location = new System.Drawing.Point(0, 125);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(350, 110);
            this.grpControl.TabIndex = 2;
            this.grpControl.TabStop = false;
            this.grpControl.Text = "제어";
            //
            // btnInitialize
            //
            this.btnInitialize.Location = new System.Drawing.Point(15, 25);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Size = new System.Drawing.Size(150, 30);
            this.btnInitialize.TabIndex = 0;
            this.btnInitialize.Text = "초기화";
            this.btnInitialize.UseVisualStyleBackColor = true;
            this.btnInitialize.Click += new System.EventHandler(this.btnInitialize_Click);
            //
            // btnLoadImages
            //
            this.btnLoadImages.Location = new System.Drawing.Point(180, 25);
            this.btnLoadImages.Name = "btnLoadImages";
            this.btnLoadImages.Size = new System.Drawing.Size(150, 30);
            this.btnLoadImages.TabIndex = 1;
            this.btnLoadImages.Text = "이미지 로드";
            this.btnLoadImages.UseVisualStyleBackColor = true;
            this.btnLoadImages.Click += new System.EventHandler(this.btnLoadImages_Click);
            //
            // btnClearImages
            //
            this.btnClearImages.Location = new System.Drawing.Point(15, 65);
            this.btnClearImages.Name = "btnClearImages";
            this.btnClearImages.Size = new System.Drawing.Size(150, 30);
            this.btnClearImages.TabIndex = 2;
            this.btnClearImages.Text = "이미지 클리어";
            this.btnClearImages.UseVisualStyleBackColor = true;
            this.btnClearImages.Click += new System.EventHandler(this.btnClearImages_Click);
            //
            // btnExecute
            //
            this.btnExecute.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnExecute.ForeColor = System.Drawing.Color.White;
            this.btnExecute.Location = new System.Drawing.Point(180, 65);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(150, 30);
            this.btnExecute.TabIndex = 3;
            this.btnExecute.Text = "검사 실행";
            this.btnExecute.UseVisualStyleBackColor = false;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            //
            // grpResult
            //
            this.grpResult.Controls.Add(this.listResult);
            this.grpResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpResult.Location = new System.Drawing.Point(0, 235);
            this.grpResult.Name = "grpResult";
            this.grpResult.Size = new System.Drawing.Size(350, 225);
            this.grpResult.TabIndex = 3;
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
            this.listResult.Location = new System.Drawing.Point(3, 19);
            this.listResult.Name = "listResult";
            this.listResult.Size = new System.Drawing.Size(344, 203);
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
            this.pnlStatusBar.Location = new System.Drawing.Point(0, 460);
            this.pnlStatusBar.Name = "pnlStatusBar";
            this.pnlStatusBar.Size = new System.Drawing.Size(350, 30);
            this.pnlStatusBar.TabIndex = 4;
            //
            // lblStatusMessage
            //
            this.lblStatusMessage.AutoSize = true;
            this.lblStatusMessage.ForeColor = System.Drawing.Color.White;
            this.lblStatusMessage.Location = new System.Drawing.Point(10, 8);
            this.lblStatusMessage.Name = "lblStatusMessage";
            this.lblStatusMessage.Size = new System.Drawing.Size(40, 15);
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
            this.Controls.Add(this.grpMode);
            this.Controls.Add(this.grpStatus);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.Name = "VisionPanel";
            this.Size = new System.Drawing.Size(350, 490);
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.grpMode.ResumeLayout(false);
            this.grpMode.PerformLayout();
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
