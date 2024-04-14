using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet;

public class MoveController(RobotManager robotManager)
{
    public Position MoveLavaBot(LavaBot lavaBot, Topography topography)
    {
        if (robotManager.CanRobotMove(lavaBot))
        {
            Position robotPosition = robotManager.robots[lavaBot];
            Position newPosition = robotManager.GetNewRobotPosition(robotPosition);

            if (robotManager.IsPositionSafeForLavaBot(lavaBot, newPosition, topography))
            {
                robotManager.UpdateRobotPosition(lavaBot, newPosition);
                robotManager.CheckIfRobotGetsStuck(lavaBot, topography, newPosition);
                return newPosition;
            }
            else
            {
                robotManager.RemoveRobot(lavaBot);
                return null;
            }
        }
        else
        {
            Console.WriteLine("The robot's movement sensors or wheels are damaged and can't move.");
            return null;
        }
    }

    public Position MoveRobot(RobotBase robot, Topography topography)
    {
        if (robotManager.CanRobotMove(robot))
        {
            Position robotPosition = robotManager.robots[robot];
            Position newPosition = robotManager.GetNewRobotPosition(robotPosition);

            if (robotManager.IsPositionSafeForRobot(robot, newPosition, topography))
            {
                robotManager.UpdateRobotPosition(robot, newPosition);
                robotManager.CheckIfRobotGetsStuck(robot, topography, newPosition);
                return newPosition;
            }
            else
            {
                robotManager.RemoveRobot(robot);
                return null;
            }
        }
        else
        {
            Console.WriteLine("The robot's movement sensors or wheels are damaged and can't move.");
            return null;
        }
    }
}