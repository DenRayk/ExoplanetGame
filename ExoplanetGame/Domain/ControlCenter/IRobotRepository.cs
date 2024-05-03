using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Domain.ControlCenter;

public interface IRobotRepository
{
    void AddRobot(RobotBase robot, Position position);

    void RemoveRobot(IRobot robot);

    void MoveRobot(IRobot robot, Position position);

    Position GetRobotPosition(RobotBase robot);

    Dictionary<IRobot, Position> GetRobots();

    public int GetRobotCount();

    void Clear();
}