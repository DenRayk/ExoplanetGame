// Exoplanet.cs

using System;
using System.Collections.Generic;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.RemoteRobot;

namespace ExoplanetGame.Exoplanet
{
    public class Exoplanet
    {
        public Topography Topography { get; set; }
        private RobotManager robotManager;

        public Exoplanet()
        {
            Topography = new Topography([
                "GSSWPFSGGL",
                "SPW4PSFFLL",
                "SGWPMSFLLF",
                "SSWWMGSFFG",
                "FGSWWSSGFG",
                "FFWWGGSGFF"
            ]);
            robotManager = new RobotManager();
        }

        public int GetRobotCount()
        {
            return robotManager.GetRobotCount();
        }

        public void RemoveRobot(RemoteRobot.RemoteRobot remoteRobot)
        {
            robotManager.RemoveRobot(remoteRobot);
        }

        public bool Land(RemoteRobot.RemoteRobot remoteRobot, Position landPosition)
        {
            return robotManager.LandRobot(remoteRobot, landPosition, Topography);
        }

        public Position Move(RemoteRobot.RemoteRobot remoteRobot)
        {
            return robotManager.MoveRobot(remoteRobot, Topography);
        }

        public Direction Rotate(RemoteRobot.RemoteRobot robot, Rotation rotation)
        {
            return robotManager.RotateRobot(robot, rotation);
        }

        public Measure Scan(RemoteRobot.RemoteRobot robot)
        {
            return Topography.GetMeasureAtPosition(robotManager.GetRobotPosition(robot));
        }

        public void Remove(RemoteRobot.RemoteRobot robot)
        {
            Console.WriteLine($"Remove: {robot.GetLanderName()}");
            robotManager.RemoveRobot(robot);
        }
    }
}