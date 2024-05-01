using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;

namespace ExoplanetGame.Exoplanet.Tracker
{
    public class RobotHeatTracker
    {
        private const int COOL_DOWN_RATE = 10;
        public Dictionary<RobotBase, double> HeatLevels { get; } = new();

        public void PerformAction(RobotBase robot, RobotAction robotAction, Topography topography)
        {
            if (!HeatLevels.ContainsKey(robot))
            {
                HeatLevels[robot] = 0;
            }

            double heatGain = CalculateHeatGainDependentOnFieldAndAction(robot, robotAction, topography);

            HeatLevels[robot] += heatGain;

            if (HeatLevels[robot] >= robot.RobotInformation.MaxHeat)
            {
                Console.WriteLine($"Overheating {robot.GetLanderName()}");
                CoolDown(robot, robot.RobotInformation.MaxHeat / 10);
            }
        }

        public void PerformAction(RobotBase robot, RobotAction robotAction, Topography topography, Position landPosition)
        {
            if (!HeatLevels.ContainsKey(robot))
            {
                HeatLevels[robot] = 0;
            }

            double heatGain = CalculateHeatGainDependentOnFieldAndAction(landPosition, robotAction, topography);

            HeatLevels[robot] += heatGain;

            if (HeatLevels[robot] >= robot.RobotInformation.MaxHeat)
            {
                Console.WriteLine($"Overheating {robot.GetLanderName()}");
                CoolDown(robot, robot.RobotInformation.MaxHeat / 10);
            }
        }

        public void WaterCoolDown(RobotBase robot)
        {
            if (HeatLevels.ContainsKey(robot))
            {
                HeatLevels[robot] = 0;
            }
        }

        private double CalculateHeatGainDependentOnFieldAndAction(RobotBase robot, RobotAction robotAction, Topography topography)
        {
            return CalculateHeatGainDependentOnFieldAndAction(robot.RobotInformation.Position, robotAction, topography);
        }

        private double CalculateHeatGainDependentOnFieldAndAction(Position landPosition, RobotAction robotAction, Topography topography)
        {
            double heatGain;

            switch (topography.GetMeasureAtPosition(landPosition).Ground)
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

                case Ground.SNOW:
                    heatGain = 0;
                    break;

                case Ground.ICE:
                    heatGain = 0;
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
                Console.WriteLine($"Cooling down {robot.GetLanderName()} Heat: {Math.Round(HeatLevels[robot], 2)}");
                if (HeatLevels[robot] > 0)
                {
                    HeatLevels[robot] -= COOL_DOWN_RATE;
                }
                Thread.Sleep(200);
            }

            Console.Clear();
        }
    }
}