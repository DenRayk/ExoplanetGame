using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet;

public class MoveController(RobotManager robotManager, RobotStatusManager robotStatusManager)
{
    public Position MoveRobot(RobotBase robot, Topography topography)
    {
        if (robotManager.CanRobotMove(robot))
        {
            Position robotPosition = robotManager.robots[robot];
            Position newPosition = robotManager.GetNewRobotPosition(robotPosition);
            newPosition = robotManager.WaterDrift(robot, newPosition, topography);

            robotStatusManager.RobotHeatTracker.PerformAction(robot, RobotAction.MOVE, topography);
            robotStatusManager.RobotPartsTracker.RobotPartDamage(robot, RobotParts.MOVEMENTSENSOR);
            robotStatusManager.RobotEnergyTracker.ConsumeEnergy(robot, RobotAction.MOVE);

            if (robotManager.IsPositionSafeForRobot(robot, newPosition, topography))
            {
                robotManager.UpdateRobotPosition(robot, newPosition);
                robotManager.CheckIfRobotGetsStuck(robot, topography, newPosition);
                return newPosition;
            }

            robotManager.RemoveRobot(robot);
            return null;
        }

        Console.WriteLine("The robot's movement sensors or wheels are damaged and can't move.");
        return null;
    }
}