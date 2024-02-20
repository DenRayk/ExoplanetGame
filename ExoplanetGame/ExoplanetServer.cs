using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Exoplanet
{
    internal class ExoplanetServer
    {
        private readonly TcpListener server = new(IPAddress.Any, 9999);
        private bool isRunning = true;

        public void Start()
        {
            server.Start();
            Console.WriteLine("Exoplanet server started...");

            while (isRunning)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Robot connected.");

                Thread clientThread = new Thread(HandleClient);
                clientThread.Start(client);
            }
        }

        private void HandleClient(object? obj)
        {
            TcpClient client = (TcpClient)obj!;
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

        public void Stop()
        {
            isRunning = false;
            server.Stop();
        }
    }
}