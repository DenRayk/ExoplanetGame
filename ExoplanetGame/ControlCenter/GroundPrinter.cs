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
                case Ground.NICHTS:
                    Console.BackgroundColor = ConsoleColor.White;
                    break;

                case Ground.SAND:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;

                case Ground.GEROELL:
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;

                case Ground.FELS:
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;

                case Ground.WASSER:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;

                case Ground.PFLANZEN:
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;

                case Ground.MORAST:
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
                case Ground.NICHTS:
                    Console.BackgroundColor = ConsoleColor.White;
                    break;

                case Ground.SAND:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;

                case Ground.GEROELL:
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;

                case Ground.FELS:
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;

                case Ground.WASSER:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;

                case Ground.PFLANZEN:
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;

                case Ground.MORAST:
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