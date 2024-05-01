using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Domain.ControlCenter;

public interface IRobotRepository
{
    void AddRobot(RobotBase robot, Position position);

    void RemoveRobot(RobotBase robot);

    void MoveRobot(RobotBase robot, Position position);

    Position GetRobotPosition(RobotBase robot);

    Dictionary<RobotBase, Position> GetRobots();

    public int GetRobotCount();

    void Clear();
}