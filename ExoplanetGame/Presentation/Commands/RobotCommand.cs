﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Presentation.Commands.ControlCenter;

namespace ExoplanetGame.Presentation.Commands
{
    public abstract class RobotCommand : BaseCommand
    {
        private ControlCenterCommand controlCenterCommand;

        public abstract override void Execute();

        protected RobotCommand(BaseCommand previousCommand, ControlCenterCommand controlCenterCommand) : base(previousCommand)
        {
            this.controlCenterCommand = controlCenterCommand;
        }
    }
}