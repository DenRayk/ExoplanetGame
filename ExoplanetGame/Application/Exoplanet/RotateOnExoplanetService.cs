using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Application.Exoplanet
{
    internal class RotateOnExoplanetService : RotateOnExoplanetUseCase
    {
        private ExoplanetService exoplanetService;

        public RotateOnExoplanetService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public RotationResult Rotate(RobotBase robot, Rotation rotation)
        {
            RotationResult rotationResult = new RotationResult();
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

        private bool CanRobotRotate(RobotBase robot, Rotation rotation, ref RotationResult rotationResult)
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