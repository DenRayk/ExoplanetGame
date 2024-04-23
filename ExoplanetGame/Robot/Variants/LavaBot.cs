using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.ExoplanetGame.Exoplanet;

namespace ExoplanetGame.Robot.Variants
{
    public class LavaBot : RobotBase
    {
        public LavaBot(ControlCenter.ControlCenter controlCenter, IExoplanet exoPlanet, int robotId) : base(exoPlanet, controlCenter, robotId, RobotVariant.LAVA)
        {
            RobotInformation.MaxHeat = 200;
        }
    }
}