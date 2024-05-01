using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Application.Exoplanet.StatusTracking
{
    public interface RobotStuckTrackingUseCase
    {
        public void CheckIfRobotGetsStuck(RobotBase robot, Topography topography, Position position);

        void RobotGetStuckRandomly(RobotBase robot);

        bool IsRobotStuck(RobotBase robot);

        void UnstuckRobot(RobotBase robot);
    }
}