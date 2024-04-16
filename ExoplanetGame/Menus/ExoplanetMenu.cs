using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Exoplanet;
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
            Console.WriteLine("Exoplanet Menu:");
            Console.WriteLine("Choose a destination planet (press F1 for help):\n");
            Console.WriteLine("1. Gaia");
            Console.WriteLine("2. Aquatica");
            Console.WriteLine("3. Terra");
            Console.WriteLine("4. Lavaria");
            Console.WriteLine("5. Tropica");
        }

        public static void DisplayExoplanetMenuInformation()
        {
            Console.WriteLine("Exoplanet Menu Information\n");
            Console.WriteLine($"{PlanetVariant.GAIA.GetDescriptionFromEnum()}:\t Beginner level");
            Console.WriteLine($"{PlanetVariant.AQUATICA.GetDescriptionFromEnum()}:\t Casual level");
            Console.WriteLine($"{PlanetVariant.TERRA.GetDescriptionFromEnum()}:\t Intermediate level");
            Console.WriteLine($"{PlanetVariant.LAVARIA.GetDescriptionFromEnum()}:\t Advanced level");
            Console.WriteLine($"{PlanetVariant.TROPICA.GetDescriptionFromEnum()}:\t Expert level\n");
            Console.WriteLine("Press ESC to go back");
        }

        public static int GetExoplanetMenuSelection(int minValue, int maxValue)
        {
            return MenuSelection.GetMenuSelection(minValue, maxValue);
        }
    }
}