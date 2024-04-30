using ExoplanetGame.Application;
using ExoplanetGame.Application.ControlCenter;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Presentation.Commands.ControlCenter;

namespace ExoplanetGame.Presentation.Commands.PlanetSelection
{
    internal class SelectPlanetCommand(PlanetVariant planetVariant, UCCollection ucCollection) : BaseCommand
    {
        private PlanetVariant planetVariant = planetVariant;

        private UCCollection ucCollection = ucCollection;

        public override void Execute()
        {
            ucCollection.UcCollectionControlCenter.SelectPlanetUseCase.SelectPlanet(planetVariant);

            ControlCenterCommand controlCenterCommand = new();
            controlCenterCommand.Execute();
        }
    }
}