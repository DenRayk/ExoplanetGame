using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Robot;

public interface RobotLandUseCase
{
    PositionResult LandRobot(RobotBase robotBase, Position landPosition);
}