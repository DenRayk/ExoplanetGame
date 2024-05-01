using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Application.Exoplanet
{
    public interface RobotPostionUseCase
    {
        bool IsPositionInBounds(Position position, Topography topography);

        PositionResult GetRobotPosition(RobotBase robot);

        void RemoveRobot(RobotBase robot);

        bool IsPositionSafeForRobot(RobotBase robot, Position newPosition, Topography topography, ref PositionResult positionResult);

        void UpdateRobotPosition(RobotBase robot, Position newPosition);

        Position WaterDrift(RobotBase robot, Position position, Topography topography);
    }
}