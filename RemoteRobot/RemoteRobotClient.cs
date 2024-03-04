using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteRobot
{
    internal class RemoteRobotClient
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Remote Robot rising up...");

            ExoPlanetClient exoPlanetClient = new();
            ControlCenterClient controlCenterClient = new();

            RemoteRobot remoteRobot = new();

            string initResponse = exoPlanetClient.ReceiveData();
            Console.WriteLine($"Response from exoplanet: {initResponse}");
            remoteRobot.HandleResponse(initResponse);

            while (true)
            {
                Console.Write("Enter message to send to exoplanet: ");
                string? message = Console.ReadLine();
                exoPlanetClient.SendData(message);

                string response = exoPlanetClient.ReceiveData();
                Console.WriteLine($"Response from exoplanet: {response}");
                remoteRobot.HandleResponse(response);

                //TODO: Send data to control center
            }
        }
    }
}