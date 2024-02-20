namespace Exoplanet.exo;

public interface Robot
{
    void InitRun(Planet planet, string name, Position position, string status);

    void Crash();

    void StatusChanged();

    string GetLanderName();
}