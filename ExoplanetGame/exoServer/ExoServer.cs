using System.Net.Sockets;
using System.Net;
using ExoServer;

//Good
namespace Exoplanet.exoServer;

public class ExoServer
{
    private ExoPlanet exoPlanet;
    private Thread serverRemote;
    private TcpListener remListener;
    private static int maxRobot = 5;

    public ExoServer(int remotePort, string? topoText, string? topoObj, bool noDelay)
    {
        if (topoText != null)
        {
            exoPlanet = new ExoPlanet(topoText, true, noDelay);
        }
        else if (topoObj != null)
        {
            exoPlanet = new ExoPlanet(topoObj, false, noDelay);
        }
        else
        {
            exoPlanet = new ExoPlanet(noDelay);
        }

        serverRemote = new Thread(() =>
        {
            try
            {
                remListener = new TcpListener(IPAddress.Any, remotePort);
                remListener.Start();

                Console.WriteLine($"ExoServer: Waiting on Port {remotePort} for text-based connections");

                while (serverRemote!.ThreadState == ThreadState.Running)
                {
                    if (remListener.Pending())
                    {
                        var roboClient = remListener.AcceptTcpClient();
                        if (exoPlanet.getRobotCount() < maxRobot)
                        {
                            RobotProxy robotProxy = new RobotProxy(roboClient, exoPlanet);
                        }
                        else
                        {
                            Console.WriteLine($"Error: maxRobot={maxRobot}");
                            roboClient.Close();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                remListener?.Stop();
                Console.WriteLine("ExoServer - RemoteControl shutdown...");
            }
        });

        serverRemote.Start();
    }

    public void AdminScan()
    {
        using (var scn = new StreamReader(Console.OpenStandardInput()))
        {
            string cmd;
            while (!(cmd = scn.ReadLine()).Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                string? str;
                float factor;
                string[] token = cmd.Split(" ");
                string str1 = token[0];

                switch (str1.ToLowerInvariant())
                {
                    case "adjust":
                        factor = 4.0F;
                        if (token.Length == 2)
                            factor = float.Parse(token[1]);

                        exoPlanet.AdjustTemperature(factor);
                        break;

                    case "prop":
                        str = Environment.GetEnvironmentVariable(token[1]);
                        Console.WriteLine($"{token[0]}: {str}");
                        break;

                    case "save":
                        if (token.Length == 2)
                        {
                            exoPlanet.SaveTopoObj(token[1]);
                            break;
                        }

                        Console.WriteLine("Falsche Argumentanzahl");
                        break;

                    case "delay":
                        exoPlanet.SetNoDelay(false);
                        break;

                    case "nodelay":
                        exoPlanet.SetNoDelay(true);
                        break;
                }

                Console.WriteLine("Ungueltiges Kommando");
            }

            serverRemote.Interrupt();
            exoPlanet.Exit();
        }
    }

    private static void Main(string[] args)
    {
        Console.WriteLine("exoplanet 1.0 beta rising up ...");
        var remotePort = 7777;
        var noDelay = false;
        string topoText = null;
        string topoObj = null;

        foreach (var arg in args)
        {
            var tokens = arg.Split('=');

            if (tokens.Length == 2)
            {
                var str = tokens[0].ToLowerInvariant();

                switch (str)
                {
                    case "topoobj":
                        topoObj = tokens[1];
                        break;

                    case "topotext":
                        topoText = tokens[1];
                        break;

                    case "maxrobot":
                        maxRobot = int.Parse(tokens[1]);
                        break;

                    case "remoteport":
                        remotePort = int.Parse(tokens[1]);
                        break;

                    case "nodelay":
                        noDelay = bool.Parse(tokens[1]);
                        break;

                    default:
                        Console.WriteLine("Unbekanntes Argument: " + arg);
                        return;
                }
            }
            else
            {
                Console.WriteLine("Ungueltiges Argument: " + arg);
                return;
            }
        }

        Console.WriteLine("maxRobots=" + ExoServer.maxRobot);
        ExoServer exoSrv = new ExoServer(remotePort, topoText, topoObj, noDelay);
        exoSrv.AdminScan();
    }
}