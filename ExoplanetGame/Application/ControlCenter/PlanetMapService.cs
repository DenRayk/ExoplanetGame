using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Application.ControlCenter
{
    internal class PlanetMapService : PlanetMapUseCase
    {
        public void UpdateMap(PlanetMap map, Position position, Ground ground)
        {
            map.UpdateMap(position, ground);
        }

        public string GetPercentageOfExploredArea(PlanetMap map)
        {
            int totalArea = map.PlanetSize.Height * map.PlanetSize.Width;
            int exploredArea = 0;

            for (int rowIndex = 0; rowIndex < map.PlanetSize.Height; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < map.PlanetSize.Width; columnIndex++)
                {
                    bool isAreaExplored = map.getGround(rowIndex, columnIndex) != Ground.NOTHING;

                    if (isAreaExplored)
                    {
                        exploredArea++;
                    }
                }
            }

            double exploredAreaPercentage = (double)exploredArea / totalArea * 100;
            string formattedPercentage = exploredAreaPercentage.ToString("0.00") + "%";

            return formattedPercentage;
        }
    }
}