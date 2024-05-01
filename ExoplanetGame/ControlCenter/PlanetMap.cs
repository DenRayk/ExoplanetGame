using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.ControlCenter
{
    public class PlanetMap
    {
        public PlanetSize PlanetSize { get; set; }
        public Ground[,] map { get; set; }

        public PlanetMap(PlanetSize planetSize)
        {
            PlanetSize = planetSize;
            map = new Ground[planetSize.Height, planetSize.Width];
            for (int i = 0; i < planetSize.Height; i++)
            {
                for (int j = 0; j < planetSize.Width; j++)
                {
                    map[i, j] = Ground.NOTHING;
                }
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

        public string GetPercentageOfExploredArea()
        {
            int totalArea = PlanetSize.Height * PlanetSize.Width;
            int exploredArea = 0;

            for (int rowIndex = 0; rowIndex < PlanetSize.Height; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < PlanetSize.Width; columnIndex++)
                {
                    bool isAreaExplored = map[rowIndex, columnIndex] != Ground.NOTHING;

                    if (isAreaExplored)
                    {
                        exploredArea++;
                    }
                }
            }

            double exploredAreaPercentage = (double)exploredArea / totalArea * 100;
            string formattedPercentage = exploredAreaPercentage.ToString("0.00") + "%";

            return formattedPercentage;
        }
    }
}