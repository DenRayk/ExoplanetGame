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
            Console.WriteLine("1. Aquatica");
            Console.WriteLine("2. Gaia");
            Console.WriteLine("3. Lavaria");
            Console.WriteLine("4. Terra");
            Console.WriteLine("5. Tropica");
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

        public static ExoplanetBase SelectTargetExoplanet()
        {
            DisplayExoplanetMenuOptions();
            int exoplanetChoice = GetExoplanetMenuSelection(1, 5);

            switch (exoplanetChoice)
            {
                case 1:
                    return PlanetManager.GetPlanet(PlanetVariant.AQUATICA);
                case 2:
                    return PlanetManager.GetPlanet(PlanetVariant.GAIA);
                case 3:
                    return PlanetManager.GetPlanet(PlanetVariant.LAVARIA);
                case 4:
                    return PlanetManager.GetPlanet(PlanetVariant.TERRA);
                case 5:
                    return PlanetManager.GetPlanet(PlanetVariant.TROPICA);
                default:
                    return null;
            }
        }
    }
}
