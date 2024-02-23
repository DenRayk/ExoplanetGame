namespace RemoteRobot.exo;

using System;
using System.Text;

public class Measure(Ground ground)
{
    public Ground Ground { get; set; }

    public Measure() : this(Ground.NICHTS)
    {
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("MEASURE|");
        sb.Append(Ground.ToString());
        return sb.ToString();
    }

    public Measure Parse(string data)
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
        return (Measure)Enum.Parse(typeof(Ground), parts[1]);
    }
}