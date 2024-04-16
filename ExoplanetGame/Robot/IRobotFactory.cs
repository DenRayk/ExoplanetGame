using ExoplanetGame.Exoplanet;

namespace ExoplanetGame.Robot;

public interface IRobotFactory
{
    RobotBase CreateDefaultRobot(ControlCenter.ControlCenter controlCenter, ExoplanetBase exoPlanet, int robotID);

    RobotBase CreateScoutRobot(ControlCenter.ControlCenter controlCenter, ExoplanetBase exoPlanet, int robotID);

    RobotBase CreateLavaRobot(ControlCenter.ControlCenter controlCenter, ExoplanetBase exoPlanet, int robotID);

    RobotBase CreateAquaRobot(ControlCenter.ControlCenter controlCenter, ExoplanetBase exoPlanet, int robotID);

    RobotBase CreateMudRobot(ControlCenter.ControlCenter controlCenter, ExoplanetBase exoPlanet, int robotID);
}