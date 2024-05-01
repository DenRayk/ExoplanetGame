﻿using ExoplanetGame.Domain.Exoplanet.Environment;
using System.Text;

namespace ExoplanetGame.Domain.ControlCenter
{
    public class Measure
    {
        public Ground Ground { get; set; }
        public double Temperature { get; set; } // Temperature property

        public Measure()
        {
            Ground = Ground.NOTHING;
            Temperature = 0.0; // Default temperature value
        }

        public Measure(Ground ground, double temperature)
        {
            Ground = ground;
            Temperature = temperature;
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

        public static Measure Parse(string data)
        {
            string[] parts = data.Split('|');
            if (parts.Length != 3) // Check if parts contain both Ground and Temperature
            {
                throw new ArgumentException("Invalid measure data: " + data);
            }

            if (parts[0] != "MEASURE")
            {
                throw new ArgumentException("Invalid measure data: " + data);
            }

            // Parse temperature value
            if (!double.TryParse(parts[2], out double temperature))
            {
                throw new ArgumentException("Invalid temperature value: " + parts[2]);
            }

            return new Measure((Ground)Enum.Parse(typeof(Ground), parts[1]), temperature);
        }
    }
}