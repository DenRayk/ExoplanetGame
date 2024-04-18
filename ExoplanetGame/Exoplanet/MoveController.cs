using ExoplanetGame.Robot;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Exoplanet;

public class MoveController(RobotManager robotManager, RobotStatusManager robotStatusManager)
{
    public PositionResult MoveRobot(RobotBase robot, Topography topography)
    {
        PositionResult moveResult = new();
        Position robotPosition = robotManager.robots[robot];

        if (!CanRobotMove(robot, ref moveResult)) return moveResult;

        Position newPosition = robotManager.GetNewRobotPosition(robotPosition);
        newPosition = robotManager.WaterDrift(robot, newPosition, topography);

        robotStatusManager.RobotHeatTracker.PerformAction(robot, RobotAction.MOVE, topography);
        robotStatusManager.RobotPartsTracker.RobotPartDamage(robot, RobotPart.MOVEMENTSENSOR);
        robotStatusManager.RobotEnergyTracker.ConsumeEnergy(robot, RobotAction.MOVE);

        if (!robotManager.IsPositionSafeForRobot(robot, newPosition, topography, ref moveResult)) return moveResult;

        robotManager.UpdateRobotPosition(robot, newPosition);
        robotManager.CheckIfRobotGetsStuck(robot, topography, newPosition);

        moveResult.IsSuccess = true;
        moveResult.HasRobotSurvived = true;
        moveResult.Position = newPosition;

        return moveResult;
    }

    private bool CanRobotMove(RobotBase robot, ref PositionResult positionResult)
    {
        bool isMovementSensorDamaged = robotStatusManager.RobotPartsTracker.isRobotPartDamaged(robot, RobotPart.MOVEMENTSENSOR);
        bool areWheelsDamaged = robotStatusManager.RobotPartsTracker.isRobotPartDamaged(robot, RobotPart.WHEELS);
        bool isRobotStuck = robotStatusManager.RobotStuckTracker.IsRobotStuck(robot);

        if (isMovementSensorDamaged)
        {
            positionResult.Message = "Movement sensor is damaged. Please repair in Control Center.\n";
        }

        if (areWheelsDamaged)
        {
            positionResult.Message += "Wheels are damaged. Please repair in Control Center.\n";
        }

        if (isRobotStuck)
        {
            positionResult.Message += "Robot is stuck. Try to rotate.\n";
        }

        bool canMove = !isMovementSensorDamaged && !areWheelsDamaged && !isRobotStuck;

        if (!canMove)
        {
            positionResult.Message += "Robot cannot move due to the above problem(s).";
            positionResult.IsSuccess = false;
            positionResult.HasRobotSurvived = true;
        }

        return canMove;
    }
}