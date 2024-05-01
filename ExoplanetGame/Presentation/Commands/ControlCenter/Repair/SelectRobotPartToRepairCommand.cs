﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Helper;

namespace ExoplanetGame.Presentation.Commands.ControlCenter.Repair
{
    internal class SelectRobotPartToRepairCommand : RobotCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;
        private ControlCenterCommand controlCenterCommand;

        public SelectRobotPartToRepairCommand(BaseCommand previousCommand, ControlCenterCommand controlCenterCommand, RobotBase robotBase, UCCollection ucCollection) : base(previousCommand, controlCenterCommand)
        {
            this.controlCenterCommand = controlCenterCommand;
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            var robotParts = ucCollection.UcCollectionRobot.RobotPartsHealthService.GetRobotPartsByRobot(robotBase);

            if (robotParts != null)
            {
                Console.WriteLine($"{robotBase.GetLanderName()} has used the following parts:");
                Console.WriteLine("Select one to repair it \n");
                var options = getRobotPartOptions(robotParts);

                BaseCommand baseCommand = ReadUserInputWithOptions(options);
                baseCommand.Execute();
            }
            Console.WriteLine($"{robotBase.GetLanderName()} has not used any parts yet. \n");

            controlCenterCommand.Execute();
        }

        private Dictionary<string, BaseCommand> getRobotPartOptions(Dictionary<RobotPart, int> robotParts)
        {
            Dictionary<string, BaseCommand> options = new();
            foreach (var robotPart in robotParts)
            {
                options.Add($"{robotPart.Key.GetDescriptionFromEnum(),-15} - Durability {GetDescriptionFromPartDurability(robotPart.Value)}", new RepairRobotPartCommand(previousCommand, controlCenterCommand, robotBase, robotPart.Key, ucCollection));
            }
            return options;
        }

        private string GetDescriptionFromPartDurability(int durability)
        {
            switch (durability)
            {
                case <= 0:
                    return "Broken";

                case <= 25:
                    return "Critical";

                case <= 50:
                    return "Damaged";

                case <= 75:
                    return "Worn";

                case <= 90:
                    return "Good";

                default:
                    return "Like new";
            }
        }
    }
}