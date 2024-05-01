using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Domain.Robot.RobotResults
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