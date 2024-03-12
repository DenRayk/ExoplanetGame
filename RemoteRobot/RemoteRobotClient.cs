﻿using System;
using System.Collections.Generic;
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

        //TODO: Implement Constructor for RemoteRobotClient --> Call in main

        private static void Main(string[] args)
        {
            Console.WriteLine("Remote Robot rising up...");

            string initResponse = exoPlanetClient.ReceiveData();
            Console.WriteLine($"Response from exoplanet: {initResponse}");
            remoteRobot.HandleResponse(initResponse);

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
            string[] parts = response.Split(':');
            string commandName = parts[0];
            string[] parameters = parts.Length > 1 ? parts[1].Split('|') : Array.Empty<string>();

            switch (commandName)
            {
                case "init":
                    Console.WriteLine($"Initializing with size: width = {parameters[1]}, height = {parameters[2]}");
                    //TODO: Init:SIZE|width|height
                    break;

                case "landed":
                    //TODO: UpdatePostion: change to position
                    Console.WriteLine($"Landed: ground = {parameters[1]}");
                    //TODO: UpdatePostion:POSITION|x|y|direction
                    break;

                case "scanned":
                    Console.WriteLine($"Scanned: ground = {parameters[1]}");
                    //TODO: NewScan:MEASURE|ground|temp
                    break;

                case "moved":
                    Console.WriteLine($"Moved: position = x:{parameters[1]}, y:{parameters[2]}, direction = {parameters[3]}");
                    //TODO: UpdatePostion:POSITION|x|y|direction
                    break;

                case "rotated":
                    Console.WriteLine($"Rotated: direction = {parameters[0]}");
                    //TODO: UpdatePostion:POSITION|x|y|direction
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
    }
}