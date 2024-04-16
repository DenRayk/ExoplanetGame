using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Robot
{
    public class RobotInformation
    {
        public Position Position { get; set; }

        public Dictionary<RobotBase, Position> OtherRobotPositions { get; set; } = new();
        public int RobotID { get; set; }
        public bool HasLanded { get; set; }
    }
}