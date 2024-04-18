using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot.Movement;

namespace ExoplanetGame.Exoplanet.Environment
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
                    ground = Ground.WATER;
                    break;

                case 'R':
                    ground = Ground.ROCK;
                    break;

                case 'S':
                    ground = Ground.SAND;
                    break;

                case 'G':
                    ground = Ground.GRAVEL;
                    break;

                case 'P':
                    ground = Ground.PLANT;
                    break;

                case 'M':
                    ground = Ground.MUD;
                    break;

                case 'L':
                    ground = Ground.LAVA;
                    break;

                case 'I':
                    ground = Ground.ICE;
                    break;

                case 'N':
                    ground = Ground.SNOW;
                    break;

                default:
                    ground = Ground.NOTHING;
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

                case Ground.GRAVEL:
                    temperature = rand.Next(10, 25);
                    break;

                case Ground.ROCK:
                    temperature = rand.Next(0, 15);
                    break;

                case Ground.WATER:
                    temperature = rand.Next(0, 10);
                    break;

                case Ground.PLANT:
                    temperature = rand.Next(5, 25);
                    break;

                case Ground.MUD:
                    temperature = rand.Next(5, 20);
                    break;

                case Ground.LAVA:
                    temperature = rand.Next(800, 1500);
                    break;

                case Ground.SNOW:
                    temperature = rand.Next(0, 5);
                    break;

                case Ground.ICE:
                    temperature = rand.Next(-5, 0);
                    break;

                default:
                    temperature = 0;
                    break;
            }

            return temperature;
        }
    }
}