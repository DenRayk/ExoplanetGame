using ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.ControlCenter
{
    public class ControlCenter
    {
        private static ControlCenter controlCenter;

        private PlanetMap planetMap;
        private Dictionary<RobotBase, Position> robots;
        public IExoplanet exoPlanet;

        public event EventHandler<RobotPositionEventArgs> RobotPositionUpdated;

        public ControlCenter(IExoplanet exoPlanet)
        {
            robots = new Dictionary<RobotBase, Position>();
            this.exoPlanet = exoPlanet;
        }

        public static ControlCenter GetInstance(IExoplanet exoPlanet)
        {
            if (controlCenter == null)
            {
                controlCenter = new ControlCenter(exoPlanet);
            }
            return controlCenter;
        }

        public void Init(IExoplanet exoPlanet)
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
            planetMap.printMap(robots);
        }

        public void ClearRobots()
        {
            robots.Clear();
        }
    }
}