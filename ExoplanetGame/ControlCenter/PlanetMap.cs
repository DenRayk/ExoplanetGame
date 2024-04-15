using ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot;

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
                    map[i, j] = Ground.NOTHING;
                }
            }
        }

        public void printMap(Dictionary<RobotBase, Position> robots)
        {
            for (int i = 0; i < planetSize.Height; i++)
            {
                for (int j = 0; j < planetSize.Width; j++)
                {
                    if (robots.ContainsValue(new Position(j, i)))
                    {
                        foreach (var robot in robots)
                        {
                            if (robot.Value == null) continue;

                            if (robot.Value.X == j && robot.Value.Y == i)
                            {
                                GroundPrinter.printGroundWithRobot(map[i, j], robot.Key);
                            }
                        }
                    }
                    else
                    {
                        GroundPrinter.printGround(map[i, j]);
                    }
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

        public string GetPercentageOfExploredArea()
        {
            int totalArea = planetSize.Height * planetSize.Width;
            int exploredArea = 0;

            for (int rowIndex = 0; rowIndex < planetSize.Height; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < planetSize.Width; columnIndex++)
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