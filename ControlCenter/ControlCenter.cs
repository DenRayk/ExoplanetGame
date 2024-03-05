using ControlCenter.exo;

namespace ControlCenter;

public class ControlCenter
{
    PlanetMap planetMap;

    public ControlCenter()
    {
    }

    public void init(PlanetSize planetSize)
    {
        planetMap = new PlanetMap(planetSize);
    }

    public void HandleResponse(string response)
    {
    }
}