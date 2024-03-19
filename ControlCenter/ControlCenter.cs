using ControlCenter.Models;

namespace ControlCenter;

public class ControlCenter
{
    private PlanetMap planetMap;
    private Dictionary<Robot, Position> robotPositions;

    public ControlCenter()
    {
        robotPositions = new Dictionary<Robot, Position>();
    }

    public void Init(PlanetSize planetSize)
    {
        planetMap = new PlanetMap(planetSize);
    }

    public void AddRobot(Robot robot)
    {
        robotPositions.Add(robot, new Position(0, 0));
    }

    public void UpdateRobotPosition(Robot robot, Position position)
    {
        robotPositions[robot] = position;
    }

    public void Crash(Robot robot)
    {
        robotPositions.Remove(robot);
    }

    public void AddMeasure(ControlMeasure measure)
    {
        planetMap.updateMap(measure.position.X, measure.position.Y, measure.Ground);
    }

    public void UpdatePosition(Position? parse)
    {
    }
}