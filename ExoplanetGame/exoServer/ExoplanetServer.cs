using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Exoplanet.exoServer
{
    internal class ExoplanetServer
    {
        private static TcpListener server;
        private static bool isRunning = true;

        private static void Main(string[] args)
        {
            server = new TcpListener(IPAddress.Any, 9999);
            server.Start();
            Console.WriteLine("Exoplanet server started...");

            while (isRunning)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Robot connected.");

                Thread clientThread = new Thread(HandleClient!);
                clientThread.Start(client);
            }
        }

        private static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            while (true)
            {
                int bytesRead;

                try
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                }
                catch
                {
                    break;
                }

                if (bytesRead == 0)
                {
                    break;
                }

                string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Received from robot: " + dataReceived);

                // Echo back to the robot
                byte[] response = Encoding.ASCII.GetBytes("Received: " + dataReceived);
                stream.Write(response, 0, response.Length);
            }

            client.Close();
            Console.WriteLine("Robot disconnected.");
        }
    }
}