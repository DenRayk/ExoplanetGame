﻿using System.Runtime.Serialization;
using System.Text;

namespace RemoteRobot.exo;

[Serializable]
public class PlanetSize(int width, int height)
{
    public int Width { get; } = width;
    public int Height { get; } = height;

    public PlanetSize() : this(0, 0)
    {
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append("SIZE|");
        sb.Append(Width);
        sb.Append('|');
        sb.Append(Height);
        return sb.ToString();
    }

    public static PlanetSize? Parse(string? s)
    {
        string[] token = s.Trim().Split('|');

        if (token is ["SIZE", _, _])
        {
            int w = int.Parse(token[1]);
            int h = int.Parse(token[2]);
            return new PlanetSize(w, h);
        }

        return null;
    }
}