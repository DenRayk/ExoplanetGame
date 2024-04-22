using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Menus.Controller;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Factory;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame
{
    public class GameServer
    {
        private static GameServer gameServer;
        private ControlCenter.ControlCenter controlCenter;

        public GameServer(ExoplanetBase targetExoplanetBase)
        {
            controlCenter = ControlCenter.ControlCenter.GetInstance(targetExoplanetBase);
            controlCenter.Init(targetExoplanetBase);
        }

        public void Start()
        {
            MainMenuController.RunMainMenu(this, controlCenter);
        }

        public static GameServer GetInstance(ExoplanetBase targetExoplanetBase)
        {
            if (gameServer == null)
            {
                gameServer = new GameServer(targetExoplanetBase);
            }

            return gameServer;
        }

        public void AddRobot(RobotVariant robotVariant)
        {
            controlCenter.AddRobot(robotVariant);
        }

        public void ControlRobot(int robotID)
        {
            RobotMenuController.RunRobotMenu(controlCenter.GetRobotByID(robotID), controlCenter);
        }

        public void ClearRobots()
        {
            controlCenter.ClearRobots();
        }
    }
}