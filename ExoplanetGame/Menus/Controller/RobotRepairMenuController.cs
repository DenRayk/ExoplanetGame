using ExoplanetGame.Robot;

namespace ExoplanetGame.Menus.Controller
{
    public class RobotRepairMenuController
    {
        public static void RunRobotRepairMenu(ControlCenter.ControlCenter controlCenter)
        {
            RobotRepairMenu.ShowRobotRepairMenuOptions(controlCenter);
            int robotChoice = RobotRepairMenu.SelectRobotForRepair(1, controlCenter.GetRobotCount());
            RobotBase selectedRobot = controlCenter.GetRobotByID(robotChoice - 1);

            bool anyRobotParts = RobotRepairMenu.ShowRobotPartsForRepairIfTheyExist(selectedRobot, controlCenter);
            if (!anyRobotParts)
                return;

            int partChoice = RobotRepairMenu.GetRobotPartsSelection(1, controlCenter.GetRobotPartsByRobot(selectedRobot).Count);
            RobotRepairMenu.RepairRobotPart(controlCenter, selectedRobot, partChoice - 1);
        }
    }
}