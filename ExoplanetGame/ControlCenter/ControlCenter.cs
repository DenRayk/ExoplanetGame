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
        private static int RobotId = 1;
        public IExoPlanet exoPlanet;

        public PlanetMap PlanetMap { get; set; }
        public Dictionary<RobotBase, Position> Robots { get; set; }
        private IRobotFactory robotFactory;

        public int MaxRobots { get; } = 5;

        public int getRobotIDandIncrement()
        {
            return RobotId++;
        }

        public event EventHandler<RobotPositionEventArgs> RobotPositionUpdated;

        public ControlCenter()
        {
            Robots = new Dictionary<RobotBase, Position>();
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

        public void UpdateRobotPosition(RobotBase robot, Position position)
        {
            Robots[robot] = position;

            OnRobotPositionUpdated(robot, position);
        }

        protected virtual void OnRobotPositionUpdated(RobotBase robot, Position position)
        {
            RobotPositionUpdated?.Invoke(this, new RobotPositionEventArgs(robot, position));
        }

        public int GetRobotCount()
        {
            return Robots.Count;
        }

        public void DisplayRobots()
        {
            int i = 1;
            foreach (var robot in Robots.Keys)
            {
                Console.WriteLine(i + ". " + robot.GetLanderName());
                i++;
            }
        }

        public RobotBase GetRobotByID(int robotId)
        {
            return Robots.Keys.ElementAt(robotId);
        }

        public void RemoveRobot(RobotBase Robot)
        {
            Robots.Remove(Robot);
            exoPlanet.RemoveRobot(Robot);
        }

        public void PrintMap()
        {
            Console.WriteLine(PlanetMap.GetPercentageOfExploredArea());
            PlanetMap.printMap(Robots);
        }

        public void ClearRobots()
        {
            Robots.Clear();
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