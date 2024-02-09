using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using RemoteRobot.exo;
using RemoteRobot.message;
using RemoteRobot.tcp;

namespace RemoteRobot
{
    internal class RemoteRobot
    {
        private ExoTcpClient controlServerClient;
        private ExoTcpClient exoPlanetClient;
        private PlanetSize planetSize;
        private List<Position> allRobotPositions;
        private Position robotPosition;
        private Measure robotMeasure;

        public RemoteRobot()
        {
            StartClients();
        }

        private void StartClients()
        {
            allRobotPositions = new List<Position>();

            Console.WriteLine("Please enter IP-Address of Exo-Planet:");
            string exoPlanetIP = Console.ReadLine();

            if (string.IsNullOrEmpty(exoPlanetIP))
                exoPlanetIP = "localhost";

            StartExoPlanetClient(exoPlanetIP);
        }

        private void StartExoPlanetClient(string ip)
        {
            // Create an instance of ExoTcpClient
            controlServerClient = new ExoTcpClient(ip, 7777);

            // Subscribe to the MessageReceived event to handle incoming messages
            controlServerClient.MessageReceived += processReceivedDataFromExoPlanet;
        }

        private void processReceivedDataFromExoPlanet(string receivedData)
        {
            Console.WriteLine("Received: " + receivedData);

            ReceivedMessage receivedMessage = GenerateReceivedMessage(receivedData);

            switch (receivedMessage.GetDataType())
            {
                case DataType.SIZE:
                    planetSize = receivedMessage.GetMessageData().GetSize();
                    break;

                case DataType.POSITION:
                    robotPosition = receivedMessage.GetMessageData().GetPosition();
                    break;

                case DataType.MEASURE:
                    robotMeasure = receivedMessage.GetMessageData().GetMeasure();
                    break;

                case DataType.DIRECTION:
                    if (robotPosition == null)
                        return;

                    robotPosition.SetDir(receivedMessage.GetMessageData().GetDirection());
                    break;

                case DataType.CRASHED:
                    DestroyRobot();
                    break;

                case DataType.UNKNOWN:
                    break;
            }
        }

        private ReceivedMessage GenerateReceivedMessage(string receivedData)
        {
            ReceivedMessage receivedMessage = new ReceivedMessage();

            string[] splittedData = receivedData.Split(':');

            switch (splittedData[0])
            {
                case "init":
                    receivedMessage.SetDataType(DataType.SIZE);
                    break;

                case "landed":
                case "scaned":
                    receivedMessage.SetDataType(DataType.MEASURE);
                    break;

                case "moved":
                case "pos":
                    receivedMessage.SetDataType(DataType.POSITION);
                    break;

                case "rotated":
                    receivedMessage.SetDataType(DataType.DIRECTION);
                    break;

                case "crashed":
                    receivedMessage.SetDataType(DataType.CRASHED);
                    break;

                case "charged":
                    receivedMessage.SetDataType(DataType.ROBOTSTATUS);
                    break;

                default:
                    receivedMessage.SetDataType(DataType.UNKNOWN);
                    break;
            }

            if (receivedMessage.GetDataType() == DataType.UNKNOWN)
            {
                return receivedMessage;
            }

            switch (receivedMessage.GetDataType())
            {
                case DataType.SIZE:
                    receivedMessage.GetMessageData().SetSize(PlanetSize.Parse(splittedData[1]));
                    break;

                case DataType.MEASURE:
                    receivedMessage.GetMessageData().SetMeasure(Measure.Parse(splittedData[1]));
                    break;

                case DataType.POSITION:
                    receivedMessage.GetMessageData().SetPosition(Position.Parse(splittedData[1]));
                    break;

                case DataType.DIRECTION:
                    Direction.TryParse(splittedData[1], out Direction newDirection);
                    receivedMessage.GetMessageData().SetDirection(newDirection);
                    break;

                case DataType.ROBOTSTATUS:
                    // TODO: receivedMessage.GetMessageData().SetRobotStatus();
                    break;

                case DataType.CRASHED:
                case DataType.UNKNOWN:
                    break;
            }

            return receivedMessage;
        }

        private void DestroyRobot()
        {
            Console.WriteLine("See you later alligator <3");
            controlServerClient.Stop();
            exoPlanetClient.Stop();
            Environment.Exit(0);
        }

        public static void Main(string[] args)
        {
            new RemoteRobot();
        }
    }
}