using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet;

public class LandController(RobotManager robotManager)
{
    public Position LandLavaBot(LavaBot lavaBot, Position landPosition, Topography topography)
    {
        landPosition = robotManager.WaterDrift(lavaBot, landPosition, topography);

        if (!robotManager.robots.ContainsKey(lavaBot) && robotManager.IsPositionSafeForLavaBot(lavaBot, landPosition, topography))
        {
            robotManager.robots.Add(lavaBot, landPosition);
            return landPosition;
        }

        robotManager.RemoveRobot(lavaBot);

        return null;
    }

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