using ExoplanetGame.Application.ControlCenter;
using ExoplanetGame.Exoplanet.Tracker;
using ExoplanetGame.Robot;

namespace ExoplanetGame.Exoplanet
{
    public class RobotStatusManager
    {
        private const int COOL_DOWN_RATE = 10;

        public Dictionary<RobotBase, int> RobotsEnergy { get; set; } = new();
        public Dictionary<RobotBase, bool> RobotsFrozen = new();
        public Dictionary<RobotBase, double> RobotHeatLevels { get; } = new();
        public Dictionary<RobotBase, DateTime> RobotsLastMove = new();
        public Dictionary<RobotBase, Dictionary<RobotPart, int>> RobotPartsHealth = new();
        public Dictionary<RobotBase, bool> RobotsStuck = new();

        public RobotHeatTracker RobotHeatTracker { get; } = new();
        public RobotEnergyTracker RobotEnergyTracker { get; } = new();
        public RobotStuckTracker RobotStuckTracker { get; } = new();
        public RobotPartsTracker RobotPartsTracker { get; } = new();
        public RobotFreezeTracker RobotFreezeTracker { get; } = new();
    }
}