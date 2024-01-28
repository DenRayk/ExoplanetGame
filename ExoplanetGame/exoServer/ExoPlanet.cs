using Exoplanet.exo;
using System.Collections.Concurrent;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Exoplanet.exoServer;

public class ExoPlanet : Planet
{
    private const int OP_LAND = 0;
    private const int OP_MOVE = 1;
    private const int OP_ROTATE = 2;
    private const int OP_SCAN = 3;
    private const int OP_CHARGE = 4;

    private string name = "Default-Planet";
    private float adjustFactor = 4.0F;

    private bool noDelay;

    private static float[,] tempTable = {
        { -100.0F, 50.0F },
        { -50.0F, 50.0F },
        { -20.0F, 30.0F },
        { 0.0F, 20.0F },
        { 20.0F, 19.0F },
        { 50.0F, 30.0F },
        { 100.0F, 50.0F },
        { 400.0F, 300.0F },
        { 800.0F, 400.0F },
        { 1100.0F, 300.0F }
    };

    private static float[] tempGround = { -999.9F, 15.0F, 17.0F, 19.0F, 10.0F, 20.0F, 10.0F, 800.0F };

    private ExoMeasure[][] topo;
    private PlanetSize size;

    private ConcurrentDictionary<Robot, ExoRobotStatus> robotPositions;
    private RobotProfil standardProfil;

    public Measure[][] getTopo()
    {
        return topo;
    }

    public ExoPlanet(bool noDelay)
    {
        string[] topoStr = { "GSS3PFSGGL", "SP34PSFFLL", "SG3PMSFLLF", "SS23MGSFFG", "FGS24SSGFG", "FF33GGSGFF" };
        string[] tempStr = { "4444555667", "4444456788", "4445457987", "3444356766", "1233445555", "0124445444" };

        this.InitFromString(topoStr, tempStr, 0.0F);
        robotPositions = new ConcurrentDictionary<Robot, ExoRobotStatus>();
        standardProfil = new RobotProfil();
    }

    public ExoPlanet(string? filename, bool fromText, bool noDelay)
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

        robotPositions = new ConcurrentDictionary<Robot, ExoRobotStatus>();
    }

    [Obsolete("Obsolete")]
    public void SaveTopoObj(string filename)
    {
        try
        {
            using FileStream fileStream = new(filename, FileMode.Create);
            BinaryFormatter formatter = new();
            formatter.Serialize(fileStream, name);
            formatter.Serialize(fileStream, size);
            formatter.Serialize(fileStream, topo);
            formatter.Serialize(fileStream, standardProfil);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }

    [Obsolete("Obsolete")]
    public bool LoadTopoObj(string? filename)
    {
        bool rc = false;

        try
        {
            using FileStream fileStream = new(filename, FileMode.Open);
            BinaryFormatter formatter = new();
            object o = formatter.Deserialize(fileStream);

            if (o is string)
            {
                name = (string)o;
                o = formatter.Deserialize(fileStream);

                if (o is int)
                {
                    size = (PlanetSize)o;
                    topo = (ExoMeasure[][])formatter.Deserialize(fileStream);
                    standardProfil = (RobotProfil)formatter.Deserialize(fileStream);
                    rc = true;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }

        return rc;
    }

    public bool LoadTopoFromTextfile(string? filename)
    {
        bool rc = false;

        try
        {
            using StreamReader sr = new(filename);
            name = sr.ReadLine();
            PlanetSize size = PlanetSize.Parse(sr.ReadLine());
            string[] topoStr = new string[size.Height];

            for (int i = 0; i < size.Height; ++i)
            {
                topoStr[i] = sr.ReadLine();
            }

            string temp = sr.ReadLine();
            string[] tempStr = null;
            float offset = 0.0F;
            string[] token = temp.Split('=');

            if (token[0].Equals("temp"))
            {
                switch (token[1])
                {
                    case "auto":
                        break;

                    case "manuell":
                        tempStr = new string[size.Height];
                        for (int i = 0; i < size.Height; ++i)
                        {
                            tempStr[i] = sr.ReadLine();
                        }
                        break;

                    default:
                        offset = float.Parse(token[1]);
                        tempStr = new string[size.Height];
                        for (int i = 0; i < size.Height; ++i)
                        {
                            tempStr[i] = sr.ReadLine();
                        }
                        break;
                }
            }

            InitFromString(topoStr, tempStr, offset);

            StringBuilder profil = new();
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                profil.AppendLine(line);
            }

            standardProfil = RobotProfil.Parse(profil.ToString());
            rc = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }

        return rc;
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

                float t;

                if (temp == null)
                {
                    t = tempGround[(int)g];
                }
                else
                {
                    t = GetTempFromChar(temp[y][x]);
                }

                if (t != -999.9F)
                {
                    t += offset;
                }

                topo[y][x] = new ExoMeasure(g, t, xDrift, yDrift);
            }
        }

        size = new PlanetSize(topoStr[0].Length, topo.Length);
    }

    private void AdjustNeighbors(int x, int y, float baseValue)
    {
        AdjustPos(x - 1, y, baseValue);
        AdjustPos(x, y - 1, baseValue);
        AdjustPos(x + 1, y, baseValue);
        AdjustPos(x, y + 1, baseValue);
    }

    private void AdjustPos(int nx, int ny, float baseValue)
    {
        if (nx >= 0 && nx < size.Width && ny >= 0 && ny < size.Height)
        {
            ExoMeasure m = topo[ny][nx];
            if (m.getGround() != Ground.LAVA)
            {
                float dt = (baseValue - m.getTemperature()) / adjustFactor;
                m.AddTemp(dt);
            }
        }
    }

    private static float GetTempFromChar(char c)
    {
        int i = c - '0';
        return i is >= 0 and < 10 ? (tempTable[i, 0] - (float)(tempTable[i, 1] * new Random().NextDouble())) : -999.9F;
    }

    public void Exit()
    {
        HashSet<Robot> set = new HashSet<Robot>(robotPositions.Keys);
        foreach (Robot r in set)
        {
            r.Crash();
        }

        robotPositions.Clear();
    }

    public RobotStatus GetInitRobotStatus()
    {
        return new ExoRobotStatus(RobotProfil.Parse(standardProfil.ToString()));
    }

    private Ground GroundFromChar(char g)
    {
        switch (g)
        {
            case 'W':
                return Ground.WASSER;

            case 'F':
                return Ground.FELS;

            case 'G':
                return Ground.GEROELL;

            case 'L':
                return Ground.LAVA;

            case 'M':
                return Ground.MORAST;

            case 'P':
                return Ground.PFLANZEN;

            case 'S':
                return Ground.SAND;

            default:
                return Ground.NICHTS;
        }
    }

    private Measure GetMeasure(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < size.Width && y < size.Height)
        {
            Measure m = topo[y][x];
            return new Measure(m.getGround(), m.getTemperature());
        }

        return new Measure();
    }

    public string getName()
    {
        return name;
    }

    public PlanetSize getSize()
    {
        return size;
    }

    public Position[] GetRobotPos()
    {
        HashSet<Robot> set = new HashSet<Robot>(robotPositions.Keys);
        Position[] pos = new Position[set.Count];
        int i = 0;

        foreach (Robot r in set)
        {
            pos[i++] = robotPositions[r].pos;
        }

        return pos;
    }

    public ExoRobotStatus[] GetRobotStatus()
    {
        HashSet<Robot> set = new HashSet<Robot>(robotPositions.Keys);
        ExoRobotStatus[] ers = new ExoRobotStatus[set.Count];
        int i = 0;

        foreach (Robot r in set)
        {
            ers[i++] = (ExoRobotStatus)robotPositions[r];
        }

        return ers;
    }

    private bool CheckPos(Robot rob, Position pos)
    {
        if (topo[pos.GetY()][pos.GetX()].getGround() == Ground.NICHTS)
        {
            return false;
        }
        else
        {
            HashSet<Robot> set = new HashSet<Robot>(robotPositions.Keys);

            foreach (Robot r in set)
            {
                if (r == rob || !robotPositions[r].pos.Equals(pos))
                {
                    continue;
                }

                return false;
            }

            return true;
        }
    }

    public Measure Land(Robot robot, Position landPos)
    {
        if (!robotPositions.ContainsKey(robot) && CheckPos(robot, landPos))
        {
            robotPositions[robot] = new ExoRobotStatus(landPos, GetNewRobotProfil());
            if (UpdateStatus(robot, 0, true))
            {
                return GetMeasure(landPos.GetX(), landPos.GetY());
            }
        }

        robot.Crash();
        return null;
    }

    public Position Move(Robot robot)
    {
        ExoRobotStatus ers = (ExoRobotStatus)robotPositions[robot];
        if (ers != null)
        {
            if (ers.mustRotate && UpdateStatus(robot, 1, true))
            {
                robot.StatusChanged(new RobotStatusMsg(ers.GetWorkTemp(), ers.GetEnergy(), "STUCK_IN_MUD"));
                return ers.pos;
            }

            Position pos = ers.pos;
            int x = pos.GetX();
            int y = pos.GetY();
            bool move = true;
            bool leftKO = false;
            bool rightKO = false;
            Direction newDir = pos.GetDir();

            int motStat = ers.rp.GetStatus(RobotPart.MOTOR);
            if (motStat <= 50 && new Random().NextDouble() > motStat / 100.0)
            {
                move = false;
                robot.StatusChanged(new RobotStatusMsg(ers.GetWorkTemp(), ers.GetEnergy(), "MOVE_STOP"));
            }

            double r = new Random().NextDouble();
            double limit = ers.rp.GetStatus(RobotPart.ROT_L) / 100.0;
            if (r > limit)
            {
                leftKO = true;
            }

            r = new Random().NextDouble();
            limit = ers.rp.GetStatus(RobotPart.ROT_R) / 100.0;
            if (r > limit)
            {
                rightKO = true;
            }

            if (leftKO ^ rightKO)
            {
                robot.StatusChanged(new RobotStatusMsg(ers.GetWorkTemp(), ers.GetEnergy(), "MOVE_DIRECTION_CHANGED"));
            }
            else
            {
                leftKO = false;
                rightKO = false;
            }

            if (move)
            {
                switch (pos.GetDir())
                {
                    case Direction.NORTH:
                        --y;
                        if (leftKO)
                        {
                            newDir = Direction.WEST;
                        }
                        else if (rightKO)
                        {
                            newDir = Direction.EAST;
                        }
                        break;

                    case Direction.EAST:
                        ++x;
                        if (leftKO)
                        {
                            newDir = Direction.NORTH;
                        }
                        else if (rightKO)
                        {
                            newDir = Direction.SOUTH;
                        }
                        break;

                    case Direction.SOUTH:
                        ++y;
                        if (leftKO)
                        {
                            newDir = Direction.EAST;
                        }
                        else if (rightKO)
                        {
                            newDir = Direction.WEST;
                        }
                        break;

                    case Direction.WEST:
                        --x;
                        if (leftKO)
                        {
                            newDir = Direction.SOUTH;
                        }
                        else if (rightKO)
                        {
                            newDir = Direction.NORTH;
                        }
                        break;
                }
            }

            pos.SetX(x);
            pos.SetY(y);
            pos.SetDir(newDir);

            moveAdvanced(ers);
            x = pos.GetX();
            y = pos.GetY();

            if (x >= 0 && y >= 0 && x < size.Width && y < size.Height && CheckPos(robot, pos))
            {
                if (UpdateStatus(robot, 1, true))
                {
                    return pos;
                }
            }
            else
            {
                robot.Crash();
                robotPositions.Remove(robot, out ers);
            }
        }

        return null;
    }

    private void moveAdvanced(ExoRobotStatus ers)
    {
        Position pos = ers.pos;
        int x = pos.GetX();
        int y = pos.GetY();

        if (x >= 0 && y >= 0 && x < size.Width && y < size.Height)
        {
            if (topo[y][x].getGround() == Ground.WASSER && topo[y][x].getTemperature() > 0.0F)
            {
                int xDrift = topo[y][x].xDrift;
                int yDrift = topo[y][x].yDrift;
                x += xDrift;
                y += yDrift;
                pos.SetX(x);
                pos.SetY(y);
            }
            else if (topo[y][x].getGround() == Ground.MORAST && topo[y][x].getTemperature() > 0.0F)
            {
                ers.mustRotate = true;
            }
        }
    }

    private bool IsWater(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < size.Width && y < size.Height)
        {
            return topo[y][x].getGround() == Ground.WASSER;
        }

        return false;
    }

    public Direction? Rotate(Robot robot, Rotation rotation)
    {
        ExoRobotStatus ers = robotPositions[robot];
        if (ers != null)
        {
            bool rotate = true;

            RobotPart lookup = rotation == Rotation.RIGHT ? RobotPart.ROT_R : RobotPart.ROT_L;
            int rotStat = ers.rp.GetStatus(lookup);
            if (rotStat <= 50 && new Random().NextDouble() > rotStat / 100.0)
            {
                rotate = false;
                robot.StatusChanged(new RobotStatusMsg(ers.GetWorkTemp(), ers.GetEnergy(), "ROTATE_STOP"));
            }

            Position pos = ers.pos;
            Direction dir = pos.GetDir();
            int d = (int)dir;
            if (rotate)
            {
                if (rotation == Rotation.RIGHT)
                {
                    d = (d + 1) % 4;
                }
                else
                {
                    d = (d - 1 + 4) % 4;
                }

                ers.mustRotate = false;
            }

            dir = (Direction)d;
            pos.SetDir(dir);
            ers.pos = pos;
            robotPositions[robot] = ers;
            if (UpdateStatus(robot, 2, true))
            {
                return dir;
            }
        }

        return null;
    }

    public Measure Scan(Robot robot)
    {
        ExoRobotStatus ers = (ExoRobotStatus)robotPositions[robot];
        if (ers != null)
        {
            bool scan = true;
            if (new Random().NextDouble() > ers.rp.GetStatus(RobotPart.SENSOR) / 100.0)
            {
                scan = false;
                robot.StatusChanged(new RobotStatusMsg(ers.GetWorkTemp(), ers.GetEnergy(), "SCAN_STOP"));
            }

            Position pos = ers.pos;
            int x = pos.GetX();
            int y = pos.GetY();

            switch (pos.GetDir())
            {
                case Direction.NORTH:
                    --y;
                    break;

                case Direction.EAST:
                    ++x;
                    break;

                case Direction.SOUTH:
                    ++y;
                    break;

                case Direction.WEST:
                    --x;
                    break;
            }

            if (UpdateStatus(robot, 3, true) && scan)
            {
                return GetMeasure(x, y);
            }
        }

        return new Measure();
    }

    public void Remove(Robot robot)
    {
        Console.WriteLine("remove:" + robot.GetLanderName());
        robotPositions.TryRemove(robot, out ExoRobotStatus ers);
    }

    public Position GetPosition(Robot robot)
    {
        ExoRobotStatus ers = (ExoRobotStatus)robotPositions[robot];
        return ers != null ? new Position(ers.pos) : null;
    }

    public RobotStatus Charge(Robot robot, int duration)
    {
        if (duration > 0)
        {
            ExoRobotStatus ers = (ExoRobotStatus)robotPositions[robot];
            if (ers != null)
            {
                int x = ers.pos.GetX();
                int y = ers.pos.GetY();
                Ground g = topo[y][x].getGround();
                float deltaE = 0.0F;

                switch (g)
                {
                    case Ground.FELS:
                        deltaE = 1.0F;
                        break;

                    case Ground.MORAST:
                    case Ground.PFLANZEN:
                        deltaE = 0.5F;
                        break;
                }

                float e = ers.energy;

                for (int i = 0; i < duration; ++i)
                {
                    e += deltaE;
                    if (e > 100.0F)
                    {
                        e = 100.0F;
                    }

                    ers.energy = e;
                    if (!UpdateStatus(robot, 4, false))
                    {
                        return null;
                    }
                }

                return new RobotStatusMsg(ers.GetWorkTemp(), ers.GetEnergy(), "CHARGE_END");
            }
        }

        return null;
    }

    private bool UpdateStatus(Robot rob, int op, bool delay)
    {
        bool robOK = true;
        ExoRobotStatus ers = (ExoRobotStatus)robotPositions[rob];

        int x = ers.pos.GetX();
        int y = ers.pos.GetY();
        Measure m = GetMeasure(x, y);
        float wt = ers.GetWorkTemp();
        int deltaHeater = 10;
        int deltaCooler = 10;

        deltaHeater = ers.rp.GetStatus(RobotPart.HEATER) / 10;
        deltaCooler = ers.rp.GetStatus(RobotPart.COOLER) / 10;

        wt = wt + (m.getTemperature() - wt) / 2.0F + (float)(ers.heaterLevel * deltaHeater) - (float)(ers.coolerLevel * deltaCooler);
        float e = ers.energy;
        e -= ers.rp.GetAttr(RobotAttribute.evCool) * (float)ers.coolerLevel;
        e -= ers.rp.GetAttr(RobotAttribute.evHeat) * (float)ers.heaterLevel;

        switch (op)
        {
            case 1:
                float factor = 1.0F;

                factor += (100.0F - (float)ers.rp.GetStatus(RobotPart.MOTOR)) / 100.0F;

                e -= ers.rp.GetAttr(RobotAttribute.evMove) * factor;
                break;

            case 2:
                e -= ers.rp.GetAttr(RobotAttribute.evRotate);
                break;
        }

        if (wt < ers.rp.GetAttr(RobotAttribute.tMinWarn))
        {
            ++ers.heaterLevel;

            if (ers.heaterLevel == 1)
            {
                rob.StatusChanged(new RobotStatusMsg(wt, (int)e, "WARN_MIN_TEMP|HEATER_ON"));
            }
        }
        else if (ers.heaterLevel > 0)
        {
            ers.heaterLevel = 0;
            rob.StatusChanged(new RobotStatusMsg(wt, (int)e, "HEATER_OFF"));
        }

        if (wt > ers.rp.GetAttr(RobotAttribute.tMaxWarn))
        {
            ++ers.coolerLevel;

            if (ers.coolerLevel == 1)
            {
                rob.StatusChanged(new RobotStatusMsg(wt, (int)e, "WARN_MAX_TEMP|COOLER_ON"));
            }
        }
        else if (ers.coolerLevel > 0)
        {
            ers.coolerLevel = 0;
            rob.StatusChanged(new RobotStatusMsg(wt, (int)e, "COOLER_OFF"));
        }

        ers.workTemp = wt;

        if (e < ers.rp.GetAttr(RobotAttribute.eMinWarn))
        {
            rob.StatusChanged(new RobotStatusMsg(wt, (int)e, "WARN_LOW_ENERGY"));
        }

        ers.energy = e;

        StringBuilder sb = new();
        bool changed = false;

        foreach (RobotPart part in Enum.GetValues(typeof(RobotPart)))
        {
            Random rand = new();
            double r = rand.NextDouble();
            //float limit = ers.rp.GetAttr(part);
            float limit = 0;

            if (r < limit)
            {
                if (changed)
                {
                    sb.Append("|");
                }

                ers.rp.ReduceStatus(part, limit);
                sb.Append(part.ToString() + "=" + ers.rp.GetStatus(part));
                changed = true;
            }
        }

        if (changed)
        {
            rob.StatusChanged(new RobotStatusMsg(wt, (int)e, sb.ToString()));
        }

        return robOK;
    }

    public void setSize(PlanetSize size)
    {
        this.size = size;
    }

    public int getRobotCount()
    {
        return robotPositions.Count;
    }

    public PlanetSize GetSize()
    {
        return size;
    }

    protected RobotProfil GetNewRobotProfil()
    {
        return RobotProfil.Parse(standardProfil.ToString());
    }

    public void AdjustTemperature(float factor)
    {
        adjustFactor = factor;

        for (var y = 0; y < size.Height; y++)
        {
            for (var x = 0; x < size.Width; x++)
            {
                var m = topo[y][x];

                if (m.getGround() == Ground.LAVA)
                {
                    float t = GetTempFromChar('9');
                    m.SetTemp(t);
                    AdjustNeighbors(x, y, t);
                }
            }
        }
    }

    public void SetNoDelay(bool noDelay)
    {
        this.noDelay = noDelay;
    }
}