using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ControlCenter
{
    internal class ControlProxy
    {
        private static int nextRobotID = 1;

        private readonly TcpClient tcpClient;
        private NetworkStream networkStream;
        private Thread clientThread;

        private int robotID;

        public ControlProxy(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            clientThread = new Thread(HandleClient);
            clientThread.Start();
            robotID = nextRobotID++;
        }

        private void HandleClient(object? obj)
        {
            networkStream = tcpClient.GetStream();

            while (clientThread.IsAlive)
            {
                string dataReceived;
                try
                {
                    dataReceived = ReadFromRobot();
                }
                catch (Exception ex)
                {
                    break;
                }

            }

            tcpClient.Close();
            Console.WriteLine("Robot disconnected.");
        }

        private string ReadFromRobot()
        {
            byte[] buffer = new byte[1024];

            int bytesRead = networkStream.Read(buffer, 0, buffer.Length);
            string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Received from robot {robotID}: {dataReceived}");
            return dataReceived;
        }
    }
}
