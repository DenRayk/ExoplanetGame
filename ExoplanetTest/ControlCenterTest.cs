using ExoplanetGame;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot;

namespace ControlCenterTest
{
    [TestClass]
    public class ControlCenterTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            Exoplanet exoplanet = new Exoplanet();
            ControlCenter.GetInstance(exoplanet).ClearRobots();
        }

        [TestMethod]
        public void AddRobots()
        {
            // Arrange
            Exoplanet exoplanet = new Exoplanet();
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
            Exoplanet exoplanet = new Exoplanet();
            ControlCenter controlCenter = ControlCenter.GetInstance(exoplanet);
            RobotFactory robotFactory = RobotFactory.GetInstance();
            RobotBase robot = robotFactory.CreateDefaultRobot(controlCenter, exoplanet, 0);
            controlCenter.AddRobot(robot);

            // Act
            robot.Land(new Position(3, 3, Direction.NORTH));

            // Assert
            Assert.AreEqual(true, controlCenter.GetRobotByID(0).RobotStatus.HasLanded);
        }
    }
}