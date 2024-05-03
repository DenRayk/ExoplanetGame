using ExoplanetGame.Application.Exoplanet.PlanetEvents;
using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Exoplanet
{
    internal class MoveOnExoplanetService : MoveOnExoplanetUseCase
    {
        private readonly ExoplanetService exoplanetService;
        private readonly PlanetEventsService planetEventsService;

        public MoveOnExoplanetService(ExoplanetService exoplanetService, PlanetEventsService planetEventsService)
        {
            this.exoplanetService = exoplanetService;
            this.planetEventsService = planetEventsService;
        }

        public PositionResult MoveRobot(IRobot robot)
        {
            var robotResult = planetEventsService.ExecutePlanetEvents(robot);

            if (!IsRobotReadyToMove(robot, robotResult, out var moveResult))
                return moveResult;

            var newPosition = GetNewRobotPosition(robot, moveResult);

            ApplyMovementEffects(robot, newPosition);
            UpdateRobotPosition(robot, newPosition);

            moveResult.IsSuccess = true;
            moveResult.HasRobotSurvived = true;
            moveResult.Position = newPosition;

            return moveResult;
        }

        private bool IsRobotReadyToMove(IRobot robot, RobotResultBase robotResult, out PositionResult moveResult)
        {
            moveResult = new PositionResult(robotResult);

            if (IsRobotUnableToMove(robot, moveResult))
                return false;

            if (IsRobotStuck(robot, moveResult))
                return false;

            if (!DoesRobotHaveEnoughEnergy(robot, moveResult))
                return false;

            return true;
        }

        private bool IsRobotUnableToMove(IRobot robot, PositionResult moveResult)
        {
            if (IsMovementSensorDamaged(robot, moveResult))
                return true;

            if (AreWheelsDamaged(robot, moveResult))
                return true;

            return false;
        }

        private bool IsMovementSensorDamaged(IRobot robot, PositionResult moveResult)
        {
            if (exoplanetService.RobotPartsTracking.IsRobotPartDamaged(robot, RobotPart.MOVEMENTSENSOR))
            {
                moveResult.Message = "Movement sensor is damaged. Please repair in Control Center.\n";
                return true;
            }
            return false;
        }

        private bool AreWheelsDamaged(IRobot robot, PositionResult moveResult)
        {
            if (exoplanetService.RobotPartsTracking.IsRobotPartDamaged(robot, RobotPart.WHEELS))
            {
                moveResult.Message += "Wheels are damaged. Please repair in Control Center.\n";
                return true;
            }
            return false;
        }

        private bool IsRobotStuck(IRobot robot, PositionResult moveResult)
        {
            if (exoplanetService.RobotStuckTracking.IsRobotStuck(robot))
            {
                moveResult.Message += "Robot is stuck. Try to rotate.\n";
                return true;
            }
            return false;
        }

        private bool DoesRobotHaveEnoughEnergy(IRobot robot, PositionResult moveResult)
        {
            if (!exoplanetService.EnergyTracking.DoesRobotHaveEnoughEneryToAction(robot, RobotAction.MOVE))
            {
                moveResult.Message += "Robot does not have enough energy to move.\n";
                return false;
            }
            return true;
        }

        private Position GetNewRobotPosition(IRobot robot, PositionResult moveResult)
        {
            var newPosition = exoplanetService.ExoPlanet.RobotPositionManager.Robots[robot].GetAdjacentPosition();
            var waterDriftPosition = exoplanetService.RobotPostionsService.WaterDrift(robot, newPosition, exoplanetService.ExoPlanet.Topography);

            if (!Equals(waterDriftPosition, newPosition))
            {
                moveResult.AddMessage("Robot landed in water and drifted to a safe position.");
                newPosition = waterDriftPosition;
            }

            return newPosition;
        }

        private void ApplyMovementEffects(IRobot robot, Position newPosition)
        {
            exoplanetService.HeatTracking.PerformAction(robot, RobotAction.MOVE, exoplanetService.ExoPlanet.Topography);
            exoplanetService.RobotPartsTracking.RobotPartDamage(robot, RobotPart.MOVEMENTSENSOR);
            exoplanetService.EnergyTracking.ConsumeEnergy(robot, RobotAction.MOVE);
            exoplanetService.RobotStuckTracking.CheckIfRobotGetsStuck(robot, exoplanetService.ExoPlanet.Topography, newPosition);
        }

        private void UpdateRobotPosition(IRobot robot, Position newPosition)
        {
            exoplanetService.ExoPlanet.RobotPositionManager.Robots[robot] = newPosition;
        }
    }
}