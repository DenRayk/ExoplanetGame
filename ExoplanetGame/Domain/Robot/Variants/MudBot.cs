using ExoplanetGame.Domain.Exoplanet;

namespace ExoplanetGame.Domain.Robot.Variants
{
    public class MudBot : IRobot
    {
        public IExoPlanet ExoPlanet { get; }

        public RobotInformation RobotInformation { get; }

        public MudBot(IExoPlanet exoPlanet, int robotId)
        {
            ExoPlanet = exoPlanet;
            RobotInformation = new RobotInformation
            {
                RobotID = robotId,
                RobotParts =
                {
                    [RobotPart.WHEELS] = 200,
                }
            };
        }

        public string GetLanderName()
        {
            return $"Robot {RobotInformation.RobotID} ({GetType().Name})";
        }
    }
}