using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Robot.Factory
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

        public RobotBase CreateRobot(ControlCenter.ControlCenter controlCenter, IExoPlanet exoPlanet, int robotID, RobotVariant robotVariant)
        {
            RobotBase robotBase;

            switch (robotVariant)
            {
                case RobotVariant.DEFAULT:
                    robotBase = new DefaultBot(controlCenter, exoPlanet, robotID);
                    break;

                case RobotVariant.SCOUT:
                    robotBase = new ScoutBot(controlCenter, exoPlanet, robotID);
                    break;

                case RobotVariant.LAVA:
                    robotBase = new LavaBot(controlCenter, exoPlanet, robotID);
                    break;

                case RobotVariant.AQUA:
                    robotBase = new AquaBot(controlCenter, exoPlanet, robotID);
                    break;

                case RobotVariant.MUD:
                    robotBase = new MudBot(controlCenter, exoPlanet, robotID);
                    break;

                default:
                    robotBase = new DefaultBot(controlCenter, exoPlanet, robotID);
                    break;
            }

            return robotBase;
        }
    }
}