using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Exoplanet.StatusTracking
{
    public interface FreezeTrackingUseCase
    {
        void FreezeRobot(IRobot robot);

        bool IsFrozen(IRobot robot);

        void UpdateLastMove(IRobot robot);

        DateTime GetLastMove(IRobot robot);
    }
}