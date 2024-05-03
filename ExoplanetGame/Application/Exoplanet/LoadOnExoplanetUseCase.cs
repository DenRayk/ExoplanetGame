using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Exoplanet
{
    public interface LoadOnExoplanetUseCase
    {
        LoadResult LoadEnergy(IRobot robot, int seconds);
    }
}