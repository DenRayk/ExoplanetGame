using ExoplanetGame.ControlCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Exoplanet;

namespace ExoplanetGame.Robot.Variants
{
    public class MudBot : RobotBase
    {
        public MudBot(ControlCenter.ControlCenter controlCenter, ExoplanetBase exoPlanet, int robotId) : base(exoPlanet, controlCenter, robotId)
        {
            RobotVariant = RobotVariant.MUD;
        }
    }
}