using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot.Movement;

namespace ExoplanetGame.Robot.RobotResults
{
    public class ScanResult : RobotResultBase
    {
        public ScanResult()
        {
        }

        public ScanResult(RobotResultBase robotResultBase)
        {
            IsSuccess = robotResultBase.IsSuccess;
            HasRobotSurvived = robotResultBase.HasRobotSurvived;
            Message = robotResultBase.Message;
        }

        public Dictionary<Measure, Position> Measures { get; set; } = new();
    }
}