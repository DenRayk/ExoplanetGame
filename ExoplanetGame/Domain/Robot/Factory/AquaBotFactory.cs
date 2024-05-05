using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Robot.Variants;

namespace ExoplanetGame.Domain.Robot.Factory
{
    internal class AquaBotFactory : RobotFactory
    {
        public override IRobot CreateRobot(IExoPlanet exoPlanet, int robotID)
        {
            return new AquaBot(exoPlanet, robotID);
        }
    }
}