using ExoplanetGame.Domain.Exoplanet;

namespace ExoplanetGame.Domain.Robot.Variants
{
    public class LavaBot : IRobot
    {
        public ExoPlanetBase ExoPlanet { get; }

        public RobotInformation RobotInformation { get; }

        public LavaBot(ExoPlanetBase exoPlanet, int robotId)
        {
            ExoPlanet = exoPlanet;
            RobotInformation = new RobotInformation
            {
                RobotID = robotId,
                MaxHeat = 200
            };
        }

        public string GetLanderName()
        {
            return $"Robot {RobotInformation.RobotID} ({GetType().Name})";
        }
    }
}