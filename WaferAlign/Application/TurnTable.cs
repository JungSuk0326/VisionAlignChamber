using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;

namespace eMotion
{
    public class ClassTable
    {
        public enum eMotorMode { Stop = 0, Run, Set, Free }
        //step motor
        SerialPort sp = null;
        string recvData = "";
        public ClassTable()
        {
            sp = new SerialPort();// ("COM7", 9600, Parity.None, 8, StopBits.One);
            sp.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);
        }
        ~ClassTable()
        {
            if (sp != null && sp.IsOpen)
            {
                sp.Close();
                sp.Dispose(); 
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            recvData += sp.ReadExisting(); 
        }

        //private void UpdateRichTextBox(string data)
        //{
        //    richTextBox.AppendText(data);
        //}
        // motor control

        public bool PortOpen(int value)
        {
            if (sp.IsOpen)
                sp.Close();

            string[] ports = SerialPort.GetPortNames();
            
            if (ports.Length == 0)
            {
                Console.WriteLine("사용 가능한 COM 포트가 없습니다.");
                return false;
            }
            else
            {
                foreach (string port in ports)
                {
                    if (port == "COM" + value.ToString())
                    {
                        sp.PortName = "COM7";
                        sp.Open();

                        Console.WriteLine("통신포트 열림 : COM" + value.ToString());
                        break;
                    }
                }
                return sp.IsOpen;
            }
        }

        private void Control(char mode, int value, bool ret = false)
        {
            int i = 0;
            byte[] table = new byte[100];
            table[i++] = 0x02;
            i++; // table[i++] = 0x00;
            table[i++] = (byte)mode;
            //switch (mode)
            //{
            //    case 0: table[i++] = (byte)'m'; break; //mode
            //    case 1: table[i++] = (byte)'c'; break; //count
            //    case 2: table[i++] = (byte)'d'; break; //delay
            //    case 3: table[i++] = (byte)'s'; break; //speed
            //    case 4: table[i++] = (byte)'v'; break; //velocity
            //    case 5: table[i++] = (byte)'k'; break; //accel
            //}
            if (ret)
                table[i - 1] = (byte)char.ToUpper((char)table[i - 1]);

            table[i++] = (byte)Convert.ToChar(((value / 1000) % 10).ToString());
            table[i++] = (byte)Convert.ToChar(((value / 100) % 10).ToString());
            table[i++] = (byte)Convert.ToChar(((value / 10) % 10).ToString());
            table[i++] = (byte)Convert.ToChar((value % 10).ToString());

            table[i++] = 0x03;

            recvData = "";
            sp.Write(table, 0, i);
        }

        public void Mode(eMotorMode mode)
        {
            Control('m', (int)mode);
        }

        //trigger count
        public void Count(int value)
        {
            Control('c', value);
        }
        
        //trigger delay
        public void Delay(int value)
        {
            Control('d', value);
        }

        //소수점2자리 2500 -> 2.5rpm
        public void Speed(int value)
        {
            Control('s', value);
        }

        //회전각
        public void Velocity(int value)
        {
            Control('v', value);
        }

        //Accel [1~10]
        public void Accel(int value)
        {
            Control('k', value);
        }
    }
}
