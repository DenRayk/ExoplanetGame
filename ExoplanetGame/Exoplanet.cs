using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exoplanet.exo;

namespace Exoplanet
{
    internal class Exoplanet : Planet
    {
        private Dictionary<Robot, Position> robots = new();
        private PlanetSize planetSize;
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
            planetSize = new PlanetSize(strings[0].Length, strings.Length);
            topography = new Measure[planetSize.Height][];

            for (int y = 0; y < planetSize.Height; y++)
            {
                topography[y] = new Measure[planetSize.Width];
                for (int x = 0; x < planetSize.Width; x++)
                {
                    topography[y][x] = new Measure(groundFromChar(strings[y][x]));
                }
            }
        }

        public int getRobotCount()
        {
            return robots.Count;
        }

        public PlanetSize getPlanetSize()
        {
            return planetSize;
        }

        public Measure Land(Robot robot, Position? landPosition)
        {
            if (!robots.ContainsKey(robot) && CheckPosition(robot, landPosition))
            {
                robots.Add(robot, landPosition);

                return GetMeasure(landPosition.X, landPosition.Y);
            }
            robot.Crash();
            return null;
        }

        private Measure GetMeasure(int x, int y)
        {
            if (x < 0 || y < 0 || x >= planetSize.Width || y >= planetSize.Height) return new Measure(Ground.NICHTS);

            Measure m = topography[y][x];
            return new Measure(m.Ground);
        }

        private bool CheckPosition(Robot robot, Position? landPosition)
        {
            if (landPosition != null && topography[landPosition.Y][landPosition.X].Ground == Ground.NICHTS)
            {
                return false;
            }

            foreach (Robot r in robots.Keys)
            {
                if (r != robot && this.robots[r].Equals(landPosition))
                {
                    return false;
                }
            }
            return true;
        }

        public Position GetPosition(Robot robot)
        {
            throw new NotImplementedException();
        }

        public Position Move(Robot robot)
        {
            Position robotPosition = robots[robot];
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

            if (CheckPosition(robot, newPosition))
            {
                robots[robot] = newPosition;
                return newPosition;
            }
            robot.Crash();
            return null;
        }

        public Direction? Rotate(Robot robot, Rotation rotation)
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

        public Measure Scan(Robot robot)
        {
            throw new NotImplementedException();
        }

        public PlanetSize GetSize()
        {
            throw new NotImplementedException();
        }

        public void Remove(Robot robot)
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