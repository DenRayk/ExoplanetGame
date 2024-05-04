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
            IRobot robotBase;

            switch (robotVariant)
            {
                case RobotVariant.DEFAULT:
                    robotBase = new DefaultBot(exoPlanet, robotID);
                    break;

                case RobotVariant.SCOUT:
                    robotBase = new ScoutBot(exoPlanet, robotID);
                    break;

                case RobotVariant.LAVA:
                    robotBase = new LavaBot(exoPlanet, robotID);
                    break;

                case RobotVariant.AQUA:
                    robotBase = new AquaBot(exoPlanet, robotID);
                    break;

                case RobotVariant.MUD:
                    robotBase = new MudBot(exoPlanet, robotID);
                    break;

                default:
                    robotBase = new DefaultBot(exoPlanet, robotID);
                    break;
            }

            return robotBase;
        }
    }
}