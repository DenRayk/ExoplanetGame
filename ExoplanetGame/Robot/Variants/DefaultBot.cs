using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.ExoplanetGame.Exoplanet;

namespace ExoplanetGame.Robot.Variants
{
    public sealed class DefaultBot : RobotBase
    {
        public DefaultBot(ControlCenter.ControlCenter controlCenter, IExoPlanet exoPlanet, int robotId) : base(exoPlanet, controlCenter, robotId, RobotVariant.DEFAULT)
        {
            RobotInformation.RobotParts[RobotPart.WHEELS] = 150;
            RobotInformation.RobotParts[RobotPart.SCANSENSOR] = 150;
        }
    }
}