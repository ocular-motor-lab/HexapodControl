using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexBuilders
{
    public enum HexapodFunctionCode
    {

    }

    public class StandardFunctions
    {
        public StandardFunctions() { }

        // Mechanical Contraints (Can be changed, but do so at your own peril)
        public const uint accessDistanceUnit = 0;
        public const float leadDistanceUnit = 5;
        public const uint oneTurnPulseNum = 10000;

        // Control protocol packet
        public const ushort confirmCode = 0x55aa;
        public const ushort passCode = 0;
        public const ushort whoAccept = 0xFFFF;
        public const ushort whoReply = 0xFFFF;

        public ushort functionCode = 0x1401;//0x1301; //  = 0x1301;  = 
        public ushort objectChannel = 1;

        public const uint absTime = 10;
        public uint SFXOut = 0x1234;
        public uint analogOut = 0x5678abcd;
        public int frameNum = 0;

        private static List<Registers> AllRegisters()
        {
            List<Registers> RegList = new List<Registers>();
            RegList.Add(new Registers("FN", 0x000A, "0, 1", "1", 0, "How MBOX will STOP on FAULT: \r\n 0: Servo enabled, TURN OFF, STOP \r\n 1: Servo KEEP LOCATION, S/D"));
            RegList.Add(new Registers("FN", 0x000B, "10 - 10,000", "500 pulses", 0, "Error band for INITIAL position"));
            RegList.Add(new Registers("FN", 0x000C, "10 - 1,000", "100/300 RPM", 0, "Speed of motor at INITIAL position"));
            RegList.Add(new Registers("FN", 0x000D, "-1 - 10,000", "500 (100 pulses)", 0, "Distance from motor position origin to overtravel point (100 PULSES is the UNITS). When -1 it means moving each axis independentlyset motor positioning set motor positioning origin to very close to distance"));
            RegList.Add(new Registers("FN", 0x0010, "0, 1", "1", 0, "D/O SYNCH: \r\n D/O + PlayDATA update the running standard synchronously \r\n 0: Update D/O from CX \r\n 1: Update D/O from PlayDATA"));
            RegList.Add(new Registers("FN", 0x0011, "0, 1", "1", 0, "A/O SYNCH: \r\n DAC + PlayDATA update + run synchronously \r\n 0: Update from CX \r\n 1: Update from PlayDATA"));
            RegList.Add(new Registers("FN", 0x0012, "-1 - 10,000", "1200", 0, "Controls Playback speed \r\n <1000: Automatic - automatically adjust Playback Speed Mode \r\n >=1000: use FX to set params"));
            RegList.Add(new Registers("FN", 0x001D, "1 - 30,000", "100 ms", 0, "MBOX Playback Action controls. Playback speed. Auto-adjust delay tolerance time"));
            RegList.Add(new Registers("FN", 0x001E, "1 - 1,000", "50 (* 0.01)", 0, "Playback Speed Control K"));
            RegList.Add(new Registers("FN", 0x001F, "0 - 10,000", "50 ms", 0, "Playback Speed Control T"));
            RegList.Add(new Registers("FN", 0x0013, "10 - 30,000", "100 ms", 0, "JOG (INCHING) MODE \r\n When COMMAND's time point does not change; use this time"));
            RegList.Add(new Registers("FN", 0x0014, "0 - 300", "10 (* 0.1s)", 0, "BACKWARDS MODE \r\n COMMAND's time point is negative; use this time"));
            RegList.Add(new Registers("FN", 0x0015, "0 - 300", "30 (* 0.1s)", 0, "FAST FORWARD MODE \r\n If PLAYDELTATIME > set parameter (ie. This is a threshold) use FFWD mode"));
            RegList.Add(new Registers("FN", 0x0016, "1 - 300", "10 (* 0.1s)", 0, "if FFWD mode use this time"));
            RegList.Add(new Registers("FN", 0x0017, "1 - 300", "10 (* 0.1s)", 0, "EMERGENCY STOP \r\n Execution time is effective"));
            RegList.Add(new Registers("FN", 0x0018, "1 - 300", "10 (* 0.1s)", 0, "EMERGENCY STOP \r\n Execution time is cancelled"));
            RegList.Add(new Registers("FN", 0x001C, "0, 1", "1", 0, "UDP Buffer enabled \r\n 0: FIFO not used \r\n 1: FIFO used"));
            RegList.Add(new Registers("FN", 0x0020, "0 - 255", "192.168.15.201", 3, "MBOX IP Address"));
            RegList.Add(new Registers("FN", 0x0024, "0 - 32767", "7408", 1, "MBOX Send/Rceive Port"));
            RegList.Add(new Registers("FN", 0x0030, "0 - 255", "192.168.15.101", 3, "Host IP Address"));
            RegList.Add(new Registers("FN", 0x0034, "0 - 32767", "8410", 1, "Host Send/Rceive Port"));
            RegList.Add(new Registers("FN", 0x0040, "0 - 2", "0", 0, "MBOX ACTIVE REPORT \r\n Report Mode: \r\n 0: Prohibited \r\n 1: Active-> fixed interval \r\n 2: Fixed intervals if FAULT"));
            RegList.Add(new Registers("FN", 0x0041, "-30,000 - 30,000", "3", 0, "MBOX ACTIVE REPORT \r\n Report Interval: \r\n '+: Unit * 1000, lo-freq \r\n '-: Unit=1, hi-freq"));
            RegList.Add(new Registers("FN", 0x0042, "0 - 255", "0", 0, "MBOX Report DX Start Address"));
            RegList.Add(new Registers("FN", 0x0043, "0 - 32", "32", 0, "MBOX Report Length"));
            RegList.Add(new Registers("FN", 0x0051, "0 - 15", "2, 6", 1, "Ethernet Indicator Light \r\n Check Table for Translation"));
            RegList.Add(new Registers("FN", 0x0053, "50 - 10,000", "100 (* 4 ms)", 0, "Ethernet Timeout"));
            RegList.Add(new Registers("FN", 0x0061, "", "500", 0, "CAN Bus Communication Rate"));
            RegList.Add(new Registers("FN", 0x0069, "", "96", 0, "MOD Bus Communication Rate"));
            RegList.Add(new Registers("FN", 0x006A, "", "1", 0, "MOD Bus Node Address"));
            RegList.Add(new Registers("FN", 0x0070, "", "0", 7, "Fault History \r\n Check Table for Translation"));
            RegList.Add(new Registers("FN", 0x0078, "", "10,000, 10,000", 1, "Analog Out Range"));
            RegList.Add(new Registers("FN", 0x007A, "", "96, 97", 1, "Analog Out Channel"));
            RegList.Add(new Registers("FN", 0x007C, "", "0", 1, "Analog Out Zero"));
            RegList.Add(new Registers("FN", 0x007E, "", "0", 1, "Analog Out Bit/Setting"));
            RegList.Add(new Registers("FN", 0x0080, "", "", 15, "Digital Out Controls"));
            RegList.Add(new Registers("FN", 0x0090, "-8 - 1", "0", 0, "E-Stop Input Control: \r\n 0: Forced SHUT \r\n 1: Forced Enabling \r\n '-1~-8: Determined by Channel"));
            RegList.Add(new Registers("FN", 0x0091, "-3 - 2", "1", 0, "Way of Quickstop \r\n 0: Return Home \r\n 1: Current Position \r\n 2: Keep x/y/z current avg \r\n '-n: Keep n horizontal"));
            RegList.Add(new Registers("FN", 0x0017, "1 - 300", "10 (* 0.1s)", 0, "Execution Time (Effective/Cancelled)"));
            RegList.Add(new Registers("FN", 0x00A0, "", "", 15, "Expanded D/O Controls"));
            RegList.Add(new Registers("FN", 0x00C0, "0 - 30,000", "0 (* 0.1 mm)", 0, "CYLINDER \r\n Electric Cylinder Effective Travel: \r\n 0: No Limit/Protection"));
            RegList.Add(new Registers("FN", 0x00C1, "1 - 30,000", "2000", 0, "Pulses per 1 mm"));
            RegList.Add(new Registers("FN", 0x00C2, "1 - 1,000", "50 (* 0.1 mm)", 0, "Travel per turn"));
            RegList.Add(new Registers("FN", 0x00C7, "1 - 300", "50", 0, "Torque to Lock"));
            RegList.Add(new Registers("FN", 0x00C8, "Day, Hour, Minute, Second", "", 0, "Runtime"));
            RegList.Add(new Registers("FN", 0x00CF, "0 - 30,000", "0", 0, "Limit Runtime \r\n 0: No limit"));
            RegList.Add(new Registers("FN", 0x0100, "", "", 15, "Analog I/P Signals"));
            RegList.Add(new Registers("FN", 0x01FF, "", "19013", 0, "Version System Software"));

            return RegList;
        }

        public static List<Registers> RegList = AllRegisters();

        public class Registers
        {
            public Registers(string t, ushort n, string r, string dv, int cl, string desc)
            {
                RegisterType = t;
                RegisterNumber = n;
                RegRange = r;
                DefaultValue = dv;
                ConnectionLength = cl;
                Description = desc;
            }
            public string RegisterType { get; set; }
            public ushort RegisterNumber { get; set; }
            public string RegRange { get; set; }
            public string DefaultValue { get; set; }
            public int ConnectionLength { get; set; }
            public string Description { get; set; }
        }

        public byte[] MoveBuildCommand(int numFrame, double x, double y, double z, double u, double v, double w, int time=(int)absTime)
        {

            var errTracker = (x >= 500) || // Verifies that max arm length is not exceeded
                             (y >= 500) || // Verifies that max arm length is not exceeded
                             (z >= 500) || // Verifies that max arm length is not exceeded
                             (u >= 500) || // Verifies that max arm length is not exceeded
                             (v >= 500) || // Verifies that max arm length is not exceeded
                             (w >= 500);   // Verifies that max arm length is not exceeded

            if (errTracker)
            {
                x = y = z = v = u = w = 0;
            }

            uint xPos;
            uint yPos;
            uint zPos;
            uint uPos;
            uint vPos;
            uint wPos;

            xPos = Convert.ToUInt32(((float)x + 5) / leadDistanceUnit * oneTurnPulseNum); // 255;
            yPos = Convert.ToUInt32(((float)y + 5) / leadDistanceUnit * oneTurnPulseNum); // 255;
            zPos = Convert.ToUInt32(((float)z + 5) / leadDistanceUnit * oneTurnPulseNum); // 255;
            uPos = Convert.ToUInt32(((float)u + 5) / leadDistanceUnit * oneTurnPulseNum); // 255;
            vPos = Convert.ToUInt32(((float)v + 5) / leadDistanceUnit * oneTurnPulseNum); // 255;
            wPos = Convert.ToUInt32(((float)w + 5) / leadDistanceUnit * oneTurnPulseNum); // 255;

            //functionCode = 0x1301;
            StringBuilder str = new StringBuilder(); ;
            // Builds the HEX String
            str.Append(confirmCode.ToString("X4"));
            str.Append(passCode.ToString("X4"));
            str.Append(functionCode.ToString("X4"));
            str.Append(objectChannel.ToString("X4"));
            str.Append(whoAccept.ToString("X4"));
            str.Append(whoReply.ToString("X4"));
            str.Append(numFrame.ToString("X8"));
            str.Append(time.ToString("X8"));
            str.Append(xPos.ToString("X8"));
            str.Append(yPos.ToString("X8"));
            str.Append(zPos.ToString("X8"));
            str.Append(uPos.ToString("X8"));
            str.Append(vPos.ToString("X8"));
            str.Append(wPos.ToString("X8"));

            str.Append(SFXOut.ToString("X4"));
            str.Append(analogOut.ToString("X8"));

            // Lowers the Case of the String
            string sendStr = str.ToString().ToLower();


            // Form the BYTE ARRAY that is "actually" sent
            byte[] sendBuf = HexStringToByteArray(sendStr);


            return sendBuf;
        }

        public string InterpretMoveCommandResponse(byte[] recBuffer)
        {
            var temp_string = BitConverter.ToString(recBuffer);
            var FNCode = temp_string.Substring(11, 6).Replace("-", "");
            var timeCode = Convert.ToInt32(temp_string.Substring(47, 12).Replace("-", ""), 16);
            var x = Convert.ToInt32(temp_string.Substring(59, 12).Replace("-", ""), 16) * 5f / 10000 - 5;
            var y = Convert.ToInt32(temp_string.Substring(71, 12).Replace("-", ""), 16) * 5f / 10000 - 5;
            var z = Convert.ToInt32(temp_string.Substring(83, 12).Replace("-", ""), 16) * 5f / 10000 - 5;
            var w = Convert.ToInt32(temp_string.Substring(95, 12).Replace("-", ""), 16) * 5f / 10000 - 5;
            var v = Convert.ToInt32(temp_string.Substring(107, 12).Replace("-", ""), 16) * 5f / 10000 - 5;
            var u = Convert.ToInt32(temp_string.Substring(119, 12).Replace("-", ""), 16) * 5f / 10000 - 5;

            string results = (DateTime.Now.ToString("hh:mm:ss.fff") + ", " +
                              FNCode + ", " +
                              String.Format("{0}", timeCode) + ", " +
                              String.Format("{0:0.00}", x) + ", " +
                              String.Format("{0:0.00}", y) + ", " +
                              String.Format("{0:0.00}", z) + ", " +
                              String.Format("{0:0.00}", w) + ", " +
                              String.Format("{0:0.00}", v) + ", " +
                              String.Format("{0:0.00}", u)
                              );

            return results;

        }

        public byte[] RegistryReadCommand(string RegWriter, int StartAddress, int NumberParameters)
        {
            StringBuilder str = new StringBuilder();

            functionCode = 0x1101;
            switch (RegWriter)
            {
                case "DN":
                    objectChannel = 0;
                    break;
                case "FN":
                    objectChannel = 1;
                    break;
                default:
                    break;
            }

            str.Append(confirmCode.ToString("X4"));
            str.Append(passCode.ToString("X4"));
            str.Append(functionCode.ToString("X4"));
            str.Append(objectChannel.ToString("X4"));
            str.Append(whoAccept.ToString("X4"));
            str.Append(whoReply.ToString("X4"));

            str.Append((StartAddress).ToString("X4"));
            str.Append((NumberParameters).ToString("X4"));

            // Lowers the Case of the String
            string sendStr = str.ToString().ToLower();

            // Form the BYTE ARRAY that is "actually" sent
            return HexStringToByteArray(sendStr);
        }

        /// <summary>
        /// Creates the packet that is sent Write
        /// </summary>
        /// <returns>A StringBuilder</returns>
        public byte[] RegistryWriteCommand(string RegType, int StartAddress,
                                                 int NumberParameters, int ParamCounter, int[] ParamValues)
        {
            StringBuilder str = new StringBuilder();

            var objC = RegType;

            functionCode = 0x1201;
            switch (objC)
            {
                case "FNm":
                    objectChannel = 0;
                    break;
                case "FN":
                    objectChannel = 1;
                    break;
                case "CN":
                    objectChannel = 2;
                    break;
                default:
                    break;
            }

            str.Append(confirmCode.ToString("X4"));
            str.Append(passCode.ToString("X4"));
            str.Append(functionCode.ToString("X4"));
            str.Append(objectChannel.ToString("X4"));
            str.Append(whoAccept.ToString("X4"));
            str.Append(whoReply.ToString("X4"));

            str.Append((StartAddress).ToString("X4"));
            str.Append((NumberParameters).ToString("X4"));

            for (int i = 0; i < ParamCounter; i++)
            {
                str.Append((ParamValues[i]).ToString("X4"));
            }

            // Lowers the Case of the String
            string sendStr = str.ToString().ToLower();

            // Form the BYTE ARRAY that is "actually" sent
            byte[] sendBuf = HexStringToByteArray(sendStr);

            return sendBuf;
        }

        /// <summary>
        /// Interprets the response of 1 parameter
        /// </summary>
        /// <param name="recBuffer">The response from the hexapod</param>
        /// <returns>String interpretation of the hex byte array</returns>
        public string InterpretReadRegistryCommandResponse(byte[] recBuffer)
        {
            var temp_string = BitConverter.ToString(recBuffer);

            var sAddress = temp_string.Substring(35, 6).Replace("-", "");
            var numParams = temp_string.Substring(41, 6).Replace("-", "");
            var respond = Convert.ToInt32(temp_string.Substring(47, 6).Replace("-", ""), 16);

            sAddress = "0x" + sAddress;
            var value = RegList.First(item => item.RegisterNumber == Convert.ToUInt16(sAddress, 16));

            string results = ("Date/Time: " + DateTime.Now.ToString("hh:mm:ss.fff") + "\r\n" +
                              "Start Address: " + sAddress + "\r\n" +
                              "Number of Parameters: " + numParams + "\r\n" +
                              "Returned Response: " + String.Format("{0:0.00}", respond) + "\r\n" +
                              "Description: " + value.Description
                              );

            return results;

        }

        public string InterpretWriteRegistryCommandResponse(byte[] recBuffer)
        {
            var temp_string = BitConverter.ToString(recBuffer);

            var sAddress = temp_string.Substring(35, 6).Replace("-", "");
            var numParams = temp_string.Substring(41, 6).Replace("-", "");
            var respond = Convert.ToInt32(temp_string.Substring(47, 6).Replace("-", ""), 16);

            sAddress = "0x" + sAddress;
            var value = RegList.First(item => item.RegisterNumber == Convert.ToUInt16(sAddress, 16));

            string results = ("Date/Time: " + DateTime.Now.ToString("hh:mm:ss.fff") + "\r\n" +
                              "Start Address: " + sAddress + "\r\n" +
                              "Number of Parameters: " + numParams + "\r\n" +
                              "Returned Response: " + String.Format("{0:0.00}", respond) + "\r\n" +
                              "Description: " + value.Description
                              );

            return results;

        }

        

        public virtual byte[] HexStringToByteArray(string s)
        {
            byte[] buffer = new byte[s.Length / 2];

            for (int i = 0; i < s.Length; i += 2)
            {
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            }

            return buffer;
        }

        public static Registers searchReg(List<Registers> RegList, ushort RegValue)
        {
            var value = RegList.First(item => item.RegisterNumber == RegValue);

            return value;
        }

    }
}
