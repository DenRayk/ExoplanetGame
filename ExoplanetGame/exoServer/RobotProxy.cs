using Exoplanet.exo;
using Exoplanet.exoServer;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ExoServer
{
    public class RobotProxy : Robot, Runnable
    {
        private const int WAIT_LAND = 0;
        private const int READY = 1;
        private int status;
        private static int nextRobotProxyID = 1;
        private int robotID;
        private Thread td;
        private Socket socket;
        private StreamReader reader;
        private StreamWriter writer;
        private ExoPlanet planet;
        private RobotStatus robStatus;

        public RobotProxy(Socket socket, ExoPlanet planet)
        {
            this.planet = planet;
            this.socket = socket;
            td = new Thread(Run);
            td.Start();
            robotID = nextRobotProxyID++;
        }

        public string GetLanderName() => $"RobotProxy{robotID}";

        public void InitRun(Planet planet, string lander, Position landPos, string userData, RobotStatus rs, StreamWriter writer)
        {
            status = 0;
            robStatus = rs;

            try
            {
                reader = new StreamReader(socket.GetStream());
                writer = new StreamWriter(socket.GetStream()) { AutoFlush = true };
            }
            catch (IOException ex)
            {
                Console.WriteLine($"RobotProxy.InitRun: {ex.Message}");
            }
        }

        public void Crash()
        {
            writer.WriteLine("crashed");
            td.Interrupt();
        }

        public void StatusChanged(RobotStatus newStatus)
        {
            writer.WriteLine($"status:{newStatus.WorkTemp}|{newStatus.Energy}|{newStatus.Message}");
        }

        public void Run()
        {
            InitRun(planet, null, null, null, planet.GetInitRobotStatus(), null);

            try
            {
                writer.WriteLine($"init:{planet.Size}");

                while (true)
                {
                    string cmd;
                    string[] token;

                    do
                    {
                        if (td.IsAlive || (cmd = reader.ReadLine()) == null)
                        {
                            return;
                        }

                        token = cmd.Split(':');
                    } while (token[0] == null);

                    try
                    {
                        Position pos = null;
                        string var4;
                        Measure m;

                        switch (var4 = token[0])
                        {
                            case "charge":
                                if (token.Length == 2)
                                {
                                    int duration = int.Parse(token[1]);
                                    RobotStatus rs = planet.Charge(this, duration);
                                    if (rs != null)
                                    {
                                        writer.WriteLine($"charged:{rs.WorkTemp}|{rs.Energy}|{rs.Message}");
                                    }
                                }
                                else
                                {
                                    Error($"missing arguments: {cmd}");
                                }
                                continue;

                            case "getpos":
                                pos = planet.GetPosition(this);
                                if (pos != null)
                                {
                                    writer.WriteLine($"pos:{pos}");
                                }
                                continue;

                            case "rotate":
                                Rotation r = Enum.Parse<Rotation>(token[1]);
                                Direction d = planet.Rotate(this, r);
                                if (d != null)
                                {
                                    writer.WriteLine($"rotated:{d}");
                                }
                                continue;

                            case "exit":
                                planet.Remove(this);
                                continue;

                            case "land":
                                if (status == 0)
                                {
                                    if (token.Length >= 2)
                                    {
                                        pos = Position.Parse(token[1]);
                                        if (pos != null)
                                        {
                                            m = planet.Land(this, pos);
                                            if (m != null)
                                            {
                                                writer.WriteLine($"landed:{m}");
                                                status = 1;
                                            }
                                        }
                                        else
                                        {
                                            Error($"invalid argument: {cmd}");
                                        }
                                    }
                                    else
                                    {
                                        Error($"missing arguments: {cmd}");
                                    }
                                }
                                else
                                {
                                    Error($"illegal state: {cmd}");
                                }
                                continue;

                            case "move":
                                pos = planet.Move(this);
                                if (pos != null)
                                {
                                    writer.WriteLine($"moved:{pos}");
                                }
                                continue;

                            case "scan":
                                m = planet.Scan(this);
                                writer.WriteLine($"scanned:{m}");
                                continue;
                        }

                        Error($"illegal operation:{cmd}");
                    }
                    catch (ArgumentException ex)
                    {
                        ex.printStackTrace();
                        Error($"ArgumentException:{cmd}");
                    }
                }
            }
            catch (IOException ex)
            {
                ex.printStackTrace();
                planet.Remove(this);
            }
            finally
            {
                writer.Close();

                try
                {
                    reader.Close();
                    socket.Close();
                }
                catch (IOException ex)
                {
                    Console.WriteLine("BoosterHandler closing");
                    ex.printStackTrace();
                }
            }
        }

        private void Error(string s)
        {
            Console.WriteLine($"sending error({GetLanderName()}): {s}");
            writer.WriteLine($"error:{s}");
        }
    }
}