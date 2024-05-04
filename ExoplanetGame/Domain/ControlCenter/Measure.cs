using ExoplanetGame.Domain.Exoplanet.Environment;
using System.Text;

namespace ExoplanetGame.Domain.ControlCenter
{
    public class Measure
    {
        public Ground Ground { get; }
        public double Temperature { get; }

        public Measure()
        {
            Ground = Ground.NOTHING;
            Temperature = 0.0;
        }

        public Measure(Ground ground, double temperature)
        {
            Ground = ground;
            Temperature = temperature;
        }

        protected bool Equals(Measure other)
        {
            return Ground == other.Ground && Temperature.Equals(other.Temperature);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Measure)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)Ground, Temperature);
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append("MEASURE");
            sb.Append("|Ground: ");
            sb.Append(Ground);
            sb.Append(", ");
            sb.Append("Temperature: ");
            sb.Append(Temperature.ToString("0.##"));
            sb.Append("°C");
            return sb.ToString();
        }
    }
}