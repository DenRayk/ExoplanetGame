using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Robot.Variants
{
    public class AquaBot : RobotBase
    {
        public AquaBot(ControlCenter.ControlCenter controlCenter, IExoplanet exoPlanet, int robotId) : base(exoPlanet, controlCenter, robotId, RobotVariant.AQUA)
        {
        }
    }
}