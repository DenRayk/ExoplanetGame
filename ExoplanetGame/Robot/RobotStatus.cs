using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Robot
{
    public class RobotStatus
    {
        public Position Position { get; set; }

        public Dictionary<RobotBase, Position> OtherRobotPositions { get; set; } = new();
        public int Energy { get; set; }
        public int RobotID { get; set; }
        public bool HasLanded { get; set; }
    }
}