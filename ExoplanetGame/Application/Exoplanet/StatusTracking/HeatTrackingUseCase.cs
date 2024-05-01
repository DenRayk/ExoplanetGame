using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGame.Application.Exoplanet.StatusTracking
{
    public interface HeatTrackingUseCase
    {
        void PerformAction(RobotBase robot, RobotAction robotAction, Topography topography);

        void PerformAction(RobotBase robot, RobotAction robotAction, Topography topography, Position landPosition);

        void WaterCoolDown(RobotBase robot);
    }
}