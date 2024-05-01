using System.ComponentModel;

namespace ExoplanetGame.Domain.Exoplanet.Environment;

public enum Ground
{
    [Description("Unknown ground")]
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
    LAVA,

    [Description("Snow")]
    SNOW,

    [Description("Ice")]
    ICE,
}