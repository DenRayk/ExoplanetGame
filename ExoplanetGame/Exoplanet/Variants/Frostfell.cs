using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Frostfell : ExoPlanetBase
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

        public Frostfell() : base(PlanetVariant.FROSTFELL)
        {
            Weather = Weather.SNOWY;

            int randomVariant = random.Next(0, frostfellVariants.Count);
            Topography = new Topography(frostfellVariants[randomVariant]);

            robotManager = new RobotManager(this);
        }

        public override void ChangeWeather()
        {
            int weatherChange = random.Next(1, 101);

            switch (weatherChange)
            {
                case <= 50:
                    Weather = Weather.SNOWY;
                    break;

                case <= 75:
                    Weather = Weather.WINDY;
                    break;

                default:
                    Weather = Weather.SUNNY;
                    break;
            }
        }

        public override PositionResult Land(RobotBase robot, Position landPosition)
        {
            FreezeRobotIfItHasntMovedForAWhile(robot);

            return base.Land(robot, landPosition);
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

        private void FreezeRobotIfItHasntMovedForAWhile(RobotBase robot)
        {
            bool isRobotAlreadyFrozen = robotManager.robotStatusManager.RobotFreezeTracker.IsFrozen(robot);
            if (isRobotAlreadyFrozen)
                return;

            int resistanceTimeAgainstFreezing = GetFreezingTimeByWeatherConditions();

            DateTime lastMoveTime = robotManager.robotStatusManager.RobotFreezeTracker.GetLastMove(robot);
            TimeSpan timeSpanSinceLastMove = DateTime.Now - lastMoveTime;
            bool isRobotFrozen = timeSpanSinceLastMove > TimeSpan.FromSeconds(resistanceTimeAgainstFreezing);

            if (isRobotFrozen)
                RobotFreeze(robot);

            robotManager.robotStatusManager.RobotFreezeTracker.UpdateLastMove(robot);
        }

        private int GetFreezingTimeByWeatherConditions()
        {
            switch (Weather)
            {
                case Weather.WINDY:
                    return 15;

                case Weather.SNOWY:
                    return 20;

                case Weather.SUNNY:
                    return 30;

                default:
                    return 30;
            }
        }

        private void RobotFreeze(RobotBase robot)
        {
            robotManager.robotStatusManager.RobotFreezeTracker.FreezeRobot(robot);
        }
    }
}