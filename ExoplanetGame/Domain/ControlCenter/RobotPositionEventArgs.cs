using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Domain.ControlCenter
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