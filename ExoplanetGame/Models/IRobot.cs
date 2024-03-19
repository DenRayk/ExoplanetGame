using ExoplanetGame.Models;

public interface IRobot
{
    public int RobotID { get; set; }

    void Crash();

    void Land(Position landPosition);

    Measure Scan();

    Position Move();

    void Rotate(Rotation rotation);

    string GetLanderName();
}