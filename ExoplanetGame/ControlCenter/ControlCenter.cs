using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Factory;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.ControlCenter
{
    public class ControlCenter
    {
        private static ControlCenter controlCenter;
        public IExoplanet exoPlanet;

        private PlanetMap planetMap;
        private Dictionary<RobotBase, Position> robots;
        private IRobotFactory robotFactory;

        private readonly int maxRobots = 5;
        private static int robotID = 1;

        public event EventHandler<RobotPositionEventArgs> RobotPositionUpdated;

        public ControlCenter(IExoplanet exoPlanet)
        {
            robots = new Dictionary<RobotBase, Position>();
            this.exoPlanet = exoPlanet;
            robotFactory = RobotFactory.GetInstance();
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

        public void AddRobot(RobotVariant robotVariant)
        {
            if (controlCenter.GetRobotCount() < maxRobots)
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
                        robotBase = robotFactory.CreateDefaultRobot(controlCenter, exoPlanet, robotID++);
                        return;
                }

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

        public void AddMeasure(Measure measure, Position position)
        {
            planetMap.updateMap(position, measure.Ground);
        }

        public void AddMeasures(Dictionary<Measure, Position> measures)
        {
            foreach (var measure in measures)
            {
                AddMeasure(measure.Key, measure.Value);
            }
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