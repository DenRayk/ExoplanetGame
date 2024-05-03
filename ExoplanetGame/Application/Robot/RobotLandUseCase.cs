using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Robot;

public interface RobotLandUseCase
{
    PositionResult LandRobot(IRobot robot, Position landPosition);
}