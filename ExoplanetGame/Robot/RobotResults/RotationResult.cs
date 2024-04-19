using ExoplanetGame.Robot.Movement;

namespace ExoplanetGame.Robot.RobotResults
{
    public class RotationResult : RobotResultBase
    {
        public RotationResult()
        {
        }

        public RotationResult(RobotResultBase robotResultBase)
        {
            IsSuccess = robotResultBase.IsSuccess;
            HasRobotSurvived = robotResultBase.HasRobotSurvived;
            Message = robotResultBase.Message;
        }

        public Direction Direction { get; set; }
    }
}