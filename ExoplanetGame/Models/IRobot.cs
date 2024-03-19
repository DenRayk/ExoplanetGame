namespace Exoplanet.exo;

public interface IRobot
{
    public int robotID { get; }
    public Position position { get; set; }

    void Crash();

    string GetLanderName();
}