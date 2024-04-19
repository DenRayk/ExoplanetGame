using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Exoplanet;

namespace ExoplanetGame.Menus.Controller
{
    public class ExoplanetMenuController
    {
        public static ExoplanetBase SelectTargetExoplanet()
        {
            while (true)
            {
                ExoplanetMenu.DisplayExoplanetMenuOptions();
                int exoplanetChoice = ExoplanetMenu.GetExoplanetMenuSelection(1, 7);

                switch (exoplanetChoice)
                {
                    case 1:
                        return PlanetManager.GetPlanet(PlanetVariant.GAIA);

                    case 2:
                        return PlanetManager.GetPlanet(PlanetVariant.AQUATICA);

                    case 3:
                        return PlanetManager.GetPlanet(PlanetVariant.TERRA);

                    case 4:
                        return PlanetManager.GetPlanet(PlanetVariant.FROSTFELL);

                    case 5:
                        return PlanetManager.GetPlanet(PlanetVariant.LAVARIA);

                    case 6:
                        return PlanetManager.GetPlanet(PlanetVariant.TROPICA);

                    case 7:
                        return PlanetManager.GetRandomPlanet();

                    case 112:
                        ShowExoplanetMenuInformation();
                        break;
                }
            }
        }

        public static void ShowExoplanetMenuInformation()
        {
            int mainMenuChoice;

            do
            {
                ExoplanetMenu.DisplayExoplanetMenuInformation();
                mainMenuChoice = MainMenu.GetMainMenuSelection(0, 0);
            } while (mainMenuChoice != 27);
        }
    }
}