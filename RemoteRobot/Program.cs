using System.Net.Sockets;
using System.Text;

namespace RemoteRobot
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                // Set the IP address and port number to connect to
                string ipAddress = "127.0.0.1";
                int port = 12345;

                TcpClient tcpClient = new TcpClient(ipAddress, port);

                Console.WriteLine($"Connected to server at {ipAddress}:{port}");

                NetworkStream networkStream = tcpClient.GetStream();

                while (true)
                {
                    // Read user input and send it to the server
                    Console.Write("Enter message (or 'exit' to close): ");
                    string message = Console.ReadLine();

                    if (message.ToLower() == "exit")
                        break;

                    byte[] messageBuffer = Encoding.ASCII.GetBytes(message);
                    networkStream.Write(messageBuffer, 0, messageBuffer.Length);

                    // Receive and display the server's response
                    byte[] responseBuffer = new byte[1024];
                    int bytesRead = networkStream.Read(responseBuffer, 0, responseBuffer.Length);
                    string response = Encoding.ASCII.GetString(responseBuffer, 0, bytesRead);
                    Console.WriteLine($"Server response: {response}");
                }

                // Close the connection
                tcpClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}