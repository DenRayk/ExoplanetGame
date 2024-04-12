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
                    map[i, j] = Ground.NICHTS;
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
    }
}