using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Exoplanet.PlanetEvents
{
    public interface VolcanicEruptionUseCase
    {
        bool HandleVolcanicEruption(RobotBase robot, out RobotResultBase robotResult);
    }
}