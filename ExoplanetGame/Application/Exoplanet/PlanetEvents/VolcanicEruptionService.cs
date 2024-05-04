using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Exoplanet.PlanetEvents
{
    internal class VolcanicEruptionService : VolcanicEruptionUseCase
    {
        private static readonly int volcanicEruptionChance = 5;

        public RobotResultBase HandleVolcanicEruption(IRobot robot)
        {
            RobotResultBase robotResult = new RobotResultBase();

            if (!DoesVolcanicEruptionHappen())
            {
                robotResult.IsSuccess = true;
                robotResult.HasRobotSurvived = true;
                return robotResult;
            }

            robotResult = new RobotResultBase()
            {
                IsSuccess = false,
                HasRobotSurvived = false,
                Message = $"{robot.GetLanderName()} was destroyed by a volcanic eruption."
            };
            return robotResult;
        }

        public bool DoesVolcanicEruptionHappen()
        {
            Random random = new();
            int randomEruption = random.Next(1, 101);
            bool isVolcanicEruption = randomEruption <= volcanicEruptionChance;

            return isVolcanicEruption;
        }
    }
}