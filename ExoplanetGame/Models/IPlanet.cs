using Exoplanet.exo;

namespace Exoplanet.Models;

public interface IPlanet
{
    Measure Land(IRobot robot, Position landPosition);

    Position GetPosition(IRobot robot);

    Position Move(IRobot robot);

    Direction? Rotate(IRobot robot, Rotation rotation);

    Measure Scan(IRobot robot);

    PlanetSize GetSize();

    void Remove(IRobot robot);
}