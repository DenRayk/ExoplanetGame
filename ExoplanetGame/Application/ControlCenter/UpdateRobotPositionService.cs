using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot;

namespace ExoplanetGame.Application.ControlCenter
{
    internal class UpdateRobotPositionService : UpdateRobotPositionUseCase
    {
        private RobotRepository robotRepository;
        private global::ExoplanetGame.ControlCenter.ControlCenter controlCenter;

        public UpdateRobotPositionService()
        {
            robotRepository = RobotRepository.GetInstance();
            controlCenter = global::ExoplanetGame.ControlCenter.ControlCenter.GetInstance();
        }

        public void UpdateRobotPosition(RobotBase robot, Position position)
        {
            robotRepository.MoveRobot(robot, position);

            controlCenter.OnRobotPositionUpdated(robot, position);
        }
    }
}