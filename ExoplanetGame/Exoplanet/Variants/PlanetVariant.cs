using System.ComponentModel;

namespace ExoplanetGame.Exoplanet.Variants;

public enum PlanetVariant
{
    [Description("Gaia - Earth-like exoplanet")]
    GAIA,

    [Description("Aquatica - Water exoplanet")]
    AQUATICA,

    [Description("Terra - Stone exoplanet")]
    TERRA,

    [Description("Frostfell - Ice exoplanet")]
    FROSTFELL,

    [Description("Lavaria - Lava exoplanet")]
    LAVARIA,

    [Description("Tropica - Jungle exoplanet")]
    TROPICA,
}