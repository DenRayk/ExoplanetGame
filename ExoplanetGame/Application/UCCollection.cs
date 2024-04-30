using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application.ControlCenter;
using ExoplanetGame.Application.Robot;

namespace ExoplanetGame.Application
{
    internal class UCCollection
    {
        public UCCollection()
        {
            UcCollectionControlCenter = new();
            UcCollectionRobot = new();
        }

        public UCCollectionControlCenter UcCollectionControlCenter { get; }
        public UCCollectionRobot UcCollectionRobot { get; }
    }
}