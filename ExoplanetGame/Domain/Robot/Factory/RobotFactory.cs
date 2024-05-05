using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Robot.Variants;

namespace ExoplanetGame.Domain.Robot.Factory
{
    public class RobotFactory : IRobotFactory
    {
        private static RobotFactory instance;

        private RobotFactory()
        { }

        public static RobotFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new RobotFactory();
            }
            return instance;
        }

        public IRobot CreateRobot(ControlCenter.ControlCenter controlCenter, IExoPlanet exoPlanet, int robotID, RobotVariant robotVariant)
        {
            IRobot robotBase = robotVariant switch
            {
                RobotVariant.DEFAULT => new DefaultBot(exoPlanet, robotID),
                RobotVariant.SCOUT => new ScoutBot(exoPlanet, robotID),
                RobotVariant.LAVA => new LavaBot(exoPlanet, robotID),
                RobotVariant.AQUA => new AquaBot(exoPlanet, robotID),
                RobotVariant.MUD => new MudBot(exoPlanet, robotID),
                _ => new DefaultBot(exoPlanet, robotID)
            };

            return robotBase;
        }
    }
}