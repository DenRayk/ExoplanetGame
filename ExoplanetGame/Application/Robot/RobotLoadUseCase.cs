using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Robot
{
    public interface RobotLoadUseCase
    {
        LoadResult LoadEnergy(IRobot robot, int seconds);
    }
}