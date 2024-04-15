using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Robot
{
    public enum RobotParts
    {
        [Description("Left Motor")]
        LEFTMOTOR,

        [Description("Right Motor")]
        RIGHTMOTOR,

        [Description("Wheels")]
        WHEELS,

        [Description("Scanning Sensor")]
        SCANSENSOR,

        [Description("Movement Sensor")]
        MOVEMENTSENSOR
    }
}