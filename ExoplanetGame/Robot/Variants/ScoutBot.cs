using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Robot.Variants
{
    public class ScoutBot : RobotBase
    {
        public ScoutBot(ControlCenter.ControlCenter controlCenter, IExoPlanet exoPlanet, int robotId) : base(exoPlanet, controlCenter, robotId, RobotVariant.SCOUT)
        {
            RobotInformation.RobotParts[RobotPart.SCANSENSOR] = 200;
            RobotInformation.MaxEnergy = 200;
        }

        public ScoutScanResult ScoutScan()
        {
            ScoutScanResult scoutScanResult = exoPlanet.ScoutScan(this);

            if (scoutScanResult.IsSuccess)
            {
                foreach (KeyValuePair<Measure, Position> measure in scoutScanResult.Measures)
                {
                    Console.WriteLine($"Scanned {measure.Key} at {measure.Value}");
                }
            }
            else
            {
                Console.WriteLine($"{scoutScanResult.Message}");
                if (!scoutScanResult.HasRobotSurvived)
                {
                    Crash();
                }
            }

            return scoutScanResult;
        }
    }
}