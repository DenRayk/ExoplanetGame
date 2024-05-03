using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Domain.ControlCenter
{
    public class RobotPositionEventArgs : EventArgs
    {
        public IRobot Robot { get; }
        public Position NewPosition { get; }

        public RobotPositionEventArgs(IRobot robot, Position newPosition)
        {
            Robot = robot;
            NewPosition = newPosition;
        }
    }
}