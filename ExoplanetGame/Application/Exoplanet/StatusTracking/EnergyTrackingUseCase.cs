using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGame.Application.Exoplanet
{
    public interface EnergyTrackingUseCase
    {
        public LoadResult LoadEnergy(IRobot robot, int seconds, Weather weather);

        public void ConsumeEnergy(IRobot robot, RobotAction robotAction);

        public int GetRobotEnergy(IRobot robot);

        public bool DoesRobotHaveEnoughEneryToAction(IRobot robot, RobotAction robotAction);
    }
}