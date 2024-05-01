using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Exoplanet
{
    public class RobotStatusManager
    {
        public Dictionary<RobotBase, int> RobotsEnergy { get; set; }
        public Dictionary<RobotBase, bool> RobotsFrozen;
        public Dictionary<RobotBase, double> RobotHeatLevels { get; }
        public Dictionary<RobotBase, DateTime> RobotsLastMove = new();
        public Dictionary<RobotBase, Dictionary<RobotPart, int>> RobotPartsHealth;
        public Dictionary<RobotBase, bool> RobotsStuck;

        public RobotStatusManager()
        {
            RobotsEnergy = new();
            RobotsFrozen = new();
            RobotHeatLevels = new();
            RobotPartsHealth = new();
            RobotsStuck = new();
        }
    }
}