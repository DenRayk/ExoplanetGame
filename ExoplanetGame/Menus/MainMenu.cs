using System;

namespace ExoplanetGame.Menus
{
    internal class MainMenu
    {
        public static void Show(GameServer gameServer, ControlCenter.ControlCenter controlCenter)
        {
            while (true)
            {
                Console.WriteLine("Main Menu");
                Console.WriteLine("1. Add Robot");
                Console.WriteLine("2. Select Robot");
                Console.WriteLine("3. Exit");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
                {
                    Console.WriteLine("Invalid input. Please enter a valid menu option.");
                }

                switch (choice)
                {
                    case 1:
                        gameServer.AddRobot();
                        break;

                    case 2:
                        SelectRobot(gameServer, controlCenter);
                        break;

                    case 3:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        public static void SelectRobot(GameServer gameServer, ControlCenter.ControlCenter controlCenter)
        {
            Console.WriteLine("Select a robot to control:");
            controlCenter.DisplayRobots();

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > controlCenter.GetRobotCount())
            {
                Console.WriteLine("Invalid input. Please enter a valid robot number.");
            }

            gameServer.ControlRobot(choice - 1);
        }
    }
}