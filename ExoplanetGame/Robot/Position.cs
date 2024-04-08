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

    public Position GetAdjacentPosition()
    {
        switch (Direction)
        {
            case Direction.NORTH:
                return new Position(X, Y - 1, Direction);

            case Direction.EAST:
                return new Position(X + 1, Y, Direction);

            case Direction.SOUTH:
                return new Position(X, Y + 1, Direction);

            case Direction.WEST:
                return new Position(X - 1, Y, Direction);

            default:
                return this;
        }
    }

    public Direction Rotate(Rotation rotation)
    {
        if (rotation == Rotation.LEFT)
        {
            Direction = Direction switch
            {
                Direction.NORTH => Direction.WEST,
                Direction.WEST => Direction.SOUTH,
                Direction.SOUTH => Direction.EAST,
                Direction.EAST => Direction.NORTH,
                _ => Direction
            };
        }
        else
        {
            Direction = Direction switch
            {
                Direction.NORTH => Direction.EAST,
                Direction.EAST => Direction.SOUTH,
                Direction.SOUTH => Direction.WEST,
                Direction.WEST => Direction.NORTH,
                _ => Direction
            };
        }

        return Direction;
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