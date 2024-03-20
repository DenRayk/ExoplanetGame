using ExoplanetGame.Models;

namespace ExoplanetGame.RemoteRobot
{
    public class RemoteRobot : RobotBase
    {
        private Exoplanet.Exoplanet exoPlanet;
        private ControlCenter.ControlCenter controlCenter;

        public override int RobotID { get; set; }
        public override Position Position { get; set; }

        public RemoteRobot(ControlCenter.ControlCenter controlCenter, Exoplanet.Exoplanet exoPlanet, int robotId)
        {
            this.controlCenter = controlCenter;
            this.exoPlanet = exoPlanet;
            RobotID = robotId;
        }

        public override void Crash()
        {
            exoPlanet.RemoveRobot(this);
            Console.WriteLine("Robot crashed");
        }

        public override void Land(Position landPosition)
        {
            bool landed = exoPlanet.Land(this, landPosition);
            if (landed)
            {
                Console.WriteLine($"Robot landed on {landPosition}");
                Position = landPosition;
            }
            else
            {
                Console.WriteLine("Robot could not land");
            }
        }

        public override string GetLanderName()
        {
            return $"RemoteRobot {RobotID}";
        }

        public override Measure Scan()
        {
            Measure measure = exoPlanet.Scan(this);
            Console.WriteLine($"Scanned {measure.Ground}");
            return measure;
        }

        public override Position Move()
        {
            Position newPosition = exoPlanet.Move(this);
            if (newPosition != null)
            {
                Console.WriteLine($"Robot moved to {newPosition}");
                Position = newPosition;
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
            Position.Direction = exoPlanet.Rotate(this, rotation);
            Console.WriteLine($"Robot rotated to {Position}");
            controlCenter.UpdateRobotPosition(this, Position);
        }

        public override bool HasLanded()
        {
            return Position != null;
        }
    }
}