using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Exoplanet
{
    public interface LandOnExoplanetUseCase
    {
        PositionResult LandExoplanet(IRobot robot, Position landPosition);
    }
}