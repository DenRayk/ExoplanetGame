﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Application.ControlCenter
{
    public interface AddRobotUseCase
    {
        void AddRobot(RobotVariant robotVariant);
    }
}