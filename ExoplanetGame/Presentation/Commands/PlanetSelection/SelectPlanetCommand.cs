using ExoplanetGame.Application.ControlCenter;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Presentation.Commands.ControlCenter;

namespace ExoplanetGame.Presentation.Commands.PlanetSelection
{
    internal class SelectPlanetCommand(PlanetVariant planetVariant) : BaseCommand
    {
        private PlanetVariant planetVariant = planetVariant;

        private SelectPlanetUseCase selectPlanetUseCase;

        public override void Execute()
        {
            //selectPlanetUseCase.SelectPlanet(planetVariant);

            ControlCenterCommand controlCenterCommand = new();
            controlCenterCommand.Execute();
        }
    }
}