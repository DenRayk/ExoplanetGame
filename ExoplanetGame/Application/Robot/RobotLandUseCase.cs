using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Application.Robot;

public interface RobotLandUseCase
{
    PositionResult LandRobot(RobotBase robotBase, Position landPosition);
}