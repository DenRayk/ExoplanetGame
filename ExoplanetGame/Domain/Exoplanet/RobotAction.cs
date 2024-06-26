﻿using System.ComponentModel;

namespace ExoplanetGame.Domain.Exoplanet
{
    public enum RobotAction
    {
        [Description("Land")]
        LAND,

        [Description("Move")]
        MOVE,

        [Description("Rotate")]
        ROTATE,

        [Description("Scan")]
        SCAN,

        [Description("Get position")]
        GETPOSITION,

        [Description("Load")]
        LOAD
    }
}