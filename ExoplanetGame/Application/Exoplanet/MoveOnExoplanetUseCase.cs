using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Exoplanet
{
    public interface MoveOnExoplanetUseCase
    {
        public PositionResult MoveRobot(IRobot robot);
    }
}