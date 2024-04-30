using ExoplanetGame.Exoplanet.Variants;

namespace ExoplanetGame.Presentation.Commands.PlanetSelection
{
    internal class SelectPlanetCommand(PlanetVariant planetVariant) : BaseCommand
    {
        private PlanetVariant planetVariant = planetVariant;

        public override void Execute()
        {
        }
    }
}