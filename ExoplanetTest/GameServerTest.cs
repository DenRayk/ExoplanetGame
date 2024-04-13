using ExoplanetGame;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Robot.Variants;

namespace GameServerTest
{
    [TestClass]
    public class GameServerTest
    {
        [TestMethod]
        public void AddRobots()
        {
            // Arrange
            GameServer server = new GameServer(new Gaia());

            // Act
            server.AddRobot(RobotVariant.DEFAULT);
            server.AddRobot(RobotVariant.DEFAULT);
            server.AddRobot(RobotVariant.DEFAULT);
            server.AddRobot(RobotVariant.DEFAULT);
            server.AddRobot(RobotVariant.DEFAULT);

            // Assert
            Assert.AreEqual(5, server.RobotCount);
        }

        [TestMethod]
        public void AddRobotsOverLimit()
        {
            // Arrange
            GameServer server = new GameServer(new Gaia());

            // Act
            server.AddRobot(RobotVariant.DEFAULT);
            server.AddRobot(RobotVariant.DEFAULT);
            server.AddRobot(RobotVariant.DEFAULT);
            server.AddRobot(RobotVariant.DEFAULT);
            server.AddRobot(RobotVariant.DEFAULT);
            server.AddRobot(RobotVariant.DEFAULT);

            // Assert
            Assert.AreEqual(5, server.RobotCount);
        }

        [TestCleanup]
        public void Cleanup()
        {
            GameServer server = new GameServer(new Gaia());
            server.ClearRobots();
        }
    }
}