using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Domain.Robot.RobotResults
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