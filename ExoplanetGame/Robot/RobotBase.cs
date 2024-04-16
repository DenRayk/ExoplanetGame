using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Robot
{
    public class RobotBase
    {
        protected ExoplanetBase exoPlanet;
        protected ControlCenter.ControlCenter controlCenter;
        public RobotInformation RobotInformation { get; set; }
        public int MaxHeat { get; set; } = 100;

        public int MaxEnergy { get; set; } = 100;

        public RobotVariant RobotVariant { get; }

        protected RobotBase(ExoplanetBase exoPlanet, ControlCenter.ControlCenter controlCenter, int robotID,
            RobotVariant robotVariant)
        {
            this.exoPlanet = exoPlanet;
            this.controlCenter = controlCenter;
            controlCenter.RobotPositionUpdated += HandleOtherRobotPositionUpdated;
            RobotVariant = robotVariant;

            RobotInformation = new RobotInformation
            {
                RobotID = robotID,
            };
        }

        public virtual void Crash()
        {
            exoPlanet.RemoveRobot(this);
            controlCenter.RemoveRobot(this);
            Console.WriteLine("Robot crashed");
        }

        public virtual Position Land(Position landPosition)
        {
            landPosition = exoPlanet.Land(this, landPosition);

            if (landPosition != null)
            {
                RobotInformation.HasLanded = true;
            }

            if (RobotInformation.HasLanded)
            {
                Console.WriteLine($"Robot landed on {landPosition}");
                RobotInformation.Position = landPosition;
                controlCenter.UpdateRobotPosition(this, landPosition);
            }
            else
            {
                Console.WriteLine("Robot could not land");
            }

            return landPosition;
        }

        public virtual string GetLanderName()
        {
            return $"Robot {RobotInformation.RobotID} ({RobotVariant})";
        }

        public virtual Measure Scan()
        {
            Measure measure = exoPlanet.Scan(this);
            Console.WriteLine($"Scanned {measure}");
            return measure;
        }

        public virtual Position Move()
        {
            if (DoesOtherRobotBlocksMove())
            {
                Console.WriteLine("Robot cannot move because another robot is blocking the way");
                return RobotInformation.Position;
            }

            Position newPosition = exoPlanet.Move(this);
            if (newPosition != null)
            {
                Console.WriteLine($"Robot moved to {newPosition}");
                RobotInformation.Position = newPosition;
                controlCenter.UpdateRobotPosition(this, newPosition);
            }
            else
            {
                Console.WriteLine("Robot crashed");
                controlCenter.RemoveRobot(this);
            }

            return newPosition;
        }

        public virtual void Rotate(Rotation rotation)
        {
            RobotInformation.Position.Direction = exoPlanet.Rotate(this, rotation);
            controlCenter.UpdateRobotPosition(this, RobotInformation.Position);

            Console.WriteLine($"Robot rotated to {RobotInformation.Position}");
        }

        public virtual bool HasLanded()
        {
            return RobotInformation.Position != null;
        }

        public virtual Position GetPosition()
        {
            return exoPlanet.GetRobotPosition(this);
        }

        public virtual void LoadEnergy(int seconds)
        {
            exoPlanet.LoadEnergy(this, seconds);
        }

        protected bool DoesOtherRobotBlocksMove()
        {
            foreach (var otherRobot in RobotInformation.OtherRobotPositions.Keys)
            {
                if (RobotInformation.OtherRobotPositions[otherRobot].Equals(RobotInformation.Position.GetAdjacentPosition()))
                {
                    return true;
                }
            }

            return false;
        }

        private void HandleOtherRobotPositionUpdated(object? sender, RobotPositionEventArgs e)
        {
            if (e.Robot.Equals(this))
                return;

            RobotInformation.OtherRobotPositions[e.Robot] = e.NewPosition;
        }

        protected bool Equals(RobotBase other)
        {
            return exoPlanet.Equals(other.exoPlanet) && controlCenter.Equals(other.controlCenter) && RobotInformation.Equals(other.RobotInformation) && MaxHeat == other.MaxHeat && RobotVariant == other.RobotVariant;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RobotBase)obj);
        }
    }
}