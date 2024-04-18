using ExoplanetGame.Robot;

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