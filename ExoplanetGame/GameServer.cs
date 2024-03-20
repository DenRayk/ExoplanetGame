using ExoplanetGame.Menus;
using ExoplanetGame.RemoteRobot;

namespace ExoplanetGame
{
    internal class GameServer
    {
        private readonly int maxRobots = 5;
        private int robotCount;
        private Exoplanet.Exoplanet exoPlanet;
        private ControlCenter.ControlCenter controlCenter;
        private IRobotFactory robotFactory;

        public GameServer()
        {
            exoPlanet = new();
            controlCenter = new ControlCenter.ControlCenter(exoPlanet);
            controlCenter.Init(exoPlanet.PlanetSize);
            robotFactory = new RobotFactory();
        }

        public void Start()
        {
            MainMenu.Show(this, controlCenter);
        }

        public void AddRobot()
        {
            if (robotCount < maxRobots)
            {
                int robotID = controlCenter.GetRobotCount();

                RobotBase robotBase = robotFactory.CreateRemoteRobot(controlCenter, exoPlanet, robotID);

                controlCenter.AddRobot(robotBase);
                robotCount++;
                Console.WriteLine("Robot added successfully.");
            }
            else
            {
                Console.WriteLine("Maximum number of robots reached.");
            }
        }

        public void ControlRobot(int robotID)
        {
            RobotMenu.Show(controlCenter.GetRobotByID(robotID), controlCenter);
        }
    }
}