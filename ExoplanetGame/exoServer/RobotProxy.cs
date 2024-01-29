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
        private int status;
        private static int nextRobotProxyID = 1;
        private int robotID;
        private Thread td;
        private TcpClient tcpClient;
        private StreamReader inStream;
        private StreamWriter outStream;
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

        public void InitRun(Planet planet, string lander, Position landPos, string userData, RobotStatus rs, StreamWriter outStream)
        {
            this.status = 0;
            this.robStatus = rs;

            try
            {
                this.inStream = new StreamReader(this.tcpClient.GetStream());
                this.outStream = new StreamWriter(this.tcpClient.GetStream()) { AutoFlush = true };
            }
            catch (IOException ex)
            {
                Console.WriteLine("RobotProxy.InitRun: " + ex.Message);
            }
        }

        public void Crash()
        {
            outStream.WriteLine("crashed");
            td.Interrupt();
        }

        public void StatusChanged(RobotStatus newStatus)
        {
            outStream.WriteLine($"status:{newStatus.GetWorkTemp()}|{newStatus.GetEnergy()}|{newStatus.GetMessage()}");
        }

        public void Dispose()
        {
            outStream?.Close();
            inStream?.Close();
            tcpClient?.Close();
        }

        private void Run()
        {
            InitRun(planet, null, null, null, planet.GetInitRobotStatus(), outStream);

            try
            {
                outStream.WriteLine("init:" + planet.GetSize());

                string cmd;

                while (td.ThreadState == ThreadState.Running && (cmd = inStream.ReadLine()) != null)
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
                                            outStream.WriteLine($"charged:{rs.GetWorkTemp()} | {rs.GetEnergy()} | {rs.GetMessage()}");
                                    }
                                    else
                                    {
                                        Error("missing arguments: " + cmd);
                                    }
                                    continue;

                                case "getpos":
                                    Position posi = planet.GetPosition(this);
                                    if (posi != null)
                                        outStream.WriteLine("pos:" + posi);
                                    continue;

                                case "rotate":
                                    Rotation r = Enum.Parse<Rotation>(token[1], true);
                                    Direction? d = planet.Rotate(this, r);
                                    if (d != null)
                                        outStream.WriteLine("rotated:" + d.ToString());
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
                                                    outStream.WriteLine("landed:" + measure.ToString());
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
                                        outStream.WriteLine("moved:" + newPos.ToString());
                                    continue;

                                case "scan":
                                    Measure m = planet.Scan(this);
                                    outStream.WriteLine("scanned:" + m.ToString());
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
                outStream.Close();
                inStream.Close();
                tcpClient.Close();
            }
        }

        private void Error(string s)
        {
            Console.WriteLine($"sending error({GetLanderName()}): {s}");
            outStream.WriteLine("error:" + s);
        }
    }
}