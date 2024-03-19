using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exoplanet.exo;
using ExoplanetGame.Models;

namespace ExoplanetGame.RemoteRobot
{
    public class RemoteRobot : IRobot
    {
        public int RobotID { get; set; }
        public Position Position { get; set; }

        private Exoplanet.Exoplanet exoPlanet;
        private ControlCenter.ControlCenter controlCenter;

        public RemoteRobot(ControlCenter.ControlCenter controlCenter, Exoplanet.Exoplanet exoPlanet, int robotId)
        {
            this.controlCenter = controlCenter;
            this.exoPlanet = exoPlanet;
            RobotID = robotId;
        }

        public void Crash()
        {
            exoPlanet.RemoveRobot(this);
            Console.WriteLine("Robot crashed");
        }

        public void Land(Position landPosition)
        {
            bool landed = exoPlanet.Land(this, landPosition);
            if (landed)
            {
                Console.WriteLine($"Robot landed on {landPosition}");
                Position = landPosition;
            }
            else
            {
                Console.WriteLine("Robot could not land");
            }
        }

        public string GetLanderName()
        {
            throw new NotImplementedException();
        }

        public Measure Scan()
        {
            Measure measure = exoPlanet.Scan(this);
            Console.WriteLine($"Scanned {measure.Ground}");
            return measure;
        }

        public Position Move()
        {
            Position newPosition = exoPlanet.Move(this);
            if (newPosition != null)
            {
                Console.WriteLine($"Robot moved to {newPosition}");
                Position = newPosition;
                controlCenter.UpdateRobotPosition(this, newPosition);
            }
            else
            {
                Console.WriteLine("Robot crashed");
                controlCenter.RemoveRobot(this);
            }

            return newPosition;
        }
    }
}