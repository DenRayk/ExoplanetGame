using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Application.Robot
{
    internal class AddMeasureService : AddMeasureUseCase
    {
        private readonly PlanetMapUseCase planetMapService;

        public AddMeasureService(PlanetMapUseCase planetMapService)
        {
            this.planetMapService = planetMapService;
        }

        public void AddMeasure(Measure measure, Position position)
        {
            planetMapService.UpdateMap(position, measure.Ground);
        }

        public void AddMeasures(Dictionary<Position, Measure > measures)
        {
            foreach (var measure in measures)
            {
                AddMeasure(measure.Value, measure.Key);
            }
        }
    }
}