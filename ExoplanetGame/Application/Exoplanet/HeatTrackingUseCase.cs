using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Application.Exoplanet
{
    public interface HeatTrackingUseCase
    {
        void PerformAction(RobotBase robot, RobotAction robotAction, Topography topography);

        void PerformAction(RobotBase robot, RobotAction robotAction, Topography topography, Position landPosition);

        void WaterCoolDown(RobotBase robot);
    }
}