using System.Net.Sockets;
using System.Text;

namespace RemoteRobot
{
    internal class RemoteRobot
    {
        private static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1", 9999);

            NetworkStream stream = client.GetStream();

            Console.WriteLine("Connected to exoplanet server.");

            while (true)
            {
                Console.Write("Enter message to send to exoplanet: ");
                string? message = Console.ReadLine();

                if (message != null)
                {
                    byte[] data = Encoding.ASCII.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                }

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Response from exoplanet: " + response);
            }

            client.Close();
        }
    }
}