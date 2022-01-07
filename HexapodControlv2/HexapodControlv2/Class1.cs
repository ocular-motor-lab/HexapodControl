using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace HexapodControlv2
{

    public class Class1
    {
        // Mechanical Contraints (Can be changed, but do so at your own peril)
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
        private uint SFXOut = 0x1234;
        private uint analogOut = 0x5678abcd;

        // udp
        private IPEndPoint mboxIPE = null;
        private IPEndPoint recvIPE = null;
        private Socket socket = null;
        private EndPoint Remote = null;

        StreamWriter logger;
        StreamWriter errorLogger;
        public string userName = "omlab-admin";
        // Track whether there were any errors
        private bool errTracker = false;
        // Need to track the frame number
        private int frameNum = 0;


        /// <summary>
        /// Takes filename as an input for the connection log
        /// </summary>
        /// <param name="filename"></param>
        public void Connect(string filename = "UDPLog.csv")
        {
            String timeString = DateTime.Now.ToString("mm\\_ss");
            mboxIPE = new IPEndPoint(IPAddress.Parse("192.168.15.255"), 7408);
            recvIPE = new IPEndPoint(IPAddress.Parse("192.168.15.100"), 8410);
            errorLogger = new StreamWriter("C:\\Users\\" + userName + "\\Documents\\" + timeString + "ErrLog.txt");

            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                Remote = (EndPoint)(recvIPE);
            } catch (Exception e)
            {
                errorLogger.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff") + e);
                errorLogger.Flush();
                throw;
            }

            logger = new StreamWriter("C:\\Users\\" + userName + "\\Documents\\" + timeString + "_" + filename);

            logger.Write(DateTime.Now.ToString("hh:mm:ss.fff"));
            logger.WriteLine(",X,Y,Z,U,V,W,HEX String");
            logger.Flush();
        }

        public bool SendCommand(double xPos, double yPos, double zPos, double uPos, double vPos, double wPos)
        {

            errTracker = (xPos >= 450) || // Verifies that max arm length is not exceeded
                         (yPos >= 450) || // Verifies that max arm length is not exceeded
                         (zPos >= 450) || // Verifies that max arm length is not exceeded
                         (uPos >= 450) || // Verifies that max arm length is not exceeded
                         (vPos >= 450) || // Verifies that max arm length is not exceeded
                         (wPos >= 450);   // Verifies that max arm length is not exceeded




            if (errTracker)
            {
                //Disconnect();
                return errTracker;
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

                try {
                    // Send it!
                    socket.SendTo(sendBuf, mboxIPE);
                } catch (Exception e)
                {
                    errorLogger.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff") + e);
                    errorLogger.Flush();
                    throw;
                }

            // Increase _GLOBAL frame counter
            frameNum += 1;

                // Send the command to the log
                logger.Write(DateTime.Now.ToString("hh:mm:ss.fff"));

                logger.WriteLine("," + xPos.ToString("n3") + "," + yPos.ToString("n3") +
                                 "," + zPos.ToString("n3") + "," + uPos.ToString("n3") +
                                 "," + vPos.ToString("n3") + "," + wPos.ToString("n3") + "," + sendStr);
                logger.Flush();

                return errTracker;
            }
        }

        /// <summary>
        /// Disconnect and Release the Socket
        /// </summary>
        public void Disconnect()
        {
            try
            {
                socket.Shutdown(SocketShutdown.Send);
                //socket.Disconnect(true);
                logger.Flush();
                logger.Close();
                errorLogger.Flush();
                errorLogger.Close();
            }
            catch (Exception e)
            {
                errorLogger.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff") + e);
                errorLogger.Flush();
                errorLogger.Close();
                logger.Close();
                throw;
            }
        }


        /// <summary>
        /// Convert a HEX String into a BYTE array to be sent
        /// </summary>
        private byte[] HexStringToByteArray(string s)
        {
            byte[] buffer = new byte[s.Length / 2];
            try {
                for (int i = 0; i < s.Length; i += 2)
                {
                    buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
                }
            } catch (Exception e)
            {
                errorLogger.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff") + e);
                errorLogger.Flush();
                throw;
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
        public StringBuilder strBldr(int numFrame, double _xPos, double _yPos, double _zPos, double _uPos, double _vPos, double _wPos)
        {
            StringBuilder str = new StringBuilder();

            //leadDistanceUnit = 5; // Convert.ToUInt16(RotLength.Text);
            //oneTurnPulseNum = 10000; // Convert.ToUInt16(RotPulses.Text);

            uint xPos;
            uint yPos;
            uint zPos;
            uint uPos;
            uint vPos;
            uint wPos;

            xPos = Convert.ToUInt32(((float)_xPos + 5) / leadDistanceUnit * oneTurnPulseNum); // 255;
            yPos = Convert.ToUInt32(((float)_yPos + 5) / leadDistanceUnit * oneTurnPulseNum); // 255;
            zPos = Convert.ToUInt32(((float)_zPos + 5) / leadDistanceUnit * oneTurnPulseNum); // 255;
            uPos = Convert.ToUInt32(((float)_uPos + 5) / leadDistanceUnit * oneTurnPulseNum); // 255;
            vPos = Convert.ToUInt32(((float)_vPos + 5) / leadDistanceUnit * oneTurnPulseNum); // 255;
            wPos = Convert.ToUInt32(((float)_wPos + 5) / leadDistanceUnit * oneTurnPulseNum); // 255;

            try {
                str.Append(confirmCode.ToString("X4"));
                str.Append(passCode.ToString("X4"));
                str.Append(functionCode.ToString("X4"));
                str.Append(objectChannel.ToString("X4"));
                str.Append(whoAccept.ToString("X4"));
                str.Append(whoReply.ToString("X4"));
                str.Append(numFrame.ToString("X8"));
                str.Append(absTime.ToString("X8"));
                str.Append(xPos.ToString("X8"));
                str.Append(yPos.ToString("X8"));
                str.Append(zPos.ToString("X8"));
                str.Append(uPos.ToString("X8"));
                str.Append(vPos.ToString("X8"));
                str.Append(wPos.ToString("X8"));

                str.Append(SFXOut.ToString("X4"));
                str.Append(analogOut.ToString("X8"));
            }
            catch (Exception e)
            {
                errorLogger.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff") + e);
                errorLogger.Flush();
                throw;
            }

            return str;

        }

    }


}
