using ExoplanetGame.Robot;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Exoplanet;

public class LandController(RobotManager robotManager, RobotStatusManager robotStatusManager)
{
    public PositionResult LandRobot(RobotBase robot, Position landPosition, Topography topography)
    {
        PositionResult landResult = new();

        landPosition = robotManager.WaterDrift(robot, landPosition, topography);

        if (!robotManager.robots.ContainsKey(robot) && robotManager.IsPositionSafeForRobot(robot, landPosition, topography, ref landResult))
        {
            robotManager.robots.Add(robot, landPosition);
            robotStatusManager.RobotHeatTracker.PerformAction(robot, RobotAction.LAND, topography, landPosition);
            robotStatusManager.RobotEnergyTracker.ConsumeEnergy(robot, RobotAction.LAND);

            landResult.IsSuccess = true;
            landResult.HasRobotSurvived = true;
            landResult.Position = landPosition;
            landResult.Message = "Robot landed successful";

            return landResult;
        }

        landResult.Message += "Robot cannot land.";

        return landResult;
    }
}