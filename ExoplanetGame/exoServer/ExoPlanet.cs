namespace Exoplanet.exoServer;

public class ExoPlanet
{
    private const int OP_LAND = 0;
    private const int OP_MOVE = 1;
    private const int OP_ROTATE = 2;
    private const int OP_SCAN = 3;
    private const int OP_CHARGE = 4;

    private string name = "Default-Planet";
    protected bool advanced;
    private bool expert;
    private float adjustFactor = 4.0F;
    private bool noDelay;
}