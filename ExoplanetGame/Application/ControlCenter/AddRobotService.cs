using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot.Factory;
using ExoplanetGame.Domain.Robot.Variants;

namespace ExoplanetGame.Application.ControlCenter
{
    internal class AddRobotService : AddRobotUseCase
    {
        private readonly Domain.ControlCenter.ControlCenter controlCenter;
        private RobotFactory robotFactory;
        private readonly IRobotRepository robotRepository;

        public AddRobotService()
        {
            controlCenter = Domain.ControlCenter.ControlCenter.GetInstance();
            robotRepository = RobotRepository.GetInstance();
        }

        public void AddRobot(RobotVariant robotVariant)
        {
            if (robotRepository.GetRobotCount() < controlCenter.MaxRobots)
            {
                robotFactory = robotVariant switch
                {
                    RobotVariant.DEFAULT => new DefaultRobotFactory(),
                    RobotVariant.SCOUT => new ScoutBotFactory(),
                    RobotVariant.MUD => new MudBotFactory(),
                    RobotVariant.LAVA => new LavaBotFactory(),
                    RobotVariant.AQUA => new AquaBotFactory()
                };

                var robotBase = robotFactory.CreateRobot(controlCenter.exoPlanet, controlCenter.GetRobotIDandIncrement());

                robotRepository.AddRobot(robotBase, null);
            }
            else
            {
                throw new RobotCapacityReachException("The maximum number of available Robots has been reached.");
            }
        }
    }
}