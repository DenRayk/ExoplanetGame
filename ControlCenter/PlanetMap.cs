using ControlCenter.exo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlCenter
{
    internal class PlanetMap
    {
        public PlanetSize planetSize { get; set; }
        public Ground[,] map { get; set; }

        public PlanetMap(PlanetSize planetSize)
        {
            this.planetSize = planetSize;
            map = new Ground[(int)planetSize.Height, (int)planetSize.Width];
            for (int i = 0; i < (int)planetSize.Height; i++)
            {
                for (int j = 0; j < (int)planetSize.Width; j++)
                {
                    map[i, j] = Ground.NICHTS;
                }
            }
        }

        public void printMap()
        {
            for (int i = 0; i < (int)planetSize.Height; i++)
            {
                for (int j = 0; j < (int)planetSize.Width; j++)
                {
                    GroundPrinter.printGround(map[i, j]);
                }
                Console.WriteLine();
            }
        }

        public void updateMap(int x, int y, Ground ground)
        {
            map[x, y] = ground;
            printMap();
        }

        public Ground getGround(int x, int y)
        {
            return map[x, y];
        }
    }
}