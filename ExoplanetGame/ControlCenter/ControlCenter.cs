using ExoplanetGame.Exoplanet;
using ExoplanetGame.RemoteRobot;

namespace ExoplanetGame.ControlCenter;

public class ControlCenter
{
    private static ControlCenter controlCenter;

    private PlanetMap planetMap;
    private Dictionary<RobotBase, Position> robots;
    public Exoplanet.Exoplanet exoPlanet;

    public ControlCenter(Exoplanet.Exoplanet exoPlanet)
    {
        robots = new Dictionary<RobotBase, Position>();
        this.exoPlanet = exoPlanet;
    }

    public static ControlCenter GetInstance(Exoplanet.Exoplanet exoPlanet)
    {
        if (controlCenter == null)
        {
            controlCenter = new ControlCenter(exoPlanet);
        }
        return controlCenter;
    }

    public void Init(PlanetSize planetSize)
    {
        planetMap = new PlanetMap(planetSize);
        Console.WriteLine("Control center initialized.");
        Console.WriteLine("Planet size: " + planetSize);
    }

    public void AddRobot(RobotBase robotBase)
    {
        robots.Add(robotBase, new Position(0, 0));
    }

    public void UpdateRobotPosition(RemoteRobot.RemoteRobot robot, Position position)
    {
        robots[robot] = position;
    }

    public void AddMeasure(Measure measure, Position position)
    {
        planetMap.updateMap(position, measure.Ground);
    }

    public int GetRobotCount()
    {
        return robots.Count;
    }

    public void DisplayRobots()
    {
        for (int i = 0; i < robots.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Robot {i + 1}");
        }
    }

    public RobotBase GetRobotByID(int robotId)
    {
        return robots.Keys.ElementAt(robotId);
    }

    public void RemoveRobot(RobotBase remoteRobot)
    {
        robots.Remove(remoteRobot);
    }

    public void PrintMap()
    {
        planetMap.printMap();
    }
}