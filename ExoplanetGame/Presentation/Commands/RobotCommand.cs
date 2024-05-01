using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands
{
    internal abstract class RobotCommand : BaseCommand
    {
        public RobotResultBase RobotResult { get; protected set; }
    }
}