using ExoplanetGame.Exoplanet;

namespace ExoplanetGame.Robot.Variants
{
    public class AquaBot : RobotBase
    {
        public AquaBot(ControlCenter.ControlCenter controlCenter, ExoplanetBase exoPlanet, int robotId) : base(exoPlanet, controlCenter, robotId, RobotVariant.AQUA)
        {
        }
    }
}