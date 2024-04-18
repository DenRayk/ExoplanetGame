﻿using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet;

public class MoveController(RobotManager robotManager, RobotStatusManager robotStatusManager)
{
    public Position MoveRobot(RobotBase robot, Topography topography)
    {
        Position robotPosition = robotManager.robots[robot];

        if (CanRobotMove(robot))
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

    private bool CanRobotMove(RobotBase robot)
    {
        bool isMovementSensorDamaged = robotStatusManager.RobotPartsTracker.isRobotPartDamaged(robot, RobotPart.MOVEMENTSENSOR);
        bool areWheelsDamaged = robotStatusManager.RobotPartsTracker.isRobotPartDamaged(robot, RobotPart.WHEELS);
        bool isRobotStuck = robotStatusManager.RobotStuckTracker.IsRobotStuck(robot);

        if (isMovementSensorDamaged)
        {
            Console.WriteLine("Movement sensor is damaged.");
        }

        if (areWheelsDamaged)
        {
            Console.WriteLine("Wheels are damaged.");
        }

        if (isRobotStuck)
        {
            Console.WriteLine("Robot is stuck. Try to rotate.");
        }

        bool canMove = !isMovementSensorDamaged && !areWheelsDamaged && !isRobotStuck;

        if (!canMove)
        {
            Console.WriteLine("Robot cannot move due to the above problem(s).");
        }

        return canMove;
    }
}