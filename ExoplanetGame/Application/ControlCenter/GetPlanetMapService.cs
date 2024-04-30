using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.ControlCenter;

namespace ExoplanetGame.Application.ControlCenter
{
    internal class GetPlanetMapService : GetPlanetMapUseCase
    {
        private global::ExoplanetGame.ControlCenter.ControlCenter controlCenter;

        public GetPlanetMapService()
        {
            controlCenter = global::ExoplanetGame.ControlCenter.ControlCenter.GetInstance();
        }

        public PlanetMap GetPlanetMap()
        {
            return controlCenter.PlanetMap;
        }
    }
}