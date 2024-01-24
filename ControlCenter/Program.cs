using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ControlCenter
{
    internal class Program
    {
        private static async Task Main()
        {
            //    TcpListener tcpListener = null;

            //    try
            //    {
            //        // Set the IP address and port number on which the server will listen
            //        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            //        int port = 12345;

            //        tcpListener = new TcpListener(ipAddress, port);

            //        // Start listening for incoming client connections
            //        tcpListener.Start();
            //        Console.WriteLine($"Server is listening on {ipAddress}:{port}");

            //        // Handle incoming client connections asynchronously
            //        while (true)
            //        {
            //            TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();

            //            // Start a new thread or use a ThreadPool thread to handle the client
            //            Task.Run(() => HandleClient(tcpClient));
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"An error occurred: {ex.Message}");
            //    }
            //    finally
            //    {
            //        tcpListener?.Stop();
            //    }
            //}

            //private static void HandleClient(TcpClient tcpClient)
            //{
            //    try
            //    {
            //        Console.WriteLine($"Client connected: {tcpClient.Client.RemoteEndPoint}");

            //        NetworkStream networkStream = tcpClient.GetStream();

            //        byte[] buffer = new byte[1024];
            //        int bytesRead;

            //        while ((bytesRead = networkStream.Read(buffer, 0, buffer.Length)) > 0)
            //        {
            //            string data = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            //            Console.WriteLine($"Received from {tcpClient.Client.RemoteEndPoint}: {data}");

            //            // Echo the data back to the client
            //            byte[] responseBuffer = Encoding.ASCII.GetBytes($"Server received: {data}");
            //            networkStream.Write(responseBuffer, 0, responseBuffer.Length);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine($"An error occurred for client {tcpClient.Client.RemoteEndPoint}: {ex.Message}");
            //    }
            //    finally
            //    {
            //        tcpClient.Close();
            //        Console.WriteLine($"Client disconnected: {tcpClient.Client.RemoteEndPoint}");
            //    }
        }
    }
}