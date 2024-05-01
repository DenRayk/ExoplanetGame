using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Exoplanet;

namespace ExoplanetGame.Domain.Robot.Variants
{
    public class LavaBot : RobotBase
    {
        public LavaBot(ControlCenter.ControlCenter controlCenter, ExoPlanetBase exoPlanet, int robotId) : base(exoPlanet, controlCenter, robotId, RobotVariant.LAVA)
        {
            RobotInformation.MaxHeat = 200;
        }
    }
}