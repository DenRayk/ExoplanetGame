namespace ControlCenter.exo;

public interface Planet
{
    Measure Land(Robot robot, Position landPosition);

    Position GetPosition(Robot robot);

    Position Move(Robot robot);

    Direction? Rotate(Robot robot, Rotation rotation);

    Measure Scan(Robot robot);

    PlanetSize GetSize();

    void Remove(Robot robot);
}