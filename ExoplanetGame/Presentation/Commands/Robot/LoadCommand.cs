using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class LoadCommand : RobotCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;

        public LoadCommand(RobotBase robotBase, UCCollection ucCollection)
        {
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            Console.WriteLine("Enter the number of seconds to load energy:");
            int seconds = GetMenuSelection(20);

            Console.WriteLine("Loading energy...");
            LoadResult loadResult = ucCollection.UcCollectionRobot.LoadRobotService.LoadEnergy(robotBase, seconds);
            RobotResult = loadResult;

            if (loadResult.IsSuccess)
            {
                Console.WriteLine($"Robot loaded energy to {loadResult.EnergyLoad}%");
            }
            else
            {
                Console.WriteLine($"{loadResult.Message}");
            }
        }
    }
}