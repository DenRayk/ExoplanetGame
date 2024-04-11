namespace ExoplanetGame.Robot;

public interface IRobotFactory
{
    RobotBase CreateDefaultRobot(ControlCenter.ControlCenter controlCenter, Exoplanet.Exoplanet exoPlanet, int robotID);

    RobotBase CreateScoutRobot(ControlCenter.ControlCenter controlCenter, Exoplanet.Exoplanet exoPlanet, int robotID);

    RobotBase CreateLavaRobot(ControlCenter.ControlCenter controlCenter, Exoplanet.Exoplanet exoPlanet, int robotID);

    RobotBase CreateAquaRobot(ControlCenter.ControlCenter controlCenter, Exoplanet.Exoplanet exoPlanet, int robotID);

    RobotBase CreateMudRobot(ControlCenter.ControlCenter controlCenter, Exoplanet.Exoplanet exoPlanet, int robotID);

    RobotBase CreateSolarRobot(ControlCenter.ControlCenter controlCenter, Exoplanet.Exoplanet exoPlanet, int robotID);
}