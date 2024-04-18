using ExoplanetGame.Exoplanet.Tracker;

namespace ExoplanetGame.Exoplanet
{
    public class RobotStatusManager
    {
        public RobotHeatTracker RobotHeatTracker { get; } = new();
        public RobotEnergyTracker RobotEnergyTracker { get; } = new();
        public RobotStuckTracker RobotStuckTracker { get; } = new();
        public RobotPartsTracker RobotPartsTracker { get; } = new();
        public RobotFreezeTracker RobotFreezeTracker { get; } = new();
    }
}