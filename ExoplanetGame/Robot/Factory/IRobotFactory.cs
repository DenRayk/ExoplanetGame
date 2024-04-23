using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Robot.Factory;

public interface IRobotFactory
{
    DefaultBot CreateDefaultRobot(ControlCenter.ControlCenter controlCenter, ExoplanetBase exoPlanet, int robotID);

    ScoutBot CreateScoutRobot(ControlCenter.ControlCenter controlCenter, ExoplanetBase exoPlanet, int robotID);

    LavaBot CreateLavaRobot(ControlCenter.ControlCenter controlCenter, ExoplanetBase exoPlanet, int robotID);

    AquaBot CreateAquaRobot(ControlCenter.ControlCenter controlCenter, ExoplanetBase exoPlanet, int robotID);

    MudBot CreateMudRobot(ControlCenter.ControlCenter controlCenter, ExoplanetBase exoPlanet, int robotID);
}