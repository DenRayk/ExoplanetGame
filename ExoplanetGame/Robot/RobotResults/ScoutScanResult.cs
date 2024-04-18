using ExoplanetGame.ControlCenter;

namespace ExoplanetGame.Robot.RobotResults
{
    public class ScoutScanResult : RobotResultBase
    {
        public Dictionary<Measure, Position> Measures { get; set; } = new();
    }
}