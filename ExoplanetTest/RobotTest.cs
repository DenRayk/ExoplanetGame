using ExoplanetGame.ControlCenter;
using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Exoplanet.Variants;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Factory;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGameTest
{
    [TestClass]
    public class RobotTest
    {
        [TestMethod]
        public void TestLand()
        {
            // Arrange
            Gaia exoplanet = new Gaia();
            ControlCenter controlCenter = ControlCenter.GetInstance(exoplanet);
            controlCenter.exoPlanet = exoplanet;
            RobotFactory robotFactory = RobotFactory.GetInstance();
            RobotBase robot = robotFactory.CreateDefaultRobot(controlCenter, exoplanet, 0);
            Position landPosition = new Position(0, 5, Direction.NORTH);

            // Act
            robot.Land(new Position(0, 5, Direction.NORTH));

            // Assert
            Assert.AreEqual(landPosition, robot.RobotInformation.Position);
        }

        [TestMethod]
        public void TestMove()
        {
            // Arrange
            Gaia exoplanet = new Gaia();
            ControlCenter controlCenter = ControlCenter.GetInstance(exoplanet);
            controlCenter.exoPlanet = exoplanet;
            RobotFactory robotFactory = RobotFactory.GetInstance();
            RobotBase robot = robotFactory.CreateDefaultRobot(controlCenter, exoplanet, 0);
            robot.Land(new Position(0, 5, Direction.NORTH));

            // Act
            robot.Move();

            // Assert
            Assert.AreEqual(new Position(0, 4, Direction.NORTH), robot.RobotInformation.Position);
        }

        [TestMethod]
        public void TestTurnLeft()
        {
            // Arrange
            Gaia exoplanet = new Gaia();
            ControlCenter controlCenter = ControlCenter.GetInstance(exoplanet);
            controlCenter.exoPlanet = exoplanet;
            RobotFactory robotFactory = RobotFactory.GetInstance();
            RobotBase robot = robotFactory.CreateDefaultRobot(controlCenter, exoplanet, 0);
            robot.Land(new Position(0, 5, Direction.NORTH));

            // Act
            robot.Rotate(Rotation.LEFT);

            // Assert
            Assert.AreEqual(new Position(0, 5, Direction.WEST), robot.RobotInformation.Position);
        }

        [TestMethod]
        public void TestTurnRight()
        {
            // Arrange
            Gaia exoplanet = new Gaia();
            ControlCenter controlCenter = ControlCenter.GetInstance(exoplanet);
            controlCenter.exoPlanet = exoplanet;
            RobotFactory robotFactory = RobotFactory.GetInstance();
            RobotBase robot = robotFactory.CreateDefaultRobot(controlCenter, exoplanet, 0);
            robot.Land(new Position(0, 5, Direction.NORTH));

            // Act
            robot.Rotate(Rotation.RIGHT);

            // Assert
            Assert.AreEqual(new Position(0, 5, Direction.EAST), robot.RobotInformation.Position);
        }

        [TestMethod]
        public void TestCrash()
        {
            // Arrange
            Gaia exoplanet = new Gaia();
            ControlCenter controlCenter = ControlCenter.GetInstance(exoplanet);
            controlCenter.exoPlanet = exoplanet;
            RobotFactory robotFactory = RobotFactory.GetInstance();
            RobotBase robot = robotFactory.CreateDefaultRobot(controlCenter, exoplanet, 0);
            robot.Land(new Position(0, 5, Direction.NORTH));

            // Act
            robot.Crash();

            // Assert
            Assert.AreEqual(0, exoplanet.GetRobotCount());
        }

        [TestMethod]
        public void TestScan()
        {
            // Arrange
            Gaia exoplanet = new Gaia();
            ControlCenter controlCenter = ControlCenter.GetInstance(exoplanet);
            controlCenter.exoPlanet = exoplanet;
            RobotFactory robotFactory = RobotFactory.GetInstance();
            RobotBase robot = robotFactory.CreateDefaultRobot(controlCenter, exoplanet, 0);
            robot.Land(new Position(0, 5, Direction.NORTH));

            // Act
            ScanResult scanResult = robot.Scan();

            // Assert
            Assert.AreEqual(Ground.SAND, scanResult.Measure.Ground);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Gaia exoplanet = new Gaia();
            ControlCenter controlCenter = ControlCenter.GetInstance(exoplanet);
            controlCenter.ClearRobots();
        }
    }
}