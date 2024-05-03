using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Robot.Variants;
using ExoplanetGame.Helper;

namespace ExoplanetGame.Domain.Robot
{
    public class RobotBase : IRobot
    {
        protected readonly ExoPlanetBase exoPlanet;
        private RobotRepository robotRepository;
        public RobotInformation RobotInformation { get; }
        public RobotVariant RobotVariant { get; }

        public RobotBase(ExoPlanetBase exoPlanet, ControlCenter.ControlCenter controlCenter, int robotID,
            RobotVariant robotVariant)
        {
            this.exoPlanet = exoPlanet;
            robotRepository = RobotRepository.GetInstance();
            robotRepository.RobotPositionUpdated += HandleOtherRobotPositionUpdated;
            RobotVariant = robotVariant;

            RobotInformation = new RobotInformation
            {
                RobotID = robotID,
            };
        }

        public virtual string GetLanderName()
        {
            return $"Robot {RobotInformation.RobotID} ({RobotVariant.GetDescriptionFromEnum()})";
        }

        public virtual bool HasLanded()
        {
            return RobotInformation.Position != null;
        }

        private void HandleOtherRobotPositionUpdated(object? sender, RobotPositionEventArgs e)
        {
            if (e.Robot.Equals(this))
                return;

            RobotInformation.OtherRobotPositions[e.Robot] = e.NewPosition;
        }

        protected bool Equals(RobotBase robotBase)
        {
            return exoPlanet.Equals(robotBase.exoPlanet) &&
                   RobotInformation.Equals(robotBase.RobotInformation) &&
                   RobotVariant == robotBase.RobotVariant;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((RobotBase)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(exoPlanet, RobotInformation, (int)RobotVariant);
        }
    }
}