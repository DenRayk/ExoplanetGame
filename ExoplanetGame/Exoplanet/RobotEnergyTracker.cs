using ExoplanetGame.Robot;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Exoplanet;

public class RobotEnergyTracker
{
    private readonly Dictionary<RobotBase, int> robotEnergy = new();

    public void ConsumeEnergy(RobotBase robot, RobotAction robotAction)
    {
        if (!robotEnergy.ContainsKey(robot))
        {
            robotEnergy.Add(robot, robot.RobotInformation.MaxEnergy);
        }

        int energyConsumed = CalculateEneryConsumtion(robotAction);

        robotEnergy[robot] -= energyConsumed;

        WarningAtLowEnery(robot);
    }

    public LoadResult LoadEnergy(RobotBase robot, int seconds, Weather weather)
    {
        LoadResult loadResult = new();

        if (!robotEnergy.ContainsKey(robot))
        {
            robotEnergy.Add(robot, robot.RobotInformation.MaxEnergy);
        }
        else
        {
            int energyLoad = CalculateEneryLoad(weather);

            for (int i = 0; i < seconds; i++)
            {
                if (robotEnergy[robot] < robot.RobotInformation.MaxEnergy)
                {
                    robotEnergy[robot] += energyLoad;

                    loadResult.IsSuccess = true;
                    loadResult.HasRobotSurvived = true;
                    loadResult.Message = $"Robot energy loaded to {robotEnergy[robot]}%";
                    loadResult.EnergyLoad = robotEnergy[robot];
                }
                else
                {
                    loadResult.IsSuccess = true;
                    loadResult.HasRobotSurvived = true;
                    loadResult.Message = $"Robot fully loaded to {robot.RobotInformation.MaxEnergy}%";
                    loadResult.EnergyLoad = robot.RobotInformation.MaxEnergy;
                    break;
                }
                Thread.Sleep(1000);
            }
        }
        return loadResult;
    }

    public int GetRobotEnergy(RobotBase robot)
    {
        if (!robotEnergy.ContainsKey(robot))
        {
            robotEnergy.Add(robot, robot.RobotInformation.MaxEnergy);
        }

        return robotEnergy[robot];
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

    private void WarningAtLowEnery(RobotBase robot)
    {
        if (GetRobotEnergy(robot) < 20)
        {
            Console.WriteLine($"Warning: Robot {robot.RobotInformation.RobotID} at low energy.");
        }
    }
}