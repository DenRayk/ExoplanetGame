namespace RemoteRobot.exo;

public interface RobotStatus
{
    float GetWorkTemp();

    int GetEnergy();

    string GetMessage();
}