using ExoplanetGame.Domain.Exoplanet;

namespace ExoplanetGame.Domain.Robot.Variants
{
    public class ScoutBot : IRobot
    {
        public IExoPlanet ExoPlanet { get; }

        public RobotInformation RobotInformation { get; }

        public ScoutBot(IExoPlanet exoPlanet, int robotId)
        {
            ExoPlanet = exoPlanet;
            RobotInformation = new RobotInformation
            {
                RobotID = robotId,
                RobotParts =
                {
                    [RobotPart.SCANSENSOR] = 200
                },
                MaxEnergy = 200
            };
        }

        public string GetLanderName()
        {
            return $"Robot {RobotInformation.RobotID} ({GetType().Name})";
        }
    }
}