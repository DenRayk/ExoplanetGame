using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Application.ControlCenter
{
    internal class ControlCenterService
    {
        private SelectPlanetService selectPlanetService;

        public ControlCenterService(SelectPlanetService selectPlanetService)
        {
            this.selectPlanetService = selectPlanetService;
        }
    }
}