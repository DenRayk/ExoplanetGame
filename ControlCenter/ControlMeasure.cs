using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlCenter
{
    public class ControlMeasure : Measure
    {
        public Position position { get; set; }

        public ControlMeasure(Position position, Ground ground) : base(ground)
        {
            this.position = position;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append("CONTROLMEASURE|");
            sb.Append(position.ToString());
            sb.Append("|");
            sb.Append(Ground.ToString());
            return sb.ToString();
        }

        public new static ControlMeasure Parse(string data)
        {
            string[] parts = data.Split('|');
            if (parts.Length != 4)
            {
                throw new ArgumentException("Invalid measure data: " + data);
            }

            if (parts[0] != "MEASURE")
            {
                throw new ArgumentException("Invalid measure data: " + data);
            }
            return new ControlMeasure(new Position(int.Parse(parts[2]), int.Parse(parts[3])), (Ground)Enum.Parse(typeof(Ground), parts[1]));
        }
    }
}