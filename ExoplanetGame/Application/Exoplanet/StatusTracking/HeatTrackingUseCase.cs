using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGame.Application.Exoplanet.StatusTracking
{
    public interface HeatTrackingUseCase
    {
        void PerformAction(IRobot robot, RobotAction robotAction, Topography topography);

        void PerformAction(IRobot robot, RobotAction robotAction, Topography topography, Position landPosition);

        void WaterCoolDown(IRobot robot);
    }
}