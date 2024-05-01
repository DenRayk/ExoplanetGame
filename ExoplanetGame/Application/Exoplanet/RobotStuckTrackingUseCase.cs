using ExoplanetGame.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Application.Exoplanet
{
    public interface RobotStuckTrackingUseCase
    {
        void RobotGetStuckRandomly(RobotBase robot);

        bool IsRobotStuck(RobotBase robot);

        void UnstuckRobot(RobotBase robot);
    }
}