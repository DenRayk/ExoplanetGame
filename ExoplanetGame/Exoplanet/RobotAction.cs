using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Exoplanet
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

        [Description("Get Position")]
        GETPOSITION
    }
}