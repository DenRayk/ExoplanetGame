using System.ComponentModel;

namespace ExoplanetGame.Robot.Variants;

public enum RobotVariant
{
    [Description("Default Robot")]
    DEFAULT,

    [Description("Scout Robot")]
    SCOUT,

    [Description("Lava Robot")]
    LAVA,

    [Description("Aqua Robot")]
    AQUA,

    [Description("Mud Robot")]
    MUD,

    [Description("Solar Robot")]
    SOLAR,
}
