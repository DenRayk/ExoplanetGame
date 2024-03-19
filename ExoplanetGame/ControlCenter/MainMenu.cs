using System;

namespace ExoplanetGame.ControlCenter
{
    internal class MainMenu
    {
        public static void Show(ControlServer server)
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
                        server.AddRobot();
                        break;

                    case 2:
                        server.SelectRobot();
                        break;

                    case 3:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}