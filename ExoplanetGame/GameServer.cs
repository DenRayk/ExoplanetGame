﻿using ExoplanetGame.Menus;
using ExoplanetGame.Robot;

namespace ExoplanetGame
{
    public class GameServer
    {
        private static GameServer gameServer;
        private readonly int maxRobots = 5;
        private static int robotID = 1;
        private Exoplanet.Exoplanet exoPlanet;
        private ControlCenter.ControlCenter controlCenter;
        private IRobotFactory robotFactory;
        public int RobotCount { get; set; }

        private GameServer()
        {
            exoPlanet = new();
            controlCenter = ControlCenter.ControlCenter.GetInstance(exoPlanet);
            controlCenter.Init(exoPlanet.Topography.PlanetSize);
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
            if (RobotCount < maxRobots)
            {
                RobotBase robotBase = robotFactory.CreateDefaultRobot(controlCenter, exoPlanet, robotID++);

                controlCenter.AddRobot(robotBase);
                RobotCount++;
                Console.WriteLine("Robot added successfully.");
            }
            else
            {
                Console.WriteLine("Maximum number of robots reached.");
            }
        }

        public void ControlRobot(int robotID)
        {
            RobotMenu.Show((DefaultRobot)controlCenter.GetRobotByID(robotID), controlCenter);
        }

        public void ClearRobots()
        {
            controlCenter.ClearRobots();
            RobotCount = 0;
        }
    }
}