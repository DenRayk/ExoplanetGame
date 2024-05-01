using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Robot
{
    public interface RobotPartsHealthUseCase
    {
        Dictionary<RobotPart, int> GetRobotPartsByRobot(RobotBase robotBase);

        void RepairRobotPart(RobotBase robotBase, RobotPart robotPart);
    }
}