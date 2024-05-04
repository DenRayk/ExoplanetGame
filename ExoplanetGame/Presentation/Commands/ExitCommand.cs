namespace ExoplanetGame.Presentation.Commands
{
    public class ExitCommand : BaseCommand
    {
        public override void Execute()
        {
            Environment.Exit(0);
        }
    }
}