using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Exoplanet.PlanetEvents
{
    internal class RandomAttackService : RandomAttackUseCase
    {
        private static readonly int mysteriousAttackChance = 5;

        public RobotResultBase HandleMysteriousAttack(IRobot robot)
        {
            RobotResultBase robotResult = new RobotResultBase();

            if (!DoesMysteriousAttackHappen())
            {
                robotResult.IsSuccess = true;
                robotResult.HasRobotSurvived = true;
                return robotResult;
            }

            string typeOfAttack = GetRandomAttackType();

            robotResult = new PositionResult()
            {
                IsSuccess = false,
                HasRobotSurvived = false,
                Message = $"{robot.GetLanderName()} was destroyed by {typeOfAttack}."
            };

            return robotResult;
        }

        private string GetRandomAttackType()
        {
            Random random = new();
            int randomAttack = random.Next(1, 101);

            return randomAttack switch
            {
                <= 20 => "a sudden surge of untraceable energy",
                <= 40 => "a trap set in the trees",
                <= 60 => "a large animal",
                <= 80 => "a mysterious jungle creature",
                _ => "a mysterious attack"
            };
        }

        private bool DoesMysteriousAttackHappen()
        {
            Random random = new();
            int randomEruption = random.Next(1, 101);
            bool isVolcanicEruption = randomEruption <= mysteriousAttackChance;

            return isVolcanicEruption;
        }
    }
}