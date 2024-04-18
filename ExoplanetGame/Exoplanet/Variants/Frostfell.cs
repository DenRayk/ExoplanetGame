using ExoplanetGame.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Robot.RobotResults;

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

        public override PositionResult Move(RobotBase robot)
        {
            FreezeRobotIfItHasntMovedForAWhile(robot);

            if (!robotManager.robotStatusManager.RobotFreezeTracker.IsFrozen(robot))
            {
                return base.Move(robot);
            }

            return new PositionResult()
            {
                HasRobotSurvived = true,
                IsSuccess = false,
                Message = "Robot is frozen and cannot move anymore.",
                Position = robot.RobotInformation.Position
            };
        }

        public override RotationResult Rotate(RobotBase robot, Rotation rotation)
        {
            RotationResult rotationResult = new();

            FreezeRobotIfItHasntMovedForAWhile(robot);

            if (robotManager.robotStatusManager.RobotFreezeTracker.IsFrozen(robot))
            {
                rotationResult.IsSuccess = false;
                rotationResult.HasRobotSurvived = true;
                rotationResult.Message = "Robot is frozen and cannot rotate anymore.";
                rotationResult.Direction = robot.RobotInformation.Position.Direction;

                return rotationResult;
            }

            return base.Rotate(robot, rotation);
        }

        public void FreezeRobotIfItHasntMovedForAWhile(RobotBase robot)
        {
            if (robotManager.robotStatusManager.RobotFreezeTracker.IsFrozen(robot))
            {
                return;
            }

            if (DateTime.Now - robotManager.robotStatusManager.RobotFreezeTracker.GetLastMove(robot) > TimeSpan.FromSeconds(60))
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