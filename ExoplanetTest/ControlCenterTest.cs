using ExoplanetGame.ControlCenter;
using ExoplanetGame.Domain.Exoplanet.Variants;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.Variants;
using ExoplanetGame.Robot.Movement;

namespace ExoplanetGameTest
{
    [TestClass]
    public class ControlCenterTest
    {
        [TestMethod]
        public void AddRobots()
        {
            // Arrange
            MockPlanet mockPlanet = new MockPlanet();
            ControlCenter controlCenter = ControlCenter.GetInstance(mockPlanet);
            controlCenter.exoPlanet = mockPlanet;

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
            controlCenter.exoPlanet = exoplanet;
            controlCenter.AddRobot(RobotVariant.DEFAULT);
            RobotBase robot = controlCenter.GetRobotByID(0);

            // Act
            robot.Land(new Position(1, 1, Direction.NORTH));

            // Assert
            Assert.AreEqual(true, robot.RobotInformation.HasLanded);
        }
    }
}