using ExoplanetGame.Application;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.Exoplanet.Variants;
using ExoplanetGame.Presentation.Commands.ControlCenter;

namespace ExoplanetGame.Presentation.Commands.PlanetSelection
{
    public class SelectPlanetCommand : BaseCommand
    {
        private readonly PlanetVariant planetVariant;

        private readonly UCCollection ucCollection;

        private readonly ExoplanetService exoplanetService;

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