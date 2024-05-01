using System.ComponentModel;

namespace ExoplanetGame.Domain.Robot
{
    public enum RobotPart
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