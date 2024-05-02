using System.ComponentModel;

namespace ExoplanetGame.Domain.Exoplanet.Environment;

public enum Weather
{
    [Description("Sunny")]
    SUNNY,

    [Description("Snowy")]
    SNOWY,

    [Description("Rainy")]
    RAINY,

    [Description("Windy")]
    WINDY,

    [Description("Foggy")]
    FOGGY,

    [Description("Cloudy")]
    CLOUDY,

    [Description("Ash in the air")]
    ASH_IN_THE_AIR
}