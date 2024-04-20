using ExoplanetGame.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Menus.Controller
{
    public class RobotRepairMenuController
    {
        public static void RunRobotRepairMenu(ControlCenter.ControlCenter controlCenter)
        {
            RobotRepairMenu.DisplayRobotRepairMenuOptions(controlCenter);
            int robotChoice = RobotRepairMenu.GetRobotRepairMenuSelection(1, controlCenter.GetRobotCount());
            RobotBase selectedRobot = controlCenter.GetRobotByID(robotChoice - 1);

            RobotRepairMenu.ShowAndSelectRobotPartsToRepair(selectedRobot, controlCenter);
        }
    }
}
