using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Exoplanet.PlanetEvents
{
    public interface RandomAttackUseCase
    {
        bool HandleMysteriousAttack(RobotBase robot, out RobotResultBase robotResult);
    }
}