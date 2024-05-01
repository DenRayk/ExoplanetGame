﻿using ExoplanetGame.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Application.Exoplanet
{
    public interface FreezeTrackingUseCase
    {
        void FreezeRobot(RobotBase robot);

        bool IsFrozen(RobotBase robot);

        void UpdateLastMove(RobotBase robot);

        DateTime GetLastMove(RobotBase robot);
    }
}