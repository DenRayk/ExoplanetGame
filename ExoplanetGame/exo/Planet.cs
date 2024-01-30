namespace Exoplanet.exo;

public interface Planet
{
    Measure Land(Robot robot, Position position);

    Position GetPosition(Robot robot);

    Position Move(Robot robot);

    Direction? Rotate(Robot robot, Rotation rotation);

    Measure Scan(Robot robot);

    PlanetSize GetSize();

    void Remove(Robot robot);

    RobotStatus Charge(Robot robot, int value);
}