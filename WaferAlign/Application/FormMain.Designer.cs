namespace eMotion
{
    partial class FormMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.groupRun = new System.Windows.Forms.GroupBox();
            this.textRunStep = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textSetRep = new System.Windows.Forms.TextBox();
            this.textRun = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textRunRep = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textSet = new System.Windows.Forms.TextBox();
            this.textPos = new System.Windows.Forms.TextBox();
            this.buttonStopRun = new System.Windows.Forms.Button();
            this.buttonStartRun = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.checkOpenCV = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonTrig = new System.Windows.Forms.Button();
            this.buttonInit = new System.Windows.Forms.Button();
            this.buttonActive = new System.Windows.Forms.Button();
            this.checkCenter = new System.Windows.Forms.CheckBox();
            this.buttonImageAdd = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.listInfo = new System.Windows.Forms.ListView();
            this.buttonImageRemove = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.buttonNotch = new System.Windows.Forms.Button();
            this.buttonTest = new System.Windows.Forms.Button();
            this.buttonNotchImg = new System.Windows.Forms.Button();
            this.buttonAlignImg = new System.Windows.Forms.Button();
            this.checkFlat = new System.Windows.Forms.CheckBox();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonMotorFree = new System.Windows.Forms.Button();
            this.textAccel = new System.Windows.Forms.TextBox();
            this.button19 = new System.Windows.Forms.Button();
            this.textVelocity = new System.Windows.Forms.TextBox();
            this.button18 = new System.Windows.Forms.Button();
            this.textSpeed = new System.Windows.Forms.TextBox();
            this.button17 = new System.Windows.Forms.Button();
            this.textDelay = new System.Windows.Forms.TextBox();
            this.button16 = new System.Windows.Forms.Button();
            this.textCount = new System.Windows.Forms.TextBox();
            this.button15 = new System.Windows.Forms.Button();
            this.buttonMotorSet = new System.Windows.Forms.Button();
            this.buttonMotorRun = new System.Windows.Forms.Button();
            this.buttonMotorStop = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.textLight = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonLight = new System.Windows.Forms.Button();
            this.textExposure = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonExposure = new System.Windows.Forms.Button();
            this.checkZoom = new System.Windows.Forms.CheckBox();
            this.checkTrigLive = new System.Windows.Forms.CheckBox();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.buttonInitGrid = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSaveGrid = new System.Windows.Forms.Button();
            this.groupRun.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupRun
            // 
            this.groupRun.Controls.Add(this.textRunStep);
            this.groupRun.Controls.Add(this.label4);
            this.groupRun.Controls.Add(this.label2);
            this.groupRun.Controls.Add(this.textSetRep);
            this.groupRun.Controls.Add(this.textRun);
            this.groupRun.Controls.Add(this.label9);
            this.groupRun.Controls.Add(this.label8);
            this.groupRun.Controls.Add(this.textRunRep);
            this.groupRun.Controls.Add(this.label6);
            this.groupRun.Controls.Add(this.textSet);
            this.groupRun.Controls.Add(this.textPos);
            this.groupRun.Controls.Add(this.buttonStopRun);
            this.groupRun.Controls.Add(this.buttonStartRun);
            this.groupRun.Controls.Add(this.label1);
            this.groupRun.Location = new System.Drawing.Point(820, 366);
            this.groupRun.Name = "groupRun";
            this.groupRun.Size = new System.Drawing.Size(225, 165);
            this.groupRun.TabIndex = 23;
            this.groupRun.TabStop = false;
            this.groupRun.Text = "Running";
            // 
            // textRunStep
            // 
            this.textRunStep.BackColor = System.Drawing.Color.CornflowerBlue;
            this.textRunStep.Location = new System.Drawing.Point(165, 80);
            this.textRunStep.Name = "textRunStep";
            this.textRunStep.ReadOnly = true;
            this.textRunStep.Size = new System.Drawing.Size(50, 21);
            this.textRunStep.TabIndex = 37;
            this.textRunStep.Text = "0";
            this.textRunStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(115, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 12);
            this.label4.TabIndex = 38;
            this.label4.Text = "runStep";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 12);
            this.label2.TabIndex = 36;
            this.label2.Text = "SetRep";
            // 
            // textSetRep
            // 
            this.textSetRep.Location = new System.Drawing.Point(60, 50);
            this.textSetRep.Name = "textSetRep";
            this.textSetRep.ReadOnly = true;
            this.textSetRep.Size = new System.Drawing.Size(50, 21);
            this.textSetRep.TabIndex = 35;
            this.textSetRep.Text = "5";
            this.textSetRep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textRun
            // 
            this.textRun.BackColor = System.Drawing.Color.CornflowerBlue;
            this.textRun.Location = new System.Drawing.Point(165, 20);
            this.textRun.Name = "textRun";
            this.textRun.ReadOnly = true;
            this.textRun.Size = new System.Drawing.Size(50, 21);
            this.textRun.TabIndex = 33;
            this.textRun.Text = "0";
            this.textRun.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(115, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 12);
            this.label9.TabIndex = 34;
            this.label9.Text = "runCnt";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(115, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 12);
            this.label8.TabIndex = 32;
            this.label8.Text = "runRep";
            // 
            // textRunRep
            // 
            this.textRunRep.BackColor = System.Drawing.Color.CornflowerBlue;
            this.textRunRep.Location = new System.Drawing.Point(165, 50);
            this.textRunRep.Name = "textRunRep";
            this.textRunRep.ReadOnly = true;
            this.textRunRep.Size = new System.Drawing.Size(50, 21);
            this.textRunRep.TabIndex = 31;
            this.textRunRep.Text = "0";
            this.textRunRep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 29;
            this.label6.Text = "setCnt";
            // 
            // textSet
            // 
            this.textSet.BackColor = System.Drawing.Color.Khaki;
            this.textSet.Location = new System.Drawing.Point(60, 20);
            this.textSet.Name = "textSet";
            this.textSet.ReadOnly = true;
            this.textSet.Size = new System.Drawing.Size(50, 21);
            this.textSet.TabIndex = 25;
            this.textSet.Text = "26";
            this.textSet.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textPos
            // 
            this.textPos.BackColor = System.Drawing.Color.Khaki;
            this.textPos.Location = new System.Drawing.Point(60, 80);
            this.textPos.Name = "textPos";
            this.textPos.ReadOnly = true;
            this.textPos.Size = new System.Drawing.Size(50, 21);
            this.textPos.TabIndex = 28;
            this.textPos.Text = "14";
            this.textPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonStopRun
            // 
            this.buttonStopRun.Location = new System.Drawing.Point(115, 115);
            this.buttonStopRun.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonStopRun.Name = "buttonStopRun";
            this.buttonStopRun.Size = new System.Drawing.Size(100, 40);
            this.buttonStopRun.TabIndex = 24;
            this.buttonStopRun.Text = "Stop";
            this.buttonStopRun.UseVisualStyleBackColor = true;
            this.buttonStopRun.Click += new System.EventHandler(this.buttonStopRun_Click);
            // 
            // buttonStartRun
            // 
            this.buttonStartRun.Location = new System.Drawing.Point(10, 115);
            this.buttonStartRun.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonStartRun.Name = "buttonStartRun";
            this.buttonStartRun.Size = new System.Drawing.Size(100, 40);
            this.buttonStartRun.TabIndex = 23;
            this.buttonStartRun.Text = "Start";
            this.buttonStartRun.UseVisualStyleBackColor = true;
            this.buttonStartRun.Click += new System.EventHandler(this.buttonStartRun_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 12);
            this.label1.TabIndex = 27;
            this.label1.Text = "Deg";
            // 
            // checkOpenCV
            // 
            this.checkOpenCV.AutoSize = true;
            this.checkOpenCV.Checked = true;
            this.checkOpenCV.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkOpenCV.Location = new System.Drawing.Point(130, 48);
            this.checkOpenCV.Name = "checkOpenCV";
            this.checkOpenCV.Size = new System.Drawing.Size(71, 16);
            this.checkOpenCV.TabIndex = 30;
            this.checkOpenCV.Text = "OpenCV";
            this.checkOpenCV.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonSave);
            this.groupBox3.Controls.Add(this.buttonOpen);
            this.groupBox3.Controls.Add(this.buttonTrig);
            this.groupBox3.Controls.Add(this.buttonInit);
            this.groupBox3.Controls.Add(this.buttonActive);
            this.groupBox3.Location = new System.Drawing.Point(820, 195);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Size = new System.Drawing.Size(225, 160);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Control";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(115, 111);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(100, 40);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "File Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(10, 111);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(100, 40);
            this.buttonOpen.TabIndex = 1;
            this.buttonOpen.Text = "File Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.button9_Click);
            // 
            // buttonTrig
            // 
            this.buttonTrig.Location = new System.Drawing.Point(10, 65);
            this.buttonTrig.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonTrig.Name = "buttonTrig";
            this.buttonTrig.Size = new System.Drawing.Size(100, 40);
            this.buttonTrig.TabIndex = 3;
            this.buttonTrig.Text = "Trigger";
            this.buttonTrig.UseVisualStyleBackColor = true;
            this.buttonTrig.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonInit
            // 
            this.buttonInit.Location = new System.Drawing.Point(10, 20);
            this.buttonInit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonInit.Name = "buttonInit";
            this.buttonInit.Size = new System.Drawing.Size(100, 40);
            this.buttonInit.TabIndex = 0;
            this.buttonInit.Text = "Open";
            this.buttonInit.UseVisualStyleBackColor = true;
            this.buttonInit.Click += new System.EventHandler(this.btn_Open1_Click);
            // 
            // buttonActive
            // 
            this.buttonActive.Location = new System.Drawing.Point(115, 20);
            this.buttonActive.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonActive.Name = "buttonActive";
            this.buttonActive.Size = new System.Drawing.Size(100, 40);
            this.buttonActive.TabIndex = 1;
            this.buttonActive.Text = "Active";
            this.buttonActive.UseVisualStyleBackColor = true;
            this.buttonActive.Click += new System.EventHandler(this.btn_Start1_Click);
            // 
            // checkCenter
            // 
            this.checkCenter.AutoSize = true;
            this.checkCenter.Checked = true;
            this.checkCenter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkCenter.Font = new System.Drawing.Font("굴림", 11F);
            this.checkCenter.Location = new System.Drawing.Point(10, 45);
            this.checkCenter.Name = "checkCenter";
            this.checkCenter.Size = new System.Drawing.Size(100, 19);
            this.checkCenter.TabIndex = 8;
            this.checkCenter.Text = "Center Line";
            this.checkCenter.UseVisualStyleBackColor = true;
            // 
            // buttonImageAdd
            // 
            this.buttonImageAdd.Location = new System.Drawing.Point(10, 20);
            this.buttonImageAdd.Name = "buttonImageAdd";
            this.buttonImageAdd.Size = new System.Drawing.Size(100, 40);
            this.buttonImageAdd.TabIndex = 19;
            this.buttonImageAdd.Text = "Open Images";
            this.buttonImageAdd.UseVisualStyleBackColor = true;
            this.buttonImageAdd.Click += new System.EventHandler(this.buttonImageAdd_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.listInfo);
            this.groupBox7.Location = new System.Drawing.Point(1060, 10);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(225, 220);
            this.groupBox7.TabIndex = 29;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Result Data";
            // 
            // listInfo
            // 
            this.listInfo.BackColor = System.Drawing.Color.NavajoWhite;
            this.listInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listInfo.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listInfo.HideSelection = false;
            this.listInfo.Location = new System.Drawing.Point(10, 15);
            this.listInfo.Name = "listInfo";
            this.listInfo.Size = new System.Drawing.Size(205, 199);
            this.listInfo.TabIndex = 0;
            this.listInfo.UseCompatibleStateImageBehavior = false;
            // 
            // buttonImageRemove
            // 
            this.buttonImageRemove.Location = new System.Drawing.Point(116, 20);
            this.buttonImageRemove.Name = "buttonImageRemove";
            this.buttonImageRemove.Size = new System.Drawing.Size(100, 40);
            this.buttonImageRemove.TabIndex = 20;
            this.buttonImageRemove.Text = "Image Clear";
            this.buttonImageRemove.UseVisualStyleBackColor = true;
            this.buttonImageRemove.Click += new System.EventHandler(this.buttonImageRemove_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.buttonNotch);
            this.groupBox5.Controls.Add(this.buttonTest);
            this.groupBox5.Controls.Add(this.buttonNotchImg);
            this.groupBox5.Controls.Add(this.buttonAlignImg);
            this.groupBox5.Location = new System.Drawing.Point(1060, 240);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(225, 116);
            this.groupBox5.TabIndex = 28;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Image Processing";
            // 
            // buttonNotch
            // 
            this.buttonNotch.Location = new System.Drawing.Point(10, 21);
            this.buttonNotch.Name = "buttonNotch";
            this.buttonNotch.Size = new System.Drawing.Size(100, 40);
            this.buttonNotch.TabIndex = 13;
            this.buttonNotch.Text = "Test Start";
            this.buttonNotch.UseVisualStyleBackColor = true;
            this.buttonNotch.Click += new System.EventHandler(this.buttonNotch_Click);
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(10, 67);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(100, 40);
            this.buttonTest.TabIndex = 36;
            this.buttonTest.Text = "Image Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonNotchImg
            // 
            this.buttonNotchImg.Location = new System.Drawing.Point(116, 20);
            this.buttonNotchImg.Name = "buttonNotchImg";
            this.buttonNotchImg.Size = new System.Drawing.Size(100, 40);
            this.buttonNotchImg.TabIndex = 23;
            this.buttonNotchImg.Text = "Result View";
            this.buttonNotchImg.UseVisualStyleBackColor = true;
            this.buttonNotchImg.Click += new System.EventHandler(this.buttonNotchImg_Click);
            // 
            // buttonAlignImg
            // 
            this.buttonAlignImg.Location = new System.Drawing.Point(116, 66);
            this.buttonAlignImg.Name = "buttonAlignImg";
            this.buttonAlignImg.Size = new System.Drawing.Size(100, 40);
            this.buttonAlignImg.TabIndex = 25;
            this.buttonAlignImg.Text = "Wafer View";
            this.buttonAlignImg.UseVisualStyleBackColor = true;
            this.buttonAlignImg.Click += new System.EventHandler(this.buttonAlignImg_Click);
            // 
            // checkFlat
            // 
            this.checkFlat.AutoSize = true;
            this.checkFlat.Font = new System.Drawing.Font("굴림", 11F);
            this.checkFlat.Location = new System.Drawing.Point(10, 70);
            this.checkFlat.Name = "checkFlat";
            this.checkFlat.Size = new System.Drawing.Size(82, 19);
            this.checkFlat.TabIndex = 26;
            this.checkFlat.Text = "Flat Test";
            this.checkFlat.UseVisualStyleBackColor = true;
            // 
            // numericUpDown
            // 
            this.numericUpDown.Font = new System.Drawing.Font("굴림", 20F);
            this.numericUpDown.Location = new System.Drawing.Point(116, 66);
            this.numericUpDown.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(99, 38);
            this.numericUpDown.TabIndex = 27;
            this.numericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // buttonConvert
            // 
            this.buttonConvert.Location = new System.Drawing.Point(10, 66);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(100, 40);
            this.buttonConvert.TabIndex = 17;
            this.buttonConvert.Text = "Save Images";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonMotorFree);
            this.groupBox2.Controls.Add(this.textAccel);
            this.groupBox2.Controls.Add(this.button19);
            this.groupBox2.Controls.Add(this.textVelocity);
            this.groupBox2.Controls.Add(this.button18);
            this.groupBox2.Controls.Add(this.textSpeed);
            this.groupBox2.Controls.Add(this.button17);
            this.groupBox2.Controls.Add(this.textDelay);
            this.groupBox2.Controls.Add(this.button16);
            this.groupBox2.Controls.Add(this.textCount);
            this.groupBox2.Controls.Add(this.button15);
            this.groupBox2.Controls.Add(this.buttonMotorSet);
            this.groupBox2.Controls.Add(this.buttonMotorRun);
            this.groupBox2.Controls.Add(this.buttonMotorStop);
            this.groupBox2.Location = new System.Drawing.Point(1291, 239);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(225, 264);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Turn Table";
            // 
            // buttonMotorFree
            // 
            this.buttonMotorFree.Location = new System.Drawing.Point(115, 65);
            this.buttonMotorFree.Name = "buttonMotorFree";
            this.buttonMotorFree.Size = new System.Drawing.Size(100, 40);
            this.buttonMotorFree.TabIndex = 13;
            this.buttonMotorFree.Tag = "3";
            this.buttonMotorFree.Text = "Free";
            this.buttonMotorFree.UseVisualStyleBackColor = true;
            // 
            // textAccel
            // 
            this.textAccel.Location = new System.Drawing.Point(9, 235);
            this.textAccel.Name = "textAccel";
            this.textAccel.Size = new System.Drawing.Size(70, 21);
            this.textAccel.TabIndex = 12;
            this.textAccel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button19
            // 
            this.button19.Location = new System.Drawing.Point(85, 235);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(130, 21);
            this.button19.TabIndex = 11;
            this.button19.Text = "Accel [1~10]";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // textVelocity
            // 
            this.textVelocity.Location = new System.Drawing.Point(10, 205);
            this.textVelocity.Name = "textVelocity";
            this.textVelocity.Size = new System.Drawing.Size(70, 21);
            this.textVelocity.TabIndex = 10;
            this.textVelocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button18
            // 
            this.button18.Location = new System.Drawing.Point(85, 205);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(130, 21);
            this.button18.TabIndex = 9;
            this.button18.Text = "Velocity [Unit: deg]";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // textSpeed
            // 
            this.textSpeed.Location = new System.Drawing.Point(10, 175);
            this.textSpeed.Name = "textSpeed";
            this.textSpeed.Size = new System.Drawing.Size(70, 21);
            this.textSpeed.TabIndex = 8;
            this.textSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button17
            // 
            this.button17.Location = new System.Drawing.Point(85, 175);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(130, 21);
            this.button17.TabIndex = 7;
            this.button17.Text = "Speed [RPM - .01]";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // textDelay
            // 
            this.textDelay.Location = new System.Drawing.Point(10, 145);
            this.textDelay.Name = "textDelay";
            this.textDelay.Size = new System.Drawing.Size(70, 21);
            this.textDelay.TabIndex = 6;
            this.textDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(85, 145);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(130, 21);
            this.button16.TabIndex = 5;
            this.button16.Text = "Delay [Trig - ms]";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // textCount
            // 
            this.textCount.Location = new System.Drawing.Point(10, 115);
            this.textCount.Name = "textCount";
            this.textCount.Size = new System.Drawing.Size(70, 21);
            this.textCount.TabIndex = 4;
            this.textCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(85, 115);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(130, 21);
            this.button15.TabIndex = 3;
            this.button15.Text = "Count [Trigger]";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // buttonMotorSet
            // 
            this.buttonMotorSet.Location = new System.Drawing.Point(10, 65);
            this.buttonMotorSet.Name = "buttonMotorSet";
            this.buttonMotorSet.Size = new System.Drawing.Size(100, 40);
            this.buttonMotorSet.TabIndex = 2;
            this.buttonMotorSet.Tag = "2";
            this.buttonMotorSet.Text = "Set";
            this.buttonMotorSet.UseVisualStyleBackColor = true;
            // 
            // buttonMotorRun
            // 
            this.buttonMotorRun.Location = new System.Drawing.Point(115, 20);
            this.buttonMotorRun.Name = "buttonMotorRun";
            this.buttonMotorRun.Size = new System.Drawing.Size(100, 40);
            this.buttonMotorRun.TabIndex = 1;
            this.buttonMotorRun.Tag = "1";
            this.buttonMotorRun.Text = "Run";
            this.buttonMotorRun.UseVisualStyleBackColor = true;
            // 
            // buttonMotorStop
            // 
            this.buttonMotorStop.Location = new System.Drawing.Point(10, 20);
            this.buttonMotorStop.Name = "buttonMotorStop";
            this.buttonMotorStop.Size = new System.Drawing.Size(100, 40);
            this.buttonMotorStop.TabIndex = 0;
            this.buttonMotorStop.Tag = "0";
            this.buttonMotorStop.Text = "Stop";
            this.buttonMotorStop.UseVisualStyleBackColor = true;
            this.buttonMotorStop.Click += new System.EventHandler(this.buttonMotorStop_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.Black;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(780, 780);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonImageAdd);
            this.groupBox1.Controls.Add(this.buttonConvert);
            this.groupBox1.Controls.Add(this.numericUpDown);
            this.groupBox1.Controls.Add(this.buttonImageRemove);
            this.groupBox1.Location = new System.Drawing.Point(1060, 365);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 116);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Image Control";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.textLight);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.buttonLight);
            this.groupBox6.Controls.Add(this.checkFlat);
            this.groupBox6.Controls.Add(this.textExposure);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.buttonExposure);
            this.groupBox6.Controls.Add(this.checkCenter);
            this.groupBox6.Controls.Add(this.checkZoom);
            this.groupBox6.Controls.Add(this.checkTrigLive);
            this.groupBox6.Controls.Add(this.checkOpenCV);
            this.groupBox6.Location = new System.Drawing.Point(820, 10);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(225, 170);
            this.groupBox6.TabIndex = 31;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Option";
            // 
            // textLight
            // 
            this.textLight.Location = new System.Drawing.Point(75, 135);
            this.textLight.Name = "textLight";
            this.textLight.Size = new System.Drawing.Size(75, 21);
            this.textLight.TabIndex = 33;
            this.textLight.Text = "10";
            this.textLight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 12);
            this.label5.TabIndex = 32;
            this.label5.Text = "Light";
            // 
            // buttonLight
            // 
            this.buttonLight.Location = new System.Drawing.Point(155, 130);
            this.buttonLight.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonLight.Name = "buttonLight";
            this.buttonLight.Size = new System.Drawing.Size(60, 30);
            this.buttonLight.TabIndex = 31;
            this.buttonLight.Text = "Set";
            this.buttonLight.UseVisualStyleBackColor = true;
            this.buttonLight.Click += new System.EventHandler(this.buttonLight_Click);
            // 
            // textExposure
            // 
            this.textExposure.Location = new System.Drawing.Point(75, 100);
            this.textExposure.Name = "textExposure";
            this.textExposure.Size = new System.Drawing.Size(75, 21);
            this.textExposure.TabIndex = 30;
            this.textExposure.Text = "10000";
            this.textExposure.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 29;
            this.label3.Text = "Exposure";
            // 
            // buttonExposure
            // 
            this.buttonExposure.Location = new System.Drawing.Point(155, 95);
            this.buttonExposure.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonExposure.Name = "buttonExposure";
            this.buttonExposure.Size = new System.Drawing.Size(60, 30);
            this.buttonExposure.TabIndex = 9;
            this.buttonExposure.Text = "Set";
            this.buttonExposure.UseVisualStyleBackColor = true;
            this.buttonExposure.Click += new System.EventHandler(this.buttonExposure_Click_1);
            // 
            // checkZoom
            // 
            this.checkZoom.AutoSize = true;
            this.checkZoom.Font = new System.Drawing.Font("굴림", 11F);
            this.checkZoom.Location = new System.Drawing.Point(130, 20);
            this.checkZoom.Name = "checkZoom";
            this.checkZoom.Size = new System.Drawing.Size(64, 19);
            this.checkZoom.TabIndex = 1;
            this.checkZoom.Text = "Zoom";
            this.checkZoom.UseVisualStyleBackColor = true;
            this.checkZoom.CheckedChanged += new System.EventHandler(this.checkZoom_CheckedChanged);
            // 
            // checkTrigLive
            // 
            this.checkTrigLive.AutoSize = true;
            this.checkTrigLive.Font = new System.Drawing.Font("굴림", 11F);
            this.checkTrigLive.Location = new System.Drawing.Point(10, 20);
            this.checkTrigLive.Name = "checkTrigLive";
            this.checkTrigLive.Size = new System.Drawing.Size(101, 19);
            this.checkTrigLive.TabIndex = 0;
            this.checkTrigLive.Text = "Trigger Live";
            this.checkTrigLive.UseVisualStyleBackColor = true;
            this.checkTrigLive.CheckedChanged += new System.EventHandler(this.checkTrigLive_CheckedChanged);
            // 
            // dataGrid
            // 
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(820, 537);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowTemplate.Height = 23;
            this.dataGrid.Size = new System.Drawing.Size(465, 272);
            this.dataGrid.TabIndex = 32;
            // 
            // buttonInitGrid
            // 
            this.buttonInitGrid.Location = new System.Drawing.Point(1070, 490);
            this.buttonInitGrid.Name = "buttonInitGrid";
            this.buttonInitGrid.Size = new System.Drawing.Size(100, 40);
            this.buttonInitGrid.TabIndex = 33;
            this.buttonInitGrid.Text = "Init Grid";
            this.buttonInitGrid.UseVisualStyleBackColor = true;
            this.buttonInitGrid.Click += new System.EventHandler(this.buttonInitGrid_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox);
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 800);
            this.panel1.TabIndex = 34;
            // 
            // buttonSaveGrid
            // 
            this.buttonSaveGrid.Location = new System.Drawing.Point(1176, 490);
            this.buttonSaveGrid.Name = "buttonSaveGrid";
            this.buttonSaveGrid.Size = new System.Drawing.Size(100, 40);
            this.buttonSaveGrid.TabIndex = 35;
            this.buttonSaveGrid.Text = "Save Grid";
            this.buttonSaveGrid.UseVisualStyleBackColor = true;
            this.buttonSaveGrid.Click += new System.EventHandler(this.buttonSaveGrid_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1295, 818);
            this.Controls.Add(this.buttonSaveGrid);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonInitGrid);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupRun);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormMain";
            this.Text = " [eMotion] WaferAlign";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupRun.ResumeLayout(false);
            this.groupRun.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.Button buttonAlignImg;
        private System.Windows.Forms.Button buttonNotchImg;
        private System.Windows.Forms.Button buttonImageRemove;
        private System.Windows.Forms.Button buttonImageAdd;
        private System.Windows.Forms.Button buttonConvert;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonNotch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textPos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textSet;
        private System.Windows.Forms.Button buttonStopRun;
        private System.Windows.Forms.Button buttonStartRun;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonMotorFree;
        private System.Windows.Forms.TextBox textAccel;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.TextBox textVelocity;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.TextBox textSpeed;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.TextBox textDelay;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.TextBox textCount;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button buttonMotorSet;
        private System.Windows.Forms.Button buttonMotorRun;
        private System.Windows.Forms.Button buttonMotorStop;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonTrig;
        private System.Windows.Forms.Button buttonInit;
        private System.Windows.Forms.Button buttonActive;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupRun;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ListView listInfo;
        private System.Windows.Forms.CheckBox checkOpenCV;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textRunRep;
        private System.Windows.Forms.TextBox textRun;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkCenter;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textSetRep;
        private System.Windows.Forms.Button buttonInitGrid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkZoom;
        private System.Windows.Forms.CheckBox checkTrigLive;
        private System.Windows.Forms.Button buttonSaveGrid;
        private System.Windows.Forms.TextBox textExposure;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonExposure;
        private System.Windows.Forms.CheckBox checkFlat;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.TextBox textRunStep;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textLight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonLight;
    }
}

