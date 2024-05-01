using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Exoplanet;

namespace ExoplanetGame.Domain.Robot.Variants
{
    public class AquaBot : RobotBase
    {
        public AquaBot(ControlCenter.ControlCenter controlCenter, ExoPlanetBase exoPlanet, int robotId) : base(exoPlanet, controlCenter, robotId, RobotVariant.AQUA)
        {
        }
    }
}