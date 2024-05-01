using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Factory;
using ExoplanetGame.Exoplanet.Variants;

namespace ExoplanetGame.Application.Exoplanet
{
    public class ExoplanetService
    {
        private ExoPlanetBaseFactory exoPlanetFactory;

        public EnergyTrackingUseCase EnergyTrackingUseCase { get; }
        public HeatTrackingUseCase HeatTrackingUseCase { get; }
        public FreezeTrackingUseCase FreezeTrackingUseCase { get; }
        public RobotStuckTrackingUseCase RobotStuckTrackingUseCase { get; }
        public RobotPartsTrackingUseCase RobotPartsTrackingUseCase { get; }

        public RobotPostionUseCase RobotPostionUseCase { get; }

        public ExoplanetService()
        {
            exoPlanetFactory = ExoPlanetFactory.GetInstance();

            EnergyTrackingUseCase = new EnergyTrackingService(this);
            HeatTrackingUseCase = new HeatTrackingService(this);
            FreezeTrackingUseCase = new FreezeTrackingService(this);
            RobotStuckTrackingUseCase = new RobotStuckTrackingService(this);
            RobotPartsTrackingUseCase = new RobotPartsTrackingService(this);
            RobotPostionUseCase = new RobotPositionService(this);
        }

        public ExoPlanetBase ExoPlanet { get; private set; }

        public void CreateExoPlanet(PlanetVariant planetVariant)
        {
            ExoPlanet = exoPlanetFactory.CreateExoPlanet(planetVariant);
        }
    }
}