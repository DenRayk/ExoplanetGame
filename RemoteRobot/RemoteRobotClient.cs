using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteRobot
{
    internal class RemoteRobotClient
    {
        private static ExoPlanetClient exoPlanetClient = new();
        private static ControlCenterClient controlCenterClient = new();

        private static RemoteRobot remoteRobot = new();

        private static Measure lastMeasure = new();
        private static PlanetSize currentPlanetSize = new();
        private static Position currentPosition = new();


        //TODO: Implement Constructor for RemoteRobotClient --> Call in main

        private static void Main(string[] args)
        {
            Console.WriteLine("Remote Robot rising up...");

            string initResponse = exoPlanetClient.ReceiveData();
            Console.WriteLine($"Response from exoplanet: {initResponse}");
            HandleResponse(initResponse);

            while (remoteRobot.isAlive)
            {
                Console.Write("Enter message to send to exoplanet: ");
                string? message = Console.ReadLine();
                exoPlanetClient.SendData(message);

                string response = exoPlanetClient.ReceiveData();
                Console.WriteLine($"Response from exoplanet: {response}");
                HandleResponse(response);
            }

            Console.WriteLine("Remote Robot shutting down...");
            exoPlanetClient.CloseConnection();
            controlCenterClient.CloseConnection();
        }

        private static void HandleResponse(string response)
        {
            if (string.IsNullOrEmpty(response)) return;

            string[] tokens = response.Split(':');
            string commandName = tokens[0];

            string[] parameters = tokens.Length > 1 ? tokens[1].Split('|') : Array.Empty<string>();

            try
            {
                switch (commandName)
                {
                    case "init":
                        currentPlanetSize = PlanetSize.Parse(tokens[1]);
                        controlCenterClient.SendData($"Init:{currentPlanetSize}");
                        break;

                    case "landed":
                    case "moved":
                        currentPosition = Position.Parse(tokens[1]);
                        controlCenterClient.SendData($"UpdatePostion:{currentPosition}");
                        break;

                    case "scanned":
                        lastMeasure.Ground = (Ground)Enum.Parse(typeof(Ground), parameters[1]);
                        controlCenterClient.SendData($"NewScan:{lastMeasure}|{currentPosition.X}|{currentPosition.Y}");
                        break;

                    case "rotated":
                        currentPosition.Direction = (Direction)Enum.Parse(typeof(Direction), parameters[0]);
                        controlCenterClient.SendData($"UpdatePostion:{currentPosition}");
                        break;

                    case "crashed":
                        remoteRobot.Crash();
                        controlCenterClient.SendData("crashed");
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
    }
}