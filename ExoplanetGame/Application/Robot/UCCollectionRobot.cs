﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Application.Robot
{
    public class UCCollectionRobot
    {
        private LandRobotUseCase landRobotUseCase;

        public UCCollectionRobot()
        {
            landRobotUseCase = new LandRobotService();
        }
    }
}