using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet;

namespace ExoplanetGame.Robot.Variants
{
    public class ScoutBot : RobotBase
    {
        public ScoutBot(ControlCenter.ControlCenter controlCenter, IExoplanet exoPlanet, int robotId) : base(exoPlanet, controlCenter, robotId)
        {
            RobotVariant = RobotVariant.SCOUT;
        }

        public Dictionary<Measure, Position> ScoutScan()
        {
            Dictionary<Measure, Position> measures = exoPlanet.ScoutScan(this);

            foreach (var measure in measures)
            {
                Console.WriteLine($"Scanned {measure.Key} at {measure.Value}");
            }

            return measures;
        }
    }
}