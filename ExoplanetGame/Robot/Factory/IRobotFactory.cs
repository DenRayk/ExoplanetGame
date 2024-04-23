using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.ExoplanetGame.Exoplanet;

namespace ExoplanetGame.Robot.Factory;

public interface IRobotFactory
{
    RobotBase CreateDefaultRobot(ControlCenter.ControlCenter controlCenter, IExoplanet exoPlanet, int robotID);

    RobotBase CreateScoutRobot(ControlCenter.ControlCenter controlCenter, IExoplanet exoPlanet, int robotID);

    RobotBase CreateLavaRobot(ControlCenter.ControlCenter controlCenter, IExoplanet exoPlanet, int robotID);

    RobotBase CreateAquaRobot(ControlCenter.ControlCenter controlCenter, IExoplanet exoPlanet, int robotID);

    RobotBase CreateMudRobot(ControlCenter.ControlCenter controlCenter, IExoplanet exoPlanet, int robotID);
}