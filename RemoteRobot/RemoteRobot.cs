using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using RemoteRobot.exo;
using RemoteRobot.tcp;

namespace RemoteRobot
{
    internal class RemoteRobot
    {
        private TcpClient controlServerClient;
        private TcpClient exoPlanetClient;
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

            Console.WriteLine("Please enter IP-Address of Control Center:");
            string controlServerIP = Console.ReadLine();

            if (string.IsNullOrEmpty(controlServerIP))
                controlServerIP = "localhost";

            StartControlServerClient(controlServerIP);

            Console.WriteLine("Please enter IP-Address of Exo-Planet:");
            string exoPlanetIP = Console.ReadLine();

            if (string.IsNullOrEmpty(exoPlanetIP))
                exoPlanetIP = "localhost";

            StartExoPlanetClient(exoPlanetIP);

            Console.WriteLine("Enter Command:");
            string command = Console.ReadLine();
            ProcessUserInput(command);
        }

        private void StartControlServerClient(string ip)
        {
            controlServerClient = new TcpClient(ip, 6666);

            controlServerClient.AddEventHandler(new TcpClientEventHandler()
            {
                OnMessage = line => ProcessReceivedDataFromControlServer(line),
                OnOpen = () => Console.WriteLine("Connected to ControlServer."),
                OnClose = () =>
                {
                    Console.WriteLine("Disconnected from ControlServer.");
                    Console.WriteLine("Reconnecting...");
                    controlServerClient.Connect();
                }
            });

            controlServerClient.Connect();
        }

        private void StartExoPlanetClient(string ip)
        {
            exoPlanetClient = new TcpClient(ip, 7777);

            exoPlanetClient.AddEventHandler(new TcpClientEventHandler()
            {
                OnMessage = line => ProcessReceivedDataFromExoPlanet(line),
                OnOpen = () => Console.WriteLine("Connected to Exo-Planet."),
                OnClose = () =>
                {
                    Console.WriteLine("Disconnected from Exo-Planet.");
                    Console.WriteLine("Reconnecting...");
                    exoPlanetClient.Connect();
                }
            });

            exoPlanetClient.Connect();
        }

        private void ProcessReceivedDataFromExoPlanet(string line)
        {
            Console.WriteLine("Received: " + line);

            ReceivedMessage receivedMessage = GenerateReceivedMessage(line);

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

            if (receivedMessage.GetDataType() == DataType.SIZE)
                return;

            SendRobotDataToControlServer();
        }

        private ReceivedMessage GenerateReceivedMessage(string receivedData)
        {
            ReceivedMessage receivedMessage = new ReceivedMessage();

            List<string> splittedData = new List<string>(receivedData.Split(':'));

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
                    receivedMessage.SetDataType(DataType.POSITION);
                    break;

                case "rotated":
                    receivedMessage.SetDataType(DataType.DIRECTION);
                    break;

                case "crashed":
                    receivedMessage.SetDataType(DataType.CRASHED);
                    break;

                case "pos":
                    receivedMessage.SetDataType(DataType.POSITION);
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
                    receivedMessage.GetMessageData().SetSize(Size.Parse(splittedData[1]));
                    break;

                case DataType.MEASURE:
                    receivedMessage.GetMessageData().SetMeasure(Measure.Parse(splittedData[1]));
                    break;

                case DataType.POSITION:
                    receivedMessage.GetMessageData().SetPosition(Position.Parse(splittedData[1]));
                    break;

                case DataType.DIRECTION:
                    receivedMessage.GetMessageData().SetDirection(Enum.Parse<Direction>(splittedData[1]));
                    break;

                case DataType.ROBOTSTATUS:
                    // TODO receivedMessage.GetMessageData().SetRobotstatus();
                    break;

                case DataType.CRASHED:
                case DataType.UNKNOWN:
                    break;
            }

            return receivedMessage;
        }

        private void ProcessUserInput(string command)
        {
            if (command == "move")
            {
                SendRobotDataToPlanet(command);
            }
            else if (command.Contains("land:"))
            {
                string[] splittedUserInput = command.Split(':');

                try
                {
                    if (Position.Parse(splittedUserInput[1]) == null)
                    {
                        Console.WriteLine("Unknown command!");
                        return;
                    }

                    robotPosition = Position.Parse(splittedUserInput[1]);

                    SendRobotDataToPlanet(command);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unknown command!");
                }
            }
            else if (command == "scan")
            {
                SendRobotDataToPlanet(command);
            }
            else if (command.Contains("rotate:"))
            {
                string[] splittedUserInput = command.Split(':');

                try
                {
                    if (Enum.TryParse(splittedUserInput[1], out Rotation rotation))
                    {
                        SendRobotDataToPlanet(command);
                    }
                    else
                    {
                        Console.WriteLine("Unknown command!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unknown command!");
                }
            }
            else if (command == "exit")
            {
                SendRobotDataToPlanet(command);
                DestroyRobot();
            }
            else if (command == "getpos")
            {
                SendRobotDataToPlanet(command);
            }
            else if (command.Contains("charge:"))
            {
                string[] splittedUserInput = command.Split(':');

                try
                {
                    if (double.TryParse(splittedUserInput[1].Replace(",", "."), out double chargeValue))
                    {
                        SendRobotDataToPlanet(command);
                    }
                    else
                    {
                        Console.WriteLine("Unknown command!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unknown command!");
                }
            }
            else
            {
                Console.WriteLine("Unknown command!");
            }

            Console.WriteLine("Enter Command:");
            string cmd = Console.ReadLine();
            ProcessUserInput(cmd);
        }

        private void SendRobotDataToPlanet(string command)
        {
            Console.WriteLine("Send: " + command);
            exoPlanetClient.Send(command);
        }

        private void SendRobotDataToControlServer()
        {
            StringBuilder messageToSend = new StringBuilder();

            messageToSend.Append("RobotInformation:");
            messageToSend.Append(robotPosition.ToString());
            messageToSend.Append(":");
            messageToSend.Append(robotMeasure.ToString());

            Console.WriteLine("Send: " + messageToSend.ToString());
            controlServerClient.Send(messageToSend.ToString());
        }

        private void DestroyRobot()
        {
            Console.WriteLine("See you later alligator <3");
            controlServerClient.Close();
            exoPlanetClient.Close();
            Environment.Exit(0);
        }

        private void ProcessReceivedDataFromControlServer(string receivedData)
        {
            Console.WriteLine("Received: " + receivedData);

            List<string> splittedData = new List<string>(receivedData.Split(':'));

            splittedData.RemoveAt(0);

            allRobotPositions.Clear();

            foreach (string position in splittedData)
            {
                allRobotPositions.Add(Position.Parse(position));
            }
        }

        public static void Main(string[] args)
        {
            new RemoteRobot();
        }
    }
}