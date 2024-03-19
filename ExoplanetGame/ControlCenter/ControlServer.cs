using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.RemoteRobot;

namespace ExoplanetGame.ControlCenter
{
    internal class ControlServer
    {
        private readonly int maxRobots = 5;
        private Exoplanet.Exoplanet exoPlanet;
        private int robotCount = 0;
        private ControlCenter controlCenter;

        public ControlServer(Exoplanet.Exoplanet exoPlanet)
        {
            this.exoPlanet = exoPlanet;
            controlCenter = new ControlCenter(exoPlanet);
        }

        public void Start()
        {
            MainMenu.Show(this);
        }

        public void AddRobot()
        {
            if (robotCount < maxRobots)
            {
                controlCenter.AddRobot(new RemoteRobot.RemoteRobot(exoPlanet, controlCenter.GetRobotCount()));
                robotCount++;
                Console.WriteLine("Robot added successfully.");
            }
            else
            {
                Console.WriteLine("Maximum number of robots reached.");
            }
        }

        public void SelectRobot()
        {
            Console.WriteLine("Select a robot to control:");
            controlCenter.DisplayRobots();

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > controlCenter.GetRobotCount())
            {
                Console.WriteLine("Invalid input. Please enter a valid robot number.");
            }

            ControlRobot(choice - 1);
        }

        private void ControlRobot(int robotID)
        {
            RobotMenu.Show(controlCenter.GetRobotByID(robotID));
        }
    }
}