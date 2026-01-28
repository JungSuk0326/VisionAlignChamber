using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace eMotion
{
    public partial class FormMain : Form
    {
        struct RunInfo
        {
            public int step;
            public int delay;
            public int count;
            public int setCount;
            public int repeat;
            public int setRepeat;
            public int pos;

            public bool cv;   //off:image grab, on: + vision inspection
            public bool mode; //off:notch, on:flat
        }
        RunInfo run;

        const int DefDelay = 6; // x 100ms; 
        
        ClassAlign Aligner = null;
        MulticamEx Grabber = null;
        ClassTable Table = null;
        LfineLight Light = null;

        int imageX, imageY;

        public FormMain()
        {
            InitializeComponent();

            Aligner = new ClassAlign();
            //Aligner.OnAngleEnd += OnAngleEnd;
            //Aligner.OnWaferEnd += OnWaferEnd;
            //Aligner.OnSizeEnd += OnSizeEnd;
            bool debug = false;
#if DEBUG
            debug = true;
#endif
            Grabber = new MulticamEx(debug);
            Grabber.OnCallback += OnCallback;

            Table = new ClassTable();
            Light = new LfineLight();
            pictureBox.Paint += new PaintEventHandler(pictureBox_Paint);
        }
        
        private void OnCallback()
        {
            this.Invoke(new Action(delegate ()
            {
                try
                {
                    pictureBox.Image = null;
                    pictureBox.Image = Grabber.GetImage();
                }
                catch
                { }
            }));
        }
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (!checkCenter.Checked) return;
            // 펜 객체 생성 (빨간색, 두께 2 픽셀)
            using (Pen pen = new Pen(Color.Blue, 1))
            {
                // PictureBox의 중앙 좌표 계산
                int centerX = pictureBox.Width / 2;
                int centerY = pictureBox.Height / 2;

                // DashStyle 속성을 Dash로 설정하여 점선으로 만듭니다.
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;

                // PictureBox의 크기를 기준으로 시작점과 끝점 좌표 설정
                // 가로선: (0, centerY)에서 (Width, centerY)까지
                e.Graphics.DrawLine(pen, 0, centerY, pictureBox.Width, centerY);

                // 세로선: (centerX, 0)에서 (centerX, Height)까지
                e.Graphics.DrawLine(pen, centerX, 0, centerX, pictureBox.Height);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DispInfo(listInfo, checkFlat.Checked);
            InitGrid(dataGrid, checkFlat.Checked);

            //Table.PortOpen(7);
            //Light.PortOpen(1);
            timer.Start();

            textSet.Text = Aligner.Setting.ImageCount.ToString();
            textPos.Text = Aligner.Setting.AngleStep.ToString();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Running();
            textRun.Text = run.count.ToString();
            textRunRep.Text = run.repeat.ToString();
            textRunStep.Text = run.step.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image File (*.jpg,*.bmp, *.png)| *.jpg; *.bmp; *.png";
            if (dialog.ShowDialog() == DialogResult.OK)
                pictureBox.Image = new Bitmap(dialog.FileName);
        }

        //=====================================================================
        // open cv

        private void buttonNotch_Click(object sender, EventArgs e)
        {
            bool flat = checkFlat.Checked;
            this.Text = "이미지 검사 중...";
            Aligner.TestMain(flat);
            this.Text = "이미지 검사 완료";
            DispInfo(listInfo, flat);
        }

        private void buttonImageAdd_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.Text = "이미지 로딩 중...";
                Aligner.AddImg(dialog.SelectedPath);
                if (imageX == 0 && imageY == 0)
                {
                    imageX = Aligner.CanvasX();
                    imageY = Aligner.CanvasY();
                }
                this.Text = "이미지 로딩 완료";
            }
        }

        private void buttonImageRemove_Click(object sender, EventArgs e)
        {
            Aligner.ClearList();
        }
        private void buttonConvert_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Aligner.SaveImage(dialog.SelectedPath);
            }
        }

        private void buttonNotchImg_Click(object sender, EventArgs e)
        {
            pictureBox.Image = null;
            pictureBox.Image = Aligner.ResultImg( checkFlat.Checked);
        }

        private void buttonAlignImg_Click(object sender, EventArgs e)
        {
            pictureBox.Image = null;
            pictureBox.Image = Aligner.WaferImg(checkFlat.Checked);
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            int value = (int)numericUpDown.Value;
            pictureBox.Image = null;
            pictureBox.Image = Aligner.GetImg(value);
        }

        //=====================================================================
        //camera
        private void btn_Open1_Click(object sender, EventArgs e)
        {
            try
            {
                if (buttonInit.Text == "Open")
                {
                    string camFile = "c:\\Work\\STC-GPB250BPCL_5120x5120_DECA_10T8_30FPS_RG.cam"; //SC.cam");   
                    Grabber.OpenBoard(0, "M", "MONO_DECA", camFile);

                    Grabber.GetWidth(out imageX);
                    Grabber.GetHeight(out imageY);

                    buttonInit.Text = "Close";
                }
                else
                {
                    if (Grabber.Actived)
                        Grabber.SetAcquisition(0);

                    Grabber.CloseBoard();

                    buttonInit.Text = "Open";
                }

            }
            catch (Exception exc) // Euresys.MultiCamException exc)
            {
                Debug.WriteLine(exc.Message);
            }
        }

        private void btn_Start1_Click(object sender, EventArgs e)
        {
            try
            {
                if (buttonActive.Text == "Active")
                {
                    Grabber.SetAcquisition(MulticamEx.eState.ACTIVE);
                    buttonActive.Text = "Idle";
                }
                else
                {
                    Grabber.SetAcquisition(MulticamEx.eState.IDLE);
                    buttonActive.Text = "Active";
                }
            }
            catch (Exception exc) // Euresys.MultiCamException exc)
            {
                Debug.WriteLine(exc.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Grabber.OnTrigger();
            }
            catch (Exception exc) //Euresys.MultiCamException exc)
            {
                Debug.WriteLine(exc.Message);
            }
        }
        private void buttonExposure_Click_1(object sender, EventArgs e)
        {
            int value;
            int.TryParse(textExposure.Text, out value);
            Grabber.SetExposureTime(value);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "JPEG Image(*.jpg)|*.jpg|PNG Image(*.png)|*.png|Bitmap Image(*.bmp)|*.bmp";
            dialog.DefaultExt = "png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Grabber.SaveImage(dialog.FileName);
            }
        }
        //=====================================================================
        //step motor
        private void buttonMotorStop_Click(object sender, EventArgs e)
        {
            int mode = Convert.ToInt32(((Button)sender).Tag);
            Table.Mode((ClassTable.eMotorMode)mode);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int value;
            int.TryParse(textCount.Text, out value);
            Table.Count(value);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            int value;
            int.TryParse(textDelay.Text, out value);
            Table.Delay(value);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            int value;
            int.TryParse(textSpeed.Text, out value);
            Table.Speed(value);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            int value;
            int.TryParse(textVelocity.Text, out value);
            Table.Velocity(value);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            int value;
            int.TryParse(textAccel.Text, out value);
            Table.Accel(value);
        }
        //=====================================================================
        //running
        private void Running()
        {
            switch (run.step)
            {
                case 0:
                default: break;

                case 1:     //data set
                    Table.Speed(1000);
                    Table.Accel(1);
                    Table.Mode(ClassTable.eMotorMode.Set);
                    Aligner.ClearList();

                    run.count = 0;
                    run.delay = 0;
                    run.step++;
                    break;
                case 2:
                    Table.Velocity(run.pos);
                    run.delay = DefDelay;
                    run.step++;
                    break;
                case 3:
                    if (run.delay-- > 0) break;
                    Grabber.OnTrigger();
                    run.step++;
                    break;
                case 4:
                    if (!Grabber.GrabDone) break;
                    Aligner.AddItem(imageX, imageY, Grabber.GetImagePointer());

                    if (++run.count >= run.setCount)
                        run.step = 10;
                    else
                        run.step = 2;
                    break;
                
                // open cv
                case 10:
                    if (run.cv)
                    {
                        Aligner.TestMain(run.mode);
                        run.delay = 50;
                        run.step++;
                    }
                    else
                    {
                        run.step = 0;
                    }
                    break;
                case 11:
                    if (Aligner.GetEnd(run.mode))
                    {
                        run.step++;
                    }
                    else if (run.delay-- < 0)
                    {
                        run.delay = 50;
                        run.step++;
                    }
                    break;
                 case 12:
                    DispInfo(listInfo, run.mode);
                    AddGrid(run.mode);
                    if (++run.repeat >= run.setRepeat)
                    {
                        run.step = 0;
                    }
                    else
                    {
                        run.step = 1;
                    }
                    break;
            }
        }

        private void buttonStartRun_Click(object sender, EventArgs e)
        {
            run.setCount = Aligner.Setting.ImageCount;
            run.pos = (int)Aligner.Setting.AngleStep;

            int value;
            int.TryParse(textSetRep.Text, out value);
            run.setRepeat = value;

            Grabber.TrigLive = false;
            run.cv = checkOpenCV.Checked;
            run.mode = checkFlat.Checked;
            run.repeat = 0;
            run.step = 1;
        }

        private void buttonStopRun_Click(object sender, EventArgs e)
        {
            run.step = 0;
        }

        //=====================================================================
        // result disp[lay
        public void DispInfo(ListView lv, bool flat)   //DataGridView의 기본 설정
        {
            lv.BeginUpdate();
            int i = 0;
            if (lv.Columns.Count == 0)
            {
                string[] title = { "Index1st","Index2nd", "OffAngle", "AbsAngle", "Width", "Height", "CenterX", "CneterY", "Radius" };

                //lv.DoubleBuffered(true);
                lv.View = View.Details;
                lv.GridLines = true;
                lv.Clear();

                lv.Columns.Add(" Name", 100);
                lv.Columns.Add(" Value", 100, HorizontalAlignment.Center);

                for (i = 0; i < title.Length; i++)
                {
                    lv.Items.Add(new ListViewItem(new string[] { title[i], "" }));
                    lv.Items[i].UseItemStyleForSubItems = false;
                    lv.Items[i].SubItems[1].BackColor = Color.LightBlue;
                }
            }
            else
            {
                ClassWafer.ResultInfo result = Aligner.GetResult(flat);
              
                lv.Items[i++].SubItems[1].Text = result.Index1st.ToString();
                lv.Items[i++].SubItems[1].Text = result.Index2nd.ToString();
                lv.Items[i++].SubItems[1].Text = result.OffAngle.ToString("F3");
                lv.Items[i++].SubItems[1].Text = result.AbsAngle.ToString("F3");
                lv.Items[i++].SubItems[1].Text = result.Width.ToString("F3");
                lv.Items[i++].SubItems[1].Text = result.Height.ToString("F3");

                lv.Items[i++].SubItems[1].Text = result.Wafer.CenterX.ToString("F3");
                lv.Items[i++].SubItems[1].Text = result.Wafer.CenterY.ToString("F3");
                lv.Items[i++].SubItems[1].Text = result.Wafer.Radius.ToString("F3");

            }
            lv.EndUpdate();
        }
        private void InitGrid(DataGridView grid, bool flat)
        {
            //grid.Rows.Clear();
            //grid.Columns.Clear();
            grid.RowHeadersVisible = false;

            string[] title = { "Index1st", "Index2nd", "OffAngle", "AbsAngle", "Width", "Height", "CenterX", "CneterY", "Radius" };
            for (int i = 0; i < title.Length; i++)
            {
                grid.Columns.Add("", title[i]); //(컬럼명, 컬럼헤더) 를 사용해서 컬럼 정의함)
                grid.Columns[i].Width = 60;
            }
            ;
            grid.RowCount = 1;
        }

        private void buttonInitGrid_Click(object sender, EventArgs e)
        {
            dataGrid.Rows.Clear(); // Clear Count = 0;
            //InitGrid(dataGrid, checkFlat.Checked);
        }

        private void checkZoom_CheckedChanged(object sender, EventArgs e)
        {
            if (checkZoom.Checked)
            {
                pictureBox.Width = imageX;
                pictureBox.Height = imageY;
                pictureBox.SizeMode = PictureBoxSizeMode.Normal;
            }
            else
            {
                pictureBox.Width = 780;
                pictureBox.Height = 780;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void checkTrigLive_CheckedChanged(object sender, EventArgs e)
        {
            Grabber.TrigLive = (checkTrigLive.Checked) ? true : false;
        }

        private void AddGrid(bool flat)
        {
            ClassWafer.ResultInfo result = Aligner.GetResult(flat);
            dataGrid.Rows.Add(result.Index1st.ToString()
                            , result.Index2nd.ToString()
                            , result.OffAngle.ToString("F3")
                            , result.AbsAngle.ToString("F3")
                            , result.Width.ToString("F3")
                            , result.Height.ToString("F3")

                            , result.Wafer.CenterX.ToString("F3")
                            , result.Wafer.CenterY.ToString("F3")
                            , result.Wafer.Radius.ToString("F3"));

        }

        private void buttonSaveGrid_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CSV File (*.csv)|*.csv";
            
            if (dialog.ShowDialog() == DialogResult.OK)
                SaveGrid(dialog.FileName, dataGrid);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image File (*.jpg)|*.jpg";

            if (dialog.ShowDialog() == DialogResult.OK)
                Aligner.TestSub(dialog.FileName);
            //Aligner.TestMain();
        }

        private void buttonLight_Click(object sender, EventArgs e)
        {
            Light.OnOff(true);
            int value;
            int.TryParse(textLight.Text, out value);
            Light.Power(value);
        }

        private void SaveGrid(string fileName, DataGridView dgv)
        {
            string delimiter = ",";
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            if (dgv.RowCount == 0) return;
            int i;
            string str = "";
            for(i=0; i<dgv.ColumnCount; i++)
            {
                str += dgv.Columns[i].HeaderText;
                if(i!= dgv.ColumnCount-1)
                    str+= delimiter;
            }
            sw.WriteLine(str);

            foreach(DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;
                str = "";
                for (i = 0; i < dgv.ColumnCount; i++)
                {
                    str += row.Cells[i].Value.ToString();
                    if (i != dgv.ColumnCount - 1)
                        str += delimiter;
                }
                sw.WriteLine(str);
            }
            sw.Flush();
            sw.Close();
            fs.Close();
        }
    }
}
