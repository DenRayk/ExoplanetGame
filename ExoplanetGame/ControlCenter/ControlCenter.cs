using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Factory;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Factory;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.ControlCenter
{
    public class ControlCenter
    {
        private static ControlCenter controlCenter;
        public IExoPlanet exoPlanet;

        public PlanetMap PlanetMap { get; set; }
        private Dictionary<RobotBase, Position> robots;
        private IRobotFactory robotFactory;

        private readonly int maxRobots = 5;
        private static int robotID = 1;

        public event EventHandler<RobotPositionEventArgs> RobotPositionUpdated;

        public ControlCenter()
        {
            robots = new Dictionary<RobotBase, Position>();
            robotFactory = RobotFactory.GetInstance();
        }

        public static ControlCenter GetInstance()
        {
            if (controlCenter == null)
            {
                controlCenter = new ControlCenter();
            }
            return controlCenter;
        }

        public void AddRobot(RobotVariant robotVariant)
        {
            if (controlCenter.GetRobotCount() < maxRobots)
            {
                RobotBase robotBase;

                robotBase = robotFactory.CreateRobot(controlCenter, exoPlanet, robotID, robotVariant);

                Console.WriteLine($"{robotBase.GetLanderName()} added successfully. \n");
                robots.Add(robotBase, null);
            }
            else
            {
                Console.WriteLine("The maximum number of available robots has been reached. \n");
            }
        }

        public void UpdateRobotPosition(RobotBase robot, Position position)
        {
            robots[robot] = position;

            OnRobotPositionUpdated(robot, position);
        }

        protected virtual void OnRobotPositionUpdated(RobotBase robot, Position position)
        {
            RobotPositionUpdated?.Invoke(this, new RobotPositionEventArgs(robot, position));
        }

        public int GetRobotCount()
        {
            return robots.Count;
        }

        public void DisplayRobots()
        {
            int i = 1;
            foreach (var robot in robots.Keys)
            {
                Console.WriteLine(i + ". " + robot.GetLanderName());
                i++;
            }
        }

        public RobotBase GetRobotByID(int robotId)
        {
            return robots.Keys.ElementAt(robotId);
        }

        public void RemoveRobot(RobotBase Robot)
        {
            robots.Remove(Robot);
            exoPlanet.RemoveRobot(Robot);
        }

        public void PrintMap()
        {
            Console.WriteLine(planetMap.GetPercentageOfExploredArea());
            planetMap.printMap(robots);
        }

        public void ClearRobots()
        {
            robots.Clear();
        }

        public void RepairRobotPart(RobotBase robot, RobotPart robotPart)
        {
            exoPlanet.RepairRobotPart(robot, robotPart);
        }

        public Dictionary<RobotPart, int> GetRobotPartsByRobot(RobotBase robotBase)
        {
            return exoPlanet.GetRobotPartsByRobot(robotBase);
        }
    }
}