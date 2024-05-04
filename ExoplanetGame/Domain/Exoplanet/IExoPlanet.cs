using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Exoplanet.Variants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Domain.Exoplanet
{
    public interface IExoPlanet
    {
        Weather Weather { get; }
        RobotPositionManager RobotPositionManager { get; }
        RobotStatusManager RobotStatusManager { get; }
        Topography Topography { get; }

        void ChangeWeather();
    }
}