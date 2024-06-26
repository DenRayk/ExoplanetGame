﻿using ExoplanetGame.Application;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Presentation.Commands.PlanetSelection;

namespace ExoplanetGame
{
    internal class ExoplanetGame
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("ExoplanetGame starting...");

            UCCollection ucCollection = new();
            ExoplanetService exoplanetService = new();

            ucCollection.Init(exoplanetService);

            ShowPlanetSelectionCommand showPlanetSelectionCommand = new(ucCollection, exoplanetService);
            showPlanetSelectionCommand.Execute();
        }
    }
}