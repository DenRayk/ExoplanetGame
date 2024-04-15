using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Robot.Variants
{
    public class SolarBot : RobotBase
    {
        public SolarBot(ControlCenter.ControlCenter controlCenter, ExoplanetBase exoPlanet, int robotId) : base(exoPlanet, controlCenter, robotId)
        {
            RobotVariant = RobotVariant.SOLAR;
        }

        public int LoadEnergy()
        {
            return 0;
        }
    }
}