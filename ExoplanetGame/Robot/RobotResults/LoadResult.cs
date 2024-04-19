namespace ExoplanetGame.Robot.RobotResults
{
    public class LoadResult : RobotResultBase
    {
        public LoadResult()
        {
        }

        public LoadResult(RobotResultBase robotResultBase)
        {
            IsSuccess = robotResultBase.IsSuccess;
            HasRobotSurvived = robotResultBase.HasRobotSurvived;
            Message = robotResultBase.Message;
        }

        public int EnergyLoad { get; set; }
    }
}