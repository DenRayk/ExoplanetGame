using ExoplanetGame.Application.ControlCenter;
using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Domain.ControlCenter
{
    public class PlanetMap
    {
        private PlanetMapUseCase planetMapUseCase = new PlanetMapService();

        public PlanetSize PlanetSize { get; set; }
        public Ground[,] map { get; set; }

        public PlanetMap(PlanetSize planetSize)
        {
            PlanetSize = planetSize;
            map = new Ground[planetSize.Height, planetSize.Width];
            for (int i = 0; i < planetSize.Height; i++)
            {
                for (int j = 0; j < planetSize.Width; j++)
                {
                    map[i, j] = Ground.NOTHING;
                }
            }
        }

        public void UpdateMap(Position position, Ground ground)
        {
            planetMapUseCase.UpdateMap(this, position, ground);
        }

        public Ground getGround(int x, int y)
        {
            return map[x, y];
        }

        public string GetPercentageOfExploredArea()
        {
            return planetMapUseCase.GetPercentageOfExploredArea(this);
        }
    }
}