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

        public LoadResult LoadEnergy(IRobot robot, int seconds, Weather weather)
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
            return weather switch
            {
                Weather.SUNNY => 10,
                Weather.WINDY => 10,
                Weather.CLOUDY => 3,
                Weather.RAINY => 3,
                Weather.FOGGY => 2,
                Weather.ASH_IN_THE_AIR => 2,
                _ => 5
            };
        }

        public void ConsumeEnergy(IRobot robot, RobotAction robotAction)
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
            return robotAction switch
            {
                RobotAction.LAND => 5,
                RobotAction.MOVE => 2,
                RobotAction.ROTATE => 1,
                RobotAction.SCAN => 2,
                RobotAction.GETPOSITION => 1,
                _ => 0
            };
        }

        public int GetRobotEnergy(IRobot robot)
        {
            if (!exoplanetService.ExoPlanet.RobotStatusManager.RobotsEnergy.ContainsKey(robot))
            {
                exoplanetService.ExoPlanet.RobotStatusManager.RobotsEnergy.Add(robot, robot.RobotInformation.MaxEnergy);
            }

            return exoplanetService.ExoPlanet.RobotStatusManager.RobotsEnergy[robot];
        }

        public bool DoesRobotHaveEnoughEneryToAction(IRobot robot, RobotAction robotAction)
        {
            if (GetRobotEnergy(robot) < CalculateEneryConsumtion(robotAction))
            {
                return false;
            }

            return true;
        }
    }
}