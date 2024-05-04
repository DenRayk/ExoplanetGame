using ExoplanetGame.Application.Exoplanet.PlanetEvents;
using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Exoplanet
{
    internal class RotateOnExoplanetService : RotateOnExoplanetUseCase
    {
        private ExoplanetService exoplanetService;
        private PlanetEventsUseCase planetEventsService;

        public RotateOnExoplanetService(ExoplanetService exoplanetService, PlanetEventsUseCase planetEventsService)
        {
            this.exoplanetService = exoplanetService;
            this.planetEventsService = planetEventsService;
        }

        public RotationResult Rotate(IRobot robot, Rotation rotation)
        {
            RobotResultBase robotResult = planetEventsService.ExecutePlanetEvents(robot);
            if (!robotResult.IsSuccess)
            {
                return new RotationResult(robotResult);
            }

            RotationResult rotationResult = new RotationResult(robotResult);
            Position robotPosition = exoplanetService.ExoPlanet.RobotPositionManager.Robots[robot];

            if (!CanRobotRotate(robot, rotation, ref rotationResult))
            {
                rotationResult.Direction = robotPosition.Direction;
                rotationResult.HasRobotSurvived = true;
                rotationResult.IsSuccess = false;
                return rotationResult;
            }

            exoplanetService.HeatTracking.PerformAction(robot, RobotAction.ROTATE, exoplanetService.ExoPlanet.Topography);
            exoplanetService.EnergyTracking.ConsumeEnergy(robot, RobotAction.ROTATE);

            if (exoplanetService.RobotStuckTracking.IsRobotStuck(robot))
            {
                exoplanetService.RobotStuckTracking.UnstuckRobot(robot);
            }

            if (rotation == Rotation.RIGHT)
            {
                exoplanetService.RobotPartsTracking.RobotPartDamage(robot, RobotPart.RIGHTMOTOR);
            }
            else
            {
                exoplanetService.RobotPartsTracking.RobotPartDamage(robot, RobotPart.LEFTMOTOR);
            }

            rotationResult.IsSuccess = true;
            rotationResult.HasRobotSurvived = true;
            rotationResult.Direction = robotPosition.Rotate(rotation);

            return rotationResult;
        }

        private bool CanRobotRotate(IRobot robot, Rotation rotation, ref RotationResult rotationResult)
        {
            bool isRightMotorDamaged = exoplanetService.RobotPartsTracking.IsRobotPartDamaged(robot, RobotPart.RIGHTMOTOR);
            bool isLeftMotorDamaged = exoplanetService.RobotPartsTracking.IsRobotPartDamaged(robot, RobotPart.LEFTMOTOR);
            bool doesRobotHaveEnery = exoplanetService.EnergyTracking.DoesRobotHaveEnoughEneryToAction(robot, RobotAction.ROTATE);

            if (isRightMotorDamaged && rotation == Rotation.RIGHT)
            {
                rotationResult.Message = "Right motor is damaged. Please repair in Control Center.";
            }

            if (isLeftMotorDamaged && rotation == Rotation.LEFT)
            {
                rotationResult.Message = "Left motor is damaged. Please repair in Control Center.";
            }

            if (!doesRobotHaveEnery)
            {
                rotationResult.Message = "Robot does not have enough energy to rotate.";
            }

            bool canRotate = !isRightMotorDamaged && !isLeftMotorDamaged && doesRobotHaveEnery;

            return canRotate;
        }
    }
}