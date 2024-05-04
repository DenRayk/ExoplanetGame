namespace ExoplanetGame.Presentation.Commands
{
    public class HelpCommand : BaseCommand
    {
        public HelpCommand()
        {
        }

        public string HelpText { get; set; }

        public override void Execute()
        {
            Console.WriteLine(HelpText);

            Console.WriteLine("Press any enter to leave...");

            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
            }

            Console.Clear();
        }
    }
}