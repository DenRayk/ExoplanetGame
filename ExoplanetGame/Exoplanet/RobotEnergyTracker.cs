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

    public void LoadEnergy(RobotBase robot, int seconds, Weather weather)
    {
        if (!robotEnergy.ContainsKey(robot))
        {
            robotEnergy.Add(robot, robot.MaxEnergy);
        }
        else
        {
            int energyLoad = CalculateEneryLoad(weather);

            for (int i = 0; i < seconds; i++)
            {
                if (robotEnergy[robot] < robot.MaxEnergy)
                {
                    Console.WriteLine($"Robot energy loaded to {robotEnergy[robot]}%");
                    robotEnergy[robot] += energyLoad;
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

    private int CalculateEneryLoad(Weather weather)
    {
        switch (weather)
        {
            case Weather.SUNNY:
                return 10;

            case Weather.WINDY:
                return 10;

            case Weather.CLOUDY:
                return 3;

            case Weather.RAINY:
                return 3;

            case Weather.FOGGY:
                return 2;

            case Weather.ASH_IN_THE_AIR:
                return 2;

            default:
                return 5;
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