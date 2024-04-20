using ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.ControlCenter
{
    public class ControlCenter
    {
        private static ControlCenter controlCenter;

        private PlanetMap planetMap;
        private Dictionary<RobotBase, Position> robots;
        public ExoplanetBase exoPlanet;

        public event EventHandler<RobotPositionEventArgs> RobotPositionUpdated;

        public ControlCenter(ExoplanetBase exoPlanet)
        {
            robots = new Dictionary<RobotBase, Position>();
            this.exoPlanet = exoPlanet;
        }

        public static ControlCenter GetInstance(ExoplanetBase exoPlanet)
        {
            if (controlCenter == null)
            {
                controlCenter = new ControlCenter(exoPlanet);
            }
            return controlCenter;
        }

        public void Init(ExoplanetBase exoPlanet)
        {
            planetMap = new PlanetMap(exoPlanet.Topography.PlanetSize);
        }

        public void AddRobot(RobotBase robotBase)
        {
            robots.Add(robotBase, null);
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

        public void AddMeasure(Measure measure, Position position)
        {
            planetMap.updateMap(position, measure.Ground);
        }

        public void AddMeasures(Dictionary<Measure, Position> measures)
        {
            foreach (var measure in measures)
            {
                planetMap.updateMap(measure.Value, measure.Key.Ground);
            }
        }

        public int GetRobotCount()
        {
            return robots.Count;
        }

        public List<RobotVariant> GetRobotVariants()
        {
            List<RobotVariant> robotVariants = new List<RobotVariant>();
            foreach (var robot in robots.Keys)
            {
                robotVariants.Add(robot.RobotVariant);
            }
            return robotVariants;
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