using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Robot.Factory;

public interface IRobotFactory
{
    RobotBase CreateRobot(ControlCenter.ControlCenter controlCenter, IExoPlanet exoPlanet, int robotID, RobotVariant robotVariant);
}