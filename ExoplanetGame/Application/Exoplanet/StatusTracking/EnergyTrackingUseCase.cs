using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGame.Application.Exoplanet
{
    public interface EnergyTrackingUseCase
    {
        public LoadResult LoadEnergy(RobotBase robot, int seconds, Weather weather);

        public void ConsumeEnergy(RobotBase robot, RobotAction robotAction);

        public int GetRobotEnergy(RobotBase robot);

        public bool DoesRobotHaveEnoughEneryToAction(RobotBase robot, RobotAction robotAction);
    }
}