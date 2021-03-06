using System;
using System.Threading.Tasks;
using System.IO;

namespace HexBuilders
{
    public class Hexapod
    {

        public UDPHex udpHex = new UDPHex();
        private Consumer<string> recorder;

        private int messageCounter = 0;
        private int frameNum = 0;

        private string timeString;

        public string IPremote = "192.168.15.255"; // Where it's going (the .255 indicates it's a broadcast!)
        public int UDPremote = 7408; 
        public string IPlocal = "192.168.15.100"; // Who we are (.100)
        public int UDPlocal = 8410; 

        public string DataFolder { get; set; }

        public bool Connected { get; set; }

        public Hexapod()
        {
            // TODO: do the test of running a lot of commands one after the other and see if the response
            // changes when it cannot run them anymore.
        }

        public async Task asyncInitialize()
        {
            try
            {
                timeString = System.DateTime.Now.ToString("yy'_'M'_'dd'_'HH'_'mm");
                var file = Path.Combine(DataFolder, "Hexapod_" + timeString + "_" + "sent.csv");

                using (var sentCommands = new StreamWriter(file, true))
                {
                    sentCommands.WriteLine("Record Time, Send Time, FN Code, FN Time, X, Y, Z, V, U, W, Receive Time, FN Code, FN Time, X, Y, Z, V, U, W");
                    sentCommands.Flush();
                }

                recorder = new Consumer<string>(RecordToFile);
                var recorderTask = recorder.Start();
                Connected = true;

                udpHex = UDPHex.Connect(IPremote, UDPremote, IPlocal, UDPlocal);
                // TODO: request a lot of info about the hexapod. 
                await recorderTask;
            }
            finally
            {
                recorder = null;
                udpHex.Disconnect();
                udpHex = null;
            }
        }

        public async Task<string> asyncMove(double x, double y, double z, double u, double v, double w, int time)
        {
            if (udpHex == null) throw new InvalidOperationException("No UDP connection");

            try
            {
                // TODO: add timing parameters

                messageCounter++;
                frameNum++;
                var moveBytes = udpHex.MoveBuildCommand(frameNum, x, y, z, u, v, w, time);
                var sentCommand = udpHex.InterpretMoveCommandResponse(moveBytes);
                var bytesResponse = await udpHex.UdpSendReceive(moveBytes);
                string response;
                
                response = udpHex.InterpretMoveCommandResponse(bytesResponse);

                var logline = sentCommand + ", " + response;
                recorder.TryAdd(logline, messageCounter);

                return response;
            }
            catch (Exception)
            {
                this.Stop();

                throw;
            }
            finally
            {
            }
        }

        private async Task asyncReset()
        {
            if (udpHex == null) throw new InvalidOperationException("No UDP connection");

            try
            {
                // TODO: add timing parameters

                messageCounter++;
                frameNum++;
                var moveBytes = udpHex.MoveBuildCommand(frameNum, 0, 0, 0, 0, 0, 0,10);
                var sentCommand = udpHex.InterpretMoveCommandResponse(moveBytes);
                var bytesResponse = await udpHex.UdpSendReceive(moveBytes);
                var response = udpHex.InterpretMoveCommandResponse(bytesResponse);

                var logline = sentCommand + ", " + response;
                recorder.TryAdd(logline, messageCounter);
                frameNum = 0;
            }
            catch (Exception)
            {
                this.Stop();

                throw;
            }
            finally
            {
            }
        }

        public void Initialize()
        {
            asyncInitialize();
        }

        public void Move(double x, double y, double z, double u, double v, double w, int time)
        {

            var task = asyncMove(x, y, z, u, v, w, time);
            task.Wait();
            if (task.IsFaulted)
            {
                throw task.Exception;
            }
        }

        public void Reset()
        {
            asyncReset();
            //var task = 
            //task.Wait();
            //if ( task.IsFaulted)
            //{
            //    throw task.Exception;
            //}
        }

        public void Stop()
        {
            recorder.Stop();
            messageCounter = 0;
        }

        /// <summary>
        /// Determine a status from the hexapod
        /// </summary>
        /// <param name="RegType">Type of Registry: FN, DN, or CN</param>
        /// <param name="StartAddress">The Address of the register, convert from HEX to DEC</param>
        /// <param name="NumberParameters">How many Parameters past the initial address to read</param>
        /// <returns>A string containing relevant Registry Information/Values</returns>
        public async Task<string> ReadSetting(string RegType, int StartAddress, int NumberParameters)
        {
            if (udpHex == null) throw new InvalidOperationException("No UDP connection");

            try
            {
                messageCounter++;
                frameNum++;
                var readBytes = udpHex.RegistryReadCommand(RegType, StartAddress, NumberParameters);
                var bytesResponse = await udpHex.UdpSendReceive(readBytes);

                var response = udpHex.InterpretReadRegistryCommandResponse(bytesResponse);

                var logline = response + "\r\n";
                recorder.TryAdd(logline, messageCounter);
                return response;
            }
            catch (Exception)
            {
                this.Stop();

                throw;
            }
            finally
            {
            }
        }

        public async Task<string> ChangeSetting(string RegType, int StartAddress,
                                                int NumberParameters, int ParamCounter, int[] ParamValues)
        {
            if (udpHex == null) throw new InvalidOperationException("No UDP connection");

            try
            {
                messageCounter++;
                frameNum++;
                var readBytes = udpHex.RegistryWriteCommand(RegType, StartAddress, NumberParameters, ParamCounter, ParamValues);
                var bytesResponse = await udpHex.UdpSendReceive(readBytes);

                var response = udpHex.InterpretWriteRegistryCommandResponse(bytesResponse);

                var logline = "\r\n" + response + "\r\n";
                recorder.TryAdd(logline, messageCounter);
                return response;
            }
            catch (Exception)
            {
                this.Stop();

                throw;
            }
            finally
            {
            }
        }

        private bool RecordToFile(string logline)
        {
            //var timeString = System.DateTime.Now.ToString("yy'_'M'_'dd'_'HH'_'mm");
            var file = Path.Combine(DataFolder, "Hexapod_" + timeString + "_" + "sent.csv");

            using (var sentCommands = new StreamWriter(file, true))
            {
                sentCommands.WriteLine(DateTime.Now.ToString("HH:mm:ss.fffffff")+ "," + logline);
                sentCommands.Flush();
            }

            return true;
        }
    }
}
