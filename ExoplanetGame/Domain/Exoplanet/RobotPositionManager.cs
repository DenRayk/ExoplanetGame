using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Domain.Exoplanet
{
    public class RobotPositionManager
    {
        public Dictionary<RobotBase, Position> Robots { get; set; }

        public RobotStatusManager RobotStatusManager { get; }

        private readonly ExoPlanetBase exoPlanet;

        public RobotPositionManager(ExoPlanetBase exoPlanet)
        {
            this.exoPlanet = exoPlanet;
            Robots = new Dictionary<RobotBase, Position>();

            RobotStatusManager = new RobotStatusManager();
        }
    }
}