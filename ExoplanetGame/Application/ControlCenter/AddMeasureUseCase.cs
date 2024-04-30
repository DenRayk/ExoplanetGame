using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Application.ControlCenter
{
    public interface AddMeasureUseCase
    {
        void AddMeasure(Measure measure, Position position);

        void AddMeasures(Dictionary<Measure, Position> measures);
    }
}