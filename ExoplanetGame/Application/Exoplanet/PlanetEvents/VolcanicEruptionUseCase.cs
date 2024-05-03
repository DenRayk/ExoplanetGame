using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Exoplanet.PlanetEvents
{
    public interface VolcanicEruptionUseCase
    {
        RobotResultBase HandleVolcanicEruption(IRobot robot);
    }
}