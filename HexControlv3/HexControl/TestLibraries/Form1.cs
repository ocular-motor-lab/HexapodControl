﻿using System;
using HexBuilders;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;

namespace TestLibraries
{
    public partial class Form1 : Form
    {
        Hexapod hexapod;
        TimerAsync tAsync;
        TimeSpan delayStart = new TimeSpan();
        TimeSpan period = new TimeSpan();

        public List<mvmt> xes = new List<mvmt>();

        public struct mvmt
        {
            public mvmt(double x, int y)
            {
                this.xPos = x;
                this.timer = y;
            }
            public double xPos { get; set; }
            public int timer { get; set; }
        }

        List<double> sineVals = new List<double>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 152, 153, 154, 155, 156, 157, 158, 159, 160, 161, 162, 163, 164, 165, 166, 167, 168, 169, 170, 171, 172, 173, 174, 175, 176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191, 192, 193, 194, 195, 196, 197, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 240, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 257, 258, 259, 260, 261, 262, 263, 264, 265, 266, 267, 268, 269, 270, 271, 272, 273, 274, 275, 276, 277, 278, 279, 280, 281, 282, 283, 284, 285, 286, 287, 288, 289, 290, 291, 292, 293, 294, 295, 296, 297, 298, 299, 300, 301, 302, 303, 304, 305, 306, 307, 308, 309, 310, 311, 312, 313, 314, 315, 316, 317, 318, 319, 320, 321, 322, 323, 324, 325, 326, 327, 328, 329, 330, 331, 332, 333, 334, 335, 336, 337, 338, 339, 340, 341, 342, 343, 344, 345, 346, 347, 348, 349, 350, 351, 352, 353, 354, 355, 356, 357, 358, 359, 360, 361, 362, 363, 364, 365, 366, 367, 368, 369, 370, 371, 372, 373, 374, 375, 376, 377, 378, 379, 380, 381, 382, 383, 384, 385, 386, 387, 388, 389, 390, 391, 392, 393, 394, 395, 396, 397, 398, 399, 400, 401 }; //{ 130, 129.99, 129.94, 129.87, 129.76, 129.63, 129.47, 129.28, 129.06, 128.81, 128.53, 128.23, 127.89, 127.53, 127.14, 126.73, 126.29, 125.82, 125.33, 124.81, 124.27, 123.7, 123.12, 122.5, 121.87, 121.21, 120.54, 119.84, 119.12, 118.39, 117.63, 116.86, 116.07, 115.27, 114.45, 113.62, 112.77, 111.91, 111.04, 110.16, 109.27, 108.37, 107.46, 106.54, 105.62, 104.69, 103.76, 102.82, 101.88, 100.94, 100, 99.058, 98.116, 97.177, 96.24, 95.307, 94.379, 93.456, 92.539, 91.63, 90.729, 89.838, 88.956, 88.086, 87.227, 86.38, 85.547, 84.729, 83.925, 83.137, 82.366, 81.613, 80.877, 80.161, 79.464, 78.787, 78.131, 77.497, 76.885, 76.295, 75.729, 75.188, 74.67, 74.178, 73.711, 73.27, 72.855, 72.467, 72.107, 71.774, 71.468, 71.191, 70.943, 70.722, 70.531, 70.369, 70.237, 70.133, 70.059, 70.015, 70 };
        List<float> sineVals2 = new List<float>();

        private float Siner(double Amp, double elements, double Period, double Phase, double Shift)
        {
            return (float)(Amp * Math.Sin(elements * 2 * Math.PI / Period + Phase) + Shift);
        }

        public Form1()
        {
            InitializeComponent();
            hexapod = new Hexapod();
            hexapod.DataFolder = "C:\\Users\\omlab-admin\\Desktop";
            Application.Idle += Application_Idle;
        }
        public int ctr = 0;
        public bool increaseFlag = true;
        private async Task Doer()
        {
            var x = sineVals[ctr];
            string response = await hexapod.asyncMove(x, x, x, x, x, x, 10);
            if (increaseFlag)
            {
                if (ctr < (sineVals.Count-1))
                {
                    ctr++;
                } else
                {
                    increaseFlag = false;
                }
            } else
            {
                if (ctr > 1)
                {
                    ctr--;
                }
                else
                {
                    increaseFlag = true;
                }
            }
        }

        private async Task Doer2()
        {
            var x = Math.Floor(sineVals2[ctr]);
            var y = sineVals2[(sineVals2.Count-1) - ctr];
            string response = await hexapod.asyncMove(x, y, x, y, x, y, 10);
            if (ctr < (sineVals2.Count-1))
            {
                ctr++;
            }
            else
            {
                ctr = 1;
            }
            
        }

        private float positionTracker = 0;
        private bool triangleFlipKey = true;
        private async Task TriangleDoer(float t)
        {
            var amp = amplitudeNUD.Value;
            var maxValue = 100 + amp;
            var minValue = 100 - amp;

            var x = 100 + positionTracker;
            var y = 100 - positionTracker;

            string response = await hexapod.asyncMove(x, y, x, y, x, y, 10);
            
            if ((positionTracker < (float)amp) && triangleFlipKey)
            {
                positionTracker += 1f;
            } else if ((positionTracker >= (float)amp) && triangleFlipKey)
            {
                triangleFlipKey = false;
            } else if ((positionTracker > -(float)amp) && !triangleFlipKey)
            {
                positionTracker -= 1f;
            }
            else
            {
                triangleFlipKey = true;
            }

        }
        private async Task TriangleDoer2()
        {
            var x = 0;
            var y = amp;
            if (triangleFlipKey)
            {
                string response = await hexapod.asyncMove(x, y, x, y, x, y, trianglePeriod);
                triangleFlipKey = false;
            } else
            {
                string response = await hexapod.asyncMove(y, x, y, x, y, x, trianglePeriod);
                triangleFlipKey = true;
            }
        }


        private double startTime = -1;
        public Stopwatch timer = new Stopwatch();
        private async Task RollFitter()
        {
            if (startTime < 0)
            {
                startTime = timer.Elapsed.TotalMilliseconds;
            }

            var tempPos = Siner(RollDegrees, (timer.Elapsed.TotalMilliseconds - startTime)/1000, 1/RollFrequency, Math.PI, 0);
            var x = hexapod.CalculateArmLength(0, 0, 110, 0, 0, tempPos, 0, 0, 18);
            string response = await hexapod.asyncMove(x[2], x[3], x[4], x[5], x[0], x[1], 10);

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
                if (triangleCB.Checked)
                {
                    periodNUD.Enabled = false;
                    phaseNUD.Enabled = false;
                    shiftNUD.Enabled = false;
                    amplitudeNUD.Maximum = 400;
                    amplitudeNUD.Minimum = 0;
                }
                else if (RollCB.Checked)
                {
                    periodNUD.Enabled = false;
                    phaseNUD.Enabled = false;
                    shiftNUD.Enabled = false;
                    amplitudeNUD.Enabled = false;
                    freqUD.Enabled = true;
                    degUD.Enabled = true;
                } else
                {
                    periodNUD.Enabled = true;
                    phaseNUD.Enabled = true;
                    shiftNUD.Enabled = true;
                    amplitudeNUD.Enabled = true;
                    amplitudeNUD.Maximum = 100;
                    freqUD.Enabled = false;
                    degUD.Enabled = false;
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

        private double RollDegrees;
        private double RollFrequency;
        private void freqUD_ValueChanged(object sender, EventArgs e)
        {
            decimal temp;
            temp = (decimal)(1.4 / Decimal.ToDouble(freqUD.Value) - 0.05);
            if (temp > 15)
            {
                degUD.Value = 15;
                freqUD.Value = (decimal)(1.4 / (Decimal.ToDouble(degUD.Value) + 0.05));
                RollDegrees = 15;
            }
            else
            {
                degUD.Value = temp;
                freqUD.Value = (decimal)(1.4 / (Decimal.ToDouble(degUD.Value) + 0.05));
                RollDegrees = (double)temp;
            }
        }
        private void degUD_ValueChanged(object sender, EventArgs e)
        {
            decimal temp;
            temp = (decimal)(1.4 / (Decimal.ToDouble(degUD.Value) + 0.05));
            if (temp > 2)
            {
                freqUD.Value = 2;
                degUD.Value = (decimal)(1.4 / Decimal.ToDouble(freqUD.Value) - 0.05);
                RollFrequency = 2;
            }
            else
            {
                freqUD.Value = temp;
                degUD.Value = (decimal)(1.4 / Decimal.ToDouble(freqUD.Value) - 0.05);
                RollFrequency= (double)temp;
            }
        }

        private float amp;
        private int trianglePeriod; 
        private async void SendButton_Click(object sender, EventArgs e)
        {
            delayStart = TimeSpan.FromMilliseconds(4000);
            period = TimeSpan.FromMilliseconds(10);

            Func<CancellationToken, Task> func;

            string response;
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
                else if (triangleCB.Checked)
                {
                    amp = (float)amplitudeNUD.Value;
                    var x = amp/2;
                    trianglePeriod = (int)(10 * amp);
                    period = TimeSpan.FromMilliseconds(trianglePeriod);

                    response = await hexapod.asyncMove(x, x, x, x, x, x, 4000);
                    func = t => TriangleDoer2();
                    tAsync = new TimerAsync(func, delayStart, period, false);
                    tAsync.Start();
                } else if (RollCB.Checked)
                {
                    //var x = sineVals[0];
                    var x = hexapod.CalculateArmLength(0, 0, 110, 0, 0, 0, 0, 0, 18);
                    response = await hexapod.asyncMove(x[0], x[1], x[2], x[3], x[4], x[5], 4000);

                    func = t => RollFitter();
                    tAsync = new TimerAsync(func, delayStart, period, false);
                    timer.Start();
                    tAsync.Start();
                }
                else
                {
                    if (useSineCB.Checked)
                    {
                        var x = sineVals2[0];
                        response = await hexapod.asyncMove(x, x, x, x, x, x, 1300);
                        func = t => Doer2();
                    }
                    else
                    {
                        var x = sineVals[0];
                        response = await hexapod.asyncMove(x, x, x, x, x, x, 1300);
                        func = t => Doer();
                    }
                    ctr++;
                    tAsync = new TimerAsync(func, delayStart, period, false);
                    tAsync.Start();
                    //for (int i = 0; i < 30; i++)
                    //{
                    //    var x = sineVals[i];
                    //    if (i == 0)
                    //    {
                    //        response = await hexapod.asyncMove(x, x, x, x, x, x, 1000);
                    //        Thread.Sleep(2000);
                    //    }
                    //    else
                    //    {
                    //        response = await hexapod.asyncMove(x, x, x, x, x, x, 1000);
                    //    }
                    //}
                }
            }
            else
            {
                response = await hexapod.ReadSetting("FN", 28, 1);
                ResponseBox.AppendText(response + "\r\n");
            }
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
            //tAsync.Stop();
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
        private void buildSineB_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Pen pen = new Pen(Brushes.Black, 1.0F);

            float x1 = 0;
            float y1 = 0;

            float y2 = 0;

            float yEx = 450;
            float eF = 100;
            var amplitude = (double)amplitudeNUD.Value;
            var period = (double)periodNUD.Value;
            var phase = (double)phaseNUD.Value;
            var shift = (double)shiftNUD.Value;

            var largest = 0f;

            for (float x = 0; x < period; x += 0.01F)
            {
                y2 = Siner(amplitude, x, period, Math.PI, 0);
                if (y2 > largest)
                {
                    largest = y2;
                }
            }
            sineVals2.Clear();
            for (float x = 0; x < Width; x += 0.01F)
            {
                y2 = Siner(amplitude, x, period, Math.PI, shift);
                if (x < period)
                    sineVals2.Add(y2);
                y2 = y2 - (float)shift;
                
                y2 /= largest;

                g.DrawLine(pen, x1 * eF, y1 * eF + yEx, x * eF, y2 * eF + yEx);
                x1 = x;
                y1 = y2;
            }

            largest = 0;
            for ( int i = 1; i < (sineVals2.Count); i++)
            {
                var temp = Math.Abs(sineVals2[i] - sineVals2[i - 1]);
                if ( temp > largest)
                {
                    largest = temp;
                }
            }

            if (largest > 1)
            {
                sineStatusTB.Text = "BAD: " + largest.ToString();
                sineStatusTB.BackColor = System.Drawing.Color.Red;
            }
            else
            {
                sineStatusTB.Text = "GOOD: " + largest.ToString();
                sineStatusTB.BackColor = System.Drawing.Color.Green;
            }
            foreach (float val in sineVals2)
            {
                ResponseBox.AppendText(val.ToString() + ", ");
            }
            //ResponseBox.Text = sineVals2.Count.ToString();

            maxLBL.Text = "MAX: " + (amplitude + shift).ToString();
            minLBL.Text = "MIN: " + ((amplitude + shift) * -1).ToString();
            g.DrawLine(pen, 0, yEx, Width, yEx);
            g.DrawLine(pen, 0, yEx-eF, Width, yEx-eF);
            g.DrawLine(pen, 0, yEx+eF, Width, yEx+eF);
        }

        private void stopTimerB_Click(object sender, EventArgs e)
        {
            tAsync.Stop();
            timer.Stop();
            startTime = -1;
        }
    }
}
