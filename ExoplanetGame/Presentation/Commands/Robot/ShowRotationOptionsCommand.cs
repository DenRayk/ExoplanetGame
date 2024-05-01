using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Helper;
using ExoplanetGame.Presentation.Commands.PlanetSelection;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class ShowRotationOptionsCommand : BaseCommand
    {
        private RobotBase robotBase;
        private UCCollection ucCollection;

        public ShowRotationOptionsCommand(RobotBase robotBase, UCCollection ucCollection)
        {
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            var options = getRotationOptions();

            BaseCommand baseCommand = ReadUserInputWithOptions(options);

            baseCommand.Execute();
        }

        private Dictionary<string, BaseCommand> getRotationOptions()
        {
            Dictionary<string, BaseCommand> options = new();
            foreach (Rotation rotation in Enum.GetValues(typeof(Rotation)))
            {
                options.Add(rotation.GetDescriptionFromEnum(), new RotateCommand(robotBase, ucCollection, rotation));
            }
            return options;
        }
    }
}