using System.Text;

namespace ExoplanetGame.RemoteRobot;

[Serializable]
public class Position
{
    public int X { get; set; }
    public int Y { get; set; }
    public Direction Direction { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
        Direction = Direction.NORTH;
    }

    public Position(int x, int y, Direction direction)
    {
        X = x;
        Y = y;
        Direction = direction;
    }

    protected bool Equals(Position other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Position)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("POSITION|");
        sb.Append(X);
        sb.Append('|');
        sb.Append(Y);
        sb.Append('|');
        sb.Append(Direction.ToString());
        return sb.ToString();
    }

    public static Position Parse(string s)
    {
        string[] token = s.Trim().Split('|');

        if (token is not ["POSITION", _, _, _]) return null;

        int x = int.Parse(token[1]);
        int y = int.Parse(token[2]);
        Direction d = (Direction)Enum.Parse(typeof(Direction), token[3]);
        return new Position(x, y, d);
    }
}