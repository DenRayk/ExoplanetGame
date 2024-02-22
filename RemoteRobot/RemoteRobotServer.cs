using System.Net.Sockets;
using System.Net;
using System.Text;

namespace RemoteRobot
{
    internal class RemoteRobotServer
    {
        private readonly IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        private readonly int port = 9999;

        public void Start()
        {
            using (TcpClient client = new TcpClient())
            {
                client.Connect(ipAddress, port);

                using (NetworkStream stream = client.GetStream())
                {
                    Console.WriteLine("Connected to exoplanet server.");

                    while (true)
                    {
                        Console.Write("Enter message to send to exoplanet: ");
                        string? message = Console.ReadLine();

                        if (string.IsNullOrEmpty(message))
                        {
                            break;
                        }

                        byte[] data = Encoding.ASCII.GetBytes(message);
                        stream.Write(data, 0, data.Length);

                        byte[] buffer = new byte[1024];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        Console.WriteLine($"Response from exoplanet: {response}");
                    }
                }
            }
        }
    }
}