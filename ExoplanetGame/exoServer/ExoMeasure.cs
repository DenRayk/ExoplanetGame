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
    public class ExoMeasure(Ground ground, float temperature, int xDrift, int yDrift)
        : Measure(ground, temperature)
    {
        public int xDrift { get; set; } = xDrift;
        public int yDrift { get; set; } = yDrift;

        public ExoMeasure(Ground ground, float temperature)
            : this(ground, temperature, 0, 0)
        {
        }

        public void SetTemp(float temp)
        {
            temperature = temp;
        }

        public void AddTemp(float temp)
        {
            temperature += temp;
        }
    }
}