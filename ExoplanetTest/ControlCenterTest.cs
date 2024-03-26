using ExoplanetGame;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.RemoteRobot;

namespace ControlCenterTest
{
    [TestClass]
    public class ControlCenterTest
    {
        [TestMethod]
        public void AddRobots()
        {
            // Arrange
            Exoplanet exoplanet = new Exoplanet();
            ControlCenter controlCenter = ControlCenter.GetInstance(exoplanet);
            RobotFactory robotFactory = RobotFactory.GetInstance();

            // Act
            RobotBase robot = robotFactory.CreateRemoteRobot(controlCenter, exoplanet, 0);
            controlCenter.AddRobot(robot);

            // Assert
            Assert.AreEqual(1, controlCenter.GetRobotCount());
        }
    }
}