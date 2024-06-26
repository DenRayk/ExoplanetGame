﻿using ExoplanetGame.Domain.Exoplanet;

namespace ExoplanetGame.Domain.Robot.Variants
{
    public class DefaultBot : IRobot
    {
        public IExoPlanet ExoPlanet { get; }

        public RobotInformation RobotInformation { get; }

        public DefaultBot(IExoPlanet exoPlanet, int robotID)
        {
            ExoPlanet = exoPlanet;
            RobotInformation = new RobotInformation
            {
                RobotID = robotID,
                RobotParts =
                {
                    [RobotPart.WHEELS] = 150,
                    [RobotPart.SCANSENSOR] = 150
                }
            };
        }

        public string GetLanderName()
        {
            return $"Robot {RobotInformation.RobotID} ({GetType().Name})";
        }
    }
}