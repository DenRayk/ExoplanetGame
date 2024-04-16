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
            Console.WriteLine("Exoplanet Menu");
            Console.WriteLine("Choose a destination planet");
            Console.WriteLine("1. Gaia");
            Console.WriteLine("2. Aquatica");
            Console.WriteLine("3. Terra");
            Console.WriteLine("4. Lavaria");
            Console.WriteLine("5. Tropica");
            Console.WriteLine("F1. Info");
        }

        public static void DisplayExoplanetMenuInformation()
        {
            Console.WriteLine("Exoplanet Menu Information");
            Console.WriteLine($"{PlanetVariant.GAIA.GetDescriptionFromEnum()}: Beginner level");
            Console.WriteLine($"{PlanetVariant.AQUATICA.GetDescriptionFromEnum()}: Casual level");
            Console.WriteLine($"{PlanetVariant.TERRA.GetDescriptionFromEnum()}: Intermediate level");
            Console.WriteLine($"{PlanetVariant.LAVARIA.GetDescriptionFromEnum()}: Advanced level");
            Console.WriteLine($"{PlanetVariant.TROPICA.GetDescriptionFromEnum()}: Expert level");
            Console.WriteLine("Press ESC to go back");
        }

        public static int GetExoplanetMenuSelection(int minValue, int maxValue)
        {
            return MenuSelection.GetMenuSelection(minValue, maxValue);
        }
    }
}
