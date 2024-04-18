using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Exoplanet
{
    public class RobotStatusManager
    {
        public RobotHeatTracker RobotHeatTracker { get; } = new();
        public RobotEnergyTracker RobotEnergyTracker { get; } = new();
        public RobotStuckTracker RobotStuckTracker { get; } = new();
        public RobotPartsTracker RobotPartsTracker { get; } = new();
        public RobotFreezeTracker RobotFreezeTracker { get; } = new();
    }
}