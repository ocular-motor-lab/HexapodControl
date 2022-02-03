using System;
using HexBuilders;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
//using System.Timers;

namespace TestLibraries
{
    public partial class Form1 : Form
    {
        Hexapod hexapod;

        public Form1()
        {
            InitializeComponent();
            hexapod = new Hexapod();
            hexapod.DataFolder = "C:\\Users\\omlab-admin\\Desktop";
            Application.Idle += Application_Idle;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            if (hexapod != null)
            {
                if (hexapod.Connected)
                {
                    SendButton.Enabled = true;
                    DisconnectButton.Enabled = true;
                    resetButton.Enabled = true;
                    ConnectButton.Enabled = false;
                }
                else
                {
                    ConnectButton.Enabled = true;
                    SendButton.Enabled = false;
                    DisconnectButton.Enabled = false;
                    resetButton.Enabled = false;
                }

                if (mvmtCB.Checked)
                {
                    xArrayPosNUD.Enabled = true;
                    arrayTimerNUD.Enabled = true;
                    clearArrayB.Enabled = true;
                    addToArrayB.Enabled = true;
                }
                else
                {
                    xArrayPosNUD.Enabled = false;
                    arrayTimerNUD.Enabled = false;
                    clearArrayB.Enabled = false;
                    addToArrayB.Enabled = false;
                }
            }
            else
            {
                SendButton.Enabled = false;
                DisconnectButton.Enabled = false;
                ConnectButton.Enabled = false;
                resetButton.Enabled = false;
            }
        }


        private void ConnectButton_Click(object sender, EventArgs e)
        {
            hexapod.Initialize();
        }
        public List<mvmt> xes = new List<mvmt>();

        public class mvmt
        {
            public mvmt(double x, int y)
            {
                this.xPos = x;
                this.timer = y;
            }
            public double xPos { get; set; }
            public int timer { get; set; }
        }

        List<double> sineVals = new List<double>() { 100, 98.75, 97.5, 96.25, 95.001, 93.752, 92.505, 91.259, 90.014, 88.771, 87.529, 86.289, 85.052, 83.817, 82.584, 81.354, 80.128, 78.904, 77.683, 76.466, 75.253, 74.043, 72.838, 71.637, 70.44, 69.248, 68.06, 66.878, 65.701, 64.529, 63.363, 62.202, 61.048, 59.899, 58.757, 57.621, 56.492, 55.37, 54.254, 53.146, 52.045, 50.952, 49.866, 48.788, 47.718, 46.656, 45.603, 44.558, 43.522, 42.494, 41.476, 40.467, 39.467, 38.476, 37.495, 36.524, 35.563, 34.612, 33.671, 32.741, 31.821, 30.911, 30.013, 29.125, 28.249, 27.383, 26.529, 25.687, 24.856, 24.037, 23.23, 22.435, 21.651, 20.881, 20.122, 19.376, 18.643, 17.922, 17.214, 16.519, 15.838, 15.169, 14.514, 13.871, 13.243, 12.628, 12.027, 11.439, 10.865, 10.305, 9.7595, 9.2278, 8.7103, 8.2071, 7.7182, 7.2437, 6.7838, 6.3384, 5.9077, 5.4916, 5.0904, 4.704, 4.3325, 3.9759, 3.6343, 3.3079, 2.9965, 2.7003, 2.4193, 2.1536, 1.9031, 1.668, 1.4483, 1.244, 1.0551, 0.88168, 0.72376, 0.58137, 0.45451, 0.34322, 0.24751, 0.16739, 0.10288, 0.053988, 0.020721, 0.0030842, 0.0010809, 0.014711, 0.043973, 0.088861, 0.14937, 0.22549, 0.3172, 0.4245, 0.54737, 0.68579, 0.83973, 1.0092, 1.1941 };

        private async void SendButton_Click(object sender, EventArgs e)
        {
            string response;
            var watch = new System.Diagnostics.Stopwatch();
            if (x13Button.Checked)
                hexapod.udpHex.functionCode = 0x1301;
            else
                hexapod.udpHex.functionCode = 0x1401;

            if (tabControl1.SelectedTab == tabPage1)
            {
                if (mvmtCB.Checked) { 
                    for (int i = 0; i < xes.Count; i++)
                    {
                        var x = xes[i].xPos;
                        var t = xes[i].timer;
                        response = await hexapod.asyncMove(x, x, x, x, x, x, t);
                    }
                }
                else
                {
                    for (int i = 0; i < 30; i++)
                    {
                        var x = sineVals[i];
                        if (i == 0)
                        {
                            response = await hexapod.asyncMove(x, x, x, x, x, x, 1000);
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            response = await hexapod.asyncMove(x, x, x, x, x, x, 1000);
                        }
                    }
                    //var x = 0;// Decimal.ToDouble(xValueNUD.Value);
                    //var y = 100;
                    //watch.Start();
                    //response = await hexapod.asyncMove(x, x, x, x, x, x, 0);
                    //x = 100;
                    //response = await hexapod.asyncMove(x, x, x, x, x, x, 10000);
                    //x = 120;
                    //response = await hexapod.asyncMove(x, x, x, x, x, x, 12000);
                    //x = 140;
                    //response = await hexapod.asyncMove(x, y, x, y, x, y, 14000);

                    //for (int i = 0; i < 30; i++)
                    //{
                    //    if (x < 400)
                    //    {
                    //        if (x < 201)
                    //            response = await hexapod.asyncMove(x, x, x, x, x, x, x * 300);
                    //        else
                    //        {
                    //            response = await hexapod.asyncMove(x, y, x, y, x, y, x * 400);
                    //            y -= 10;
                    //        }
                    //    }
                    //    x += 10;
                    //}
                    //watch.Stop();
                }
            }
            else
            {
                response = await hexapod.ReadSetting("FN", 28, 1);
                ResponseBox.AppendText(response + "\r\n");
            }
            
            //ResponseBox.AppendText(watch.ElapsedMilliseconds + "\r\n");

        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            hexapod.Connected = false;
            hexapod.Stop();
        }

        private void chooseFilePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newFolder = new FolderBrowserDialog();//.ToString();

            if ((newFolder != null) && (newFolder.ShowDialog(this) == DialogResult.OK))
            {
                hexapod.DataFolder = newFolder.SelectedPath;
            }
        }

        private void sameValueCB_CheckedChanged(object sender, EventArgs e)
        {
            if (sameValueCB.Checked == true)
            {
                var x = xValueNUD.Value;
                xValueNUD.Value = yValueNUD.Value = zValueNUD.Value =
                vValueNUD.Value = uValueNUD.Value = wValueNUD.Value = x;

                //xValueNUD.Enabled = yValueNUD.Enabled = zValueNUD.Enabled =
                //vValueNUD.Enabled = uValueNUD.Enabled = wValueNUD.Enabled = false;
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            hexapod.Reset();
        }

        private void addToArrayB_Click(object sender, EventArgs e)
        {
            var x = (double)xArrayPosNUD.Value;
            var timez = (int)arrayTimerNUD.Value;
            xes.Add(new mvmt(x, timez));
            ResponseBox.AppendText("Value: " + x + " Time: " + timez + "\r\n");
        }

        private void clearArrayB_Click(object sender, EventArgs e)
        {
            xes.Clear();
            ResponseBox.Clear();
        }
    }
}
