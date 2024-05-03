using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Robot
{
    public interface RobotPartsHealthUseCase
    {
        Dictionary<RobotPart, int> GetRobotPartsByRobot(IRobot robot);

        void RepairRobotPart(IRobot robot, RobotPart robotPart);
    }
}