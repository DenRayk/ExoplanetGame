using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Helper;

namespace ExoplanetGame.Presentation.Commands.ControlCenter.Repair
{
    internal class SelectRobotPartToRepairCommand : BaseCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;

        public SelectRobotPartToRepairCommand(RobotBase robotBase, UCCollection ucCollection)
        {
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            var robotParts = ucCollection.UcCollectionRobot.RobotPartsHealthService.GetRobotPartsByRobot(robotBase);

            if (robotParts != null)
            {
                Console.WriteLine($"{robotBase.GetLanderName()} has used the following parts:");
                Console.WriteLine("Select one to repair it \n");
                var options = getRobotPartOptions(robotParts);

                BaseCommand baseCommand = ReadUserInputWithOptions(options);
                baseCommand.Execute();
            }
            else
            {
                Console.WriteLine($"{robotBase.GetLanderName()} has not used any parts yet. \n");
            }
        }

        private Dictionary<string, BaseCommand> getRobotPartOptions(Dictionary<RobotPart, int> robotParts)
        {
            Dictionary<string, BaseCommand> options = new();
            foreach (var robotPart in robotParts)
            {
                options.Add($"{robotPart.Key.GetDescriptionFromEnum(),-15} - Durability {GetDescriptionFromPartDurability(robotPart.Value)}", new RepairRobotPartCommand(robotBase, robotPart.Key, ucCollection));
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