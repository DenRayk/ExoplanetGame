using ExoplanetGame.Application;
using ExoplanetGame.Presentation.Commands.ControlCenter.Repair;

namespace ExoplanetGame.Presentation.Commands.ControlCenter
{
    public class ControlCenterCommand : BaseCommand
    {
        private UCCollection ucCollection;

        private readonly string helpText =
            "Control Center Menu Information\n" +
            "Add robot:\t Add a robot to the current layout\n" +
            "Control robot:\t Select a robot to be sent from the layout to the exoplanet\n" +
            "Repair robot:\t Repair a robot's part\n" +
            "Print Map:\t Display status of the exoplanet's exploration area\n";

        public ControlCenterCommand(UCCollection ucCollection, BaseCommand previousCommand) : base(previousCommand)
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
                helpCommand.Execute();
            }

            baseCommand.Execute();
        }

        private Dictionary<string, BaseCommand> getControlCenterOptions()
        {
            var options = new Dictionary<string, BaseCommand>
            {
                { "Add Robot", new SelectRobotTypeCommand(ucCollection, this) },
                { "Control Robot" , new ControlRobotCommand(ucCollection,this) },
                { "Repair Robot", new SelectRobotToRepairCommand(ucCollection, this, this) },
                { "Print Map", new PrintMapCommand(ucCollection, this) },
                { "Exit", new ExitCommand(this)}
            };

            return options;
        }
    }
}