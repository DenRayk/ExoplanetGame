namespace ExoplanetGame.RemoteRobot
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

        public RobotBase CreateRemoteRobot(ControlCenter.ControlCenter controlCenter, Exoplanet.Exoplanet exoPlanet, int robotID)
        {
            return new RemoteRobot(controlCenter, exoPlanet, robotID);
        }
    }
}