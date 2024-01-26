using Exoplanet.exo;
using Exoplanet.exoServer;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;

public class ExoPlanet : Planet
{
    private const int OP_LAND = 0;
    private const int OP_MOVE = 1;
    private const int OP_ROTATE = 2;
    private const int OP_SCAN = 3;
    private const int OP_CHARGE = 4;

    private string name = "Default-Planet";
    protected bool advanced;
    private bool expert;
    private float adjustFactor = 4.0F;
    private bool noDelay;

    private static float[][] tempTable = new float[][]
    {
        [-100.0F, 50.0F],
        [-50.0F, 50.0F],
        [-20.0F, 30.0F],
        [0.0F, 20.0F],
        [20.0F, 19.0F],
        [50.0F, 30.0F],
        [100.0F, 50.0F],
        [400.0F, 300.0F],
        [800.0F, 400.0F],
        [1100.0F, 300.0F]
    };

    private static float[] tempGround = new float[] { -999.9F, 15.0F, 17.0F, 19.0F, 10.0F, 20.0F, 10.0F, 800.0F };

    private Measure[][]? topo;
    private PlanetSize? planetSize;
    private Dictionary<Robot, ExoRobotStatus> robobtPositions;
    private RobotProfil standardProfil;

    private static int[] _switchTableDirection;
    private static int[] _switchTableGround;

    public Measure[][]? GetTopo()
    {
        return topo;
    }

    public string GetName()
    {
        return name;
    }

    public void SetNoDelay(bool noDelay)
    {
        this.noDelay = noDelay;
    }

    public ExoPlanet(int level, bool noDelay)
    {
        string[] topoStr = [
            "GSS3PFSGGL",
            "SP34PSFFLL",
            "SG3PMSFLLF",
            "SS23MGSFFG",
            "FGS24SSGFG",
            "FF33GGSGFF"];

        string[] tempStr = [
            "4444555667",
            "4444456788",
            "4445457987",
            "3444356766",
            "1233445555",
            "0124445444"];

        InitFromString(topoStr, tempStr, 0.0F);
        robobtPositions = new Dictionary<Robot, ExoRobotStatus>();
        advanced = level > 0;
        expert = level == 2;
        standardProfil = new RobotProfil();
        this.noDelay = noDelay;
    }

    public ExoPlanet(int level, string filename, bool fromText, bool noDelay)
    {
        Console.WriteLine("filename=" + filename);
        if (!fromText)
        {
            LoadTopoObj(filename);
        }
        else
        {
            LoadTopoFromTextfile(filename);
        }

        robobtPositions = new Dictionary<Robot, ExoRobotStatus>();
        advanced = level > 0;
        expert = level == 2;
        this.noDelay = noDelay;
    }

    private void InitFromString(string[] topoStr, string[] temp, float offset)
    {
        topo = new ExoMeasure[topoStr.Length][];

        for (int y = 0; y < topo.Length; ++y)
        {
            topo[y] = new ExoMeasure[topoStr[y].Length];

            for (int x = 0; x < topoStr[y].Length; ++x)
            {
                Ground g = GroundFromChar(topoStr[y][x]);
                int xDrift = 0;
                int yDrift = 0;

                if (g == Ground.WASSER)
                {
                    switch (topoStr[y][x])
                    {
                        case '1':
                            yDrift = -1;
                            break;

                        case '2':
                            xDrift = 1;
                            break;

                        case '3':
                            yDrift = 1;
                            break;

                        case '4':
                            xDrift = -1;
                            break;

                        case '5':
                            yDrift = -2;
                            break;

                        case '6':
                            xDrift = 2;
                            break;

                        case '7':
                            yDrift = 2;
                            break;

                        case '8':
                            xDrift = -2;
                            break;
                    }
                }

                float t = temp == null ? tempGround[(int)g] : GetTempFromChar(temp[y][x]);

                if (t != -999.9F)
                {
                    t += offset;
                }

                topo[y][x] = new ExoMeasure(g, t, xDrift, yDrift);
            }
        }

        planetSize = new PlanetSize(topoStr[0].Length, topo.Length);
    }

    //TODO: Reimplement this method with JSONSerializer
    [Obsolete("Obsolete")]
    public bool LoadTopoObj(string filename)
    {
        bool rc = false;

        try
        {
            using FileStream fileStream = new(filename, FileMode.Open);
            BinaryFormatter binaryFormatter = new();

            try
            {
                object o = binaryFormatter.Deserialize(fileStream);
                if (o is string)
                {
                    name = (string)o;
                    o = binaryFormatter.Deserialize(fileStream);
                    if (o is PlanetSize)
                    {
                        planetSize = (PlanetSize)o;
                        topo = (ExoMeasure[][])binaryFormatter.Deserialize(fileStream);
                        standardProfil = (RobotProfil)binaryFormatter.Deserialize(fileStream);
                        rc = true;
                    }
                }
            }
            finally
            {
                fileStream.Close();
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
        catch (IOException ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
        catch (SerializationException ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }

        return rc;
    }

    public bool LoadTopoFromTextfile(string filename)
    {
        bool rc = false;

        try
        {
            using StreamReader reader = new(filename);
            try
            {
                name = reader.ReadLine();
                PlanetSize planetSize = PlanetSize.Parse(reader.ReadLine());
                string[] topoStr = new string[planetSize.Height];

                for (int i = 0; i < planetSize.Height; ++i)
                {
                    topoStr[i] = reader.ReadLine();
                }

                string temp = reader.ReadLine();
                string[] tempStr = null;
                float offset = 0.0f;
                string[] token = temp.Split('=');
                if (token[0] == "temp")
                {
                    switch (token[1])
                    {
                        case "auto":
                            break;

                        case "manuell":
                            tempStr = new string[planetSize.Height];
                            for (int i = 0; i < planetSize.Height; ++i)
                            {
                                tempStr[i] = reader.ReadLine();
                            }
                            break;

                        default:
                            offset = float.Parse(token[1]);
                            tempStr = new string[planetSize.Height];
                            for (int i = 0; i < planetSize.Height; ++i)
                            {
                                tempStr[i] = reader.ReadLine();
                            }
                            break;
                    }
                }

                InitFromString(topoStr, tempStr, offset);
                StringBuilder profil = new();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    profil.Append(line + "\n");
                }

                standardProfil = RobotProfil.Parse(profil.ToString());
                rc = true;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
        catch (IOException ex)
        {
            Console.WriteLine(ex.StackTrace);
        }

        return rc;
    }

    private float GetTempFromChar(char c)
    {
        int i = c - '0';
        Random rand = new();
        return i is >= 0 and < 10 ? tempTable[i][0] - (tempTable[i][1] * (float)rand.NextDouble()) : -999.9f;
    }

    private Ground GroundFromChar(char g)
    {
        return g switch
        {
            '1' => Ground.WASSER,
            '2' => Ground.WASSER,
            '3' => Ground.WASSER,
            '4' => Ground.WASSER,
            '5' => Ground.WASSER,
            '6' => Ground.WASSER,
            '7' => Ground.WASSER,
            '8' => Ground.WASSER,
            'W' => Ground.WASSER,
            'F' => Ground.FELS,
            'G' => Ground.GEROELL,
            'L' => Ground.LAVA,
            'M' => Ground.MORAST,
            'P' => Ground.PFLANZEN,
            'S' => Ground.SAND,
            _ => Ground.NICHTS
        };
    }

    public Measure Land(Robot robot, Position position)
    {
        throw new NotImplementedException();
    }

    public Position GetPosition(Robot robot)
    {
        throw new NotImplementedException();
    }

    public Position Move(Robot robot)
    {
        throw new NotImplementedException();
    }

    public Direction? Rotate(Robot robot, Rotation rotation)
    {
        throw new NotImplementedException();
    }

    public Measure Scan(Robot robot)
    {
        throw new NotImplementedException();
    }

    public PlanetSize GetSize()
    {
        throw new NotImplementedException();
    }

    public void Remove(Robot robot)
    {
        throw new NotImplementedException();
    }

    public RobotStatus Charge(Robot robot, int value)
    {
        throw new NotImplementedException();
    }
}