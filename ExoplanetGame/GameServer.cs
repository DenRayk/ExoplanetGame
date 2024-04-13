using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Menus;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame
{
    public class GameServer
    {
        private static GameServer gameServer;
        private readonly int maxRobots = 5;
        private static int robotID = 1;
        private Gaia exoPlanet;
        private ControlCenter.ControlCenter controlCenter;
        private IRobotFactory robotFactory;
        public int RobotCount { get; set; }

        private GameServer()
        {
            exoPlanet = new();
            controlCenter = ControlCenter.ControlCenter.GetInstance(exoPlanet);
            controlCenter.Init(exoPlanet.Topography.PlanetSize);
            robotFactory = RobotFactory.GetInstance();
        }

        public static GameServer GetInstance()
        {
            if (gameServer == null)
            {
                gameServer = new GameServer();
            }
            return gameServer;
        }

        public void Start()
        {
            MenuController.ShowMainMenu(this, controlCenter);
        }

        public void AddRobot(RobotVariant robotVariant)
        {
            if (RobotCount < maxRobots)
            {
                RobotBase robotBase;

                switch (robotVariant)
                {
                    case RobotVariant.DEFAULT:
                        robotBase = robotFactory.CreateDefaultRobot(controlCenter, exoPlanet, robotID++);
                        break;
                    case RobotVariant.SCOUT:
                        robotBase = robotFactory.CreateScoutRobot(controlCenter, exoPlanet, robotID++);
                        break;
                    case RobotVariant.SOLAR:
                        robotBase = robotFactory.CreateSolarRobot(controlCenter, exoPlanet, robotID++);
                        break;
                    case RobotVariant.LAVA:
                        robotBase = robotFactory.CreateLavaRobot(controlCenter, exoPlanet, robotID++);
                        break;
                    case RobotVariant.AQUA:
                        robotBase = robotFactory.CreateAquaRobot(controlCenter, exoPlanet, robotID++);
                        break;
                    case RobotVariant.MUD:
                        robotBase = robotFactory.CreateMudRobot(controlCenter, exoPlanet, robotID++);
                        break;
                    default:
                        Console.WriteLine("Invalid robot variant.");
                        return;
                }

                controlCenter.AddRobot(robotBase);
                RobotCount++;
                Console.WriteLine($"{robotBase.GetLanderName()} added successfully.");
            }
            else
            {
                Console.WriteLine("Maximum number of robots reached.");
            }
        }

        public void ControlRobot(int robotID)
        {
            MenuController.ShowRobotMenu(controlCenter.GetRobotByID(robotID), controlCenter);
        }

        public void ClearRobots()
        {
            controlCenter.ClearRobots();
            RobotCount = 0;
        }
    }
}