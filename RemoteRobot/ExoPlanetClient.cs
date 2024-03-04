using System.Net.Sockets;
using System.Net;
using System.Text;

namespace RemoteRobot
{
    internal class ExoPlanetClient : TcpClient
    {
        private readonly IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        private readonly int port = 9999;

        public ExoPlanetClient()
        {
            Connect(ipAddress, port);
            RemoteRobot remoteRobot = new RemoteRobot();
            using (NetworkStream stream = GetStream())
            {
                Console.WriteLine("Connected to exoplanet server.");

                // Receive initial message from the server
                byte[] initBuffer = new byte[1024];
                int initBytesRead = stream.Read(initBuffer, 0, initBuffer.Length);
                string initResponse = Encoding.ASCII.GetString(initBuffer, 0, initBytesRead);
                Console.WriteLine($"Response from exoplanet: {initResponse}");
                remoteRobot.HandleResponse(initResponse);

                while (true)
                {
                    // Send Data
                    Console.Write("Enter message to send to exoplanet: ");
                    string? message = Console.ReadLine();

                    if (string.IsNullOrEmpty(message))
                    {
                        break;
                    }

                    byte[] data = Encoding.ASCII.GetBytes(message);
                    stream.Write(data, 0, data.Length);

                    // Receive Data
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"Response from exoplanet: {response}");
                    remoteRobot.HandleResponse(response);
                }
            }
        }

        private static void Main(string[] args)
        {
            ExoPlanetClient exoPlanetClient = new();
        }
    }
}