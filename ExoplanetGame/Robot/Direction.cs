﻿using System.ComponentModel;

namespace ExoplanetGame.Robot;

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