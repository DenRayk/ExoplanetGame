using ExoplanetGame.Menus;

namespace ExoplanetGame
{
    internal class GameServer
    {
        private readonly int maxRobots = 5;
        private int robotCount;
        private Exoplanet.Exoplanet exoPlanet;
        private ControlCenter.ControlCenter controlCenter;

        public GameServer()
        {
            exoPlanet = new();
            controlCenter = new ControlCenter.ControlCenter(exoPlanet);
            controlCenter.Init(exoPlanet.PlanetSize);
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
                controlCenter.AddRobot(new RemoteRobot.RemoteRobot(controlCenter, exoPlanet, robotID));
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