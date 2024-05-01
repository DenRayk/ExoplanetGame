using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Presentation.Commands.ControlCenter;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class LoadCommand : RobotCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;
        private ControlCenterCommand controlCenterCommand;

        public LoadCommand(RobotBase robotBase, UCCollection ucCollection, BaseCommand previousCommand, ControlCenterCommand controlCenterCommand) : base(previousCommand, controlCenterCommand)
        {
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
            this.controlCenterCommand = controlCenterCommand;
        }

        public override void Execute()
        {
            Console.WriteLine("Enter the number of seconds to load energy:");
            int seconds = GetMenuSelection(20);

            Console.WriteLine("Loading energy...");
            LoadResult loadResult = ucCollection.UcCollectionRobot.LoadRobotService.LoadEnergy(robotBase, seconds);

            if (loadResult.IsSuccess)
            {
                Console.WriteLine($"Robot loaded energy to {loadResult.EnergyLoad}%");
            }
            else
            {
                Console.WriteLine($"{loadResult.Message}");
                if (!loadResult.HasRobotSurvived)
                {
                    controlCenterCommand.Execute();
                }
            }
            previousCommand.Execute();
        }
    }
}