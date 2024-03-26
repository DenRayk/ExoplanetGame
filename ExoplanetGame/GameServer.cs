﻿using ExoplanetGame.Menus;
using ExoplanetGame.RemoteRobot;
using System;

namespace ExoplanetGame
{
    internal class GameServer
    {
        private static GameServer gameServer;
        private readonly int maxRobots = 5;
        private int robotCount;
        private Exoplanet.Exoplanet exoPlanet;
        private ControlCenter.ControlCenter controlCenter;
        private IRobotFactory robotFactory;

        private GameServer()
        {
            exoPlanet = new();
            controlCenter = new ControlCenter.ControlCenter(exoPlanet);
            controlCenter.Init(exoPlanet.PlanetSize);
            robotFactory = RobotFactory.GetInstance();
        }

        public static GameServer GetInstance()
        {
            if (gameServer == null)
            {
                gameServer = new GameServer();
            }
            return gameServer;
        }

        public void Start()
        {
            MainMenu.Show(this, controlCenter);
        }

        public void AddRobot()
        {
            if (robotCount < maxRobots)
            {
                int robotID = controlCenter.GetRobotCount();

                RobotBase robotBase = robotFactory.CreateRemoteRobot(controlCenter, exoPlanet, robotID);

                controlCenter.AddRobot(robotBase);
                robotCount++;
                Console.WriteLine("Robot added successfully.");
            }
            else
            {
                Console.WriteLine("Maximum number of robots reached.");
            }
        }

        public void ControlRobot(int robotID)
        {
            RobotMenu.Show(controlCenter.GetRobotByID(robotID), controlCenter);
        }
    }
}