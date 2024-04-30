using ExoplanetGame.Exoplanet.ExoplanetGame.Exoplanet;
using ExoplanetGame.Menus.Controller;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame
{
    public class GameServer
    {
        private static GameServer gameServer;
        private ControlCenter.ControlCenter controlCenter;

        public GameServer(IExoPlanet targetExoPlanetBase)
        {
            controlCenter = ControlCenter.ControlCenter.GetInstance();
            controlCenter.Init(targetExoPlanetBase);
        }

        public void Start()
        {
            ControlCenterController.RunMainMenu(this, controlCenter);
        }

        public static GameServer GetInstance(IExoPlanet targetExoPlanetBase)
        {
            if (gameServer == null)
            {
                gameServer = new GameServer(targetExoPlanetBase);
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
    }
}