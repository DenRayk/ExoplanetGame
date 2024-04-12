using ExoplanetGame.Robot;
using System.Threading;

namespace ExoplanetGame.Exoplanet
{
    internal class RobotHeatTracker
    {
        private const int HEAT_PER_ACTION = 10;
        private const int COOL_DOWN_RATE = 10;
        private Dictionary<RobotBase, int> heatLevels = new();

        public void PerformAction(RobotBase robot)
        {
            if (Console.IsOutputRedirected == false)
            {
                Console.Clear();
            }

            if (!heatLevels.ContainsKey(robot))
            {
                heatLevels[robot] = 0;
            }

            heatLevels[robot] += HEAT_PER_ACTION;

            if (heatLevels[robot] >= robot.MaxHeat)
            {
                Console.WriteLine($"Overheating {robot.GetLanderName()}");
                CoolDown(robot, robot.MaxHeat / 10);
            }
        }

        private void CoolDown(RobotBase robot, int seconds)
        {
            for (int i = 0; i < seconds; i++)
            {
                Console.WriteLine($"Cooling down {robot.GetLanderName()} Heat: {heatLevels[robot]}");
                if (heatLevels[robot] > 0)
                {
                    heatLevels[robot] -= COOL_DOWN_RATE;
                }
                Thread.Sleep(1000);
            }
        }
    }
}