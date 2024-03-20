namespace ExoplanetGame.RemoteRobot;

public interface IRobotFactory
{
    RobotBase CreateRobot(ControlCenter.ControlCenter controlCenter, Exoplanet.Exoplanet exoPlanet, int robotID);
}