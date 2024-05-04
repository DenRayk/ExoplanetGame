using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application.ControlCenter;
using ExoplanetGame.Presentation.Commands;

namespace ExoplanetGameTest.Mocks.Commands
{
    internal class TestCommand : BaseCommand
    {
        public override void Execute()
        {
            Console.WriteLine("Test command executed");
        }

        public override int GetMenuSelection(int minValue, int maxValue)
        {
            return 1;
        }
    }
}