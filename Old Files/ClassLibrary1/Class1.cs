using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClassLibrary1
{
    
    public class Class1
    {
        //Action platform mechanical parameters
        //public uint AccessDistanceUnit { get; set; }//350 ActLength
        //public uint LeadDistanceUnit { get; set; }  //10 RotLength
        //public uint OneTurnPulseNum { get; set; }   //10000 RotPulse

        readonly private float leadDistanceUnit  = 5;
        readonly private uint oneTurnPulseNum    = 10000;

        //Control protocol packet
        private const ushort confirmCode = 0x55aa;
        private const ushort passCode = 0;
        private const ushort functionCode = 0x1301;
        private const ushort objectChannel = 1;
        private const ushort whoAccept = 0xFFFF;
        private const ushort whoReply = 0xFFFF;
        private const ushort UDPverification = 0;
        private uint absTime = 50;//millisecond
        private uint xPos = 1;
        private uint yPos = 1;
        private uint zPos = 1;
        private uint uPos = 1;
        private uint vPos = 1;
        private uint wPos = 1;
        //private uint baseDout1 = 0;
        //private uint baseDout2 = 0;
        //private const uint dac = 0;
        //private uint baseDout3 = 0;
        //private uint baseDout4 = 0;
        private uint SFXOut = 0x1234;
        private uint analogOut = 0x5678abcd;

        //udp
        private IPEndPoint mboxIPE = null;
        private Socket socket = null;

        public void connectUDP()
        {
            mboxIPE = new IPEndPoint(IPAddress.Parse("192.168.15.255"), 7408);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        public void constructPacket(uint s_xPos, uint s_yPos, uint s_zPos, uint s_uPos, uint s_vPos, uint s_wPos)
        {
            StringBuilder str = new StringBuilder();

            uint _xPos;// = (accessDistanceUnit / leadDistanceUnit) * oneTurnPulseNum * xPos; // 255;
            uint _yPos;// = (accessDistanceUnit / leadDistanceUnit) * oneTurnPulseNum * yPos; // 255;
            uint _zPos;// = (accessDistanceUnit / leadDistanceUnit) * oneTurnPulseNum * zPos; // 255;
            uint _uPos;// = (accessDistanceUnit / leadDistanceUnit) * oneTurnPulseNum * uPos; // 255;
            uint _vPos;// = (accessDistanceUnit / leadDistanceUnit) * oneTurnPulseNum * vPos; // 255;
            uint _wPos;// s = (accessDistanceUnit / leadDistanceUnit) * oneTurnPulseNum * wPos; // 255;

            _xPos = Convert.ToUInt32(((s_xPos + 5) / leadDistanceUnit) * oneTurnPulseNum * xPos); // 255;
            _yPos = Convert.ToUInt32(((s_yPos + 5) / leadDistanceUnit) * oneTurnPulseNum * yPos); // 255;
            _zPos = Convert.ToUInt32(((s_zPos + 5) / leadDistanceUnit) * oneTurnPulseNum * zPos); // 255;
            _uPos = Convert.ToUInt32(((s_uPos + 5) / leadDistanceUnit) * oneTurnPulseNum * uPos); // 255;
            _vPos = Convert.ToUInt32(((s_vPos + 5) / leadDistanceUnit) * oneTurnPulseNum * vPos); // 255;
            _wPos = Convert.ToUInt32(((s_wPos + 5) / leadDistanceUnit) * oneTurnPulseNum * wPos); // 255;


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
            //str.Append(baseDout1.ToString("X2"));
            //str.Append(baseDout4.ToString("X2"));
            //str.Append(dac.ToString("X8"));
            //str.Append(baseDout3.ToString("X2"));
            //str.Append(baseDout2.ToString("X2"));
            str.Append(SFXOut.ToString("X4"));
            str.Append(analogOut.ToString("X8"));

            string sendStr = str.ToString().ToLower();

            byte[] sendBuf = HexStringToByteArray(sendStr);
            socket.SendTo(sendBuf, mboxIPE);
            //Test_String.Text = sendStr;
            //Console.WriteLine(sendBuf);

        }

        public void Reset()
        {
            string str = "55aa000013010001ffffffff000000000000000000000000000000000000000000000000000000000000000012345678abcd";
            byte[] sendBuf = HexStringToByteArray(str);
            //Test_String.Text = str;
            socket.SendTo(sendBuf, mboxIPE);
        }

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
    }
}
