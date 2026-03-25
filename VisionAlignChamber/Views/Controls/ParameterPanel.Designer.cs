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
            this.grpPNCheck = new System.Windows.Forms.GroupBox();
            this.lblPN_HoldTime = new System.Windows.Forms.Label();
            this.txtPN_HoldTime = new System.Windows.Forms.TextBox();
            this.lblPN_Timeout = new System.Windows.Forms.Label();
            this.txtPN_Timeout = new System.Windows.Forms.TextBox();
            this.lblPN_PollInterval = new System.Windows.Forms.Label();
            this.txtPN_PollInterval = new System.Windows.Forms.TextBox();
            this.grpVisionScan = new System.Windows.Forms.GroupBox();
            this.lblScanImageCount = new System.Windows.Forms.Label();
            this.txtScanImageCount = new System.Windows.Forms.TextBox();
            this.lblScanStepAngle = new System.Windows.Forms.Label();
            this.txtScanStepAngle = new System.Windows.Forms.TextBox();
            this.lblScanWidthOffset = new System.Windows.Forms.Label();
            this.txtScanWidthOffset = new System.Windows.Forms.TextBox();
            this.grpTheta = new System.Windows.Forms.GroupBox();
            this.lblTheta_Home = new System.Windows.Forms.Label();
            this.txtTheta_Home = new System.Windows.Forms.TextBox();
            this.lblTheta_Vel = new System.Windows.Forms.Label();
            this.txtTheta_Vel = new System.Windows.Forms.TextBox();
            this.lblTheta_Acc = new System.Windows.Forms.Label();
            this.txtTheta_Acc = new System.Windows.Forms.TextBox();
            this.lblTheta_Dec = new System.Windows.Forms.Label();
            this.txtTheta_Dec = new System.Windows.Forms.TextBox();
            this.grpCenterR = new System.Windows.Forms.GroupBox();
            this.lblCenterR_MinCtr = new System.Windows.Forms.Label();
            this.txtCenterR_MinCtr = new System.Windows.Forms.TextBox();
            this.lblCenterR_Open = new System.Windows.Forms.Label();
            this.txtCenterR_Open = new System.Windows.Forms.TextBox();
            this.lblCenterR_Vel = new System.Windows.Forms.Label();
            this.txtCenterR_Vel = new System.Windows.Forms.TextBox();
            this.lblCenterR_Acc = new System.Windows.Forms.Label();
            this.txtCenterR_Acc = new System.Windows.Forms.TextBox();
            this.lblCenterR_Dec = new System.Windows.Forms.Label();
            this.txtCenterR_Dec = new System.Windows.Forms.TextBox();
            this.grpCenterL = new System.Windows.Forms.GroupBox();
            this.lblCenterL_MinCtr = new System.Windows.Forms.Label();
            this.txtCenterL_MinCtr = new System.Windows.Forms.TextBox();
            this.lblCenterL_Open = new System.Windows.Forms.Label();
            this.txtCenterL_Open = new System.Windows.Forms.TextBox();
            this.lblCenterL_Vel = new System.Windows.Forms.Label();
            this.txtCenterL_Vel = new System.Windows.Forms.TextBox();
            this.lblCenterL_Acc = new System.Windows.Forms.Label();
            this.txtCenterL_Acc = new System.Windows.Forms.TextBox();
            this.lblCenterL_Dec = new System.Windows.Forms.Label();
            this.txtCenterL_Dec = new System.Windows.Forms.TextBox();
            this.grpChuckZ = new System.Windows.Forms.GroupBox();
            this.lblChuckZ_Vacuum = new System.Windows.Forms.Label();
            this.txtChuckZ_Vacuum = new System.Windows.Forms.TextBox();
            this.lblChuckZ_Vision = new System.Windows.Forms.Label();
            this.txtChuckZ_Vision = new System.Windows.Forms.TextBox();
            this.lblChuckZ_Down = new System.Windows.Forms.Label();
            this.txtChuckZ_Down = new System.Windows.Forms.TextBox();
            this.lblChuckZ_Vel = new System.Windows.Forms.Label();
            this.txtChuckZ_Vel = new System.Windows.Forms.TextBox();
            this.lblChuckZ_Acc = new System.Windows.Forms.Label();
            this.txtChuckZ_Acc = new System.Windows.Forms.TextBox();
            this.lblChuckZ_Dec = new System.Windows.Forms.Label();
            this.txtChuckZ_Dec = new System.Windows.Forms.TextBox();
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
            this.grpPNCheck.SuspendLayout();
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
            this.panelLeft.Controls.Add(this.grpPNCheck);
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
            this.grpChuckZ.Controls.Add(this.lblChuckZ_Vacuum);
            this.grpChuckZ.Controls.Add(this.txtChuckZ_Vacuum);
            this.grpChuckZ.Controls.Add(this.lblChuckZ_Vision);
            this.grpChuckZ.Controls.Add(this.txtChuckZ_Vision);
            this.grpChuckZ.Controls.Add(this.lblChuckZ_Down);
            this.grpChuckZ.Controls.Add(this.txtChuckZ_Down);
            this.grpChuckZ.Controls.Add(this.lblChuckZ_Vel);
            this.grpChuckZ.Controls.Add(this.txtChuckZ_Vel);
            this.grpChuckZ.Controls.Add(this.lblChuckZ_Acc);
            this.grpChuckZ.Controls.Add(this.txtChuckZ_Acc);
            this.grpChuckZ.Controls.Add(this.lblChuckZ_Dec);
            this.grpChuckZ.Controls.Add(this.txtChuckZ_Dec);
            this.grpChuckZ.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(149)))), ((int)(((byte)(237)))));
            this.grpChuckZ.Location = new System.Drawing.Point(10, 10);
            this.grpChuckZ.Name = "grpChuckZ";
            this.grpChuckZ.Size = new System.Drawing.Size(320, 130);
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
            // lblChuckZ_Vacuum
            //
            this.lblChuckZ_Vacuum.AutoSize = true;
            this.lblChuckZ_Vacuum.ForeColor = System.Drawing.Color.White;
            this.lblChuckZ_Vacuum.Location = new System.Drawing.Point(220, 25);
            this.lblChuckZ_Vacuum.Name = "lblChuckZ_Vacuum";
            this.lblChuckZ_Vacuum.Size = new System.Drawing.Size(34, 15);
            this.lblChuckZ_Vacuum.TabIndex = 4;
            this.lblChuckZ_Vacuum.Text = "Vacuum";
            //
            // txtChuckZ_Vacuum
            //
            this.txtChuckZ_Vacuum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtChuckZ_Vacuum.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtChuckZ_Vacuum.ForeColor = System.Drawing.Color.White;
            this.txtChuckZ_Vacuum.Location = new System.Drawing.Point(220, 45);
            this.txtChuckZ_Vacuum.Name = "txtChuckZ_Vacuum";
            this.txtChuckZ_Vacuum.Size = new System.Drawing.Size(90, 22);
            this.txtChuckZ_Vacuum.TabIndex = 5;
            this.txtChuckZ_Vacuum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtChuckZ_Vacuum.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // lblChuckZ_Vel
            //
            this.lblChuckZ_Vel.AutoSize = true;
            this.lblChuckZ_Vel.ForeColor = System.Drawing.Color.LightGray;
            this.lblChuckZ_Vel.Location = new System.Drawing.Point(10, 75);
            this.lblChuckZ_Vel.Name = "lblChuckZ_Vel";
            this.lblChuckZ_Vel.Size = new System.Drawing.Size(22, 15);
            this.lblChuckZ_Vel.TabIndex = 6;
            this.lblChuckZ_Vel.Text = "Vel";
            //
            // txtChuckZ_Vel
            //
            this.txtChuckZ_Vel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtChuckZ_Vel.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtChuckZ_Vel.ForeColor = System.Drawing.Color.White;
            this.txtChuckZ_Vel.Location = new System.Drawing.Point(10, 95);
            this.txtChuckZ_Vel.Name = "txtChuckZ_Vel";
            this.txtChuckZ_Vel.Size = new System.Drawing.Size(90, 22);
            this.txtChuckZ_Vel.TabIndex = 7;
            this.txtChuckZ_Vel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtChuckZ_Vel.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // lblChuckZ_Acc
            //
            this.lblChuckZ_Acc.AutoSize = true;
            this.lblChuckZ_Acc.ForeColor = System.Drawing.Color.LightGray;
            this.lblChuckZ_Acc.Location = new System.Drawing.Point(115, 75);
            this.lblChuckZ_Acc.Name = "lblChuckZ_Acc";
            this.lblChuckZ_Acc.Size = new System.Drawing.Size(25, 15);
            this.lblChuckZ_Acc.TabIndex = 8;
            this.lblChuckZ_Acc.Text = "Acc";
            //
            // txtChuckZ_Acc
            //
            this.txtChuckZ_Acc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtChuckZ_Acc.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtChuckZ_Acc.ForeColor = System.Drawing.Color.White;
            this.txtChuckZ_Acc.Location = new System.Drawing.Point(115, 95);
            this.txtChuckZ_Acc.Name = "txtChuckZ_Acc";
            this.txtChuckZ_Acc.Size = new System.Drawing.Size(90, 22);
            this.txtChuckZ_Acc.TabIndex = 9;
            this.txtChuckZ_Acc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtChuckZ_Acc.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // lblChuckZ_Dec
            //
            this.lblChuckZ_Dec.AutoSize = true;
            this.lblChuckZ_Dec.ForeColor = System.Drawing.Color.LightGray;
            this.lblChuckZ_Dec.Location = new System.Drawing.Point(220, 75);
            this.lblChuckZ_Dec.Name = "lblChuckZ_Dec";
            this.lblChuckZ_Dec.Size = new System.Drawing.Size(26, 15);
            this.lblChuckZ_Dec.TabIndex = 10;
            this.lblChuckZ_Dec.Text = "Dec";
            //
            // txtChuckZ_Dec
            //
            this.txtChuckZ_Dec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtChuckZ_Dec.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtChuckZ_Dec.ForeColor = System.Drawing.Color.White;
            this.txtChuckZ_Dec.Location = new System.Drawing.Point(220, 95);
            this.txtChuckZ_Dec.Name = "txtChuckZ_Dec";
            this.txtChuckZ_Dec.Size = new System.Drawing.Size(90, 22);
            this.txtChuckZ_Dec.TabIndex = 11;
            this.txtChuckZ_Dec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtChuckZ_Dec.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // grpCenterL
            //
            this.grpCenterL.Controls.Add(this.lblCenterL_MinCtr);
            this.grpCenterL.Controls.Add(this.txtCenterL_MinCtr);
            this.grpCenterL.Controls.Add(this.lblCenterL_Open);
            this.grpCenterL.Controls.Add(this.txtCenterL_Open);
            this.grpCenterL.Controls.Add(this.lblCenterL_Vel);
            this.grpCenterL.Controls.Add(this.txtCenterL_Vel);
            this.grpCenterL.Controls.Add(this.lblCenterL_Acc);
            this.grpCenterL.Controls.Add(this.txtCenterL_Acc);
            this.grpCenterL.Controls.Add(this.lblCenterL_Dec);
            this.grpCenterL.Controls.Add(this.txtCenterL_Dec);
            this.grpCenterL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(238)))), ((int)(((byte)(144)))));
            this.grpCenterL.Location = new System.Drawing.Point(10, 145);
            this.grpCenterL.Name = "grpCenterL";
            this.grpCenterL.Size = new System.Drawing.Size(320, 115);
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
            // lblCenterL_Vel
            //
            this.lblCenterL_Vel.AutoSize = true;
            this.lblCenterL_Vel.ForeColor = System.Drawing.Color.LightGray;
            this.lblCenterL_Vel.Location = new System.Drawing.Point(10, 65);
            this.lblCenterL_Vel.Name = "lblCenterL_Vel";
            this.lblCenterL_Vel.Size = new System.Drawing.Size(22, 15);
            this.lblCenterL_Vel.TabIndex = 4;
            this.lblCenterL_Vel.Text = "Vel";
            //
            // txtCenterL_Vel
            //
            this.txtCenterL_Vel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtCenterL_Vel.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtCenterL_Vel.ForeColor = System.Drawing.Color.White;
            this.txtCenterL_Vel.Location = new System.Drawing.Point(10, 85);
            this.txtCenterL_Vel.Name = "txtCenterL_Vel";
            this.txtCenterL_Vel.Size = new System.Drawing.Size(90, 22);
            this.txtCenterL_Vel.TabIndex = 5;
            this.txtCenterL_Vel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCenterL_Vel.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // lblCenterL_Acc
            //
            this.lblCenterL_Acc.AutoSize = true;
            this.lblCenterL_Acc.ForeColor = System.Drawing.Color.LightGray;
            this.lblCenterL_Acc.Location = new System.Drawing.Point(115, 65);
            this.lblCenterL_Acc.Name = "lblCenterL_Acc";
            this.lblCenterL_Acc.Size = new System.Drawing.Size(25, 15);
            this.lblCenterL_Acc.TabIndex = 6;
            this.lblCenterL_Acc.Text = "Acc";
            //
            // txtCenterL_Acc
            //
            this.txtCenterL_Acc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtCenterL_Acc.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtCenterL_Acc.ForeColor = System.Drawing.Color.White;
            this.txtCenterL_Acc.Location = new System.Drawing.Point(115, 85);
            this.txtCenterL_Acc.Name = "txtCenterL_Acc";
            this.txtCenterL_Acc.Size = new System.Drawing.Size(90, 22);
            this.txtCenterL_Acc.TabIndex = 7;
            this.txtCenterL_Acc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCenterL_Acc.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // lblCenterL_Dec
            //
            this.lblCenterL_Dec.AutoSize = true;
            this.lblCenterL_Dec.ForeColor = System.Drawing.Color.LightGray;
            this.lblCenterL_Dec.Location = new System.Drawing.Point(220, 65);
            this.lblCenterL_Dec.Name = "lblCenterL_Dec";
            this.lblCenterL_Dec.Size = new System.Drawing.Size(26, 15);
            this.lblCenterL_Dec.TabIndex = 8;
            this.lblCenterL_Dec.Text = "Dec";
            //
            // txtCenterL_Dec
            //
            this.txtCenterL_Dec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtCenterL_Dec.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtCenterL_Dec.ForeColor = System.Drawing.Color.White;
            this.txtCenterL_Dec.Location = new System.Drawing.Point(220, 85);
            this.txtCenterL_Dec.Name = "txtCenterL_Dec";
            this.txtCenterL_Dec.Size = new System.Drawing.Size(90, 22);
            this.txtCenterL_Dec.TabIndex = 9;
            this.txtCenterL_Dec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCenterL_Dec.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // grpCenterR
            //
            this.grpCenterR.Controls.Add(this.lblCenterR_MinCtr);
            this.grpCenterR.Controls.Add(this.txtCenterR_MinCtr);
            this.grpCenterR.Controls.Add(this.lblCenterR_Open);
            this.grpCenterR.Controls.Add(this.txtCenterR_Open);
            this.grpCenterR.Controls.Add(this.lblCenterR_Vel);
            this.grpCenterR.Controls.Add(this.txtCenterR_Vel);
            this.grpCenterR.Controls.Add(this.lblCenterR_Acc);
            this.grpCenterR.Controls.Add(this.txtCenterR_Acc);
            this.grpCenterR.Controls.Add(this.lblCenterR_Dec);
            this.grpCenterR.Controls.Add(this.txtCenterR_Dec);
            this.grpCenterR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(200)))), ((int)(((byte)(100)))));
            this.grpCenterR.Location = new System.Drawing.Point(10, 265);
            this.grpCenterR.Name = "grpCenterR";
            this.grpCenterR.Size = new System.Drawing.Size(320, 115);
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
            // lblCenterR_Vel
            //
            this.lblCenterR_Vel.AutoSize = true;
            this.lblCenterR_Vel.ForeColor = System.Drawing.Color.LightGray;
            this.lblCenterR_Vel.Location = new System.Drawing.Point(10, 65);
            this.lblCenterR_Vel.Name = "lblCenterR_Vel";
            this.lblCenterR_Vel.Size = new System.Drawing.Size(22, 15);
            this.lblCenterR_Vel.TabIndex = 4;
            this.lblCenterR_Vel.Text = "Vel";
            //
            // txtCenterR_Vel
            //
            this.txtCenterR_Vel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtCenterR_Vel.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtCenterR_Vel.ForeColor = System.Drawing.Color.White;
            this.txtCenterR_Vel.Location = new System.Drawing.Point(10, 85);
            this.txtCenterR_Vel.Name = "txtCenterR_Vel";
            this.txtCenterR_Vel.Size = new System.Drawing.Size(90, 22);
            this.txtCenterR_Vel.TabIndex = 5;
            this.txtCenterR_Vel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCenterR_Vel.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // lblCenterR_Acc
            //
            this.lblCenterR_Acc.AutoSize = true;
            this.lblCenterR_Acc.ForeColor = System.Drawing.Color.LightGray;
            this.lblCenterR_Acc.Location = new System.Drawing.Point(115, 65);
            this.lblCenterR_Acc.Name = "lblCenterR_Acc";
            this.lblCenterR_Acc.Size = new System.Drawing.Size(25, 15);
            this.lblCenterR_Acc.TabIndex = 6;
            this.lblCenterR_Acc.Text = "Acc";
            //
            // txtCenterR_Acc
            //
            this.txtCenterR_Acc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtCenterR_Acc.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtCenterR_Acc.ForeColor = System.Drawing.Color.White;
            this.txtCenterR_Acc.Location = new System.Drawing.Point(115, 85);
            this.txtCenterR_Acc.Name = "txtCenterR_Acc";
            this.txtCenterR_Acc.Size = new System.Drawing.Size(90, 22);
            this.txtCenterR_Acc.TabIndex = 7;
            this.txtCenterR_Acc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCenterR_Acc.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // lblCenterR_Dec
            //
            this.lblCenterR_Dec.AutoSize = true;
            this.lblCenterR_Dec.ForeColor = System.Drawing.Color.LightGray;
            this.lblCenterR_Dec.Location = new System.Drawing.Point(220, 65);
            this.lblCenterR_Dec.Name = "lblCenterR_Dec";
            this.lblCenterR_Dec.Size = new System.Drawing.Size(26, 15);
            this.lblCenterR_Dec.TabIndex = 8;
            this.lblCenterR_Dec.Text = "Dec";
            //
            // txtCenterR_Dec
            //
            this.txtCenterR_Dec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtCenterR_Dec.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtCenterR_Dec.ForeColor = System.Drawing.Color.White;
            this.txtCenterR_Dec.Location = new System.Drawing.Point(220, 85);
            this.txtCenterR_Dec.Name = "txtCenterR_Dec";
            this.txtCenterR_Dec.Size = new System.Drawing.Size(90, 22);
            this.txtCenterR_Dec.TabIndex = 9;
            this.txtCenterR_Dec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCenterR_Dec.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // grpTheta
            //
            this.grpTheta.Controls.Add(this.lblTheta_Home);
            this.grpTheta.Controls.Add(this.txtTheta_Home);
            this.grpTheta.Controls.Add(this.lblTheta_Vel);
            this.grpTheta.Controls.Add(this.txtTheta_Vel);
            this.grpTheta.Controls.Add(this.lblTheta_Acc);
            this.grpTheta.Controls.Add(this.txtTheta_Acc);
            this.grpTheta.Controls.Add(this.lblTheta_Dec);
            this.grpTheta.Controls.Add(this.txtTheta_Dec);
            this.grpTheta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(182)))), ((int)(((byte)(193)))));
            this.grpTheta.Location = new System.Drawing.Point(10, 385);
            this.grpTheta.Name = "grpTheta";
            this.grpTheta.Size = new System.Drawing.Size(320, 115);
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
            // lblTheta_Vel
            //
            this.lblTheta_Vel.AutoSize = true;
            this.lblTheta_Vel.ForeColor = System.Drawing.Color.LightGray;
            this.lblTheta_Vel.Location = new System.Drawing.Point(10, 65);
            this.lblTheta_Vel.Name = "lblTheta_Vel";
            this.lblTheta_Vel.Size = new System.Drawing.Size(22, 15);
            this.lblTheta_Vel.TabIndex = 2;
            this.lblTheta_Vel.Text = "Vel";
            //
            // txtTheta_Vel
            //
            this.txtTheta_Vel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtTheta_Vel.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtTheta_Vel.ForeColor = System.Drawing.Color.White;
            this.txtTheta_Vel.Location = new System.Drawing.Point(10, 85);
            this.txtTheta_Vel.Name = "txtTheta_Vel";
            this.txtTheta_Vel.Size = new System.Drawing.Size(90, 22);
            this.txtTheta_Vel.TabIndex = 3;
            this.txtTheta_Vel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTheta_Vel.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // lblTheta_Acc
            //
            this.lblTheta_Acc.AutoSize = true;
            this.lblTheta_Acc.ForeColor = System.Drawing.Color.LightGray;
            this.lblTheta_Acc.Location = new System.Drawing.Point(115, 65);
            this.lblTheta_Acc.Name = "lblTheta_Acc";
            this.lblTheta_Acc.Size = new System.Drawing.Size(25, 15);
            this.lblTheta_Acc.TabIndex = 4;
            this.lblTheta_Acc.Text = "Acc";
            //
            // txtTheta_Acc
            //
            this.txtTheta_Acc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtTheta_Acc.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtTheta_Acc.ForeColor = System.Drawing.Color.White;
            this.txtTheta_Acc.Location = new System.Drawing.Point(115, 85);
            this.txtTheta_Acc.Name = "txtTheta_Acc";
            this.txtTheta_Acc.Size = new System.Drawing.Size(90, 22);
            this.txtTheta_Acc.TabIndex = 5;
            this.txtTheta_Acc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTheta_Acc.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // lblTheta_Dec
            //
            this.lblTheta_Dec.AutoSize = true;
            this.lblTheta_Dec.ForeColor = System.Drawing.Color.LightGray;
            this.lblTheta_Dec.Location = new System.Drawing.Point(220, 65);
            this.lblTheta_Dec.Name = "lblTheta_Dec";
            this.lblTheta_Dec.Size = new System.Drawing.Size(26, 15);
            this.lblTheta_Dec.TabIndex = 6;
            this.lblTheta_Dec.Text = "Dec";
            //
            // txtTheta_Dec
            //
            this.txtTheta_Dec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtTheta_Dec.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtTheta_Dec.ForeColor = System.Drawing.Color.White;
            this.txtTheta_Dec.Location = new System.Drawing.Point(220, 85);
            this.txtTheta_Dec.Name = "txtTheta_Dec";
            this.txtTheta_Dec.Size = new System.Drawing.Size(90, 22);
            this.txtTheta_Dec.TabIndex = 7;
            this.txtTheta_Dec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTheta_Dec.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // grpVisionScan
            //
            this.grpVisionScan.Controls.Add(this.lblScanWidthOffset);
            this.grpVisionScan.Controls.Add(this.txtScanWidthOffset);
            this.grpVisionScan.Controls.Add(this.lblScanImageCount);
            this.grpVisionScan.Controls.Add(this.txtScanImageCount);
            this.grpVisionScan.Controls.Add(this.lblScanStepAngle);
            this.grpVisionScan.Controls.Add(this.txtScanStepAngle);
            this.grpVisionScan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(0)))));
            this.grpVisionScan.Location = new System.Drawing.Point(10, 505);
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
            // lblScanWidthOffset
            //
            this.lblScanWidthOffset.AutoSize = true;
            this.lblScanWidthOffset.ForeColor = System.Drawing.Color.White;
            this.lblScanWidthOffset.Location = new System.Drawing.Point(220, 20);
            this.lblScanWidthOffset.Name = "lblScanWidthOffset";
            this.lblScanWidthOffset.Size = new System.Drawing.Size(80, 15);
            this.lblScanWidthOffset.TabIndex = 4;
            this.lblScanWidthOffset.Text = "Width Offset";
            //
            // txtScanWidthOffset
            //
            this.txtScanWidthOffset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtScanWidthOffset.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtScanWidthOffset.ForeColor = System.Drawing.Color.White;
            this.txtScanWidthOffset.Location = new System.Drawing.Point(220, 40);
            this.txtScanWidthOffset.Name = "txtScanWidthOffset";
            this.txtScanWidthOffset.Size = new System.Drawing.Size(90, 22);
            this.txtScanWidthOffset.TabIndex = 5;
            this.txtScanWidthOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtScanWidthOffset.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            //
            // grpPNCheck
            //
            this.grpPNCheck.Controls.Add(this.lblPN_HoldTime);
            this.grpPNCheck.Controls.Add(this.txtPN_HoldTime);
            this.grpPNCheck.Controls.Add(this.lblPN_Timeout);
            this.grpPNCheck.Controls.Add(this.txtPN_Timeout);
            this.grpPNCheck.Controls.Add(this.lblPN_PollInterval);
            this.grpPNCheck.Controls.Add(this.txtPN_PollInterval);
            this.grpPNCheck.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(216)))), ((int)(((byte)(230)))));
            this.grpPNCheck.Location = new System.Drawing.Point(10, 580);
            this.grpPNCheck.Name = "grpPNCheck";
            this.grpPNCheck.Size = new System.Drawing.Size(320, 70);
            this.grpPNCheck.TabIndex = 6;
            this.grpPNCheck.TabStop = false;
            this.grpPNCheck.Text = "PN Check (ms)";
            //
            // lblPN_HoldTime
            //
            this.lblPN_HoldTime.AutoSize = true;
            this.lblPN_HoldTime.ForeColor = System.Drawing.Color.White;
            this.lblPN_HoldTime.Location = new System.Drawing.Point(10, 20);
            this.lblPN_HoldTime.Name = "lblPN_HoldTime";
            this.lblPN_HoldTime.Size = new System.Drawing.Size(59, 15);
            this.lblPN_HoldTime.TabIndex = 0;
            this.lblPN_HoldTime.Text = "HoldTime";
            //
            // txtPN_HoldTime
            //
            this.txtPN_HoldTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtPN_HoldTime.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtPN_HoldTime.ForeColor = System.Drawing.Color.White;
            this.txtPN_HoldTime.Location = new System.Drawing.Point(10, 40);
            this.txtPN_HoldTime.Name = "txtPN_HoldTime";
            this.txtPN_HoldTime.Size = new System.Drawing.Size(90, 22);
            this.txtPN_HoldTime.TabIndex = 1;
            this.txtPN_HoldTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // lblPN_Timeout
            //
            this.lblPN_Timeout.AutoSize = true;
            this.lblPN_Timeout.ForeColor = System.Drawing.Color.White;
            this.lblPN_Timeout.Location = new System.Drawing.Point(115, 20);
            this.lblPN_Timeout.Name = "lblPN_Timeout";
            this.lblPN_Timeout.Size = new System.Drawing.Size(51, 15);
            this.lblPN_Timeout.TabIndex = 2;
            this.lblPN_Timeout.Text = "Timeout";
            //
            // txtPN_Timeout
            //
            this.txtPN_Timeout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtPN_Timeout.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtPN_Timeout.ForeColor = System.Drawing.Color.White;
            this.txtPN_Timeout.Location = new System.Drawing.Point(115, 40);
            this.txtPN_Timeout.Name = "txtPN_Timeout";
            this.txtPN_Timeout.Size = new System.Drawing.Size(90, 22);
            this.txtPN_Timeout.TabIndex = 3;
            this.txtPN_Timeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // lblPN_PollInterval
            //
            this.lblPN_PollInterval.AutoSize = true;
            this.lblPN_PollInterval.ForeColor = System.Drawing.Color.White;
            this.lblPN_PollInterval.Location = new System.Drawing.Point(220, 20);
            this.lblPN_PollInterval.Name = "lblPN_PollInterval";
            this.lblPN_PollInterval.Size = new System.Drawing.Size(72, 15);
            this.lblPN_PollInterval.TabIndex = 4;
            this.lblPN_PollInterval.Text = "PollInterval";
            //
            // txtPN_PollInterval
            //
            this.txtPN_PollInterval.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.txtPN_PollInterval.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtPN_PollInterval.ForeColor = System.Drawing.Color.White;
            this.txtPN_PollInterval.Location = new System.Drawing.Point(220, 40);
            this.txtPN_PollInterval.Name = "txtPN_PollInterval";
            this.txtPN_PollInterval.Size = new System.Drawing.Size(90, 22);
            this.txtPN_PollInterval.TabIndex = 5;
            this.txtPN_PollInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.grpPNCheck.ResumeLayout(false);
            this.grpPNCheck.PerformLayout();
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
        private System.Windows.Forms.Label lblChuckZ_Vacuum;
        private System.Windows.Forms.TextBox txtChuckZ_Vacuum;
        private System.Windows.Forms.Label lblChuckZ_Vel;
        private System.Windows.Forms.TextBox txtChuckZ_Vel;
        private System.Windows.Forms.Label lblChuckZ_Acc;
        private System.Windows.Forms.TextBox txtChuckZ_Acc;
        private System.Windows.Forms.Label lblChuckZ_Dec;
        private System.Windows.Forms.TextBox txtChuckZ_Dec;

        // Centering L
        private System.Windows.Forms.GroupBox grpCenterL;
        private System.Windows.Forms.Label lblCenterL_Open;
        private System.Windows.Forms.TextBox txtCenterL_Open;
        private System.Windows.Forms.Label lblCenterL_MinCtr;
        private System.Windows.Forms.TextBox txtCenterL_MinCtr;
        private System.Windows.Forms.Label lblCenterL_Vel;
        private System.Windows.Forms.TextBox txtCenterL_Vel;
        private System.Windows.Forms.Label lblCenterL_Acc;
        private System.Windows.Forms.TextBox txtCenterL_Acc;
        private System.Windows.Forms.Label lblCenterL_Dec;
        private System.Windows.Forms.TextBox txtCenterL_Dec;

        // Centering R
        private System.Windows.Forms.GroupBox grpCenterR;
        private System.Windows.Forms.Label lblCenterR_Open;
        private System.Windows.Forms.TextBox txtCenterR_Open;
        private System.Windows.Forms.Label lblCenterR_MinCtr;
        private System.Windows.Forms.TextBox txtCenterR_MinCtr;
        private System.Windows.Forms.Label lblCenterR_Vel;
        private System.Windows.Forms.TextBox txtCenterR_Vel;
        private System.Windows.Forms.Label lblCenterR_Acc;
        private System.Windows.Forms.TextBox txtCenterR_Acc;
        private System.Windows.Forms.Label lblCenterR_Dec;
        private System.Windows.Forms.TextBox txtCenterR_Dec;

        // Theta
        private System.Windows.Forms.GroupBox grpTheta;
        private System.Windows.Forms.Label lblTheta_Home;
        private System.Windows.Forms.TextBox txtTheta_Home;
        private System.Windows.Forms.Label lblTheta_Vel;
        private System.Windows.Forms.TextBox txtTheta_Vel;
        private System.Windows.Forms.Label lblTheta_Acc;
        private System.Windows.Forms.TextBox txtTheta_Acc;
        private System.Windows.Forms.Label lblTheta_Dec;
        private System.Windows.Forms.TextBox txtTheta_Dec;

        // Vision Scan
        private System.Windows.Forms.GroupBox grpVisionScan;
        private System.Windows.Forms.Label lblScanStepAngle;
        private System.Windows.Forms.TextBox txtScanStepAngle;
        private System.Windows.Forms.Label lblScanImageCount;
        private System.Windows.Forms.TextBox txtScanImageCount;
        private System.Windows.Forms.Label lblScanWidthOffset;
        private System.Windows.Forms.TextBox txtScanWidthOffset;

        // PN Check
        private System.Windows.Forms.GroupBox grpPNCheck;
        private System.Windows.Forms.Label lblPN_HoldTime;
        private System.Windows.Forms.TextBox txtPN_HoldTime;
        private System.Windows.Forms.Label lblPN_Timeout;
        private System.Windows.Forms.TextBox txtPN_Timeout;
        private System.Windows.Forms.Label lblPN_PollInterval;
        private System.Windows.Forms.TextBox txtPN_PollInterval;

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
