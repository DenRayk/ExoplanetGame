using ExoplanetGame.Exoplanet;

namespace ExoplanetGame.Robot.Variants
{
    public class MudBot : RobotBase
    {
        public MudBot(ControlCenter.ControlCenter controlCenter, ExoPlanetBase exoPlanet, int robotId) : base(exoPlanet,
            controlCenter, robotId, RobotVariant.MUD)
        {
            RobotInformation.RobotParts[RobotPart.WHEELS] = 200;
        }
    }
}