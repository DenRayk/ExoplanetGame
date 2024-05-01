using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.ControlCenter;
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