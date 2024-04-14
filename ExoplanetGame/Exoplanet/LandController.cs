using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet;

public class LandController(RobotManager robotManager)
{
    public bool LandLavaBot(LavaBot lavaBot, Position landPosition, Topography topography)
    {
        if (!robotManager.robots.ContainsKey(lavaBot) && robotManager.IsPositionSafeForLavaBot(lavaBot, landPosition, topography))
        {
            robotManager.robots.Add(lavaBot, landPosition);
            return true;
        }

        robotManager.RemoveRobot(lavaBot);

        return false;
    }

    public bool LandRobot(RobotBase robot, Position landPosition, Topography topography)
    {
        if (!robotManager.robots.ContainsKey(robot) && robotManager.IsPositionSafeForRobot(robot, landPosition, topography))
        {
            robotManager.robots.Add(robot, landPosition);
            return true;
        }

        robotManager.RemoveRobot(robot);

        return false;
    }
}