using Exoplanet.exo;

//Good
namespace Exoplanet.exoServer;

public class RobotStatusMsg : RobotStatus
{
    private float temperature;
    private int energy;
    private string msg;

    public RobotStatusMsg(float temperature, int energy, string msg)
    {
        temperature = temperature;
        energy = energy;
        msg = msg;
    }

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