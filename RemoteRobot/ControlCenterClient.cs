using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RemoteRobot
{
    internal class ControlCenterClient : TcpClient, ITcpClientMethods
    {
        private readonly IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        private readonly int port = 8888;
        private readonly NetworkStream stream;

        public ControlCenterClient()
        {
            Connect(ipAddress, port);
            stream = GetStream();
        }

        public void SendData(string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        public string ReceiveData()
        {
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            return Encoding.ASCII.GetString(buffer, 0, bytesRead);
        }

        public void CloseConnection()
        {
            stream.Close();
            Close();
        }
    }
}