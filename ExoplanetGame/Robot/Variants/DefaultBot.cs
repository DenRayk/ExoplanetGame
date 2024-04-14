﻿using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet;

namespace ExoplanetGame.Robot.Variants
{
    public sealed class DefaultBot : RobotBase
    {
        public DefaultBot(ControlCenter.ControlCenter controlCenter, ExoplanetBase exoPlanet, int robotId) : base(exoPlanet, controlCenter, robotId)
        {
            RobotVariant = RobotVariant.DEFAULT;
        }
    }
}