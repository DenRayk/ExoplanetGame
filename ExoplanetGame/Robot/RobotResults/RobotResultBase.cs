using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Exoplanet;

namespace ExoplanetGame.Robot.RobotResults
{
    public class RobotResultBase
    {
        public bool IsSuccess { get; set; }
        public bool HasRobotSurvived { get; set; }
        public string Message { get; set; }
    }
}