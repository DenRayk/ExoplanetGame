using ExoplanetGame.Application;
using ExoplanetGame.Application.ControlCenter;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Exoplanet.Variants;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Factory;
using ExoplanetGame.Domain.Robot.Variants;
using ExoplanetGame.Presentation.Commands.Robot;
using ExoplanetGame.Presentation.Commands.PlanetSelection;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGameTest
{
    [TestClass]
    public class RobotTest
    {
        [TestMethod]
        public void TestLand()
        {
            // Arrange
            UCCollection ucCollection = new UCCollection();
            ExoplanetService exoplanetService = new ExoplanetService();
            exoplanetService.CreateExoPlanet(PlanetVariant.GAIA);
            ucCollection.UcCollectionControlCenter.SelectPlanetUseCase.SelectPlanet(exoplanetService.ExoPlanet);
            RobotBase robotBase = new RobotBase(exoplanetService.ExoPlanet, ControlCenter.GetInstance(), 0, RobotVariant.DEFAULT);

            ucCollection.init(exoplanetService);

            // Act
            ucCollection.UcCollectionRobot.RobotLandService.LandRobot(robotBase, new Position(1, 1));

            // Assert
            Assert.IsTrue(robotBase.HasLanded());
        }

        [TestMethod]
        public void TestMove()
        {
            // Arrange
            MockedPlanet mockedPlanet = new MockedPlanet();
            UCCollection ucCollection = new UCCollection();
            ExoplanetService exoplanetService = new ExoplanetService();
            exoplanetService.ExoPlanet = mockedPlanet;
            ucCollection.UcCollectionControlCenter.SelectPlanetUseCase.SelectPlanet(exoplanetService.ExoPlanet);
            RobotBase robotBase = new RobotBase(exoplanetService.ExoPlanet, ControlCenter.GetInstance(), 0, RobotVariant.DEFAULT);

            ucCollection.init(exoplanetService);

            // Act
            ucCollection.UcCollectionRobot.RobotLandService.LandRobot(robotBase, new Position(1, 1));
            PositionResult positionResult = ucCollection.UcCollectionRobot.MoveRobotService.Move(robotBase);

            // Assert
            Assert.AreEqual(new Position(1, 0), positionResult.Position);
        }

        [TestMethod]
        public void TestTurnLeft()
        {
            // Arrange
            MockedPlanet mockedPlanet = new MockedPlanet();
            UCCollection ucCollection = new UCCollection();
            ExoplanetService exoplanetService = new ExoplanetService();
            exoplanetService.ExoPlanet = mockedPlanet;
            ucCollection.UcCollectionControlCenter.SelectPlanetUseCase.SelectPlanet(exoplanetService.ExoPlanet);
            RobotBase robotBase = new RobotBase(exoplanetService.ExoPlanet, ControlCenter.GetInstance(), 0, RobotVariant.DEFAULT);

            ucCollection.init(exoplanetService);

            // Act
            ucCollection.UcCollectionRobot.RobotLandService.LandRobot(robotBase, new Position(1, 1));
            RotationResult rotationResult = ucCollection.UcCollectionRobot.RotateRobotService.Rotate(robotBase, Rotation.LEFT);

            // Assert
            Assert.AreEqual(Direction.WEST, rotationResult.Direction);
        }

        [TestMethod]
        public void TestTurnRight()
        {
            // Arrange
            MockedPlanet mockedPlanet = new MockedPlanet();
            UCCollection ucCollection = new UCCollection();
            ExoplanetService exoplanetService = new ExoplanetService();
            exoplanetService.ExoPlanet = mockedPlanet;
            ucCollection.UcCollectionControlCenter.SelectPlanetUseCase.SelectPlanet(exoplanetService.ExoPlanet);
            RobotBase robotBase = new RobotBase(exoplanetService.ExoPlanet, ControlCenter.GetInstance(), 0, RobotVariant.DEFAULT);

            ucCollection.init(exoplanetService);

            // Act
            ucCollection.UcCollectionRobot.RobotLandService.LandRobot(robotBase, new Position(1, 1));
            RotationResult rotationResult = ucCollection.UcCollectionRobot.RotateRobotService.Rotate(robotBase, Rotation.RIGHT);

            // Assert
            Assert.AreEqual(Direction.EAST, rotationResult.Direction);
        }

        [TestMethod]
        public void TestCrash()
        {
            // Arrange
            MockedPlanet mockedPlanet = new MockedPlanet();
            UCCollection ucCollection = new UCCollection();
            ExoplanetService exoplanetService = new ExoplanetService();
            exoplanetService.ExoPlanet = mockedPlanet;
            ucCollection.UcCollectionControlCenter.SelectPlanetUseCase.SelectPlanet(exoplanetService.ExoPlanet);
            RobotBase robotBase = new RobotBase(exoplanetService.ExoPlanet, ControlCenter.GetInstance(), 0, RobotVariant.DEFAULT);

            ucCollection.init(exoplanetService);

            // Act
            ucCollection.UcCollectionRobot.RobotLandService.LandRobot(robotBase, new Position(1, 1));
            RobotResultBase robotResult = ucCollection.UcCollectionRobot.CrashRobotService.Crash(robotBase);

            // Assert
            Assert.IsFalse(robotResult.HasRobotSurvived);
        }

        [TestMethod]
        public void TestScan()
        {
            // Arrange
            MockedPlanet mockedPlanet = new MockedPlanet();
            UCCollection ucCollection = new UCCollection();
            ExoplanetService exoplanetService = new ExoplanetService();
            exoplanetService.ExoPlanet = mockedPlanet;
            ucCollection.UcCollectionControlCenter.SelectPlanetUseCase.SelectPlanet(exoplanetService.ExoPlanet);
            RobotBase robotBase = new RobotBase(exoplanetService.ExoPlanet, ControlCenter.GetInstance(), 0, RobotVariant.DEFAULT);

            ucCollection.init(exoplanetService);

            // Act
            ucCollection.UcCollectionRobot.RobotLandService.LandRobot(robotBase, new Position(1, 1));
            ScanResult scanResult = ucCollection.UcCollectionRobot.RobotScanService.Scan(robotBase);

            // Assert
            Assert.AreEqual(new Measure(Ground.NOTHING, 0), scanResult.Measures.Keys.First());
        }
    }
}