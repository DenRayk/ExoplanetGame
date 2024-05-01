using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Robot
{
    public interface RobotScanUseCase
    {
        ScanResult Scan(RobotBase robot);
    }
}