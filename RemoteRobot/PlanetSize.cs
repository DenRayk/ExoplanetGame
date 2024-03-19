using System.Text;

namespace RemoteRobot;

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

        if (token is not ["SIZE", _, _]) return null;

        int w = int.Parse(token[1]);
        int h = int.Parse(token[2]);
        return new PlanetSize(w, h);
    }
}