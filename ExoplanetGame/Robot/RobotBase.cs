using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot.RobotResults;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Robot
{
    public class RobotBase
    {
        protected ExoplanetBase exoPlanet;
        protected ControlCenter.ControlCenter controlCenter;
        public RobotInformation RobotInformation { get; set; }

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

        public virtual PositionResult Land(Position landPosition)
        {
            PositionResult landResult = exoPlanet.Land(this, landPosition);

            if (landResult.IsSuccess)
            {
                RobotInformation.HasLanded = true;
            }

            if (RobotInformation.HasLanded)
            {
                Console.WriteLine($"Robot landed on {landResult.Position}");
                RobotInformation.Position = landResult.Position;
                controlCenter.UpdateRobotPosition(this, landResult.Position);
            }
            else
            {
                Console.WriteLine($"{landResult.Message}");
            }

            return landResult;
        }

        public virtual string GetLanderName()
        {
            return $"Robot {RobotInformation.RobotID} ({RobotVariant})";
        }

        public virtual ScanResult Scan()
        {
            ScanResult scanResult = exoPlanet.Scan(this);

            if (scanResult.IsSuccess)
            {
                Console.WriteLine($"Scanned {scanResult.Measure}");
            }

            return scanResult;
        }

        public virtual PositionResult Move()
        {
            if (DoesOtherRobotBlocksMove())
            {
                Console.WriteLine("Robot cannot move because another robot is blocking the way");
                return new PositionResult()
                {
                    IsSuccess = false,
                    Message = "Robot cannot move because another robot is blocking the way",
                    Position = RobotInformation.Position
                };
            }

            PositionResult positionResult = exoPlanet.Move(this);
            if (positionResult.IsSuccess)
            {
                Console.WriteLine($"Robot moved to {positionResult.Position}");
                RobotInformation.Position = positionResult.Position;
                controlCenter.UpdateRobotPosition(this, positionResult.Position);
            }
            else
            {
                Console.WriteLine($"{positionResult.Message}");
                if (!positionResult.HasRobotSurvived)
                {
                    controlCenter.RemoveRobot(this);
                }
            }

            return positionResult;
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
            return exoPlanet.Equals(other.exoPlanet) && controlCenter.Equals(other.controlCenter) && RobotInformation.Equals(other.RobotInformation) && RobotInformation.MaxHeat == other.RobotInformation.MaxHeat && RobotVariant == other.RobotVariant;
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