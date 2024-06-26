﻿using ExoplanetGame.Application;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.Exoplanet.Variants;
using ExoplanetGame.Helper;

namespace ExoplanetGame.Presentation.Commands.PlanetSelection
{
    internal class ShowPlanetSelectionCommand : BaseCommand
    {
        private readonly UCCollection ucCollection;
        private readonly ExoplanetService exoplanetService;

        private readonly string helpText =
            "Exoplanet Menu Information\n" +
            $"{PlanetVariant.GAIA.GetDescriptionFromEnum()}:\t Beginner level\n" +
            $"{PlanetVariant.AQUATICA.GetDescriptionFromEnum()}:\t Casual level\n" +
            $"{PlanetVariant.TERRA.GetDescriptionFromEnum()}:\t Intermediate level\n" +
            $"{PlanetVariant.FROSTFELL.GetDescriptionFromEnum()}:\t Advanced level\n" +
            $"{PlanetVariant.LAVARIA.GetDescriptionFromEnum()}:\t Expert level\n" +
            $"{PlanetVariant.TROPICA.GetDescriptionFromEnum()}:\t Grandmaster level\n";

        public ShowPlanetSelectionCommand(UCCollection ucCollection, ExoplanetService exoplanetService)
        {
            this.ucCollection = ucCollection;
            this.exoplanetService = exoplanetService;
        }

        public override void Execute()
        {
            var options = GetPlanetOptions();

            BaseCommand baseCommand;
            do
            {
                Console.WriteLine("Choose a destination planet (press F1 for help):\n");
                baseCommand = ReadUserInputWithOptions(options);

                if (baseCommand is HelpCommand helpCommand)
                {
                    helpCommand.HelpText = helpText;
                    helpCommand.Execute();
                }
            } while (baseCommand is HelpCommand);

            baseCommand.Execute();
        }

        private Dictionary<string, BaseCommand> GetPlanetOptions()
        {
            var options = new Dictionary<string, BaseCommand>();

            foreach (PlanetVariant planetVariant in Enum.GetValues(typeof(PlanetVariant)))
            {
                options.Add(planetVariant.GetDescriptionFromEnum(), new SelectPlanetCommand(planetVariant, ucCollection, exoplanetService));
            }
            options.Add("Random", new SelectPlanetCommand(GetRandomPlanetVariant(), ucCollection, exoplanetService));
            return options;
        }

        private PlanetVariant GetRandomPlanetVariant()
        {
            Array values = Enum.GetValues(typeof(PlanetVariant));
            Random random = new Random();
            return (PlanetVariant)(values.GetValue(random.Next(values.Length)) ?? PlanetVariant.GAIA);
        }
    }
}