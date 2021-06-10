using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

using System.Timers;

using System.Text;
using System.Windows.Forms;
using System.Linq;


namespace HexapodLibrary
{
    public partial class Form1 : Form
    {
        // Mechanical Contraints (Can be  changed, but do so at your own peril)
        public uint accessDistanceUnit = 0;
        public float leadDistanceUnit = 5;
        public uint oneTurnPulseNum = 10000;

        // Control protocol packet
        private const ushort confirmCode = 0x55aa;
        private const ushort passCode = 0;
        private const ushort functionCode = 0x1301;
        private const ushort objectChannel = 1;
        private const ushort whoAccept = 0xFFFF;
        private const ushort whoReply = 0xFFFF;
        //private const ushort UDPverification = 0;
        private uint absTime = 100;//millisecond
        //private uint xPos = 1;
        //private uint yPos = 1;
        //private uint zPos = 1;
        //private uint uPos = 1;
        //private uint vPos = 1;
        //private uint wPos = 1;

        private uint SFXOut = 0x1234;
        private uint analogOut = 0x5678abcd;

        // udp
        private IPEndPoint mboxIPE = null;
        private IPEndPoint recvIPE = null;
        private Socket socket = null;
        private EndPoint Remote;

        StreamWriter logger;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Establishes the UDP connection between the comp and the Platform. Can take a string to rename the log
        /// </summary>
        /// <param name="filename">The Name of the Log File, include csv</param>
        public void Connect(string filename = "UDPLog.csv")
        {
            mboxIPE = new IPEndPoint(IPAddress.Parse("192.168.15.255"), 7408);
            recvIPE = new IPEndPoint(IPAddress.Parse("192.168.15.100"), 8410);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            Remote = (EndPoint)(recvIPE);//(mboxIPE);

            logger = new StreamWriter("C:\\Users\\raulr\\Documents\\" + filename);

            logger.Write(DateTime.Now.ToString("hh:mm:ss.fff"));
            logger.WriteLine(",X,Y,Z,U,V,W,HEX String");
            logger.Flush();
        }

        /// <summary>
        /// Take a position, verifies that it does not exceed velocity or position limitations.
        /// Creates the HEX array, and sends it to the Platorm, and Logs the command
        /// </summary>
        // Need to track the frame number
        public int frameNum = 0;

        // The previous Positions, to check velocity
        private uint pX = 0;
        private uint pY = 0;
        private uint pZ = 0;
        private uint pU = 0;
        private uint pV = 0;
        private uint pW = 0;

        // Track whether there were any errors
        bool errTracker = false;

        public void SendCommand(uint xPos, uint yPos, uint zPos, uint uPos, uint vPos, uint wPos)
        {
            // Verifies that velocity is within limits
            if ( (xPos - pX > 9) || (yPos - pY > 9) || (zPos - pZ > 9) || 
                 (uPos - pU > 9) || (vPos - pV > 9) || (wPos - pW > 9))
            {
                errTracker = true;
            // Verifies that max arm length is not exceeded
            } else if ( (xPos >= 450) || (yPos >= 450) || (zPos >= 450) ||
                        (uPos >= 450) || (vPos >= 450) || (wPos >= 450))
            {
                errTracker = true;
            }

            // Pop up an error box if the command is bad
            if (errTracker)
            {
                MessageBox.Show("Exceeded limitations",
                                "Error Message",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else
            {

                StringBuilder str;
                // Builds the HEX String
                str = strBldr(frameNum, xPos, yPos, zPos,
                                        uPos, vPos, wPos);

                // Lowers the Case of the String
                string sendStr = str.ToString().ToLower();

                // Form the BYTE ARRAY that is "actually" sent
                byte[] sendBuf = HexStringToByteArray(sendStr);

                // Send it!
                socket.SendTo(sendBuf, mboxIPE);

                // Send the command to the log
                logger.Write(DateTime.Now.ToString("hh:mm:ss.fff"));

                logger.WriteLine("," + xPos.ToString() + "," + yPos.ToString() +
                                 "," + zPos.ToString() + "," + uPos.ToString() +
                                 "," + vPos.ToString() + "," + wPos.ToString() + "," + sendStr);
                logger.Flush();

                // Increase _GLOBAL frame counter
                frameNum += 1;

                // Update the previous recorded position
                pX = xPos;
                pY = yPos;
                pZ = zPos;
                pU = uPos;
                pV = vPos;
                pW = wPos;
            }
        }
        

        /// <summary>
        /// Disconnect and Release the Socket
        /// </summary>
        private void Disconnect()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Disconnect(true);
        }

        // Called when the form is loaded
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        ///////////////////////////////////////////////////////////////////////////////
        /// HOME OF UTILITY FUNCTIONS
        ///////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Convert a HEX String into a BYTE array to be sent
        /// </summary>
        private byte[] HexStringToByteArray(string s)
        {
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
            {
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            }
            return buffer;
        }


        /// <summary>
        /// Creates the packet that is sent
        /// </summary>
        /// <param name="numFrame">Current Frame Number, should be continuous</param>
        /// <param name="_xPos">Position of the X Motor</param>
        /// <param name="_yPos">Position of the Y Motor</param>
        /// <param name="_zPos">Position of the Z Motor</param>
        /// <param name="_uPos">Position of the U Motor</param>
        /// <param name="_vPos">Position of the V Motor</param>
        /// <param name="_wPos">Position of the W Motor</param>
        /// <returns></returns>
        public StringBuilder strBldr(int numFrame, uint _xPos, uint _yPos, uint _zPos, uint _uPos, uint _vPos, uint _wPos)
        {
            StringBuilder str = new StringBuilder();

            //leadDistanceUnit = 5; // Convert.ToUInt16(RotLength.Text);
            //oneTurnPulseNum = 10000; // Convert.ToUInt16(RotPulses.Text);

            _xPos = Convert.ToUInt32(((Convert.ToUInt32(_xPos) + 5) / leadDistanceUnit) * oneTurnPulseNum); // 255;
            _yPos = Convert.ToUInt32(((Convert.ToUInt32(_yPos) + 5) / leadDistanceUnit) * oneTurnPulseNum); // 255;
            _zPos = Convert.ToUInt32(((Convert.ToUInt32(_zPos) + 5) / leadDistanceUnit) * oneTurnPulseNum); // 255;
            _uPos = Convert.ToUInt32(((Convert.ToUInt32(_uPos) + 5) / leadDistanceUnit) * oneTurnPulseNum); // 255;
            _vPos = Convert.ToUInt32(((Convert.ToUInt32(_vPos) + 5) / leadDistanceUnit) * oneTurnPulseNum); // 255;
            _wPos = Convert.ToUInt32(((Convert.ToUInt32(_wPos) + 5) / leadDistanceUnit) * oneTurnPulseNum); // 255;

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
    }
}
