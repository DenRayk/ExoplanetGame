using ExoplanetGame.Domain.Exoplanet;

namespace ExoplanetGame.Domain.Robot.Variants
{
    public class ScoutBot : RobotBase
    {
        public ScoutBot(ControlCenter.ControlCenter controlCenter, ExoPlanetBase exoPlanet, int robotId) : base(exoPlanet, controlCenter, robotId, RobotVariant.SCOUT)
        {
            RobotInformation.RobotParts[RobotPart.SCANSENSOR] = 200;
            RobotInformation.MaxEnergy = 200;
        }
    }
}