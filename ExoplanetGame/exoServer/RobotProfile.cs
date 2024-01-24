using System.Text;

namespace Exoplanet.exoServer;

[Serializable]
public class RobotProfil
{
    private static readonly long serialVersionUID = 2L;
    private int[] partStatus;
    private Dictionary<RobotAttribute, float> attr;

    public RobotProfil()
    {
        RobotPart[] parts = (RobotPart[])Enum.GetValues(typeof(RobotPart));
        partStatus = new int[parts.Length];

        for (int i = 0; i < parts.Length; ++i)
        {
            partStatus[i] = 100;
        }

        attr = new Dictionary<RobotAttribute, float>
        {
            { RobotAttribute.age, 0.0f },
            { RobotAttribute.eMinWarn, 15.0f },
            { RobotAttribute.eOK, 60.0f },
            { RobotAttribute.evCool, 2.0f },
            { RobotAttribute.evHeat, 2.0f },
            { RobotAttribute.evMove, 1.0f },
            { RobotAttribute.evRotate, 0.5f },
            { RobotAttribute.tMaxCrash, 280.0f },
            { RobotAttribute.tMaxWarn, 150.0f },
            { RobotAttribute.tMinCrash, -120.0f },
            { RobotAttribute.tMinWarn, -50.0f },
            { RobotAttribute.failMove, 0.05f },
            { RobotAttribute.failRotL, 0.1f },
            { RobotAttribute.failRotR, 0.1f },
            { RobotAttribute.failCooler, 0.1f },
            { RobotAttribute.failHeater, 0.1f },
            { RobotAttribute.failSensor, 0.02f },
            { RobotAttribute.delayRemote, 3.0f },
            { RobotAttribute.delayPlugIn, 1.0f }
        };
    }

    public float GetAttr(RobotAttribute RobotAttribute)
    {
        return attr.GetValueOrDefault(RobotAttribute, 0.0f);
    }

    public void SetAttr(RobotAttribute RobotAttribute, float value)
    {
        attr[RobotAttribute] = value;
    }

    public int GetStatus(RobotPart RobotPart)
    {
        return partStatus[(int)RobotPart];
    }

    public void ReduceStatus(RobotPart RobotPart, float factor)
    {
        partStatus[(int)RobotPart] -= (int)(100.0f * factor);

        if (partStatus[(int)RobotPart] < 0)
        {
            partStatus[(int)RobotPart] = 0;
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("PART-STATUS=");

        for (int i = 0; i < partStatus.Length; ++i)
        {
            if (i > 0)
            {
                sb.Append('|');
            }

            sb.Append(partStatus[i]);
        }

        sb.Append('\n');

        foreach (KeyValuePair<RobotAttribute, float> kvp in attr)
        {
            sb.Append(kvp.Key.ToString());
            sb.Append('=');
            sb.Append(kvp.Value);
            sb.Append('\n');
        }

        return sb.ToString();
    }

    public static RobotProfil Parse(string s)
    {
        string[] token = s.Trim().Split("\n");
        RobotProfil rp = new();

        if (token.Length <= 0) return rp;

        string[] p = token[0].Split("=");

        if (p[0].Equals("PART-STATUS"))
        {
            string[] val = p[1].Split('|');

            for (int i = 0; i < val.Length; ++i)
            {
                rp.partStatus[i] = int.Parse(val[i]);
            }
        }

        for (int i = 1; i < token.Length; ++i)
        {
            p = token[i].Split('=');
            RobotAttribute key = Enum.Parse<RobotAttribute>(p[0]);
            rp.attr[key] = float.Parse(p[1]);
        }

        return rp;
    }

    public static void Main(string[] args)
    {
        RobotProfil rp = new();
        Console.WriteLine(rp.ToString());
        rp = Parse(rp.ToString());
        Console.WriteLine(rp.ToString());
    }
}