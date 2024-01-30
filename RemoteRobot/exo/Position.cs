namespace RemoteRobot.exo;

[Serializable]
public class Position
{
    private int x;
    private int y;
    private Direction dir;

    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
        dir = Direction.NORTH;
    }

    public Position(int x, int y, Direction dir)
    {
        this.x = x;
        this.y = y;
        this.dir = dir;
    }

    public Position(Position? pos)
    {
        if (pos != null)
        {
            x = pos.x;
            y = pos.y;
            dir = pos.dir;
        }
        else
        {
            dir = Direction.NORTH;
        }
    }

    public int GetX()
    {
        return x;
    }

    public void SetX(int x)
    {
        this.x = x;
    }

    public int GetY()
    {
        return y;
    }

    public void SetY(int y)
    {
        this.y = y;
    }

    public Direction GetDir()
    {
        return dir;
    }

    public void SetDir(Direction dir)
    {
        this.dir = dir;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Position)obj);
    }

    protected bool Equals(Position other)
    {
        return x == other.x && y == other.y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(x, y);
    }

    public override string ToString()
    {
        System.Text.StringBuilder sb = new();
        sb.Append("POSITION|");
        sb.Append(x);
        sb.Append('|');
        sb.Append(y);
        sb.Append('|');
        sb.Append(dir.ToString());
        return sb.ToString();
    }

    public static Position? Parse(string s)
    {
        string[] token = s.Trim().Split('|');

        if (token is not ["POSITION", _, _, _]) return null;

        int x = int.Parse(token[1]);
        int y = int.Parse(token[2]);
        Direction d = Enum.Parse<Direction>(token[3]);

        return new Position(x, y, d);
    }
}