using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Presentation.Commands;

namespace ExoplanetGameTest.Mocks.Commands
{
    internal class EscCommand : BaseCommand
    {
        public override void Execute()
        {
            throw new NotImplementedException();
        }

        public override int GetMenuSelection(int maxValue)
        {
            return (int)ConsoleKey.Escape;
        }
    }
}