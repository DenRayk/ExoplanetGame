using Exoplanet.exo;

namespace ExoplanetGame.Models;

public interface IPlanet
{
    bool Land(RemoteRobot.RemoteRobot robot, Position landPosition);

    Position GetPosition(RemoteRobot.RemoteRobot robot);

    Position Move(RemoteRobot.RemoteRobot robot);

    Direction? Rotate(RemoteRobot.RemoteRobot robot, Rotation rotation);

    Measure Scan(RemoteRobot.RemoteRobot robot);

    PlanetSize GetSize();

    void Remove(RemoteRobot.RemoteRobot robot);
}