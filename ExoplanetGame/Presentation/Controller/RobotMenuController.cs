﻿using ExoplanetGame.Robot;

namespace ExoplanetGame.Menus.Controller
{
    public class RobotMenuController
    {
        public static void RunRobotMenu(RobotBase robot, ControlCenter.ControlCenter controlCenter)
        {
            bool keepMenuRunning = true;
            bool hasRobotLanded = robot.HasLanded();

            while (keepMenuRunning)
            {
                Console.WriteLine(Console.CursorTop == 0
                    ? $"{robot.GetLanderName()} Menu"
                    : $"\n{robot.GetLanderName()} Menu");

                RobotMenu.LoadCurrentExploredMap(controlCenter);
                RobotMenu.DisplayRobotMenuOptions(hasRobotLanded);

                int maxChoices = hasRobotLanded ? 7 : 2;

                int choice = RobotMenu.GetRobotMenuSelection(1, maxChoices);

                switch (choice)
                {
                    case 1:
                        if (hasRobotLanded)
                        {
                            keepMenuRunning = RobotMenu.ShowCurrentPosition(robot);
                        }
                        else
                        {
                            RobotMenu.HandleLandOption(robot, ref hasRobotLanded);
                        }
                        break;

                    case 2:
                        if (hasRobotLanded)
                        {
                            keepMenuRunning = RobotMenu.ScanEnvironment(robot, controlCenter);
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
                            keepMenuRunning = RobotMenu.RotateRobot(robot);
                        }
                        break;

                    case 5:
                        if (hasRobotLanded)
                        {
                            keepMenuRunning = RobotMenu.LoadRobot(robot);
                        }
                        break;

                    case 6:
                        if (hasRobotLanded)
                        {
                            keepMenuRunning = RobotMenu.CrashRobot(robot, controlCenter);
                        }
                        else
                        {
                            keepMenuRunning = false;
                        }
                        break;

                    case 7:
                        if (hasRobotLanded)
                        {
                            keepMenuRunning = false;
                        }
                        break;

                    case 112:
                        ShowRobotMenuInformation(robot, controlCenter);
                        break;
                }
            }
        }

        public static void ShowRobotMenuInformation(RobotBase robot, ControlCenter.ControlCenter controlCenter)
        {
            bool hasRobotLanded = robot.HasLanded();
            int mainMenuChoice;

            do
            {
                RobotMenu.DisplayRobotMenuInformation(hasRobotLanded);
                mainMenuChoice = MainMenu.GetMainMenuSelection(0, 0);
            } while (mainMenuChoice != 27);

            RunRobotMenu(robot, controlCenter);
        }

        public static void RunRobotSelectionMenu(GameServer gameServer, ControlCenter.ControlCenter controlCenter)
        {
            Console.WriteLine("Select a robot to control: \n");
            controlCenter.DisplayRobots();

            int choice = MenuSelection.GetMenuSelection(1, controlCenter.GetRobotCount(), false);

            gameServer.ControlRobot(choice - 1);
        }
    }
}