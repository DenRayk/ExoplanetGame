using Exoplanet.exo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Good
namespace Exoplanet.exoServer
{
    [Serializable]
    public class ExoMeasure(Ground ground, int xDrift, int yDrift)
        : Measure(ground)
    {
        public int xDrift { get; set; } = xDrift;
        public int yDrift { get; set; } = yDrift;

        public ExoMeasure(Ground ground)
            : this(ground, 0, 0)
        {
        }
    }
}