using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet;

namespace ExoplanetGame.Robot.Variants
{
    public class ScoutRobot : RobotBase
    {
        private Exoplanet.Exoplanet exoPlanet;
        private ControlCenter.ControlCenter controlCenter;

        public override RobotStatus RobotStatus { get; set; }
        public override int MaxHeat { get; set; } = 100;

        public override void Crash()
        {
            exoPlanet.RemoveRobot(this);
            Console.WriteLine("Robot crashed");
        }

        public override bool Land(Position landPosition)
        {
            RobotStatus.HasLanded = exoPlanet.Land(this, landPosition);
            if (RobotStatus.HasLanded)
            {
                Console.WriteLine($"Robot landed on {landPosition}");
                RobotStatus.Position = landPosition;
            }
            else
            {
                Console.WriteLine("Robot could not land");
            }

            return RobotStatus.HasLanded;
        }

        public override string GetLanderName()
        {
            return $"Robot {RobotStatus.RobotID}";
        }

        public override Measure Scan()
        {
            Measure measure = exoPlanet.Scan(this);
            Console.WriteLine($"Scanned {measure}");
            return measure;
        }

        public override Position Move()
        {
            Position newPosition = exoPlanet.Move(this);
            if (newPosition != null)
            {
                Console.WriteLine($"Robot moved to {newPosition}");
                RobotStatus.Position = newPosition;
                controlCenter.UpdateRobotPosition(this, newPosition);
            }
            else
            {
                Console.WriteLine("Robot crashed");
                controlCenter.RemoveRobot(this);
            }

            return newPosition;
        }

        public override void Rotate(Rotation rotation)
        {
            RobotStatus.Position.Direction = exoPlanet.Rotate(this, rotation);
            Console.WriteLine($"Robot rotated to {RobotStatus.Position}");
            controlCenter.UpdateRobotPosition(this, RobotStatus.Position);
        }

        public override bool HasLanded()
        {
            return RobotStatus.Position != null;
        }

        public override Position GetPosition()
        {
            return exoPlanet.GetRobotPosition(this);
        }
    }
}
