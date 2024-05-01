using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Exoplanet.StatusTracking
{
    public interface FreezeTrackingUseCase
    {
        void FreezeRobot(RobotBase robot);

        bool IsFrozen(RobotBase robot);

        void UpdateLastMove(RobotBase robot);

        DateTime GetLastMove(RobotBase robot);
    }
}