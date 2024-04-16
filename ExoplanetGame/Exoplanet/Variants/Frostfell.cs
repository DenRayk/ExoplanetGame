using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Frostfell : ExoplanetBase
    {
        public Frostfell() : base(PlanetVariant.Frostfell)
        {
            Weather = Weather.SNOWY;

            //Advanced level
            Topography = new Topography(new string[]
            {
                "NNNWWWIIIINNIIWWWIIIN",
                "NNNNWWIINNNNIIIWWIINN",
                "WNNNWWWIIINNNNNIWWWIN",
                "WNNNNWWWIINNNNNIIWWIN",
                "WNNWWWWWWIINNNNIIWIIN",
                "WWWNNIIIWWWWIIINNNNII",
                "WWWWNNNNNIIIWWWWIIIII",
            });
            robotManager = new RobotManager(this);
        }


        public override void ChangeWeather()
        {
            Random random = new Random();
            int weatherChange = random.Next(1, 101);

            if (weatherChange <= 50)
            {
                Weather = Weather.SNOWY;
            }
            else
            {
                Weather = Weather.WINDY;
            }
        }
    }
}
