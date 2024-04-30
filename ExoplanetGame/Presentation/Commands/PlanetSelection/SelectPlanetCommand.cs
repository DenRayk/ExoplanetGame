using ExoplanetGame.Application;
using ExoplanetGame.Application.ControlCenter;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Presentation.Commands.ControlCenter;

namespace ExoplanetGame.Presentation.Commands.PlanetSelection
{
    internal class SelectPlanetCommand : BaseCommand
    {
        private PlanetVariant planetVariant;

        private UCCollection ucCollection;

        public SelectPlanetCommand(PlanetVariant planetVariant, UCCollection ucCollection)
        {
            this.planetVariant = planetVariant;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            ucCollection.UcCollectionControlCenter.SelectPlanetUseCase.SelectPlanet(planetVariant);

            ControlCenterCommand controlCenterCommand = new(ucCollection);
            controlCenterCommand.Execute();
        }
    }
}