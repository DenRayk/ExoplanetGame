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
        private Dictionary<Robot, Position> robots = new Dictionary<Robot, Position>();
        private PlanetSize planetSize;
        private Measure[][] topography;

        public Exoplanet()
        {
            string[] topography = new string[]
            {
                "GSSWPFSGGL",
                "SPW4PSFFLL",
                "SGWPMSFLLF",
                "SSWWMGSFFG",
                "FGSWWSSGFG",
                "FFWWGGSGFF"
            };

            initfromString(topography);
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

        public Measure Land(Robot robot, Position landPosition)
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
            if (x < 0 || y < 0 || x >= planetSize.Width || y >= planetSize.Height) return new Measure();

            Measure m = topography[y][x];
            return new Measure(m.Ground);
        }

        private bool CheckPosition(Robot robot, Position landPosition)
        {
            throw new NotImplementedException();
        }

        public Position GetPosition(Robot robot)
        {
            throw new NotImplementedException();
        }

        public Position Move(Robot robot)
        {
            throw new NotImplementedException();
        }

        public Direction? Rotate(Robot robot, Rotation rotation)
        {
            throw new NotImplementedException();
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