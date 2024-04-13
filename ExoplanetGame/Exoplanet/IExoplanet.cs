using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;

namespace ExoplanetGame.Exoplanet
{
    public interface IExoplanet
    {
        int GetRobotCount();

        void RemoveRobot(RobotBase robot);

        bool Land(RobotBase robot, Position landPosition);

        Position Move(RobotBase robot);

        Direction Rotate(RobotBase robot, Rotation rotation);

        Measure Scan(RobotBase robot);

        Dictionary<Measure, Position> ScoutScan(RobotBase robot);

        Position GetRobotPosition(RobotBase robot);

        void Remove(RobotBase robot);
    }
}
