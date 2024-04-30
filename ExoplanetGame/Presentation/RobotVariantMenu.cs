using ExoplanetGame.Helper;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Menus
{
    public class RobotVariantMenu
    {
        public static void DisplayRobotVariantOptions()
        {
            Console.WriteLine("Robot Variant Menu");
            Console.WriteLine("Select a robot variant (press F1 for help):\n");
            Console.WriteLine("1. Default robot");
            Console.WriteLine("2. Scout robot");
            Console.WriteLine("3. Lava robot");
            Console.WriteLine("4. Aqua robot");
            Console.WriteLine("5. Mud robot");
            Console.WriteLine("6. Random robot");
        }

        public static void DisplayRobotVariantInformation()
        {
            Console.WriteLine("Robot Variant Information\n");
            Console.WriteLine($"{RobotVariant.DEFAULT.GetDescriptionFromEnum()}:\t Basic robot with no special abilities");
            Console.WriteLine($"{RobotVariant.SCOUT.GetDescriptionFromEnum()}:\t Scan roboter with a larger scanning range");
            Console.WriteLine($"{RobotVariant.LAVA.GetDescriptionFromEnum()}:\t Robot that can withstand high temperatures");
            Console.WriteLine($"{RobotVariant.AQUA.GetDescriptionFromEnum()}:\t Robot that can withstand water drift");
            Console.WriteLine($"{RobotVariant.MUD.GetDescriptionFromEnum()}:\t Robot that can move through mud\n");
            Console.WriteLine($"Press ESC to go back");
        }

        public static int GetRobotVariantSelection(int minValue, int maxValue)
        {
            return MenuSelection.GetMenuSelection(minValue, maxValue, true);
        }
    }
}