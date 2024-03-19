using Exoplanet.exo;
using ExoplanetGame.Models;

namespace ExoplanetGame.ControlCenter;

public class ControlCenter
{
    private PlanetMap planetMap;
    private Dictionary<RemoteRobot.RemoteRobot, Position> robots;
    public Exoplanet.Exoplanet exoPlanet;

    public ControlCenter(Exoplanet.Exoplanet exoPlanet)
    {
        robots = new Dictionary<RemoteRobot.RemoteRobot, Position>();
        this.exoPlanet = exoPlanet;
    }

    public void Init(PlanetSize planetSize)
    {
        planetMap = new PlanetMap(planetSize);
    }

    public void AddRobot(RemoteRobot.RemoteRobot robot)
    {
        robots.Add(robot, new Position(0, 0));
    }

    public void UpdateRobotPosition(RemoteRobot.RemoteRobot robot, Position position)
    {
        robots[robot] = position;
    }

    public void UpdatePosition(Position? parse)
    {
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

    public RemoteRobot.RemoteRobot GetRobotByID(int robotId)
    {
        return robots.Keys.ElementAt(robotId);
    }

    public void RemoveRobot(RemoteRobot.RemoteRobot remoteRobot)
    {
        robots.Remove(remoteRobot);
    }
}