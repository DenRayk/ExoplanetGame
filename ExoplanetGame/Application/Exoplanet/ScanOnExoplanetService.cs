using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Domain.Robot.Variants;
using ExoplanetGame.Domain.Exoplanet;

namespace ExoplanetGame.Application.Exoplanet
{
    internal class ScanOnExoplanetService : ScanOnExoplanetUseCase
    {
        private ExoplanetService exoplanetService;

        public ScanOnExoplanetService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public ScanResult Scan(IRobot robot)
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

            bool isFirstPositionValid = exoplanetService.RobotPostionsService.IsPositionInBounds(firstForwardPosition, exoplanetService.ExoPlanet.Topography);
            bool isSecondPositionValid = exoplanetService.RobotPostionsService.IsPositionInBounds(secondForwardPosition, exoplanetService.ExoPlanet.Topography);

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

        private bool CanRobotScan(IRobot robot, ref RobotResultBase robotResult)
        {
            bool isScanSensorDamaged = exoplanetService.RobotPartsTracking.IsRobotPartDamaged(robot, RobotPart.SCANSENSOR);
            bool doesRobotHaveEnery = exoplanetService.EnergyTracking.DoesRobotHaveEnoughEneryToAction(robot, RobotAction.SCAN);

            if (isScanSensorDamaged)
            {
                robotResult.Message = "Scan sensor is damaged. Please repair in Control Center.\n";
            }

            if (!doesRobotHaveEnery)
            {
                robotResult.Message += "Robot does not have enough energy to scan.\n";
            }

            bool canScan = !isScanSensorDamaged && doesRobotHaveEnery;

            return canScan;
        }
    }
}