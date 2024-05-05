using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Application.Robot
{
    public interface PlanetMapUseCase
    {
        void UpdateMap(Position position, Ground ground);

        string GetPercentageOfExploredArea();
    }
}