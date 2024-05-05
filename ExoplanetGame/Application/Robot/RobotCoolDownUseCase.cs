using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Robot
{
    public interface RobotCoolDownUseCase
    {
        void CoolDownRobot(IRobot robot, int seconds);
    }
}
