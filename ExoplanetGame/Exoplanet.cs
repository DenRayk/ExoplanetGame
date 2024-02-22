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

        public int getRobotCount()
        {
            return robots.Count;
        }
    }
}