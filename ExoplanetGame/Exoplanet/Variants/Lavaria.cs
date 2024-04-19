using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Lavaria : ExoplanetBase
    {
        private static readonly int volcanicEruptionChance = 5;

        private readonly List<string[]> lavariaVariants = new()
        {
            new string[]
            {
                "GLLLLLGGGGGLLLGGGGGGG",
                "LLGGGLLLLLLLLLGGGGLLL",
                "GGGGGLLLLLLLLLLLGGGLL",
                "LLLLLLLLGGGGGGGLLLGGL",
                "GGGLLGGRRRRRRRRLLLLRL",
                "RRRGGGGGGLLLRRRRRRRRR",
                "RRRRRRRRRGGGGGGGRRRRR"
            },

            new string[]
            {
                "GLGLGRRGLLLRGRGGLGRRG",
                "LLGLGRRRLLGRGRGGLLLRG",
                "LGGLGRRRLLRRLGGGGLLRR",
                "LGGLLGRGLRRRLGGGGLLRR",
                "LGGLLGRLLRRRLGGLGGLRR",
                "LLLLGGRLLLGRRGGLLGRRR",
                "GLLLGGRLLLGRRGGLLLLRR"
            },

            new string[]
            {
                "LLLLLLLLGGLLGRLLLGRLG",
                "GGGLLGGLGGLLGLLLLGRRG",
                "RRRGGGGLLLLGGRLLLGRRG",
                "RRRRRRRGLLLGGRGGLGRRG",
                "GRRRLLRGLLLRGRGGLLLRG",
                "LLLRLLLGLLGRGRGGGLLRR",
                "LLLRRLLGLLGRLGGGGLLRR"
            },

            new string[]
            {
                "LGGRGLLLGGRRLLLLLLLGG",
                "LLLRGLLLRGGGGGLLGGRRR",
                "GGLLGLLGRGLLLLGGGGGGL",
                "GGLLGLLGRLLLLLRLRRRRG",
                "LLLLLGGLLGLLGGLLLGGGG",
                "LLGGLGGLLGGRRRLLLGGGG",
                "GGGGLLLLGGGGGLLLLLLGG"
            },

            new string[]
            {
                "RRRLLLRLGGLGRRLGLLGRG",
                "LLLLGGRLGGLLGRLGLLGRL",
                "LLLLLRRLGGLLGRRRGRLLL",
                "LRRRLRRLLLLGGRRRGLLLL",
                "RRGGLGRGLLLGGRRRGRLLL",
                "RRRRLGRGLLLRGRLGGRGGL",
                "LRLRLGRGLLGRGRLRGRGGL"
            }
        };

        private Random random = new();

        public Lavaria() : base(PlanetVariant.LAVARIA)
        {
            Weather = Weather.FOGGY;

            int randomVariant = random.Next(0, lavariaVariants.Count);
            Topography = new Topography(lavariaVariants[randomVariant]);

            robotManager = new RobotManager(this);
        }

        public override void ChangeWeather()
        {
            int weatherChange = random.Next(1, 101);

            if (weatherChange <= 25)
            {
                Weather = Weather.SUNNY;
            }
            else if (weatherChange <= 50)
            {
                Weather = Weather.FOGGY;
            }
            else if (weatherChange <= 75)
            {
                Weather = Weather.ASH_IN_THE_AIR;
            }
            else
            {
                int cloudyOrWindy = random.Next(1, 3);
                Weather = (cloudyOrWindy == 1) ? Weather.CLOUDY : Weather.WINDY;
            }
        }

        public bool DoesVolcanicEruptionHappen()
        {
            int randomEruption = random.Next(1, 101);

            if (randomEruption <= volcanicEruptionChance)
            {
                return true;
            }

            return false;
        }

        public override PositionResult GetRobotPosition(RobotBase robot)
        {
            if (HandleVolcanicEruption(robot, out PositionResult positionResult))
                return positionResult;

            return base.GetRobotPosition(robot);
        }

        public override LoadResult LoadEnergy(RobotBase robot, int seconds)
        {
            if (HandleVolcanicEruption(robot, out LoadResult loadResult))
                return loadResult;

            return base.LoadEnergy(robot, seconds);
        }

        public override PositionResult Move(RobotBase robot)
        {
            if (HandleVolcanicEruption(robot, out PositionResult positionResult))
                return positionResult;

            return base.Move(robot);
        }

        public override RotationResult Rotate(RobotBase robot, Rotation rotation)
        {
            if (HandleVolcaincEruption(robot, out RotationResult rotationResult))
                return rotationResult;

            return base.Rotate(robot, rotation);
        }

        public override ScanResult Scan(RobotBase robot)
        {
            if (HandleVolcaincEruption(robot, out ScanResult scanResult))
                return scanResult;

            return base.Scan(robot);
        }

        public override ScoutScanResult ScoutScan(RobotBase robot)
        {
            if (HandleVolcanicEruption(robot, out ScoutScanResult scoutScanResult))
                return scoutScanResult;

            return base.ScoutScan(robot);
        }

        private bool IsRobotDestroyedRandomly(RobotBase robot)
        {
            Random rand = new();
            int randomRobot = rand.Next(0, 100);

            if (randomRobot <= 50)
            {
                return true;
            }

            return false;
        }

        private bool HandleVolcaincEruption(RobotBase robot, out RotationResult rotationResult)
        {
            rotationResult = new();

            if (VolcanicEruption(robot))
            {
                rotationResult = new RotationResult()
                {
                    IsSuccess = false,
                    HasRobotSurvived = false,
                    Message = $"Robot {robot.RobotInformation.RobotID} is destroyed by volcanic eruption."
                };
                return true;
            }

            return false;
        }

        private bool HandleVolcaincEruption(RobotBase robot, out ScanResult scanResult)
        {
            scanResult = new();

            if (VolcanicEruption(robot))
            {
                scanResult = new ScanResult()
                {
                    IsSuccess = false,
                    HasRobotSurvived = false,
                    Message = $"Robot {robot.RobotInformation.RobotID} is destroyed by volcanic eruption."
                };
                return true;
            }

            return false;
        }

        private bool HandleVolcanicEruption(RobotBase robot, out LoadResult loadResult)
        {
            loadResult = new();

            if (VolcanicEruption(robot))
            {
                loadResult = new LoadResult()
                {
                    IsSuccess = false,
                    HasRobotSurvived = false,
                    Message = $"Robot {robot.RobotInformation.RobotID} is destroyed by volcanic eruption."
                };
                return true;
            }

            return false;
        }

        private bool HandleVolcanicEruption(RobotBase robot, out ScoutScanResult scoutScanResult)
        {
            scoutScanResult = new();

            if (VolcanicEruption(robot))
            {
                scoutScanResult = new ScoutScanResult()
                {
                    IsSuccess = false,
                    HasRobotSurvived = false,
                    Message = $"Robot {robot.RobotInformation.RobotID} is destroyed by volcanic eruption."
                };
                return true;
            }

            return false;
        }

        private bool HandleVolcanicEruption(RobotBase robot, out PositionResult positionResult)
        {
            positionResult = new();

            if (VolcanicEruption(robot))
            {
                positionResult = new PositionResult()
                {
                    IsSuccess = false,
                    HasRobotSurvived = false,
                    Message = $"Robot {robot.RobotInformation.RobotID} is destroyed by volcanic eruption."
                };
                return true;
            }

            return false;
        }

        private bool VolcanicEruption(RobotBase robot)
        {
            if (DoesVolcanicEruptionHappen())
            {
                if (IsRobotDestroyedRandomly(robot))
                {
                    return true;
                }
            }
            return false;
        }
    }
}