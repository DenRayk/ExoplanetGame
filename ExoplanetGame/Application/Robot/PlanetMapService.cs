using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Application.Robot
{
    internal class PlanetMapService : PlanetMapUseCase
    {
        private readonly Domain.ControlCenter.ControlCenter controlCenter;

        public PlanetMapService()
        {
            controlCenter = Domain.ControlCenter.ControlCenter.GetInstance();
        }

        public void UpdateMap(Position position, Ground ground)
        {
            controlCenter.PlanetMap.map[position.Y, position.X] = ground;
        }

        public string GetPercentageOfExploredArea()
        {
            int totalArea = controlCenter.PlanetMap.PlanetSize.Height * controlCenter.PlanetMap.PlanetSize.Width;
            int exploredArea = 0;

            for (int rowIndex = 0; rowIndex < controlCenter.PlanetMap.PlanetSize.Height; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < controlCenter.PlanetMap.PlanetSize.Width; columnIndex++)
                {
                    bool isAreaExplored = controlCenter.PlanetMap.GetGround(rowIndex, columnIndex) != Ground.NOTHING;

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