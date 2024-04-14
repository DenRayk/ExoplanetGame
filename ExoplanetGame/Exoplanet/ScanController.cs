using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet;

public class ScanController
{
    private RobotManager robotManager;
    private RobotPartsTracker robotPartsTracker;
    private RobotHeatTracker robotHeatTracker;

    public ScanController(RobotManager robotManager, RobotPartsTracker robotPartsTracker, RobotHeatTracker robotHeatTracker)
    {
        this.robotManager = robotManager;
        this.robotPartsTracker = robotPartsTracker;
        this.robotHeatTracker = robotHeatTracker;
    }

    public Measure Scan(RobotBase robot, Topography topography)
    {
        if (!robotPartsTracker.isRobotPartDamaged(robot, RobotParts.SCANSENSOR))
        {
            robotHeatTracker.PerformAction(robot);
            robotPartsTracker.RobotPartDamage(robot, RobotParts.SCANSENSOR);
            return topography.GetMeasureAtPosition(robotManager.robots[robot]);
        }
        else
        {
            Console.WriteLine("The robot's scan sensor is damaged and can't scan.");
            return null;
        }
    }

    public Dictionary<Measure, Position> ScoutScan(RobotBase robot, Topography topography)
    {
        if (!robotPartsTracker.isRobotPartDamaged(robot, RobotParts.SCANSENSOR))
        {
            Dictionary<Measure, Position> scoutScanResults = new Dictionary<Measure, Position>();
            Position currentRobotPosition = robotManager.GetRobotPosition(robot);

            Measure currentMeasure = topography.GetMeasureAtPosition(currentRobotPosition);
            scoutScanResults.Add(currentMeasure, currentRobotPosition);

            if (robot.RobotVariant == RobotVariant.SCOUT)
            {
                Position firstForwardPosition = currentRobotPosition.GetAdjacentPosition();
                Position secondForwardPosition = firstForwardPosition.GetAdjacentPosition();

                bool isFirstPositionValid = RobotManager.IsPositionInBounds(firstForwardPosition, topography);
                bool isSecondPositionValid = RobotManager.IsPositionInBounds(secondForwardPosition, topography);

                if (isFirstPositionValid)
                {
                    Measure firstPositionMeasure = topography.GetMeasureAtPosition(firstForwardPosition);
                    scoutScanResults.Add(firstPositionMeasure, firstForwardPosition);
                }
                if (isSecondPositionValid)
                {
                    Measure secondPositionMeasure = topography.GetMeasureAtPosition(secondForwardPosition);
                    scoutScanResults.Add(secondPositionMeasure, secondForwardPosition);
                }
            }

            return scoutScanResults;
        }
        else
        {
            Console.WriteLine("The robot's scan sensor is damaged and can't scan.");
            return null;
        }
    }
}