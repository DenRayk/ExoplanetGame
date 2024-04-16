using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Menus.Controller
{
    public class MainMenuController
    {
        public static void RunMainMenu(GameServer gameServer, ControlCenter.ControlCenter controlCenter)
        {
            while (true)
            {
                MainMenu.DisplayMainMenuOptions();
                var mainMenuChoice = MainMenu.GetMainMenuSelection(1, 4);

                switch (mainMenuChoice)
                {
                    case 1:
                        var robotVariantChoice = RobotVariantMenuController.SelectRobotVariant();
                        gameServer.AddRobot(robotVariantChoice);
                        break;

                    case 2:
                        if (controlCenter.GetRobotCount() == 0)
                        {
                            Console.WriteLine("No robots to control.");
                            break;
                        }
                        RobotMenuController.RunRobotSelectionMenu(gameServer, controlCenter);
                        break;

                    case 3:
                        RobotMenu.LoadCurrentExploredMap(controlCenter);
                        break;

                    case 4:
                        Environment.Exit(0);
                        break;

                    case 112:
                        ShowMainMenuInformation(gameServer, controlCenter);
                        break;
                }
            }
        }

        public static void ShowMainMenuInformation(GameServer gameServer, ControlCenter.ControlCenter controlCenter)
        {
            int mainMenuChoice;

            do
            {
                MainMenu.DisplayMainMenuInformation();
                mainMenuChoice = MainMenu.GetMainMenuSelection(0, 0);
            } while (mainMenuChoice != 27);

            RunMainMenu(gameServer, controlCenter);
        }
    }
}
