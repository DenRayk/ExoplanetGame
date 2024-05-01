using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Exoplanet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Application.Exoplanet.StatusTracking
{
    public interface HeatTrackingUseCase
    {
        void PerformAction(RobotBase robot, RobotAction robotAction, Topography topography);

        void PerformAction(RobotBase robot, RobotAction robotAction, Topography topography, Position landPosition);

        void WaterCoolDown(RobotBase robot);
    }
}