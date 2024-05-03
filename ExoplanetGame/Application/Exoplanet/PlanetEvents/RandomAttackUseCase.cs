using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Exoplanet.PlanetEvents
{
    public interface RandomAttackUseCase
    {
        RobotResultBase HandleMysteriousAttack(IRobot robot);
    }
}