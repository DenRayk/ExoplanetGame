using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Exoplanet
{
    public interface RobotPostionUseCase
    {
        bool IsPositionInBounds(Position position, Topography topography);

        PositionResult GetRobotPosition(IRobot robot);

        void RemoveRobot(IRobot robot);

        bool IsPositionSafeForRobot(IRobot robot, Position newPosition, Topography topography,
            ref PositionResult positionResult);

        void UpdateRobotPosition(RobotBase robot, Position newPosition);

        Position WaterDrift(IRobot robot, Position position, Topography topography);
    }
}