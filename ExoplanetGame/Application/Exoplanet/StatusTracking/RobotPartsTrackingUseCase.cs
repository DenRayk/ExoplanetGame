using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Exoplanet
{
    public interface RobotPartsTrackingUseCase
    {
        void RobotPartDamage(IRobot robot, RobotPart part);

        bool IsRobotPartDamaged(IRobot robot, RobotPart part);

        int GetRobotPartHealth(IRobot robot, RobotPart part);

        Dictionary<RobotPart, int> GetRobotPartsByRobot(IRobot robot);

        void RepairRobotPart(IRobot robot, RobotPart part);
    }
}