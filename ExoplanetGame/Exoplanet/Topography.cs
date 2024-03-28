using ExoplanetGame.ControlCenter;
using ExoplanetGame.RemoteRobot;
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
                    topography[y][x] = new Measure(GroundFromChar(strings[y][x]));
                }
            }
        }

        public Measure GetMeasureAtPosition(Position position)
        {
            int x = position.X;
            int y = position.Y;
            if (x < 0 || y < 0 || x >= topography[0].Length || y >= topography.Length)
                return new Measure(Ground.NICHTS);

            return topography[y][x];
        }

        private Ground GroundFromChar(char g)
        {
            return g switch
            {
                'W' => Ground.WASSER,
                'F' => Ground.FELS,
                'S' => Ground.SAND,
                'G' => Ground.GEROELL,
                'P' => Ground.PFLANZEN,
                'M' => Ground.MORAST,
                'L' => Ground.LAVA,
                _ => Ground.NICHTS
            };
        }
    }
}