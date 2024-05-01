using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot.RobotResults;
using ExoplanetGame.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Application.Exoplanet
{
    internal interface EnergyTrackingUseCase
    {
        public LoadResult LoadEnergy(RobotBase robot, int seconds, Weather weather);

        public void ConsumeEnergy(RobotBase robot, RobotAction robotAction);

        public int GetRobotEnergy(RobotBase robot);

        public bool DoesRobotHaveEnoughEneryToAction(RobotBase robot, RobotAction robotAction);
    }
}