using ExoplanetGame.Robot;
using Timer = System.Timers.Timer;

namespace ExoplanetGame.Exoplanet;

public class RobotEnergyTracker
{
    private readonly Dictionary<RobotBase, int> robotEnergy = new();

    public void ConsumeEnergy(RobotBase robot, RobotAction robotAction)
    {
        if (!robotEnergy.ContainsKey(robot))
        {
            robotEnergy.Add(robot, robot.MaxEnergy);
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

    public void LoadEnergy(RobotBase robot, int seconds)
    {
        if (!robotEnergy.ContainsKey(robot))
        {
            robotEnergy.Add(robot, robot.MaxEnergy);
        }
        else
        {
            for (int i = 0; i < seconds; i++)
            {
                if (robotEnergy[robot] < robot.MaxEnergy)
                {
                    Console.WriteLine($"Robot energy loaded to {robotEnergy[robot]}%");
                    robotEnergy[robot] += 10;
                }
                else
                {
                    Console.WriteLine($"Robot fully loaded to {robot.MaxEnergy}%");
                    break;
                }
                Thread.Sleep(1000);
            }
        }
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