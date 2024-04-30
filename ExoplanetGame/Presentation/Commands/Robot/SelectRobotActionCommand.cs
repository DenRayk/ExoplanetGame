using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application;
using ExoplanetGame.Presentation.Commands.ControlCenter;
using ExoplanetGame.Robot;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class SelectRobotActionCommand : BaseCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;

        private readonly string helpText =
            "Robot Menu Information\n" +
            "Position:\t Show current position of the Robot\n" +
            "Scan:\t\t Scan the environment\n" +
            "Move:\t\t Move the robot in the direction it is facing\n" +
            "Rotate:\t\t Rotate the robot left or right\n" +
            "Load:\t\t Load energy to the robot\n" +
            "Crash:\t\t Crash the robot\n";

        public SelectRobotActionCommand(UCCollection ucCollection, RobotBase robotBase)
        {
            this.ucCollection = ucCollection;
            this.robotBase = robotBase;
        }

        public override void Execute()
        {
            var options = GetRobotMenuOptions();

            BaseCommand baseCommand = ReadUserInputWithOptions(options);
            baseCommand.Execute();
        }

        private Dictionary<string, BaseCommand> GetRobotMenuOptions()
        {
            var options = new Dictionary<string, BaseCommand>
            {
                { "Position", new GetPositionCommand(robotBase) },
                { "Scan", new ScanCommand(robotBase) },
                { "Move", new MoveCommand(robotBase) },
                { "Rotate", new RotateCommand(robotBase) },
                { "Load", new LoadCommand(robotBase) },
                { "Crash", new CrashCommand(robotBase) },
                { "Back", new ControlCenterCommand(ucCollection) }
            };

            return options;
        }
    }
}