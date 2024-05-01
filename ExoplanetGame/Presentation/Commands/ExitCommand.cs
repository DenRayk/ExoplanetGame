﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Presentation.Commands
{
    internal class ExitCommand(BaseCommand previousCommand) : BaseCommand(previousCommand)
    {
        public override void Execute()
        {
            Environment.Exit(0);
        }
    }
}