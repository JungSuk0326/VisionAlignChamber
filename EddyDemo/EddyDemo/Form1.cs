namespace EddyDemo
{
    public partial class Form1 : Form
    {

        eddyCurrent eddy;
        public Form1()
        {
            eddy = new eddyCurrent();
            InitializeComponent();
            btDisconnect.Enabled = false;
        //    btZero.Enabled = false;
        //    btGetData.Enabled = false;
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            if (!System.Net.IPAddress.TryParse(tbIp.Text, out _))
            {
                MessageBox.Show("유효하지 않은 IP 주소입니다.", "오류");
                tbIp.Focus();
                return;
            }

            int port = -1;

            bool bPaser =  int.TryParse(tbPort.Text, out port);

            if (!bPaser || !(port >= 0 && port <= 65535))
            {

                MessageBox.Show("유효하지 않은 Port 입니다.", "오류");
                tbPort.Focus();
                return;

            }

            if(eddy.Connect(tbIp.Text, port))
            {
                btConnect.Enabled = false;
                btDisconnect.Enabled = true;
                btZero.Enabled = true;

            }
            else
            {
                MessageBox.Show("연결 실패", "오류");
              
                return;
            }

        }

        private void btDisconnect_Click(object sender, EventArgs e)
        {

            eddy.Disconnect();

            btDisconnect.Enabled = false;
            btZero.Enabled = false;
            btGetData.Enabled = false;

        }

        private void btZero_Click(object sender, EventArgs e)
        {
            
            if (!eddy.SetZero())
            {
                MessageBox.Show("연결 실패", "오류");
                return;
            }


            btGetData.Enabled = true;



        }

        private void btGetData_Click(object sender, EventArgs e)
        {
            double readData = eddy.GetData();
           

            lbGetData.Text = readData.ToString();


            //  btGetData.Enabled = false;

        }
    }
}
