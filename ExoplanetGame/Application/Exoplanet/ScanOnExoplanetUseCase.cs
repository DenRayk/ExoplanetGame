using ExoplanetGame.Robot.RobotResults;
using ExoplanetGame.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Application.Exoplanet
{
    public interface ScanOnExoplanetUseCase
    {
        ScanResult Scan(RobotBase robot);
    }
}