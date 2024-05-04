using ExoplanetGame.Application.ControlCenter;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Application.Robot;

namespace ExoplanetGame.Application
{
    public class UCCollection
    {
        public UCCollection()
        {
            UcCollectionControlCenter = new UCCollectionControlCenter();
        }

        public UCCollectionControlCenter UcCollectionControlCenter { get; }
        public UCCollectionRobot UcCollectionRobot { get; set; }

        public void Init(ExoplanetService exoplanetService)
        {
            UcCollectionRobot = new UCCollectionRobot(exoplanetService);
        }
    }
}