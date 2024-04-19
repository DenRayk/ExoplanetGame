using ExoplanetGame.ControlCenter;

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

        public Measure Measure { get; set; }
    }
}