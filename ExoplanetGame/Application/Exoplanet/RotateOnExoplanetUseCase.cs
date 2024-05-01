using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Exoplanet
{
    public interface RotateOnExoplanetUseCase
    {
        RotationResult Rotate(RobotBase robot, Rotation rotation);
    }
}