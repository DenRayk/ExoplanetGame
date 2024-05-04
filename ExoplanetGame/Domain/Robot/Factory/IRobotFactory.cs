using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Robot.Variants;

namespace ExoplanetGame.Domain.Robot.Factory;

public interface IRobotFactory
{
    IRobot CreateRobot(ControlCenter.ControlCenter controlCenter, IExoPlanet exoPlanet, int robotID, RobotVariant robotVariant);
}