using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Exoplanet
{
    internal class RobotProxy
    {
        private static int nextRobotID = 1;

        private readonly TcpClient tcpClient;
        private NetworkStream networkStream;
        private Exoplanet exoplanet;
        private int robotID;

        public RobotProxy(TcpClient tcpClient, Exoplanet exoplanet)
        {
            this.tcpClient = tcpClient;
            this.exoplanet = exoplanet;
            Thread clientThread = new Thread(HandleClient);
            clientThread.Start();
            robotID = nextRobotID++;
        }

        private void HandleClient()
        {
            networkStream = tcpClient.GetStream();
            byte[] buffer = new byte[1024];

            SendToRobot("init:" + exoplanet.getPlanetSize());
            while (true)
            {
                int bytesRead;

                try
                {
                    bytesRead = networkStream.Read(buffer, 0, buffer.Length);
                }
                catch
                {
                    break;
                }

                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received from robot: " + dataReceived);

                SendToRobot(dataReceived);
            }

            tcpClient.Close();
            Console.WriteLine("Robot disconnected.");
        }

        private void SendToRobot(string message)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(message);
            networkStream.Write(buffer, 0, buffer.Length);
        }
    }
}