using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Domain.ControlCenter;

public interface IRobotRepository
{
    void AddRobot(IRobot robot, Position position);

    void RemoveRobot(IRobot robot);

    void MoveRobot(IRobot robot, Position position);

    Position GetRobotPosition(IRobot robot);

    Dictionary<IRobot, Position> GetRobots();

    public int GetRobotCount();

    void Clear();
}