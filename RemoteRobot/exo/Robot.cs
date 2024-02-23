namespace RemoteRobot.exo;

public interface Robot
{
    void InitRun(Planet planet, string name, Position position, string status);

    void Crash();

    string GetLanderName();
}