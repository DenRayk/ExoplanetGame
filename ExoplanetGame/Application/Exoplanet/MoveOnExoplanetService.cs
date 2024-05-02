using ExoplanetGame.Application.Exoplanet.PlanetEvents;
using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Exoplanet
{
    internal class MoveOnExoplanetService : MoveOnExoplanetUseCase
    {
        private ExoplanetService exoplanetService;
        private PlanetEventsService planetEventsService;

        public MoveOnExoplanetService(ExoplanetService exoplanetService, PlanetEventsService planetEventsService)
        {
            this.exoplanetService = exoplanetService;
            this.planetEventsService = planetEventsService;
        }

        public PositionResult MoveRobot(RobotBase robot)
        {
            RobotResultBase robotResult = planetEventsService.ExecutePlanetEvents(robot);

            if (robotResult.IsSuccess == false)
            {
                return new PositionResult(robotResult);
            }

            PositionResult moveResult = new(robotResult);
            Position robotPosition = exoplanetService.ExoPlanet.RobotPositionManager.Robots[robot];

            if (!CanRobotMove(robot, ref moveResult))
                return moveResult;

            Position newPosition = robotPosition.GetAdjacentPosition();
            Position waterDriftPosition = exoplanetService.RobotPostionsService.WaterDrift(robot, newPosition, exoplanetService.ExoPlanet.Topography);

            if (!Equals(waterDriftPosition, newPosition))
            {
                moveResult.AddMessage("Robot landed in water and drifted to a safe position.");
                newPosition = waterDriftPosition;
            }

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