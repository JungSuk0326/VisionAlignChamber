namespace VisionAlignChamber.Views.Controls
{
    partial class AlarmHistoryPanel
    {
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.tableLayoutFilter = new System.Windows.Forms.TableLayoutPanel();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.btnWeek = new System.Windows.Forms.Button();
            this.btnMonth = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.lblSeverity = new System.Windows.Forms.Label();
            this.cboSeverity = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.dgvAlarms = new System.Windows.Forms.DataGridView();
            this.colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSeverity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOccurredTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colClearedTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnClearSelected = new System.Windows.Forms.Button();
            this.lblActiveValue = new System.Windows.Forms.Label();
            this.lblActiveLabel = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblTotalLabel = new System.Windows.Forms.Label();
            this.grpFilter.SuspendLayout();
            this.tableLayoutFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlarms)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpFilter
            // 
            this.grpFilter.Controls.Add(this.tableLayoutFilter);
            this.grpFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFilter.ForeColor = System.Drawing.Color.White;
            this.grpFilter.Location = new System.Drawing.Point(0, 0);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Padding = new System.Windows.Forms.Padding(8);
            this.grpFilter.Size = new System.Drawing.Size(780, 90);
            this.grpFilter.TabIndex = 0;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "필터";
            // 
            // tableLayoutFilter
            // 
            this.tableLayoutFilter.ColumnCount = 14;
            this.tableLayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutFilter.Controls.Add(this.lblStartDate, 0, 0);
            this.tableLayoutFilter.Controls.Add(this.dtpStartDate, 1, 0);
            this.tableLayoutFilter.Controls.Add(this.lblEndDate, 2, 0);
            this.tableLayoutFilter.Controls.Add(this.dtpEndDate, 3, 0);
            this.tableLayoutFilter.Controls.Add(this.btnSearch, 4, 0);
            this.tableLayoutFilter.Controls.Add(this.btnToday, 5, 0);
            this.tableLayoutFilter.Controls.Add(this.btnWeek, 6, 0);
            this.tableLayoutFilter.Controls.Add(this.btnMonth, 7, 0);
            this.tableLayoutFilter.Controls.Add(this.btnAll, 8, 0);
            this.tableLayoutFilter.Controls.Add(this.lblSeverity, 0, 1);
            this.tableLayoutFilter.Controls.Add(this.cboSeverity, 1, 1);
            this.tableLayoutFilter.Controls.Add(this.lblCategory, 2, 1);
            this.tableLayoutFilter.Controls.Add(this.cboCategory, 3, 1);
            this.tableLayoutFilter.Controls.Add(this.lblStatus, 10, 1);
            this.tableLayoutFilter.Controls.Add(this.cboStatus, 11, 1);
            this.tableLayoutFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutFilter.Location = new System.Drawing.Point(8, 22);
            this.tableLayoutFilter.Name = "tableLayoutFilter";
            this.tableLayoutFilter.RowCount = 2;
            this.tableLayoutFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutFilter.Size = new System.Drawing.Size(764, 60);
            this.tableLayoutFilter.TabIndex = 0;
            // 
            // lblStartDate
            // 
            this.lblStartDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.ForeColor = System.Drawing.Color.White;
            this.lblStartDate.Location = new System.Drawing.Point(3, 9);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(33, 12);
            this.lblStartDate.TabIndex = 0;
            this.lblStartDate.Text = "시작:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(53, 4);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(110, 21);
            this.dtpStartDate.TabIndex = 1;
            // 
            // lblEndDate
            // 
            this.lblEndDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.ForeColor = System.Drawing.Color.White;
            this.lblEndDate.Location = new System.Drawing.Point(173, 9);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(14, 12);
            this.lblEndDate.TabIndex = 2;
            this.lblEndDate.Text = "~";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(193, 4);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(110, 21);
            this.dtpEndDate.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSearch.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSearch.ForeColor = System.Drawing.Color.Black;
            this.btnSearch.Location = new System.Drawing.Point(313, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(54, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "조회";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnToday
            // 
            this.btnToday.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnToday.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnToday.ForeColor = System.Drawing.Color.Black;
            this.btnToday.Location = new System.Drawing.Point(373, 3);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(44, 23);
            this.btnToday.TabIndex = 5;
            this.btnToday.Text = "오늘";
            this.btnToday.UseVisualStyleBackColor = true;
            // 
            // btnWeek
            // 
            this.btnWeek.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnWeek.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnWeek.ForeColor = System.Drawing.Color.Black;
            this.btnWeek.Location = new System.Drawing.Point(423, 3);
            this.btnWeek.Name = "btnWeek";
            this.btnWeek.Size = new System.Drawing.Size(44, 23);
            this.btnWeek.TabIndex = 6;
            this.btnWeek.Text = "7일";
            this.btnWeek.UseVisualStyleBackColor = true;
            // 
            // btnMonth
            // 
            this.btnMonth.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnMonth.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMonth.ForeColor = System.Drawing.Color.Black;
            this.btnMonth.Location = new System.Drawing.Point(473, 3);
            this.btnMonth.Name = "btnMonth";
            this.btnMonth.Size = new System.Drawing.Size(44, 23);
            this.btnMonth.TabIndex = 7;
            this.btnMonth.Text = "30일";
            this.btnMonth.UseVisualStyleBackColor = true;
            // 
            // btnAll
            // 
            this.btnAll.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAll.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAll.ForeColor = System.Drawing.Color.Black;
            this.btnAll.Location = new System.Drawing.Point(523, 3);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(44, 23);
            this.btnAll.TabIndex = 8;
            this.btnAll.Text = "전체";
            this.btnAll.UseVisualStyleBackColor = true;
            // 
            // lblSeverity
            // 
            this.lblSeverity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSeverity.AutoSize = true;
            this.lblSeverity.ForeColor = System.Drawing.Color.White;
            this.lblSeverity.Location = new System.Drawing.Point(3, 33);
            this.lblSeverity.Name = "lblSeverity";
            this.lblSeverity.Size = new System.Drawing.Size(29, 24);
            this.lblSeverity.TabIndex = 9;
            this.lblSeverity.Text = "심각도:";
            // 
            // cboSeverity
            // 
            this.cboSeverity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboSeverity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSeverity.Location = new System.Drawing.Point(53, 35);
            this.cboSeverity.Name = "cboSeverity";
            this.cboSeverity.Size = new System.Drawing.Size(110, 20);
            this.cboSeverity.TabIndex = 10;
            // 
            // lblCategory
            // 
            this.lblCategory.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCategory.AutoSize = true;
            this.lblCategory.ForeColor = System.Drawing.Color.White;
            this.lblCategory.Location = new System.Drawing.Point(173, 30);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(14, 30);
            this.lblCategory.TabIndex = 11;
            this.lblCategory.Text = "분류:";
            // 
            // cboCategory
            // 
            this.cboCategory.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tableLayoutFilter.SetColumnSpan(this.cboCategory, 5);
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.Location = new System.Drawing.Point(193, 35);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(110, 20);
            this.cboCategory.TabIndex = 12;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(583, 39);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(33, 12);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "상태:";
            // 
            // cboStatus
            // 
            this.cboStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Location = new System.Drawing.Point(638, 35);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(79, 20);
            this.cboStatus.TabIndex = 14;
            // 
            // dgvAlarms
            // 
            this.dgvAlarms.AllowUserToAddRows = false;
            this.dgvAlarms.AllowUserToDeleteRows = false;
            this.dgvAlarms.AllowUserToResizeRows = false;
            this.dgvAlarms.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.dgvAlarms.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAlarms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlarms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNo,
            this.colCode,
            this.colName,
            this.colSeverity,
            this.colCategory,
            this.colOccurredTime,
            this.colClearedTime,
            this.colStatus,
            this.colSource});
            this.dgvAlarms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAlarms.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dgvAlarms.Location = new System.Drawing.Point(0, 90);
            this.dgvAlarms.Name = "dgvAlarms";
            this.dgvAlarms.ReadOnly = true;
            this.dgvAlarms.RowHeadersVisible = false;
            this.dgvAlarms.RowTemplate.Height = 23;
            this.dgvAlarms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlarms.Size = new System.Drawing.Size(780, 470);
            this.dgvAlarms.TabIndex = 1;
            // 
            // colNo
            // 
            this.colNo.HeaderText = "No";
            this.colNo.Name = "colNo";
            this.colNo.ReadOnly = true;
            this.colNo.Width = 40;
            // 
            // colCode
            // 
            this.colCode.HeaderText = "코드";
            this.colCode.Name = "colCode";
            this.colCode.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.HeaderText = "알람명";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 140;
            // 
            // colSeverity
            // 
            this.colSeverity.HeaderText = "심각도";
            this.colSeverity.Name = "colSeverity";
            this.colSeverity.ReadOnly = true;
            this.colSeverity.Width = 65;
            // 
            // colCategory
            // 
            this.colCategory.HeaderText = "분류";
            this.colCategory.Name = "colCategory";
            this.colCategory.ReadOnly = true;
            this.colCategory.Width = 65;
            // 
            // colOccurredTime
            // 
            this.colOccurredTime.HeaderText = "발생시간";
            this.colOccurredTime.Name = "colOccurredTime";
            this.colOccurredTime.ReadOnly = true;
            this.colOccurredTime.Width = 130;
            // 
            // colClearedTime
            // 
            this.colClearedTime.HeaderText = "해제시간";
            this.colClearedTime.Name = "colClearedTime";
            this.colClearedTime.ReadOnly = true;
            this.colClearedTime.Width = 130;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "상태";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 55;
            // 
            // colSource
            // 
            this.colSource.HeaderText = "소스";
            this.colSource.Name = "colSource";
            this.colSource.ReadOnly = true;
            this.colSource.Width = 50;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnClearAll);
            this.pnlBottom.Controls.Add(this.btnClearSelected);
            this.pnlBottom.Controls.Add(this.lblActiveValue);
            this.pnlBottom.Controls.Add(this.lblActiveLabel);
            this.pnlBottom.Controls.Add(this.lblTotalValue);
            this.pnlBottom.Controls.Add(this.lblTotalLabel);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 560);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(780, 40);
            this.pnlBottom.TabIndex = 2;
            // 
            // btnClearAll
            // 
            this.btnClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearAll.ForeColor = System.Drawing.Color.Black;
            this.btnClearAll.Location = new System.Drawing.Point(680, 8);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(90, 25);
            this.btnClearAll.TabIndex = 5;
            this.btnClearAll.Text = "전체 Clear";
            this.btnClearAll.UseVisualStyleBackColor = true;
            // 
            // btnClearSelected
            // 
            this.btnClearSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearSelected.ForeColor = System.Drawing.Color.Black;
            this.btnClearSelected.Location = new System.Drawing.Point(580, 8);
            this.btnClearSelected.Name = "btnClearSelected";
            this.btnClearSelected.Size = new System.Drawing.Size(90, 25);
            this.btnClearSelected.TabIndex = 4;
            this.btnClearSelected.Text = "선택 Clear";
            this.btnClearSelected.UseVisualStyleBackColor = true;
            // 
            // lblActiveValue
            // 
            this.lblActiveValue.AutoSize = true;
            this.lblActiveValue.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.lblActiveValue.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblActiveValue.Location = new System.Drawing.Point(146, 12);
            this.lblActiveValue.Name = "lblActiveValue";
            this.lblActiveValue.Size = new System.Drawing.Size(16, 17);
            this.lblActiveValue.TabIndex = 3;
            this.lblActiveValue.Text = "0";
            // 
            // lblActiveLabel
            // 
            this.lblActiveLabel.AutoSize = true;
            this.lblActiveLabel.ForeColor = System.Drawing.Color.Silver;
            this.lblActiveLabel.Location = new System.Drawing.Point(100, 14);
            this.lblActiveLabel.Name = "lblActiveLabel";
            this.lblActiveLabel.Size = new System.Drawing.Size(43, 12);
            this.lblActiveLabel.TabIndex = 2;
            this.lblActiveLabel.Text = "Active:";
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalValue.ForeColor = System.Drawing.Color.Cyan;
            this.lblTotalValue.Location = new System.Drawing.Point(48, 12);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(16, 17);
            this.lblTotalValue.TabIndex = 1;
            this.lblTotalValue.Text = "0";
            // 
            // lblTotalLabel
            // 
            this.lblTotalLabel.AutoSize = true;
            this.lblTotalLabel.ForeColor = System.Drawing.Color.Silver;
            this.lblTotalLabel.Location = new System.Drawing.Point(10, 14);
            this.lblTotalLabel.Name = "lblTotalLabel";
            this.lblTotalLabel.Size = new System.Drawing.Size(37, 12);
            this.lblTotalLabel.TabIndex = 0;
            this.lblTotalLabel.Text = "Total:";
            // 
            // AlarmHistoryPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.dgvAlarms);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.grpFilter);
            this.Name = "AlarmHistoryPanel";
            this.Size = new System.Drawing.Size(780, 600);
            this.grpFilter.ResumeLayout(false);
            this.tableLayoutFilter.ResumeLayout(false);
            this.tableLayoutFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlarms)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutFilter;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Button btnWeek;
        private System.Windows.Forms.Button btnMonth;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Label lblSeverity;
        private System.Windows.Forms.ComboBox cboSeverity;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.DataGridView dgvAlarms;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSeverity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOccurredTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colClearedTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSource;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblTotalLabel;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Label lblActiveLabel;
        private System.Windows.Forms.Label lblActiveValue;
        private System.Windows.Forms.Button btnClearSelected;
        private System.Windows.Forms.Button btnClearAll;
    }
}
