namespace ExoplanetGame.Domain.Robot.RobotResults
{
    public class RobotResultBase
    {
        public bool IsSuccess { get; set; }
        public bool HasRobotSurvived { get; set; }
        public string Message { get; set; }

        public void AddMessage(string message)
        {
            Message += message;
        }
    }
}