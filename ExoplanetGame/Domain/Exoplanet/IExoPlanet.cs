using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGame.Domain.Exoplanet
{
    public interface IExoPlanet
    {
        Weather Weather { get; }
        RobotPositionManager RobotPositionManager { get; }
        RobotStatusManager RobotStatusManager { get; }
        Topography Topography { get; }

        void ChangeWeather();
    }
}