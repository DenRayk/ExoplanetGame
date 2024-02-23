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
            networkStream = tcpClient.GetStream();
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
                Measure measure;
                Rotation rotation;
                Direction direction;
                Position? position = null;

                switch (tokens[0])
                {
                    case "exit":
                        exoplanet.Remove(this);
                        break;

                    case "land":
                        if (tokens.Length != 2) return;
                        position = Position.Parse(tokens[1]);
                        measure = exoplanet.Land(this, position);
                        SendToRobot("landed:" + measure);
                        break;

                    default:
                        SendToRobot(command);
                        return;
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