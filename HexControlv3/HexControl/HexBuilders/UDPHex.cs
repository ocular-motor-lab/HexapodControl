using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace HexBuilders
{
    public class UDPHex : StandardFunctions
    {
        private UdpClient udpClient = new UdpClient();
        private IPEndPoint mboxIPE;
        private IPEndPoint localIPE;

        public static UDPHex Connect(
            string IPremote = "192.168.15.255",
            int UDPremote = 7408,
            string IPlocal = "192.168.15.100",
            int UDPlocal = 8410)
        {
            var connection = new UDPHex();

            connection.mboxIPE = new IPEndPoint(IPAddress.Parse(IPremote), UDPremote); // Where it's going (the .255 indicates it's a broadcast!)
            connection.localIPE = new IPEndPoint(IPAddress.Parse(IPlocal), UDPlocal); // Who we are (.100)

            connection.udpClient = new UdpClient();
            connection.udpClient.Client.Bind(connection.localIPE);
            return connection;
        }

        public async Task<byte[]> UdpSendReceive(byte[] bytes)
        {
            var receiveTask = Task.Run(
                () =>
                {
                    return udpClient.Receive(ref mboxIPE);
                });

            udpClient.Send(bytes, bytes.Length, mboxIPE);

            var response = await receiveTask;
            return response;
        }

        public void Disconnect()
        {
            //TODO
            try
            {
                udpClient.Close();
                udpClient.Dispose();
            }
            catch(Exception ex)
            {
                //TODO
            }

        }

    }
}