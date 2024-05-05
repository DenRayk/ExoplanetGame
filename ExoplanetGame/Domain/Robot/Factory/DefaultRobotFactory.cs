using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Robot.Variants;

namespace ExoplanetGame.Domain.Robot.Factory
{
    public class DefaultRobotFactory : RobotFactory
    {
        public override IRobot CreateRobot(IExoPlanet exoPlanet, int robotID)
        {
            return new DefaultBot(exoPlanet, robotID);
        }
    }
}