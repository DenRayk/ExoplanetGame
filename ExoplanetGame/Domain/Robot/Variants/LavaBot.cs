﻿using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Domain.Robot.Variants
{
    public class LavaBot : RobotBase
    {
        public LavaBot(ControlCenter.ControlCenter controlCenter, ExoPlanetBase exoPlanet, int robotId) : base(exoPlanet, controlCenter, robotId, RobotVariant.LAVA)
        {
            RobotInformation.MaxHeat = 200;
        }
    }
}