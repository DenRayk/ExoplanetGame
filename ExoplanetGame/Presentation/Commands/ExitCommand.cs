namespace ExoplanetGame.Presentation.Commands
{
    internal class ExitCommand : BaseCommand
    {
        public override void Execute()
        {
            Environment.Exit(0);
        }
    }
}