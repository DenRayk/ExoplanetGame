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
            string[] parts = response.Split(':');
            string commandName = parts[0];
            string[] parameters = parts.Length > 1 ? parts[1].Split('|') : Array.Empty<string>();

            switch (commandName)
            {
                case "init":
                    Console.WriteLine($"Initializing with size: width = {parameters[1]}, height = {parameters[2]}");
                    break;

                case "landed":
                    Console.WriteLine($"Landed: ground = {parameters[1]}");
                    break;

                case "scanned":
                    Console.WriteLine($"Scanned: ground = {parameters[1]}");
                    break;

                case "moved":
                    Console.WriteLine($"Moved: position = x:{parameters[1]}, y:{parameters[2]}, direction = {parameters[3]}");
                    break;

                case "rotated":
                    Console.WriteLine($"Rotated: direction = {parameters[0]}");
                    break;

                case "crashed":
                    Crash();
                    break;

                default:
                    Console.WriteLine($"Unknown command: {commandName}");
                    break;
            }
        }
    }
}