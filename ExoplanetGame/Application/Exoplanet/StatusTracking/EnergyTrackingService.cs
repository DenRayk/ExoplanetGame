using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Exoplanet.StatusTracking
{
    internal class EnergyTrackingService : EnergyTrackingUseCase
    {
        private ExoplanetService exoplanetService;

        public EnergyTrackingService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public LoadResult LoadEnergy(RobotBase robot, int seconds, Weather weather)
        {
            LoadResult loadResult = new();

            if (!exoplanetService.ExoPlanet.RobotStatusManager.RobotsEnergy.ContainsKey(robot))
            {
                exoplanetService.ExoPlanet.RobotStatusManager.RobotsEnergy.Add(robot, robot.RobotInformation.MaxEnergy);
            }
            else
            {
                int energyLoad = CalculateEneryLoad(weather);

                for (int i = 0; i < seconds; i++)
                {
                    if (exoplanetService.ExoPlanet.RobotStatusManager.RobotsEnergy[robot] < robot.RobotInformation.MaxEnergy)
                    {
                        exoplanetService.ExoPlanet.RobotStatusManager.RobotsEnergy[robot] += energyLoad;

                        loadResult.IsSuccess = true;
                        loadResult.HasRobotSurvived = true;
                        loadResult.Message = $"RobotPositionManager energy loaded to {exoplanetService.ExoPlanet.RobotStatusManager.RobotsEnergy[robot]}%";
                        loadResult.EnergyLoad = exoplanetService.ExoPlanet.RobotStatusManager.RobotsEnergy[robot];
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
            if (!exoplanetService.ExoPlanet.RobotStatusManager.RobotsEnergy.ContainsKey(robot))
            {
                exoplanetService.ExoPlanet.RobotStatusManager.RobotsEnergy.Add(robot, robot.RobotInformation.MaxEnergy);
            }

            int energyConsumed = CalculateEneryConsumtion(robotAction);

            exoplanetService.ExoPlanet.RobotStatusManager.RobotsEnergy[robot] -= energyConsumed;
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
            if (!exoplanetService.ExoPlanet.RobotStatusManager.RobotsEnergy.ContainsKey(robot))
            {
                exoplanetService.ExoPlanet.RobotStatusManager.RobotsEnergy.Add(robot, robot.RobotInformation.MaxEnergy);
            }

            return exoplanetService.ExoPlanet.RobotStatusManager.RobotsEnergy[robot];
        }

        public bool DoesRobotHaveEnoughEneryToAction(RobotBase robot, RobotAction robotAction)
        {
            if (GetRobotEnergy(robot) < CalculateEneryConsumtion(robotAction))
            {
                return false;
            }

            return true;
        }
    }
}