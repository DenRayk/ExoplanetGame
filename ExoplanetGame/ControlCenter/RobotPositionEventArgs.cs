using ExoplanetGame.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.ControlCenter
{
    public class RobotPositionEventArgs : EventArgs
    {
        public RobotBase Robot { get; }
        public Position NewPosition { get; }

        public RobotPositionEventArgs(RobotBase robot, Position newPosition)
        {
            Robot = robot;
            NewPosition = newPosition;
        }
    }
}