namespace RemoteRobot;

public interface ITcpClientMethods
{
    public void SendData(string message);

    public string ReceiveData();

    public void CloseConnection();
}