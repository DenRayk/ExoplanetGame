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
        [Description("Left motor")]
        LEFTMOTOR,

        [Description("Right motor")]
        RIGHTMOTOR,

        [Description("Wheels")]
        WHEELS,

        [Description("Scanning sensor")]
        SCANSENSOR,

        [Description("Movement sensor")]
        MOVEMENTSENSOR,

        [Description("Solar panels")]
        SOLARPANELS
    }
}