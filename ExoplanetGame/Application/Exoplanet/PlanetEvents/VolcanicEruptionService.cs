using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Exoplanet.PlanetEvents
{
    internal class VolcanicEruptionService : VolcanicEruptionUseCase
    {
        private static readonly int volcanicEruptionChance = 5;

        public bool HandleVolcanicEruption(RobotBase robot, out RobotResultBase robotResult)
        {
            robotResult = new RobotResultBase();

            if (!VolcanicEruption())
                return false;

            robotResult = new PositionResult()
            {
                IsSuccess = false,
                HasRobotSurvived = false,
                Message = $"{robot.GetLanderName()} was destroyed by a volcanic eruption."
            };
            return true;
        }

        private bool VolcanicEruption()
        {
            if (!DoesVolcanicEruptionHappen())
                return false;

            return true;
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