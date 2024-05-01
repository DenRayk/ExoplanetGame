using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Robot
{
    public interface RobotLoadUseCase
    {
        LoadResult LoadEnergy(RobotBase robotBase, int seconds);
    }
}