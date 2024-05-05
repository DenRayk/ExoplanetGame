using ExoplanetGame.Domain.Exoplanet;

namespace ExoplanetGame.Domain.Robot.Factory;

public abstract class RobotFactory
{
    public abstract IRobot CreateRobot(IExoPlanet exoPlanet, int robotID);
}