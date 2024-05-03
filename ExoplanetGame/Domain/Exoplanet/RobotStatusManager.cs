using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Domain.Exoplanet
{
    public class RobotStatusManager
    {
        public Dictionary<IRobot, int> RobotsEnergy { get; set; }
        public Dictionary<IRobot, bool> RobotsFrozen;
        public Dictionary<IRobot, double> RobotHeatLevels { get; }
        public Dictionary<IRobot, DateTime> RobotsLastMove = new();
        public Dictionary<IRobot, Dictionary<RobotPart, int>> RobotPartsHealth;
        public Dictionary<IRobot, bool> RobotsStuck;

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