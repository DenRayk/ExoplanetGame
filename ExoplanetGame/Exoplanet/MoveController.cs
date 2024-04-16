using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet;

public class MoveController(RobotManager robotManager, RobotStatusManager robotStatusManager)
{
    public Position MoveRobot(RobotBase robot, Topography topography)
    {
        Position robotPosition = robotManager.robots[robot];

        if (robotManager.CanRobotMove(robot))
        {
            
            Position newPosition = robotManager.GetNewRobotPosition(robotPosition);
            newPosition = robotManager.WaterDrift(robot, newPosition, topography);

            robotStatusManager.RobotHeatTracker.PerformAction(robot, RobotAction.MOVE, topography);
            robotStatusManager.RobotPartsTracker.RobotPartDamage(robot, RobotPart.MOVEMENTSENSOR);
            robotStatusManager.RobotEnergyTracker.ConsumeEnergy(robot, RobotAction.MOVE);

            if (robotManager.IsPositionSafeForRobot(robot, newPosition, topography))
            {
                robotManager.UpdateRobotPosition(robot, newPosition);
                robotManager.CheckIfRobotGetsStuck(robot, topography, newPosition);
                return newPosition;
            }

            return null;
        }

        return robotPosition;
    }
}