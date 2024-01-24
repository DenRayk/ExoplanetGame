using System.Runtime.Serialization;
using System.Text;

namespace Exoplanet.exo;

[Serializable]
public class Size
{
    public int Width { get; }
    public int Height { get; }

    public Size(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public Size() : this(0, 0)
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

    public static Size? Parse(string s)
    {
        string[] token = s.Trim().Split('|');

        if (token is ["SIZE", _, _])
        {
            int w = int.Parse(token[1]);
            int h = int.Parse(token[2]);
            return new Size(w, h);
        }

        return null;
    }
}