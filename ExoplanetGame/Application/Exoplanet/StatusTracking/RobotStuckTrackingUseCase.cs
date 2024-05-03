using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Application.Exoplanet.StatusTracking
{
    public interface RobotStuckTrackingUseCase
    {
        public void CheckIfRobotGetsStuck(IRobot robot, Topography topography, Position position);

        void RobotGetStuckRandomly(IRobot robot);

        bool IsRobotStuck(IRobot robot);

        void UnstuckRobot(IRobot robot);
    }
}