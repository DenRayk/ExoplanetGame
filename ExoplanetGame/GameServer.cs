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
        private readonly int maxRobots = 5;
        private static int robotID = 1;
        private ControlCenter.ControlCenter controlCenter;

        private IRobotFactory robotFactory;

        public GameServer(ExoplanetBase targetExoplanetBase)
        {
            controlCenter = ControlCenter.ControlCenter.GetInstance(targetExoplanetBase);
            controlCenter.Init(targetExoplanetBase);

            robotFactory = RobotFactory.GetInstance();
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
            if (controlCenter.GetRobotCount() < maxRobots)
            {
                RobotBase robotBase;

                switch (robotVariant)
                {
                    case RobotVariant.DEFAULT:
                        robotBase = robotFactory.CreateDefaultRobot(controlCenter, PlanetManager.TargetPlanet, robotID++);
                        break;

                    case RobotVariant.SCOUT:
                        robotBase = robotFactory.CreateScoutRobot(controlCenter, PlanetManager.TargetPlanet, robotID++);
                        break;

                    case RobotVariant.LAVA:
                        robotBase = robotFactory.CreateLavaRobot(controlCenter, PlanetManager.TargetPlanet, robotID++);
                        break;

                    case RobotVariant.AQUA:
                        robotBase = robotFactory.CreateAquaRobot(controlCenter, PlanetManager.TargetPlanet, robotID++);
                        break;

                    case RobotVariant.MUD:
                        robotBase = robotFactory.CreateMudRobot(controlCenter, PlanetManager.TargetPlanet, robotID++);
                        break;

                    default:
                        Console.WriteLine("Invalid robot variant. \n");
                        return;
                }

                controlCenter.AddRobot(robotBase);
                Console.WriteLine($"{robotBase.GetLanderName()} added successfully. \n");
            }
            else
            {
                Console.WriteLine("The maximum number of available robots has been reached. \n");
            }
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