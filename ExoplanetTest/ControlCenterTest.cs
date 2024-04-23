using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGameTest
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

            // Act
            controlCenter.AddRobot(RobotVariant.DEFAULT);

            // Assert
            Assert.AreEqual(1, controlCenter.GetRobotCount());
        }

        [TestMethod]
        public void LandRobot()
        {
            // Arrange
            Gaia exoplanet = new Gaia();
            ControlCenter controlCenter = ControlCenter.GetInstance(exoplanet);
            controlCenter.AddRobot(RobotVariant.DEFAULT);
            RobotBase robot = controlCenter.GetRobotByID(0);

            // Act
            robot.Land(new Position(1, 1, Direction.NORTH));

            // Assert
            Assert.AreEqual(true, robot.RobotInformation.HasLanded);
        }
    }
}