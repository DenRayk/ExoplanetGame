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

        public RotateCommand(RobotBase robotBase, UCCollection ucCollection, Rotation rotation)
        {
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            //PositionResult positionResult = ucCollection.UcCollectionRobot.RotateRobotService.Rotate(robotBase);

            //if (positionResult.IsSuccess)
            //{
            //    Console.WriteLine($"RobotPositionManager moved to {positionResult.Position}");
            //}
            //else
            //{
            //    Console.WriteLine($"{positionResult.Message}");

            //    if (!positionResult.HasRobotSurvived)
            //    {
            //        ControlCenterCommand controlCenterCommand = new(ucCollection);
            //        controlCenterCommand.Execute();
            //    }
            //}

            //SelectRobotActionCommand selectRobotActionCommand = new(ucCollection, robotBase);
            //selectRobotActionCommand.Execute();
        }
    }
}