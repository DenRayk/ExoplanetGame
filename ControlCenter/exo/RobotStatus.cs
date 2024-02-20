namespace Exoplanet.exo;

public interface RobotStatus
{
    float GetWorkTemp();

    int GetEnergy();

    string GetMessage();
}