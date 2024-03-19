using Exoplanet.exo;

namespace ExoplanetGame.Models;

public interface IPlanet
{
    bool Land(RemoteRobot.RemoteRobot remoteRobot, Position landPosition);

    Position GetPosition(RemoteRobot.RemoteRobot robot);

    Position Move(RemoteRobot.RemoteRobot remoteRobot);

    Direction Rotate(RemoteRobot.RemoteRobot robot, Rotation rotation);

    Measure Scan(RemoteRobot.RemoteRobot robot);

    void Remove(RemoteRobot.RemoteRobot robot);
}