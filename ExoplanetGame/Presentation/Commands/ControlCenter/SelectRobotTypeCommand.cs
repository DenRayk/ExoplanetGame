using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot.Variants;
using ExoplanetGame.Helper;

namespace ExoplanetGame.Presentation.Commands.ControlCenter
{
    internal class SelectRobotTypeCommand : BaseCommand
    {
        private readonly UCCollection ucCollection;

        private readonly string helpText =
            "Robot Variant Information\n" +
            $"{RobotVariant.DEFAULT.GetDescriptionFromEnum()}:\t Basic robot with no special abilities\n" +
            $"{RobotVariant.SCOUT.GetDescriptionFromEnum()}:\t Scan roboter with a larger scanning range\n" +
            $"{RobotVariant.LAVA.GetDescriptionFromEnum()}:\t Robot that can withstand high temperatures\n" +
            $"{RobotVariant.AQUA.GetDescriptionFromEnum()}:\t Robot that can withstand water drift\n" +
            $"{RobotVariant.MUD.GetDescriptionFromEnum()}:\t Robot that can move through mud\n";

        public SelectRobotTypeCommand(UCCollection ucCollection)
        {
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            var options = GetRobotOptions();

            BaseCommand baseCommand;
            do
            {
                Console.WriteLine("Robot Variant Menu");
                Console.WriteLine("Select a robot variant (press F1 for help):\n");
                baseCommand = ReadUserInputWithOptions(options);

                if (baseCommand is HelpCommand helpCommand)
                {
                    helpCommand.HelpText = helpText;
                    helpCommand.Execute();
                }
            } while (baseCommand is HelpCommand);

            baseCommand.Execute();
        }

        private Dictionary<string, BaseCommand> GetRobotOptions()
        {
            var options = new Dictionary<string, BaseCommand>();

            foreach (RobotVariant planetVariant in Enum.GetValues(typeof(RobotVariant)))
            {
                options.Add(planetVariant.GetDescriptionFromEnum(), new AddRobotCommand(planetVariant, ucCollection));
            }
            options.Add("Random", new AddRobotCommand(GetRandomRobotVariant(), ucCollection));
            return options;
        }

        private RobotVariant GetRandomRobotVariant()
        {
            Array values = Enum.GetValues(typeof(RobotVariant));
            Random random = new Random();
            return (RobotVariant)(values.GetValue(random.Next(values.Length)) ?? RobotVariant.DEFAULT);
        }
    }
}