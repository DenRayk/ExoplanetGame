using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Tropica : ExoplanetBase
    {
        public Tropica() : base(PlanetVariant.TROPICA)
        {
            Weather = Weather.SUNNY;

            //Grandmaster level
            Topography = new Topography(new string[]
            {
                "GGWWPMWPGGGPWWWWRRGGWSPGGPSSSG",
                "GGWWSMWWWPMMPGGSWPWPWMGPGMMPSS",
                "GMMWSPPRWWWWMGGSSWMMWSMGMWWPRM",
                "GPMWSSRPGMMWWRRWSPMWWWSPPPWRPM",
                "SGPWGSMMMMPMWWPWWWWWSWWPPGWWGM",
                "SSWWGSPPGGMWGWWWWWWWSMWSMGPWMM",
                "GGSWGMMPGGGMPGSPRGWWSSWSSGMWMP",
                "SSPWWPMMGGGPPPMWMGWPSMWPSPMWWM",
                "MSPWWRMGGPMPPSMWGGWPSMWSSWPMWW",
                "RRMRWRPGGPWRRGSMGGWPSMSRRWGGRW"
            });
            robotManager = new RobotManager(this);
        }

        public override void ChangeWeather()
        {
            Random random = new Random();
            int weatherChange = random.Next(1, 101);

            if (weatherChange <= 60)
            {
                Weather = Weather.SUNNY;
            }
            else if (weatherChange <= 90)
            {
                Weather = Weather.RAINY;
            }
            else if (weatherChange <= 95)
            {
                Weather = Weather.CLOUDY;
            }
            else
            {
                Weather = Weather.FOGGY;
            }
        }
    }
}