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
            newPosition = WaterDrift(lavaBot, newPosition, topography);

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
            newPosition = WaterDrift(robot, newPosition, topography);

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

    public Position MoveMudBot(MudBot mudBot, Topography topography)
    {
        if (robotManager.CanRobotMove(mudBot))
        {
            Position robotPosition = robotManager.robots[mudBot];
            Position newPosition = robotManager.GetNewRobotPosition(robotPosition);
            newPosition = WaterDrift(mudBot, newPosition, topography);

            if (robotManager.IsPositionSafeForRobot(mudBot, newPosition, topography))
            {
                robotManager.UpdateRobotPosition(mudBot, newPosition);

                return newPosition;
            }
            else
            {
                robotManager.RemoveRobot(mudBot);
                return null;
            }
        }
        else
        {
            Console.WriteLine("The robot's movement sensors or wheels are damaged and can't move.");
            return null;
        }
    }

    private Position WaterDrift(RobotBase robot, Position position, Topography topography)
    {
        while (position.Y < topography.PlanetSize.Height - 1 && topography.GetMeasureAtPosition(position).Ground == Ground.WASSER)
        {
            position = new Position(position.X, position.Y + 1);
        }

        while (position.Y == topography.PlanetSize.Height - 1 && topography.GetMeasureAtPosition(position).Ground == Ground.WASSER)
        {
            position = new Position(position.X + 1, position.Y);
        }

        return position;
    }

    public Position MoveAquaBot(AquaBot aquaBot, Topography topography)
    {
        if (robotManager.CanRobotMove(aquaBot))
        {
            Position robotPosition = robotManager.robots[aquaBot];
            Position newPosition = robotManager.GetNewRobotPosition(robotPosition);

            if (robotManager.IsPositionSafeForRobot(aquaBot, newPosition, topography))
            {
                robotManager.UpdateRobotPosition(aquaBot, newPosition);

                return newPosition;
            }
            else
            {
                robotManager.RemoveRobot(aquaBot);
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