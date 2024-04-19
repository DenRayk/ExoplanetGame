using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet.Controller;

public class ScanController(RobotManager robotManager, RobotStatusManager robotStatusManager)
{
    public ScanResult Scan(RobotBase robot, Topography topography)
    {
        ScanResult scanResult = new ScanResult();

        if (!robotStatusManager.RobotPartsTracker.isRobotPartDamaged(robot, RobotPart.SCANSENSOR))
        {
            robotStatusManager.RobotHeatTracker.PerformAction(robot, RobotAction.SCAN, topography);
            robotStatusManager.RobotPartsTracker.RobotPartDamage(robot, RobotPart.SCANSENSOR);
            robotStatusManager.RobotEnergyTracker.ConsumeEnergy(robot, RobotAction.SCAN);

            scanResult.Measure = topography.GetMeasureAtPosition(robotManager.robots[robot]);
            scanResult.IsSuccess = true;
            scanResult.HasRobotSurvived = true;
            return scanResult;
        }

        scanResult.Message = "The robot's scan sensor is damaged and can't scan. Please repair in Control Center.";
        scanResult.IsSuccess = false;
        scanResult.HasRobotSurvived = true;
        return scanResult;
    }

    public ScoutScanResult ScoutScan(RobotBase robot, Topography topography)
    {
        ScoutScanResult scoutScanResult = new ScoutScanResult();

        if (robotStatusManager.RobotPartsTracker.isRobotPartDamaged(robot, RobotPart.SCANSENSOR))
        {
            scoutScanResult.Message = "The robot's scan sensor is damaged and can't scan. Please repair in Control Center.";
            scoutScanResult.IsSuccess = false;
            scoutScanResult.HasRobotSurvived = true;
            return scoutScanResult;
        }

        PositionResult positionResult = robotManager.GetRobotPosition(robot);
        Position currentRobotPosition = positionResult.Position;

        Measure currentMeasure = topography.GetMeasureAtPosition(currentRobotPosition);
        scoutScanResult.Measures.Add(currentMeasure, currentRobotPosition);

        if (robot.RobotVariant == RobotVariant.SCOUT)
        {
            Position firstForwardPosition = currentRobotPosition.GetAdjacentPosition();
            Position secondForwardPosition = firstForwardPosition.GetAdjacentPosition();

            bool isFirstPositionValid = RobotManager.IsPositionInBounds(firstForwardPosition, topography);
            bool isSecondPositionValid = RobotManager.IsPositionInBounds(secondForwardPosition, topography);

            if (isFirstPositionValid)
            {
                Measure firstPositionMeasure = topography.GetMeasureAtPosition(firstForwardPosition);
                scoutScanResult.Measures.Add(firstPositionMeasure, firstForwardPosition);
            }

            if (isSecondPositionValid)
            {
                Measure secondPositionMeasure = topography.GetMeasureAtPosition(secondForwardPosition);
                scoutScanResult.Measures.Add(secondPositionMeasure, secondForwardPosition);
            }
        }

        robotStatusManager.RobotHeatTracker.PerformAction(robot, RobotAction.SCAN, topography);
        robotStatusManager.RobotPartsTracker.RobotPartDamage(robot, RobotPart.SCANSENSOR);
        robotStatusManager.RobotEnergyTracker.ConsumeEnergy(robot, RobotAction.SCAN);

        scoutScanResult.IsSuccess = true;
        scoutScanResult.HasRobotSurvived = true;

        return scoutScanResult;
    }
}