using Exoplanet.exo;

namespace Exoplanet.exoServer;

public class RobotStatusMsg(float temperature, int energy, string msg) : RobotStatus
{
    public float GetWorkTemp()
    {
        return temperature;
    }

    public int GetEnergy()
    {
        return energy;
    }

    public string GetMessage()
    {
        return msg;
    }
}