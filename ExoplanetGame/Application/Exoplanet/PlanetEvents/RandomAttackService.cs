﻿using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Exoplanet.PlanetEvents
{
    internal class RandomAttackService : RandomAttackUseCase
    {
        private static readonly int mysteriousAttackChance = 50;

        public bool HandleMysteriousAttack(RobotBase robot, out RobotResultBase robotResult)
        {
            robotResult = new RobotResultBase();

            if (!MysteriousAttack())
                return false;

            string typeOfAttack = GetRandomAttackType();

            robotResult = new PositionResult()
            {
                IsSuccess = false,
                HasRobotSurvived = false,
                Message = $"{robot.GetLanderName()} was destroyed by {typeOfAttack}."
            };

            return true;
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

        private bool MysteriousAttack()
        {
            if (!DoesMysteriousAttackHappen())
                return false;

            return true;
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