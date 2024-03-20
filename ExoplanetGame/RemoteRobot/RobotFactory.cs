using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.RemoteRobot
{
    internal class RobotFactory : IRobotFactory
    {
        public RobotBase CreateRobot(ControlCenter.ControlCenter controlCenter, Exoplanet.Exoplanet exoPlanet, int robotID)
        {
            return new RemoteRobot(controlCenter, exoPlanet, robotID);
        }
    }
}