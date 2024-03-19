using Exoplanet.exo;
using ExoplanetGame.Models;

namespace ExoplanetGame.Exoplanet
{
    public class Exoplanet : IPlanet
    {
        private Dictionary<RemoteRobot.RemoteRobot, Position?> robots = new();
        public PlanetSize PlanetSize { get; set; }
        private Measure[][] topography;

        public Exoplanet()
        {
            string[] topographyStr = {
                "GSSWPFSGGL",
                "SPW4PSFFLL",
                "SGWPMSFLLF",
                "SSWWMGSFFG",
                "FGSWWSSGFG",
                "FFWWGGSGFF"
            };

            initfromString(topographyStr);
        }

        private void initfromString(string[] strings)
        {
            PlanetSize = new PlanetSize(strings[0].Length, strings.Length);
            topography = new Measure[PlanetSize.Height][];

            for (int y = 0; y < PlanetSize.Height; y++)
            {
                topography[y] = new Measure[PlanetSize.Width];
                for (int x = 0; x < PlanetSize.Width; x++)
                {
                    topography[y][x] = new Measure(groundFromChar(strings[y][x]));
                }
            }
        }

        public int getRobotCount()
        {
            return robots.Count;
        }

        public void RemoveRobot(RemoteRobot.RemoteRobot remoteRobot)
        {
            robots.Remove(remoteRobot);
        }

        public bool Land(RemoteRobot.RemoteRobot remoteRobot, Position landPosition)
        {
            if (!robots.ContainsKey(remoteRobot) && CheckPosition(remoteRobot, landPosition))
            {
                robots.Add(remoteRobot, landPosition);

                if (landPosition != null) return true;
            }
            RemoveRobot(remoteRobot);
            return false;
        }

        private Measure GetMeasure(int x, int y)
        {
            if (x < 0 || y < 0 || x >= PlanetSize.Width || y >= PlanetSize.Height) return new Measure(Ground.NICHTS);

            Measure m = topography[y][x];
            return new Measure(m.Ground);
        }

        private bool CheckPosition(RemoteRobot.RemoteRobot robot, Position position)
        {
            if (!CheckIfPositionInBounds(position))
            {
                return false;
            }

            if (topography[position.Y][position.X].Ground == Ground.NICHTS)
            {
                return false;
            }

            foreach (RemoteRobot.RemoteRobot r in robots.Keys)
            {
                if (r != robot && robots[r].Equals(position))
                {
                    return false;
                }
            }
            return true;
        }

        private bool CheckIfPositionInBounds(Position position)
        {
            return position.X >= 0 && position.X < PlanetSize.Width && position.Y >= 0 && position.Y < PlanetSize.Height;
        }

        public Position GetPosition(RemoteRobot.RemoteRobot robot)
        {
            return robots[robot];
        }

        public Position Move(RemoteRobot.RemoteRobot remoteRobot)
        {
            Position robotPosition = robots[remoteRobot];
            Position newPosition = new Position(robotPosition.X, robotPosition.Y, robotPosition.Direction);

            switch (robotPosition.Direction)
            {
                case Direction.NORTH:
                    newPosition.Y--;
                    break;

                case Direction.EAST:
                    newPosition.X++;
                    break;

                case Direction.SOUTH:
                    newPosition.Y++;
                    break;

                case Direction.WEST:
                    newPosition.X--;
                    break;
            }

            if (CheckPosition(remoteRobot, newPosition))
            {
                robots[remoteRobot] = newPosition;
                return newPosition;
            }
            RemoveRobot(remoteRobot);
            return null;
        }

        public Direction? Rotate(RemoteRobot.RemoteRobot robot, Rotation rotation)
        {
            Position robotPosition = robots[robot];

            if (rotation == Rotation.LEFT)
            {
                robotPosition.Direction = robotPosition.Direction switch
                {
                    Direction.NORTH => Direction.WEST,
                    Direction.WEST => Direction.SOUTH,
                    Direction.SOUTH => Direction.EAST,
                    Direction.EAST => Direction.NORTH,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            else
            {
                robotPosition.Direction = robotPosition.Direction switch
                {
                    Direction.NORTH => Direction.EAST,
                    Direction.EAST => Direction.SOUTH,
                    Direction.SOUTH => Direction.WEST,
                    Direction.WEST => Direction.NORTH,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            return robotPosition.Direction;
        }

        public Measure Scan(RemoteRobot.RemoteRobot robot)
        {
            return GetMeasure(robots[robot].X, robots[robot].Y);
        }

        public void Remove(RemoteRobot.RemoteRobot robot)
        {
            Console.WriteLine($"Remove: {robot.GetLanderName()}");
            robots.Remove(robot);
        }

        private Ground groundFromChar(char g)
        {
            return g switch
            {
                'W' => Ground.WASSER,
                'F' => Ground.FELS,
                'S' => Ground.SAND,
                'G' => Ground.GEROELL,
                'P' => Ground.PFLANZEN,
                'M' => Ground.MORAST,
                'L' => Ground.LAVA,
                _ => Ground.NICHTS
            };
        }
    }
}