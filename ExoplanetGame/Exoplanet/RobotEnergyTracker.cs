using ExoplanetGame.Robot;

namespace ExoplanetGame.Exoplanet;

public class RobotEnergyTracker
{
    private readonly Dictionary<RobotBase, int> robotEnergy = new();

    public void ConsumeEnergy(RobotBase robot, int energy)
    {
        if (!robotEnergy.ContainsKey(robot))
        {
            robotEnergy.Add(robot, robot.Energy);
        }

        robotEnergy[robot] -= energy;
    }

    public void LoadEnergy(RobotBase robot, int energy)
    {
        if (!robotEnergy.ContainsKey(robot))
        {
            robotEnergy.Add(robot, robot.Energy);
        }

        robotEnergy[robot] += energy;
    }

    public int GetEnergy(RobotBase robot)
    {
        if (robotEnergy.ContainsKey(robot))
        {
            return robotEnergy[robot];
        }

        return 0;
    }
}