using ExoplanetGame.Application.Exoplanet.StatusTracking;
using ExoplanetGame.Application.Robot;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Factory;
using ExoplanetGame.Exoplanet.Variants;

namespace ExoplanetGame.Application.Exoplanet
{
    public class ExoplanetService
    {
        private ExoPlanetBaseFactory exoPlanetFactory;
        public EnergyTrackingUseCase EnergyTracking { get; }
        public HeatTrackingUseCase HeatTracking { get; }
        public FreezeTrackingUseCase FreezeTracking { get; }
        public RobotStuckTrackingUseCase RobotStuckTracking { get; }
        public RobotPartsTrackingUseCase RobotPartsTracking { get; }
        public RobotPostionUseCase RobotPostionsService { get; }
        public ScanOnExoplanetUseCase ScanOnExoplanetService { get; }
        public MoveOnExoplanetUseCase MoveOnExoplanetService { get; }
        public LandOnExoplanetUseCase LandOnExoplanetService { get; }
        public RotateOnExoplanetUseCase RotateOnExoplanetService { get; }

        public ExoplanetService()
        {
            exoPlanetFactory = ExoPlanetFactory.GetInstance();

            EnergyTracking = new EnergyTrackingService(this);
            HeatTracking = new HeatTrackingService(this);
            FreezeTracking = new FreezeTrackingService(this);
            RobotStuckTracking = new RobotStuckTrackingService(this);
            RobotPartsTracking = new RobotPartsTrackingService(this);
            RobotPostionsService = new RobotPositionService(this);
            ScanOnExoplanetService = new ScanOnExoplanetService(this);
            MoveOnExoplanetService = new MoveOnExoplanetService(this);
            LandOnExoplanetService = new LandOnExoplanetService(this);
            RotateOnExoplanetService = new RotateOnExoplanetService(this);
        }

        public ExoPlanetBase ExoPlanet { get; private set; }

        public void CreateExoPlanet(PlanetVariant planetVariant)
        {
            ExoPlanet = exoPlanetFactory.CreateExoPlanet(planetVariant);
        }
    }
}