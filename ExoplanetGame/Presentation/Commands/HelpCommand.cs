namespace ExoplanetGame.Presentation.Commands
{
    internal class HelpCommand : BaseCommand
    {
        public HelpCommand()
        {
        }

        public HelpCommand(BaseCommand previousCommand)
        {
            this.PreviousCommand = previousCommand;
        }

        public HelpCommand(BaseCommand previousCommand, string helpText)
        {
            this.PreviousCommand = previousCommand;
            HelpText = helpText;
        }

        public string HelpText { get; set; }
        public BaseCommand PreviousCommand { get; set; }

        public override void Execute()
        {
            Console.WriteLine(HelpText);

            Console.WriteLine("Press any key to leave...");
            Console.ReadKey();
            Console.Clear();

            PreviousCommand.Execute();
        }
    }
}