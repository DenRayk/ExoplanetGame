using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Environment;

namespace ExoplanetGame.Application.Exoplanet
{
    internal class MoveOnExoplanetService : MoveOnExoplanetUseCase
    {
        private ExoplanetService exoplanetService;

        public MoveOnExoplanetService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public PositionResult MoveRobot(RobotBase robot)
        {
            PositionResult moveResult = new();
            Position robotPosition = exoplanetService.ExoPlanet.RobotPositionManager.Robots[robot];

            if (!CanRobotMove(robot, ref moveResult))
                return moveResult;

            Position newPosition = robotPosition.GetAdjacentPosition();
            newPosition = exoplanetService.RobotPostionsService.WaterDrift(robot, newPosition, exoplanetService.ExoPlanet.Topography);

            if (!exoplanetService.RobotPostionsService.IsPositionSafeForRobot(robot, newPosition, exoplanetService.ExoPlanet.Topography, ref moveResult))
                return moveResult;

            exoplanetService.HeatTracking.PerformAction(robot, RobotAction.MOVE, exoplanetService.ExoPlanet.Topography);
            exoplanetService.RobotPartsTracking.RobotPartDamage(robot, RobotPart.MOVEMENTSENSOR);
            exoplanetService.EnergyTracking.ConsumeEnergy(robot, RobotAction.MOVE);

            exoplanetService.ExoPlanet.RobotPositionManager.Robots[robot] = newPosition;
            exoplanetService.RobotStuckTracking.CheckIfRobotGetsStuck(robot, exoplanetService.ExoPlanet.Topography, newPosition);

            moveResult.IsSuccess = true;
            moveResult.HasRobotSurvived = true;
            moveResult.Position = newPosition;

            return moveResult;
        }

        private bool CanRobotMove(RobotBase robot, ref PositionResult positionResult)
        {
            bool isMovementSensorDamaged = exoplanetService.RobotPartsTracking.IsRobotPartDamaged(robot, RobotPart.MOVEMENTSENSOR);
            bool areWheelsDamaged = exoplanetService.RobotPartsTracking.IsRobotPartDamaged(robot, RobotPart.WHEELS);
            bool isRobotStuck = exoplanetService.RobotStuckTracking.IsRobotStuck(robot);
            bool doesRobotHaveEnergy = exoplanetService.EnergyTracking.DoesRobotHaveEnoughEneryToAction(robot, RobotAction.MOVE);

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

            if (!doesRobotHaveEnergy)
            {
                positionResult.Message += "Robot does not have enough energy to move.\n";
            }

            bool canMove = !isMovementSensorDamaged && !areWheelsDamaged && !isRobotStuck && doesRobotHaveEnergy;

            if (!canMove)
            {
                positionResult.Message += "Robot cannot move due to the above problem(s).";
                positionResult.IsSuccess = false;
                positionResult.HasRobotSurvived = true;
            }

            return canMove;
        }
    }
}