using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.ExoplanetGame.Exoplanet;
using ExoplanetGame.Helper;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Robot
{
    public class RobotBase
    {
        protected readonly IExoPlanet exoPlanet;
        protected readonly ControlCenter.ControlCenter controlCenter;
        public RobotInformation RobotInformation { get; }
        public RobotVariant RobotVariant { get; }

        protected RobotBase(IExoPlanet exoPlanet, ControlCenter.ControlCenter controlCenter, int robotID,
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

        protected bool DoesOtherRobotBlockMove()
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

        public virtual void Crash()
        {
            Console.WriteLine("Robot crashed \n");
            exoPlanet.RemoveRobot(this);
            controlCenter.RemoveRobot(this);
        }

        public virtual PositionResult Land(Position landPosition)
        {
            PositionResult landResult = exoPlanet.Land(this, landPosition);

            if (landResult.IsSuccess)
            {
                Console.WriteLine($"Robot landed on {landResult.Position}");
                RobotInformation.HasLanded = true;
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
            return $"Robot {RobotInformation.RobotID} ({RobotVariant.GetDescriptionFromEnum()})";
        }

        public virtual ScanResult Scan()
        {
            ScanResult scanResult = exoPlanet.Scan(this);

            if (scanResult.IsSuccess)
            {
                Console.WriteLine($"Scanned {scanResult.Measure}");
            }
            else
            {
                Console.WriteLine($"{scanResult.Message}");
                if (!scanResult.HasRobotSurvived)
                {
                    Crash();
                }
            }

            return scanResult;
        }

        public virtual PositionResult Move()
        {
            PositionResult positionResult;

            if (DoesOtherRobotBlockMove())
            {
                positionResult = new PositionResult()
                {
                    IsSuccess = false,
                    Message = "Robot cannot move because another robot is blocking the way",
                    HasRobotSurvived = true,
                    Position = RobotInformation.Position
                };

                Console.WriteLine(positionResult.Message);

                return positionResult;
            }

            positionResult = exoPlanet.Move(this);

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
                    Crash();
                }
            }

            return positionResult;
        }

        public virtual RotationResult Rotate(Rotation rotation)
        {
            RotationResult rotationResult = exoPlanet.Rotate(this, rotation);
            controlCenter.UpdateRobotPosition(this, RobotInformation.Position);

            if (rotationResult.IsSuccess)
            {
                Console.WriteLine($"Robot rotated to {rotationResult.Direction}");
            }
            else
            {
                if (!rotationResult.HasRobotSurvived)
                {
                    Crash();
                }
                Console.WriteLine($"{rotationResult.Message}");
            }

            return rotationResult;
        }

        public virtual bool HasLanded()
        {
            return RobotInformation.Position != null;
        }

        public virtual PositionResult GetPosition()
        {
            PositionResult positionResult = exoPlanet.GetRobotPosition(this);

            if (!positionResult.HasRobotSurvived)
            {
                Crash();
            }

            return positionResult;
        }

        public virtual LoadResult LoadEnergy(int seconds)
        {
            Console.WriteLine("Loading energy...");
            LoadResult loadResult = exoPlanet.LoadEnergy(this, seconds);

            if (loadResult.IsSuccess)
            {
                Console.WriteLine($"Robot loaded energy to {loadResult.EnergyLoad}%");
            }
            else
            {
                if (!loadResult.HasRobotSurvived)
                {
                    Crash();
                }

                Console.WriteLine($"{loadResult.Message}");
            }
            return loadResult;
        }

        private void HandleOtherRobotPositionUpdated(object? sender, RobotPositionEventArgs e)
        {
            if (e.Robot.Equals(this))
                return;

            RobotInformation.OtherRobotPositions[e.Robot] = e.NewPosition;
        }

        protected bool Equals(RobotBase other)
        {
            return exoPlanet.Equals(other.exoPlanet) &&
                   controlCenter.Equals(other.controlCenter) &&
                   RobotInformation.Equals(other.RobotInformation) &&
                   RobotVariant == other.RobotVariant;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RobotBase)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(exoPlanet, controlCenter, RobotInformation, (int)RobotVariant);
        }
    }
}