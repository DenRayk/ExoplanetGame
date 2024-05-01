using ExoplanetGame.Application;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.Exoplanet.Variants;
using ExoplanetGame.Helper;

namespace ExoplanetGame.Presentation.Commands.PlanetSelection
{
    internal class ShowPlanetSelectionCommand : BaseCommand
    {
        private UCCollection ucCollection;
        private ExoplanetService exoplanetService;

        private readonly string helpText =
            "Exoplanet Menu Information\n" +
            $"{PlanetVariant.GAIA.GetDescriptionFromEnum()}:\t Beginner level\n" +
            $"{PlanetVariant.AQUATICA.GetDescriptionFromEnum()}:\t Casual level\n" +
            $"{PlanetVariant.TERRA.GetDescriptionFromEnum()}:\t Intermediate level\n" +
            $"{PlanetVariant.FROSTFELL.GetDescriptionFromEnum()}:\t Advanced level\n" +
            $"{PlanetVariant.LAVARIA.GetDescriptionFromEnum()}:\t Expert level\n" +
            $"{PlanetVariant.TROPICA.GetDescriptionFromEnum()}:\t Grandmaster level\n";

        public ShowPlanetSelectionCommand(UCCollection ucCollection, ExoplanetService exoplanetService, BaseCommand previousCommand)
        {
            this.ucCollection = ucCollection;
            this.exoplanetService = exoplanetService;
        }

        public override void Execute()
        {
            Console.WriteLine("Choose a destination planet (press F1 for help):\n");

            var options = getPlanetOptions();

            BaseCommand baseCommand;
            do
            {
                baseCommand = ReadUserInputWithOptions(options);

                if (baseCommand is HelpCommand helpCommand)
                {
                    helpCommand.HelpText = helpText;
                    helpCommand.Execute();
                }
            } while (baseCommand is HelpCommand);

            baseCommand.Execute();
        }

        private Dictionary<string, BaseCommand> getPlanetOptions()
        {
            var options = new Dictionary<string, BaseCommand>();

            foreach (PlanetVariant planetVariant in Enum.GetValues(typeof(PlanetVariant)))
            {
                options.Add(planetVariant.GetDescriptionFromEnum(), new SelectPlanetCommand(planetVariant, ucCollection, exoplanetService, this));
            }
            options.Add("Random", new SelectPlanetCommand(getRandomPlanetVariant(), ucCollection, exoplanetService, this));
            return options;
        }

        private PlanetVariant getRandomPlanetVariant()
        {
            Array values = Enum.GetValues(typeof(PlanetVariant));
            Random random = new Random();
            return (PlanetVariant)(values.GetValue(random.Next(values.Length)) ?? PlanetVariant.GAIA);
        }
    }
}