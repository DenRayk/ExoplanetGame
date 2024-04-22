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
        RobotResultBase robotResult = new();

        if (CanRobotScan(robot, ref robotResult))
        {
            robotStatusManager.RobotHeatTracker.PerformAction(robot, RobotAction.SCAN, topography);
            robotStatusManager.RobotPartsTracker.RobotPartDamage(robot, RobotPart.SCANSENSOR);
            robotStatusManager.RobotEnergyTracker.ConsumeEnergy(robot, RobotAction.SCAN);

            return new ScanResult(robotResult)
            {
                Measure = topography.GetMeasureAtPosition(robotManager.robots[robot]),
                IsSuccess = true,
                HasRobotSurvived = true
            };
        }

        return new ScanResult(robotResult);
    }

    public ScoutScanResult ScoutScan(RobotBase robot, Topography topography)
    {
        RobotResultBase robotResult = new();

        if (!CanRobotScan(robot, ref robotResult))
        {
            return new ScoutScanResult(robotResult)
            {
                IsSuccess = false,
                HasRobotSurvived = true
            };
        }

        Position currentRobotPosition = robotManager.robots[robot];
        Measure currentMeasure = topography.GetMeasureAtPosition(currentRobotPosition);

        ScoutScanResult scoutScanResult = new(robotResult);
        scoutScanResult.Measures.Add(currentMeasure, currentRobotPosition);

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

        robotStatusManager.RobotHeatTracker.PerformAction(robot, RobotAction.SCAN, topography);
        robotStatusManager.RobotPartsTracker.RobotPartDamage(robot, RobotPart.SCANSENSOR);
        robotStatusManager.RobotEnergyTracker.ConsumeEnergy(robot, RobotAction.SCAN);

        scoutScanResult.IsSuccess = true;
        scoutScanResult.HasRobotSurvived = true;

        return scoutScanResult;
    }

    private bool CanRobotScan(RobotBase robot, ref RobotResultBase robotResult)
    {
        bool isScanSensorDamaged = robotStatusManager.RobotPartsTracker.isRobotPartDamaged(robot, RobotPart.SCANSENSOR);
        bool doesRobotHaveEnery = robotStatusManager.RobotEnergyTracker.DoesRobotHaveEnoughEneryToAction(robot, RobotAction.SCAN);

        if (isScanSensorDamaged)
        {
            robotResult.Message = "Scan sensor is damaged. Please repair in Control Center.\n";
        }

        if (doesRobotHaveEnery)
        {
            robotResult.Message += "Robot does not have enough energy to scan.\n";
        }

        bool canScan = !isScanSensorDamaged && doesRobotHaveEnery;

        return canScan;
    }
}