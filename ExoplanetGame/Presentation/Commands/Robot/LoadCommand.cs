using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application;
using ExoplanetGame.Presentation.Commands.ControlCenter;
using ExoplanetGame.Robot;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class LoadCommand : RobotCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;

        public LoadCommand(RobotBase robotBase, UCCollection ucCollection, BaseCommand previousCommand, ControlCenterCommand controlCenterCommand) : base(previousCommand, controlCenterCommand)
        {
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}