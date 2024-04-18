using ExoplanetGame.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Frostfell : ExoplanetBase
    {
        private Random random = new();

        private readonly List<string[]> frostfellVariants = new()
        {
            new string[]
            {
                "RRNWWWIIIIRNIIWWWIIIN",
                "NNNNWWIINNRRIIIWWIINN",
                "WNNNWWWIIINRNNNIWWWIN",
                "WNNNNWWWIINNNNNIIWWIN",
                "WNNWWWWWWIINNNNIIWIIR",
                "WWWNNIIIWWWWIIINNNNRR",
                "WWWWNNNNNIIWWWWWIIRRR",
                "WWWNNNNIIIIIIIINNNNRR"
            },

            new string[]
            {
                "RNWWWWWWINIIWWNIWWWII",
                "RNNNNWWWINIIIWIIIIWWW",
                "RRNNNWWWRRNNIWIIIIWWI",
                "RRNNWNWNNRRNNWWIINWII",
                "RWWNWNNNIIRNNIWINNWNI",
                "WWWWWINNIIRNNIWINIWII",
                "IIWWWINNWINNNIWINWWII",
                "IIIWWINIWWIIINWNWWNII"
            },

            new string[]
            {
                "WWWWNNNNNNRRWWIIINIII",
                "WWWNNNNIWNNNWWWIINIIN",
                "IIRNIIWWWNNNNWWWINNNN",
                "NNRRIIIWWNNWWWWWWWNNN",
                "IINRRRNIWWWNNIIINWWNI",
                "IINNNNNIWWWWNNNNNNNNI",
                "WIINNNNIWWWNNNNINIINI",
                "WWWWIIINIIRNIIWWNIINI"
            },

            new string[]
            {
                "WIINNNNIIINRRRNIIIIII",
                "WWWWIIINIINNNNNIIIIII",
                "NIIWWWWWWIINNNNIIIWWI",
                "IIIIIIINWWWWIIINIWWNI",
                "WIRRNNNWNIIWWWWWWIINI",
                "WIINNIWWIRIIIIINNNRNN",
                "WWWWWWWNWRRINNNWNRRNN",
                "IWWINIIIWIINNIWWNNNNN"
            },

            new string[]
            {
                "NNNNNINIIIWWNIWWNNWNR",
                "NNRRNINWIIIWIRRIIWWNN",
                "NRRRRNWWNNIWIIRIIINRR",
                "RWWWRNNNRNNWWIINIINNN",
                "WWIIRNRNRNNIWINNWIINN",
                "WWWWRRRNRNNIWINIWWWWI",
                "RWRWRNNNNNNIWINWNIIWW",
                "RRRRNNNNIIINWNWWIRIII"
            }
        };

        public Frostfell() : base(PlanetVariant.Frostfell)
        {
            Weather = Weather.SNOWY;

            int randomVariant = random.Next(0, frostfellVariants.Count);
            Topography = new Topography(frostfellVariants[randomVariant]);

            robotManager = new RobotManager(this);
        }

        public override void ChangeWeather()
        {
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

        public override Position Move(RobotBase robot)
        {
            FreezeRobotIfItHasntMovedForAWhile(robot);

            if (!robotManager.robotStatusManager.RobotFreezeTracker.IsFrozen(robot))
            {
                return base.Move(robot);
            }

            Console.WriteLine("Robot is frozen and cannot move anymore.");
            return null;
        }

        public override Direction Rotate(RobotBase robot, Rotation rotation)
        {
            FreezeRobotIfItHasntMovedForAWhile(robot);
            return base.Rotate(robot, rotation);
        }

        public void FreezeRobotIfItHasntMovedForAWhile(RobotBase robot)
        {
            if (robotManager.robotStatusManager.RobotFreezeTracker.IsFrozen(robot))
            {
                return;
            }

            if (DateTime.Now - robotManager.robotStatusManager.RobotFreezeTracker.GetLastMove(robot) > TimeSpan.FromSeconds(10))
            {
                RobotFreeze(robot);
            }

            robotManager.robotStatusManager.RobotFreezeTracker.UpdateLastMove(robot);
        }

        public void RobotFreeze(RobotBase robot)
        {
            robotManager.robotStatusManager.RobotFreezeTracker.FreezeRobot(robot);
        }
    }
}