using ExoplanetGame;

namespace GameServerTest
{
    [TestClass]
    public class GameServerTest
    {
        [TestMethod]
        public void AddRobots()
        {
            // Arrange
            GameServer server = GameServer.GetInstance();

            // Act
            server.AddRobot();
            server.AddRobot();
            server.AddRobot();
            server.AddRobot();
            server.AddRobot();

            // Assert
            Assert.AreEqual(5, server.RobotCount);
        }

        [TestMethod]
        public void AddRobotsOverLimit()
        {
            // Arrange
            GameServer server = GameServer.GetInstance();

            // Act
            server.AddRobot();
            server.AddRobot();
            server.AddRobot();
            server.AddRobot();
            server.AddRobot();
            server.AddRobot();

            // Assert
            Assert.AreEqual(5, server.RobotCount);
        }

        [TestCleanup]
        public void Cleanup()
        {
            GameServer server = GameServer.GetInstance();
            server.ClearRobots();
        }
    }
}