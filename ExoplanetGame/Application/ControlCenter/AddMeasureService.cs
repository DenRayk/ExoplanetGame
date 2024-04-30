using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot.Movement;

namespace ExoplanetGame.Application.ControlCenter
{
    internal class AddMeasureService : AddMeasureUseCase
    {
        private global::ExoplanetGame.ControlCenter.ControlCenter controlCenter;

        public AddMeasureService()
        {
            controlCenter = global::ExoplanetGame.ControlCenter.ControlCenter.GetInstance();
        }

        public void AddMeasure(Measure measure, Position position)
        {
            controlCenter.PlanetMap.updateMap(position, measure.Ground);
        }

        public void AddMeasures(Dictionary<Measure, Position> measures)
        {
            foreach (var measure in measures)
            {
                controlCenter.PlanetMap.updateMap(measure.Value, measure.Key.Ground);
            }
        }
    }
}