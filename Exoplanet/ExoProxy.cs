using System.Net.Sockets;
using System.Text;
using Exoplanet.exo;
using Exoplanet.Models;

namespace Exoplanet
{
    internal class ExoProxy : IRobot
    {
        private static int nextRobotID = 1;

        private readonly TcpClient tcpClient;
        private NetworkStream networkStream;
        private Thread clientThread;
        private bool threadShouldStop;

        private Exoplanet exoplanet;
        private int robotID;

        public ExoProxy(TcpClient tcpClient, Exoplanet exoplanet)
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

            try
            {
                while (!threadShouldStop)
                {
                    string dataReceived = ReadFromRobot();
                    GetCommand(dataReceived);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in robot {robotID}: {ex.Message}");
            }
            finally
            {
                tcpClient.Close();
                Console.WriteLine($"Connection with robot {robotID} closed");
            }
        }

        private void GetCommand(string command)
        {
            string[] tokens = command.Split(':');
            if (tokens[0] == "") return;

            try
            {
                Measure measure;
                Rotation rotation;
                Direction? direction;
                Position? position = null;

                switch (tokens[0])
                {
                    case "land":
                        if (tokens.Length != 2) return;
                        position = Position.Parse(tokens[1]);
                        measure = exoplanet.Land(this, position);
                        SendToRobot($"landed:{position}");
                        break;

                    case "getpos":
                        position = exoplanet.GetPosition(this);
                        SendToRobot($"position:{position}");
                        break;

                    case "rotate":
                        if (tokens.Length != 2) return;
                        rotation = (Rotation)Enum.Parse(typeof(Rotation), tokens[1]);
                        direction = exoplanet.Rotate(this, rotation);
                        SendToRobot($"rotated:{direction}");
                        break;

                    case "move":
                        position = exoplanet.Move(this);
                        SendToRobot($"moved:{position}");
                        break;

                    case "scan":
                        measure = exoplanet.Scan(this);
                        SendToRobot($"scanned:{measure}");
                        break;

                    case "exit":
                        exoplanet.Remove(this);
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
            threadShouldStop = true;
        }

        public string GetLanderName()
        {
            return $"IRobot {robotID}";
        }
    }
}