﻿using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Robot
{
    public class RobotBase
    {
        protected IExoplanet exoPlanet;
        protected ControlCenter.ControlCenter controlCenter;
        public RobotStatus RobotStatus { get; set; }
        public int MaxHeat { get; set; } = 100;
        public RobotVariant RobotVariant { get; set; }

        protected RobotBase(IExoplanet exoPlanet, ControlCenter.ControlCenter controlCenter, int robotID)
        {
            this.exoPlanet = exoPlanet;
            this.controlCenter = controlCenter;
            controlCenter.RobotPositionUpdated += HandleOtherRobotPositionUpdated;
            RobotStatus = new RobotStatus
            {
                RobotID = robotID,
                Energy = 100
            };
        }

        public virtual void Crash()
        {
            exoPlanet.RemoveRobot(this);
            controlCenter.RemoveRobot(this);
            Console.WriteLine("Robot crashed");
        }

        public virtual bool Land(Position landPosition)
        {
            RobotStatus.HasLanded = exoPlanet.Land(this, landPosition);

            if (RobotStatus.HasLanded)
            {
                Console.WriteLine($"Robot landed on {landPosition}");
                RobotStatus.Position = landPosition;
                controlCenter.UpdateRobotPosition(this, landPosition);
            }
            else
            {
                Console.WriteLine("Robot could not land");
            }

            return RobotStatus.HasLanded;
        }

        public virtual string GetLanderName()
        {
            return $"Robot {RobotStatus.RobotID} ({RobotVariant})";
        }

        public virtual Measure Scan()
        {
            Measure measure = exoPlanet.Scan(this);
            Console.WriteLine($"Scanned {measure}");
            return measure;
        }

        public virtual Position Move()
        {
            if (DoesOtherRobotBlocksMove())
            {
                Console.WriteLine("Robot cannot move because another robot is blocking the way");
                return RobotStatus.Position;
            }

            Position newPosition = exoPlanet.Move(this);
            if (newPosition != null)
            {
                Console.WriteLine($"Robot moved to {newPosition}");
                RobotStatus.Position = newPosition;
                controlCenter.UpdateRobotPosition(this, newPosition);
            }
            else
            {
                Console.WriteLine("Robot crashed");
                controlCenter.RemoveRobot(this);
            }

            return newPosition;
        }

        public virtual void Rotate(Rotation rotation)
        {
            RobotStatus.Position.Direction = exoPlanet.Rotate(this, rotation);
            controlCenter.UpdateRobotPosition(this, RobotStatus.Position);

            Console.WriteLine($"Robot rotated to {RobotStatus.Position}");
        }

        public virtual bool HasLanded()
        {
            return RobotStatus.Position != null;
        }

        public virtual Position GetPosition()
        {
            return exoPlanet.GetRobotPosition(this);
        }

        private bool DoesOtherRobotBlocksMove()
        {
            foreach (var otherRobot in RobotStatus.OtherRobotPositions.Keys)
            {
                if (RobotStatus.OtherRobotPositions[otherRobot].Equals(RobotStatus.Position.GetAdjacentPosition()))
                {
                    return true;
                }
            }

            return false;
        }

        private void HandleOtherRobotPositionUpdated(object? sender, RobotPositionEventArgs e)
        {
            if (e.Robot.Equals(this))
                return;

            RobotStatus.OtherRobotPositions[e.Robot] = e.NewPosition;
        }
    }
}