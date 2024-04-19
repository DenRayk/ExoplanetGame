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

        private readonly Random random = new();

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

            switch (weatherChange)
            {
                case <= 25:
                    Weather = Weather.SUNNY;
                    break;

                case <= 50:
                    Weather = Weather.FOGGY;
                    break;

                case <= 75:
                    Weather = Weather.ASH_IN_THE_AIR;
                    break;

                case <= 85:
                    Weather = Weather.WINDY;
                    break;

                default:
                    Weather = Weather.CLOUDY;
                    break;
            }
        }

        public override PositionResult GetRobotPosition(RobotBase robot)
        {
            if (HandleVolcanicEruption(robot, out RobotResultBase robotResult))
            {
                PositionResult positionResult = new PositionResult(robotResult);

                return positionResult;
            }

            return base.GetRobotPosition(robot);
        }

        public override LoadResult LoadEnergy(RobotBase robot, int seconds)
        {
            if (HandleVolcanicEruption(robot, out RobotResultBase robotResult))
            {
                LoadResult loadResult = new LoadResult(robotResult);

                return loadResult;
            }

            return base.LoadEnergy(robot, seconds);
        }

        public override PositionResult Move(RobotBase robot)
        {
            if (HandleVolcanicEruption(robot, out RobotResultBase robotResult))
            {
                PositionResult positionResult = new PositionResult(robotResult);

                return positionResult;
            }

            return base.Move(robot);
        }

        public override RotationResult Rotate(RobotBase robot, Rotation rotation)
        {
            if (HandleVolcanicEruption(robot, out RobotResultBase robotResult))
            {
                RotationResult rotationResult = new RotationResult(robotResult);

                return rotationResult;
            }

            return base.Rotate(robot, rotation);
        }

        public override ScanResult Scan(RobotBase robot)
        {
            if (HandleVolcanicEruption(robot, out RobotResultBase robotResult))
            {
                ScanResult scanResult = new ScanResult(robotResult);

                return scanResult;
            }

            return base.Scan(robot);
        }

        public override ScoutScanResult ScoutScan(RobotBase robot)
        {
            if (HandleVolcanicEruption(robot, out RobotResultBase robotResult))
            {
                ScoutScanResult scoutScanResult = new ScoutScanResult(robotResult);

                return scoutScanResult;
            }

            return base.ScoutScan(robot);
        }

        private bool HandleVolcanicEruption(RobotBase robot, out RobotResultBase robotResult)
        {
            robotResult = new RobotResultBase();

            if (!VolcanicEruption())
                return false;

            robotResult = new PositionResult()
            {
                IsSuccess = false,
                HasRobotSurvived = false,
                Message = $"{robot.GetLanderName()} was destroyed by a volcanic eruption."
            };
            return true;
        }

        private bool VolcanicEruption()
        {
            if (!DoesVolcanicEruptionHappen())
                return false;

            return true;
        }

        public bool DoesVolcanicEruptionHappen()
        {
            int randomEruption = random.Next(1, 101);
            bool isVolcanicEruption = randomEruption <= volcanicEruptionChance;

            return isVolcanicEruption;
        }
    }
}