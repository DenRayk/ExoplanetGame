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

        public RobotBase CreateDefaultRobot(ControlCenter.ControlCenter controlCenter, IExoplanet exoPlanet, int robotID)
        {
            return new DefaultBot(controlCenter, exoPlanet, robotID);
        }

        public RobotBase CreateScoutRobot(ControlCenter.ControlCenter controlCenter, IExoplanet exoPlanet, int robotID)
        {
            return new ScoutBot(controlCenter, exoPlanet, robotID);
        }

        public RobotBase CreateLavaRobot(ControlCenter.ControlCenter controlCenter, IExoplanet exoPlanet, int robotID)
        {
            return new LavaBot(controlCenter, exoPlanet, robotID);
        }

        public RobotBase CreateAquaRobot(ControlCenter.ControlCenter controlCenter, IExoplanet exoPlanet, int robotID)
        {
            return new AquaBot(controlCenter, exoPlanet, robotID);
        }

        public RobotBase CreateMudRobot(ControlCenter.ControlCenter controlCenter, IExoplanet exoPlanet, int robotID)
        {
            return new MudBot(controlCenter, exoPlanet, robotID);
        }
    }
}