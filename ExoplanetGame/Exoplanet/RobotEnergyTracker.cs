using ExoplanetGame.Robot;

namespace ExoplanetGame.Exoplanet;

public class RobotEnergyTracker
{
    private readonly Dictionary<RobotBase, int> robotEnergy = new();

    public void ConsumeEnergy(RobotBase robot, RobotAction robotAction)
    {
        if (!robotEnergy.ContainsKey(robot))
        {
            robotEnergy.Add(robot, robot.Energy);
        }

        int energyConsumed = CalculateEneryConsumtion(robotAction);

        robotEnergy[robot] -= energyConsumed;
    }

    public int GetEnergy(RobotBase robot)
    {
        if (robotEnergy.ContainsKey(robot))
        {
            return robotEnergy[robot];
        }

        return 0;
    }

    public void LoadEnergy(RobotBase robot)
    {
        if (!robotEnergy.ContainsKey(robot))
        {
            robotEnergy.Add(robot, robot.Energy);
        }

        robotEnergy[robot] = 100;
    }

    private int CalculateEneryConsumtion(RobotAction robotAction)
    {
        switch (robotAction)
        {
            case RobotAction.LAND:
                return 5;

            case RobotAction.MOVE:
                return 2;

            case RobotAction.ROTATE:
                return 1;

            case RobotAction.SCAN:
                return 2;

            default:
                return 0;
        }
    }
}