using ExoplanetGame.Domain.Robot.Variants;

namespace ExoplanetGame.Application.ControlCenter
{
    public interface AddRobotUseCase
    {
        void AddRobot(RobotVariant robotVariant);
    }
}