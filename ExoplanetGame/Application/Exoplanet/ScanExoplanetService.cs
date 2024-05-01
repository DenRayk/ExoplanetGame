using ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.RobotResults;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Application.Exoplanet
{
    internal class ScanExoplanetService : ScanExoplanetUseCase
    {
        private ExoplanetService exoplanetService;

        public ScanExoplanetService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public ScanResult Scan(RobotBase robot)
        {
            RobotResultBase robotResult = new();

            if (!CanRobotScan(robot, ref robotResult))
            {
                return new ScanResult(robotResult)
                {
                    IsSuccess = false,
                    HasRobotSurvived = true
                };
            }

            Position currentRobotPosition = exoplanetService.ExoPlanet.RobotPositionManager.Robots[robot];
            Measure currentMeasure = exoplanetService.ExoPlanet.Topography.GetMeasureAtPosition(currentRobotPosition);

            ScanResult scanResult = new(robotResult);
            scanResult.Measures.Add(currentMeasure, currentRobotPosition);

            if (robot is not ScoutBot)
            {
                exoplanetService.HeatTracking.PerformAction(robot, RobotAction.SCAN, exoplanetService.ExoPlanet.Topography);
                exoplanetService.RobotPartsTracking.RobotPartDamage(robot, RobotPart.SCANSENSOR);
                exoplanetService.EnergyTracking.ConsumeEnergy(robot, RobotAction.SCAN);

                scanResult.IsSuccess = true;
                scanResult.HasRobotSurvived = true;
                return scanResult;
            }

            Position firstForwardPosition = currentRobotPosition.GetAdjacentPosition();
            Position secondForwardPosition = firstForwardPosition.GetAdjacentPosition();

            bool isFirstPositionValid = exoplanetService.RobotPostions.IsPositionInBounds(firstForwardPosition, exoplanetService.ExoPlanet.Topography);
            bool isSecondPositionValid = exoplanetService.RobotPostions.IsPositionInBounds(secondForwardPosition, exoplanetService.ExoPlanet.Topography);

            if (isFirstPositionValid)
            {
                Measure firstPositionMeasure = exoplanetService.ExoPlanet.Topography.GetMeasureAtPosition(firstForwardPosition);
                scanResult.Measures.Add(firstPositionMeasure, firstForwardPosition);
            }

            if (isSecondPositionValid)
            {
                Measure secondPositionMeasure = exoplanetService.ExoPlanet.Topography.GetMeasureAtPosition(secondForwardPosition);
                scanResult.Measures.Add(secondPositionMeasure, secondForwardPosition);
            }

            exoplanetService.HeatTracking.PerformAction(robot, RobotAction.SCAN, exoplanetService.ExoPlanet.Topography);
            exoplanetService.RobotPartsTracking.RobotPartDamage(robot, RobotPart.SCANSENSOR);
            exoplanetService.EnergyTracking.ConsumeEnergy(robot, RobotAction.SCAN);

            scanResult.IsSuccess = true;
            scanResult.HasRobotSurvived = true;

            return scanResult;
        }

        private bool CanRobotScan(RobotBase robot, ref RobotResultBase robotResult)
        {
            bool isScanSensorDamaged = exoplanetService.RobotPartsTracking.IsRobotPartDamaged(robot, RobotPart.SCANSENSOR);
            bool doesRobotHaveEnery = exoplanetService.EnergyTracking.DoesRobotHaveEnoughEneryToAction(robot, RobotAction.SCAN);

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
}