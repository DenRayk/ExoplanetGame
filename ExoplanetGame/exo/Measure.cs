namespace Exoplanet.exo;

using System;
using System.Text;

[Serializable]
public class Measure(Ground ground, float temperature)
{
    protected Ground ground = ground;
    protected float temperature = temperature;
    public static readonly float TEMP_UNKNOWN = -999.9F;

    public Measure(Ground ground) : this(ground, -999.9F)
    {
    }

    public Measure() : this(Ground.NICHTS, TEMP_UNKNOWN)
    {
    }

    public Ground getGround()
    {
        return ground;
    }

    public float getTemperature()
    {
        return temperature;
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("MEASURE|");
        sb.Append(ground.ToString());
        sb.Append('|');
        sb.Append(temperature);
        return sb.ToString();
    }

    public static Measure? Parse(string s)
    {
        string[] token = s.Trim().Split('|');
        if (token is not ["MEASURE", _, _]) return null;
        try
        {
            Ground g = Enum.Parse<Ground>(token[1]);
            float temp = float.Parse(token[2]);
            return new Measure(g, temp);
        }
        catch (Exception)
        {
            Console.WriteLine("Can't parse measurement");
        }

        return null;
    }
}