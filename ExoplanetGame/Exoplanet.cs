using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exoplanet.exo;

namespace Exoplanet
{
    internal class Exoplanet
    {
        private Dictionary<int, Robot> robots = new Dictionary<int, Robot>();
        private PlanetSize planetSize = new PlanetSize(5, 6);

        public int getRobotCount()
        {
            return robots.Count;
        }

        public PlanetSize getPlanetSize()
        {
            return planetSize;
        }
    }
}