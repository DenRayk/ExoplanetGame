﻿using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Application.Exoplanet
{
    internal class EnergyTrackingService : EnergyTrackingUseCase
    {
        private ExoPlanetBase exoPlanet;
        private Dictionary<RobotBase, int> robotEnergy = new();

        public EnergyTrackingService(ExoplanetService exoplanetService)
        {
            exoPlanet = exoplanetService.ExoPlanet;
        }

        public LoadResult LoadEnergy(RobotBase robot, int seconds, Weather weather)
        {
            robotEnergy = exoPlanet.RobotPositionManager.RobotStatusManager.RobotsEnergy;
            LoadResult loadResult = new();

            if (!robotEnergy.ContainsKey(robot))
            {
                robotEnergy.Add(robot, robot.RobotInformation.MaxEnergy);
            }
            else
            {
                int energyLoad = CalculateEneryLoad(weather);

                for (int i = 0; i < seconds; i++)
                {
                    if (robotEnergy[robot] < robot.RobotInformation.MaxEnergy)
                    {
                        Console.WriteLine($"Loading energy {robotEnergy[robot]}%...");
                        robotEnergy[robot] += energyLoad;

                        loadResult.IsSuccess = true;
                        loadResult.HasRobotSurvived = true;
                        loadResult.Message = $"RobotPositionManager energy loaded to {robotEnergy[robot]}%";
                        loadResult.EnergyLoad = robotEnergy[robot];
                    }
                    else
                    {
                        loadResult.IsSuccess = true;
                        loadResult.HasRobotSurvived = true;
                        loadResult.Message = $"RobotPositionManager fully loaded to {robot.RobotInformation.MaxEnergy}%";
                        loadResult.EnergyLoad = robot.RobotInformation.MaxEnergy;
                        break;
                    }
                    Thread.Sleep(1000);
                }
            }
            return loadResult;
        }

        private int CalculateEneryLoad(Weather weather)
        {
            switch (weather)
            {
                case Weather.SUNNY:
                    return 10;

                case Weather.WINDY:
                    return 10;

                case Weather.CLOUDY:
                    return 3;

                case Weather.RAINY:
                    return 3;

                case Weather.FOGGY:
                    return 2;

                case Weather.ASH_IN_THE_AIR:
                    return 2;

                default:
                    return 5;
            }
        }

        public void ConsumeEnergy(RobotBase robot, RobotAction robotAction)
        {
            if (!robotEnergy.ContainsKey(robot))
            {
                robotEnergy.Add(robot, robot.RobotInformation.MaxEnergy);
            }

            int energyConsumed = CalculateEneryConsumtion(robotAction);

            robotEnergy[robot] -= energyConsumed;
        }

        private int CalculateEneryConsumtion(RobotAction robotAction)
        {
            switch (robotAction)
            {
                case RobotAction.LAND:
                    return 5;

                case RobotAction.MOVE:
                    return 2;

                case RobotAction.ROTATE:
                    return 1;

                case RobotAction.SCAN:
                    return 2;

                case RobotAction.GETPOSITION:
                    return 1;

                default:
                    return 0;
            }
        }

        public int GetRobotEnergy(RobotBase robot)
        {
            if (!robotEnergy.ContainsKey(robot))
            {
                robotEnergy.Add(robot, robot.RobotInformation.MaxEnergy);
            }

            return robotEnergy[robot];
        }

        public bool DoesRobotHaveEnoughEneryToAction(RobotBase robot, RobotAction robotAction)
        {
            if (GetRobotEnergy(robot) < CalculateEneryConsumtion(robotAction))
            {
                Console.WriteLine($"RobotPositionManager {robot.RobotInformation.RobotID} does not have enough energy to perform action.");
                return false;
            }

            return true;
        }
    }
}