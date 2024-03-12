using RemoteRobot.exo;

namespace RemoteRobot
{
    internal class RemoteRobot
    {
        private Position robotPosition;
        private Measure robotMeasure;
        public bool isAlive { get; set; }

        public RemoteRobot()
        {
            isAlive = true;
        }

        public void InitRun(Position position, string status)
        {
            throw new NotImplementedException();
        }

        public void Crash()
        {
            Console.WriteLine("Crashed");
            isAlive = false;
        }

        internal void HandleResponse(string response)
        {
        }
    }
}