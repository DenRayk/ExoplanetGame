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

            switch (randomAttack)
            {
                case <= 20:
                    return "a sudden surge of untraceable energy";

                case <= 40:
                    return "a trap set in the trees";

                case <= 60:
                    return "a large animal";

                case <= 80:
                    return "a mysterious jungle creature";

                default:
                    return "a mysterious attack";
            }
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