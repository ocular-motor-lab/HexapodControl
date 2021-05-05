using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

using System.Timers;

using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace Hex_Control
{
    public partial class RotationLength : Form
    {

        //Action platform mechanical parameters
        //public uint AccessDistanceUnit { get; set; }//350 ActLength
        //public uint LeadDistanceUnit { get; set; }  //10 RotLength
        //public uint OneTurnPulseNum { get; set; }   //10000 RotPulse

        public uint accessDistanceUnit;
        public float leadDistanceUnit;
        public uint oneTurnPulseNum;

        //Control protocol packet
        private const ushort confirmCode = 0x55aa;
        private const ushort passCode = 0;
        private const ushort functionCode = 0x1301;
        private const ushort objectChannel = 1;
        private const ushort whoAccept = 0xFFFF;
        private const ushort whoReply = 0xFFFF;
        private const ushort UDPverification = 0;
        private uint absTime = 10;//millisecond
        private uint xPos = 1;
        private uint yPos = 1;
        private uint zPos = 1;
        private uint uPos = 1;
        private uint vPos = 1;
        private uint wPos = 1;

        private uint SFXOut = 0x1234;
        private uint analogOut = 0x5678abcd;

        //udp
        private IPEndPoint mboxIPE = null;
        private IPEndPoint recvIPE = null;
        private Socket socket = null;
        private EndPoint Remote;

        StreamWriter logger;

        UDPListener duck;

        
        private void constructPacket()
        {
            StringBuilder str = new StringBuilder();
            accessDistanceUnit = Convert.ToUInt16(ActLength.Text);
            leadDistanceUnit   = Convert.ToUInt16(RotLength.Text);
            oneTurnPulseNum    = Convert.ToUInt16(RotPulses.Text);

            uint _xPos;// = (accessDistanceUnit / leadDistanceUnit) * oneTurnPulseNum * xPos; // 255;
            uint _yPos;// = (accessDistanceUnit / leadDistanceUnit) * oneTurnPulseNum * yPos; // 255;
            uint _zPos;// = (accessDistanceUnit / leadDistanceUnit) * oneTurnPulseNum * zPos; // 255;
            uint _uPos;// = (accessDistanceUnit / leadDistanceUnit) * oneTurnPulseNum * uPos; // 255;
            uint _vPos;// = (accessDistanceUnit / leadDistanceUnit) * oneTurnPulseNum * vPos; // 255;
            uint _wPos;// s = (accessDistanceUnit / leadDistanceUnit) * oneTurnPulseNum * wPos; // 255;

            if (checkBox1.Checked)
            {
                _xPos = Convert.ToUInt32(((Convert.ToUInt32(s_xPos.Text) + 5) / leadDistanceUnit) * oneTurnPulseNum * xPos); // 255;
                _yPos = Convert.ToUInt32(((Convert.ToUInt32(s_yPos.Text) + 5) / leadDistanceUnit) * oneTurnPulseNum * yPos); // 255;
                _zPos = Convert.ToUInt32(((Convert.ToUInt32(s_zPos.Text) + 5) / leadDistanceUnit) * oneTurnPulseNum * zPos); // 255;
                _uPos = Convert.ToUInt32(((Convert.ToUInt32(s_uPos.Text) + 5) / leadDistanceUnit) * oneTurnPulseNum * uPos); // 255;
                _vPos = Convert.ToUInt32(((Convert.ToUInt32(s_vPos.Text) + 5) / leadDistanceUnit) * oneTurnPulseNum * vPos); // 255;
                _wPos = Convert.ToUInt32(((Convert.ToUInt32(s_wPos.Text) + 5) / leadDistanceUnit) * oneTurnPulseNum * wPos); // 255;

                if (Convert.ToUInt32(s_xPos.Text) >= 450 | Convert.ToUInt32(s_yPos.Text) >= 450 | Convert.ToUInt32(s_zPos.Text) >= 450 |
                Convert.ToUInt32(s_uPos.Text) >= 450 | Convert.ToUInt32(s_vPos.Text) >= 450 | Convert.ToUInt32(s_wPos.Text) >= 450)
                {
                    _xPos = 0;
                    _yPos = 0;
                    _zPos = 0;
                    _uPos = 0;
                    _vPos = 0;
                    _wPos = 0;
                }
            }
            else
            {
                _xPos = Convert.ToUInt32(((accessDistanceUnit + 5) / leadDistanceUnit) * oneTurnPulseNum * xPos); // 255;
                _yPos = Convert.ToUInt32(((accessDistanceUnit + 5) / leadDistanceUnit) * oneTurnPulseNum * yPos); // 255;
                _zPos = Convert.ToUInt32(((accessDistanceUnit + 5) / leadDistanceUnit) * oneTurnPulseNum * zPos); // 255;
                _uPos = Convert.ToUInt32(((accessDistanceUnit + 5) / leadDistanceUnit) * oneTurnPulseNum * uPos); // 255;
                _vPos = Convert.ToUInt32(((accessDistanceUnit + 5) / leadDistanceUnit) * oneTurnPulseNum * vPos); // 255;
                _wPos = Convert.ToUInt32(((accessDistanceUnit + 5) / leadDistanceUnit) * oneTurnPulseNum * wPos); // 255;
                if (accessDistanceUnit >= 450)
                {
                    _xPos = 0;
                    _yPos = 0;
                    _zPos = 0;
                    _uPos = 0;
                    _vPos = 0;
                    _wPos = 0;
                }
            }

            

            str.Append(confirmCode.ToString("X4"));
            str.Append(passCode.ToString("X4"));
            str.Append(functionCode.ToString("X4"));
            str.Append(objectChannel.ToString("X4"));
            str.Append(whoAccept.ToString("X4"));
            str.Append(whoReply.ToString("X4"));
            str.Append(UDPverification.ToString("X8"));
            str.Append(absTime.ToString("X8"));
            str.Append(_xPos.ToString("X8"));
            str.Append(_yPos.ToString("X8"));
            str.Append(_zPos.ToString("X8"));
            str.Append(_uPos.ToString("X8"));
            str.Append(_vPos.ToString("X8"));
            str.Append(_wPos.ToString("X8"));

            str.Append(SFXOut.ToString("X4"));
            str.Append(analogOut.ToString("X8"));

            string sendStr = str.ToString().ToLower();

            byte[] sendBuf = HexStringToByteArray(sendStr);
            socket.SendTo(sendBuf, mboxIPE);
            Test_String.Text = sendStr;
            //Console.WriteLine(sendBuf);
            if (checkBox1.Checked)
            {
                logger.Write(DateTime.Now);
                logger.WriteLine("," + s_xPos.Text + "," + s_yPos.Text + "," + s_zPos.Text + "," + s_uPos.Text +
                                 "," + s_vPos.Text + "," + s_wPos.Text + "," + sendStr);
                logger.Flush();
            }
            else
            {
                logger.Write(DateTime.Now);
                logger.WriteLine("," + accessDistanceUnit.ToString() + "," + accessDistanceUnit.ToString() + 
                                 "," + accessDistanceUnit.ToString() + "," + accessDistanceUnit.ToString() +
                                 "," + accessDistanceUnit.ToString() + "," + accessDistanceUnit.ToString() + "," + sendStr);
                logger.Flush();
            }

            //if (checkBox1.Checked)
            //{
                
                
            //}

        }

        //public static void listenFromUDP()
        //{
        //    var recInfo = new byte[256];
        //    try
        //    {
        //        int recv = socket.ReceiveFrom(recInfo, ref Remote);
        //        Test_String.Text = Encoding.ASCII.GetString(recInfo, 0, recv).Trim();
        //    }
        //    catch (Exception e)
        //    {
        //        Test_String.Text = e.ToString();
        //    }
        //}
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer() { Interval = 10 };

        public StringBuilder strBldr(int numFrame, uint _xPos, uint _yPos, uint _zPos, uint _uPos, uint _vPos, uint _wPos)
        {
            StringBuilder str = new StringBuilder();

            accessDistanceUnit = 271;// Convert.ToUInt16(ActLength.Text);
            leadDistanceUnit = 5; // Convert.ToUInt16(RotLength.Text);
            oneTurnPulseNum = 10000; // Convert.ToUInt16(RotPulses.Text);

            str.Append(confirmCode.ToString("X4"));
            str.Append(passCode.ToString("X4"));
            str.Append(functionCode.ToString("X4"));
            str.Append(objectChannel.ToString("X4"));
            str.Append(whoAccept.ToString("X4"));
            str.Append(whoReply.ToString("X4"));
            str.Append(numFrame.ToString("X8"));
            str.Append(absTime.ToString("X8"));
            str.Append(_xPos.ToString("X8"));
            str.Append(_yPos.ToString("X8"));
            str.Append(_zPos.ToString("X8"));
            str.Append(_uPos.ToString("X8"));
            str.Append(_vPos.ToString("X8"));
            str.Append(_wPos.ToString("X8"));

            str.Append(SFXOut.ToString("X4"));
            str.Append(analogOut.ToString("X8"));

            return str;

        }

        System.Windows.Forms.Timer yawTimer = new System.Windows.Forms.Timer() { Interval = 10 };
        UInt32[] yawSin = new UInt32[100];
        public void contYaw()
        {
            float[] arr = linspace(195, 359, 100);
            for (int i = 0; i < arr.Length; i++)
            {
                yawSin[i] = Convert.ToUInt32((((arr[i] + 5) / leadDistanceUnit) * oneTurnPulseNum));
            }

            yawTimer.Tick += new EventHandler(sendYawSin);
            yawTimer.Start();

        }
        int yawCtr = 0;
        int _cycles = 10;
        int cyc_Tracker = 0;
        int tot_ctr = 0;

        private void sendYawSin(Object source, EventArgs e)
        {
            //Test_String.Text = counter.ToString();

            StringBuilder str = new StringBuilder();

            if (cyc_Tracker % 2 == 0)
            {
                str = strBldr(tot_ctr, yawSin[100 - (yawCtr + 1)], yawSin[yawCtr], yawSin[100 - (yawCtr + 1)], yawSin[yawCtr], yawSin[100 - (yawCtr + 1)], yawSin[yawCtr]);
            } else
            {
                str = strBldr(tot_ctr, yawSin[yawCtr], yawSin[100 - (yawCtr + 1)], yawSin[yawCtr], yawSin[100 - (yawCtr + 1)], yawSin[yawCtr], yawSin[100 - (yawCtr + 1)]);
            }

            tot_ctr += 1;

            string sendStr = str.ToString().ToLower();

            byte[] sendBuf = HexStringToByteArray(sendStr);
            socket.SendTo(sendBuf, mboxIPE);
            Test_String.Text = sendStr;
            //Console.WriteLine(sendBuf);

            logger.Write(DateTime.Now);
            UInt32 once;

            UInt32 twice;
            if (cyc_Tracker % 2 == 0)
            {
                twice  = yawSin[yawCtr];
                once = yawSin[100 - (yawCtr + 1)];
            }
            else
            {
                once = yawSin[yawCtr];
                twice  = yawSin[100 - (yawCtr + 1)];
            }


            logger.WriteLine("," + once.ToString() + "," + twice.ToString() +
                             "," + once.ToString() + "," + twice.ToString() +
                             "," + once.ToString() + "," + twice.ToString() + "," + sendStr);
            logger.Flush();
            yawCtr += 1;

            if (yawCtr == 100)
            {
                //yawTimer.Stop();
                yawCtr = 0;
                cyc_Tracker += 1;
                if (cyc_Tracker == _cycles)
                {
                    yawTimer.Stop();
                }
            }


        }

        public void sinTest()
        {
            
            float[] arr = linspace(195, 359, 100);
            float[] brr = linspace(195, 271, 100);
            float[] crr = linspace(271, 359, 100);

            for (int i = 0; i < arr.Length; i++)
            {
                boop[i] = Convert.ToUInt32((((arr[i] + 5) / leadDistanceUnit) * oneTurnPulseNum));
                setupBoopB[i] = Convert.ToUInt32((((brr[i] + 5) / leadDistanceUnit) * oneTurnPulseNum));
                setupBoopC[i] = Convert.ToUInt32((((crr[i] + 5) / leadDistanceUnit) * oneTurnPulseNum));
            }

            t.Tick += new EventHandler(sendSinCom);
            t.Start();
            
        }

        UInt32[] setupBoopB = new UInt32[100];
        UInt32[] setupBoopC = new UInt32[100];
        UInt32[] boop = new UInt32[100];
        public bool setupFlag = false;

        static ushort counter = 0;
        static ushort setupCtr = 0;
        private void sendSinCom(Object source, EventArgs e)
        {
            //Test_String.Text = counter.ToString();

            StringBuilder str = new StringBuilder();
            
            if (setupCtr == 99)
            {
                setupFlag = true;
            } else
            {
                str = strBldr(setupCtr, setupBoopB[100 - (setupCtr + 1)], setupBoopC[setupCtr], setupBoopB[100 - (setupCtr + 1)], setupBoopC[setupCtr], setupBoopB[100 - (setupCtr + 1)], setupBoopC[setupCtr]);
                setupCtr += 1;
            }

            if (setupFlag)
            {
                var temp = setupCtr + counter;
                str = strBldr(temp, boop[counter], boop[100 - (counter + 1)], boop[counter], boop[100 - (counter + 1)], boop[counter], boop[100 - (counter + 1)]);
                counter += 1;
            }


            string sendStr = str.ToString().ToLower();

            byte[] sendBuf = HexStringToByteArray(sendStr);
            socket.SendTo(sendBuf, mboxIPE);
            Test_String.Text = sendStr;
            //Console.WriteLine(sendBuf);

            logger.Write(DateTime.Now);
            UInt32 once;

            UInt32 twice;
            if (setupFlag)
            {
                once = boop[counter];// / 10000 * 5 - 5;
                twice = boop[100 - (counter + 1)];// / 10000 * 5 - 5;
            } else
            {
                twice = setupBoopC[setupCtr];// / 10000 * 5 - 5;
                once = setupBoopB[100 - (setupCtr + 1)];// / 10000 * 5 - 5;
            }
            

            logger.WriteLine("," + once.ToString() + "," + twice.ToString() +
                             "," + once.ToString() + "," + twice.ToString() +
                             "," + once.ToString() + "," + twice.ToString() + "," + sendStr);
            logger.Flush();


            if (counter == 99)
            {
                t.Stop();
                counter = 0;
                setupCtr = 0;
            }


        }


        /*
        public void sinTest()
        {
            StringBuilder str = new StringBuilder();
            accessDistanceUnit = 271;// Convert.ToUInt16(ActLength.Text);
            leadDistanceUnit = 5; // Convert.ToUInt16(RotLength.Text);
            oneTurnPulseNum = 10000; // Convert.ToUInt16(RotPulses.Text);

            uint _xPos;// = (accessDistanceUnit / leadDistanceUnit) * oneTurnPulseNum * xPos; // 255;
            uint _yPos;// = (accessDistanceUnit / leadDistanceUnit) * oneTurnPulseNum * yPos; // 255;
            uint _zPos;// = (accessDistanceUnit / leadDistanceUnit) * oneTurnPulseNum * zPos; // 255;
            uint _uPos;// = (accessDistanceUnit / leadDistanceUnit) * oneTurnPulseNum * uPos; // 255;
            uint _vPos;// = (accessDistanceUnit / leadDistanceUnit) * oneTurnPulseNum * vPos; // 255;
            uint _wPos;// s = (accessDistanceUnit / leadDistanceUnit) * oneTurnPulseNum * wPos; // 255;


            _xPos = Convert.ToUInt32(((accessDistanceUnit + 5) / leadDistanceUnit) * oneTurnPulseNum * xPos); // 255;
            _yPos = Convert.ToUInt32(((accessDistanceUnit + 5) / leadDistanceUnit) * oneTurnPulseNum * yPos); // 255;
            _zPos = Convert.ToUInt32(((accessDistanceUnit + 5) / leadDistanceUnit) * oneTurnPulseNum * zPos); // 255;
            _uPos = Convert.ToUInt32(((accessDistanceUnit + 5) / leadDistanceUnit) * oneTurnPulseNum * uPos); // 255;
            _vPos = Convert.ToUInt32(((accessDistanceUnit + 5) / leadDistanceUnit) * oneTurnPulseNum * vPos); // 255;
            _wPos = Convert.ToUInt32(((accessDistanceUnit + 5) / leadDistanceUnit) * oneTurnPulseNum * wPos); // 255;
            if (accessDistanceUnit >= 450)
            {
                _xPos = 0;
                _yPos = 0;
                _zPos = 0;
                _uPos = 0;
                _vPos = 0;
                _wPos = 0;
            }



            str.Append(confirmCode.ToString("X4"));
            str.Append(passCode.ToString("X4"));
            str.Append(functionCode.ToString("X4"));
            str.Append(objectChannel.ToString("X4"));
            str.Append(whoAccept.ToString("X4"));
            str.Append(whoReply.ToString("X4"));
            str.Append(UDPverification.ToString("X8"));
            str.Append(absTime.ToString("X8"));
            str.Append(_xPos.ToString("X8"));
            str.Append(_yPos.ToString("X8"));
            str.Append(_zPos.ToString("X8"));
            str.Append(_uPos.ToString("X8"));
            str.Append(_vPos.ToString("X8"));
            str.Append(_wPos.ToString("X8"));

            str.Append(SFXOut.ToString("X4"));
            str.Append(analogOut.ToString("X8"));

            string sendStr = str.ToString().ToLower();

            byte[] sendBuf = HexStringToByteArray(sendStr);
            socket.SendTo(sendBuf, mboxIPE);
            Test_String.Text = sendStr;
            //Console.WriteLine(sendBuf);
            if (checkBox1.Checked)
            {
                logger.Write(DateTime.Now);
                logger.WriteLine("," + s_xPos.Text + "," + s_yPos.Text + "," + s_zPos.Text + "," + s_uPos.Text +
                                 "," + s_vPos.Text + "," + s_wPos.Text + "," + sendStr);
                logger.Flush();
            }
            else
            {
                logger.Write(DateTime.Now);
                logger.WriteLine("," + accessDistanceUnit.ToString() + "," + accessDistanceUnit.ToString() +
                                 "," + accessDistanceUnit.ToString() + "," + accessDistanceUnit.ToString() +
                                 "," + accessDistanceUnit.ToString() + "," + accessDistanceUnit.ToString() + "," + sendStr);
                logger.Flush();
            }

            float[] arr = linspace(195, 359, 100);
            UInt32[] boop = new UInt32[100];
            for (int i = 0; i < arr.Length; i++)
            {
                boop[i] = Convert.ToUInt32(Math.Floor(arr[i]));
            }

            for (int i = 0; i < 100; i++)
            {
                str.Remove(24, 8);
                str.Insert(24, i.ToString("X8"));
                str.Remove(32, 8);
                str.Insert(32, 100.ToString("X8"));
                str.Remove(40, 8);
                str.Insert(40, boop[i].ToString("X8"));
                str.Remove(48, 8);
                str.Insert(48, boop[100 - (i + 1)].ToString("X8"));
                str.Remove(56, 8);
                str.Insert(56, boop[i].ToString("X8"));
                str.Remove(64, 8);
                str.Insert(64, boop[100 - (i + 1)].ToString("X8"));
                str.Remove(72, 8);
                str.Insert(72, boop[i].ToString("X8"));
                str.Remove(80, 8);
                str.Insert(80, boop[100- (i + 1)].ToString("X8"));


                sendStr = str.ToString().ToLower();

                sendBuf = HexStringToByteArray(sendStr);
                socket.SendTo(sendBuf, mboxIPE);
                Test_String.Text = sendStr;
                //Console.WriteLine(sendBuf);

                logger.Write(DateTime.Now);
                logger.WriteLine("," + boop[i].ToString() + "," + boop[100 - (i + 1)].ToString() +
                                 "," + boop[i].ToString() + "," + boop[100 - (i + 1)].ToString() +
                                 "," + boop[i].ToString() + "," + boop[100 - (i + 1)].ToString() + "," + sendStr);
                logger.Flush();



            }

        }
        */
        public static float[] linspace(float startval, float endval, int steps)
        {
            float interval = (Math.Abs(endval) - Math.Abs(startval)) / (steps - 1);
            return (from val in Enumerable.Range(0, steps)
                    select startval + (val * interval)).ToArray();
        }

        public RotationLength()
        {
            InitializeComponent();
            s_xPos.Enabled = false;
            s_yPos.Enabled = false;
            s_zPos.Enabled = false;
            s_uPos.Enabled = false;
            s_vPos.Enabled = false;
            s_wPos.Enabled = false;
            mboxIPE = new IPEndPoint(IPAddress.Parse("192.168.15.255"), 7408);
            recvIPE = new IPEndPoint(IPAddress.Parse("192.168.15.100"), 8410);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Remote = (EndPoint)(recvIPE);//(mboxIPE);

            logger = new StreamWriter("C:\\Users\\raulr\\Documents\\UDPLog.csv");

            logger.Write(DateTime.Now);
            logger.WriteLine(",X,Y,Z,U,V,W,HEX String");
            logger.Flush();

            //duck = new UDPListener();
            //duck.StartListener(94);
            //RecText.Text = duck.words;

            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void RotationLength_Load(object sender, EventArgs e)
        {

        }

        private void ActLength_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void sendCom_Click(object sender, EventArgs e)
        {
            constructPacket();
        }


        /// <summary>
        /// Reset
        /// </summary>
        public void Reset()
        {
            string str = "55aa000013010001ffffffff000000000000000000000000000000000000000000000000000000000000000012345678abcd";
            byte[] sendBuf = HexStringToByteArray(str);
            Test_String.Text = str;
            socket.SendTo(sendBuf, mboxIPE);
            logger.Write(DateTime.Now);
            logger.Write(",0,0,0,0,0,0,");
            logger.WriteLine(str);
            logger.Flush();
        }

        /// <summary>
        /// Convert a HEX String into a BYTE array to be sent
        /// </summary>
        private byte[] HexStringToByteArray(string s)
        {
            //s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
            {
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            }
            return buffer;
        }

        private void retOrigin_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                s_xPos.Enabled = true;
                s_yPos.Enabled = true;
                s_zPos.Enabled = true;
                s_uPos.Enabled = true;
                s_vPos.Enabled = true;
                s_wPos.Enabled = true;
            } else
            {
                s_xPos.Enabled = false;
                s_yPos.Enabled = false;
                s_zPos.Enabled = false;
                s_uPos.Enabled = false;
                s_vPos.Enabled = false;
                s_wPos.Enabled = false;
            }
        }

        private void Test_String_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void sinButton_Click(object sender, EventArgs e)
        {
            sinTest();
        }

        private void yawSinButton_Click(object sender, EventArgs e)
        {
            contYaw();
        }
    }

    /////////////////////////////////////////////////////////////////////////////
    ///
    public class UDPListener
    {
        private int m_portToListen = 8410;//2003;
        private volatile bool listening;
        Thread m_ListeningThread;
        public event EventHandler<MyMessageArgs> NewMessageReceived;
        public string words;

        //constructor
        public UDPListener()
        {
            this.listening = false;
        }

        public void StartListener(int exceptedMessageLength)
        {
            if (!this.listening)
            {
                m_ListeningThread = new Thread(ListenForUDPPackages);
                m_ListeningThread.IsBackground = true;
                this.listening = true;
                words = "PlaceHolder";
                m_ListeningThread.Start();
            }
        }

        public void StopListener()
        {
            this.listening = false;
        }

        public void ListenForUDPPackages()
        {
            UdpClient listener = null;
            try
            {
                listener = new UdpClient(m_portToListen);
            }
            catch (SocketException)
            {
                //do nothing
            }

            if (listener != null)
            {
                IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, m_portToListen);

                try
                {
                    while (this.listening)
                    {
                        Console.WriteLine("Waiting for UDP broadcast to port " + m_portToListen);
                        byte[] bytes = listener.Receive(ref groupEP);

                        //raise event                        
                        NewMessageReceived(this, new MyMessageArgs(bytes));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                finally
                {
                    listener.Close();
                    Console.WriteLine("Done listening for UDP broadcast");
                }
            }
        }
    }

    public class MyMessageArgs : EventArgs
    {
        public byte[] data { get; set; }

        public MyMessageArgs(byte[] newData)
        {
            data = newData;
        }
    }
}
