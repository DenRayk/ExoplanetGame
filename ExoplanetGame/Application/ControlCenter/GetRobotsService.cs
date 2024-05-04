using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Application.ControlCenter
{
    public class GetRobotsService
    {
        private IRobotRepository robotRepository;

        public GetRobotsService()
        {
            robotRepository = RobotRepository.GetInstance();
        }

        public Dictionary<IRobot, Position> GetAllRobots()
        {
            return robotRepository.GetRobots();
        }
    }
}