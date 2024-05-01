using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Domain.Robot.RobotResults
{
    public class PositionResult : RobotResultBase
    {
        public PositionResult()
        { }

        public PositionResult(RobotResultBase robotResultBase)
        {
            IsSuccess = robotResultBase.IsSuccess;
            Message = robotResultBase.Message;
            HasRobotSurvived = robotResultBase.HasRobotSurvived;
        }

        public Position Position { get; set; }
    }
}