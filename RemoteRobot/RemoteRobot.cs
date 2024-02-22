using System.Net.Sockets;
using System.Text;

namespace RemoteRobot
{
    internal class RemoteRobot
    {
        private static void Main(string[] args)
        {
            RemoteRobotServer remoteRobotServer = new();
            remoteRobotServer.Start();
        }
    }
}