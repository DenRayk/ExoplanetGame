using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet;

public class LandController(RobotManager robotManager, RobotHeatTracker robotHeatTracker)
{
    public Position LandRobot(RobotBase robot, Position landPosition, Topography topography)
    {
        landPosition = robotManager.WaterDrift(robot, landPosition, topography);

        if (!robotManager.robots.ContainsKey(robot) && robotManager.IsPositionSafeForRobot(robot, landPosition, topography))
        {
            robotManager.robots.Add(robot, landPosition);
            return landPosition;
        }

        robotManager.RemoveRobot(robot);

        return null;
    }
}