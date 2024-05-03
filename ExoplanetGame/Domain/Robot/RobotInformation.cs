using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Domain.Robot
{
    public class RobotInformation
    {
        public Position Position { get; set; }
        public Dictionary<IRobot, Position> OtherRobotPositions { get; set; } = new();
        public int RobotID { get; set; }
        public bool HasLanded { get; set; }
        public int MaxHeat { get; set; } = 100;
        public int MaxEnergy { get; set; } = 100;

        public Dictionary<RobotPart, int> RobotParts { get; set; } = new();

        public RobotInformation()
        {
            InitalizeRobotParts();
        }

        public void InitalizeRobotParts()
        {
            foreach (RobotPart part in Enum.GetValues(typeof(RobotPart)))
            {
                RobotParts.Add(part, 100);
            }
        }
    }
}