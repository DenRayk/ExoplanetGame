using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot.Factory;
using ExoplanetGame.Domain.Robot.Variants;

namespace ExoplanetGame.Application.ControlCenter
{
    internal class AddRobotService : AddRobotUseCase
    {
        private readonly Domain.ControlCenter.ControlCenter controlCenter;
        private readonly RobotFactory robotFactory;
        private readonly IRobotRepository robotRepository;

        public AddRobotService()
        {
            controlCenter = Domain.ControlCenter.ControlCenter.GetInstance();
            robotFactory = RobotFactory.GetInstance();
            robotRepository = RobotRepository.GetInstance();
        }

        public void AddRobot(RobotVariant robotVariant)
        {
            if (robotRepository.GetRobotCount() < controlCenter.MaxRobots)
            {
                var robotBase = robotFactory.CreateRobot(controlCenter, controlCenter.exoPlanet, controlCenter.GetRobotIDandIncrement(), robotVariant);

                robotRepository.AddRobot(robotBase, null);
            }
            else
            {
                throw new RobotCapacityReachException("The maximum number of available Robots has been reached.");
            }
        }
    }
}