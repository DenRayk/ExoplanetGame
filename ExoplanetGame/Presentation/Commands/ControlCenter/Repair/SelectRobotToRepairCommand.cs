﻿using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Presentation.Commands.ControlCenter.Repair
{
    internal class SelectRobotToRepairCommand : BaseCommand
    {
        private readonly UCCollection ucCollection;

        public SelectRobotToRepairCommand(UCCollection ucCollection)
        {
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            Console.WriteLine("Robot Repair Menu");
            Console.WriteLine("Select a robot to repair: \n");

            var options = GetRobotOptions(ucCollection.UcCollectionControlCenter.GetRobotsService.GetAllRobots());
            if (options.Count != 0)
            {
                BaseCommand baseCommand = ReadUserInputWithOptions(options);

                baseCommand.Execute();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("No robots to repair.");
            }
        }

        private Dictionary<string, BaseCommand> GetRobotOptions(Dictionary<IRobot, Position> getAllRobots)
        {
            Dictionary<string, BaseCommand> options = new();
            foreach (var robot in getAllRobots)
            {
                options.Add(robot.Key.GetLanderName(), new SelectRobotPartToRepairCommand(robot.Key, ucCollection));
            }
            return options;
        }
    }
}