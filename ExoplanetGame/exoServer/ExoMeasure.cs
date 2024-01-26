using Exoplanet.exo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exoplanet.exoServer
{
    [Serializable]
    public class ExoMeasure(Ground ground, float temperature, int xDrift, int yDrift)
        : Measure(ground, temperature)
    {
        private static readonly long serialVersionUID = 2L;
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

        public Ground Ground
        {
            get => ground;
            set => ground = value;
        }

        public float Temperature
        {
            get => temperature;
            set => temperature = value;
        }
    }
}