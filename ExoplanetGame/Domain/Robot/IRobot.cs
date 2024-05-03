using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Domain.Robot.Variants;

namespace ExoplanetGame.Domain.Robot
{
    public interface IRobot
    {
        RobotInformation RobotInformation { get; }
        RobotVariant RobotVariant { get; }

        string GetLanderName();

        bool HasLanded();
    }
}