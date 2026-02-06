namespace VisionAlignChamber.Views.Controls
{
    partial class LogPanel
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.grpLevel = new System.Windows.Forms.GroupBox();
            this.chkFatal = new System.Windows.Forms.CheckBox();
            this.chkError = new System.Windows.Forms.CheckBox();
            this.chkWarn = new System.Windows.Forms.CheckBox();
            this.chkInfo = new System.Windows.Forms.CheckBox();
            this.chkDebug = new System.Windows.Forms.CheckBox();
            this.grpCategory = new System.Windows.Forms.GroupBox();
            this.btnDeselectAll = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.chkOther = new System.Windows.Forms.CheckBox();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.chkDB = new System.Windows.Forms.CheckBox();
            this.chkSequence = new System.Windows.Forms.CheckBox();
            this.chkAlarm = new System.Windows.Forms.CheckBox();
            this.chkVision = new System.Windows.Forms.CheckBox();
            this.chkIO = new System.Windows.Forms.CheckBox();
            this.chkMotion = new System.Windows.Forms.CheckBox();
            this.chkSystem = new System.Windows.Forms.CheckBox();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.chkAutoScroll = new System.Windows.Forms.CheckBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.listViewLogs = new System.Windows.Forms.ListView();
            this.colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colLogger = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelBottom = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.grpLevel.SuspendLayout();
            this.grpCategory.SuspendLayout();
            this.grpOptions.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            //
            // panelTop
            //
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panelTop.Controls.Add(this.grpOptions);
            this.panelTop.Controls.Add(this.grpLevel);
            this.panelTop.Controls.Add(this.grpCategory);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(780, 90);
            this.panelTop.TabIndex = 0;
            //
            // grpCategory
            //
            this.grpCategory.Controls.Add(this.btnDeselectAll);
            this.grpCategory.Controls.Add(this.btnSelectAll);
            this.grpCategory.Controls.Add(this.chkOther);
            this.grpCategory.Controls.Add(this.chkTrace);
            this.grpCategory.Controls.Add(this.chkDB);
            this.grpCategory.Controls.Add(this.chkSequence);
            this.grpCategory.Controls.Add(this.chkAlarm);
            this.grpCategory.Controls.Add(this.chkVision);
            this.grpCategory.Controls.Add(this.chkIO);
            this.grpCategory.Controls.Add(this.chkMotion);
            this.grpCategory.Controls.Add(this.chkSystem);
            this.grpCategory.ForeColor = System.Drawing.Color.White;
            this.grpCategory.Location = new System.Drawing.Point(5, 5);
            this.grpCategory.Name = "grpCategory";
            this.grpCategory.Size = new System.Drawing.Size(390, 80);
            this.grpCategory.TabIndex = 0;
            this.grpCategory.TabStop = false;
            this.grpCategory.Text = "Category";
            //
            // chkSystem
            //
            this.chkSystem.AutoSize = true;
            this.chkSystem.Checked = true;
            this.chkSystem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSystem.ForeColor = System.Drawing.Color.LightGray;
            this.chkSystem.Location = new System.Drawing.Point(10, 20);
            this.chkSystem.Name = "chkSystem";
            this.chkSystem.Size = new System.Drawing.Size(60, 17);
            this.chkSystem.TabIndex = 0;
            this.chkSystem.Text = "System";
            this.chkSystem.UseVisualStyleBackColor = true;
            this.chkSystem.CheckedChanged += new System.EventHandler(this.chkSystem_CheckedChanged);
            //
            // chkMotion
            //
            this.chkMotion.AutoSize = true;
            this.chkMotion.Checked = true;
            this.chkMotion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMotion.ForeColor = System.Drawing.Color.LightGray;
            this.chkMotion.Location = new System.Drawing.Point(75, 20);
            this.chkMotion.Name = "chkMotion";
            this.chkMotion.Size = new System.Drawing.Size(58, 17);
            this.chkMotion.TabIndex = 1;
            this.chkMotion.Text = "Motion";
            this.chkMotion.UseVisualStyleBackColor = true;
            this.chkMotion.CheckedChanged += new System.EventHandler(this.chkMotion_CheckedChanged);
            //
            // chkIO
            //
            this.chkIO.AutoSize = true;
            this.chkIO.Checked = true;
            this.chkIO.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIO.ForeColor = System.Drawing.Color.LightGray;
            this.chkIO.Location = new System.Drawing.Point(138, 20);
            this.chkIO.Name = "chkIO";
            this.chkIO.Size = new System.Drawing.Size(36, 17);
            this.chkIO.TabIndex = 2;
            this.chkIO.Text = "IO";
            this.chkIO.UseVisualStyleBackColor = true;
            this.chkIO.CheckedChanged += new System.EventHandler(this.chkIO_CheckedChanged);
            //
            // chkVision
            //
            this.chkVision.AutoSize = true;
            this.chkVision.Checked = true;
            this.chkVision.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkVision.ForeColor = System.Drawing.Color.LightGray;
            this.chkVision.Location = new System.Drawing.Point(179, 20);
            this.chkVision.Name = "chkVision";
            this.chkVision.Size = new System.Drawing.Size(53, 17);
            this.chkVision.TabIndex = 3;
            this.chkVision.Text = "Vision";
            this.chkVision.UseVisualStyleBackColor = true;
            this.chkVision.CheckedChanged += new System.EventHandler(this.chkVision_CheckedChanged);
            //
            // chkAlarm
            //
            this.chkAlarm.AutoSize = true;
            this.chkAlarm.Checked = true;
            this.chkAlarm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAlarm.ForeColor = System.Drawing.Color.LightGray;
            this.chkAlarm.Location = new System.Drawing.Point(237, 20);
            this.chkAlarm.Name = "chkAlarm";
            this.chkAlarm.Size = new System.Drawing.Size(52, 17);
            this.chkAlarm.TabIndex = 4;
            this.chkAlarm.Text = "Alarm";
            this.chkAlarm.UseVisualStyleBackColor = true;
            this.chkAlarm.CheckedChanged += new System.EventHandler(this.chkAlarm_CheckedChanged);
            //
            // chkSequence
            //
            this.chkSequence.AutoSize = true;
            this.chkSequence.Checked = true;
            this.chkSequence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSequence.ForeColor = System.Drawing.Color.LightGray;
            this.chkSequence.Location = new System.Drawing.Point(10, 42);
            this.chkSequence.Name = "chkSequence";
            this.chkSequence.Size = new System.Drawing.Size(72, 17);
            this.chkSequence.TabIndex = 5;
            this.chkSequence.Text = "Sequence";
            this.chkSequence.UseVisualStyleBackColor = true;
            this.chkSequence.CheckedChanged += new System.EventHandler(this.chkSequence_CheckedChanged);
            //
            // chkDB
            //
            this.chkDB.AutoSize = true;
            this.chkDB.Checked = true;
            this.chkDB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDB.ForeColor = System.Drawing.Color.LightGray;
            this.chkDB.Location = new System.Drawing.Point(88, 42);
            this.chkDB.Name = "chkDB";
            this.chkDB.Size = new System.Drawing.Size(39, 17);
            this.chkDB.TabIndex = 6;
            this.chkDB.Text = "DB";
            this.chkDB.UseVisualStyleBackColor = true;
            this.chkDB.CheckedChanged += new System.EventHandler(this.chkDB_CheckedChanged);
            //
            // chkTrace
            //
            this.chkTrace.AutoSize = true;
            this.chkTrace.Checked = true;
            this.chkTrace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTrace.ForeColor = System.Drawing.Color.LightGray;
            this.chkTrace.Location = new System.Drawing.Point(133, 42);
            this.chkTrace.Name = "chkTrace";
            this.chkTrace.Size = new System.Drawing.Size(52, 17);
            this.chkTrace.TabIndex = 7;
            this.chkTrace.Text = "Trace";
            this.chkTrace.UseVisualStyleBackColor = true;
            this.chkTrace.CheckedChanged += new System.EventHandler(this.chkTrace_CheckedChanged);
            //
            // chkOther
            //
            this.chkOther.AutoSize = true;
            this.chkOther.Checked = true;
            this.chkOther.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOther.ForeColor = System.Drawing.Color.LightGray;
            this.chkOther.Location = new System.Drawing.Point(191, 42);
            this.chkOther.Name = "chkOther";
            this.chkOther.Size = new System.Drawing.Size(51, 17);
            this.chkOther.TabIndex = 8;
            this.chkOther.Text = "Other";
            this.chkOther.UseVisualStyleBackColor = true;
            this.chkOther.CheckedChanged += new System.EventHandler(this.chkOther_CheckedChanged);
            //
            // btnSelectAll
            //
            this.btnSelectAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAll.ForeColor = System.Drawing.Color.White;
            this.btnSelectAll.Location = new System.Drawing.Point(295, 15);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(45, 25);
            this.btnSelectAll.TabIndex = 9;
            this.btnSelectAll.Text = "All";
            this.btnSelectAll.UseVisualStyleBackColor = false;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAllCategories_Click);
            //
            // btnDeselectAll
            //
            this.btnDeselectAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnDeselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeselectAll.ForeColor = System.Drawing.Color.White;
            this.btnDeselectAll.Location = new System.Drawing.Point(340, 15);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size(45, 25);
            this.btnDeselectAll.TabIndex = 10;
            this.btnDeselectAll.Text = "None";
            this.btnDeselectAll.UseVisualStyleBackColor = false;
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAllCategories_Click);
            //
            // grpLevel
            //
            this.grpLevel.Controls.Add(this.chkFatal);
            this.grpLevel.Controls.Add(this.chkError);
            this.grpLevel.Controls.Add(this.chkWarn);
            this.grpLevel.Controls.Add(this.chkInfo);
            this.grpLevel.Controls.Add(this.chkDebug);
            this.grpLevel.ForeColor = System.Drawing.Color.White;
            this.grpLevel.Location = new System.Drawing.Point(400, 5);
            this.grpLevel.Name = "grpLevel";
            this.grpLevel.Size = new System.Drawing.Size(200, 80);
            this.grpLevel.TabIndex = 1;
            this.grpLevel.TabStop = false;
            this.grpLevel.Text = "Level";
            //
            // chkDebug
            //
            this.chkDebug.AutoSize = true;
            this.chkDebug.Checked = true;
            this.chkDebug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDebug.ForeColor = System.Drawing.Color.Gray;
            this.chkDebug.Location = new System.Drawing.Point(10, 20);
            this.chkDebug.Name = "chkDebug";
            this.chkDebug.Size = new System.Drawing.Size(57, 17);
            this.chkDebug.TabIndex = 0;
            this.chkDebug.Text = "Debug";
            this.chkDebug.UseVisualStyleBackColor = true;
            this.chkDebug.CheckedChanged += new System.EventHandler(this.chkDebug_CheckedChanged);
            //
            // chkInfo
            //
            this.chkInfo.AutoSize = true;
            this.chkInfo.Checked = true;
            this.chkInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInfo.ForeColor = System.Drawing.Color.LightGreen;
            this.chkInfo.Location = new System.Drawing.Point(73, 20);
            this.chkInfo.Name = "chkInfo";
            this.chkInfo.Size = new System.Drawing.Size(43, 17);
            this.chkInfo.TabIndex = 1;
            this.chkInfo.Text = "Info";
            this.chkInfo.UseVisualStyleBackColor = true;
            this.chkInfo.CheckedChanged += new System.EventHandler(this.chkInfo_CheckedChanged);
            //
            // chkWarn
            //
            this.chkWarn.AutoSize = true;
            this.chkWarn.Checked = true;
            this.chkWarn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWarn.ForeColor = System.Drawing.Color.Yellow;
            this.chkWarn.Location = new System.Drawing.Point(122, 20);
            this.chkWarn.Name = "chkWarn";
            this.chkWarn.Size = new System.Drawing.Size(51, 17);
            this.chkWarn.TabIndex = 2;
            this.chkWarn.Text = "Warn";
            this.chkWarn.UseVisualStyleBackColor = true;
            this.chkWarn.CheckedChanged += new System.EventHandler(this.chkWarn_CheckedChanged);
            //
            // chkError
            //
            this.chkError.AutoSize = true;
            this.chkError.Checked = true;
            this.chkError.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkError.ForeColor = System.Drawing.Color.OrangeRed;
            this.chkError.Location = new System.Drawing.Point(10, 42);
            this.chkError.Name = "chkError";
            this.chkError.Size = new System.Drawing.Size(47, 17);
            this.chkError.TabIndex = 3;
            this.chkError.Text = "Error";
            this.chkError.UseVisualStyleBackColor = true;
            this.chkError.CheckedChanged += new System.EventHandler(this.chkError_CheckedChanged);
            //
            // chkFatal
            //
            this.chkFatal.AutoSize = true;
            this.chkFatal.Checked = true;
            this.chkFatal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFatal.ForeColor = System.Drawing.Color.Red;
            this.chkFatal.Location = new System.Drawing.Point(63, 42);
            this.chkFatal.Name = "chkFatal";
            this.chkFatal.Size = new System.Drawing.Size(48, 17);
            this.chkFatal.TabIndex = 4;
            this.chkFatal.Text = "Fatal";
            this.chkFatal.UseVisualStyleBackColor = true;
            this.chkFatal.CheckedChanged += new System.EventHandler(this.chkFatal_CheckedChanged);
            //
            // grpOptions
            //
            this.grpOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOptions.Controls.Add(this.txtSearch);
            this.grpOptions.Controls.Add(this.lblSearch);
            this.grpOptions.Controls.Add(this.chkAutoScroll);
            this.grpOptions.Controls.Add(this.btnClear);
            this.grpOptions.ForeColor = System.Drawing.Color.White;
            this.grpOptions.Location = new System.Drawing.Point(605, 5);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(170, 80);
            this.grpOptions.TabIndex = 2;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            //
            // chkAutoScroll
            //
            this.chkAutoScroll.AutoSize = true;
            this.chkAutoScroll.Checked = true;
            this.chkAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoScroll.ForeColor = System.Drawing.Color.LightGray;
            this.chkAutoScroll.Location = new System.Drawing.Point(10, 20);
            this.chkAutoScroll.Name = "chkAutoScroll";
            this.chkAutoScroll.Size = new System.Drawing.Size(77, 17);
            this.chkAutoScroll.TabIndex = 0;
            this.chkAutoScroll.Text = "Auto Scroll";
            this.chkAutoScroll.UseVisualStyleBackColor = true;
            this.chkAutoScroll.CheckedChanged += new System.EventHandler(this.chkAutoScroll_CheckedChanged);
            //
            // btnClear
            //
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(95, 15);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(65, 25);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            //
            // lblSearch
            //
            this.lblSearch.AutoSize = true;
            this.lblSearch.ForeColor = System.Drawing.Color.LightGray;
            this.lblSearch.Location = new System.Drawing.Point(7, 48);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(44, 13);
            this.lblSearch.TabIndex = 2;
            this.lblSearch.Text = "Search:";
            //
            // txtSearch
            //
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.ForeColor = System.Drawing.Color.White;
            this.txtSearch.Location = new System.Drawing.Point(57, 45);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(103, 20);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            //
            // listViewLogs
            //
            this.listViewLogs.BackColor = System.Drawing.Color.White;
            this.listViewLogs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTime,
            this.colLevel,
            this.colLogger,
            this.colMessage});
            this.listViewLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewLogs.Font = new System.Drawing.Font("Consolas", 9F);
            this.listViewLogs.ForeColor = System.Drawing.Color.Black;
            this.listViewLogs.FullRowSelect = true;
            this.listViewLogs.GridLines = true;
            this.listViewLogs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewLogs.HideSelection = false;
            this.listViewLogs.Location = new System.Drawing.Point(0, 90);
            this.listViewLogs.Name = "listViewLogs";
            this.listViewLogs.Size = new System.Drawing.Size(780, 507);
            this.listViewLogs.TabIndex = 1;
            this.listViewLogs.UseCompatibleStateImageBehavior = false;
            this.listViewLogs.View = System.Windows.Forms.View.Details;
            this.listViewLogs.VirtualMode = true;
            this.listViewLogs.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listViewLogs_RetrieveVirtualItem);
            //
            // colTime
            //
            this.colTime.Text = "Time";
            this.colTime.Width = 100;
            //
            // colLevel
            //
            this.colLevel.Text = "Level";
            this.colLevel.Width = 60;
            //
            // colLogger
            //
            this.colLogger.Text = "Logger";
            this.colLogger.Width = 80;
            //
            // colMessage
            //
            this.colMessage.Text = "Message";
            this.colMessage.Width = 520;
            //
            // panelBottom
            //
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panelBottom.Controls.Add(this.lblStatus);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 597);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(780, 25);
            this.panelBottom.TabIndex = 2;
            //
            // lblStatus
            //
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.LightGray;
            this.lblStatus.Location = new System.Drawing.Point(5, 5);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(108, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Total: 0 | Filtered: 0";
            //
            // LogPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.listViewLogs);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.Name = "LogPanel";
            this.Size = new System.Drawing.Size(780, 622);
            this.panelTop.ResumeLayout(false);
            this.grpLevel.ResumeLayout(false);
            this.grpLevel.PerformLayout();
            this.grpCategory.ResumeLayout(false);
            this.grpCategory.PerformLayout();
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.GroupBox grpCategory;
        private System.Windows.Forms.CheckBox chkSystem;
        private System.Windows.Forms.CheckBox chkMotion;
        private System.Windows.Forms.CheckBox chkIO;
        private System.Windows.Forms.CheckBox chkVision;
        private System.Windows.Forms.CheckBox chkAlarm;
        private System.Windows.Forms.CheckBox chkSequence;
        private System.Windows.Forms.CheckBox chkDB;
        private System.Windows.Forms.CheckBox chkTrace;
        private System.Windows.Forms.CheckBox chkOther;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnDeselectAll;
        private System.Windows.Forms.GroupBox grpLevel;
        private System.Windows.Forms.CheckBox chkDebug;
        private System.Windows.Forms.CheckBox chkInfo;
        private System.Windows.Forms.CheckBox chkWarn;
        private System.Windows.Forms.CheckBox chkError;
        private System.Windows.Forms.CheckBox chkFatal;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.CheckBox chkAutoScroll;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ListView listViewLogs;
        private System.Windows.Forms.ColumnHeader colTime;
        private System.Windows.Forms.ColumnHeader colLevel;
        private System.Windows.Forms.ColumnHeader colLogger;
        private System.Windows.Forms.ColumnHeader colMessage;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblStatus;
    }
}
