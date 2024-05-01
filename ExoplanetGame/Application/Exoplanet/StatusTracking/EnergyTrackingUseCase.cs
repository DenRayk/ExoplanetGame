using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Exoplanet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

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