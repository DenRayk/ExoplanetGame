using Exoplanet.exo;

namespace Exoplanet.exoServer;

public class ExoRobotStatus(float temp, int energy, Position pos, RobotProfil rp)
    : RobotStatus
{
    private float energy = energy;
    protected int heaterLevel = 0;
    protected int coolerLevel = 0;
    protected bool mustRotate = false;
    public RobotProfil rp = rp;

    public ExoRobotStatus(float workTemp, int energy, RobotProfil rp)
        : this(workTemp, energy, new Position(0, 0, Direction.SOUTH), rp)
    {
    }

    public ExoRobotStatus(Position pos, RobotProfil rp)
        : this(35.0F, 100, pos, rp)
    {
    }

    public ExoRobotStatus(RobotProfil rp)
        : this(35.0F, 100, rp)
    {
    }

    public override bool Equals(object obj)
    {
        if (obj == this) return true;

        if (obj is Position)
        {
            return pos.Equals(obj);
        }
        ExoRobotStatus ers = (ExoRobotStatus)obj;
        return pos.Equals(ers.GetPos());
    }

    public Position GetPos()
    {
        return pos;
    }

    public void SetPos(Position pos1)
    {
        pos = pos1;
    }

    public void SetWorkTemp(float workTemp)
    {
        temp = workTemp;
    }

    public void SetEnergy(float energy)
    {
        this.energy = energy;
    }

    public float GetEnergyF()
    {
        return energy;
    }

    public float GetWorkTemp()
    {
        return temp;
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