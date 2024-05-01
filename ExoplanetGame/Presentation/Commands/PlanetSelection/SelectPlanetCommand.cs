using ExoplanetGame.Application;
using ExoplanetGame.Application.ControlCenter;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Presentation.Commands.ControlCenter;

namespace ExoplanetGame.Presentation.Commands.PlanetSelection
{
    internal class SelectPlanetCommand : BaseCommand
    {
        private PlanetVariant planetVariant;

        private UCCollection ucCollection;

        private ExoplanetService exoplanetService;

        public SelectPlanetCommand(PlanetVariant planetVariant, UCCollection ucCollection, ExoplanetService exoplanetService)
        {
            this.planetVariant = planetVariant;
            this.ucCollection = ucCollection;
            this.exoplanetService = exoplanetService;
        }

        public override void Execute()
        {
            exoplanetService.CreateExoPlanet(planetVariant);
            ucCollection.UcCollectionControlCenter.SelectPlanetUseCase.SelectPlanet(exoplanetService.ExoPlanet);

            ControlCenterCommand controlCenterCommand = new(ucCollection);
            controlCenterCommand.Execute();
        }
    }
}