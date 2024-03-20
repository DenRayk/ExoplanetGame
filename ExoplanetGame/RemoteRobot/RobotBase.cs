using ExoplanetGame.Models;

namespace ExoplanetGame
{
    public abstract class RobotBase
    {
        public abstract int RobotID { get; set; }
        public abstract Position Position { get; set; }

        public abstract void Crash();

        public abstract void Land(Position landPosition);

        public abstract string GetLanderName();

        public abstract Measure Scan();

        public abstract Position Move();

        public abstract void Rotate(Rotation rotation);

        public abstract bool HasLanded();
    }
}