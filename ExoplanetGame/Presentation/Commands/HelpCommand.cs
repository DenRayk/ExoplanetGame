namespace ExoplanetGame.Presentation.Commands
{
    internal class HelpCommand : BaseCommand
    {
        public HelpCommand(BaseCommand previousCommand) : base(previousCommand)
        {
        }

        public string HelpText { get; set; }

        public override void Execute()
        {
            Console.WriteLine(HelpText);

            Console.WriteLine("Press any key to leave...");
            Console.ReadKey();
            Console.Clear();

            previousCommand.Execute();
        }
    }
}