using System.ComponentModel;

namespace ExoplanetGame.Exoplanet;

public enum Ground
{

    [Description("Unknown Ground")]
    NOTHING,

    [Description("Sand")]
    SAND,

    [Description("Gravel")]
    GRAVEL,

    [Description("Rock")]
    ROCK,

    [Description("Water")]
    WATER,

    [Description("Plant")]
    PLANT,

    [Description("Mud")]
    MUD,

    [Description("Lava")]
    LAVA
}