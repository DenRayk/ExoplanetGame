using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Domain.Exoplanet
{
    public class RobotPositionManager
    {
        public Dictionary<IRobot, Position> Robots { get; set; }

        public RobotPositionManager()
        {
            Robots = new Dictionary<IRobot, Position>();
        }
    }
}