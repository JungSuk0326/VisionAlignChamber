using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using Devices.Sensor.RSQ.Napson;
using Devices.Sensor.LaserDisplacement;

using BaseMachine.FORMS.PopUp;
using BaseMachine.Define;

using COMM.PLC;
using MyFormControl.Device;
using Devices.Sensor.CDX;
using System.Web.UI;

namespace BaseMachine.FORMS
{
    public partial class frmSensor : Form
    {

        ContactR sensor;
        Panasonic_HL_G1 LaserSensor;
        CdxControl Cdx;
        CancellationTokenSource LaserSensorReadCts;
        CancellationTokenSource RReadCts;

        System.Windows.Forms.Timer _readingTimer;

        public frmSensor()
        {
            InitializeComponent();
            initCbBox();
            InitDataview();

            _readingTimer = new System.Windows.Forms.Timer();
            _readingTimer.Tick += _readingTimer_Tick;
        }

      

        private void initCbBox()
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

            sensor.Open(MainClass.Setup.SensorPort, MainClass.Setup.SensorBaudRate);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sensor.Close();
        }

        private void frmSensor_Load(object sender, EventArgs e)
        {
            //Cdx = MainClass.DM.CDXSensor;
        }

        private  void btnPara_Click(object sender, EventArgs e)
        {
        }

        private void btnMode_Click(object sender, EventArgs e)
        {
            Run(MainClass.DM.Chuck.SetSensor, "Rs Sensor Set");
        }


        private void btnReady_Click(object sender, EventArgs e)
        {
        }

        private void btnRead_Click(object sender, EventArgs e)
        {

        }


        private void UpdateRValue(double value)
        {
        }


        public async void Run(Func<bool> action, string text, Control btn = null)
        {

            if (MainClass.isMachineStop() == false)
            {

                MyMessgeBox.ShowMyMsgBox("It is currently unable to operate.. ");
                return;
            }

            string msg = string.Format("{0} Would you like to perform manual operation?", text);

            if (MyMessgeBox.ShowMyMsgBox(msg, true, this) != DialogResult.Yes)
                return;


            MainClass.LogWrite(Define.ELog.Main, string.Format("MANUAL {0} {1} Try", MainClass.CurID.ToString(), text));


            var temp = MainClass.CurMainState;
            MainClass.CurMainState = Define.EMachinState.Manual;

            if (btn != null) btn.Enabled = false;

            bool result = await Task.Run(action);

            if (btn != null) btn.Enabled = true;

            MainClass.CurMainState = temp;
            if (result == true)
            {
                MainClass.LogWrite(ELog.Main, string.Format("MANUAL {0} {1} PASS", MainClass.CurID.ToString(), text));
                MyMessgeBox.ShowMyMsgBox("Manual complete");
            }
            else
            {
                MainClass.LogWrite(ELog.Main, string.Format("MANUAL {0} {1} Fail", MainClass.CurID.ToString(), text));
                MyMessgeBox.ShowMyMsgBox("Manual failed");
            }


            return;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //open

            Run(() => Cdx.Connect(MainClass.Setup.CDX_IP, MainClass.Setup.CDX_Port), "Laser Sensor Open");

            //Run(()=>LaserSensor.Open(MainClass.Setup.LaserSensorPort, MainClass.Setup.LaserSensorBaudRate), "Laser Sensor Open");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Close
            //Run(() => LaserSensor.Close(), 
            //    "Laser Sensor Close");

            //Close
            Run(() => Cdx.Close(),
                "Laser Sensor Close");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //zero
            Run(() => LaserSensor.SetZero(1, true) == Panasonic_HL_G1.Error.None,
                "Laser Sensor SetZero On");

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Run(() => LaserSensor.SetZero(1, false) == Panasonic_HL_G1.Error.None,
               "Laser Sensor SetZero Off");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //laserOn
            //Run(() => LaserSensor.LaserControl(1, true) == Panasonic_HL_G1.Error.None,
            //  "Laser Sensor LaserON");


           // Run(() => LaserSensor.LaserControl(1, true) == Panasonic_HL_G1.Error.None,
           //"Laser Sensor LaserON");

            Run(() => Cdx.LaserOnOff(true), "Laser Sensor LaserON");


        }

        private void button7_Click(object sender, EventArgs e)
        {
            //LaserOff
            //Run(() => LaserSensor.LaserControl(1, false) == Panasonic_HL_G1.Error.None,
            //"Laser Sensor LaserOFF");

            Run(() => Cdx.LaserOnOff(false), "Laser Sensor LaserOFF");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Readstart
            LaserSensorReadCts = new CancellationTokenSource();
            Run(() => ReadLaserData(LaserSensorReadCts.Token), "Read Start LaserSensor");

        }

        private bool ReadLaserData(CancellationToken token)
        {
            while (true)
            {
                if (token.IsCancellationRequested) break;

                Thread.Sleep(100);
                int data = 0;

                //bool rtn = LaserSensor.ReadData(1, ref data) == Panasonic_HL_G1.Error.None;


                bool rtn = Cdx.ReadData(ref data);

                double convertData = data / 1000000d;

                if (Math.Abs(convertData) > 25)
                    rtn = false;

                this.Invoke(new Action(() =>
                {
                }));
            }
            return true;
        }

        private bool ReadRData()
        {
            RValue value = new RValue();
            bool rtn = MainClass.DM.Chuck.Rsensor.SendStart(ref value);
            if (rtn == false) return false;
            this.Invoke(new Action(() => UpdateRValue(value.R)));
            return true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //readOff
            LaserSensorReadCts?.Cancel();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            RReadCts?.Cancel();
        }



        //plc

     


        private void InitDataview()
        {
            //GridData.Columns.Add("NAME", "NAME");
            //GridData.Columns.Add("DATA", "DATA");


            //GridData.Columns[0].Width = 200;
            //GridData.Columns[1].Width = 300;

            //int rowh = 25;
            //GridData.init(rowh);
            //GridData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //GridData.MultiSelect = false;
            //GridData.ReadOnly = false;
            //GridData.Font = new Font("Consolas", 14);
            //GridData.AutoResizeColumns();


            //string[] names = Enum.GetNames(typeof(EBoolData));
            //GridData.Rows.Add(names.Length);

            //for (int nCnt = 0; nCnt < names.Length ; nCnt++)
            //{
            //    GridData.Rows[nCnt].Cells[0].Value = names[nCnt];
            //    GridData.Rows[nCnt].Cells[1] = new DataGridViewCheckBoxCell { Value = false };
            //}

            //names = Enum.GetNames(typeof(EStringData));
            //for (int nCnt = 0; nCnt < names.Length; nCnt++)
            //{
            //    GridData.Rows.Add(names[nCnt], "");
            //}
        }

        private void UpdatePlcDataView()
        {
            //var plc = MainClass.DM._plcHandler;

            //string[] names = Enum.GetNames(typeof(EBoolData));
            //int count = names.Length;

            //for (int nCnt = 0; nCnt < names.Length; nCnt++)
            //{
            //    GridData.Rows[nCnt].Cells[1].Value =
            //        plc.GetBoolData((EBoolData)Enum.Parse(typeof(EBoolData), names[nCnt])).ToString();
            //}


            //names = Enum.GetNames(typeof(EStringData));
            //for (int nCnt = 0; nCnt < names.Length; nCnt++)
            //{
            //    GridData.Rows[nCnt + count].Cells[1].Value =
            //      plc.GetMsgData((EStringData)Enum.Parse(typeof(EStringData), names[nCnt])).ToString();
            //}
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //var plc = MainClass.DM._plcHandler;
            //string[] names = Enum.GetNames(typeof(EBoolData));
            //int count = names.Length;

            //plc.ReadDatasValue();
            //for (int nCnt = 0; nCnt < names.Length; nCnt++)
            //{
            //    bool value = GridData.Rows[nCnt].Cells[1].Value.ToString() =="False"? false: true;
            //    plc.SetBoolData((EBoolData)Enum.Parse(typeof(EBoolData), names[nCnt]), value); 
            //}
            //plc.WriteBoolData();
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            Run(() => Cdx.ReadStatus(), "Read State");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            _readingTimer.Enabled = true;
            MainClass.DM.RunReading();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            _readingTimer.Enabled = false;
            MainClass.DM.RunStop();
        }

        private void _readingTimer_Tick(object sender, EventArgs e)
        {
            lblTop.Text = MainClass.DM.TopValue.ToString();
            lblBot.Text = MainClass.DM.BotValue.ToString();
            lblThick.Text = MainClass.DM.Thick.ToString();
        }

        private void frmSensor_FormClosing(object sender, FormClosingEventArgs e)
        {
            _readingTimer.Enabled = false;
            MainClass.DM.RunStop();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            double calValue = (double)numericUpDown1.Value;
            MainClass.DM.iFC2466Ontrol.ChangeCal(calValue);




        }

        private void button21_Click(object sender, EventArgs e)
        {
            //zero

            MainClass.DM.eddyCurrent.WriteSingleCoil(1, 00002, true);


        }

        public static string ByteToAsciiHex(ushort value)
        {
            char high = (value >> 8).ToString("X")[0];  // 상위 4비트
            char low = (value & 0xFF).ToString("X")[0]; // 하위 4비트

            return $"{high}{low}";

        }
        private void button17_Click(object sender, EventArgs e)
        {
            //read
              var readValue =  MainClass.DM.eddyCurrent.ReadInputRegisters(1, 1, 4);


            if (readValue != null)
            {
                byte[] bytes = new byte[8];
                string sValue = "";
                // 각 ushort를 2바이트씩 byte 배열로 변환
                for (int i = 0; i < readValue.Length; i++)
                {
                    byte[] temp = BitConverter.GetBytes(readValue[i]).Reverse().ToArray();
                    
                    Array.Copy(temp, 0, bytes, i * 2, 2);
                }

                sValue = Encoding.ASCII.GetString(bytes);

                label6.Text = sValue;

            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MainClass.DM.eddyCurrent.WriteSingleCoil(1, 00001, true);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MainClass.DM.eddyCurrent.WriteSingleCoil(1, 00001, false);
        }
    }

}
