using ExoplanetGame.Robot.Variants;
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
        }

        public static void DisplayRobotVariantInformation()
        {
            Console.WriteLine("Robot Variant Information");
            Console.WriteLine($"{RobotVariant.DEFAULT.GetDescriptionFromEnum()}: Basic robot with no special abilities");
            Console.WriteLine($"{RobotVariant.SCOUT.GetDescriptionFromEnum()}: Scan roboter with a larger scanning range");
            Console.WriteLine($"{RobotVariant.LAVA.GetDescriptionFromEnum()}: Robot that can withstand high temperatures");
            Console.WriteLine($"{RobotVariant.AQUA.GetDescriptionFromEnum()}: Robot that can withstand water drift");
            Console.WriteLine($"{RobotVariant.MUD.GetDescriptionFromEnum()}: Robot that can move through mud");
            Console.WriteLine($"Press ESC to go back");
        }

        public static int GetRobotVariantSelection(int minValue, int maxValue)
        {
            return MenuSelection.GetMenuSelection(minValue, maxValue);
        }
    }
}