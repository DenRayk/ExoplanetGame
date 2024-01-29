using System.Net.Sockets;

namespace RemoteRobot.exo;

public interface Robot
{
    void InitRun(Planet planet, string name, Position position, string status, RobotStatus robotStatus, StreamWriter outStream);

    void Crash();

    void StatusChanged(RobotStatus robotStatus);

    string GetLanderName();
}