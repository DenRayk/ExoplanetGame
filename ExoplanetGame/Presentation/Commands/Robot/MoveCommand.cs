using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Presentation.Commands.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class MoveCommand : BaseCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;

        public MoveCommand(RobotBase robotBase, UCCollection ucCollection)
        {
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            PositionResult positionResult = ucCollection.UcCollectionRobot.MoveRobotService.Move(robotBase);

            if (positionResult.IsSuccess)
            {
                Console.WriteLine($"RobotPositionManager moved to {positionResult.Position}");
                SelectRobotActionCommand selectRobotActionCommand = new(ucCollection, robotBase);
                selectRobotActionCommand.Execute();
            }
            else
            {
                Console.WriteLine($"{positionResult.Message}");
                ControlCenterCommand controlCenterCommand = new(ucCollection);
                controlCenterCommand.Execute();
            }
        }
    }
}