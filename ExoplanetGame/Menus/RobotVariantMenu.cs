using ExoplanetGame.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Menus
{
    public class RobotVariantMenu
    {
        public static void DisplayRobotVariantOptions()
        {
            Console.WriteLine("Robot Variant Menu");
            Console.WriteLine("Select a robot variant:");
            Console.WriteLine("1. Default robot");
            Console.WriteLine("2. Scout robot");
            Console.WriteLine("3. Lava robot");
            Console.WriteLine("4. Aqua robot");
            Console.WriteLine("5. Mud robot");
            Console.WriteLine("6. Solar robot");
        }

        public static RobotVariant GetRobotVariantSelection()
        {
            int choice;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 6)
                {
                    break;
                }
                Console.WriteLine("Invalid input. Please enter a valid robot variant.");
            }

            Console.Clear();

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
                case 6:
                    return RobotVariant.SOLAR;
                default:
                    return RobotVariant.DEFAULT;
            }
        }
    }
}
