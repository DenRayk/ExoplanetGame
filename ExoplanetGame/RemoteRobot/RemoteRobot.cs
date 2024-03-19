using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exoplanet.exo;

namespace ExoplanetGame.RemoteRobot
{
    public class RemoteRobot : IRobot
    {
        public int robotID { get; set; }

        public void Crash()
        {
            throw new NotImplementedException();
        }

        public string GetLanderName()
        {
            throw new NotImplementedException();
        }
    }
}