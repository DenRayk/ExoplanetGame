using ExoplanetGame.Presentation.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGameTest.Mocks.Commands
{
    internal class F1Command : BaseCommand
    {
        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override int GetMenuSelection(int maxValue)
        {
            return (int)ConsoleKey.F1;
        }
    }
}