using System.Runtime.CompilerServices;
using ExoplanetGame.Application;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Helper;
using ExoplanetGame.Presentation.Commands.PlanetSelection;

namespace ExoplanetGame.Presentation.Commands.ControlCenter
{
    internal class ControlCenterCommand : BaseCommand
    {
        private UCCollection ucCollection;

        private readonly string helpText =
            "Control Center Menu Information\n" +
            "Add Robot:\t Add a robot to the current layout\n" +
            "Control Robot:\t Select a robot to be sent from the layout to the exoplanet\n" +
            "Repair Robot:\t Repair a robot's part\n" +
            "Print Map:\t Display status of the exoplanet's exploration area\n";

        public ControlCenterCommand(UCCollection ucCollection)
        {
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            Console.WriteLine("Control Center");
            Console.WriteLine("Select an option (press F1 for help):\n");

            var options = getControlCenterOptions();

            BaseCommand baseCommand = ReadUserInputWithOptions(options);

            if (baseCommand is HelpCommand helpCommand)
            {
                helpCommand.HelpText = helpText;
                helpCommand.PreviousCommand = this;
                helpCommand.Execute();
            }

            baseCommand.Execute();
        }

        private Dictionary<string, BaseCommand> getControlCenterOptions()
        {
            var options = new Dictionary<string, BaseCommand>
            {
                { "Add Robot", new SelectRobotTypeCommand(ucCollection) },
                { "Control Robot" , new ControlRobotCommand(ucCollection) },
                { "Repair Robot", new RepairRobotCommand() },
                { "Print Map", new PrintMapCommand() },
                { "Exit", new ExitCommand()}
            };

            return options;
        }
    }
}