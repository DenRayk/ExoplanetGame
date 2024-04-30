using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot;

namespace ExoplanetGame.ControlCenter;

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