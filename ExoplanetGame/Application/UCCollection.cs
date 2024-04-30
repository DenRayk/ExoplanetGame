using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application.ControlCenter;
using ExoplanetGame.Application.Robot;

namespace ExoplanetGame.Application
{
    public class UCCollection
    {
        public UCCollection()
        {
            UcCollectionControlCenter = new UCCollectionControlCenter();
            UcCollectionRobot = new UCCollectionRobot();
        }

        public UCCollectionControlCenter UcCollectionControlCenter { get; }
        public UCCollectionRobot UcCollectionRobot { get; }
    }
}