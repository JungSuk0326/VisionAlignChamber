namespace VisionAlignChamber.Views.Controls
{
    partial class ParameterPanel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.grpVisionScan = new System.Windows.Forms.GroupBox();
            this.lblScanImageCount = new System.Windows.Forms.Label();
            this.txtScanImageCount = new System.Windows.Forms.TextBox();
            this.lblScanStepAngle = new System.Windows.Forms.Label();
            this.txtScanStepAngle = new System.Windows.Forms.TextBox();
            this.grpTheta = new System.Windows.Forms.GroupBox();
            this.lblTheta_Home = new System.Windows.Forms.Label();
            this.txtTheta_Home = new System.Windows.Forms.TextBox();
            this.grpCenterR = new System.Windows.Forms.GroupBox();
            this.lblCenterR_MinCtr = new System.Windows.Forms.Label();
            this.txtCenterR_MinCtr = new System.Windows.Forms.TextBox();
            this.lblCenterR_Open = new System.Windows.Forms.Label();
            this.txtCenterR_Open = new System.Windows.Forms.TextBox();
            this.grpCenterL = new System.Windows.Forms.GroupBox();
            this.lblCenterL_MinCtr = new System.Windows.Forms.Label();
            this.txtCenterL_MinCtr = new System.Windows.Forms.TextBox();
            this.lblCenterL_Open = new System.Windows.Forms.Label();
            this.txtCenterL_Open = new System.Windows.Forms.TextBox();
            this.grpChuckZ = new System.Windows.Forms.GroupBox();
            this.lblChuckZ_Eddy = new System.Windows.Forms.Label();
            this.txtChuckZ_Eddy = new System.Windows.Forms.TextBox();
            this.lblChuckZ_Vision = new System.Windows.Forms.Label();
            this.txtChuckZ_Vision = new System.Windows.Forms.TextBox();
            this.lblChuckZ_Down = new System.Windows.Forms.Label();
            this.txtChuckZ_Down = new System.Windows.Forms.TextBox();
            this.panelRight = new System.Windows.Forms.Panel();
            this.grpSequence = new System.Windows.Forms.GroupBox();
            this.dgvSequence = new System.Windows.Forms.DataGridView();
            this.colStep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChuckZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCenterL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCenterR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTheta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.grpVisionScan.SuspendLayout();
            this.grpTheta.SuspendLayout();
            this.grpCenterR.SuspendLayout();
            this.grpCenterL.SuspendLayout();
            this.grpChuckZ.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.grpSequence.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSequence)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            //
            // splitMain
            //
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 40);
            this.splitMain.Name = "splitMain";
            //
            // splitMain.Panel1
            //
            this.splitMain.Panel1.Controls.Add(this.panelLeft);
            this.splitMain.Panel1MinSize = 300;
            //
            // splitMain.Panel2
            //
            this.splitMain.Panel2.Controls.Add(this.panelRight);
            this.splitMain.Size = new System.Drawing.Size(774, 551);
            this.splitMain.SplitterDistance = 350;
            this.splitMain.TabIndex = 0;
            //
            // panelLeft
            //
            this.panelLeft.AutoScroll = true;
            this.panelLeft.Controls.Add(this.grpVisionScan);
            this.panelLeft.Controls.Add(this.grpTheta);
            this.panelLeft.Controls.Add(this.grpCenterR);
            this.panelLeft.Controls.Add(this.grpCenterL);
            this.panelLeft.Controls.Add(this.grpChuckZ);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(350, 551);
            this.panelLeft.TabIndex = 0;
            //
            // grpChuckZ
            //
            this.grpChuckZ.Controls.Add(this.lblChuckZ_Eddy);
            this.grpChuckZ.Controls.Add(this.txtChuckZ_Eddy);
            this.grpChuckZ.Controls.Add(this.lblChuckZ_Vision);
            this.grpChuckZ.Controls.Add(this.txtChuckZ_Vision);
            this.grpChuckZ.Controls.Add(this.lblChuckZ_Down);
            this.grpChuckZ.Controls.Add(this.txtChuckZ_Down);
            this.grpChuckZ.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(149)))), ((int)(((byte)(237)))));
            this.grpChuckZ.Location = new System.Drawing.Point(10, 10);
            this.grpChuckZ.Name = "grpChuckZ";
            this.grpChuckZ.Size = new System.Drawing.Size(320, 85);
            this.grpChuckZ.TabIndex = 0;
            this.grpChuckZ.TabStop = false;
            this.grpChuckZ.Text = "Chuck Z (Axis 0)";
            //
            // lblChuckZ_Down
            //
            this.lblChuckZ_Down.AutoSize = true;
            this.lblChuckZ_Down.ForeColor = System.Drawing.Color.White;
            this.lblChuckZ_Down.Location = new System.Drawing.Point(10, 25);
            this.lblChuckZ_Down.Name = "lblChuckZ_Down";
            this.lblChuckZ_Down.Size = new System.Drawing.Size(39, 15);
            this.lblChuckZ_Down.TabIndex = 0;
            this.lblChuckZ_Down.Text = "Down";
            //
            // txtChuckZ_Down
            //
            this.txtChuckZ_Down.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtChuckZ_Down.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtChuckZ_Down.ForeColor = System.Drawing.Color.White;
            this.txtChuckZ_Down.Location = new System.Drawing.Point(10, 45);
            this.txtChuckZ_Down.Name = "txtChuckZ_Down";
            this.txtChuckZ_Down.Size = new System.Drawing.Size(90, 22);
            this.txtChuckZ_Down.TabIndex = 1;
            this.txtChuckZ_Down.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtChuckZ_Down.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // lblChuckZ_Vision
            //
            this.lblChuckZ_Vision.AutoSize = true;
            this.lblChuckZ_Vision.ForeColor = System.Drawing.Color.White;
            this.lblChuckZ_Vision.Location = new System.Drawing.Point(115, 25);
            this.lblChuckZ_Vision.Name = "lblChuckZ_Vision";
            this.lblChuckZ_Vision.Size = new System.Drawing.Size(40, 15);
            this.lblChuckZ_Vision.TabIndex = 2;
            this.lblChuckZ_Vision.Text = "Vision";
            //
            // txtChuckZ_Vision
            //
            this.txtChuckZ_Vision.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtChuckZ_Vision.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtChuckZ_Vision.ForeColor = System.Drawing.Color.White;
            this.txtChuckZ_Vision.Location = new System.Drawing.Point(115, 45);
            this.txtChuckZ_Vision.Name = "txtChuckZ_Vision";
            this.txtChuckZ_Vision.Size = new System.Drawing.Size(90, 22);
            this.txtChuckZ_Vision.TabIndex = 3;
            this.txtChuckZ_Vision.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtChuckZ_Vision.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // lblChuckZ_Eddy
            //
            this.lblChuckZ_Eddy.AutoSize = true;
            this.lblChuckZ_Eddy.ForeColor = System.Drawing.Color.White;
            this.lblChuckZ_Eddy.Location = new System.Drawing.Point(220, 25);
            this.lblChuckZ_Eddy.Name = "lblChuckZ_Eddy";
            this.lblChuckZ_Eddy.Size = new System.Drawing.Size(34, 15);
            this.lblChuckZ_Eddy.TabIndex = 4;
            this.lblChuckZ_Eddy.Text = "Eddy";
            //
            // txtChuckZ_Eddy
            //
            this.txtChuckZ_Eddy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtChuckZ_Eddy.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtChuckZ_Eddy.ForeColor = System.Drawing.Color.White;
            this.txtChuckZ_Eddy.Location = new System.Drawing.Point(220, 45);
            this.txtChuckZ_Eddy.Name = "txtChuckZ_Eddy";
            this.txtChuckZ_Eddy.Size = new System.Drawing.Size(90, 22);
            this.txtChuckZ_Eddy.TabIndex = 5;
            this.txtChuckZ_Eddy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtChuckZ_Eddy.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // grpCenterL
            //
            this.grpCenterL.Controls.Add(this.lblCenterL_MinCtr);
            this.grpCenterL.Controls.Add(this.txtCenterL_MinCtr);
            this.grpCenterL.Controls.Add(this.lblCenterL_Open);
            this.grpCenterL.Controls.Add(this.txtCenterL_Open);
            this.grpCenterL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(238)))), ((int)(((byte)(144)))));
            this.grpCenterL.Location = new System.Drawing.Point(10, 100);
            this.grpCenterL.Name = "grpCenterL";
            this.grpCenterL.Size = new System.Drawing.Size(320, 70);
            this.grpCenterL.TabIndex = 1;
            this.grpCenterL.TabStop = false;
            this.grpCenterL.Text = "Centering L (Axis 2)";
            //
            // lblCenterL_Open
            //
            this.lblCenterL_Open.AutoSize = true;
            this.lblCenterL_Open.ForeColor = System.Drawing.Color.White;
            this.lblCenterL_Open.Location = new System.Drawing.Point(10, 20);
            this.lblCenterL_Open.Name = "lblCenterL_Open";
            this.lblCenterL_Open.Size = new System.Drawing.Size(36, 15);
            this.lblCenterL_Open.TabIndex = 0;
            this.lblCenterL_Open.Text = "Open";
            //
            // txtCenterL_Open
            //
            this.txtCenterL_Open.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtCenterL_Open.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtCenterL_Open.ForeColor = System.Drawing.Color.White;
            this.txtCenterL_Open.Location = new System.Drawing.Point(10, 40);
            this.txtCenterL_Open.Name = "txtCenterL_Open";
            this.txtCenterL_Open.Size = new System.Drawing.Size(90, 22);
            this.txtCenterL_Open.TabIndex = 1;
            this.txtCenterL_Open.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCenterL_Open.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // lblCenterL_MinCtr
            //
            this.lblCenterL_MinCtr.AutoSize = true;
            this.lblCenterL_MinCtr.ForeColor = System.Drawing.Color.White;
            this.lblCenterL_MinCtr.Location = new System.Drawing.Point(115, 20);
            this.lblCenterL_MinCtr.Name = "lblCenterL_MinCtr";
            this.lblCenterL_MinCtr.Size = new System.Drawing.Size(76, 15);
            this.lblCenterL_MinCtr.TabIndex = 2;
            this.lblCenterL_MinCtr.Text = "MIN_CTR";
            //
            // txtCenterL_MinCtr
            //
            this.txtCenterL_MinCtr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtCenterL_MinCtr.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtCenterL_MinCtr.ForeColor = System.Drawing.Color.White;
            this.txtCenterL_MinCtr.Location = new System.Drawing.Point(115, 40);
            this.txtCenterL_MinCtr.Name = "txtCenterL_MinCtr";
            this.txtCenterL_MinCtr.Size = new System.Drawing.Size(90, 22);
            this.txtCenterL_MinCtr.TabIndex = 3;
            this.txtCenterL_MinCtr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCenterL_MinCtr.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // grpCenterR
            //
            this.grpCenterR.Controls.Add(this.lblCenterR_MinCtr);
            this.grpCenterR.Controls.Add(this.txtCenterR_MinCtr);
            this.grpCenterR.Controls.Add(this.lblCenterR_Open);
            this.grpCenterR.Controls.Add(this.txtCenterR_Open);
            this.grpCenterR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(200)))), ((int)(((byte)(100)))));
            this.grpCenterR.Location = new System.Drawing.Point(10, 175);
            this.grpCenterR.Name = "grpCenterR";
            this.grpCenterR.Size = new System.Drawing.Size(320, 70);
            this.grpCenterR.TabIndex = 2;
            this.grpCenterR.TabStop = false;
            this.grpCenterR.Text = "Centering R (Axis 3)";
            //
            // lblCenterR_Open
            //
            this.lblCenterR_Open.AutoSize = true;
            this.lblCenterR_Open.ForeColor = System.Drawing.Color.White;
            this.lblCenterR_Open.Location = new System.Drawing.Point(10, 20);
            this.lblCenterR_Open.Name = "lblCenterR_Open";
            this.lblCenterR_Open.Size = new System.Drawing.Size(36, 15);
            this.lblCenterR_Open.TabIndex = 0;
            this.lblCenterR_Open.Text = "Open";
            //
            // txtCenterR_Open
            //
            this.txtCenterR_Open.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtCenterR_Open.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtCenterR_Open.ForeColor = System.Drawing.Color.White;
            this.txtCenterR_Open.Location = new System.Drawing.Point(10, 40);
            this.txtCenterR_Open.Name = "txtCenterR_Open";
            this.txtCenterR_Open.Size = new System.Drawing.Size(90, 22);
            this.txtCenterR_Open.TabIndex = 1;
            this.txtCenterR_Open.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCenterR_Open.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // lblCenterR_MinCtr
            //
            this.lblCenterR_MinCtr.AutoSize = true;
            this.lblCenterR_MinCtr.ForeColor = System.Drawing.Color.White;
            this.lblCenterR_MinCtr.Location = new System.Drawing.Point(115, 20);
            this.lblCenterR_MinCtr.Name = "lblCenterR_MinCtr";
            this.lblCenterR_MinCtr.Size = new System.Drawing.Size(76, 15);
            this.lblCenterR_MinCtr.TabIndex = 2;
            this.lblCenterR_MinCtr.Text = "MIN_CTR";
            //
            // txtCenterR_MinCtr
            //
            this.txtCenterR_MinCtr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtCenterR_MinCtr.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtCenterR_MinCtr.ForeColor = System.Drawing.Color.White;
            this.txtCenterR_MinCtr.Location = new System.Drawing.Point(115, 40);
            this.txtCenterR_MinCtr.Name = "txtCenterR_MinCtr";
            this.txtCenterR_MinCtr.Size = new System.Drawing.Size(90, 22);
            this.txtCenterR_MinCtr.TabIndex = 3;
            this.txtCenterR_MinCtr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCenterR_MinCtr.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // grpTheta
            //
            this.grpTheta.Controls.Add(this.lblTheta_Home);
            this.grpTheta.Controls.Add(this.txtTheta_Home);
            this.grpTheta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(182)))), ((int)(((byte)(193)))));
            this.grpTheta.Location = new System.Drawing.Point(10, 250);
            this.grpTheta.Name = "grpTheta";
            this.grpTheta.Size = new System.Drawing.Size(320, 85);
            this.grpTheta.TabIndex = 3;
            this.grpTheta.TabStop = false;
            this.grpTheta.Text = "Theta (Axis 1)";
            //
            // lblTheta_Home
            //
            this.lblTheta_Home.AutoSize = true;
            this.lblTheta_Home.ForeColor = System.Drawing.Color.White;
            this.lblTheta_Home.Location = new System.Drawing.Point(10, 25);
            this.lblTheta_Home.Name = "lblTheta_Home";
            this.lblTheta_Home.Size = new System.Drawing.Size(40, 15);
            this.lblTheta_Home.TabIndex = 0;
            this.lblTheta_Home.Text = "Home";
            //
            // txtTheta_Home
            //
            this.txtTheta_Home.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtTheta_Home.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtTheta_Home.ForeColor = System.Drawing.Color.White;
            this.txtTheta_Home.Location = new System.Drawing.Point(10, 45);
            this.txtTheta_Home.Name = "txtTheta_Home";
            this.txtTheta_Home.Size = new System.Drawing.Size(90, 22);
            this.txtTheta_Home.TabIndex = 1;
            this.txtTheta_Home.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTheta_Home.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // grpVisionScan
            //
            this.grpVisionScan.Controls.Add(this.lblScanImageCount);
            this.grpVisionScan.Controls.Add(this.txtScanImageCount);
            this.grpVisionScan.Controls.Add(this.lblScanStepAngle);
            this.grpVisionScan.Controls.Add(this.txtScanStepAngle);
            this.grpVisionScan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(0)))));
            this.grpVisionScan.Location = new System.Drawing.Point(10, 340);
            this.grpVisionScan.Name = "grpVisionScan";
            this.grpVisionScan.Size = new System.Drawing.Size(320, 70);
            this.grpVisionScan.TabIndex = 5;
            this.grpVisionScan.TabStop = false;
            this.grpVisionScan.Text = "Vision Scan";
            //
            // lblScanStepAngle
            //
            this.lblScanStepAngle.AutoSize = true;
            this.lblScanStepAngle.ForeColor = System.Drawing.Color.White;
            this.lblScanStepAngle.Location = new System.Drawing.Point(10, 20);
            this.lblScanStepAngle.Name = "lblScanStepAngle";
            this.lblScanStepAngle.Size = new System.Drawing.Size(66, 15);
            this.lblScanStepAngle.TabIndex = 0;
            this.lblScanStepAngle.Text = "Step Angle";
            //
            // txtScanStepAngle
            //
            this.txtScanStepAngle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtScanStepAngle.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtScanStepAngle.ForeColor = System.Drawing.Color.White;
            this.txtScanStepAngle.Location = new System.Drawing.Point(10, 40);
            this.txtScanStepAngle.Name = "txtScanStepAngle";
            this.txtScanStepAngle.Size = new System.Drawing.Size(90, 22);
            this.txtScanStepAngle.TabIndex = 1;
            this.txtScanStepAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtScanStepAngle.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // lblScanImageCount
            //
            this.lblScanImageCount.AutoSize = true;
            this.lblScanImageCount.ForeColor = System.Drawing.Color.White;
            this.lblScanImageCount.Location = new System.Drawing.Point(115, 20);
            this.lblScanImageCount.Name = "lblScanImageCount";
            this.lblScanImageCount.Size = new System.Drawing.Size(77, 15);
            this.lblScanImageCount.TabIndex = 2;
            this.lblScanImageCount.Text = "Image Count";
            //
            // txtScanImageCount
            //
            this.txtScanImageCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtScanImageCount.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtScanImageCount.ForeColor = System.Drawing.Color.White;
            this.txtScanImageCount.Location = new System.Drawing.Point(115, 40);
            this.txtScanImageCount.Name = "txtScanImageCount";
            this.txtScanImageCount.Size = new System.Drawing.Size(90, 22);
            this.txtScanImageCount.TabIndex = 3;
            this.txtScanImageCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtScanImageCount.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // panelRight
            //
            this.panelRight.Controls.Add(this.grpSequence);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(5);
            this.panelRight.Size = new System.Drawing.Size(420, 551);
            this.panelRight.TabIndex = 0;
            //
            // grpSequence
            //
            this.grpSequence.Controls.Add(this.dgvSequence);
            this.grpSequence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSequence.ForeColor = System.Drawing.Color.White;
            this.grpSequence.Location = new System.Drawing.Point(5, 5);
            this.grpSequence.Name = "grpSequence";
            this.grpSequence.Size = new System.Drawing.Size(410, 541);
            this.grpSequence.TabIndex = 0;
            this.grpSequence.TabStop = false;
            this.grpSequence.Text = "Sequence Preview (10 Steps)";
            //
            // dgvSequence
            //
            this.dgvSequence.AllowUserToAddRows = false;
            this.dgvSequence.AllowUserToDeleteRows = false;
            this.dgvSequence.AllowUserToResizeRows = false;
            this.dgvSequence.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.dgvSequence.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSequence.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSequence.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSequence.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStep,
            this.colName,
            this.colChuckZ,
            this.colCenterL,
            this.colCenterR,
            this.colTheta});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSequence.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSequence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSequence.EnableHeadersVisualStyles = false;
            this.dgvSequence.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.dgvSequence.Location = new System.Drawing.Point(3, 19);
            this.dgvSequence.Name = "dgvSequence";
            this.dgvSequence.ReadOnly = true;
            this.dgvSequence.RowHeadersVisible = false;
            this.dgvSequence.RowTemplate.Height = 28;
            this.dgvSequence.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSequence.Size = new System.Drawing.Size(404, 519);
            this.dgvSequence.TabIndex = 0;
            //
            // colStep
            //
            this.colStep.HeaderText = "Step";
            this.colStep.Name = "colStep";
            this.colStep.ReadOnly = true;
            this.colStep.Width = 40;
            //
            // colName
            //
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 90;
            //
            // colChuckZ
            //
            this.colChuckZ.HeaderText = "Chuck Z";
            this.colChuckZ.Name = "colChuckZ";
            this.colChuckZ.ReadOnly = true;
            this.colChuckZ.Width = 65;
            //
            // colCenterL
            //
            this.colCenterL.HeaderText = "Center L";
            this.colCenterL.Name = "colCenterL";
            this.colCenterL.ReadOnly = true;
            this.colCenterL.Width = 65;
            //
            // colCenterR
            //
            this.colCenterR.HeaderText = "Center R";
            this.colCenterR.Name = "colCenterR";
            this.colCenterR.ReadOnly = true;
            this.colCenterR.Width = 65;
            //
            // colTheta
            //
            this.colTheta.HeaderText = "Theta";
            this.colTheta.Name = "colTheta";
            this.colTheta.ReadOnly = true;
            this.colTheta.Width = 65;
            //
            // panelButtons
            //
            this.panelButtons.Controls.Add(this.btnLoad);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Controls.Add(this.lblTitle);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelButtons.Location = new System.Drawing.Point(0, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(774, 40);
            this.panelButtons.TabIndex = 1;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(173, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Teaching Parameters";
            //
            // btnSave
            //
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(669, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnLoad
            //
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoad.ForeColor = System.Drawing.Color.White;
            this.btnLoad.Location = new System.Drawing.Point(563, 5);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(100, 30);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            //
            // ParameterPanel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.panelButtons);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ParameterPanel";
            this.Size = new System.Drawing.Size(774, 591);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.panelLeft.ResumeLayout(false);
            this.grpVisionScan.ResumeLayout(false);
            this.grpVisionScan.PerformLayout();
            this.grpTheta.ResumeLayout(false);
            this.grpTheta.PerformLayout();
            this.grpCenterR.ResumeLayout(false);
            this.grpCenterR.PerformLayout();
            this.grpCenterL.ResumeLayout(false);
            this.grpCenterL.PerformLayout();
            this.grpChuckZ.ResumeLayout(false);
            this.grpChuckZ.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.grpSequence.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSequence)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.panelButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;

        // Chuck Z
        private System.Windows.Forms.GroupBox grpChuckZ;
        private System.Windows.Forms.Label lblChuckZ_Down;
        private System.Windows.Forms.TextBox txtChuckZ_Down;
        private System.Windows.Forms.Label lblChuckZ_Vision;
        private System.Windows.Forms.TextBox txtChuckZ_Vision;
        private System.Windows.Forms.Label lblChuckZ_Eddy;
        private System.Windows.Forms.TextBox txtChuckZ_Eddy;

        // Centering L
        private System.Windows.Forms.GroupBox grpCenterL;
        private System.Windows.Forms.Label lblCenterL_Open;
        private System.Windows.Forms.TextBox txtCenterL_Open;
        private System.Windows.Forms.Label lblCenterL_MinCtr;
        private System.Windows.Forms.TextBox txtCenterL_MinCtr;

        // Centering R
        private System.Windows.Forms.GroupBox grpCenterR;
        private System.Windows.Forms.Label lblCenterR_Open;
        private System.Windows.Forms.TextBox txtCenterR_Open;
        private System.Windows.Forms.Label lblCenterR_MinCtr;
        private System.Windows.Forms.TextBox txtCenterR_MinCtr;

        // Theta
        private System.Windows.Forms.GroupBox grpTheta;
        private System.Windows.Forms.Label lblTheta_Home;
        private System.Windows.Forms.TextBox txtTheta_Home;

        // Vision Scan
        private System.Windows.Forms.GroupBox grpVisionScan;
        private System.Windows.Forms.Label lblScanStepAngle;
        private System.Windows.Forms.TextBox txtScanStepAngle;
        private System.Windows.Forms.Label lblScanImageCount;
        private System.Windows.Forms.TextBox txtScanImageCount;

        // Sequence Preview
        private System.Windows.Forms.GroupBox grpSequence;
        private System.Windows.Forms.DataGridView dgvSequence;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStep;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChuckZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCenterL;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCenterR;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTheta;
    }
}
