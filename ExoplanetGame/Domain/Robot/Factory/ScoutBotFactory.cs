using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Robot.Variants;

namespace ExoplanetGame.Domain.Robot.Factory
{
    internal class ScoutBotFactory : RobotFactory
    {
        public override IRobot CreateRobot(IExoPlanet exoPlanet, int robotID)
        {
            return new ScoutBot(exoPlanet, robotID);
        }
    }
}