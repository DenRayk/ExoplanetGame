using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Domain.Robot.Variants
{
    public sealed class DefaultBot : RobotBase
    {
        public DefaultBot(ControlCenter.ControlCenter controlCenter, ExoPlanetBase exoPlanet, int robotId) : base(exoPlanet, controlCenter, robotId, RobotVariant.DEFAULT)
        {
            RobotInformation.RobotParts[RobotPart.WHEELS] = 150;
            RobotInformation.RobotParts[RobotPart.SCANSENSOR] = 150;
        }
    }
}