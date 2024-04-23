using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Robot.Factory;

public interface IRobotFactory
{
    DefaultBot CreateDefaultRobot(ControlCenter.ControlCenter controlCenter, IExoplanet exoPlanet, int robotID);

    ScoutBot CreateScoutRobot(ControlCenter.ControlCenter controlCenter, IExoplanet exoPlanet, int robotID);

    LavaBot CreateLavaRobot(ControlCenter.ControlCenter controlCenter, IExoplanet exoPlanet, int robotID);

    AquaBot CreateAquaRobot(ControlCenter.ControlCenter controlCenter, IExoplanet exoPlanet, int robotID);

    MudBot CreateMudRobot(ControlCenter.ControlCenter controlCenter, IExoplanet exoPlanet, int robotID);
}