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

        /// <summary>
        /// takes in desired height and degree rotations, converts
        /// them to displacement of motor arms.
        /// </summary>
        public double[] CalculateArmLength(double X, double Y, double Z, double phi, double theta, double psi, double RefX, double RefY, double RefZ)
        {
            // CONSTS
            double LongBottom = 146.5; // cm
            double ShortBottom = 31.5; // cm
            double LongTop = 162; // cm
            double ShortTop = 31.5; // cm

            double RestHeight = 89; // cm;
            double MinArmLength = 114;
            double MaxArmLength = 155;

            double[] x = new double[3] { X, Y, Z };

            double[] q = new double[6] { X, Y, Z, phi, theta, psi };
            phi = (Math.PI / 180) * phi;
            theta = (Math.PI / 180) * theta;
            psi = (Math.PI / 180) * psi;

            double[,] Rwp = new double[3, 3]
                {
                    { Math.Cos(theta) * Math.Cos(phi)  , Math.Cos(theta) * Math.Sin(phi) * Math.Sin(psi) - Math.Sin(theta) * Math.Cos(psi) , Math.Cos(theta) * Math.Sin(phi) * Math.Cos(psi) + Math.Sin(theta) * Math.Sin(psi) },
                    { Math.Sin(theta) * Math.Cos(phi)  , Math.Sin(theta) * Math.Sin(phi) * Math.Sin(psi) + Math.Cos(theta) * Math.Cos(psi) , Math.Sin(theta) * Math.Sin(phi) * Math.Cos(psi) - Math.Cos(theta) * Math.Sin(psi)},
                    { -Math.Sin(phi)                   , Math.Cos(phi) * Math.Sin(psi)                                                     , Math.Cos(phi) * Math.Cos(psi)}
                };

            double RBottom = Math.Sqrt(Math.Pow((LongBottom / 2), 2) + Math.Pow((ShortBottom + LongBottom / 2), 2) / 3);
            double DBottom = 2 * (180 / Math.PI) * Math.Asin(ShortBottom / 2 / RBottom);
            double RTop = Math.Sqrt(Math.Pow((LongTop / 2), 2) + Math.Pow((ShortTop + LongTop / 2), 2) / 3);
            double DTop = 2 * (180 / Math.PI) * Math.Asin(ShortTop / 2 / RTop);
            double[] ArmAnglesBottom = new double[6] { 60 + (0 - DBottom / 2), 60 + (0 + DBottom / 2),
                                                       60 + ((120) - DBottom / 2), 60 + ((120) + DBottom / 2),
                                                       60 + ((240) - DBottom / 2), 60 + ((240) + DBottom / 2) };

            double[] ArmAnglesTop = new double[6] { (0 + DTop / 2),
                                                    ((120) - DTop / 2), ((120) + DTop / 2),
                                                    ((240) - DTop / 2), ((240) + DTop / 2),
                                                    (0 - DTop / 2) };


            double[,] biw = new double[3, 6];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                        biw[j, i] = RBottom * Math.Cos((Math.PI / 180) * ArmAnglesBottom[i]);
                    else if (j == 1)
                        biw[j, i] = RBottom * Math.Sin((Math.PI / 180) * ArmAnglesBottom[i]);
                    else
                        biw[j, i] = 0;
                }
            }

            double[,] aip = new double[3, 6];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                        aip[j, i] = RTop * Math.Cos((Math.PI / 180) * ArmAnglesTop[i]);
                    else if (j == 1)
                        aip[j, i] = RTop * Math.Sin((Math.PI / 180) * ArmAnglesTop[i]);
                    else
                        aip[j, i] = 0;
                }
            }

            //biw = [RBottom * cosd(ArmAnglesBottom); RBottom* sind(ArmAnglesBottom); zeros(size(ArmAnglesBottom))];
            //positions of the arms at the top
            //aip = [RTop * cosd(ArmAnglesTop); RTop* sind(ArmAnglesTop); zeros(size(ArmAnglesTop))];
            double[] RefCombo = new double[3] { RefX, RefY, RefZ };
            double[,] ai = pluser(RepMat(x, 1, 6), pluser(timeser(Rwp, minuser(aip, RepMat(RefCombo, 1, 6))), RepMat(RefCombo, 1, 6)));
            double[] RestHeightCombo = new double[3] { 0, 0, RestHeight };
            double[,] aizero = pluser(RepMat(RestHeightCombo, 1, 6), aip);
            double[] sumHolder = new double[6];

            double[] restingArmLengths = new double[6];
            double[] currentArmLengths = new double[6];
            restingArmLengths = summerSqrt(elementTimeser(minuser(biw, aizero), minuser(biw, aizero)));
            currentArmLengths = summerSqrt(elementTimeser(minuser(biw, ai), minuser(biw, ai)));

            double[] ArmExtensionMMs = new double[6];
            ArmExtensionMMs = scalerOp(scalerOp(currentArmLengths, "minus", MinArmLength), "times", 10);

            return ArmExtensionMMs;
        }

        // HELPER FUNCTIONS
        private double[] scalerOp(double[] vectory, string operation, double value)
        {
            double[] output = new double[6];
            if (operation == "times")
            {
                for (int i = 0; i < vectory.Length; i++)
                {
                    output[i] = vectory[i] * value;
                }
            }
            else if (operation == "minus")
            {
                for (int i = 0; i < vectory.Length; i++)
                {
                    output[i] = vectory[i] - value;
                }
            }
            return output;
        }

        private double[] summerSqrt(double[,] matrix)
        {
            double[] sumHolder = new double[6];

            double tempSum = 0;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    tempSum += matrix[j, i];
                }
                sumHolder[i] = Math.Sqrt(tempSum);
                tempSum = 0;
            }

            return sumHolder;
        }

        private double[,] minuser(double[,] first, double[,] second)
        {
            double[,] output = new double[first.GetLength(0), first.GetLength(1)];

            for (int i = 0; i < first.GetLength(0); i++)
            {
                for (int j = 0; j < first.GetLength(1); j++)
                {
                    output[i, j] = first[i, j] - second[i, j];
                }
            }

            return output;
        }

        private double[,] pluser(double[,] first, double[,] second)
        {
            double[,] output = new double[first.GetLength(0), first.GetLength(1)];

            for (int i = 0; i < first.GetLength(0); i++)
            {
                for (int j = 0; j < first.GetLength(1); j++)
                {
                    output[i, j] = first[i, j] + second[i, j];
                }
            }

            return output;
        }
        private double[,] elementTimeser(double[,] first, double[,] second)
        {
            double[,] output = new double[first.GetLength(0), first.GetLength(1)];

            for (int i = 0; i < first.GetLength(0); i++)
            {
                for (int j = 0; j < first.GetLength(1); j++)
                {
                    output[i, j] = first[i, j] * second[i, j];
                }
            }

            return output;
        }

        private double[,] timeser(double[,] first, double[,] second)
        {
            //double[,] output = new double[first.GetLength(0), first.GetLength(1)];
            double[,] c = new double[first.GetLength(0), second.GetLength(1)];

            for (int i = 0; i < first.GetLength(0); i++)
            {
                for (int j = 0; j < second.GetLength(1); j++)
                {
                    c[i, j] = 0;
                    for (int k = 0; k < first.GetLength(1); k++)
                    {
                        c[i, j] += first[i, k] * second[k, j];
                    }
                    //output[i, j] = first[i, j] - second[i, j];
                }
            }

            return c;
        }

        private double[,] RepMat(double[] Inputer, int dim, int numReps)
        {
            double[,] repmat = new double[Inputer.Length, numReps];

            if (dim == 1)
            {
                for (int i = 0; i < Inputer.Length; i++)
                {
                    for (int j = 0; j < numReps; j++)
                    {
                        repmat[i, j] = Inputer[i];
                    }
                }
            }

            return repmat;
        }

    }
}
