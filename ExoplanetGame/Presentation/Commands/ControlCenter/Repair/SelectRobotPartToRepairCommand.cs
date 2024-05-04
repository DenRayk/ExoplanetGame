using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Helper;

namespace ExoplanetGame.Presentation.Commands.ControlCenter.Repair
{
    internal class SelectRobotPartToRepairCommand : BaseCommand
    {
        private UCCollection ucCollection;
        private IRobot robot;

        public SelectRobotPartToRepairCommand(IRobot robot, UCCollection ucCollection)
        {
            this.robot = robot;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            var robotParts = ucCollection.UcCollectionRobot.RobotPartsHealthService.GetRobotPartsByRobot(robot);

            if (robotParts != null)
            {
                Console.WriteLine($"{robot.GetLanderName()} has used the following parts:");
                Console.WriteLine("Select one to repair it \n");
                var options = GetRobotPartOptions(robotParts);

                BaseCommand baseCommand = ReadUserInputWithOptions(options);
                baseCommand.Execute();
            }
            else
            {
                Console.WriteLine($"{robot.GetLanderName()} has not used any parts yet. \n");
            }
        }

        private Dictionary<string, BaseCommand> GetRobotPartOptions(Dictionary<RobotPart, int> robotParts)
        {
            Dictionary<string, BaseCommand> options = new();
            foreach (var robotPart in robotParts)
            {
                options.Add($"{robotPart.Key.GetDescriptionFromEnum(),-15} - Durability {GetDescriptionFromPartDurability(robotPart.Value)}", new RepairRobotPartCommand(robot, robotPart.Key, ucCollection));
            }
            return options;
        }

        private string GetDescriptionFromPartDurability(int durability)
        {
            switch (durability)
            {
                case <= 0:
                    return "Broken";

                case <= 25:
                    return "Critical";

                case <= 50:
                    return "Damaged";

                case <= 75:
                    return "Worn";

                case <= 90:
                    return "Good";

                default:
                    return "Like new";
            }
        }
    }
}