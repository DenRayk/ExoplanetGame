using ExoplanetGame.Robot.Variants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Menus.Controller
{
    public class RobotVariantMenuController
    {
        public static RobotVariant SelectRobotVariant()
        {
            while (true)
            {
                RobotVariantMenu.DisplayRobotVariantOptions();

                int choice = RobotVariantMenu.GetRobotVariantSelection(1, 5);

                switch (choice)
                {
                    case 1:
                        return RobotVariant.DEFAULT;

                    case 2:
                        return RobotVariant.SCOUT;

                    case 3:
                        return RobotVariant.LAVA;

                    case 4:
                        return RobotVariant.AQUA;

                    case 5:
                        return RobotVariant.MUD;

                    case 112:
                        ShowRobotVariantMenuInformation();
                        break;
                }
            }
        }

        public static void ShowRobotVariantMenuInformation()
        {
            int mainMenuChoice;

            do
            {
                RobotVariantMenu.DisplayRobotVariantInformation();
                mainMenuChoice = MainMenu.GetMainMenuSelection(0, 0);
            } while (mainMenuChoice != 27);
        }
    }
}