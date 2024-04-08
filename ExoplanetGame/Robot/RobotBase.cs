using ExoplanetGame.ControlCenter;

namespace ExoplanetGame.Robot
{
    public abstract class RobotBase
    {
        public abstract RobotStatus RobotStatus { get; set; }

        public abstract void Crash();

        public abstract void Land(Position landPosition);

        public abstract string GetLanderName();

        public abstract Measure Scan();

        public abstract Position Move();

        public abstract void Rotate(Rotation rotation);

        public abstract bool HasLanded();
    }
}