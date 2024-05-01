using ExoplanetGame.Application;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Exoplanet.Variants;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Factory;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.Variants;
using ExoplanetGame.Presentation.Commands.ControlCenter;

namespace ExoplanetGameTest
{
    [TestClass]
    public class ControlCenterTest
    {
        [TestMethod]
        public void AddRobot()
        {
            // Arrange
            UCCollection ucCollection = new UCCollection();

            AddRobotCommand addRobotCommand = new AddRobotCommand(RobotVariant.DEFAULT, ucCollection);

            // Act
            addRobotCommand.Execute();

            // Assert
            Assert.AreEqual(1, ucCollection.UcCollectionControlCenter.GetRobotsService.GetAllRobots().Count);
        }

        [TestMethod]
        public void AddTooManyRobots()
        {
            // Arrange
            UCCollection ucCollection = new UCCollection();

            AddRobotCommand addRobotCommand = new AddRobotCommand(RobotVariant.DEFAULT, ucCollection);

            // Act
            addRobotCommand.Execute();
            addRobotCommand.Execute();
            addRobotCommand.Execute();
            addRobotCommand.Execute();
            addRobotCommand.Execute();
            addRobotCommand.Execute();

            // Assert
            Assert.AreEqual(5, ucCollection.UcCollectionControlCenter.GetRobotsService.GetAllRobots().Count);
        }
    }
}