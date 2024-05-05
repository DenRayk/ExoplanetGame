using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Domain.Exoplanet.Environment
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
            Ground ground = g switch
            {
                'W' => Ground.WATER,
                'R' => Ground.ROCK,
                'S' => Ground.SAND,
                'G' => Ground.GRAVEL,
                'P' => Ground.PLANT,
                'M' => Ground.MUD,
                'L' => Ground.LAVA,
                'I' => Ground.ICE,
                'N' => Ground.SNOW,
                _ => Ground.NOTHING
            };
            return ground;
        }

        private double GetRandomTemperature(Ground ground, Random rand)
        {
            double temperature = ground switch
            {
                Ground.SAND => rand.Next(20, 35),
                Ground.GRAVEL => rand.Next(10, 25),
                Ground.ROCK => rand.Next(0, 15),
                Ground.WATER => rand.Next(0, 10),
                Ground.PLANT => rand.Next(5, 25),
                Ground.MUD => rand.Next(5, 20),
                Ground.LAVA => rand.Next(800, 1500),
                Ground.SNOW => rand.Next(0, 5),
                Ground.ICE => rand.Next(-5, 0),
                _ => 0
            };

            return temperature;
        }
    }
}