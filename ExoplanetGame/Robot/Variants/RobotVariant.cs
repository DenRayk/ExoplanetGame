using System.ComponentModel;

namespace ExoplanetGame.Robot.Variants;

public enum RobotVariant
{
    [Description("Default robot")]
    DEFAULT,

    [Description("Scout robot")]
    SCOUT,

    [Description("Lava robot")]
    LAVA,

    [Description("Aqua robot")]
    AQUA,

    [Description("Mud robot")]
    MUD,

    [Description("Solar robot")]
    SOLAR,
}