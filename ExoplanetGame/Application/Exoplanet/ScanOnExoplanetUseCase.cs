using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Exoplanet
{
    public interface ScanOnExoplanetUseCase
    {
        ScanResult Scan(RobotBase robot);
    }
}