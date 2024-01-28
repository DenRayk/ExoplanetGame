using Exoplanet.exo;
using Exoplanet.exoServer;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ExoServer
{
    public class RobotProxy : Robot, IDisposable
    {
        private const int WAIT_LAND = 0;
        private const int READY = 1;

        private static int nextRobotProxyID = 1;

        private int status;
        private int robotID;
        private Thread td;
        private TcpClient tcpClient;
        private StreamReader reader;
        private StreamWriter writer;
        private ExoPlanet planet;
        private RobotStatus robStatus;

        public RobotProxy(TcpClient tcpClient, ExoPlanet planet)
        {
            this.planet = planet;
            this.tcpClient = tcpClient;
            this.td = new Thread(Run);
            this.td.Start();
            this.robotID = nextRobotProxyID++;
        }

        public string GetLanderName() => "RobotProxy" + robotID;

        public void InitRun(Planet planet, string lander, Position landPos, string userData, RobotStatus rs)
        {
            status = 0;
            robStatus = rs;

            try
            {
                reader = new StreamReader(tcpClient.GetStream());
                writer = new StreamWriter(tcpClient.GetStream()) { AutoFlush = true };
            }
            catch (IOException e)
            {
                Console.WriteLine("RobotProxy.InitRun: " + e.Message);
            }
        }

        public void Crash()
        {
            writer.WriteLine("crashed");
            td.Interrupt();
        }

        public void StatusChanged(RobotStatus newStatus)
        {
            writer.WriteLine($"status:{newStatus.GetWorkTemp()}|{newStatus.GetEnergy()}|{newStatus.GetMessage()}");
        }

        public void Dispose()
        {
            writer?.Close();
            reader?.Close();
            tcpClient?.Close();
        }

        private void Run()
        {
            InitRun(planet, null, null, null, planet.GetInitRobotStatus());

            try
            {
                writer.WriteLine("init:" + planet.GetSize());

                string cmd;

                while (td.ThreadState == ThreadState.Running && (cmd = reader.ReadLine()) != null)
                {
                    var token = cmd.Split(':');

                    if (token[0] != null)
                    {
                        try
                        {
                            switch (token[0].ToLowerInvariant())
                            {
                                case "charge":
                                    if (token.Length == 2)
                                    {
                                        int duration = int.Parse(token[1]);
                                        RobotStatus rs = planet.Charge(this, duration);
                                        if (rs != null)
                                            writer.WriteLine($"charged:{rs.GetWorkTemp()} | {rs.GetEnergy()} | {rs.GetMessage()}");
                                    }
                                    else
                                    {
                                        Error("missing arguments: " + cmd);
                                    }
                                    continue;

                                case "getpos":
                                    Position posi = planet.GetPosition(this);
                                    if (posi != null)
                                        writer.WriteLine("pos:" + posi);
                                    continue;

                                case "rotate":
                                    Rotation r = Enum.Parse<Rotation>(token[1], true);
                                    Direction? d = planet.Rotate(this, r);
                                    if (d != null)
                                        writer.WriteLine("rotated:" + d.ToString());
                                    continue;

                                case "exit":
                                    planet.Remove(this);
                                    continue;

                                case "land":
                                    if (status == 0)
                                    {
                                        if (token.Length >= 2)
                                        {
                                            Position pos = Position.Parse(token[1]);
                                            if (pos != null)
                                            {
                                                Measure measure = planet.Land(this, pos);
                                                if (measure != null)
                                                {
                                                    writer.WriteLine("landed:" + measure.ToString());
                                                    status = 1;
                                                }
                                                continue;
                                            }
                                            else
                                            {
                                                Error("invalid argument: " + cmd);
                                            }
                                        }
                                        else
                                        {
                                            Error("missing arguments: " + cmd);
                                        }
                                    }
                                    else
                                    {
                                        Error("illegal state: " + cmd);
                                    }
                                    continue;

                                case "move":
                                    Position newPos = planet.Move(this);
                                    if (newPos != null)
                                        writer.WriteLine("moved:" + newPos.ToString());
                                    continue;

                                case "scan":
                                    Measure m = planet.Scan(this);
                                    writer.WriteLine("scanned:" + m.ToString());
                                    continue;
                            }

                            Error("illegal operation:" + cmd);
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine(e.ToString());
                            Error("ArgumentException:" + cmd);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
                planet.Remove(this);
            }
            finally
            {
                writer.Close();
                reader.Close();
                tcpClient.Close();
            }
        }

        private void Error(string s)
        {
            Console.WriteLine($"sending error({GetLanderName()}): {s}");
            writer.WriteLine("error:" + s);
        }
    }
}