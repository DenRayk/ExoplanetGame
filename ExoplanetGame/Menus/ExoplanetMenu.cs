using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Menus
{
    public class ExoplanetMenu
    {
        public static void DisplayExoplanetMenuOptions()
        {
            Console.WriteLine("Exoplanet Menu");
            Console.WriteLine("Select the destination planet");
            Console.WriteLine("1. Exoplanet 1");
            Console.WriteLine("2. Exoplanet 2");
            Console.WriteLine("3. Exoplanet 3");
            Console.WriteLine("4. Exoplanet 4");
            Console.WriteLine("5. Exoplanet 5");
        }

        public static int GetExoplanetMenuSelection(int minValue, int maxValue)
        {
            int choice;

            while (!int.TryParse(Console.ReadLine(), out choice) || choice < minValue || choice > maxValue)
            {
                Console.WriteLine($"Invalid input. Please enter a number between {minValue} and {maxValue}.");
            }

            Console.Clear();

            return choice;
        }
    }
}
