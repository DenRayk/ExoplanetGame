using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot.Factory;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Application.ControlCenter
{
    internal class AddRobotService : AddRobotUseCase
    {
        private readonly global::ExoplanetGame.ControlCenter.ControlCenter controlCenter;
        private readonly RobotFactory robotFactory;
        private readonly RobotRepository robotRepository;

        public AddRobotService()
        {
            controlCenter = global::ExoplanetGame.ControlCenter.ControlCenter.GetInstance();
            robotFactory = RobotFactory.GetInstance();
            robotRepository = RobotRepository.GetInstance();
        }

        public void AddRobot(RobotVariant robotVariant)
        {
            if (robotRepository.GetRobotCount() < controlCenter.MaxRobots)
            {
                var robotBase = robotFactory.CreateRobot(controlCenter, controlCenter.exoPlanet, controlCenter.getRobotIDandIncrement(), robotVariant);

                robotRepository.AddRobot(robotBase, null);
            }
            else
            {
                throw new RobotCapacityReachException("The maximum number of available Robots has been reached.");
            }
        }
    }
}