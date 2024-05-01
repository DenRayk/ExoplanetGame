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
    internal class RotateCommand : BaseCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;
        private Rotation rotation;

        public RotateCommand(RobotBase robotBase, UCCollection ucCollection, Rotation rotation)
        {
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
            this.rotation = rotation;
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
                    ControlCenterCommand controlCenterCommand = new(ucCollection);
                    controlCenterCommand.Execute();
                }
            }

            SelectRobotActionCommand selectRobotActionCommand = new(ucCollection, robotBase);
            selectRobotActionCommand.Execute();
        }
    }
}