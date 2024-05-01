using ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Robot.Factory;

public interface IRobotFactory
{
    RobotBase CreateRobot(ControlCenter.ControlCenter controlCenter, ExoPlanetBase exoPlanet, int robotID, RobotVariant robotVariant);
}