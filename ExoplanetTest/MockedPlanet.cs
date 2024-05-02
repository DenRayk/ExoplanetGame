using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGameTest
{
    internal class MockedPlanet : ExoPlanetBase
    {
        public MockedPlanet()
        {
            Topography = new Topography(new string[]
            {
                "FFFFFFFF",
                "FFFFFFFF",
            });
        }
    }
}