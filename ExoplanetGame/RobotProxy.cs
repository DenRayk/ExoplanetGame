using System.Net.Sockets;
using System.Text;
using Exoplanet.exo;

namespace Exoplanet
{
    internal class RobotProxy : Robot
    {
        private static int nextRobotID = 1;

        private readonly TcpClient tcpClient;
        private NetworkStream networkStream;
        private Exoplanet exoplanet;
        private Thread clientThread;
        private int robotID;

        public RobotProxy(TcpClient tcpClient, Exoplanet exoplanet)
        {
            this.tcpClient = tcpClient;
            this.exoplanet = exoplanet;
            clientThread = new Thread(HandleClient);
            clientThread.Start();
            robotID = nextRobotID++;
        }

        private void HandleClient()
        {
            this.networkStream = tcpClient.GetStream();
            SendToRobot("init:" + exoplanet.getPlanetSize());

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

                GetCommand(dataReceived);
            }

            tcpClient.Close();
            Console.WriteLine("Robot disconnected.");
        }

        private void GetCommand(string command)
        {
            string[] tokens = command.Split(':');
            if (tokens[0] == "") return;

            try
            {
                Measure m;
                Rotation r;
                Direction d;
                Position pos = null;

                switch (tokens[0])
                {
                    case "exit":
                        exoplanet.Remove(this);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void SendToRobot(string message)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(message);
            networkStream.Write(buffer, 0, buffer.Length);
        }

        private string ReadFromRobot()
        {
            byte[] buffer = new byte[1024];

            int bytesRead = networkStream.Read(buffer, 0, buffer.Length);
            string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Received from robot {robotID}: {dataReceived}");
            return dataReceived;
        }

        public void InitRun(Planet planet, string name, Position position, NetworkStream networkStream)
        {
            throw new NotImplementedException();
        }

        public void Crash()
        {
            SendToRobot("crashed");
            clientThread.Interrupt();
        }

        public string GetLanderName()
        {
            return $"Robot {robotID}";
        }
    }
}