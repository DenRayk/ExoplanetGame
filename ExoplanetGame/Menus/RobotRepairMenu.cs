using ExoplanetGame.ControlCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Robot;

namespace ExoplanetGame.Menus
{
    public class RobotRepairMenu
    {
        public static void DisplayRobotRepairMenuOptions(ControlCenter.ControlCenter controlCenter)
        {
            Console.WriteLine("Robot Repair Menu");
            Console.WriteLine("Select a robot to repair:");
            controlCenter.DisplayRobots();
        }

        public static void ShowAndSelectRobotPartsToRepair(RobotBase robotToRepair, ControlCenter.ControlCenter controlCenter)
        {
            Dictionary<RobotPart, int> robotPart = controlCenter.GetRobotPartsByRobot(robotToRepair);

            if (robotPart != null)
            {
                Console.WriteLine("Select a part to repair:");

                int counter = 1;
                foreach (KeyValuePair<RobotPart, int> part in robotPart)
                {
                    Console.WriteLine($"{counter}. {part.Key.GetDescriptionFromEnum()}: {part.Value}");
                    counter++;
                }

                KeyValuePair<RobotPart, int> partToRepair = robotPart.ElementAt(GetRobotRepairMenuSelection(1, robotPart.Count) - 1);

                controlCenter.RepairRobotPart(robotToRepair, partToRepair.Key);

                Console.WriteLine($"Repaired {partToRepair.Key.GetDescriptionFromEnum()} on {robotToRepair.GetLanderName()} \n");

                return;
            }

            Console.WriteLine($"{robotToRepair.GetLanderName()} has not used any parts yet. \n");
        }

        public static int GetRobotRepairMenuSelection(int minValue, int maxValue)
        {
            return MenuSelection.GetMenuSelection(minValue, maxValue, false);
        }
    }
}
