using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Robot.Variants
{
    public class AquaBot : RobotBase
    {
        public AquaBot(ControlCenter.ControlCenter controlCenter, ExoplanetBase exoPlanet, int robotId) : base(exoPlanet, controlCenter, robotId)
        {
            RobotVariant = RobotVariant.AQUA;
        }

        public override Position Land(Position landPosition)
        {
            landPosition = exoPlanet.LandAquaBot(this, landPosition);

            if (landPosition != null)
            {
                RobotStatus.HasLanded = true;
            }

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

            return landPosition;
        }

        public override Position Move()
        {
            if (DoesOtherRobotBlocksMove())
            {
                Console.WriteLine("Robot cannot move because another robot is blocking the way");
                return RobotStatus.Position;
            }

            Position newPosition = exoPlanet.MoveAquaBot(this);
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
    }
}