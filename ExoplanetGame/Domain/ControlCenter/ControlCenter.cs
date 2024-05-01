using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Domain.ControlCenter
{
    public class ControlCenter
    {
        private static ControlCenter controlCenter;
        private static int RobotId = 1;
        public ExoPlanetBase exoPlanet;

        public PlanetMap PlanetMap { get; set; }

        public int MaxRobots => 5;

        public int getRobotIDandIncrement()
        {
            return RobotId++;
        }

        public ControlCenter()
        {
        }

        public void AddMeasure(Measure measure, Position position)
        {
            PlanetMap.updateMap(position, measure.Ground);
        }

        public void AddMeasures(Dictionary<Measure, Position> measures)
        {
            foreach (var measure in measures)
            {
                AddMeasure(measure.Key, measure.Value);
            }
        }

        public static ControlCenter GetInstance()
        {
            if (controlCenter == null)
            {
                controlCenter = new ControlCenter();
            }
            return controlCenter;
        }
    }
}