using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;

namespace Exoplanet
{
    internal class ExoServer : TcpListener
    {
        private readonly int maxRobots = 5;
        private readonly Exoplanet exoPlanet;
        private static readonly int port = 9999;

        public ExoServer() : base(IPAddress.Any, port)
        {
            exoPlanet = new Exoplanet();

            Start();

            while (Active)
            {
                if (exoPlanet.getRobotCount() < maxRobots)
                {
                    TcpClient client = AcceptTcpClient();
                    Console.WriteLine("Robot connected.");
                    ExoProxy robotProxy = new(client, exoPlanet);
                }
                else
                {
                    Console.WriteLine("Max robots reached.");
                }
            }
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Exoplanet rising up...");

            ExoServer exoServer = new();
            exoServer.Start();
        }
    }
}