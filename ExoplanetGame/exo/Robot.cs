using System.Net.Sockets;

namespace Exoplanet.exo;

public interface Robot
{
    void initRun(Planet planet, string name, Position position, string status, RobotStatus robotStatus, TcpClient tcpClient);

    void crash();

    void statusChanged(RobotStatus robotStatus);

    string getLanderName();
}