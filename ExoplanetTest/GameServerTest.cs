using ExoplanetGame;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Domain.Robot.Variants;
using ExoplanetGame.Exoplanet.Variants;

namespace ExoplanetGameTest
{
    [TestClass]
    public class GameServerTest
    {
        [TestMethod]
        public void AddRobots()
        {
            // Arrange
            MockPlanet mockPlanet = new MockPlanet();
            GameServer server = GameServer.GetInstance(mockPlanet);
            ControlCenter controlCenter = ControlCenter.GetInstance(mockPlanet);
            controlCenter.exoPlanet = mockPlanet;

            // Act
            server.AddRobot(RobotVariant.DEFAULT);
            server.AddRobot(RobotVariant.DEFAULT);
            server.AddRobot(RobotVariant.DEFAULT);
            server.AddRobot(RobotVariant.DEFAULT);
            server.AddRobot(RobotVariant.DEFAULT);

            // Assert
            Assert.AreEqual(5, controlCenter.GetRobotCount());
        }

        [TestMethod]
        public void AddRobotsOverLimit()
        {
            // Arrange
            MockPlanet mockPlanet = new MockPlanet();
            GameServer server = GameServer.GetInstance(mockPlanet);
            ControlCenter controlCenter = ControlCenter.GetInstance(mockPlanet);
            controlCenter.exoPlanet = mockPlanet;

            // Act
            server.AddRobot(RobotVariant.DEFAULT);
            server.AddRobot(RobotVariant.DEFAULT);
            server.AddRobot(RobotVariant.DEFAULT);
            server.AddRobot(RobotVariant.DEFAULT);
            server.AddRobot(RobotVariant.DEFAULT);
            server.AddRobot(RobotVariant.DEFAULT);

            // Assert
            Assert.AreEqual(5, controlCenter.GetRobotCount());
        }

        [TestCleanup]
        public void ClearRobots()
        {
            // Arrange
            MockPlanet mockPlanet = new MockPlanet();
            GameServer server = new GameServer(mockPlanet);
            ControlCenter controlCenter = ControlCenter.GetInstance(mockPlanet);
            controlCenter.ClearRobots();
        }
    }
}