﻿using System.ComponentModel;

namespace ExoplanetGame.Domain.Robot.Movement;

public enum Direction
{
    [Description("North")]
    NORTH,

    [Description("East")]
    EAST,

    [Description("South")]
    SOUTH,

    [Description("West")]
    WEST
}