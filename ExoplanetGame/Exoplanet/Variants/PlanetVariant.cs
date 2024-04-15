using System.ComponentModel;

namespace ExoplanetGame.Exoplanet.Variants;

public enum PlanetVariant
{
    [Description("Earth-like exoplanet")]
    GAIA,

    [Description("Water exoplanet")]
    AQUATICA,

    [Description("Stone exoplanet")]
    TERRA,

    [Description("Lava exoplanet")]
    LAVARIA,

    [Description("Jungle exoplanet")]
    TROPICA,
}