using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Helper;
using ExoplanetGame.Presentation.Commands.PlanetSelection;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Presentation.Commands.ControlCenter
{
    internal class SelectRobotTypeCommand : BaseCommand
    {
        private UCCollection ucCollection;

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
            Console.WriteLine("Robot Variant Menu");
            Console.WriteLine("Select a robot variant (press F1 for help):\n");

            var options = getRobotOptions();

            BaseCommand baseCommand = ReadUserInputWithOptions(options);

            if (baseCommand is HelpCommand helpCommand)
            {
                helpCommand.HelpText = helpText;
                helpCommand.PreviousCommand = this;
                helpCommand.Execute();
            }
            baseCommand.Execute();
        }

        private Dictionary<string, BaseCommand> getRobotOptions()
        {
            var options = new Dictionary<string, BaseCommand>();

            foreach (RobotVariant planetVariant in Enum.GetValues(typeof(RobotVariant)))
            {
                options.Add(planetVariant.GetDescriptionFromEnum(), new AddRobotCommand(planetVariant, ucCollection));
            }
            options.Add("Random", new AddRobotCommand(getRandomRobotVariant(), ucCollection));
            return options;
        }

        private RobotVariant getRandomRobotVariant()
        {
            Array values = Enum.GetValues(typeof(RobotVariant));
            Random random = new Random();
            return (RobotVariant)(values.GetValue(random.Next(values.Length)) ?? RobotVariant.DEFAULT);
        }
    }
}