using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exoplanet
{
    internal class Exoplanet
    {
        private static void Main(string[] args)
        {
            ExoplanetServer exoplanetServer = new();
            exoplanetServer.Start();
        }
    }
}