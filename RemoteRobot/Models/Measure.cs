using RemoteRobot.Models;

namespace RemoteRobot;

using System;
using System.Text;

public class Measure
{
    public Ground Ground { get; set; }

    public Measure()
    {
        Ground = Ground.NICHTS;
    }

    public Measure(Ground ground)
    {
        Ground = ground;
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("MEASURE|");
        sb.Append(Ground.ToString());
        return sb.ToString();
    }

    public static Measure Parse(string data)
    {
        string[] parts = data.Split('|');
        if (parts.Length != 2)
        {
            throw new ArgumentException("Invalid measure data: " + data);
        }

        if (parts[0] != "MEASURE")
        {
            throw new ArgumentException("Invalid measure data: " + data);
        }
        return new Measure((Ground)Enum.Parse(typeof(Ground), parts[1]));
    }
}