using ExoplanetGame;
using ExoplanetGame.ControlCenter;
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
            GameServer server = GameServer.GetInstance(new Gaia());
            ControlCenter controlCenter = ControlCenter.GetInstance(new Gaia());

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
            GameServer server = GameServer.GetInstance(new Gaia());
            ControlCenter controlCenter = ControlCenter.GetInstance(new Gaia());

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
        public void Cleanup()
        {
            GameServer server = GameServer.GetInstance(new Gaia());
            server.ClearRobots();
        }
    }
}