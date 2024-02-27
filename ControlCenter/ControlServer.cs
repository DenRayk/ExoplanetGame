using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ControlCenter
{
    internal class ControlServer : TcpListener
    {
        private readonly int maxRobots = 5;
        private static readonly int port = 9999;
        private int robotCount = 0;


        public ControlServer() : base(IPAddress.Any, port)
        {
            Start();

            while (Active)
            {
                if (robotCount < maxRobots)
                {
                    TcpClient client = AcceptTcpClient();
                    Console.WriteLine("Robot connected.");
                    robotCount++;
                }
                else
                {
                    Console.WriteLine("Max robots reached.");
                }
            }
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Control Center rising up...");

            ControlServer controlServer = new();
            controlServer.Start();
        }
    }
}
