using ExoplanetGame.Domain.ControlCenter;

namespace ExoplanetGame.Application.ControlCenter
{
    internal class GetPlanetMapService : GetPlanetMapUseCase
    {
        private readonly Domain.ControlCenter.ControlCenter controlCenter;

        public GetPlanetMapService()
        {
            controlCenter = Domain.ControlCenter.ControlCenter.GetInstance();
        }

        public PlanetMap GetPlanetMap()
        {
            return controlCenter.PlanetMap;
        }
    }
}