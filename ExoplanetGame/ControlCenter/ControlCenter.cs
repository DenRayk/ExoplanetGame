using ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;

namespace ExoplanetGame.ControlCenter
{
    public class ControlCenter
    {
        private static ControlCenter controlCenter;
        private static int RobotId = 1;
        public ExoPlanetBase exoPlanet;

        public PlanetMap PlanetMap { get; set; }

        public int MaxRobots { get; } = 5;

        public int getRobotIDandIncrement()
        {
            return RobotId++;
        }

        public virtual void OnRobotPositionUpdated(RobotBase robot, Position position)
        {
            RobotPositionUpdated?.Invoke(this, new RobotPositionEventArgs(robot, position));
        }

        public event EventHandler<RobotPositionEventArgs> RobotPositionUpdated;

        public ControlCenter()
        {
        }

        public static ControlCenter GetInstance()
        {
            if (controlCenter == null)
            {
                controlCenter = new ControlCenter();
            }
            return controlCenter;
        }
    }
}