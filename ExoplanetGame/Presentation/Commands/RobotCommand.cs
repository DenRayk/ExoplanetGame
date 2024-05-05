using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands
{
    public abstract class RobotCommand : BaseCommand
    {
        public RobotResultBase RobotResult { get; protected set; }
    }
}