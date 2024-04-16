using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet;

public class LandController(RobotManager robotManager, RobotStatusManager robotStatusManager)
{
    public Position LandRobot(RobotBase robot, Position landPosition, Topography topography)
    {
        landPosition = robotManager.WaterDrift(robot, landPosition, topography);

        if (!robotManager.robots.ContainsKey(robot) && robotManager.IsPositionSafeForRobot(robot, landPosition, topography))
        {
            robotManager.robots.Add(robot, landPosition);
            robotStatusManager.RobotHeatTracker.PerformAction(robot, RobotAction.LAND, topography, landPosition);
            robotStatusManager.RobotEnergyTracker.ConsumeEnergy(robot, RobotAction.LAND);
            return landPosition;
        }

        robotManager.RemoveRobot(robot);

        return null;
    }
}