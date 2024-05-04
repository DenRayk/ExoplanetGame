using ExoplanetGame.Domain.Exoplanet;

namespace ExoplanetGame.Domain.Robot.Variants
{
    public class AquaBot : IRobot
    {
        public ExoPlanetBase ExoPlanet { get; }

        public RobotInformation RobotInformation { get; }

        public AquaBot(ExoPlanetBase exoPlanet, int robotID)

        {
            ExoPlanet = exoPlanet;
            RobotInformation = new RobotInformation
            {
                RobotID = robotID,
            };
        }

        public string GetLanderName()
        {
            return $"Robot {RobotInformation.RobotID} ({GetType().Name})";
        }
    }
}