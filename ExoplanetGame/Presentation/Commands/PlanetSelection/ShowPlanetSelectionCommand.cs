using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Helper;

namespace ExoplanetGame.Presentation.Commands.PlanetSelection
{
    internal class ShowPlanetSelectionCommand : BaseCommand
    {
        public override void Execute()
        {
            var options = new Dictionary<string, BaseCommand>();

            foreach (PlanetVariant planetVariant in Enum.GetValues(typeof(PlanetVariant)))
            {
                options.Add(planetVariant.GetDescriptionFromEnum(), new SelectPlanetCommand(planetVariant));
            }
            options.Add("Random", new SelectPlanetCommand(getRandomPlanetVariant()));

            BaseCommand selectPlanetCommand = readUserInputWithOptions(options);
            selectPlanetCommand.Execute();
        }

        private PlanetVariant getRandomPlanetVariant()
        {
            Array values = Enum.GetValues(typeof(PlanetVariant));
            Random random = new Random();
            return (PlanetVariant)(values.GetValue(random.Next(values.Length)) ?? PlanetVariant.GAIA);
        }
    }
}