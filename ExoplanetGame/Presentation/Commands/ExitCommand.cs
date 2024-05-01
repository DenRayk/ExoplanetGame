namespace ExoplanetGame.Presentation.Commands
{
    internal class ExitCommand(BaseCommand previousCommand) : BaseCommand(previousCommand)
    {
        public override void Execute()
        {
            Environment.Exit(0);
        }
    }
}