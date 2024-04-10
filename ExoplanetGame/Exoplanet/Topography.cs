using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using System;

namespace ExoplanetGame.Exoplanet
{
    public class Topography
    {
        private Measure[][] topography;
        public PlanetSize PlanetSize { get; set; }

        public Topography(string[] topographyStr)
        {
            InitializeFromStrings(topographyStr);
        }

        private void InitializeFromStrings(string[] strings)
        {
            PlanetSize = new PlanetSize(strings[0].Length, strings.Length);
            topography = new Measure[PlanetSize.Height][];

            for (int y = 0; y < PlanetSize.Height; y++)
            {
                topography[y] = new Measure[PlanetSize.Width];
                for (int x = 0; x < PlanetSize.Width; x++)
                {
                    Ground ground = GroundFromChar(strings[y][x]);
                    topography[y][x] = new Measure(ground, GetRandomTemperature(ground, new Random()));
                }
            }
        }

        public Measure GetMeasureAtPosition(Position position)
        {
            int x = position.X;
            int y = position.Y;
            if (x < 0 || y < 0 || x >= topography[0].Length || y >= topography.Length)
                return new Measure();

            return topography[y][x];
        }

        private Ground GroundFromChar(char g)
        {
            Ground ground;

            switch (g)
            {
                case 'W':
                    ground = Ground.WASSER;
                    break;

                case 'F':
                    ground = Ground.FELS;
                    break;

                case 'S':
                    ground = Ground.SAND;
                    break;

                case 'G':
                    ground = Ground.GEROELL;
                    break;

                case 'P':
                    ground = Ground.PFLANZEN;
                    break;

                case 'M':
                    ground = Ground.MORAST;
                    break;

                case 'L':
                    ground = Ground.LAVA;
                    break;

                default:
                    ground = Ground.NICHTS;
                    break;
            }
            return ground;
        }

        private double GetRandomTemperature(Ground ground, Random rand)
        {
            double temperature;

            switch (ground)
            {
                case Ground.SAND:
                    temperature = rand.Next(20, 35);
                    break;

                case Ground.GEROELL:
                    temperature = rand.Next(10, 25);
                    break;

                case Ground.FELS:
                    temperature = rand.Next(0, 15);
                    break;

                case Ground.WASSER:
                    temperature = rand.Next(0, 10);
                    break;

                case Ground.PFLANZEN:
                    temperature = rand.Next(5, 25);
                    break;

                case Ground.MORAST:
                    temperature = rand.Next(5, 20);
                    break;

                case Ground.LAVA:
                    temperature = rand.Next(800, 1500);
                    break;

                default:
                    temperature = 0;
                    break;
            }

            return temperature;
        }
    }
}