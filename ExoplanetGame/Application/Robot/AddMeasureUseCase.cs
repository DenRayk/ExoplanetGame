using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Application.Robot
{
    public interface AddMeasureUseCase
    {
        void AddMeasure(Measure measure, Position position);

        public void AddMeasures(Dictionary<Measure, Position> measures);
    }
}