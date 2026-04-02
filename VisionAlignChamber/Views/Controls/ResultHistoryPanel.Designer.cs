namespace VisionAlignChamber.Views.Controls
{
    partial class ResultHistoryPanel
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
            this.btnExport = new System.Windows.Forms.Button();
            this.lblWaferType = new System.Windows.Forms.Label();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbNotch = new System.Windows.Forms.RadioButton();
            this.rbFlat = new System.Windows.Forms.RadioButton();
            this.chkValidOnly = new System.Windows.Forms.CheckBox();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.colNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMeasuredTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWaferType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAbsAngle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalOffset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCenterX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCenterY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRadius = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEddyValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpStatistics = new System.Windows.Forms.GroupBox();
            this.tableLayoutStats = new System.Windows.Forms.TableLayoutPanel();
            this.lblTotalLabel = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblValidLabel = new System.Windows.Forms.Label();
            this.lblValidValue = new System.Windows.Forms.Label();
            this.lblRateLabel = new System.Windows.Forms.Label();
            this.lblRateValue = new System.Windows.Forms.Label();
            this.lblAvgOffsetLabel = new System.Windows.Forms.Label();
            this.lblAvgOffsetValue = new System.Windows.Forms.Label();
            this.grpFilter.SuspendLayout();
            this.tableLayoutFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.grpStatistics.SuspendLayout();
            this.tableLayoutStats.SuspendLayout();
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
            this.tableLayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 12F));
            this.tableLayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
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
            this.tableLayoutFilter.Controls.Add(this.lblWaferType, 0, 1);
            this.tableLayoutFilter.Controls.Add(this.rbAll, 1, 1);
            this.tableLayoutFilter.Controls.Add(this.rbNotch, 2, 1);
            this.tableLayoutFilter.Controls.Add(this.rbFlat, 4, 1);
            this.tableLayoutFilter.Controls.Add(this.chkValidOnly, 6, 1);
            this.tableLayoutFilter.Controls.Add(this.btnExport, 11, 1);
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
            this.btnAll.ForeColor = System.Drawing.Color.Black;
            this.btnAll.Location = new System.Drawing.Point(523, 3);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(44, 23);
            this.btnAll.TabIndex = 8;
            this.btnAll.Text = "전체";
            this.btnAll.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnExport.ForeColor = System.Drawing.Color.Black;
            this.btnExport.Location = new System.Drawing.Point(633, 33);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(62, 23);
            this.btnExport.TabIndex = 14;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // lblWaferType
            // 
            this.lblWaferType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblWaferType.AutoSize = true;
            this.lblWaferType.ForeColor = System.Drawing.Color.White;
            this.lblWaferType.Location = new System.Drawing.Point(3, 39);
            this.lblWaferType.Name = "lblWaferType";
            this.lblWaferType.Size = new System.Drawing.Size(38, 12);
            this.lblWaferType.TabIndex = 9;
            this.lblWaferType.Text = "Type:";
            // 
            // rbAll
            // 
            this.rbAll.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.ForeColor = System.Drawing.Color.White;
            this.rbAll.Location = new System.Drawing.Point(53, 37);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(47, 16);
            this.rbAll.TabIndex = 10;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "전체";
            this.rbAll.UseVisualStyleBackColor = true;
            // 
            // rbNotch
            // 
            this.rbNotch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbNotch.AutoSize = true;
            this.tableLayoutFilter.SetColumnSpan(this.rbNotch, 2);
            this.rbNotch.ForeColor = System.Drawing.Color.White;
            this.rbNotch.Location = new System.Drawing.Point(173, 37);
            this.rbNotch.Name = "rbNotch";
            this.rbNotch.Size = new System.Drawing.Size(56, 16);
            this.rbNotch.TabIndex = 11;
            this.rbNotch.Text = "Notch";
            this.rbNotch.UseVisualStyleBackColor = true;
            // 
            // rbFlat
            // 
            this.rbFlat.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbFlat.AutoSize = true;
            this.rbFlat.ForeColor = System.Drawing.Color.White;
            this.rbFlat.Location = new System.Drawing.Point(313, 37);
            this.rbFlat.Name = "rbFlat";
            this.rbFlat.Size = new System.Drawing.Size(43, 16);
            this.rbFlat.TabIndex = 12;
            this.rbFlat.Text = "Flat";
            this.rbFlat.UseVisualStyleBackColor = true;
            // 
            // chkValidOnly
            // 
            this.chkValidOnly.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkValidOnly.AutoSize = true;
            this.tableLayoutFilter.SetColumnSpan(this.chkValidOnly, 3);
            this.chkValidOnly.ForeColor = System.Drawing.Color.White;
            this.chkValidOnly.Location = new System.Drawing.Point(423, 37);
            this.chkValidOnly.Name = "chkValidOnly";
            this.chkValidOnly.Size = new System.Drawing.Size(88, 16);
            this.chkValidOnly.TabIndex = 13;
            this.chkValidOnly.Text = "유효 결과만";
            this.chkValidOnly.UseVisualStyleBackColor = true;
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AllowUserToResizeRows = false;
            this.dgvResults.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.dgvResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNo,
            this.colMeasuredTime,
            this.colWaferType,
            this.colValid,
            this.colAbsAngle,
            this.colTotalOffset,
            this.colCenterX,
            this.colCenterY,
            this.colRadius,
            this.colWidth,
            this.colHeight,
            this.colEddyValue,
            this.colPN});
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dgvResults.Location = new System.Drawing.Point(0, 90);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.RowTemplate.Height = 23;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.Size = new System.Drawing.Size(780, 470);
            this.dgvResults.TabIndex = 1;
            // 
            // colNo
            // 
            this.colNo.HeaderText = "No";
            this.colNo.Name = "colNo";
            this.colNo.ReadOnly = true;
            this.colNo.Width = 50;
            // 
            // colMeasuredTime
            // 
            this.colMeasuredTime.HeaderText = "측정시간";
            this.colMeasuredTime.Name = "colMeasuredTime";
            this.colMeasuredTime.ReadOnly = true;
            this.colMeasuredTime.Width = 140;
            // 
            // colWaferType
            // 
            this.colWaferType.HeaderText = "Type";
            this.colWaferType.Name = "colWaferType";
            this.colWaferType.ReadOnly = true;
            this.colWaferType.Width = 60;
            // 
            // colValid
            // 
            this.colValid.HeaderText = "Valid";
            this.colValid.Name = "colValid";
            this.colValid.ReadOnly = true;
            this.colValid.Width = 50;
            // 
            // colAbsAngle
            // 
            this.colAbsAngle.HeaderText = "Angle (°)";
            this.colAbsAngle.Name = "colAbsAngle";
            this.colAbsAngle.ReadOnly = true;
            this.colAbsAngle.Width = 80;
            // 
            // colTotalOffset
            // 
            this.colTotalOffset.HeaderText = "Offset (mm)";
            this.colTotalOffset.Name = "colTotalOffset";
            this.colTotalOffset.ReadOnly = true;
            this.colTotalOffset.Width = 80;
            // 
            // colCenterX
            // 
            this.colCenterX.HeaderText = "Center X";
            this.colCenterX.Name = "colCenterX";
            this.colCenterX.ReadOnly = true;
            this.colCenterX.Width = 80;
            // 
            // colCenterY
            // 
            this.colCenterY.HeaderText = "Center Y";
            this.colCenterY.Name = "colCenterY";
            this.colCenterY.ReadOnly = true;
            this.colCenterY.Width = 80;
            //
            // colRadius
            //
            this.colRadius.HeaderText = "Radius";
            this.colRadius.Name = "colRadius";
            this.colRadius.ReadOnly = true;
            this.colRadius.Width = 70;
            //
            // colWidth
            //
            this.colWidth.HeaderText = "Width";
            this.colWidth.Name = "colWidth";
            this.colWidth.ReadOnly = true;
            this.colWidth.Width = 70;
            //
            // colHeight
            //
            this.colHeight.HeaderText = "Height";
            this.colHeight.Name = "colHeight";
            this.colHeight.ReadOnly = true;
            this.colHeight.Width = 70;
            //
            // colEddyValue
            //
            this.colEddyValue.HeaderText = "Eddy";
            this.colEddyValue.Name = "colEddyValue";
            this.colEddyValue.ReadOnly = true;
            this.colEddyValue.Width = 70;
            // 
            // colPN
            // 
            this.colPN.HeaderText = "P/N";
            this.colPN.Name = "colPN";
            this.colPN.ReadOnly = true;
            this.colPN.Width = 50;
            // 
            // grpStatistics
            // 
            this.grpStatistics.Controls.Add(this.tableLayoutStats);
            this.grpStatistics.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpStatistics.ForeColor = System.Drawing.Color.White;
            this.grpStatistics.Location = new System.Drawing.Point(0, 560);
            this.grpStatistics.Name = "grpStatistics";
            this.grpStatistics.Size = new System.Drawing.Size(780, 50);
            this.grpStatistics.TabIndex = 2;
            this.grpStatistics.TabStop = false;
            this.grpStatistics.Text = "통계";
            // 
            // tableLayoutStats
            // 
            this.tableLayoutStats.ColumnCount = 8;
            this.tableLayoutStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutStats.Controls.Add(this.lblTotalLabel, 0, 0);
            this.tableLayoutStats.Controls.Add(this.lblTotalValue, 1, 0);
            this.tableLayoutStats.Controls.Add(this.lblValidLabel, 2, 0);
            this.tableLayoutStats.Controls.Add(this.lblValidValue, 3, 0);
            this.tableLayoutStats.Controls.Add(this.lblRateLabel, 4, 0);
            this.tableLayoutStats.Controls.Add(this.lblRateValue, 5, 0);
            this.tableLayoutStats.Controls.Add(this.lblAvgOffsetLabel, 6, 0);
            this.tableLayoutStats.Controls.Add(this.lblAvgOffsetValue, 7, 0);
            this.tableLayoutStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutStats.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutStats.Name = "tableLayoutStats";
            this.tableLayoutStats.RowCount = 1;
            this.tableLayoutStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutStats.Size = new System.Drawing.Size(774, 30);
            this.tableLayoutStats.TabIndex = 0;
            // 
            // lblTotalLabel
            // 
            this.lblTotalLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTotalLabel.AutoSize = true;
            this.lblTotalLabel.ForeColor = System.Drawing.Color.Silver;
            this.lblTotalLabel.Location = new System.Drawing.Point(3, 9);
            this.lblTotalLabel.Name = "lblTotalLabel";
            this.lblTotalLabel.Size = new System.Drawing.Size(33, 12);
            this.lblTotalLabel.TabIndex = 0;
            this.lblTotalLabel.Text = "총계:";
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalValue.ForeColor = System.Drawing.Color.Cyan;
            this.lblTotalValue.Location = new System.Drawing.Point(63, 6);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(16, 17);
            this.lblTotalValue.TabIndex = 1;
            this.lblTotalValue.Text = "0";
            // 
            // lblValidLabel
            // 
            this.lblValidLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblValidLabel.AutoSize = true;
            this.lblValidLabel.ForeColor = System.Drawing.Color.Silver;
            this.lblValidLabel.Location = new System.Drawing.Point(143, 9);
            this.lblValidLabel.Name = "lblValidLabel";
            this.lblValidLabel.Size = new System.Drawing.Size(33, 12);
            this.lblValidLabel.TabIndex = 2;
            this.lblValidLabel.Text = "유효:";
            // 
            // lblValidValue
            // 
            this.lblValidValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblValidValue.AutoSize = true;
            this.lblValidValue.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.lblValidValue.ForeColor = System.Drawing.Color.LightGreen;
            this.lblValidValue.Location = new System.Drawing.Point(203, 6);
            this.lblValidValue.Name = "lblValidValue";
            this.lblValidValue.Size = new System.Drawing.Size(16, 17);
            this.lblValidValue.TabIndex = 3;
            this.lblValidValue.Text = "0";
            // 
            // lblRateLabel
            // 
            this.lblRateLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRateLabel.AutoSize = true;
            this.lblRateLabel.ForeColor = System.Drawing.Color.Silver;
            this.lblRateLabel.Location = new System.Drawing.Point(283, 9);
            this.lblRateLabel.Name = "lblRateLabel";
            this.lblRateLabel.Size = new System.Drawing.Size(45, 12);
            this.lblRateLabel.TabIndex = 4;
            this.lblRateLabel.Text = "유효율:";
            // 
            // lblRateValue
            // 
            this.lblRateValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRateValue.AutoSize = true;
            this.lblRateValue.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.lblRateValue.ForeColor = System.Drawing.Color.Yellow;
            this.lblRateValue.Location = new System.Drawing.Point(343, 6);
            this.lblRateValue.Name = "lblRateValue";
            this.lblRateValue.Size = new System.Drawing.Size(32, 17);
            this.lblRateValue.TabIndex = 5;
            this.lblRateValue.Text = "0 %";
            // 
            // lblAvgOffsetLabel
            // 
            this.lblAvgOffsetLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAvgOffsetLabel.AutoSize = true;
            this.lblAvgOffsetLabel.ForeColor = System.Drawing.Color.Silver;
            this.lblAvgOffsetLabel.Location = new System.Drawing.Point(423, 9);
            this.lblAvgOffsetLabel.Name = "lblAvgOffsetLabel";
            this.lblAvgOffsetLabel.Size = new System.Drawing.Size(65, 12);
            this.lblAvgOffsetLabel.TabIndex = 6;
            this.lblAvgOffsetLabel.Text = "평균Offset:";
            // 
            // lblAvgOffsetValue
            // 
            this.lblAvgOffsetValue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAvgOffsetValue.AutoSize = true;
            this.lblAvgOffsetValue.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.lblAvgOffsetValue.ForeColor = System.Drawing.Color.Orange;
            this.lblAvgOffsetValue.Location = new System.Drawing.Point(503, 6);
            this.lblAvgOffsetValue.Name = "lblAvgOffsetValue";
            this.lblAvgOffsetValue.Size = new System.Drawing.Size(72, 17);
            this.lblAvgOffsetValue.TabIndex = 7;
            this.lblAvgOffsetValue.Text = "0.000 mm";
            // 
            // ResultHistoryPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.grpStatistics);
            this.Controls.Add(this.grpFilter);
            this.Name = "ResultHistoryPanel";
            this.Size = new System.Drawing.Size(780, 610);
            this.grpFilter.ResumeLayout(false);
            this.tableLayoutFilter.ResumeLayout(false);
            this.tableLayoutFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.grpStatistics.ResumeLayout(false);
            this.tableLayoutStats.ResumeLayout(false);
            this.tableLayoutStats.PerformLayout();
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
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblWaferType;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbNotch;
        private System.Windows.Forms.RadioButton rbFlat;
        private System.Windows.Forms.CheckBox chkValidOnly;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMeasuredTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWaferType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAbsAngle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalOffset;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCenterX;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCenterY;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRadius;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWidth;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEddyValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPN;
        private System.Windows.Forms.GroupBox grpStatistics;
        private System.Windows.Forms.TableLayoutPanel tableLayoutStats;
        private System.Windows.Forms.Label lblTotalLabel;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Label lblValidLabel;
        private System.Windows.Forms.Label lblValidValue;
        private System.Windows.Forms.Label lblRateLabel;
        private System.Windows.Forms.Label lblRateValue;
        private System.Windows.Forms.Label lblAvgOffsetLabel;
        private System.Windows.Forms.Label lblAvgOffsetValue;
    }
}
