using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGame.Application.Exoplanet.StatusTracking
{
    internal class HeatTrackingService : HeatTrackingUseCase
    {
        private ExoplanetService exoplanetService;

        public HeatTrackingService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public void PerformAction(IRobot robot, RobotAction robotAction, Topography topography)
        {
            if (!exoplanetService.ExoPlanet.RobotStatusManager.RobotHeatLevels.ContainsKey(robot))
            {
                exoplanetService.ExoPlanet.RobotStatusManager.RobotHeatLevels[robot] = 0;
            }

            double heatGain = CalculateHeatGainDependentOnFieldAndAction(robot, robotAction, topography);

            exoplanetService.ExoPlanet.RobotStatusManager.RobotHeatLevels[robot] += heatGain;

            if (exoplanetService.ExoPlanet.RobotStatusManager.RobotHeatLevels[robot] >= robot.RobotInformation.MaxHeat)
            {
                throw new RobotOverheatException($"{robot.GetLanderName()} has overheated, please wait...");
            }
        }

        public void PerformAction(IRobot robot, RobotAction robotAction, Topography topography, Position landPosition)
        {
            if (!exoplanetService.ExoPlanet.RobotStatusManager.RobotHeatLevels.ContainsKey(robot))
            {
                exoplanetService.ExoPlanet.RobotStatusManager.RobotHeatLevels[robot] = 0;
            }

            double heatGain = CalculateHeatGainDependentOnFieldAndAction(landPosition, robotAction, topography);

            exoplanetService.ExoPlanet.RobotStatusManager.RobotHeatLevels[robot] += heatGain;

            if (exoplanetService.ExoPlanet.RobotStatusManager.RobotHeatLevels[robot] >= robot.RobotInformation.MaxHeat)
            {
                throw new RobotOverheatException($"{robot.GetLanderName()} has overheated, please wait...");
            }
        }

        public void WaterCoolDown(IRobot robot)
        {
            if (exoplanetService.ExoPlanet.RobotStatusManager.RobotHeatLevels.ContainsKey(robot))
            {
                exoplanetService.ExoPlanet.RobotStatusManager.RobotHeatLevels[robot] = 0;
            }
        }

        private double CalculateHeatGainDependentOnFieldAndAction(IRobot robot, RobotAction robotAction, Topography topography)
        {
            return CalculateHeatGainDependentOnFieldAndAction(robot.RobotInformation.Position, robotAction, topography);
        }

        private double CalculateHeatGainDependentOnFieldAndAction(Position landPosition, RobotAction robotAction, Topography topography)
        {
            double heatGain = topography.GetMeasureAtPosition(landPosition).Ground switch
            {
                Ground.WATER => 0,
                Ground.ROCK => 5,
                Ground.SAND => 10,
                Ground.LAVA => 20,
                Ground.GRAVEL => 5,
                Ground.PLANT => 5,
                Ground.MUD => 8,
                Ground.SNOW => 0,
                Ground.ICE => 0,
                _ => 0
            };

            if (robotAction == RobotAction.LAND)
                heatGain *= 1.5;
            else if (robotAction == RobotAction.MOVE)
                heatGain *= 1.2;
            else if (robotAction == RobotAction.ROTATE)
                heatGain *= 1.1;
            else if (robotAction == RobotAction.SCAN)
                heatGain *= 1.3;
            else if (robotAction == RobotAction.GETPOSITION) heatGain *= 1.1;

            return heatGain;
        }
    }
}