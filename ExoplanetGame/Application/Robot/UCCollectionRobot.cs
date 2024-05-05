using ExoplanetGame.Application.Exoplanet;

namespace ExoplanetGame.Application.Robot
{
    public class UCCollectionRobot
    {
        public RobotLandUseCase RobotLandService;
        public GetPositionUseCase GetPositionService;
        public RobotScanUseCase RobotScanService;
        public RobotMoveUseCase MoveRobotService;
        public RobotRotateUseCase RotateRobotService;
        public RobotCrashUseCase CrashRobotService;
        public RobotLoadUseCase LoadRobotService;
        public RobotPartsHealthUseCase RobotPartsHealthService;
        public PlanetMapUseCase PlanetMapService;
        public AddMeasureUseCase AddMeasureService;
        public RobotCoolDownUseCase RobotCoolDownService;

        public UCCollectionRobot(ExoplanetService exoplanetService)
        {
            PlanetMapService = new PlanetMapService();
            AddMeasureService = new AddMeasureService(PlanetMapService);
            RobotLandService = new RobotLandService(exoplanetService);
            GetPositionService = new GetPositionService(exoplanetService);
            RobotScanService = new RobotScanService(exoplanetService, AddMeasureService);
            MoveRobotService = new RobotMoveService(exoplanetService);
            RotateRobotService = new RobotRotateService(exoplanetService);
            CrashRobotService = new RobotCrashService(exoplanetService);
            LoadRobotService = new RobotLoadService(exoplanetService);
            RobotPartsHealthService = new RobotPartsHealthService(exoplanetService);
            RobotCoolDownService = new RobotCoolDownService(exoplanetService);
        }
    }
}