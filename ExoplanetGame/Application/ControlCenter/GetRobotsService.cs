using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Application.ControlCenter
{
    public class GetRobotsService
    {
        private RobotRepository robotRepository;

        public GetRobotsService()
        {
            robotRepository = RobotRepository.GetInstance();
        }

        public Dictionary<RobotBase, Position> GetAllRobots()
        {
            return robotRepository.GetRobots();
        }
    }
}