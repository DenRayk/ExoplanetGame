using System;
using System.Collections.Generic;
using ExoplanetGame.Exoplanet;

namespace ExoplanetGame.ControlCenter
{
    internal class GroundPrinter
    {
        private static readonly Dictionary<Ground, ConsoleColor> groundColors = new Dictionary<Ground, ConsoleColor>
        {
            { Ground.NICHTS, ConsoleColor.White },
            { Ground.SAND, ConsoleColor.Yellow },
            { Ground.GEROELL, ConsoleColor.Gray },
            { Ground.FELS, ConsoleColor.DarkGray },
            { Ground.WASSER, ConsoleColor.Blue },
            { Ground.PFLANZEN, ConsoleColor.Green },
            { Ground.MORAST, ConsoleColor.DarkGreen },
            { Ground.LAVA, ConsoleColor.Red }
        };

        public static void PrintGround(Ground ground)
        {
            SetGroundColor(ground);
            Console.Write("   ");
            Console.ResetColor();
        }

        public static void SetGroundColor(Ground ground)
        {
            if (groundColors.TryGetValue(ground, out ConsoleColor color))
            {
                Console.BackgroundColor = color;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.White; // Default color
            }
        }

        public static void ResetColor()
        {
            Console.ResetColor();
        }
    }
}