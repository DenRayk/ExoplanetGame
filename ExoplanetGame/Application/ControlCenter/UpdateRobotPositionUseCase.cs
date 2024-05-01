using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Application.ControlCenter
{
    public interface UpdateRobotPositionUseCase
    {
        public void UpdateRobotPosition(RobotBase robot, Position position);
    }
}