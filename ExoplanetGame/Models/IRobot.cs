using ExoplanetGame.Models;

namespace Exoplanet.exo;

public interface IRobot
{
    public int RobotID { get; set; }

    void Crash();

    void Land(Position landPosition);

    string GetLanderName();
}