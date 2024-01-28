using Exoplanet.exo;

//Good
namespace Exoplanet.exoServer;

public class ExoRobotStatus : RobotStatus
{
    public float workTemp { get; set; }
    public float energy { get; set; }

    public int heaterLevel { get; set; } = 0;
    public int coolerLevel { get; set; } = 0;
    public bool mustRotate { get; set; } = false;

    public Position pos { get; set; }
    public RobotProfil rp { get; set; }

    public ExoRobotStatus(float workTemp, int energy, Position pos, RobotProfil rp)
    {
        this.workTemp = workTemp;
        this.energy = energy;
        this.rp = rp;
        this.pos = pos;
    }

    public ExoRobotStatus(float workTemp, int energy, RobotProfil rp)
    {
    }

    public ExoRobotStatus(Position pos, RobotProfil rp) : this(35.0F, 100, pos, rp)
    {
    }

    public ExoRobotStatus(RobotProfil rp) : this(35.0F, 100, rp)
    {
    }

    public override bool Equals(object? obj)
    {
        if (obj == this) return true;

        if (obj is Position)
        {
            return pos.Equals(obj);
        }

        var ers = obj as ExoRobotStatus;

        return pos.Equals(ers?.pos);
    }

    public float GetWorkTemp()
    {
        return workTemp;
    }

    public int GetEnergy()
    {
        return (int)energy;
    }

    public string GetMessage()
    {
        return "none";
    }
}