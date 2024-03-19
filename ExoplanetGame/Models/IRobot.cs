using ExoplanetGame.Models;

namespace Exoplanet.exo;

public interface IRobot
{
    public int robotID { get; set; }

    void Crash();

    string GetLanderName();
}