using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteRobot.exo;

namespace RemoteRobot.message
{
    internal class MessageData
    {
        private Ground ground;
        private PlanetSize planetSize;
        private Position position;
        private Measure measure;
        private Rotation rotation;
        private RobotStatus robotstatus;
        private Direction direction;

        public Direction GetDirection()
        {
            return direction;
        }

        public void SetDirection(Direction direction)
        {
            this.direction = direction;
        }

        public Ground GetGround()
        {
            return ground;
        }

        public void SetGround(Ground ground)
        {
            this.ground = ground;
        }

        public PlanetSize GetSize()
        {
            return planetSize;
        }

        public void SetSize(PlanetSize planetSize)
        {
            this.planetSize = planetSize;
        }

        public Position GetPosition()
        {
            return position;
        }

        public void SetPosition(Position position)
        {
            this.position = position;
        }

        public Measure GetMeasure()
        {
            return measure;
        }

        public void SetMeasure(Measure measure)
        {
            this.measure = measure;
        }

        public Rotation GetRotation()
        {
            return rotation;
        }

        public void SetRotation(Rotation rotation)
        {
            this.rotation = rotation;
        }

        public RobotStatus GetRobotstatus()
        {
            return robotstatus;
        }

        public void SetRobotstatus(RobotStatus robotstatus)
        {
            this.robotstatus = robotstatus;
        }
    }
}