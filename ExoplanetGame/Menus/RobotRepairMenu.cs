using ExoplanetGame.Robot;

namespace ExoplanetGame.Menus
{
    public class RobotRepairMenu
    {
        public static void ShowRobotRepairMenuOptions(ControlCenter.ControlCenter controlCenter)
        {
            Console.WriteLine("Robot Repair Menu");
            Console.WriteLine("Select a robot to repair: \n");
            controlCenter.DisplayRobots();
        }

        public static bool ShowRobotPartsForRepairIfTheyExist(RobotBase robotToRepair, ControlCenter.ControlCenter controlCenter)
        {
            Dictionary<RobotPart, int> robotPart = controlCenter.GetRobotPartsByRobot(robotToRepair);

            if (robotPart != null)
            {
                Console.WriteLine($"{robotToRepair.GetLanderName()} has used the following parts: \n");

                int counter = 1;
                foreach (KeyValuePair<RobotPart, int> part in robotPart)
                {
                    Console.WriteLine($"{counter}. {part.Key.GetDescriptionFromEnum(),-15} - {GetDescriptionFromPartDurability(part.Value)}");
                    counter++;
                }
                return true;
            }

            Console.WriteLine($"{robotToRepair.GetLanderName()} has not used any parts yet. \n");
            return false;
        }

        public static int SelectRobotForRepair(int minValue, int maxValue)
        {
            return MenuSelection.GetMenuSelection(minValue, maxValue, false);
        }

        public static int GetRobotPartsSelection(int minValue, int maxValue)
        {
            return MenuSelection.GetMenuSelection(minValue, maxValue, false);
        }

        public static void RepairRobotPart(ControlCenter.ControlCenter controlCenter, RobotBase robotToRepair, int partChoice)
        {
            Dictionary<RobotPart, int> robotPart = controlCenter.GetRobotPartsByRobot(robotToRepair);

            KeyValuePair<RobotPart, int> partToRepair = robotPart.ElementAt(partChoice);
            controlCenter.RepairRobotPart(robotToRepair, partToRepair.Key);
            Console.WriteLine($"Repaired: {partToRepair.Key.GetDescriptionFromEnum()} on {robotToRepair.GetLanderName()} \n");
        }

        private static string GetDescriptionFromPartDurability(int durability)
        {
            switch (durability)
            {
                case <= 0:
                    return "Broken";

                case <= 25:
                    return "Critical";

                case <= 50:
                    return "Damaged";

                case <= 75:
                    return "Worn";

                case <= 90:
                    return "Good";

                default:
                    return "Like new";
            }
        }
    }
}