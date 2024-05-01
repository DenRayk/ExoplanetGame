using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Robot;

namespace ExoplanetGame.Application.Robot
{
    public interface RobotRotateUseCase
    {
        RotationResult Rotate(RobotBase robot, Rotation rotation);
    }
}