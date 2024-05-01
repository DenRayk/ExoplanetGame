using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet
{
    public class RobotPositionManager
    {
        public Dictionary<RobotBase, Position> Robots { get; set; }

        public RobotStatusManager RobotStatusManager { get; }

        private readonly ExoPlanetBase _exoPlanet;

        public RobotPositionManager(ExoPlanetBase exoPlanet)
        {
            this._exoPlanet = exoPlanet;
            Robots = new Dictionary<RobotBase, Position>();

            RobotStatusManager = new RobotStatusManager();
        }
    }
}