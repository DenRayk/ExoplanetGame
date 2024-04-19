using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot.Movement;

namespace ExoplanetGame.Robot.RobotResults
{
    public class ScoutScanResult : RobotResultBase
    {
        public ScoutScanResult()
        {
        }

        public ScoutScanResult(RobotResultBase robotResultBase)
        {
            IsSuccess = robotResultBase.IsSuccess;
            HasRobotSurvived = robotResultBase.HasRobotSurvived;
            Message = robotResultBase.Message;
        }

        public Dictionary<Measure, Position> Measures { get; set; } = new();
    }
}