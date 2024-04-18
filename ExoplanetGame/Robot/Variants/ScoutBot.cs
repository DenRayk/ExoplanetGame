using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Robot.Variants
{
    public class ScoutBot : RobotBase
    {
        public ScoutBot(ControlCenter.ControlCenter controlCenter, ExoplanetBase exoPlanet, int robotId) : base(exoPlanet, controlCenter, robotId, RobotVariant.SCOUT)
        {
            RobotInformation.RobotParts[RobotPart.SCANSENSOR] = 200;
            RobotInformation.MaxEnergy = 200;
        }

        public ScoutScanResult ScoutScan()
        {
            ScoutScanResult scoutScanResult = exoPlanet.ScoutScan(this);

            foreach (KeyValuePair<Measure, Position> measure in scoutScanResult.Measures)
            {
                Console.WriteLine($"Scanned {measure.Key} at {measure.Value}");
            }

            return scoutScanResult;
        }
    }
}