﻿using ExoplanetGame.Application.Exoplanet.PlanetEvents;
using ExoplanetGame.Application.Exoplanet.StatusTracking;
using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Exoplanet.Factory;
using ExoplanetGame.Domain.Exoplanet.Variants;

namespace ExoplanetGame.Application.Exoplanet
{
    public class ExoplanetService : IExoplanetService
    {
        private ExoPlanetFactory exoPlanetFactory;
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

        public ChangeWeatherUseCase ChangeWeatherService { get; }
        public FreezeUseCase FreezeService { get; }

        public RandomAttackUseCase RandomAttackService { get; }

        public VolcanicEruptionUseCase VolcanicEruptionService { get; }
        public PlanetEventsUseCase PlanetEventsService { get; }

        public ExoplanetService()
        {
            EnergyTracking = new EnergyTrackingService(this);
            HeatTracking = new HeatTrackingService(this);
            FreezeTracking = new FreezeTrackingService(this);
            RobotStuckTracking = new RobotStuckTrackingService(this);
            RobotPartsTracking = new RobotPartsTrackingService(this);
            RobotPostionsService = new RobotPositionService(this);
            ScanOnExoplanetService = new ScanOnExoplanetService(this);
            ChangeWeatherService = new ChangeWeatherService(this);
            FreezeService = new FreezeService(this);
            RandomAttackService = new RandomAttackService();
            VolcanicEruptionService = new VolcanicEruptionService();
            PlanetEventsService = new PlanetEventsService(
                this,
                ChangeWeatherService,
                FreezeService,
                RandomAttackService,
                VolcanicEruptionService);
            MoveOnExoplanetService = new MoveOnExoplanetService(this, PlanetEventsService);
            LandOnExoplanetService = new LandOnExoplanetService(this);
            RotateOnExoplanetService = new RotateOnExoplanetService(this, PlanetEventsService);
            LoadOnExoplanetService = new LoadOnExoplanetService(this);
        }

        public IExoPlanet ExoPlanet { get; set; }

        public void CreateExoPlanet(PlanetVariant planetVariant)
        {
            exoPlanetFactory = planetVariant switch
            {
                PlanetVariant.GAIA => new GaiaPlanetFactory(),
                PlanetVariant.AQUATICA => new AquaticaPlanetFactory(),
                PlanetVariant.TERRA => new TerraPlanetFactory(),
                PlanetVariant.FROSTFELL => new FrostfellPlanetFactory(),
                PlanetVariant.LAVARIA => new LavariaPlanetFactory(),
                PlanetVariant.TROPICA => new TropicaPlanetFactory(),
                _ => exoPlanetFactory
            };

            ExoPlanet = exoPlanetFactory.CreateExoPlanet();
        }
    }
}