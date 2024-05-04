using ExoplanetGame.Application.Exoplanet.PlanetEvents;
using ExoplanetGame.Application.Exoplanet.StatusTracking;
using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Exoplanet.Factory;
using ExoplanetGame.Domain.Exoplanet.Variants;

namespace ExoplanetGame.Application.Exoplanet
{
    public class ExoplanetService
    {
        private IExoPlanetBaseFactory exoPlanetFactory;
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
        public LoadOnExoplanetUseCase LoadOnExoplanetService { get; }
        public PlanetEventsService PlanetEventsService { get; }

        public ExoplanetService()
        {
            exoPlanetFactory = ExoPlanetFactory.GetInstance();
            PlanetEventsService = new PlanetEventsService(this);

            EnergyTracking = new EnergyTrackingService(this);
            HeatTracking = new HeatTrackingService(this);
            FreezeTracking = new FreezeTrackingService(this);
            RobotStuckTracking = new RobotStuckTrackingService(this);
            RobotPartsTracking = new RobotPartsTrackingService(this);
            RobotPostionsService = new RobotPositionService(this);
            ScanOnExoplanetService = new ScanOnExoplanetService(this);
            MoveOnExoplanetService = new MoveOnExoplanetService(this, PlanetEventsService);
            LandOnExoplanetService = new LandOnExoplanetService(this);
            RotateOnExoplanetService = new RotateOnExoplanetService(this, PlanetEventsService);
            LoadOnExoplanetService = new LoadOnExoplanetService(this);
        }

        public IExoPlanet ExoPlanet { get; set; }

        public void CreateExoPlanet(PlanetVariant planetVariant)
        {
            ExoPlanet = exoPlanetFactory.CreateExoPlanet(planetVariant);
        }
    }
}