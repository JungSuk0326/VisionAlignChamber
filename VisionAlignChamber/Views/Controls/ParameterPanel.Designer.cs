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
            this.numScanImageCount = new System.Windows.Forms.NumericUpDown();
            this.lblScanStepAngle = new System.Windows.Forms.Label();
            this.numScanStepAngle = new System.Windows.Forms.NumericUpDown();
            this.grpMotion = new System.Windows.Forms.GroupBox();
            this.lblDecel = new System.Windows.Forms.Label();
            this.numDecel = new System.Windows.Forms.NumericUpDown();
            this.lblAccel = new System.Windows.Forms.Label();
            this.numAccel = new System.Windows.Forms.NumericUpDown();
            this.lblVelocity = new System.Windows.Forms.Label();
            this.numVelocity = new System.Windows.Forms.NumericUpDown();
            this.grpTheta = new System.Windows.Forms.GroupBox();
            this.lblTheta_ScanEnd = new System.Windows.Forms.Label();
            this.numTheta_ScanEnd = new System.Windows.Forms.NumericUpDown();
            this.lblTheta_ScanStart = new System.Windows.Forms.Label();
            this.numTheta_ScanStart = new System.Windows.Forms.NumericUpDown();
            this.lblTheta_Home = new System.Windows.Forms.Label();
            this.numTheta_Home = new System.Windows.Forms.NumericUpDown();
            this.grpCenterR = new System.Windows.Forms.GroupBox();
            this.lblCenterR_MinCtr = new System.Windows.Forms.Label();
            this.numCenterR_MinCtr = new System.Windows.Forms.NumericUpDown();
            this.lblCenterR_Open = new System.Windows.Forms.Label();
            this.numCenterR_Open = new System.Windows.Forms.NumericUpDown();
            this.grpCenterL = new System.Windows.Forms.GroupBox();
            this.lblCenterL_MinCtr = new System.Windows.Forms.Label();
            this.numCenterL_MinCtr = new System.Windows.Forms.NumericUpDown();
            this.lblCenterL_Open = new System.Windows.Forms.Label();
            this.numCenterL_Open = new System.Windows.Forms.NumericUpDown();
            this.grpChuckZ = new System.Windows.Forms.GroupBox();
            this.lblChuckZ_Eddy = new System.Windows.Forms.Label();
            this.numChuckZ_Eddy = new System.Windows.Forms.NumericUpDown();
            this.lblChuckZ_Vision = new System.Windows.Forms.Label();
            this.numChuckZ_Vision = new System.Windows.Forms.NumericUpDown();
            this.lblChuckZ_Down = new System.Windows.Forms.Label();
            this.numChuckZ_Down = new System.Windows.Forms.NumericUpDown();
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
            ((System.ComponentModel.ISupportInitialize)(this.numScanImageCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numScanStepAngle)).BeginInit();
            this.grpMotion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDecel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAccel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVelocity)).BeginInit();
            this.grpTheta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTheta_ScanEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTheta_ScanStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTheta_Home)).BeginInit();
            this.grpCenterR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCenterR_MinCtr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCenterR_Open)).BeginInit();
            this.grpCenterL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCenterL_MinCtr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCenterL_Open)).BeginInit();
            this.grpChuckZ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numChuckZ_Eddy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChuckZ_Vision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChuckZ_Down)).BeginInit();
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
            this.panelLeft.Controls.Add(this.grpMotion);
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
            this.grpChuckZ.Controls.Add(this.numChuckZ_Eddy);
            this.grpChuckZ.Controls.Add(this.lblChuckZ_Vision);
            this.grpChuckZ.Controls.Add(this.numChuckZ_Vision);
            this.grpChuckZ.Controls.Add(this.lblChuckZ_Down);
            this.grpChuckZ.Controls.Add(this.numChuckZ_Down);
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
            // numChuckZ_Down
            //
            this.numChuckZ_Down.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.numChuckZ_Down.DecimalPlaces = 1;
            this.numChuckZ_Down.ForeColor = System.Drawing.Color.White;
            this.numChuckZ_Down.Location = new System.Drawing.Point(10, 45);
            this.numChuckZ_Down.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            this.numChuckZ_Down.Minimum = new decimal(new int[] { 10000000, 0, 0, -2147483648 });
            this.numChuckZ_Down.Name = "numChuckZ_Down";
            this.numChuckZ_Down.Size = new System.Drawing.Size(90, 23);
            this.numChuckZ_Down.TabIndex = 1;
            this.numChuckZ_Down.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numChuckZ_Down.ValueChanged += new System.EventHandler(this.NumericUpDown_ValueChanged);
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
            // numChuckZ_Vision
            //
            this.numChuckZ_Vision.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.numChuckZ_Vision.DecimalPlaces = 1;
            this.numChuckZ_Vision.ForeColor = System.Drawing.Color.White;
            this.numChuckZ_Vision.Location = new System.Drawing.Point(115, 45);
            this.numChuckZ_Vision.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            this.numChuckZ_Vision.Minimum = new decimal(new int[] { 10000000, 0, 0, -2147483648 });
            this.numChuckZ_Vision.Name = "numChuckZ_Vision";
            this.numChuckZ_Vision.Size = new System.Drawing.Size(90, 23);
            this.numChuckZ_Vision.TabIndex = 3;
            this.numChuckZ_Vision.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numChuckZ_Vision.ValueChanged += new System.EventHandler(this.NumericUpDown_ValueChanged);
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
            // numChuckZ_Eddy
            //
            this.numChuckZ_Eddy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.numChuckZ_Eddy.DecimalPlaces = 1;
            this.numChuckZ_Eddy.ForeColor = System.Drawing.Color.White;
            this.numChuckZ_Eddy.Location = new System.Drawing.Point(220, 45);
            this.numChuckZ_Eddy.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            this.numChuckZ_Eddy.Minimum = new decimal(new int[] { 10000000, 0, 0, -2147483648 });
            this.numChuckZ_Eddy.Name = "numChuckZ_Eddy";
            this.numChuckZ_Eddy.Size = new System.Drawing.Size(90, 23);
            this.numChuckZ_Eddy.TabIndex = 5;
            this.numChuckZ_Eddy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numChuckZ_Eddy.ValueChanged += new System.EventHandler(this.NumericUpDown_ValueChanged);
            //
            // grpCenterL
            //
            this.grpCenterL.Controls.Add(this.lblCenterL_MinCtr);
            this.grpCenterL.Controls.Add(this.numCenterL_MinCtr);
            this.grpCenterL.Controls.Add(this.lblCenterL_Open);
            this.grpCenterL.Controls.Add(this.numCenterL_Open);
            this.grpCenterL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(238)))), ((int)(((byte)(144)))));
            this.grpCenterL.Location = new System.Drawing.Point(10, 100);
            this.grpCenterL.Name = "grpCenterL";
            this.grpCenterL.Size = new System.Drawing.Size(320, 70);
            this.grpCenterL.TabIndex = 1;
            this.grpCenterL.TabStop = false;
            this.grpCenterL.Text = "Centering L (Axis 1)";
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
            // numCenterL_Open
            //
            this.numCenterL_Open.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.numCenterL_Open.DecimalPlaces = 1;
            this.numCenterL_Open.ForeColor = System.Drawing.Color.White;
            this.numCenterL_Open.Location = new System.Drawing.Point(10, 40);
            this.numCenterL_Open.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            this.numCenterL_Open.Minimum = new decimal(new int[] { 10000000, 0, 0, -2147483648 });
            this.numCenterL_Open.Name = "numCenterL_Open";
            this.numCenterL_Open.Size = new System.Drawing.Size(90, 23);
            this.numCenterL_Open.TabIndex = 1;
            this.numCenterL_Open.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCenterL_Open.ValueChanged += new System.EventHandler(this.NumericUpDown_ValueChanged);
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
            // numCenterL_MinCtr
            //
            this.numCenterL_MinCtr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.numCenterL_MinCtr.DecimalPlaces = 1;
            this.numCenterL_MinCtr.ForeColor = System.Drawing.Color.White;
            this.numCenterL_MinCtr.Location = new System.Drawing.Point(115, 40);
            this.numCenterL_MinCtr.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            this.numCenterL_MinCtr.Minimum = new decimal(new int[] { 10000000, 0, 0, -2147483648 });
            this.numCenterL_MinCtr.Name = "numCenterL_MinCtr";
            this.numCenterL_MinCtr.Size = new System.Drawing.Size(90, 23);
            this.numCenterL_MinCtr.TabIndex = 3;
            this.numCenterL_MinCtr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCenterL_MinCtr.ValueChanged += new System.EventHandler(this.NumericUpDown_ValueChanged);
            //
            // grpCenterR
            //
            this.grpCenterR.Controls.Add(this.lblCenterR_MinCtr);
            this.grpCenterR.Controls.Add(this.numCenterR_MinCtr);
            this.grpCenterR.Controls.Add(this.lblCenterR_Open);
            this.grpCenterR.Controls.Add(this.numCenterR_Open);
            this.grpCenterR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(200)))), ((int)(((byte)(100)))));
            this.grpCenterR.Location = new System.Drawing.Point(10, 175);
            this.grpCenterR.Name = "grpCenterR";
            this.grpCenterR.Size = new System.Drawing.Size(320, 70);
            this.grpCenterR.TabIndex = 2;
            this.grpCenterR.TabStop = false;
            this.grpCenterR.Text = "Centering R (Axis 2)";
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
            // numCenterR_Open
            //
            this.numCenterR_Open.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.numCenterR_Open.DecimalPlaces = 1;
            this.numCenterR_Open.ForeColor = System.Drawing.Color.White;
            this.numCenterR_Open.Location = new System.Drawing.Point(10, 40);
            this.numCenterR_Open.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            this.numCenterR_Open.Minimum = new decimal(new int[] { 10000000, 0, 0, -2147483648 });
            this.numCenterR_Open.Name = "numCenterR_Open";
            this.numCenterR_Open.Size = new System.Drawing.Size(90, 23);
            this.numCenterR_Open.TabIndex = 1;
            this.numCenterR_Open.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCenterR_Open.ValueChanged += new System.EventHandler(this.NumericUpDown_ValueChanged);
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
            // numCenterR_MinCtr
            //
            this.numCenterR_MinCtr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.numCenterR_MinCtr.DecimalPlaces = 1;
            this.numCenterR_MinCtr.ForeColor = System.Drawing.Color.White;
            this.numCenterR_MinCtr.Location = new System.Drawing.Point(115, 40);
            this.numCenterR_MinCtr.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            this.numCenterR_MinCtr.Minimum = new decimal(new int[] { 10000000, 0, 0, -2147483648 });
            this.numCenterR_MinCtr.Name = "numCenterR_MinCtr";
            this.numCenterR_MinCtr.Size = new System.Drawing.Size(90, 23);
            this.numCenterR_MinCtr.TabIndex = 3;
            this.numCenterR_MinCtr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCenterR_MinCtr.ValueChanged += new System.EventHandler(this.NumericUpDown_ValueChanged);
            //
            // grpTheta
            //
            this.grpTheta.Controls.Add(this.lblTheta_ScanEnd);
            this.grpTheta.Controls.Add(this.numTheta_ScanEnd);
            this.grpTheta.Controls.Add(this.lblTheta_ScanStart);
            this.grpTheta.Controls.Add(this.numTheta_ScanStart);
            this.grpTheta.Controls.Add(this.lblTheta_Home);
            this.grpTheta.Controls.Add(this.numTheta_Home);
            this.grpTheta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(182)))), ((int)(((byte)(193)))));
            this.grpTheta.Location = new System.Drawing.Point(10, 250);
            this.grpTheta.Name = "grpTheta";
            this.grpTheta.Size = new System.Drawing.Size(320, 85);
            this.grpTheta.TabIndex = 3;
            this.grpTheta.TabStop = false;
            this.grpTheta.Text = "Theta (Axis 3)";
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
            // numTheta_Home
            //
            this.numTheta_Home.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.numTheta_Home.DecimalPlaces = 2;
            this.numTheta_Home.ForeColor = System.Drawing.Color.White;
            this.numTheta_Home.Location = new System.Drawing.Point(10, 45);
            this.numTheta_Home.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            this.numTheta_Home.Minimum = new decimal(new int[] { 10000000, 0, 0, -2147483648 });
            this.numTheta_Home.Name = "numTheta_Home";
            this.numTheta_Home.Size = new System.Drawing.Size(90, 23);
            this.numTheta_Home.TabIndex = 1;
            this.numTheta_Home.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTheta_Home.ValueChanged += new System.EventHandler(this.NumericUpDown_ValueChanged);
            //
            // lblTheta_ScanStart
            //
            this.lblTheta_ScanStart.AutoSize = true;
            this.lblTheta_ScanStart.ForeColor = System.Drawing.Color.White;
            this.lblTheta_ScanStart.Location = new System.Drawing.Point(115, 25);
            this.lblTheta_ScanStart.Name = "lblTheta_ScanStart";
            this.lblTheta_ScanStart.Size = new System.Drawing.Size(61, 15);
            this.lblTheta_ScanStart.TabIndex = 2;
            this.lblTheta_ScanStart.Text = "ScanStart";
            //
            // numTheta_ScanStart
            //
            this.numTheta_ScanStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.numTheta_ScanStart.DecimalPlaces = 2;
            this.numTheta_ScanStart.ForeColor = System.Drawing.Color.White;
            this.numTheta_ScanStart.Location = new System.Drawing.Point(115, 45);
            this.numTheta_ScanStart.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            this.numTheta_ScanStart.Minimum = new decimal(new int[] { 10000000, 0, 0, -2147483648 });
            this.numTheta_ScanStart.Name = "numTheta_ScanStart";
            this.numTheta_ScanStart.Size = new System.Drawing.Size(90, 23);
            this.numTheta_ScanStart.TabIndex = 3;
            this.numTheta_ScanStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTheta_ScanStart.ValueChanged += new System.EventHandler(this.NumericUpDown_ValueChanged);
            //
            // lblTheta_ScanEnd
            //
            this.lblTheta_ScanEnd.AutoSize = true;
            this.lblTheta_ScanEnd.ForeColor = System.Drawing.Color.White;
            this.lblTheta_ScanEnd.Location = new System.Drawing.Point(220, 25);
            this.lblTheta_ScanEnd.Name = "lblTheta_ScanEnd";
            this.lblTheta_ScanEnd.Size = new System.Drawing.Size(54, 15);
            this.lblTheta_ScanEnd.TabIndex = 4;
            this.lblTheta_ScanEnd.Text = "ScanEnd";
            //
            // numTheta_ScanEnd
            //
            this.numTheta_ScanEnd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.numTheta_ScanEnd.DecimalPlaces = 2;
            this.numTheta_ScanEnd.ForeColor = System.Drawing.Color.White;
            this.numTheta_ScanEnd.Location = new System.Drawing.Point(220, 45);
            this.numTheta_ScanEnd.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            this.numTheta_ScanEnd.Minimum = new decimal(new int[] { 10000000, 0, 0, -2147483648 });
            this.numTheta_ScanEnd.Name = "numTheta_ScanEnd";
            this.numTheta_ScanEnd.Size = new System.Drawing.Size(90, 23);
            this.numTheta_ScanEnd.TabIndex = 5;
            this.numTheta_ScanEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTheta_ScanEnd.ValueChanged += new System.EventHandler(this.NumericUpDown_ValueChanged);
            //
            // grpMotion
            //
            this.grpMotion.Controls.Add(this.lblDecel);
            this.grpMotion.Controls.Add(this.numDecel);
            this.grpMotion.Controls.Add(this.lblAccel);
            this.grpMotion.Controls.Add(this.numAccel);
            this.grpMotion.Controls.Add(this.lblVelocity);
            this.grpMotion.Controls.Add(this.numVelocity);
            this.grpMotion.ForeColor = System.Drawing.Color.Silver;
            this.grpMotion.Location = new System.Drawing.Point(10, 340);
            this.grpMotion.Name = "grpMotion";
            this.grpMotion.Size = new System.Drawing.Size(320, 85);
            this.grpMotion.TabIndex = 4;
            this.grpMotion.TabStop = false;
            this.grpMotion.Text = "Motion Parameters";
            //
            // lblVelocity
            //
            this.lblVelocity.AutoSize = true;
            this.lblVelocity.ForeColor = System.Drawing.Color.White;
            this.lblVelocity.Location = new System.Drawing.Point(10, 25);
            this.lblVelocity.Name = "lblVelocity";
            this.lblVelocity.Size = new System.Drawing.Size(49, 15);
            this.lblVelocity.TabIndex = 0;
            this.lblVelocity.Text = "Velocity";
            //
            // numVelocity
            //
            this.numVelocity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.numVelocity.DecimalPlaces = 1;
            this.numVelocity.ForeColor = System.Drawing.Color.White;
            this.numVelocity.Location = new System.Drawing.Point(10, 45);
            this.numVelocity.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            this.numVelocity.Name = "numVelocity";
            this.numVelocity.Size = new System.Drawing.Size(90, 23);
            this.numVelocity.TabIndex = 1;
            this.numVelocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // lblAccel
            //
            this.lblAccel.AutoSize = true;
            this.lblAccel.ForeColor = System.Drawing.Color.White;
            this.lblAccel.Location = new System.Drawing.Point(115, 25);
            this.lblAccel.Name = "lblAccel";
            this.lblAccel.Size = new System.Drawing.Size(36, 15);
            this.lblAccel.TabIndex = 2;
            this.lblAccel.Text = "Accel";
            //
            // numAccel
            //
            this.numAccel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.numAccel.DecimalPlaces = 1;
            this.numAccel.ForeColor = System.Drawing.Color.White;
            this.numAccel.Location = new System.Drawing.Point(115, 45);
            this.numAccel.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            this.numAccel.Name = "numAccel";
            this.numAccel.Size = new System.Drawing.Size(90, 23);
            this.numAccel.TabIndex = 3;
            this.numAccel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // lblDecel
            //
            this.lblDecel.AutoSize = true;
            this.lblDecel.ForeColor = System.Drawing.Color.White;
            this.lblDecel.Location = new System.Drawing.Point(220, 25);
            this.lblDecel.Name = "lblDecel";
            this.lblDecel.Size = new System.Drawing.Size(36, 15);
            this.lblDecel.TabIndex = 4;
            this.lblDecel.Text = "Decel";
            //
            // numDecel
            //
            this.numDecel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.numDecel.DecimalPlaces = 1;
            this.numDecel.ForeColor = System.Drawing.Color.White;
            this.numDecel.Location = new System.Drawing.Point(220, 45);
            this.numDecel.Maximum = new decimal(new int[] { 10000000, 0, 0, 0 });
            this.numDecel.Name = "numDecel";
            this.numDecel.Size = new System.Drawing.Size(90, 23);
            this.numDecel.TabIndex = 5;
            this.numDecel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // grpVisionScan
            //
            this.grpVisionScan.Controls.Add(this.lblScanImageCount);
            this.grpVisionScan.Controls.Add(this.numScanImageCount);
            this.grpVisionScan.Controls.Add(this.lblScanStepAngle);
            this.grpVisionScan.Controls.Add(this.numScanStepAngle);
            this.grpVisionScan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(0)))));
            this.grpVisionScan.Location = new System.Drawing.Point(10, 430);
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
            // numScanStepAngle
            //
            this.numScanStepAngle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.numScanStepAngle.DecimalPlaces = 1;
            this.numScanStepAngle.ForeColor = System.Drawing.Color.White;
            this.numScanStepAngle.Location = new System.Drawing.Point(10, 40);
            this.numScanStepAngle.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            this.numScanStepAngle.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numScanStepAngle.Name = "numScanStepAngle";
            this.numScanStepAngle.Size = new System.Drawing.Size(90, 23);
            this.numScanStepAngle.TabIndex = 1;
            this.numScanStepAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numScanStepAngle.Value = new decimal(new int[] { 15, 0, 0, 0 });
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
            // numScanImageCount
            //
            this.numScanImageCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.numScanImageCount.ForeColor = System.Drawing.Color.White;
            this.numScanImageCount.Location = new System.Drawing.Point(115, 40);
            this.numScanImageCount.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            this.numScanImageCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numScanImageCount.Name = "numScanImageCount";
            this.numScanImageCount.Size = new System.Drawing.Size(90, 23);
            this.numScanImageCount.TabIndex = 3;
            this.numScanImageCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numScanImageCount.Value = new decimal(new int[] { 24, 0, 0, 0 });
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
            ((System.ComponentModel.ISupportInitialize)(this.numScanImageCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numScanStepAngle)).EndInit();
            this.grpMotion.ResumeLayout(false);
            this.grpMotion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDecel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAccel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVelocity)).EndInit();
            this.grpTheta.ResumeLayout(false);
            this.grpTheta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTheta_ScanEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTheta_ScanStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTheta_Home)).EndInit();
            this.grpCenterR.ResumeLayout(false);
            this.grpCenterR.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCenterR_MinCtr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCenterR_Open)).EndInit();
            this.grpCenterL.ResumeLayout(false);
            this.grpCenterL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCenterL_MinCtr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCenterL_Open)).EndInit();
            this.grpChuckZ.ResumeLayout(false);
            this.grpChuckZ.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numChuckZ_Eddy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChuckZ_Vision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChuckZ_Down)).EndInit();
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
        private System.Windows.Forms.NumericUpDown numChuckZ_Down;
        private System.Windows.Forms.Label lblChuckZ_Vision;
        private System.Windows.Forms.NumericUpDown numChuckZ_Vision;
        private System.Windows.Forms.Label lblChuckZ_Eddy;
        private System.Windows.Forms.NumericUpDown numChuckZ_Eddy;

        // Centering L
        private System.Windows.Forms.GroupBox grpCenterL;
        private System.Windows.Forms.Label lblCenterL_Open;
        private System.Windows.Forms.NumericUpDown numCenterL_Open;
        private System.Windows.Forms.Label lblCenterL_MinCtr;
        private System.Windows.Forms.NumericUpDown numCenterL_MinCtr;

        // Centering R
        private System.Windows.Forms.GroupBox grpCenterR;
        private System.Windows.Forms.Label lblCenterR_Open;
        private System.Windows.Forms.NumericUpDown numCenterR_Open;
        private System.Windows.Forms.Label lblCenterR_MinCtr;
        private System.Windows.Forms.NumericUpDown numCenterR_MinCtr;

        // Theta
        private System.Windows.Forms.GroupBox grpTheta;
        private System.Windows.Forms.Label lblTheta_Home;
        private System.Windows.Forms.NumericUpDown numTheta_Home;
        private System.Windows.Forms.Label lblTheta_ScanStart;
        private System.Windows.Forms.NumericUpDown numTheta_ScanStart;
        private System.Windows.Forms.Label lblTheta_ScanEnd;
        private System.Windows.Forms.NumericUpDown numTheta_ScanEnd;

        // Motion
        private System.Windows.Forms.GroupBox grpMotion;
        private System.Windows.Forms.Label lblVelocity;
        private System.Windows.Forms.NumericUpDown numVelocity;
        private System.Windows.Forms.Label lblAccel;
        private System.Windows.Forms.NumericUpDown numAccel;
        private System.Windows.Forms.Label lblDecel;
        private System.Windows.Forms.NumericUpDown numDecel;

        // Vision Scan
        private System.Windows.Forms.GroupBox grpVisionScan;
        private System.Windows.Forms.Label lblScanStepAngle;
        private System.Windows.Forms.NumericUpDown numScanStepAngle;
        private System.Windows.Forms.Label lblScanImageCount;
        private System.Windows.Forms.NumericUpDown numScanImageCount;

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
