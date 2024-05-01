using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Presentation.Commands.ControlCenter.Repair
{
    internal class SelectRobotToRepairCommand : BaseCommand
    {
        private UCCollection ucCollection;

        public SelectRobotToRepairCommand(UCCollection ucCollection, BaseCommand previousCommand)
        {
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            Console.WriteLine("Robot Repair Menu");
            Console.WriteLine("Select a robot to repair: \n");

            var options = getRobotOptions(ucCollection.UcCollectionControlCenter.GetRobotsService.GetAllRobots());

            BaseCommand baseCommand = ReadUserInputWithOptions(options);

            baseCommand.Execute();
        }

        private Dictionary<string, BaseCommand> getRobotOptions(Dictionary<RobotBase, Position> getAllRobots)
        {
            Dictionary<string, BaseCommand> options = new();
            foreach (var robot in getAllRobots)
            {
                options.Add(robot.Key.GetLanderName(), new SelectRobotPartToRepairCommand(this, robot.Key, ucCollection));
            }
            return options;
        }
    }
}