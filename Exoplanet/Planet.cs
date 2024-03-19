﻿namespace Exoplanet.exo;

public interface Planet
{
    Measure Land(IRobot robot, Position landPosition);

    Position GetPosition(IRobot robot);

    Position Move(IRobot robot);

    Direction? Rotate(IRobot robot, Rotation rotation);

    Measure Scan(IRobot robot);

    PlanetSize GetSize();

    void Remove(IRobot robot);
}