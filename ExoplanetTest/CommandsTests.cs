using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Presentation.Commands;
using ExoplanetGameTest.Mocks.Commands;

namespace ExoplanetGameTest
{
    [TestClass]
    public class CommandsTests
    {
        [TestMethod]
        public void TestReadUserInputWithOptions()
        {
            TestCommand testCommand = new TestCommand();

            // Arrange
            Dictionary<string, BaseCommand> options = new Dictionary<string, BaseCommand>
            {
                { "Option 1", new HelpCommand() },
                { "Option 2", new ExitCommand() }
            };

            // Act
            BaseCommand result = testCommand.ReadUserInputWithOptions(options);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestReadUserInputWithOptionsAndF1()
        {
            F1Command f1TestCommand = new F1Command();

            // Arrange
            Dictionary<string, BaseCommand> options = new Dictionary<string, BaseCommand>
            {
                { "Option 1", new HelpCommand() },
                { "Option 2", new ExitCommand() }
            };

            // Act
            BaseCommand result = f1TestCommand.ReadUserInputWithOptions(options);

            // Assert
            Assert.IsInstanceOfType(result, typeof(HelpCommand));
        }

        [TestMethod]
        public void TestReadUserInputWithOptionsAndEsc()
        {
            EscCommand escTestCommand = new EscCommand();

            // Arrange
            Dictionary<string, BaseCommand> options = new Dictionary<string, BaseCommand>
            {
                { "Option 1", new HelpCommand() },
                { "Option 2", new ExitCommand() }
            };

            // Act
            BaseCommand result = escTestCommand.ReadUserInputWithOptions(options);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ExitCommand));
        }
    }
}