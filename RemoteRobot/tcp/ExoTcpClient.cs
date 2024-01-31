using System.Net.Sockets;
using System.Text;

namespace RemoteRobot.tcp;

public class ExoTcpClient
{
    private TcpClient tcpClient;
    private StreamReader reader;
    private StreamWriter writer;
    private Thread receiveThread;
    private bool isRunning = false;

    public event Action<string> MessageReceived;

    public ExoTcpClient(string serverIp, int serverPort)
    {
        tcpClient = new TcpClient();
        tcpClient.Connect(serverIp, serverPort);

        NetworkStream stream = tcpClient.GetStream();
        reader = new StreamReader(stream, Encoding.UTF8);
        writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

        receiveThread = new Thread(new ThreadStart(ReceiveMessages));
        receiveThread.Start();
        isRunning = true;
    }

    public void Send(string message)
    {
        if (tcpClient == null || !tcpClient.Connected)
        {
            Console.WriteLine("Client not connected.");
            return;
        }

        try
        {
            writer.WriteLine(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending message: " + ex.Message);
        }
    }

    private void ReceiveMessages()
    {
        while (isRunning)
        {
            try
            {
                string receivedMessage = reader.ReadLine();

                if (receivedMessage != null)
                {
                    OnMessageReceived(receivedMessage);
                }
            }
            catch (IOException)
            {
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error receiving message: " + ex.Message);
            }
        }

        Stop();
    }

    private void OnMessageReceived(string message)
    {
        MessageReceived?.Invoke(message);
    }

    public void Stop()
    {
        isRunning = false;

        try
        {
            receiveThread?.Join();
            tcpClient?.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error stopping client: " + ex.Message);
        }
    }
}