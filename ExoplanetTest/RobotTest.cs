using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Robot;

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
            RobotFactory robotFactory = RobotFactory.GetInstance();
            RobotBase robot = robotFactory.CreateDefaultRobot(controlCenter, exoplanet, 0);
            Position landPosition = new Position(1, 1, Direction.NORTH);

            // Act
            robot.Land(new Position(1, 1, Direction.NORTH));

            // Assert
            Assert.AreEqual(landPosition, robot.RobotStatus.Position);
        }

        [TestMethod]
        public void TestMove()
        {
            // Arrange
            Gaia exoplanet = new Gaia();
            ControlCenter controlCenter = ControlCenter.GetInstance(exoplanet);
            RobotFactory robotFactory = RobotFactory.GetInstance();
            RobotBase robot = robotFactory.CreateDefaultRobot(controlCenter, exoplanet, 0);
            robot.Land(new Position(1, 1, Direction.NORTH));

            // Act
            robot.Move();

            // Assert
            Assert.AreEqual(new Position(1, 0, Direction.NORTH), robot.RobotStatus.Position);
        }

        [TestMethod]
        public void TestTurnLeft()
        {
            // Arrange
            Gaia exoplanet = new Gaia();
            ControlCenter controlCenter = ControlCenter.GetInstance(exoplanet);
            RobotFactory robotFactory = RobotFactory.GetInstance();
            RobotBase robot = robotFactory.CreateDefaultRobot(controlCenter, exoplanet, 0);
            robot.Land(new Position(1, 1, Direction.NORTH));

            // Act
            robot.Rotate(Rotation.LEFT);

            // Assert
            Assert.AreEqual(new Position(1, 1, Direction.WEST), robot.RobotStatus.Position);
        }

        [TestMethod]
        public void TestTurnRight()
        {
            // Arrange
            Gaia exoplanet = new Gaia();
            ControlCenter controlCenter = ControlCenter.GetInstance(exoplanet);
            RobotFactory robotFactory = RobotFactory.GetInstance();
            RobotBase robot = robotFactory.CreateDefaultRobot(controlCenter, exoplanet, 0);
            robot.Land(new Position(1, 1, Direction.NORTH));

            // Act
            robot.Rotate(Rotation.RIGHT);

            // Assert
            Assert.AreEqual(new Position(1, 1, Direction.EAST), robot.RobotStatus.Position);
        }

        [TestMethod]
        public void TestCrash()
        {
            // Arrange
            Gaia exoplanet = new Gaia();
            ControlCenter controlCenter = ControlCenter.GetInstance(exoplanet);
            RobotFactory robotFactory = RobotFactory.GetInstance();
            RobotBase robot = robotFactory.CreateDefaultRobot(controlCenter, exoplanet, 0);
            robot.Land(new Position(3, 3, Direction.NORTH));

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
            RobotFactory robotFactory = RobotFactory.GetInstance();
            RobotBase robot = robotFactory.CreateDefaultRobot(controlCenter, exoplanet, 0);
            robot.Land(new Position(3, 2, Direction.NORTH));

            // Act
            Measure measure = robot.Scan();

            // Assert
            Assert.AreEqual(Ground.PLANT, measure.Ground);
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