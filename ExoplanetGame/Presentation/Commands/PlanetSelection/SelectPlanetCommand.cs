using ExoplanetGame.Application;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.Exoplanet.Variants;
using ExoplanetGame.Presentation.Commands.ControlCenter;

namespace ExoplanetGame.Presentation.Commands.PlanetSelection
{
    internal class SelectPlanetCommand : BaseCommand
    {
        private PlanetVariant planetVariant;

        private UCCollection ucCollection;

        private ExoplanetService exoplanetService;

        public SelectPlanetCommand(PlanetVariant planetVariant, UCCollection ucCollection, ExoplanetService exoplanetService, BaseCommand previousCommand) : base(previousCommand)
        {
            this.planetVariant = planetVariant;
            this.ucCollection = ucCollection;
            this.exoplanetService = exoplanetService;
        }

        public override void Execute()
        {
            exoplanetService.CreateExoPlanet(planetVariant);
            ucCollection.UcCollectionControlCenter.SelectPlanetUseCase.SelectPlanet(exoplanetService.ExoPlanet);

            ControlCenterCommand controlCenterCommand = new(ucCollection, this);
            controlCenterCommand.Execute();
        }
    }
}