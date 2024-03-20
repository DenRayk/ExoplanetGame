namespace ExoplanetGame.RemoteRobot;

public interface IRobotFactory
{
    RobotBase CreateRemoteRobot(ControlCenter.ControlCenter controlCenter, Exoplanet.Exoplanet exoPlanet, int robotID);
}