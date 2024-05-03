using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Exoplanet.PlanetEvents
{
    public interface FreezeUseCase
    {
        RobotResultBase FreezeRobotIfItHasntMovedForAWhile(IRobot robot);
    }
}