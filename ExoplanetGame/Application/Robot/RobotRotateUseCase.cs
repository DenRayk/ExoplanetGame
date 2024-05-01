using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Robot
{
    public interface RobotRotateUseCase
    {
        RotationResult Rotate(RobotBase robot, Rotation rotation);
    }
}