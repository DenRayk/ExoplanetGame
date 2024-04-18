using ExoplanetGame.ControlCenter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Robot.RobotResults
{
    public class ScoutScanResult : RobotResultBase
    {
        public Dictionary<Measure, Position> Measures { get; set; } = new();
    }
}