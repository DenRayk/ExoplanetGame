using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Exoplanet
{
    public interface RobotPartsTrackingUseCase
    {
        void RobotPartDamage(RobotBase robot, RobotPart part);

        bool IsRobotPartDamaged(RobotBase robot, RobotPart part);

        int GetRobotPartHealth(RobotBase robot, RobotPart part);

        Dictionary<RobotPart, int> GetRobotPartsByRobot(RobotBase robot);

        void RepairRobotPart(RobotBase robot, RobotPart part);
    }
}