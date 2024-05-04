using ExoplanetGame.Application;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot.Variants;
using ExoplanetGame.Presentation.Commands.ControlCenter;
using ExoplanetGameTest.Mocks.Planets;

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

        [TestMethod]
        public void GetPercantageOfExploredMap()
        {
            // Arrange
            RockPlanet mockedPlanet = new RockPlanet();
            UCCollection ucCollection = new UCCollection();
            ExoplanetService exoplanetService = new ExoplanetService();
            exoplanetService.ExoPlanet = mockedPlanet;
            ucCollection.Init(exoplanetService);

            // Act
            ucCollection.UcCollectionControlCenter.SelectPlanetUseCase.SelectPlanet(exoplanetService.ExoPlanet);
            string exploredMap = ucCollection.UcCollectionRobot.PlanetMapService.GetPercentageOfExploredArea();

            // Assert
            Assert.AreEqual("0,00%", exploredMap);
        }

        [TestCleanup]
        public void CleanUp()
        {
            RobotRepository.GetInstance().Clear();
        }
    }
}