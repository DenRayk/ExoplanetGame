using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Domain.ControlCenter
{
    public class ControlCenter
    {
        private static ControlCenter controlCenter;
        private static int RobotId = 1;
        public IExoPlanet exoPlanet;

        public PlanetMap PlanetMap { get; set; }

        public int MaxRobots => 5;

        public int GetRobotIDandIncrement()
        {
            return RobotId++;
        }

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