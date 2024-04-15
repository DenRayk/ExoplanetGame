using ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot;

namespace ExoplanetGame.ControlCenter
{
    internal class GroundPrinter
    {
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
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;

                case Ground.ROCK:
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;

                case Ground.WATER:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;

                case Ground.PLANT:
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;

                case Ground.MUD:
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    break;

                case Ground.LAVA:
                    Console.BackgroundColor = ConsoleColor.Red;
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
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;

                case Ground.ROCK:
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;

                case Ground.WATER:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;

                case Ground.PLANT:
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;

                case Ground.MUD:
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    break;

                case Ground.LAVA:
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;

                default:
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
            }
            Console.Write($" {robot.RobotStatus.RobotID} ");
            Console.ResetColor();
        }
    }
}