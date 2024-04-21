﻿using System.Text;

namespace ExoplanetGame.Robot.Movement;

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
        Position adjacentPosition;

        switch (Direction)
        {
            case Direction.NORTH:
                adjacentPosition = new Position(X, Y - 1, Direction);
                break;

            case Direction.EAST:
                adjacentPosition = new Position(X + 1, Y, Direction);
                break;

            case Direction.SOUTH:
                adjacentPosition = new Position(X, Y + 1, Direction);
                break;

            case Direction.WEST:
                adjacentPosition = new Position(X - 1, Y, Direction);
                break;

            default:
                adjacentPosition = this;
                break;
        }

        return adjacentPosition;
    }

    public Direction Rotate(Rotation rotation)
    {
        if (rotation == Rotation.LEFT)
        {
            switch (Direction)
            {
                case Direction.NORTH:
                    Direction = Direction.WEST;
                    break;

                case Direction.WEST:
                    Direction = Direction.SOUTH;
                    break;

                case Direction.SOUTH:
                    Direction = Direction.EAST;
                    break;

                case Direction.EAST:
                    Direction = Direction.NORTH;
                    break;
            }
        }
        else
        {
            switch (Direction)
            {
                case Direction.NORTH:
                    Direction = Direction.EAST;
                    break;

                case Direction.EAST:
                    Direction = Direction.SOUTH;
                    break;

                case Direction.SOUTH:
                    Direction = Direction.WEST;
                    break;

                case Direction.WEST:
                    Direction = Direction.NORTH;
                    break;
            }
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