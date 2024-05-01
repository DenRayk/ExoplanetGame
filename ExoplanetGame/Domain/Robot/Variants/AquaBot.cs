using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Domain.Robot.Variants
{
    public class AquaBot : RobotBase
    {
        public AquaBot(ControlCenter.ControlCenter controlCenter, ExoPlanetBase exoPlanet, int robotId) : base(exoPlanet, controlCenter, robotId, RobotVariant.AQUA)
        {
        }
    }
}