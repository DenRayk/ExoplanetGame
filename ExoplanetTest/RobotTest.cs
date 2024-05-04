using ExoplanetGame.Application;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Exoplanet.Variants;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Variants;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGameTest.Mocks;

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
            IRobot robotBase = new DefaultBot(exoplanetService.ExoPlanet, 0);

            ucCollection.Init(exoplanetService);

            // Act
            ucCollection.UcCollectionRobot.RobotLandService.LandRobot(robotBase, new Position(1, 1));

            // Assert
            Assert.IsTrue(robotBase.RobotInformation.HasLanded);
        }

        [TestMethod]
        public void TestLandOnAnotherRobot()
        {
            // Arrange
            UCCollection ucCollection = new UCCollection();
            ExoplanetService exoplanetService = new ExoplanetService();
            exoplanetService.CreateExoPlanet(PlanetVariant.GAIA);
            ucCollection.UcCollectionControlCenter.SelectPlanetUseCase.SelectPlanet(exoplanetService.ExoPlanet);
            IRobot robotBase = new DefaultBot(exoplanetService.ExoPlanet, 0);
            IRobot robotBase2 = new DefaultBot(exoplanetService.ExoPlanet, 1);

            ucCollection.Init(exoplanetService);

            // Act
            ucCollection.UcCollectionRobot.RobotLandService.LandRobot(robotBase, new Position(1, 1));
            ucCollection.UcCollectionRobot.RobotLandService.LandRobot(robotBase2, new Position(1, 1));

            // Assert
            Assert.IsFalse(robotBase2.RobotInformation.HasLanded);
        }

        [TestMethod]
        public void TestMove()
        {
            // Arrange
            RockPlanet mockedPlanet = new RockPlanet();
            UCCollection ucCollection = new UCCollection();
            ExoplanetService exoplanetService = new ExoplanetService();
            exoplanetService.ExoPlanet = mockedPlanet;
            ucCollection.UcCollectionControlCenter.SelectPlanetUseCase.SelectPlanet(exoplanetService.ExoPlanet);
            IRobot robotBase = new DefaultBot(exoplanetService.ExoPlanet, 0);

            ucCollection.Init(exoplanetService);

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
            RockPlanet mockedPlanet = new RockPlanet();
            UCCollection ucCollection = new UCCollection();
            ExoplanetService exoplanetService = new ExoplanetService();
            exoplanetService.ExoPlanet = mockedPlanet;
            ucCollection.UcCollectionControlCenter.SelectPlanetUseCase.SelectPlanet(exoplanetService.ExoPlanet);
            IRobot robotBase = new DefaultBot(exoplanetService.ExoPlanet, 0);

            ucCollection.Init(exoplanetService);

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
            RockPlanet mockedPlanet = new RockPlanet();
            UCCollection ucCollection = new UCCollection();
            ExoplanetService exoplanetService = new ExoplanetService();
            exoplanetService.ExoPlanet = mockedPlanet;
            ucCollection.UcCollectionControlCenter.SelectPlanetUseCase.SelectPlanet(exoplanetService.ExoPlanet);
            IRobot robotBase = new DefaultBot(exoplanetService.ExoPlanet, 0);

            ucCollection.Init(exoplanetService);

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
            RockPlanet mockedPlanet = new RockPlanet();
            UCCollection ucCollection = new UCCollection();
            ExoplanetService exoplanetService = new ExoplanetService();
            exoplanetService.ExoPlanet = mockedPlanet;
            ucCollection.UcCollectionControlCenter.SelectPlanetUseCase.SelectPlanet(exoplanetService.ExoPlanet);
            IRobot robotBase = new DefaultBot(exoplanetService.ExoPlanet, 0);

            ucCollection.Init(exoplanetService);

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
            RockPlanet mockedPlanet = new RockPlanet();
            UCCollection ucCollection = new UCCollection();
            ExoplanetService exoplanetService = new ExoplanetService();
            exoplanetService.ExoPlanet = mockedPlanet;
            ucCollection.UcCollectionControlCenter.SelectPlanetUseCase.SelectPlanet(exoplanetService.ExoPlanet);
            IRobot robotBase = new DefaultBot(exoplanetService.ExoPlanet, 0);

            ucCollection.Init(exoplanetService);

            // Act
            ucCollection.UcCollectionRobot.RobotLandService.LandRobot(robotBase, new Position(1, 1));
            ScanResult scanResult = ucCollection.UcCollectionRobot.RobotScanService.Scan(robotBase);

            // Assert
            Assert.AreEqual(new Measure(Ground.NOTHING, 0), scanResult.Measures.Keys.First());
        }
    }
}