using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Tropica : ExoplanetBase
    {
        private static readonly int mysteriousAttackChance = 50;

        private readonly List<string[]> tropicaVariants = new()
        {
            new string[]
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
                "RRMRWRPGGPWRRGSMGGWPSMSRRWGGRR"
            },

            new string[]
            {
                "GGGGSSGSMRGGWWPMWPGGGGGGSSGSMR",
                "GGMPGSGSSRGGWWSMWWWPGGMPGSGSSR",
                "WWMMPWSPPMGMMWSPPRWWWWMMPWSPPM",
                "WWWWWWWWWRGPMWSSRPGMWWWWWWWWWR",
                "PSSSGGGWWWSGPWGSMMMMPSSSGGGWWW",
                "MMPSSSMPRRSSWWGSPPGGMMPSSSMPRR",
                "WWPRMPMMMPGGSWGMMPGGWWPRMPMMMP",
                "PWRPMPPMGGSSPWWPMMGGPWRPMPPMGG",
                "GWWGMGGGGGMSPWWRMGGPGWWGMGGGGG",
                "GPWMMGGGPPRRMRWRPGGPGPWMMGGGPP"
            },

            new string[]
            {
                "GPWWWWRRGGGMWMPMGGMWGPWWWWRRGG",
                "MMPGGSWPWPPMWWMWMPPRMMPGGSWPWP",
                "WWMGGSSWMMWPMWWGPPPRWWMGGSSWMM",
                "MWWRRWSPMWWGGRWWGPSGMWWRRWSPMW",
                "PMWWPWWWWWWGGRPWSMMSPMWWPWWWWW",
                "MWGWWWWWWWWSSWWWPWWMMWGWWWWWWW",
                "GMPGSPRGWWRWSSWWRMGGGMPGSPRGWW",
                "GPPPMWMGWPRPWPWWGGGGGPPPMWMGWP",
                "MPPSMWGGWPGWMMWWWWWWMPPSMWGGWP",
                "WRRGSMGGWPGPMWWWWPPPWRRGSMGGWP"
            },

            new string[]
            {
                "WWWWSSSSSSSWWGSPPGGGWWWWSSSSSS",
                "SMSWWMSMMGGSWGMMPGGSSMSWWMSMMM",
                "PGMSWWWWWSSPWWPMMGGMPGMSWWWWWS",
                "GPGPPSSPSMSPWWRMGGPMGPGPPSSPSR",
                "GGMPPMSSSRRMRWRPGGPMGGMPPMSSSR",
                "PMWPGGGPWGGGGSSGSMRMPMWPGGGPWW",
                "SMWWWPMMPGGMPGSGSSRPSMWWWPMMPG",
                "SPPRWWWWMWWMMPWSPPMMSPPRWWWWMG",
                "SSRPGMMWWWWWWWWWWWRWSSRPGMMWWR",
                "GSMMMMPMWPSSSGGGWWWWGSMMMMPMWM"
            },

            new string[]
            {
                "WGGRWWGPSGWWWRRGGGMWWWWWSSSSSS",
                "WGGRPWSMMSGGSWPWPPMWSMSWWMSMMM",
                "WSSWWWPWWMGGSSWMMWPMPGMSWWWWWS",
                "RWSSWWRMGGRRWSPMWWGGGPGPPSSPSR",
                "PPWPWWGGGGWPWWWWWWGGGGMPPMSSSR",
                "PRRRWWWWWWWWWWWWWWSSPMWPGGGPWW",
                "RRGGWWWPPPGSPRGWWRWSSMWWWPMMPG",
                "RRRRWWRRGGPMWMGWPRPWSPPRWWWWMG",
                "PRPRMSWPWPSMWGGWPGWMSSRPGMMWWR",
                "WPPPMSSWMMGSMGGWPGPMGSMMMMPMWR"
            }
        };

        private readonly Random random = new();

        public Tropica() : base(PlanetVariant.TROPICA)
        {
            Weather = Weather.SUNNY;

            int randomVariant = random.Next(0, tropicaVariants.Count);
            Topography = new Topography(tropicaVariants[randomVariant]);

            robotManager = new RobotManager(this);
        }

        public override void ChangeWeather()
        {
            int weatherChange = random.Next(1, 101);

            switch (weatherChange)
            {
                case <= 70:
                    Weather = Weather.RAINY;
                    break;

                case <= 80:
                    Weather = Weather.SUNNY;
                    break;

                case <= 90:
                    Weather = Weather.CLOUDY;
                    break;

                default:
                    Weather = Weather.FOGGY;
                    break;
            }
        }

        public override PositionResult GetRobotPosition(RobotBase robot)
        {
            if (HandleMysteriousAttack(robot, out RobotResultBase robotResult))
            {
                PositionResult positionResult = new PositionResult(robotResult);

                return positionResult;
            }

            return base.GetRobotPosition(robot);
        }

        public override LoadResult LoadEnergy(RobotBase robot, int seconds)
        {
            if (HandleMysteriousAttack(robot, out RobotResultBase robotResult))
            {
                LoadResult loadResult = new LoadResult(robotResult);

                return loadResult;
            }

            return base.LoadEnergy(robot, seconds);
        }

        public override PositionResult Move(RobotBase robot)
        {
            if (HandleMysteriousAttack(robot, out RobotResultBase robotResult))
            {
                PositionResult positionResult = new PositionResult(robotResult);

                return positionResult;
            }

            return base.Move(robot);
        }

        public override RotationResult Rotate(RobotBase robot, Rotation rotation)
        {
            if (HandleMysteriousAttack(robot, out RobotResultBase robotResult))
            {
                RotationResult rotationResult = new RotationResult(robotResult);

                return rotationResult;
            }

            return base.Rotate(robot, rotation);
        }

        public override ScanResult Scan(RobotBase robot)
        {
            if (HandleMysteriousAttack(robot, out RobotResultBase robotResult))
            {
                ScanResult scanResult = new ScanResult(robotResult);

                return scanResult;
            }

            return base.Scan(robot);
        }

        public override ScoutScanResult ScoutScan(RobotBase robot)
        {
            if (HandleMysteriousAttack(robot, out RobotResultBase robotResult))
            {
                ScoutScanResult scoutScanResult = new ScoutScanResult(robotResult);

                return scoutScanResult;
            }

            return base.ScoutScan(robot);
        }

        private bool HandleMysteriousAttack(RobotBase robot, out RobotResultBase robotResult)
        {
            robotResult = new RobotResultBase();

            if (!MysteriousAttack())
                return false;

            string typeOfAttack = GetRandomAttackType();

            robotResult = new PositionResult()
            {
                IsSuccess = false,
                HasRobotSurvived = false,
                Message = $"{robot.GetLanderName()} was destroyed by {typeOfAttack}."
            };

            return true;
        }

        private string GetRandomAttackType()
        {
            int randomAttack = random.Next(1, 101);

            switch (randomAttack)
            {
                case <= 20:
                    return "a sudden surge of untraceable energy";

                case <= 40:
                    return "a trap set in the trees";

                case <= 60:
                    return "a large animal";

                case <= 80:
                    return "a mysterious jungle creature";

                default:
                    return "a mysterious attack";
            }
        }

        private bool MysteriousAttack()
        {
            if (!DoesMysteriousAttackHappen())
                return false;

            return true;
        }

        private bool DoesMysteriousAttackHappen()
        {
            int randomEruption = random.Next(1, 101);
            bool isVolcanicEruption = randomEruption <= mysteriousAttackChance;

            return isVolcanicEruption;
        }
    }
}