using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGame.Presentation
{
    internal class MapPrinter
    {
        public static void PrintMap(Dictionary<IRobot, Position> robots, PlanetMap planetMap)
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
                                PrintGroundWithRobot(planetMap.map[i, j], robot.Key);
                            }
                        }
                    }
                    else
                    {
                        PrintGround(planetMap.map[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }

        public static void PrintGround(Ground ground)
        {
            Console.BackgroundColor = ground switch
            {
                Ground.NOTHING => ConsoleColor.White,
                Ground.SAND => ConsoleColor.Yellow,
                Ground.GRAVEL => ConsoleColor.DarkGray,
                Ground.ROCK => ConsoleColor.Gray,
                Ground.WATER => ConsoleColor.Blue,
                Ground.PLANT => ConsoleColor.Green,
                Ground.MUD => ConsoleColor.DarkYellow,
                Ground.LAVA => ConsoleColor.Red,
                Ground.SNOW => ConsoleColor.Cyan,
                Ground.ICE => ConsoleColor.DarkCyan,
                _ => ConsoleColor.White
            };
            Console.Write("   ");
            Console.ResetColor();
        }

        public static void PrintGroundWithRobot(Ground ground, IRobot robot)
        {
            Console.BackgroundColor = ground switch
            {
                Ground.NOTHING => ConsoleColor.White,
                Ground.SAND => ConsoleColor.Yellow,
                Ground.GRAVEL => ConsoleColor.DarkGray,
                Ground.ROCK => ConsoleColor.Gray,
                Ground.WATER => ConsoleColor.Blue,
                Ground.PLANT => ConsoleColor.Green,
                Ground.MUD => ConsoleColor.DarkYellow,
                Ground.LAVA => ConsoleColor.Red,
                Ground.SNOW => ConsoleColor.Cyan,
                Ground.ICE => ConsoleColor.DarkCyan,
                _ => ConsoleColor.White
            };
            Console.Write($" {robot.RobotInformation.RobotID} ");
            Console.ResetColor();
        }
    }
}