using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.RemoteRobot;

namespace ExoplanetGameTest
{
    [TestClass]
    internal class RobotTest
    {
        [TestMethod]
        public void TestLand()
        {
            // Arrange
            Exoplanet exoplanet = new Exoplanet();
            ControlCenter controlCenter = ControlCenter.GetInstance(exoplanet);
            RobotFactory robotFactory = RobotFactory.GetInstance();
            RobotBase robot = robotFactory.CreateRemoteRobot(controlCenter, exoplanet, 0);
            Position landPosition = new Position(3, 3, Direction.NORTH);

            // Act
            robot.Land(new Position(3, 3, Direction.NORTH));

            // Assert
            Assert.AreEqual(landPosition, robot.RobotStatus.Position);
        }
    }
}