using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application;
using ExoplanetGame.Robot;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class LandCommand : BaseCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;

        public LandCommand(RobotBase robotBase, UCCollection ucCollection)
        {
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
        }
    }
}