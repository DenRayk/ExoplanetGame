using ExoplanetGame.Robot;
using System.Threading;

namespace ExoplanetGame.Exoplanet
{
    public class RobotHeatTracker
    {
        private const int COOL_DOWN_RATE = 10;
        private Dictionary<RobotBase, double> heatLevels = new();

        public void PerformAction(RobotBase robot, RobotAction robotAction, Topography topography)
        {
            if (!heatLevels.ContainsKey(robot))
            {
                heatLevels[robot] = 0;
            }

            double heatGain = CalculateHeatGainDependentOnFieldAndAction(robot, robotAction, topography);

            heatLevels[robot] += heatGain;

            if (heatLevels[robot] >= robot.MaxHeat)
            {
                Console.WriteLine($"Overheating {robot.GetLanderName()}");
                CoolDown(robot, robot.MaxHeat / 10);
            }
        }

        private double CalculateHeatGainDependentOnFieldAndAction(RobotBase robot, RobotAction robotAction, Topography topography)
        {
            double heatGain;

            switch (topography.GetMeasureAtPosition(robot.RobotStatus.Position).Ground)
            {
                case Ground.WATER:
                    heatGain = 0;
                    break;

                case Ground.ROCK:
                    heatGain = 5;
                    break;

                case Ground.SAND:
                    heatGain = 10;
                    break;

                case Ground.LAVA:
                    heatGain = 20;
                    break;

                case Ground.GRAVEL:
                    heatGain = 5;
                    break;

                case Ground.PLANT:
                    heatGain = 5;
                    break;

                case Ground.MUD:
                    heatGain = 8;
                    break;

                default:
                    heatGain = 0;
                    break;
            }

            switch (robotAction)
            {
                case RobotAction.LAND:
                    heatGain *= 1.5;
                    break;

                case RobotAction.MOVE:
                    heatGain *= 1.2;
                    break;

                case RobotAction.ROTATE:
                    heatGain *= 1.1;
                    break;

                case RobotAction.SCAN:
                    heatGain *= 1.3;
                    break;

                case RobotAction.GETPOSITION:
                    heatGain *= 1.1;
                    break;
            }

            return heatGain;
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
                Thread.Sleep(200);
            }

            Console.Clear();
        }

        public void WaterCoolDown(RobotBase robot)
        {
            if (heatLevels.ContainsKey(robot))
            {
                heatLevels[robot] = 0;
            }
        }
    }
}