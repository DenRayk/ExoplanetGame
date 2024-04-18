using ExoplanetGame;
using ExoplanetGame.ControlCenter.Core;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Factory;
using ExoplanetGame.Robot.Movement;

namespace ControlCenterTest
{
    [TestClass]
    public class ControlCenterTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Gaia exoplanet = new Gaia();
            ControlCenter.GetInstance(exoplanet).ClearRobots();
        }

        [TestMethod]
        public void AddRobots()
        {
            // Arrange
            Gaia exoplanet = new Gaia();
            ControlCenter controlCenter = ControlCenter.GetInstance(exoplanet);
            RobotFactory robotFactory = RobotFactory.GetInstance();

            // Act
            RobotBase robot = robotFactory.CreateDefaultRobot(controlCenter, exoplanet, 0);
            controlCenter.AddRobot(robot);

            // Assert
            Assert.AreEqual(1, controlCenter.GetRobotCount());
        }

        [TestMethod]
        public void LandRobot()
        {
            // Arrange
            Gaia exoplanet = new Gaia();
            ControlCenter controlCenter = ControlCenter.GetInstance(exoplanet);
            RobotFactory robotFactory = RobotFactory.GetInstance();
            RobotBase robot = robotFactory.CreateDefaultRobot(controlCenter, exoplanet, 0);
            controlCenter.AddRobot(robot);

            // Act
            robot.Land(new Position(0, 5, Direction.NORTH));

            // Assert
            Assert.AreEqual(true, controlCenter.GetRobotByID(0).RobotInformation.HasLanded);
        }
    }
}