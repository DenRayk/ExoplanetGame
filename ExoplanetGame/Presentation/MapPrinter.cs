using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Robot;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot.Movement;

namespace ExoplanetGame.Presentation
{
    internal class MapPrinter
    {
        public static void PrintMap(Dictionary<RobotBase, Position> robots, PlanetMap planetMap)
        {
            for (int i = 0; i < planetMap.PlanetSize.Height; i++)
            {
                for (int j = 0; j < planetMap.PlanetSize.Width; j++)
                {
                    if (robots.ContainsValue(new Position(j, i)))
                    {
                        foreach (var robot in robots)
                        {
                            if (robot.Value == null) continue;

                            if (robot.Value.X == j && robot.Value.Y == i)
                            {
                                printGroundWithRobot(planetMap.map[i, j], robot.Key);
                            }
                        }
                    }
                    else
                    {
                        printGround(planetMap.map[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }

        public static void printGround(Ground ground)
        {
            switch (ground)
            {
                case Ground.NOTHING:
                    Console.BackgroundColor = ConsoleColor.White;
                    break;

                case Ground.SAND:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;

                case Ground.GRAVEL:
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;

                case Ground.ROCK:
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;

                case Ground.WATER:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;

                case Ground.PLANT:
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;

                case Ground.MUD:
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    break;

                case Ground.LAVA:
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;

                case Ground.SNOW:
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    break;

                case Ground.ICE:
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    break;

                default:
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
            }
            Console.Write("   ");
            Console.ResetColor();
        }

        public static void printGroundWithRobot(Ground ground, RobotBase robot)
        {
            switch (ground)
            {
                case Ground.NOTHING:
                    Console.BackgroundColor = ConsoleColor.White;
                    break;

                case Ground.SAND:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;

                case Ground.GRAVEL:
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;

                case Ground.ROCK:
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;

                case Ground.WATER:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;

                case Ground.PLANT:
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;

                case Ground.MUD:
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    break;

                case Ground.LAVA:
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;

                case Ground.SNOW:
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    break;

                case Ground.ICE:
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    break;

                default:
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
            }
            Console.Write($" {robot.RobotInformation.RobotID} ");
            Console.ResetColor();
        }
    }
}