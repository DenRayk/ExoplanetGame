using System.Net.Sockets;

namespace Exoplanet.exo;

public interface Robot
{
    void InitRun(Planet planet, string name, Position position, string status, RobotStatus robotStatus);

    void Crash();

    void StatusChanged(RobotStatus robotStatus);

    string GetLanderName();
}