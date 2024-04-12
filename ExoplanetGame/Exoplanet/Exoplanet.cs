using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet
{
    public class Exoplanet
    {
        public Topography Topography { get; set; }
        private RobotManager robotManager;

        public Exoplanet()
        {
            Topography = new Topography([
                "GSSWPFSGGL",
                "SPW4PSFFLL",
                "SGWPMSFLLF",
                "SSWWMGSFFG",
                "FGSWWSSGFG",
                "FFWWGGSGFF"
            ]);
            robotManager = new RobotManager();
        }

        public int GetRobotCount()
        {
            return robotManager.GetRobotCount();
        }

        public void RemoveRobot(RobotBase Robot)
        {
            robotManager.RemoveRobot(Robot);
        }

        public bool Land(RobotBase Robot, Position landPosition)
        {
            return robotManager.LandRobot(Robot, landPosition, Topography);
        }

        public Position Move(RobotBase Robot)
        {
            return robotManager.MoveRobot(Robot, Topography);
        }

        public Direction Rotate(RobotBase robot, Rotation rotation)
        {
            return robotManager.RotateRobot(robot, rotation);
        }

        public Measure Scan(RobotBase robot)
        {
            return Topography.GetMeasureAtPosition(robotManager.GetRobotPosition(robot));
        }

        public Dictionary<Measure, Position> ScoutScan(RobotBase robot)
        {
            Dictionary<Measure, Position> scoutScanResults = new Dictionary<Measure, Position>();
            Position currentRobotPosition = robotManager.GetRobotPosition(robot);

            Measure currentMeasure = Topography.GetMeasureAtPosition(currentRobotPosition);
            scoutScanResults.Add(currentMeasure, currentRobotPosition);

            if (robot.RobotVariant == RobotVariant.SCOUT)
            {
                Position firstForwardPosition = currentRobotPosition.GetAdjacentPosition();
                Position secondForwardPosition = firstForwardPosition.GetAdjacentPosition();

                bool isFirstPositionValid = robotManager.IsPositionInBounds(firstForwardPosition, Topography);
                bool isSecondPositionValid = robotManager.IsPositionInBounds(secondForwardPosition, Topography);

                if (isFirstPositionValid)
                {
                    Measure firstPositionMeasure = Topography.GetMeasureAtPosition(firstForwardPosition);
                    scoutScanResults.Add(firstPositionMeasure, firstForwardPosition);
                }
                if (isSecondPositionValid)
                {
                    Measure secondPositionMeasure = Topography.GetMeasureAtPosition(secondForwardPosition);
                    scoutScanResults.Add(secondPositionMeasure, secondForwardPosition);
                }
            }

            return scoutScanResults;
        }


        public Position GetRobotPosition(RobotBase robot)
        {
            return robotManager.GetRobotPosition(robot);
        }

        public void Remove(RobotBase robot)
        {
            Console.WriteLine($"Remove: {robot.GetLanderName()}");
            robotManager.RemoveRobot(robot);
        }
    }
}