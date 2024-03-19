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
        public int robotID { get; set; }

        private Exoplanet.Exoplanet exoPlanet;

        public RemoteRobot(Exoplanet.Exoplanet exoPlanet, int robotId)
        {
            this.exoPlanet = exoPlanet;
            robotID = robotId;
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
    }
}