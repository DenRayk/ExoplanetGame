namespace ExoplanetGame.RemoteRobot
{
    internal class RobotFactory : IRobotFactory
    {
        public RobotBase CreateRemoteRobot(ControlCenter.ControlCenter controlCenter, Exoplanet.Exoplanet exoPlanet, int robotID)
        {
            return new RemoteRobot(controlCenter, exoPlanet, robotID);
        }
    }
}