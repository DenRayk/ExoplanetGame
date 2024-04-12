using ExoplanetGame.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Menus
{
    public class MenuController
    {
        public static void ShowMainMenu(GameServer gameServer, ControlCenter.ControlCenter controlCenter)
        {
            while (true)
            {
                MainMenu.DisplayMainManueOptions();
                var mainMenuChoice = MainMenu.GetMainManueSelection();

                switch (mainMenuChoice)
                {
                    case 1:
                        RobotVariantMenu.DisplayRobotVariantOptions();
                        var robotVariantChoice = RobotVariantMenu.GetRobotVariantSelection();
                        gameServer.AddRobot(robotVariantChoice);
                        break;
                    case 2:
                        if (controlCenter.GetRobotCount() == 0)
                        {
                            Console.WriteLine("No robots to control.");
                            break;
                        }
                        MainMenu.SelectRobot(gameServer, controlCenter);
                        break;

                    case 3:
                        controlCenter.PrintMap();
                        break;

                    case 4:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        public static void ShowRobotMenu(RobotBase robot, ControlCenter.ControlCenter controlCenter)
        {
            bool keepMenuRunning = true;
            bool hasRobotLanded = robot.HasLanded();

            while (keepMenuRunning)
            {
                Console.WriteLine($"{robot.GetLanderName()} Menu");
                RobotMenu.DisplayRobotMenuOptions(hasRobotLanded);

                int maxChoices = hasRobotLanded ? 8 : 2;

                int choice = RobotMenu.GetRobotMenuSelection(1, maxChoices);

                switch (choice)
                {
                    case 1:
                        if (hasRobotLanded)
                        {
                            RobotMenu.ShowCurrentPosition(robot);
                        }
                        else
                        {
                            RobotMenu.HandleLandOption(robot, ref hasRobotLanded);
                        }
                        break;

                    case 2:
                        if (hasRobotLanded)
                        {
                            RobotMenu.ScanEnvironment(robot, controlCenter);
                        }
                        else
                        {
                            keepMenuRunning = false;
                        }
                        break;

                    case 3:
                        if (hasRobotLanded)
                        {
                            keepMenuRunning = RobotMenu.MoveRobot(robot);
                        }
                        break;

                    case 4:
                        if (hasRobotLanded)
                        {
                            RobotMenu.RotateRobot(robot);
                        }
                        break;

                    case 5:
                        if (hasRobotLanded)
                        {
                            RobotMenu.CrashRobot(robot, controlCenter, ref keepMenuRunning);
                        }
                        else
                        {
                            keepMenuRunning = false;
                        }
                        break;

                    case 6:
                        if (hasRobotLanded)
                        {
                            keepMenuRunning = false;
                        }
                        break;
                }
            }

            Console.Clear();
        }

    }
}
