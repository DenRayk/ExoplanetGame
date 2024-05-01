using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Exoplanet.Variants;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

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