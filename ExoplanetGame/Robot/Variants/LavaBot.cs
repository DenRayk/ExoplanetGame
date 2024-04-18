using ExoplanetGame.Exoplanet;

namespace ExoplanetGame.Robot.Variants
{
    public class LavaBot : RobotBase
    {
        public LavaBot(ControlCenter.ControlCenter controlCenter, ExoplanetBase exoPlanet, int robotId) : base(exoPlanet, controlCenter, robotId, RobotVariant.LAVA)
        {
            RobotInformation.MaxHeat = 200;
        }
    }
}