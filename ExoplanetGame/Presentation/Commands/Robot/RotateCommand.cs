using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application;
using ExoplanetGame.Presentation.Commands.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class RotateCommand : RobotCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;
        private Rotation rotation;
        private ControlCenterCommand controlCenterCommand;

        public RotateCommand(RobotBase robotBase, UCCollection ucCollection, Rotation rotation, BaseCommand previousCommand, ControlCenterCommand controlCenterCommand) : base(previousCommand, controlCenterCommand)
        {
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
            this.rotation = rotation;
            this.controlCenterCommand = controlCenterCommand;
        }

        public override void Execute()
        {
            RotationResult rotationResult = ucCollection.UcCollectionRobot.RotateRobotService.Rotate(robotBase, rotation);

            if (rotationResult.IsSuccess)
            {
                Console.WriteLine($"Robot rotated to {rotationResult.Direction}");
            }
            else
            {
                Console.WriteLine($"{rotationResult.Message}");

                if (!rotationResult.HasRobotSurvived)
                {
                    controlCenterCommand.Execute();
                }
            }

            previousCommand.Execute();
        }
    }
}