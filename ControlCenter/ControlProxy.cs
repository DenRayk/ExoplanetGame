using System.Net.Sockets;
using System.Text;
using ControlCenter.exo;

namespace ControlCenter
{
    internal class ControlProxy
    {
        private static int nextRobotID = 1;

        private readonly TcpClient tcpClient;
        private NetworkStream networkStream;
        private Thread clientThread;
        private bool threadShouldStop;

        private ControlCenter controlCenter;
        private int robotID;

        public ControlProxy(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            controlCenter = new ControlCenter();
            clientThread = new Thread(HandleClient);
            clientThread.Start();
            robotID = nextRobotID++;
        }

        private void HandleClient(object? obj)
        {
            networkStream = tcpClient.GetStream();

            try
            {
                while (!threadShouldStop)
                {
                    string dataReceived = ReadFromRobot();
                    GetMessage(dataReceived);
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

        private void GetMessage(string dataReceived)
        {
            string[] tokens = dataReceived.Split(':');
            string commandName = tokens[0];

            string[] parameters = tokens.Length > 1 ? tokens[1].Split('|') : Array.Empty<string>();

            try
            {
                switch (commandName)
                {
                    case "Init":
                        controlCenter.Init(PlanetSize.Parse(tokens[1]));
                        break;

                    case "NewScan":
                        controlCenter.AddMeasure(ControlMeasure.Parse(tokens[1]));
                        break;

                    case "crashed":
                        Crash();
                        break;

                    default:
                        Console.WriteLine($"Unknown command: {commandName}");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error handling command '{commandName}': {e}");
                throw;
            }
        }

        private void Crash()
        {
            Console.WriteLine($"Robot {robotID} crashed");
            threadShouldStop = true;
        }

        private string ReadFromRobot()
        {
            byte[] buffer = new byte[1024];

            int bytesRead = networkStream.Read(buffer, 0, buffer.Length);
            string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"Received from robot {robotID}: {dataReceived}");
            return dataReceived;
        }
    }
}