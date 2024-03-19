using ExoplanetGame.Models;

namespace ExoplanetGame.ControlCenter
{
    internal class PlanetMap
    {
        public PlanetSize planetSize { get; set; }
        public Ground[,] map { get; set; }

        public PlanetMap(PlanetSize planetSize)
        {
            this.planetSize = planetSize;
            map = new Ground[planetSize.Height, planetSize.Width];
            for (int i = 0; i < planetSize.Height; i++)
            {
                for (int j = 0; j < planetSize.Width; j++)
                {
                    map[i, j] = Ground.NICHTS;
                }
            }
        }

        public void printMap()
        {
            for (int i = 0; i < planetSize.Height; i++)
            {
                for (int j = 0; j < planetSize.Width; j++)
                {
                    GroundPrinter.printGround(map[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void updateMap(Position position, Ground ground)
        {
            map[position.Y, position.X] = ground;
        }

        public Ground getGround(int x, int y)
        {
            return map[x, y];
        }
    }
}